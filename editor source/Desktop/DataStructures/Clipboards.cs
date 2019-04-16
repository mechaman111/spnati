using System;
using System.Collections.Generic;

namespace Desktop
{
	public static class Clipboards
	{
		private static Dictionary<Type, object> _clipboards = new Dictionary<Type, object>();

		public static event EventHandler ClipboardUpdated;

		public static TValue Get<TClipboard, TValue>()
		{
			object obj;
			if (_clipboards.TryGetValue(typeof(TClipboard), out obj))
			{
				return (TValue)obj;
			}
			else
			{
				return default(TValue);
			}
		}

		public static void Set<TClipboard>(object data)
		{
			_clipboards[typeof(TClipboard)] = data;
			ClipboardUpdated?.Invoke(typeof(TClipboard), EventArgs.Empty);
		}

		public static bool Has<TClipboard>()
		{
			return _clipboards.ContainsKey(typeof(TClipboard));
		}

		public static bool Contains<TClipboard, TValue>()
		{
			object obj = Get<TClipboard, object>();
			return obj is TValue;
		}
	}
}
