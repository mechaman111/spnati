using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedTextBox : TextBox, ISkinControl
	{
		protected override void OnCreateControl()
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			ForeColor = skin.Surface.ForeColor;
			BackColor = skin.FieldBackColor;
		}
	}
}
