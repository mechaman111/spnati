using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class RecordListControl : PropertyEditControl
	{
		private string _filterMethodName;

		public RecordListControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			RecordListAttribute attr = parameters as RecordListAttribute;
			RecordColumn col = grid.Columns[0] as RecordColumn;
			col.RecordType = attr.RecordType;
			col.AllowsNew = attr.AllowCreate;
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
					RecordColumn col = grid.Columns[0] as RecordColumn;
					col.RecordFilter = filter;
				}
			}

			if (PropertyType == typeof(string))
			{
				string list = GetValue()?.ToString() ?? "";
				string[] pieces = list.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string piece in pieces)
				{
					DataGridViewRow row = grid.Rows[grid.Rows.Add()];
					RecordCell cell = row.Cells[0] as RecordCell;
					cell.Value = piece;
				}
			}
		}

		protected override void AddHandlers()
		{
			grid.CellValueChanged += Grid_CellValueChanged;
		}

		protected override void RemoveHandlers()
		{
			grid.CellValueChanged -= Grid_CellValueChanged;
		}

		private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			List<string> records = new List<string>();
			foreach (DataGridViewRow row in grid.Rows)
			{
				string key = row.Cells[0]?.Value?.ToString();
				if (string.IsNullOrEmpty(key))
				{
					continue;
				}
				records.Add(key);
			}

			if (PropertyType == typeof(string))
			{
				string data = string.Join(" ", records);
				SetValue(data);
			}
		}
	}

	public class RecordListAttribute : EditControlAttribute
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
		/// Name of function on data object for the filter to run on records
		/// </summary>
		public string RecordFilter { get; set; }

		public override Type EditControlType
		{
			get { return typeof(RecordListControl); }
		}
	}
}
