using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Set that allows multiple of a key
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CountedSet<T>
	{
		private Dictionary<T, int> _counts = new Dictionary<T, int>();

		public int Count
		{
			get { return _counts.Keys.Count; }
		}

		public IEnumerable<T> Values
		{
			get { return _counts.Keys; }
		}

		public void Add(T key)
		{
			int count;
			_counts.TryGetValue(key, out count);
			count++;
			_counts[key] = count;
		}

		public void Remove(T key)
		{
			int count;
			if (_counts.TryGetValue(key, out count))
			{
				count--;
				if (count == 0)
				{
					_counts.Remove(key);
				}
				else
				{
					_counts[key] = count;
				}
			}
		}

		public int GetCount(T key)
		{
			int count;
			_counts.TryGetValue(key, out count);
			return count;
		}

		public bool Contains(T key)
		{
			return _counts.ContainsKey(key);
		}

		public CountedSet<T> Copy()
		{
			CountedSet<T> copy = new CountedSet<T>();
			foreach (KeyValuePair<T, int> kvp in _counts)
			{
				copy._counts[kvp.Key] = kvp.Value;
			}
			return copy;
		}
	}
}
