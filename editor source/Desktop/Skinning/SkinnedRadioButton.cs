using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedRadioButton : RadioButton, ISkinControl
	{
		public VisualState MouseState { get; private set; }

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; OnUpdateSkin(SkinManager.Instance.CurrentSkin); Invalidate(); }
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (DesignMode) { return; }

			MouseEnter += MouseEnterEvent;
			MouseLeave += MouseLeaveEvent;
			MouseDown += MouseDownEvent;
			MouseUp += MouseUpEvent;
		}

		private void MouseEnterEvent(object sender, EventArgs e)
		{
			MouseState = VisualState.Hover;
			Invalidate();
		}

		private void MouseLeaveEvent(object sender, EventArgs e)
		{
			MouseState = VisualState.Normal;
			Invalidate();
		}

		private void MouseDownEvent(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				MouseState = VisualState.Pressed;
				Invalidate();
			}
		}

		private void MouseUpEvent(object sender, MouseEventArgs e)
		{
			MouseState = VisualState.Hover;
			Invalidate();
		}

		public void OnUpdateSkin(Skin skin)
		{
			Font = Skin.TextFont;
		}

		private const int RadioSize = 12;

		protected override void OnPaint(PaintEventArgs pevent)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			Graphics g = pevent.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			g.Clear(DesignMode ? Parent.BackColor : this.GetSkinnedPanelBackColor());

			if (Appearance == Appearance.Normal)
			{
				ColorSet set = skin.GetWidgetColorSet(FieldType);
				using (Brush boxColor = new SolidBrush(Enabled ? skin.FieldBackColor : skin.FieldDisabledBackColor))
				{
					int start = pevent.ClipRectangle.Y + pevent.ClipRectangle.Height / 2 - RadioSize / 2;
					Rectangle rect = new Rectangle(0, start, RadioSize, RadioSize);
					g.FillEllipse(boxColor, rect);
					Pen borderPen = set.GetBorderPen(MouseState, Focused, Enabled);
					g.DrawEllipse(borderPen, rect);

					if (Checked)
					{
						Brush checkBrush = set.GetBrush(MouseState, Focused, Enabled);
						g.FillEllipse(checkBrush, rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6);
					}
				}

				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

				Color foreColor = DesignMode ? ForeColor : this.GetSkinnedPanelForeColor();
				int left = 4 + RadioSize;
				Rectangle textRect = new Rectangle(left, 1, ClientRectangle.Width - left, ClientRectangle.Height - 2);
				TextRenderer.DrawText(g, Text, Font, textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
				if (Focused)
				{
					textRect = new Rectangle(left - 1, textRect.Y - 1, textRect.Width, textRect.Height + 3);
					SkinManager.Instance.DrawFocusRectangle(g, textRect);
				}
			}
			else
			{
				DrawButton(g, skin);
			}
		}

		private void DrawButton(Graphics g, Skin skin)
		{
			ColorSet set = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Normal);

			Rectangle bounds = ClientRectangle;

			VisualState state = MouseState;
			if (Checked)
			{
				//back
				SolidBrush backColor = set.GetBrush(state, Focused, Enabled);
				g.FillRectangle(backColor, bounds);

				//text
				SkinnedButton.DrawContent(g, bounds, set.ForeColor, Image, ImageAlign, Text, ContentAlignment.MiddleCenter);

				if (Focused)
				{
					SkinManager.Instance.DrawFocusRectangle(g, bounds);
				}

				//border
				Pen borderPen = set.GetBorderPen(state, true, Enabled);
				g.DrawRectangle(borderPen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			}
			else
			{
				Color foreColor = skin.GetForeColor(FieldType);
				Color backColor = Parent.BackColor;

				//back
				if (state == VisualState.Hover)
				{
					backColor = set.Hover;
					foreColor = set.ForeColor;
				}
				else if (state == VisualState.Pressed)
				{
					backColor = set.Pressed;
					foreColor = set.ForeColor;
				}
				using (SolidBrush br = new SolidBrush(backColor))
				{
					g.FillRectangle(br, bounds);
				}
				//text
				SkinnedButton.DrawContent(g, bounds, foreColor, Image, ImageAlign, Text, ContentAlignment.MiddleCenter);

				if (Focused)
				{
					SkinManager.Instance.DrawFocusRectangle(g, bounds);
				}
			}
		}
	}
}
