using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewButtonCell : DataGridViewButtonCell
	{
		private bool _flat = false;
		public bool Flat
		{
			get { return _flat; }
			set { _flat = value; _colorSet = null; }
		}

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; _colorSet = null; }
		}
		private ColorSet _colorSet;
		private Color _foreColor;
		private bool _mouseInBounds;
		private ButtonState _buttonState;

		private void GetColorSet()
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			if (Flat)
			{
				_colorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Light);
				_foreColor = skin.GetForeColor(FieldType);
			}
			else
			{
				_colorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Normal);
				_foreColor = _colorSet.Normal;
			}
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
			_mouseInBounds = GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
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
			GetColorSet();
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

			// paint borders and background
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

			Graphics g = graphics;
			Rectangle contentBounds = new Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width - 3, cellBounds.Height - 3);
			if (Flat)
			{
				DrawFlat(g, state, contentBounds, formattedValue?.ToString(), cellStyle.BackColor);
			}
			else
			{
				DrawButton(g, state, contentBounds, formattedValue?.ToString());
			}
		}

		private void DrawFlat(Graphics g, VisualState state, Rectangle bounds, string text, Color backColor)
		{
			Color foreColor = _foreColor;

			//back
			if (state == VisualState.Hover)
			{
				backColor = _colorSet.Hover;
				foreColor = _colorSet.ForeColor;
			}
			else if (state == VisualState.Pressed)
			{
				backColor = _colorSet.Pressed;
				foreColor = _colorSet.ForeColor;
			}
			using (SolidBrush br = new SolidBrush(backColor))
			{
				g.FillRectangle(br, bounds);
			}
			//text
			SkinnedButton.DrawContent(g, bounds, foreColor, null, ContentAlignment.MiddleCenter, text, ContentAlignment.MiddleCenter);

			if (state == VisualState.Focused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, bounds);
			}
		}

		private void DrawButton(Graphics g, VisualState state, Rectangle bounds, string text)
		{
			//back
			SolidBrush backColor = _colorSet.GetBrush(state, false, true);
			g.FillRectangle(backColor, bounds);

			//text
			SkinnedButton.DrawContent(g, bounds, _colorSet.ForeColor, null, ContentAlignment.MiddleCenter, text, ContentAlignment.MiddleCenter);

			if (state == VisualState.Focused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, bounds);
			}

			//border
			Pen borderPen = _colorSet.GetBorderPen(state, false, true);
			g.DrawRectangle(borderPen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
		}

		public override object Clone()
		{
			SkinnedDataGridViewButtonCell copy = base.Clone() as SkinnedDataGridViewButtonCell;
			if (copy != null)
			{
				copy.Flat = Flat;
				copy.FieldType = FieldType;
			}
			return copy;
		}
	}
}