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

			Graphics g = e.Graphics;

			DrawSplitter(g);
		}

		private void DrawSplitter(Graphics g)
		{
			g.FillRectangle(_splitterBrush, SplitterRectangle);
		}
	}
}
