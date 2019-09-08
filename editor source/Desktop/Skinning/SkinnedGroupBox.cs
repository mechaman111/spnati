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

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Surface; }
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.Surface.Normal;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			Graphics g = e.Graphics;
			g.Clear(Enabled ? BackColor : skin.Surface.Disabled);

			Rectangle rect = new Rectangle(ClientRectangle.Left + 1, ClientRectangle.Top, ClientRectangle.Width - 3, ClientRectangle.Height - 2);
			Pen pen = skin.Surface.GetBorderPen(VisualState.Normal, false, Enabled);
			g.DrawRectangle(pen, rect);
			using (Pen shadow = new Pen(skin.SurfaceShadowColor))
			{
				g.DrawLine(shadow, rect.Left - 1, rect.Y, rect.Left - 1, rect.Bottom + 1);
				g.DrawLine(shadow, rect.Right + 1, rect.Y, rect.Right + 1, rect.Bottom + 1);
				g.DrawLine(shadow, ClientRectangle.X, rect.Bottom + 1, rect.Right, rect.Bottom + 1);
			}

			if (Image != null)
			{
				g.DrawImage(Image, ClientRectangle.Left + 4, ClientRectangle.Top + 4);
			}

			string text = Text;
			Font font = Skin.HeaderFont;
			SizeF textSize = g.MeasureString(text, font);
			RectangleF textRect = new RectangleF(ClientRectangle.Left + 4 + (Image == null ? 0 : Image.Width), ClientRectangle.Top + 1, textSize.Width, textSize.Height);
			Color textColor = Enabled ? skin.PrimaryForeColor : skin.Surface.DisabledForeColor;
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
