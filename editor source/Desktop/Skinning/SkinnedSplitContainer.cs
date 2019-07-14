using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedSplitContainer : SplitContainer, ISkinControl
	{
		public SkinnedBackgroundType SplitterColor { get; set; } = SkinnedBackgroundType.Primary;

		private SolidBrush _splitterBrush = new SolidBrush(Color.White);

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			_splitterBrush.Color = skin.GetBackColor(SplitterColor);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Skin skin = SkinManager.Instance.CurrentSkin;
			Graphics g = e.Graphics;

			DrawSplitter(g, skin);
		}

		private void DrawSplitter(Graphics g, Skin skin)
		{
			g.FillRectangle(_splitterBrush, SplitterRectangle);
		}
	}
}
