using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewComboBoxCell : DataGridViewTextBoxCell, ISkinnedComboBox
	{
		private bool _mouseInBounds;
		private ButtonState _buttonState;
		private bool _needToShowDropdown;
		private Timer _showTimer = new Timer();

		public SkinnedDataGridViewComboBoxCell()
		{
			Items = new SkinnedComboBox.ObjectCollection(this);
			_showTimer.Interval = 1;
			_showTimer.Tick += DataGridView_EditingControlShowing;
		}

		private bool _autoComplete;
		[DefaultValue(true)]
		public bool AutoComplete
		{
			get { return _autoComplete; }
			set
			{
				if (value != _autoComplete)
				{
					_autoComplete = value;
				}
				if (OwnsEditingComboBox(RowIndex))
				{
					if (value)
					{
						EditingComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
						EditingComboBox.AutoCompleteMode = AutoCompleteMode.Append;
					}
					else
					{
						EditingComboBox.AutoCompleteMode = AutoCompleteMode.None;
						EditingComboBox.AutoCompleteSource = AutoCompleteSource.None;
					}
				}
			}
		}

		private SkinnedDataGridViewComboBoxEditingControl EditingComboBox { get; set; }

		public override Type EditType
		{
			get
			{
				return typeof(SkinnedDataGridViewComboBoxEditingControl);
			}
		}

		private SkinnedComboBox.ObjectCollection _items;
		public SkinnedComboBox.ObjectCollection Items
		{
			get { return _items; }
			set
			{
				_items = value;
				if (OwnsEditingComboBox(RowIndex))
				{
					EditingComboBox.Items.Clear();
					EditingComboBox.Items.AddRange(_items);
				}
			}
		}

		private string _displayMember;
		public string DisplayMember
		{
			get { return _displayMember; }
			set
			{
				_displayMember = value;
				if (OwnsEditingComboBox(RowIndex))
				{
					EditingComboBox.DisplayMember = value;
				}
			}
		}

		private bool OwnsEditingComboBox(int rowIndex)
		{
			return rowIndex != -1 && EditingComboBox != null && rowIndex == EditingComboBox.EditingControlRowIndex;
		}

		public void PreItemChange()
		{

		}

		public void SortItems()
		{

		}

		public void PostItemChange()
		{

		}

		public bool Sorted
		{
			get { return _items.Sorted; }
			set
			{
				_items.Sorted = value;
				if (OwnsEditingComboBox(RowIndex))
				{
					EditingComboBox.Sorted = value;
				}
			}
		}

		public override object Clone()
		{
			SkinnedDataGridViewComboBoxCell copy = base.Clone() as SkinnedDataGridViewComboBoxCell;
			if (copy != null)
			{
				copy.AutoComplete = AutoComplete;
				copy.DisplayMember = DisplayMember;
				copy.Sorted = Sorted;
				copy.Items.AddRange(Items);
			}
			return copy;
		}

		private void UpdateButtonState(ButtonState state, int rowIndex)
		{
			if (_buttonState != state)
			{
				_buttonState = state;
				DataGridView.InvalidateCell(ColumnIndex, rowIndex);
			}
		}

		protected override void OnMouseLeave(int rowIndex)
		{
			if (DataGridView == null) { return; }

			if (_mouseInBounds)
			{
				_mouseInBounds = false;
				if (ColumnIndex >= 0 && rowIndex >= 0)
				{
					DataGridView.InvalidateCell(ColumnIndex, rowIndex);
				}
			}
			if ((_buttonState & ButtonState.Pushed) != 0)
			{
				UpdateButtonState(_buttonState & ~ButtonState.Pushed, rowIndex);
			}
			base.OnMouseLeave(rowIndex);
		}

		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (DataGridView == null) { return; }

			bool oldInBounds = _mouseInBounds;
			_mouseInBounds = true;
			if (oldInBounds != _mouseInBounds)
			{
				DataGridView.InvalidateCell(ColumnIndex, e.RowIndex);
			}

			base.OnMouseMove(e);
		}

		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (DataGridView == null) { return; }

			if (_buttonState != ButtonState.Normal)
			{
				UpdateButtonState(ButtonState.Normal, rowIndex);
			}

			base.OnLeave(rowIndex, throughMouseClick);
		}

		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (DataGridView == null) { return; }

			if (e.Button == MouseButtons.Left && _mouseInBounds)
			{
				UpdateButtonState(_buttonState | ButtonState.Pushed, e.RowIndex);
			}
			_needToShowDropdown = true;
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (DataGridView == null) { return; }
			_needToShowDropdown = false;
			if (e.Button == MouseButtons.Left)
			{
				UpdateButtonState(_buttonState & ~ButtonState.Pushed, e.RowIndex);
			}
			base.OnMouseUp(e);
		}

		public override void DetachEditingControl()
		{
			if (EditingComboBox != null)
			{
				//Remove dropdown event handler, whatever it was
				EditingComboBox = null;
			}
			base.DetachEditingControl();
		}

		public override Type FormattedValueType
		{
			get { return typeof(string); }
		}

		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (value == null)
			{
				return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			}
			object displayValue = value;
			return displayValue;
		}

		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (formattedValue == null)
			{
				return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
			}
			string value = formattedValue.ToString();
			foreach (object o in Items)
			{
				if (o.ToString() == value)
				{
					return o;
				}
			}
			return formattedValue;
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			SkinnedDataGridViewComboBoxEditingControl comboBox = DataGridView.EditingControl as SkinnedDataGridViewComboBoxEditingControl;
			if (comboBox != null)
			{
				comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				comboBox.Items.Clear();
				comboBox.Items.AddRange(Items);
				comboBox.Sorted = Sorted;
				comboBox.FieldType = SkinnedFieldType.Surface;
				comboBox.SelectedItem = initialFormattedValue;
				comboBox.DisplayMember = DisplayMember;
				//attach to dropdown event for resizing?

				EditingComboBox = comboBox;
				Rectangle rectBottomSection = DataGridView.GetCellDisplayRectangle(ColumnIndex, rowIndex, true);
				rectBottomSection.Y += 21;
				rectBottomSection.Height -= 21;
				DataGridView.Invalidate(rectBottomSection);

				if (_needToShowDropdown)
				{
					_showTimer.Start();
				}
			}
		}

		private void DataGridView_EditingControlShowing(object sender, EventArgs e)
		{
			_showTimer.Stop();
			EditingComboBox.ShowDropDown();
			_needToShowDropdown = false;
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			// paint borders and background
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

			Graphics g = graphics;
			Rectangle contentBounds = new Rectangle(cellBounds.X + 1, cellBounds.Y, cellBounds.Width - 2, cellBounds.Height - 1);

			bool focused = (cellState & DataGridViewElementStates.Selected) != 0;

			VisualState state = VisualState.Normal;

			if ((cellState & DataGridViewElementStates.Selected) != 0)
			{
				state = VisualState.Focused;
			}
			if (_mouseInBounds)
			{
				state = VisualState.Hover;
			}
			if (_buttonState == ButtonState.Pushed)
			{
				state = VisualState.Pressed;
			}

			string formatted = SkinnedComboBox.GetFormattedValue(formattedValue, DisplayMember);
			SkinnedComboBox.RenderComboBox(g, contentBounds, formatted, false, focused, DataGridView.Enabled, ComboBoxStyle.DropDownList, SkinnedFieldType.Surface, state);
		}
	}
}