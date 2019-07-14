using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

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

		public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				list.Add(item);
			}
		}

		public static void Sort<T>(this ObservableCollection<T> list)
		{
			List<T> temp = new List<T>();
			temp.AddRange(list);
			temp.Sort();
			list.Clear();
			list.AddRange(temp);
		}

		/// <summary>
		/// Converts a color to #RRGGBB
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static string ToHexValue(this Color color)
		{
			return "#" + color.R.ToString("X2") +
					   color.G.ToString("X2") +
					   color.B.ToString("X2");
		}
	}
}
