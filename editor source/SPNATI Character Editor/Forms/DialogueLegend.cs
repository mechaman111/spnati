using Desktop.Skinning;

namespace SPNATI_Character_Editor.Forms
{
	public partial class DialogueLegend : SkinnedForm
	{
		public DialogueLegend()
		{
			InitializeComponent();
		}

		protected override void OnUpdateSkin(Skin skin)
		{
			base.OnUpdateSkin(skin);
			lblBlue.ForeColor = skin.Blue;
			lblGray.ForeColor = skin.Gray;
			lblGreen.ForeColor = skin.Green;
			lblLightGray.ForeColor = skin.LightGray;
			lblOrange.ForeColor = skin.Orange;
		}
	}
}
