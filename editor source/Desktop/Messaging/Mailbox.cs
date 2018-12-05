using System;
using System.Collections.Generic;

namespace Desktop.Messaging
{
	public class Mailbox
	{
		private PostOffice _office;
		private Dictionary<int, object> _subscriptions = new Dictionary<int, object>();

		public Mailbox(PostOffice office)
		{
			_office = office;
		}

		public void SendMessage<T>(int message, T args)
		{
			_office.SendMessage(message, args);
		}

		/// <summary>
		/// Subscribes to a global message
		/// </summary>
		/// <param name="message">Message to subscribe to</param>
		/// <param name="handler">Function to call when receiving the message</param>
		public void Subscribe(int message, Action handler)
		{
			_office.Subscribe(message, handler);
			_subscriptions.Add(message, handler);
		}

		/// <summary>
		/// Subscribes to a global message
		/// </summary>
		/// <param name="message">Message to subscribe to</param>
		/// <param name="handler">Function to call when receiving the message</param>
		public void Subscribe<T>(int message, Action<T> handler)
		{
			_office.Subscribe(message, handler);
			_subscriptions.Add(message, handler);
		}

		/// <summary>
		/// Unsubscribes from all subscriptions
		/// </summary>
		public void UnsubscribeAll()
		{
			foreach (var kvp in _subscriptions)
			{
				_office.Unsubscribe(kvp.Key, kvp.Value);
			}
			_subscriptions.Clear();
		}

		public void Destroy()
		{
			UnsubscribeAll();
		}
	}
}
