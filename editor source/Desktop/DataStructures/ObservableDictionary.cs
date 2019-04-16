using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Desktop
{
	public sealed class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, ICloneable
	{
		private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

		public TValue this[TKey key]
		{
			get
			{
				return _dict[key];
			}

			set
			{
				List<TValue> newItems = new List<TValue>();
				newItems.Add(value);
				NotifyCollectionChangedEventArgs args;
				if (_dict.ContainsKey(key))
				{
					List<TValue> oldItems = new List<TValue>();
					oldItems.Add(_dict[key]);
					args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems);
				}
				else
				{
					args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems);
				}
				_dict[key] = value;
				NotifyCollectionChanged(args);
			}
		}

		public int Count
		{
			get
			{
				return _dict.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public ICollection<TKey> Keys
		{
			get
			{
				return _dict.Keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				return _dict.Values;
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		private void NotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			CollectionChanged?.Invoke(this, args);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}

		public void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public void Clear()
		{
			List<TValue> removedItems = new List<TValue>();
			removedItems.AddRange(_dict.Values);
			NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, removedItems);
			_dict.Clear();
			NotifyCollectionChanged(args);
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return _dict.Contains(item);
		}

		public bool ContainsKey(TKey key)
		{
			if (key == null) { return false; }
			return _dict.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return Remove(item.Key);
		}

		public bool Remove(TKey key)
		{
			if (_dict.ContainsKey(key))
			{
				List<TValue> oldItems = new List<TValue>();
				oldItems.Add(_dict[key]);
				_dict.Remove(key);
				NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
				return true;
			}
			return false;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dict.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		public object Clone()
		{
			ObservableDictionary<TKey, TValue> copy = new ObservableDictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> kvp in this)
			{
				ICloneable cloneableItem = kvp.Value as ICloneable;
				if (cloneableItem != null)
				{
					copy[kvp.Key] = (TValue)cloneableItem.Clone();
				}
				else
				{
					copy[kvp.Key] = kvp.Value;
				}
			}
			return copy;
		}
	}
}
