using Desktop;
using Desktop.Skinning;
using System;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class FirstLaunchSetup : SkinnedForm
	{
		public FirstLaunchSetup()
		{
			InitializeComponent();
			string gameDir = Config.GetString(Settings.GameDirectory);
			txtApplicationDirectory.Text = gameDir;
			txtKisekae.Text = Config.KisekaeDirectory;
			folderBrowserDialog1.SelectedPath = gameDir;
			txtUserName.Text = Config.UserName;
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtApplicationDirectory.Text))
			{
				try
				{
					folderBrowserDialog1.SelectedPath = txtApplicationDirectory.Text;
				}
				catch { }
			}
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtApplicationDirectory.Text = folderBrowserDialog1.SelectedPath;
				ValidateApplicationDirectory();
			}
		}

		private void ValidateApplicationDirectory()
		{
			if (string.IsNullOrEmpty(txtApplicationDirectory.Text))
			{
				return;
			}

			//try to make things easier for people who got close but not perfect by auto-adjusting
			string original = Path.GetFullPath(txtApplicationDirectory.Text);
			if (!string.IsNullOrEmpty(original) && original.EndsWith("\\"))
			{
				original = original.Substring(0, original.Length - 1);
			}
			string dir = original;
			if (!SettingsSetup.VerifyApplicationDirectory(dir))
			{
				//try going up a level
				dir = Path.GetDirectoryName(original);
			}
			if (!SettingsSetup.VerifyApplicationDirectory(dir))
			{
				//Nope. How about down a level?
				bool succeed = false;
				foreach (string subfolder in Directory.EnumerateDirectories(original))
				{
					if (SettingsSetup.VerifyApplicationDirectory(subfolder))
					{
						dir = subfolder;
						succeed = true;
						break;
					}
				}
				if (!succeed)
				{
					dir = original;
				}
			}
			if (dir != txtApplicationDirectory.Text)
			{
				txtApplicationDirectory.Text = dir;
			}
		}

		private void cmdBrowseKisekae_Click(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtKisekae.Text))
			{
				openFileDialog1.InitialDirectory = Path.GetDirectoryName(txtKisekae.Text);
				openFileDialog1.FileName = Path.GetFileName(txtKisekae.Text);
			}
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string file = openFileDialog1.FileName;
				txtKisekae.Text = Path.GetFullPath(file);
			}
		}

		private void txtApplicationDirectory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ValidateApplicationDirectory();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			string dir = txtApplicationDirectory.Text;
			if (!SettingsSetup.VerifyApplicationDirectory(dir, true))
			{
				MessageBox.Show("The provided directory does not appear to contain SPNATI! This application cannot start without a valid directory.");
				return;
			}
			Config.Set(Settings.GameDirectory, dir);
			
			Config.UserName = txtUserName.Text;
			Config.KisekaeDirectory = txtKisekae.Text;
			
			DialogResult = DialogResult.OK;
			Config.Save();
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.SettingsUpdated);
			Close();
		}
	}
}
