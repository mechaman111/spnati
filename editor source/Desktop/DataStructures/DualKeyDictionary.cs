using System.Collections;
using System.Collections.Generic;

namespace Desktop
{
	public class DualKeyDictionary<TKey1, TKey2, TValue> : IEnumerable<KeyValuePair<TKey1, Dictionary<TKey2, TValue>>>
	{
		private Dictionary<TKey1, Dictionary<TKey2, TValue>> _innerDictionary = new Dictionary<TKey1, Dictionary<TKey2, TValue>>();

		public IEnumerable<TKey1> Keys
		{
			get { return _innerDictionary.Keys; }
		}

		/// <summary>
		/// Gets a value
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <returns>The value or the value type's default value if not found</returns>
		public TValue Get(TKey1 key1, TKey2 key2)
		{
			Dictionary<TKey2, TValue> inner;
			if (_innerDictionary.TryGetValue(key1, out inner))
			{
				TValue result;
				if (inner.TryGetValue(key2, out result))
				{
					return result;
				}
			}
			return default(TValue);
		}

		public Dictionary<TKey2, TValue> this[TKey1 key]
		{
			get
			{
				Dictionary<TKey2, TValue> inner;
				if (_innerDictionary.TryGetValue(key, out inner))
				{
					return inner;
				}
				return null;
			}
		}

		public TValue this[TKey1 key1, TKey2 key2]
		{
			get
			{
				return Get(key1, key2);
			}
		}

		/// <summary>
		/// Sets a value in the dictionary
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="value"></param>
		public void Set(TKey1 key1, TKey2 key2, TValue value)
		{
			Dictionary<TKey2, TValue> inner;
			if (!_innerDictionary.TryGetValue(key1, out inner))
			{
				inner = new Dictionary<TKey2, TValue>();
				_innerDictionary[key1] = inner;
			}
			inner[key2] = value;
		}

		public void Clear()
		{
			_innerDictionary.Clear();
		}

		/// <summary>
		/// Removes all items under the primary key
		/// </summary>
		/// <param name="key1"></param>
		public void Remove(TKey1 key1)
		{
			_innerDictionary.Remove(key1);
		}

		/// <summary>
		/// Removes an item.
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		public void Remove(TKey1 key1, TKey2 key2)
		{
			Dictionary<TKey2, TValue> inner;
			if (!_innerDictionary.TryGetValue(key1, out inner))
			{
				return;
			}
			inner.Remove(key2);

			//Remove primary key too if this was the last secondary key
			if (inner.Count == 0)
			{
				_innerDictionary.Remove(key1);
			}
		}

		/// <summary>
		/// Checks whether a dual key pair is in the dictionary
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <returns></returns>
		public bool ContainsKey(TKey1 key1, TKey2 key2)
		{
			Dictionary<TKey2, TValue> inner;
			if (!_innerDictionary.TryGetValue(key1, out inner))
			{
				return false;
			}
			return inner.ContainsKey(key2);
		}

		/// <summary>
		/// Gets whether a primary key has any values
		/// </summary>
		/// <param name="key1"></param>
		/// <returns></returns>
		public bool ContainsPrimaryKey(TKey1 key1)
		{
			return _innerDictionary.ContainsKey(key1);
		}

		/// <summary>
		/// Gets a dictionary of TKey2,TValue pairs for a given primary key</TKey2>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public bool TryGetValue(TKey1 key, out Dictionary<TKey2, TValue> values)
		{
			return _innerDictionary.TryGetValue(key, out values);
		}

		public IEnumerator<KeyValuePair<TKey1, Dictionary<TKey2, TValue>>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey1, Dictionary<TKey2, TValue>>>)_innerDictionary).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey1, Dictionary<TKey2, TValue>>>)_innerDictionary).GetEnumerator();
		}

		/// <summary>
		/// Gets the number of primary keys in the dictionary
		/// </summary>
		public int Count
		{
			get
			{
				return _innerDictionary.Count;
			}
		}
	}
}
