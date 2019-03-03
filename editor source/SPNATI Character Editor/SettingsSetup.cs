using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class SettingsSetup : Form
	{
		public SettingsSetup()
		{
			InitializeComponent();
			string gameDir = Config.GetString(Settings.GameDirectory);
			txtApplicationDirectory.Text = gameDir;
			folderBrowserDialog1.SelectedPath = gameDir;
			txtUserName.Text = Config.UserName;
			valAutoSave.Value = Config.AutoSaveInterval;
			chkIntellisense.Checked = Config.UseIntellisense;
			chkHideImages.Checked = Config.UsePrefixlessImages;
			txtFilter.Text = Config.PrefixFilter;
			chkAutoBanter.Checked = Config.AutoLoadBanterWizard;
			chkAutoBackup.Checked = Config.AutoBackupEnabled;
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
				ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find directory: {0}", path));
				return false;
			}
			string opponentsDir = Path.Combine(path, "opponents");
			if (!Directory.Exists(opponentsDir))
			{
				ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find opponents directory: {0}", path));
				return false;
			}
			if (!File.Exists(Path.Combine(opponentsDir, "listing.xml")))
			{
				ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find listing.mxl: {0}", path));
				return false;
			}
			return true; //Pretty stupid validation, but how thorough do we really need to be?
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			string dir = txtApplicationDirectory.Text;
			if (!VerifyApplicationDirectory(dir))
			{
				MessageBox.Show("The provided directory does not appear to contain SPNATI! This application cannot start without a valid directory.");
				return;
			}
			Config.Set(Settings.GameDirectory, dir);
			Config.AutoSaveInterval = (int)valAutoSave.Value;
			Config.UserName = txtUserName.Text;
			Config.UseIntellisense = chkIntellisense.Checked;
			Config.UsePrefixlessImages = chkHideImages.Checked;
			Config.PrefixFilter = txtFilter.Text;
			Config.AutoLoadBanterWizard = chkAutoBanter.Checked;
			Config.AutoBackupEnabled = chkAutoBackup.Checked;
			DialogResult = DialogResult.OK;
			Config.Save();
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void chkHideImages_CheckedChanged(object sender, EventArgs e)
		{
			txtFilter.Enabled = chkHideImages.Checked;
		}

		private void tabsSections_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			Brush textBrush;
			Brush backBrush;

			Rectangle rect = tabsSections.ClientRectangle;

			// Get the item from the collection.
			TabPage tabPage = tabsSections.TabPages[e.Index];

			// Get the real bounds for the tab rectangle.
			Rectangle tabBounds = tabsSections.GetTabRect(e.Index);

			if (e.State == DrawItemState.Selected)
			{
				// Draw a different background color, and don't paint a focus rectangle.
				textBrush = new SolidBrush(Color.White);
				backBrush = new SolidBrush(Color.SlateBlue);
				g.FillRectangle(backBrush, e.Bounds);
			}
			else
			{
				textBrush = new SolidBrush(e.ForeColor);
				//e.DrawBackground();
				g.FillRectangle(Brushes.White, e.Bounds);
			}

			// Use our own font.
			Font tabFont = new Font("Arial", (float)11.0, FontStyle.Bold, GraphicsUnit.Pixel);

			// Draw string. Center the text.
			StringFormat stringFlags = new StringFormat();
			stringFlags.Alignment = StringAlignment.Center;
			stringFlags.LineAlignment = StringAlignment.Center;
			g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
		}
	}
}
