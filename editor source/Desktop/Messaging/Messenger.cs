using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	/// <summary>
	/// Handles delayed method invokations as opposed to the PostOffice which works indirectly and immediately
	/// </summary>
	public class Messenger : IDisposable
	{
		private Timer _timer;
		private int _nextId;
		private List<Message> _messages = new List<Message>();

		public Messenger()
		{
			_timer = new Timer();
			_timer.Interval = 100;
			_timer.Tick += _timer_Tick;
		}

		/// <summary>
		/// Invokes an action after a delay
		/// </summary>
		/// <param name="action">Action to run</param>
		/// <param name="delayMs">Number of milliseconds to delay</param>
		/// <returns>Message ID for canceling later</returns>
		public int Send(Action action, int delayMs = 0)
		{
			Message message = new Message(action, delayMs, ++_nextId);
			_messages.Add(message);
			if (!_timer.Enabled)
			{
				_timer.Start();
			}
			return message.Id;
		}

		/// <summary>
		/// Cancels the message with the given ID
		/// </summary>
		/// <param name="id"></param>
		public void Cancel(int id)
		{
			for (int i = _messages.Count - 1; i >= 0; i--)
			{
				if (_messages[i].Id == id)
				{
					_messages.RemoveAt(i);
					if (_messages.Count == 0)
					{
						_timer.Stop();
					}
					return;
				}
			}
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			int elapsed = _timer.Interval; //note: this isn't a very accurate way of measuring true elapsed time, but we don't care about precise values here
			for (int i = 0; i < _messages.Count; i++)
			{
				Message message = _messages[i];
				message.Wait -= elapsed;
				if (message.Wait <= 0)
				{
					_messages.RemoveAt(i);
					i--;
					message.Action();
				}
			}
			if (_messages.Count == 0)
			{
				_timer.Stop();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_timer?.Dispose();
			}
		}

		private class Message
		{
			public int Id;
			public Action Action;
			public int Wait;

			public Message(Action action, int delay, int id)
			{
				Wait = delay;
				Action = action;
				Id = id;
			}
		}
	}
}
