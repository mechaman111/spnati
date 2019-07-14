using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class DiscardResponsePrompt : SkinnedForm
	{
		public DiscardResponsePrompt()
		{
			InitializeComponent();
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
			Close();
		}

		private void cmdDiscard_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
