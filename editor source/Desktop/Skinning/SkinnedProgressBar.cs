using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedProgressBar : ProgressBar
	{
		public SkinnedProgressBar()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Rectangle rec = e.ClipRectangle;

			rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
			e.Graphics.FillRectangle(skin.PrimaryLightColor.GetBrush(VisualState.Normal), e.ClipRectangle);
			rec.Height = rec.Height - 4;
			e.Graphics.FillRectangle(skin.PrimaryColor.GetBrush(VisualState.Normal, false, Enabled), 2, 2, rec.Width, rec.Height);
		}
	}
}
