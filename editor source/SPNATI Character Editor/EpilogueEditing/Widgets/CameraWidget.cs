using Desktop.Skinning;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class CameraWidget : KeyframedWidget
	{
		private SolidBrush _titleBrush = new SolidBrush(Color.Gray);
		private SolidBrush _rowBrush = new SolidBrush(Color.LightGray);
		private static Color _accentColor;

		protected override SolidBrush GetFillBrush(bool selected)
		{
			return _rowBrush;
		}

		protected override void OnUpdateSkin(Skin skin)
		{
			base.OnUpdateSkin(skin);
			_titleBrush.Color = skin.GetAppColor("WidgetHeaderRow");
			_rowBrush.Color = skin.GetAppColor("WidgetRow");
			_accentColor = skin.GetAppColor("CameraAccent");
		}

		protected override SolidBrush GetTitleBrush()
		{
			return _titleBrush;
		}

		public CameraWidget(LiveAnimatedObject data, Timeline timeline) : base(data, timeline)
		{
			AllowDelete = false;
			IsCollapsed = true;
			AllowParenting = false;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public override Image GetThumbnail()
		{
			return Properties.Resources.VideoCamera;
		}

		public override string ToString()
		{
			return "Camera";
		}

		protected override Color GetAccentColor()
		{
			return _accentColor;
		}
	}
}
