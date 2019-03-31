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
		public object PreviewData { get; set; }
		public UndoManager UndoManager { get; set; }

		private MemberInfo _propertyInfo;
		private IList _list;
		private HashSet<string> _boundProperties = new HashSet<string>();
		public List<string> Bindings = new List<string>();
		private INotifyPropertyChanged _bindableData;
		private INotifyPropertyChanged _bindablePreviewData;
		private bool _selfUpdating;

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

		public virtual void OnInitialAdd()
		{
		}

		public virtual void ApplyMacro(List<string> values)
		{
		}

		public virtual void BuildMacro(List<string> values)
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

		protected object GetPreviewValue()
		{
			if (_list != null)
			{
				throw new NotImplementedException("Preview data not supported with lists.");
			}
			return _propertyInfo.GetValue(PreviewData);
		}

		protected void SetValue(object value)
		{
			object old = null;

			//quit out if the data hasn't changed
			if (_list != null)
			{
				old = _list[Index];
				if (old == value)
				{
					return;
				}
			}
			else
			{
				old = GetValue();
				if (!((old != null || value != null) && ((old != null && !old.Equals(value)) || (value != null && !value.Equals(old)))))
				{
					return;
				}
			}

			//update the data either directly if no UndoManager is present, or through the UndoManager if one is
			_selfUpdating = true;
			if (UndoManager != null)
			{
				SetValueCommand command = new SetValueCommand(Data, this, old, value);
				UndoManager.Commit(command);
			}
			else
			{
				SetValueDirectly(Data, value);
			}
			_selfUpdating = false;
		}
		internal void SetValueDirectly(object data, object value)
		{
			if (_list != null)
			{
				_list[Index] = value;
			}
			else
			{
				_propertyInfo.SetValue(data, value);
			}
			if (data == Data)
			{
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
			get { return _propertyInfo.GetDataType(); }
		}

		protected virtual void OnDestroy()
		{

		}
		public void Destroy()
		{
			OnDestroy();
			ClearBindableData();
		}

		private void ClearBindableData()
		{
			if (_bindableData != null)
			{
				_bindableData.PropertyChanged -= BindableData_PropertyChanged;
				_bindableData = null;
			}
			if (_bindablePreviewData != null)
			{
				_bindablePreviewData.PropertyChanged -= BindableData_PropertyChanged;
				_bindablePreviewData = null;
			}
		}

		private void SetBindableData(object data, ref INotifyPropertyChanged bindableData)
		{
			INotifyPropertyChanged bindable = data as INotifyPropertyChanged;
			if (bindable != null)
			{
				bindableData = bindable;
				bindable.PropertyChanged += BindableData_PropertyChanged;
			}
		}

		/// <summary>
		/// Sets the current data
		/// </summary>
		/// <param name="data"></param>
		/// <param name="property"></param>
		public void SetData(object data, string property, int index, object context, UndoManager undoManager, object previewData)
		{
			SetBindableData(data, ref _bindableData);
			SetBindableData(previewData, ref _bindablePreviewData);
			UndoManager = undoManager;
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
			PreviewData = previewData;
			UpdateBinding(false);
		}

		/// <summary>
		/// Called when the property behind this control is either first set or updated externally
		/// </summary>
		private void UpdateBinding(bool rebind)
		{
			if (rebind)
			{
				RemoveHandlers();
			}
			OnBoundData();
			AddHandlers();
		}

		protected virtual void RemoveHandlers()
		{

		}
		protected virtual void AddHandlers()
		{

		}

		private void BindableData_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (_selfUpdating || e.PropertyName != Property)
			{
				return;
			}
			UpdateBinding(true);
		}

		protected virtual void OnBoundData()
		{
		}

		public void Rebind(object data, object previewData, object context)
		{
			ClearBindableData();
			Data = data;
			PreviewData = previewData;
			SetBindableData(data, ref _bindableData);
			SetBindableData(previewData, ref _bindablePreviewData);
			Context = context;
			Rebind();
		}

		/// <summary>
		/// Called to indicate that the data has been changed externally and should be reloaded
		/// </summary>
		public void Rebind()
		{
			UpdateBinding(true);
		}

		/// <summary>
		/// Clears the backing field
		/// </summary>
		public virtual void Clear() { }

		/// <summary>
		/// Saves the current data
		/// </summary>
		public virtual void Save() { }

		private class SetValueCommand : ICommand
		{
			private object _data;
			private object _oldValue;
			private object _newValue;
			private PropertyEditControl _ctl;

			public SetValueCommand(object data, PropertyEditControl ctl, object oldValue, object newValue)
			{
				_data = data;
				_ctl = ctl;
				_oldValue = oldValue;
				_newValue = newValue;
			}

			public void Do()
			{
				_ctl.SetValueDirectly(_data, _newValue);
			}

			public void Undo()
			{
				_ctl.SetValueDirectly(_data, _oldValue);
			}
		}
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
			if (obj == null) { return null; }
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
