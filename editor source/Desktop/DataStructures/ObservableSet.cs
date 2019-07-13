using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Desktop
{
	public class ObservableSet<T> : ISet<T>, INotifyCollectionChanged, ICloneable
	{
		private HashSet<T> _set = new HashSet<T>();

		public int Count
		{
			get
			{
				return _set.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public bool Add(T item)
		{
			if (!_set.Contains(item))
			{
				_set.Add(item);
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
				return true;
			}
			return false;
		}

		public void Clear()
		{
			_set.Clear();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public object Clone()
		{
			ObservableSet<T> copy = new ObservableSet<T>();
			foreach (T value in _set)
			{
				ICloneable cloneableItem = value as ICloneable;
				if (cloneableItem != null)
				{
					copy.Add((T)cloneableItem.Clone());
				}
				else
				{
					copy.Add(value);
				}
			}
			return copy;
		}

		public bool Contains(T item)
		{
			return _set.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_set.CopyTo(array, arrayIndex);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			_set.ExceptWith(other);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _set.GetEnumerator();
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			_set.IntersectWith(other);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return _set.IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return _set.IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return _set.IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return _set.IsSupersetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return _set.Overlaps(other);
		}

		public bool Remove(T item)
		{
			if (_set.Remove(item))
			{
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
				return true;
			}
			return false;
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return _set.SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			_set.SymmetricExceptWith(other);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			_set.UnionWith(other);
		}

		void ICollection<T>.Add(T item)
		{
			Add(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
