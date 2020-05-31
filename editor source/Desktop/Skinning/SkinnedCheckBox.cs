using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedCheckBox : CheckBox, ISkinControl
	{
		public VisualState MouseState { get; private set; }
		private static StringFormat _sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap };

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; Invalidate(); }
		}

		public SkinnedCheckBox()
		{
			if (DesignMode)
			{
				OnUpdateSkin(new Skin());
			}
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
			Invalidate();
		}

		public const int CheckBoxSize = 12;
		public static readonly Point[] CheckmarkPoints = { new Point(1, 5), new Point(5, 9), new Point(11, 3) };

		public static void RenderCheckbox(Graphics g, int x, int y, SkinnedFieldType fieldType, CheckState checkState, VisualState state, bool enabled)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Rectangle rect = new Rectangle(x, y, CheckBoxSize, CheckBoxSize);

			using (Brush boxColor = new SolidBrush(enabled ? skin.FieldBackColor : skin.FieldDisabledBackColor))
			{
				ColorSet set = skin.GetWidgetColorSet(fieldType);
				g.FillRectangle(boxColor, rect);
				Pen borderPen = set.GetBorderPen(state, false, enabled);
				g.DrawRectangle(borderPen, rect);

				Brush checkBrush = set.GetBrush(state, false, enabled);
				if (checkState == CheckState.Indeterminate)
				{
					g.FillRectangle(checkBrush, rect.X + 3, rect.Y + 3, rect.Width - 5, rect.Height - 5);
				}
				else if (checkState == CheckState.Checked)
				{
					g.FillRectangle(checkBrush, rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 1);
					using (Pen check = new Pen(enabled ? set.ForeColor : set.DisabledForeColor, 2))
					{
						for (int i = 0; i < CheckmarkPoints.Length - 1; i++)
						{
							Point pt0 = new Point(x + CheckmarkPoints[i].X, y + CheckmarkPoints[i].Y);
							Point pt1 = new Point(x + CheckmarkPoints[i + 1].X, y + CheckmarkPoints[i + 1].Y);
							g.DrawLine(check, pt0, pt1);
						}
					}

				}
			}
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			Graphics g = pevent.Graphics;

			g.Clear(DesignMode ? SystemColors.Control : this.GetSkinnedPanelBackColor());

			if (Appearance == Appearance.Normal)
			{
				using (Brush boxColor = new SolidBrush(skin.FieldBackColor))
				{
					int start = pevent.ClipRectangle.Y + pevent.ClipRectangle.Height / 2 - CheckBoxSize / 2 - 1;
					RenderCheckbox(g, 0, start, FieldType, CheckState, MouseState, Enabled);
				}

				Color foreColor = DesignMode ? ForeColor : (Enabled ? this.GetSkinnedPanelForeColor() : skin.LabelForeColor);
				int left = 2 + CheckBoxSize;
				Rectangle textRect = new Rectangle(left, 0, ClientRectangle.Width - left, ClientRectangle.Height - 2);
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
				Pen borderPen = set.GetBorderPen(state, Focused, Enabled);
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
