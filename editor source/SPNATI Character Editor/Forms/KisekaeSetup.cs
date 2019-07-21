using Desktop.Skinning;
using System;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class KisekaeSetup : SkinnedForm
	{
		public KisekaeSetup()
		{
			InitializeComponent();
		}

		private void cmdBrowseKisekae_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = txtKisekae.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string file = openFileDialog1.FileName;
				txtKisekae.Text = Path.GetFullPath(file);
			}
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			Config.KisekaeDirectory = txtKisekae.Text;
			Config.Save();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
