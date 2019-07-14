using System;
using System.Globalization;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class RecordEditingControl : RecordField, IDataGridViewEditingControl
	{
		private DataGridView _dataGridView;
		private bool _valueChanged;
		private int _rowIndex;

		public RecordEditingControl() : base()
		{
			TabStop = false;
		}

		public virtual DataGridView EditingControlDataGridView
		{
			get
			{
				return _dataGridView;
			}
			set
			{
				_dataGridView = value;
			}
		}

		public virtual object EditingControlFormattedValue
		{
			get
			{
				return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				string valueStr = value as string;
				if (valueStr != null)
				{
					Text = valueStr;
				}
			}
		}

		public virtual int EditingControlRowIndex
		{
			get
			{
				return _rowIndex;
			}
			set
			{
				_rowIndex = value;
			}
		}

		public virtual bool EditingControlValueChanged
		{
			get
			{
				return _valueChanged;
			}
			set
			{
				_valueChanged = value;
			}
		}

		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return !dataGridViewWantsInputKey;
		}

		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			ValidateField();
			return RecordKey ?? "";
		}

		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				SelectAll();
			}
		}

		protected override void OnRecordChanged()
		{
			NotifyDataGridViewOfValueChange();
		}

		private void NotifyDataGridViewOfValueChange()
		{
			_valueChanged = true;
			_dataGridView.NotifyCurrentCellDirty(true);
		}

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			
		}
	}
}
