using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedIcon : SkinnedButton
	{
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Color backColor = DesignMode ? SystemColors.Control : this.GetSkinnedPanelBackColor();
			if (MouseState == VisualState.Hover)
			{
				backColor = SkinManager.Instance.CurrentSkin.PrimaryColor.Hover;
			}
			pevent.Graphics.Clear(backColor);

			if (Image != null)
			{
				pevent.Graphics.DrawImage(Image, ClientRectangle.X + ClientRectangle.Width / 2 - Image.Width / 2, ClientRectangle.Y + ClientRectangle.Height / 2 - Image.Height / 2);
			}
		}

	}
}
