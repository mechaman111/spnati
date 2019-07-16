using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedNumericUpDown : NumericUpDown, ISkinControl
	{
		protected override void OnCreateControl()
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			Font = Skin.TextFont;
			ForeColor = skin.Surface.ForeColor;
			BackColor = skin.FieldBackColor;
		}
	}
}
