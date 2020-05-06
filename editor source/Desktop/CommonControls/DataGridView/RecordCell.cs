using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class RecordCell : DataGridViewTextBoxCell
	{
		private RecordEditingControl EditingControl { get; set; }

		private Type _recordType;
		public Type RecordType
		{
			get { return _recordType; }
			set
			{
				if (_recordType != value)
				{
					_recordType = value;
					if (OwnsEditingControl(RowIndex))
					{
						EditingControl.RecordType = value;
					}
				}
			}
		}

		private Func<IRecord, bool> _filter;
		public Func<IRecord, bool> RecordFilter
		{
			get { return _filter; }
			set
			{
				if (_filter != value)
				{
					_filter = value;
					if (OwnsEditingControl(RowIndex))
					{
						EditingControl.RecordFilter = value;
					}
				}
			}
		}

		private bool _allowsNew;
		public bool AllowsNew
		{
			get { return _allowsNew; }
			set
			{
				if (_allowsNew != value)
				{
					_allowsNew = value;
					if (OwnsEditingControl(RowIndex))
					{
						EditingControl.AllowCreate = value;
					}
				}
			}
		}

		private bool OwnsEditingControl(int rowIndex)
		{
			return rowIndex != -1 && EditingControl != null && rowIndex == EditingControl.EditingControlRowIndex;
		}

		public override Type EditType
		{
			get { return typeof(RecordEditingControl); }
		}

		public override object Clone()
		{
			RecordCell copy = base.Clone() as RecordCell;
			if (copy != null)
			{
				copy.RecordType = RecordType;
				copy.RecordFilter = RecordFilter;
				copy.AllowsNew = AllowsNew;
			}
			return copy;
		}

		public override void DetachEditingControl()
		{
			if (EditingControl != null)
			{
				EditingControl = null;
			}
			base.DetachEditingControl();
		}

		public override Type FormattedValueType
		{
			get { return typeof(string); }
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			RecordEditingControl control = DataGridView.EditingControl as RecordEditingControl;
			if (control != null)
			{
				control.AllowCreate = AllowsNew;
				control.RecordType = RecordType;
				control.RecordFilter = RecordFilter;
				control.RecordKey = initialFormattedValue?.ToString();
				EditingControl = control;
			}
		}
	}
}
