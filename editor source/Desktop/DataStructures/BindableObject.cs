using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.DataStructures
{
	public class BindableObject : INotifyPropertyChanged, ICloneable, IPropertyChangedNotifier, IDisposable
	{
		private Dictionary<string, object> _values = new Dictionary<string, object>();
		private Dictionary<string, NotifyCollectionChangedEventHandler> _collectionHandlers = new Dictionary<string, NotifyCollectionChangedEventHandler>();
		private Dictionary<string, PropertyChangedEventHandler> _propertyHandlers = new Dictionary<string, PropertyChangedEventHandler>();
		private DualKeyDictionary<string, INotifyPropertyChanged, PropertyChangedEventHandler> _listItemHandlers = new DualKeyDictionary<string, INotifyPropertyChanged, PropertyChangedEventHandler>();

		public event PropertyChangedEventHandler PropertyChanged;

		public T Get<T>([CallerMemberName] string propName = null, T defaultValue = default(T))
		{
			if (propName == null)
			{
				throw new ArgumentException("Parameter cannot be null or empty.", "propName");
			}
			object value;
			if (!_values.TryGetValue(propName, out value))
			{
				return defaultValue;
			}
			return (T)value;
		}

		public void Set<T>(T value, [CallerMemberName] string propName = null)
		{
			if (propName == null)
			{
				throw new ArgumentException("Parameter cannot be null or empty.", "propName");
			}

			//see if it's changed
			T old = Get<T>(propName);
			if ((old == null && value != null) || (old != null && !old.Equals(value)))
			{
				RemoveHandlers(old, propName);

				//set the value
				_values[propName] = value;

				AttachHandlers(value, propName);

				NotifyPropertyChanged(propName);
			}
		}

		public void Clear()
		{
			List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>();
			pairs.AddRange(_values);
			foreach (KeyValuePair<string, object> kvp in pairs)
			{
				RemoveHandlers(kvp.Value, kvp.Key);
				_values.Remove(kvp.Key);
				NotifyPropertyChanged(kvp.Key);
			}
			_values.Clear();
		}

		/// <summary>
		/// Clears out a property completely
		/// </summary>
		/// <param name="propName"></param>
		public void Delete(string propName)
		{
			object old;
			if (_values.TryGetValue(propName, out old))
			{
				RemoveHandlers(old, propName);
				_values.Remove(propName);
				NotifyPropertyChanged(propName);
			}
		}

		private void RemoveHandlers<T>(T value, string propName)
		{
			IList list = value as IList;
			if (list != null)
			{
				//for lists, stop tracking changes to current items
				foreach (object o in list)
				{
					UntrackListItem(propName, o);
				}
			}
			if (_collectionHandlers.ContainsKey(propName))
			{
				//remove old CollectionChanged for collections
				INotifyCollectionChanged col = value as INotifyCollectionChanged;
				if (col != null)
				{
					col.CollectionChanged -= _collectionHandlers[propName];
					_collectionHandlers.Remove(propName);
				}
			}
			else
			{
				//attach PropertyChanged for properties
				if (_propertyHandlers.ContainsKey(propName))
				{
					INotifyPropertyChanged property = value as INotifyPropertyChanged;
					if (property != null)
					{
						property.PropertyChanged -= _propertyHandlers[propName];
						_propertyHandlers.Remove(propName);
					}
				}
			}
		}

		private void AttachHandlers<T>(T value, string propName)
		{
			IList list = value as IList;
			if (list != null)
			{
				//for lists, track changes to current items
				foreach (object o in list)
				{
					TrackListItem(propName, o);
				}
			}
			INotifyCollectionChanged collection = value as INotifyCollectionChanged;
			if (collection != null)
			{
				//attach CollectionChanged for collections
				NotifyCollectionChangedEventHandler handler = new NotifyCollectionChangedEventHandler((sender, e) =>
				{
					Collection_CollectionChanged(propName, sender, e);
				});
				collection.CollectionChanged += handler;
				_collectionHandlers[propName] = handler;
			}
			else
			{
				//attach PropertyChanged for properties
				INotifyPropertyChanged property = value as INotifyPropertyChanged;
				if (property != null)
				{
					PropertyChangedEventHandler handler = new PropertyChangedEventHandler((sender, e) =>
					{
						NotifyPropertyChanged(propName);
					});
					property.PropertyChanged += handler;
					_propertyHandlers[propName] = handler;
				}
			}
		}

		private void Collection_CollectionChanged(string propName, object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (object o in e.NewItems)
					{
						TrackListItem(propName, o);
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					foreach (object o in e.OldItems)
					{
						UntrackListItem(propName, o);
					}
					foreach (object o in e.NewItems)
					{
						TrackListItem(propName, o);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (object o in e.OldItems)
					{
						UntrackListItem(propName, o);
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					if (_listItemHandlers.ContainsPrimaryKey(propName))
					{
						List<INotifyPropertyChanged> toRemove = new List<INotifyPropertyChanged>();
						foreach (KeyValuePair<INotifyPropertyChanged, PropertyChangedEventHandler> kvp in _listItemHandlers[propName])
						{
							toRemove.Add(kvp.Key);
						}
						foreach (BindableObject key in toRemove)
						{
							UntrackListItem(propName, key);
						}
						_listItemHandlers.Remove(propName);
					}
					break;
			}
			NotifyPropertyChanged(propName);
		}

		private void TrackListItem(string propName, object o)
		{
			INotifyPropertyChanged bo = o as INotifyPropertyChanged;
			if (bo != null)
			{
				PropertyChangedEventHandler handler = new PropertyChangedEventHandler((changedSender, changedE) =>
				{
					NotifyPropertyChanged(propName);
				});
				bo.PropertyChanged += handler;
				_listItemHandlers.Set(propName, bo, handler);
			}
		}

		private void UntrackListItem(string propName, object o)
		{
			BindableObject bo = o as BindableObject;
			if (bo != null)
			{
				if (_listItemHandlers.ContainsKey(propName, bo))
				{
					bo.PropertyChanged -= _listItemHandlers[propName, bo];
					_listItemHandlers.Remove(propName, bo);
				}
			}
		}

		/// <summary>
		/// Performs a shallow copy of all trackable properties into another data object
		/// </summary>
		/// <param name="dest"></param>
		public void CopyPropertiesInto(BindableObject dest)
		{
			foreach (KeyValuePair<string, object> kvp in _values)
			{
				ICloneable cloneable = kvp.Value as ICloneable;
				if (cloneable != null)
				{
					object copy = cloneable.Clone();
					dest.Set(copy, kvp.Key);
				}
				else
				{
					IList list = kvp.Value as IList;
					if (list != null)
					{
						IList copy = Activator.CreateInstance(list.GetType()) as IList;
						dest.Set(copy, kvp.Key);
						foreach (object item in list)
						{
							ICloneable cloneableItem = item as ICloneable;
							if (cloneableItem != null)
							{
								object copiedItem = cloneableItem.Clone();
								copy.Add(copiedItem);
							}
							else
							{
								copy.Add(item);
							}
						}
					}
					else
					{
						dest.Set(kvp.Value, kvp.Key);
					}
				}
			}
		}

		protected virtual void OnPropertyChanged(string propName)
		{
		}

		public void NotifyPropertyChanged([CallerMemberName] string propName = "")
		{
			OnPropertyChanged(propName);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>
		/// Enumerates bindable values
		/// </summary>
		public IEnumerable<KeyValuePair<string, object>> DataStore
		{
			get
			{
				return _values;
			}
		}

		public bool HasProperty(string propName)
		{
			object val;
			if (!_values.TryGetValue(propName, out val) || string.IsNullOrEmpty(val?.ToString()))
			{
				return false;
			}
			return true;
		}

		public object Clone()
		{
			BindableObject copy = Activator.CreateInstance(GetType()) as BindableObject;
			CopyPropertiesInto(copy);
			return copy;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void OnDispose()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (KeyValuePair<string, object> kvp in _values)
				{
					RemoveHandlers(kvp.Value, kvp.Key);
				}
				OnDispose();
			}
		}
	}
}
