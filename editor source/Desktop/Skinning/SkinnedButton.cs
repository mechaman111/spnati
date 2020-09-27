using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedButton : Button, ISkinControl
	{
		public VisualState MouseState { get; private set; }

		private bool _flat = false;
		public bool Flat
		{
			get { return _flat; }
			set { _flat = value; _colorSet = null; Invalidate(); }
		}

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; _colorSet = null; Invalidate(); }
		}
		private ColorSet _colorSet;

		private SkinnedBackgroundType _background = SkinnedBackgroundType.Surface;
		public SkinnedBackgroundType Background
		{
			get { return _background; }
			set
			{
				_background = value;
				Invalidate(true);
			}
		}

		private Animator _animator = new Animator(0.15f);

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (DesignMode) { return; }

			MouseEnter += SkinnedButton_MouseEnter;
			MouseLeave += SkinnedButton_MouseLeave;
			MouseDown += SkinnedButton_MouseDown;
			MouseUp += SkinnedButton_MouseUp;
			KeyDown += SkinnedButton_KeyDown;
			KeyUp += SkinnedButton_KeyUp;
			_animator.OnUpdate += _animator_OnUpdate;
		}

		private void _animator_OnUpdate(object sender, float e)
		{
			Invalidate();
		}

		private void SkinnedButton_MouseEnter(object sender, EventArgs e)
		{
			MouseState = VisualState.Hover;
			_animator.StartAnimation(AnimationDirection.In);
			Invalidate();
		}

		private void SkinnedButton_MouseLeave(object sender, EventArgs e)
		{
			MouseState = VisualState.Normal;
			_animator.StartAnimation(AnimationDirection.Out);
			Invalidate();
		}

		private void SkinnedButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				MouseState = VisualState.Pressed;
				Invalidate();
			}
		}

		private void SkinnedButton_MouseUp(object sender, MouseEventArgs e)
		{
			MouseState = VisualState.Hover;
			Invalidate();
		}

		private void SkinnedButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				MouseState = VisualState.Pressed;
				Invalidate();
			}
		}

		private void SkinnedButton_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				MouseState = VisualState.Normal;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			if (Flat)
			{
				DrawFlat(pevent);
			}
			else
			{
				DrawButton(pevent);
			}
		}

		private void DrawFlat(PaintEventArgs pevent)
		{
			if (_colorSet == null)
			{
				GetColorSet();
			}

			Graphics g = pevent.Graphics;
			Color backColor = DesignMode ? SystemColors.Control : this.GetSkinnedPanelBackColor();
			Color hoverColor = MouseState == VisualState.Pressed ? _colorSet.Pressed : _colorSet.Hover;
			backColor = ColorSet.BlendColor(backColor, hoverColor, _animator.Value);

			g.Clear(backColor);

			Color foreColor = Enabled ? ForeColor : _colorSet.DisabledForeColor;
			Color hoverForeColor = Enabled ? _colorSet.ForeColor : _colorSet.DisabledForeColor;
			foreColor = ColorSet.BlendColor(foreColor, hoverForeColor, _animator.Value);

			DrawContent(g, ClientRectangle, foreColor, Image, ImageAlign, Text, TextAlign);
		}

		private void DrawButton(PaintEventArgs pevent)
		{
			if (_colorSet == null)
			{
				GetColorSet();
			}

			Graphics g = pevent.Graphics;

			Color backColor = _colorSet.GetColor(VisualState.Normal, Focused, Enabled);
			Color hoverColor = MouseState == VisualState.Pressed ? _colorSet.Pressed : _colorSet.Hover;
			backColor = ColorSet.BlendColor(backColor, hoverColor, _animator.Value);

			g.Clear(backColor);
			if (Focused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, pevent.ClipRectangle);
			}
			DrawContent(g, ClientRectangle, Enabled ? _colorSet.ForeColor : _colorSet.DisabledForeColor, Image, ImageAlign, Text, TextAlign);

			//border
			Pen borderPen = _colorSet.GetBorderPen(MouseState, Focused, Enabled);
			g.DrawRectangle(borderPen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}

		public static void DrawContent(Graphics g, Rectangle bounds, Color color, Image image, ContentAlignment imageAlign, string text, ContentAlignment textAlign)
		{
			SkinManager manager = SkinManager.Instance;
			Brush foreColor = manager.GetBrush(color);

			const int InnerPadding = 5;
			Rectangle textRect = new Rectangle(bounds.X + InnerPadding, bounds.Y + InnerPadding, bounds.Width - InnerPadding * 2, bounds.Height - InnerPadding * 2);

			if (image != null)
			{
				Rectangle imageRect = bounds;
				switch (imageAlign)
				{
					case ContentAlignment.BottomCenter:
						imageRect = new Rectangle(bounds.Width / 2 - image.Width / 2, bounds.Height - InnerPadding - image.Height, image.Width, image.Height);
						break;
					case ContentAlignment.BottomLeft:
						imageRect = new Rectangle(InnerPadding, bounds.Height - InnerPadding - image.Height, image.Width, image.Height);
						break;
					case ContentAlignment.BottomRight:
						imageRect = new Rectangle(bounds.Width - InnerPadding - image.Width, bounds.Height - InnerPadding - image.Height, image.Width, image.Height);
						break;
					case ContentAlignment.MiddleCenter:
						imageRect = new Rectangle(bounds.Width / 2 - image.Width / 2, bounds.Height / 2 - image.Height / 2, image.Width, image.Height);
						break;
					case ContentAlignment.MiddleLeft:
						imageRect = new Rectangle(InnerPadding, bounds.Height / 2 - image.Height / 2, image.Width, image.Height);
						break;
					case ContentAlignment.MiddleRight:
						imageRect = new Rectangle(bounds.Width - InnerPadding - image.Width, bounds.Height / 2 - image.Height / 2, image.Width, image.Height);
						break;
					case ContentAlignment.TopCenter:
						imageRect = new Rectangle(bounds.Width / 2 - image.Width / 2, InnerPadding, image.Width, image.Height);
						break;
					case ContentAlignment.TopLeft:
						imageRect = new Rectangle(InnerPadding, InnerPadding, image.Width, image.Height);
						break;
					case ContentAlignment.TopRight:
						imageRect = new Rectangle(bounds.Width - InnerPadding - image.Width, InnerPadding, image.Width, image.Height);
						break;
				}
				g.DrawImage(image, imageRect);
			}

			StringFormat sf = new StringFormat();
			sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			switch (textAlign)
			{
				case ContentAlignment.BottomCenter:
					sf.LineAlignment = StringAlignment.Far;
					sf.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					sf.LineAlignment = StringAlignment.Far;
					sf.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomRight:
					sf.LineAlignment = StringAlignment.Far;
					sf.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleCenter:
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					sf.LineAlignment = StringAlignment.Near;
					sf.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopLeft:
					sf.LineAlignment = StringAlignment.Near;
					sf.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					sf.LineAlignment = StringAlignment.Near;
					sf.Alignment = StringAlignment.Far;
					break;
			}
			sf.FormatFlags = StringFormatFlags.NoWrap;
			g.DrawString(text?.ToUpper() ?? "", Skin.ButtonFont, foreColor, textRect, sf);
		}

		public void OnUpdateSkin(Skin skin)
		{
			_colorSet = null;
		}

		private void GetColorSet()
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			if (Flat)
			{
				_colorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Light);
				ForeColor = skin.GetForeColor(FieldType);

				//forecolor depends on parent background
				if (Parent != null && Parent is ISkinnedPanel)
				{
					ISkinnedPanel panel = Parent as ISkinnedPanel;
					switch (panel.PanelType)
					{
						case SkinnedBackgroundType.PrimaryDark:
							ForeColor = FieldType == SkinnedFieldType.Secondary ? skin.SecondaryForeColor : skin.PrimaryDarkColor.ForeColor;
							break;
						case SkinnedBackgroundType.SecondaryDark:
							ForeColor = FieldType == SkinnedFieldType.Secondary ? skin.SecondaryDarkColor.ForeColor : skin.PrimaryForeColor;
							break;
					}
				}
			}
			else
			{
				_colorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Normal);
			}
		}
	}

	public enum ColorMode
	{
		Primary,
		Secondary
	}
}
