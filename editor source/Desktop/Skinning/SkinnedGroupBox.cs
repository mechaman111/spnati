using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedGroupBox : GroupBox, ISkinControl, ISkinnedPanel
	{
		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Surface; }
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

			string text = Text;
			Font font = Skin.HeaderFont;
			SizeF textSize = g.MeasureString(text, font);
			RectangleF textRect = new RectangleF(ClientRectangle.Left + 4, ClientRectangle.Top + 1, textSize.Width, textSize.Height);
			using (Brush textBrush = new SolidBrush(Enabled ? skin.PrimaryForeColor : skin.Surface.DisabledForeColor))
			{
				using (Brush back = new SolidBrush(BackColor))
				{
					g.FillRectangle(back, textRect);
				}
				g.DrawString(text, font, textBrush, textRect);
			}
		}
	}
}
