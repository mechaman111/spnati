using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewComboBoxEditingControl : SkinnedComboBox, IDataGridViewEditingControl
	{
		private DataGridView _dataGridView;
		private bool _valueChanged;
		private int _rowIndex;

		public SkinnedDataGridViewComboBoxEditingControl() : base()
		{
			DropDownStyle = ComboBoxStyle.DropDownList;
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
					if (string.Compare(valueStr, this.Text, true, CultureInfo.CurrentCulture) != 0)
					{
						SelectedIndex = -1;
					}
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

		public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			if (dataGridViewCellStyle.BackColor.A < 255)
			{
				// Our ComboBox does not support transparent back colors
				Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
				BackColor = opaqueBackColor;
				_dataGridView.EditingPanel.BackColor = opaqueBackColor;
			}
			else
			{
				BackColor = dataGridViewCellStyle.BackColor;
			}
			ForeColor = dataGridViewCellStyle.ForeColor;
		}

		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			if ((keyData & Keys.KeyCode) == Keys.Down ||
				(keyData & Keys.KeyCode) == Keys.Up ||
				(DroppedDown && ((keyData & Keys.KeyCode) == Keys.Escape) || (keyData & Keys.KeyCode) == Keys.Enter))
			{
				return true;
			}
			return !dataGridViewWantsInputKey;
		}

		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return Text;
		}

		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				SelectAll();
			}
		}

		private void NotifyDataGridViewOfValueChange()
		{
			_valueChanged = true;
			_dataGridView.NotifyCurrentCellDirty(true);
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (SelectedIndex != -1)
			{
				NotifyDataGridViewOfValueChange();
			}
		}
	}
}
