using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedMenuStrip : MenuStrip, ISkinControl
	{
		private SkinnedBackgroundType _background = SkinnedBackgroundType.PrimaryDark;
		public SkinnedBackgroundType Background
		{
			get { return _background; }
			set
			{
				_background = value;
				Tag = _background.ToString();
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				Invalidate(true);
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			Renderer = new SkinnedToolStripRenderer(skin, this);
		}
	}
}
