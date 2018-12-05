using System;
using System.Collections.Generic;

namespace Desktop.Messaging
{
	/// <summary>
	/// Sends message between objects
	/// </summary>
	public class PostOffice
	{
		private Dictionary<int, Subscription> _subscriptions = new Dictionary<int, Subscription>();

		public Mailbox GetMailbox()
		{
			return new Mailbox(this);
		}

		/// <summary>
		/// Subscribes to a message
		/// </summary>
		/// <typeparam name="T">Argument type the message accepts. Refer to the message's documentation</typeparam>
		/// <param name="message">Message to listen to</param>
		/// <param name="messageHandler">Function to call when the message is received</param>
		public void Subscribe(int message, Action messageHandler)
		{
			Subscription s;
			if (!_subscriptions.TryGetValue(message, out s))
			{
				s = new Subscription();
				_subscriptions[message] = s;
			}
			s.AddSubscriber(messageHandler);
		}

		/// <summary>
		/// Subscribes to a message
		/// </summary>
		/// <typeparam name="T">Argument type the message accepts. Refer to the message's documentation</typeparam>
		/// <param name="message">Message to listen to</param>
		/// <param name="messageHandler">Function to call when the message is received</param>
		public void Subscribe<T>(int message, Action<T> messageHandler)
		{
			Subscription s;
			if (!_subscriptions.TryGetValue(message, out s))
			{
				s = new Subscription();
				_subscriptions[message] = s;
			}
			s.AddSubscriber(messageHandler);
		}

		/// <summary>
		/// Unsubscribes a listener from a message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="messageHandler"></param>
		public void Unsubscribe(int message, object messageHandler)
		{
			Subscription s;
			if (_subscriptions.TryGetValue(message, out s))
			{
				s.RemoveSubscriber(messageHandler);
				if (s.Count == 0) //For local subscriptions, get rid of the whole dictionary if all subscriptions have been removed
				{
					_subscriptions.Remove(message);
				}
			}
		}

		/// <summary>
		/// Sends a message to all listeners
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public bool SendMessage(int message)
		{
			bool sent = false;
			Subscription s;
			if (_subscriptions.TryGetValue(message, out s))
			{
				sent = true;
				s.Send();
			}
			return sent;
		}

		/// <summary>
		/// Sends a message to all listeners
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public bool SendMessage<T>(int message, T args)
		{
			bool sent = false;
			Subscription s;
			if (_subscriptions.TryGetValue(message, out s))
			{
				sent = true;
				s.Send(args);
			}
			return sent;
		}
	}
}