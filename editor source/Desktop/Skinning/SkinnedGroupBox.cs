using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedGroupBox : GroupBox, ISkinControl, ISkinnedPanel
	{
		private Image _image;
		public Image Image
		{
			get { return _image; }
			set
			{
				_image = value;
				Invalidate();
			}
		}

		private SkinnedHighlight _highlight = SkinnedHighlight.Heading;
		public SkinnedHighlight Highlight
		{
			get { return _highlight; }
			set
			{
				_highlight = value;
				Invalidate();
			}
		}

		private SkinnedBackgroundType _panelType = SkinnedBackgroundType.Surface;
		public SkinnedBackgroundType PanelType
		{
			get { return _panelType; }
			set
			{
				_panelType = value;
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				Invalidate();
			}
		}

		private bool _showIndicatorBar;
		public bool ShowIndicatorBar
		{
			get { return _showIndicatorBar; }
			set
			{
				_showIndicatorBar = value;
				Invalidate();
			}
		}

		private SolidBrush _indicatorBrush = new SolidBrush(Color.Black);

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		private ColorSet GetColorSet(Skin skin)
		{
			switch (_panelType)
			{
				case SkinnedBackgroundType.Group1:
					return skin.Group1Set;
				case SkinnedBackgroundType.Group2:
					return skin.Group2Set;
				case SkinnedBackgroundType.Group3:
					return skin.Group3Set;
				case SkinnedBackgroundType.Group4:
					return skin.Group4Set;
				case SkinnedBackgroundType.Group5:
					return skin.Group5Set;
				case SkinnedBackgroundType.Critical:
					return skin.Critical;
				default:
					return skin.Surface;
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = GetColorSet(skin).Normal;
			UpdateIndicatorColor();
		}

		private void UpdateIndicatorColor()
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			switch (_panelType)
			{
				case SkinnedBackgroundType.Group1:
					_indicatorBrush.Color = skin.Group1;
					break;
				case SkinnedBackgroundType.Group2:
					_indicatorBrush.Color = skin.Group2;
					break;
				case SkinnedBackgroundType.Group3:
					_indicatorBrush.Color = skin.Group3;
					break;
				case SkinnedBackgroundType.Group4:
					_indicatorBrush.Color = skin.Group4;
					break;
				case SkinnedBackgroundType.Group5:
					_indicatorBrush.Color = skin.Group5;
					break;
				default:
					_indicatorBrush.Color = skin.Surface.Normal;
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			const int IndicatorWidth = 3;
			Skin skin = SkinManager.Instance.CurrentSkin;

			Graphics g = e.Graphics;
			ColorSet set = GetColorSet(skin);
			g.Clear(Enabled ? BackColor : set.Disabled);

			Rectangle rect = new Rectangle(ClientRectangle.Left + 1, ClientRectangle.Top, ClientRectangle.Width - 3, ClientRectangle.Height - 2);
			Pen pen = set.GetBorderPen(VisualState.Normal, false, Enabled);
			g.DrawRectangle(pen, rect);
			using (Pen shadow = new Pen(skin.SurfaceShadowColor))
			{
				g.DrawLine(shadow, rect.Left - 1, rect.Y, rect.Left - 1, rect.Bottom + 1);
				g.DrawLine(shadow, rect.Right + 1, rect.Y, rect.Right + 1, rect.Bottom + 1);
				g.DrawLine(shadow, ClientRectangle.X, rect.Bottom + 1, rect.Right, rect.Bottom + 1);
			}

			if (_showIndicatorBar && _panelType != SkinnedBackgroundType.Surface)
			{
				g.FillRectangle(_indicatorBrush, ClientRectangle.Left + 1, ClientRectangle.Top + 1, IndicatorWidth, ClientRectangle.Height - 2);
			}

			if (Image != null)
			{
				g.DrawImage(Image, ClientRectangle.Left + 4, ClientRectangle.Top + 4);
			}

			string text = Text;
			Font font = Skin.HeaderFont;
			SizeF textSize = g.MeasureString(text, font);
			RectangleF textRect = new RectangleF(ClientRectangle.Left + 4 + (Image == null ? 0 : Image.Width), ClientRectangle.Top + 1, textSize.Width, textSize.Height);
			Color textColor = set.DisabledForeColor;
			if (Enabled)
			{
				switch (_panelType)
				{
					case SkinnedBackgroundType.Group1:
						textColor = skin.Group1;
						break;
					case SkinnedBackgroundType.Group2:
						textColor = skin.Group2;
						break;
					case SkinnedBackgroundType.Group3:
						textColor = skin.Group3;
						break;
					case SkinnedBackgroundType.Group4:
						textColor = skin.Group4;
						break;
					case SkinnedBackgroundType.Group5:
						textColor = skin.Group5;
						break;
					case SkinnedBackgroundType.Critical:
						textColor = set.ForeColor;
						break;
					default:
						textColor = skin.PrimaryForeColor;
						break;
				}
			}
			if (Enabled && Highlight != SkinnedHighlight.Heading)
			{
				textColor = skin.GetHighlightColor(Highlight);
			}
			using (Brush textBrush = new SolidBrush(textColor))
			{
				using (Brush back = new SolidBrush(BackColor))
				{
					g.FillRectangle(back, textRect);
				}
				g.DrawString(text, font, textBrush, textRect);
			}
		}

		private Shield _shield;
		public void Shield()
		{
			if (_shield != null) { return; }
			_shield = new Shield();
			_shield.Dock = DockStyle.Fill;
			Controls.Add(_shield);
			_shield.BringToFront();
		}

		public void Unshield()
		{
			if (_shield == null) { return; }
			Controls.Remove(_shield);
			_shield.Dispose();
			_shield = null;
		}
	}
}
