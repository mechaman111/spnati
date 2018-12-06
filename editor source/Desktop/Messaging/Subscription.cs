using System;
using System.Collections.Generic;
using System.Reflection;

namespace Desktop.Messaging
{
	public class Subscription
	{
		private List<object> _subscribers = new List<object>();
		private List<object> _removals = new List<object>();
		private int _sendingMessage = 0;

		public Subscription()
		{
		}

		public int Count
		{
			get { return _subscribers.Count; }
		}

		/// <summary>
		/// Adds a subscriber
		/// </summary>
		/// <param name="subscriber"></param>
		public void AddSubscriber(object subscriber)
		{
			_subscribers.Add(subscriber);
		}

		/// <summary>
		/// Removes a subscriber
		/// </summary>
		/// <param name="subscriber"></param>
		public void RemoveSubscriber(object subscriber)
		{
			if (_sendingMessage > 0)
			{
				_removals.Add(subscriber);
				return;
			}
			_subscribers.Remove(subscriber);
		}

		/// <summary>
		/// Sends a message to all subscribers
		/// </summary>
		public void Send()
		{
			_sendingMessage++;
			for (int i = 0; i < _subscribers.Count; i++)
			{
				var subscriber = _subscribers[i];
				Action handler = (Action)subscriber;
				handler();
			}
			_sendingMessage--;
			RemoveLapsedSubscribers();
		}

		/// <summary>
		/// Sends this subscription message to all subscribers
		/// </summary>
		public void Send<T>(T args)
		{
			_sendingMessage++;
			for (int i = 0; i < _subscribers.Count; i++)
			{
				var subscriber = _subscribers[i];
				Action<T> handler = subscriber as Action<T>;
				if (handler == null)
				{
					MulticastDelegate del = subscriber as MulticastDelegate;
					ParameterInfo[] parms = del.Method.GetParameters();
					Type parameterType = parms[0].ParameterType;
					object value = (parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null);
					del.DynamicInvoke(value);
				}
				else
				{
					handler(args);
				}
			}
			_sendingMessage--;
			RemoveLapsedSubscribers();
		}

		private void RemoveLapsedSubscribers()
		{
			if (_sendingMessage == 0)
			{
				foreach (object s in _removals)
				{
					RemoveSubscriber(s);
				}
				_removals.Clear();
			}
		}
	}
}