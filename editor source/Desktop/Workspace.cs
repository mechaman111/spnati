using Desktop.Messaging;
using System;
using System.Collections.Generic;

namespace Desktop
{
	public class Workspace : IWorkspace
	{
		public WorkspaceControl Control { get; set; }

		public int Id { get; set; }
		public IRecord Record { get; set; }
		public bool IsDefault { get; set; }

		private PostOffice _postOffice = new PostOffice();

		public IActivity ActiveActivity { get; set; }
		public IActivity ActiveSidebarActivity { get; set; }
		public Dictionary<WorkspacePane, List<IActivity>> Activities = new Dictionary<WorkspacePane, List<IActivity>>();

		public IActivity GetFirstActivity()
		{
			List<IActivity> list = Activities[WorkspacePane.Main];
			return list.Count > 0 ? list[0] : null;
		}

		public IActivity GetFirstSidebarActivity()
		{
			List<IActivity> list = Activities[WorkspacePane.Sidebar];
			return list.Count > 0 ? list[0] : null;
		}

		public IActivity Find<T>() where T : IActivity
		{
			foreach (KeyValuePair<WorkspacePane, List<IActivity>> kvp in Activities)
			{
				foreach (var activity in kvp.Value)
				{
					if (activity.GetType() == typeof(T))
					{
						return activity;
					}
				}
			}
			return null;
		}

		public override string ToString()
		{
			return Caption;
		}

		public virtual string Caption
		{
			get { return Record.Name ?? Record.Key; }
		}

		public void Initialize()
		{
			Activities.Add(WorkspacePane.Main, new List<IActivity>());
			Activities.Add(WorkspacePane.Sidebar, new List<IActivity>());
			OnInitialize();
		}
		protected virtual void OnInitialize()
		{
		}

		public void Activate()
		{
			OnActivate();
			Activated?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnActivate()
		{

		}
		public event EventHandler Activated;

		public virtual bool CanDeactivate(DeactivateReason reason)
		{
			//The activity activity is already asked separately from this, so this should just be workspace specific behavior
			return true;
		}

		public void Deactivate()
		{
			OnDeactivate();
		}
		protected virtual void OnDeactivate()
		{
		}

		public void AddActivity(IActivity activity)
		{
			Activities[activity.Pane].Add(activity);
		}

		public void RemoveActivity(IActivity activity)
		{
			Activities[activity.Pane].Remove(activity);
		}

		public bool CanQuit(CloseReason reason)
		{
			Dictionary<IActivity, bool> saveData = new Dictionary<IActivity, bool>();
			foreach (KeyValuePair<WorkspacePane, List<IActivity>> kvp in Activities)
			{
				foreach (var activity in kvp.Value)
				{
					CloseArgs args = new CloseArgs(reason);
					if (!activity.CanQuit(args))
						return false;
					saveData[activity] = args.SaveData;
				}
			}

			if (!OnCanQuit(reason))
				return false;

			//Save data
			ActiveActivity?.Save();
			ActiveSidebarActivity?.Save();
			//foreach (var kvp in saveData)
			//{
			//	if (kvp.Value)
			//		kvp.Key.Save();
			//}
			return true;
		}
		protected virtual bool OnCanQuit(CloseReason reason)
		{
			return true;
		}

		public void Quit()
		{
			foreach (KeyValuePair<WorkspacePane, List<IActivity>> kvp in Activities)
			{
				foreach (var activity in kvp.Value)
				{
					activity.Quit();
				}
			}
			OnQuit();
		}
		protected virtual void OnQuit()
		{
		}

		public void Destroy()
		{
			Control = null;
			foreach (KeyValuePair<WorkspacePane, List<IActivity>> kvp in Activities)
			{
				foreach (var activity in kvp.Value)
				{
					activity.Destroy();
				}
			}
			Activities.Clear();
		}

		public bool ActivateActivity(IActivity activity)
		{
			return TryActivateActivity(activity);
		}

		internal bool TryActivateActivity(IActivity activity)
		{
			switch (activity.Pane)
			{
				case WorkspacePane.Main:
					if (ActiveActivity != null)
					{
						if (!ActiveActivity.CanDeactivate(new DeactivateArgs(DeactivateReason.SwitchingWorkspaces)))
							return false;
						ActiveActivity.Deactivate();
						ActiveActivity = null;
					}
					ActiveActivity = activity;
					ActiveActivity.Activate();
					return true;
				case WorkspacePane.Sidebar:
					if (ActiveSidebarActivity != null)
					{
						if (!ActiveSidebarActivity.CanDeactivate(new DeactivateArgs(DeactivateReason.SwitchingWorkspaces)))
							return false;
						ActiveSidebarActivity.Deactivate();
						ActiveSidebarActivity = null;
					}
					ActiveSidebarActivity = activity;
					ActiveSidebarActivity.Activate();
					return true;
			}
			return false;
			
		}

		public Mailbox GetMailbox()
		{
			return new Mailbox(_postOffice);
		}

		public void SendMessage(int message)
		{
			_postOffice.SendMessage(message);
		}

		public void SendMessage<T>(int message, T args)
		{
			_postOffice.SendMessage(message, args);
		}

		public bool IsSidebarExpanded
		{
			get { return Control.IsSidebarExpanded; }
		}

		public void ToggleSidebar(bool expanded)
		{
			Control.ToggleSidebar(expanded);
		}
	}
}
