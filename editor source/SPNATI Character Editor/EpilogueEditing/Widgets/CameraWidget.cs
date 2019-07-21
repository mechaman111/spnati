using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class CameraWidget : KeyframedWidget
	{
		protected override Brush GetFillBrush(bool selected)
		{
			return selected ? Brushes.LightGray : Brushes.Gray;
		}

		protected override Brush GetTitleBrush()
		{
			return Brushes.DarkGray;
		}

		public CameraWidget(LiveAnimatedObject data, Timeline timeline) : base(data, timeline)
		{
			AllowDelete = false;
			IsCollapsed = true;
		}

		public override Image GetThumbnail()
		{
			return Properties.Resources.VideoCamera;
		}
	}
}
