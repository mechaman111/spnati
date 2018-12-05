using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor
{
	public class BackgroundQueue
	{
		private Task previousTask = Task.FromResult(true);
		private object key = new object();
		private int _id;
		private List<WorkerTask> _tasks = new List<WorkerTask>();
		private Dictionary<int, object> _requesters = new Dictionary<int, object>();
		private Dictionary<int, WorkerTask> _hashToTask = new Dictionary<int, WorkerTask>();

		public Task<T> QueueTask<T>(Func<T> work, int priority, object requester, int hashCode)
		{
			lock (key)
			{
				Task<T> task = new Task<T>(work);
				WorkerTask workerTask = new WorkerTask(task, priority, ++_id, hashCode, requester);
				_tasks.Add(workerTask);
				if (requester != null)
				{
					_requesters.Add(task.Id, requester);

					//lower the priority of any other pre-existing requests from the same requester
					//the assumption is that if the same object is asking for multiple things, it's because there's a big backlog in the queue
					//and the only image it really cares about is the newest request
					for (int i = 0; i < _tasks.Count; i++)
					{
						var existingWorker = _tasks[i];
						Task existingTask = existingWorker.Task;
						if (existingTask == task)
							continue;
						object existingRequester;
						if (_requesters.TryGetValue(existingTask.Id, out existingRequester) && existingRequester == requester)
						{
							existingWorker.Priority += 100;
						}
					}
				}
				_tasks.Sort();
				var nextTask = previousTask.ContinueWith(t => ProcessQueue(), CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
				previousTask = nextTask;
				return task;
			}
		}

		/// <summary>
		/// Changes the priority of all queued requests belonging to a particular requester
		/// </summary>
		/// <param name="requester"></param>
		/// <param name="priority"></param>
		public void ChangePriority(object requester, int priority)
		{
			lock (key)
			{
				bool changed = false;
				for (int i = 0; i < _tasks.Count; i++)
				{
					WorkerTask task = _tasks[i];
					if (task.Requester != null && task.Requester.Equals(requester))
					{
						changed = true;
						task.Priority = priority;
					}
				}
				if (changed)
					_tasks.Sort();
			}
		}

		private class WorkerTask : IComparable<WorkerTask>
		{
			public int Priority;
			public int Id;
			public Task Task;
			public int HashCode;
			public object Requester;

			public WorkerTask(Task task, int priority, int id, int hashCode, object requester)
			{
				Task = task;
				Priority = priority;
				Id = id;
				HashCode = hashCode;
				Requester = requester;
			}

			public override int GetHashCode()
			{
				if (HashCode == 0)
					return base.GetHashCode();
				return HashCode;
			}

			public override bool Equals(object obj)
			{
				return base.Equals(obj);
			}

			public int CompareTo(WorkerTask other)
			{
				//Order is low->high priority, with LIFO for same priority
				int compare = Priority.CompareTo(other.Priority);
				if (compare == 0)
					compare = Id.CompareTo(other.Id);
				return compare;
			}
		}

		private void ProcessQueue()
		{
			WorkerTask next = null;
			lock (key)
			{
				if (_tasks.Count == 0)
				{
					return;
				}
				next = _tasks[0];
				_tasks.RemoveAt(0);
				_requesters.Remove(next.Task.Id);
				_hashToTask.Remove(next.HashCode);
			}
			Task task = next.Task;
			task.Start();
			task.Wait();
			return;
		}
	}
}
