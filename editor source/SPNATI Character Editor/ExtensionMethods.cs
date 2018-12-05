using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public static class ExtensionMethods
	{
		private static Random _random = new Random();

		public static TValue GetOrAddDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultFunc)
		{
			TValue existing;
			if (key == null)
			{
				return defaultFunc();
			}
			if (!dictionary.TryGetValue(key, out existing))
			{
				existing = defaultFunc();
				dictionary[key] = existing;
			}
			return existing;
		}

		public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue existing;
			if (key == null || !dictionary.TryGetValue(key, out existing))
			{
				return default(TValue);
			}
			return existing;
		}

		public static T GetRandom<T>(this List<T> list)
		{
			int index = _random.Next(list.Count);
			return list[index];
		}
	}
}
