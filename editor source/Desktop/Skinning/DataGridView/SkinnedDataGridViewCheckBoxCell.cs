using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewCheckBoxCell : DataGridViewCheckBoxCell, ISkinControl
	{
		private bool _mouseInBounds;
		private ButtonState _buttonState;

		public void OnUpdateSkin(Skin skin)
		{
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
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (DataGridView == null) { return; }
			if (e.Button == MouseButtons.Left)
			{
				UpdateButtonState(_buttonState & ~ButtonState.Pushed, e.RowIndex);
			}
			base.OnMouseUp(e);
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			// paint borders and background
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

			int x = cellBounds.Left + cellBounds.Width / 2 - SkinnedCheckBox.CheckBoxSize / 2 - 1;
			int y = cellBounds.Top + cellBounds.Height / 2 - SkinnedCheckBox.CheckBoxSize / 2 - 1;

			VisualState state = VisualState.Normal;

			if ((elementState & DataGridViewElementStates.Selected) != 0)
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

			CheckState checkState = CheckState.Unchecked;
			if (formattedValue != null && formattedValue is CheckState)
			{
				checkState = (CheckState)formattedValue;
			}
			else if (formattedValue != null && formattedValue is bool)
			{
				checkState = (bool)formattedValue ? CheckState.Checked : CheckState.Unchecked;
			}

			SkinnedCheckBox.RenderCheckbox(graphics, x, y, SkinnedFieldType.Primary, checkState, state, DataGridView.Enabled);
		}
	}
}
