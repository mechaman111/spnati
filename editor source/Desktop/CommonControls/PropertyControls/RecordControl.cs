using System;
using System.Collections.Generic;
using System.Reflection;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class RecordControl : PropertyEditControl
	{
		private string _filterMethodName;

		public RecordControl()
		{
			InitializeComponent();
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				string key = values[0];
				if (string.IsNullOrEmpty(key))
				{
					key = null;
				}
				recField.RecordKey = key;
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(recField.RecordKey ?? "");
		}

		public override void OnInitialAdd()
		{
			recField.DoSearch();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			RecordSelectAttribute attr = parameters as RecordSelectAttribute;
			recField.RecordType = attr.RecordType;
			recField.AllowCreate = attr.AllowCreate;
			recField.UseAutoComplete = attr.UseAutoComplete;
			_filterMethodName = attr.RecordFilter;
		}

		protected override void OnBoundData()
		{
			if (!string.IsNullOrEmpty(_filterMethodName))
			{
				MethodInfo mi = Data.GetType().GetMethod(_filterMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (mi != null)
				{
					Func<IRecord, bool> filter = (Func<IRecord, bool>)Delegate.CreateDelegate(typeof(Func<IRecord, bool>), Data, mi);
					recField.RecordFilter = filter;
				}
			}

			if (Bindings.Count > 0)
			{
				string contextBinding = Bindings[0];
				object data = GetBindingValue(Bindings[0]);
				recField.RecordContext = data;
			}
			else
			{
				recField.RecordContext = Context;
			}

			if (DataType == typeof(string))
			{
				recField.RecordKey = GetValue()?.ToString();
			}
			else if (typeof(IRecord).IsAssignableFrom(DataType))
			{
				IRecord record = GetValue() as IRecord;
				recField.Record = record;
			}
		}

		protected override void OnBindingUpdated(string property)
		{
			if (Bindings.Count > 0 && property == Bindings[0])
			{
				recField.RecordContext = GetBindingValue(property);
			}
		}

		protected override void OnClear()
		{
			recField.Record = null;
		}

		protected override void OnSave()
		{
			IRecord record = recField.Record;
			if (record == null)
			{
				SetValue(null);
			}
			else
			{
				if (DataType == typeof(string))
				{
					SetValue(record.Key);
				}
				else if (typeof(IRecord).IsAssignableFrom(DataType))
				{
					SetValue(record);
				}
			}
		}

		private void recField_RecordChanged(object sender, RecordEventArgs e)
		{
			Save();
		}
	}

	public class RecordSelectAttribute : EditControlAttribute
	{
		/// <summary>
		/// What type of records to read
		/// </summary>
		public Type RecordType { get; set; }

		/// <summary>
		/// Whether new records should be able to be created
		/// </summary>
		public bool AllowCreate { get; set; }

		/// <summary>
		/// Whether the field should display auto-complete items
		/// </summary>
		public bool UseAutoComplete { get; set; }

		/// <summary>
		/// Name of function on data object for the filter to run on records
		/// </summary>
		public string RecordFilter { get; set; }

		public override Type EditControlType
		{
			get { return typeof(RecordControl); }
		}
	}
}
