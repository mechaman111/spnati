using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	/// <summary>
	/// Base class for controls that can be used in a list manager
	/// </summary>
	[ToolboxItem(false)]
	public partial class PropertyEditControl : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public object Context { get; set; }
		public object Data { get; set; }

		private MemberInfo _propertyInfo;
		private IList _list;
		private HashSet<string> _boundProperties = new HashSet<string>();
		public List<string> Bindings = new List<string>();

		public Type DataType { get { return _propertyInfo.GetDataType(); } }

		public string Property { get; set; }
		public int Index { get; set; }

		public void SetParameters(EditControlAttribute parameters)
		{
			if (parameters.BoundProperties != null)
			{
				foreach (string property in parameters.BoundProperties)
				{
					_boundProperties.Add(property);
					Bindings.Add(property);
				}
			}
			OnSetParameters(parameters);
		}
		protected virtual void OnSetParameters(EditControlAttribute parameters)
		{
		}

		internal void RemoveFromList()
		{
			if (_list != null)
			{
				_list.RemoveAt(Index);
			}
		}

		protected object GetValue()
		{
			if (_list != null)
			{
				return _list[Index];
			}
			return _propertyInfo.GetValue(Data);
		}

		protected void SetValue(object value)
		{
			if (_list != null)
			{
				_list[Index] = value;
				return;
			}
			object old = GetValue();
			if ((old != null || value != null) && ((old != null && !old.Equals(value)) || (value != null && !value.Equals(old))))
			{
				_propertyInfo.SetValue(Data, value);
				NotifyPropertyChanged();
			}
		}

		protected object GetBindingValue(string property)
		{
			MemberInfo sourceMember = Data.GetType().GetMember(property)[0];
			return sourceMember.GetValue(Data);
		}

		protected void NotifyPropertyChanged()
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
		}

		public void OnOtherPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (_boundProperties.Contains(e.PropertyName))
			{
				OnBindingUpdated(e.PropertyName);
			}
		}
		protected virtual void OnBindingUpdated(string property)
		{
		}

		protected Type PropertyType
		{
			get	{ return _propertyInfo.GetDataType(); }
		}

		/// <summary>
		/// Sets the current data
		/// </summary>
		/// <param name="data"></param>
		/// <param name="property"></param>
		public void SetData(object data, string property, int index, object context)
		{
			Context = context;
			Property = property;
			Index = index;
			Type dataType = data.GetType();
			MemberInfo[] matches = dataType.GetMember(Property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (matches.Length > 0)
			{
				_propertyInfo = matches[0];
				if (index >= 0)
				{
					_list = _propertyInfo.GetValue(data) as IList;
				}
			}
			Data = data;
			OnBoundData();
		}
		protected virtual void OnBoundData()
		{
		}

		/// <summary>
		/// Called to indicate that the data has been changed externally and should be reloaded
		/// </summary>
		public void Rebind()
		{
			OnRebindData();
		}
		protected virtual void OnRebindData()
		{
		}

		/// <summary>
		/// Clears the backing field
		/// </summary>
		public virtual void Clear() { }

		/// <summary>
		/// Saves the current data
		/// </summary>
		public virtual void Save() { }
	}

	public static class MemberInfoExtensions
	{
		public static void SetValue(this MemberInfo mi, object obj, object value)
		{
			switch (mi.MemberType)
			{
				case MemberTypes.Field:
					((FieldInfo)mi).SetValue(obj, value);
					break;
				case MemberTypes.Property:
					((PropertyInfo)mi).SetValue(obj, value);
					break;
			}
		}

		public static object GetValue(this MemberInfo mi, object obj)
		{
			switch (mi.MemberType)
			{
				case MemberTypes.Field:
					return ((FieldInfo)mi).GetValue(obj);
				case MemberTypes.Property:
					return ((PropertyInfo)mi).GetValue(obj);
			}
			return null;
		}

		public static Type GetDataType(this MemberInfo mi)
		{
			switch (mi.MemberType)
			{
				case MemberTypes.Field:
					return ((FieldInfo)mi).FieldType;
				case MemberTypes.Property:
					return ((PropertyInfo)mi).PropertyType;
			}
			return null;
		}
	}
}
