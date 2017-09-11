using System;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class SettingsSetup : Form
	{
		public SettingsSetup()
		{
			InitializeComponent();
			txtApplicationDirectory.Text = Config.GameDirectory;
			txtKisekae.Text = Config.KisekaeDirectory;
			folderBrowserDialog1.SelectedPath = Config.GameDirectory;
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtApplicationDirectory.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		public static bool VerifyApplicationDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				return false;
			}
			string opponentsDir = Path.Combine(path, "opponents");
			if (!Directory.Exists(opponentsDir))
			{
				return false;
			}
			if (!File.Exists(Path.Combine(opponentsDir, "listing.xml")))
				return false;
			return true; //Pretty stupid validation, but how thorough do we really need to be?
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			string dir = folderBrowserDialog1.SelectedPath;
			if (!VerifyApplicationDirectory(dir))
			{
				MessageBox.Show("The provided directory does not appear to contain SPNATI! This application cannot start without a valid directory.");
				return;
			}
			Config.GameDirectory = dir;
			DialogResult = DialogResult.OK;
			Config.Save();
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
