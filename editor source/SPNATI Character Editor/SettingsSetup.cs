using Desktop;
using Desktop.Skinning;
using TinifyAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class SettingsSetup : SkinnedForm
	{
		public SettingsSetup()
		{
			InitializeComponent();
			string gameDir = Config.GetString(Settings.GameDirectory);
			txtApplicationDirectory.Text = gameDir;
			txtKisekae.Text = Config.KisekaeDirectory;
			folderBrowserDialog1.SelectedPath = gameDir;
			txtUserName.Text = Config.UserName;
			valAutoSave.Value = Config.AutoSaveInterval;
			chkIntellisense.Checked = Config.UseIntellisense;
			chkHidePrefixlessImages.Checked = Config.UsePrefixlessImages;
			txtFilter.Text = Config.PrefixFilter;
			chkAutoBackup.Checked = Config.BackupEnabled;
			chkInitialAdd.Checked = Config.AutoOpenConditions;
			chkDefaults.Checked = !Config.SuppressDefaults;
			chkCaseTree.Checked = !Config.UseSimpleTree;
			chkColorTargets.Checked = Config.ColorTargetedLines;
			chkWorkflowTracer.Checked = !Config.DisableWorkflowTracer;
			chkEmptyCases.Checked = !Config.HideEmptyCases;
			cboImportMethod.Items.Add("Locally (kkl.exe must be running)");
			cboImportMethod.Items.Add("Remotely (Internet connection required)");
			cboImportMethod.Items.Add("Legacy (kkl.exe must be running)");
			cboImportMethod.SelectedIndex = Config.ImportMethod;
			valFrequency.Value = Config.BackupPeriod;
			valLifetime.Value = Config.BackupLifeTime;
			panelSnapshot.Enabled = Config.BackupEnabled;
			txtTinify.Text = Config.TinifyKey;
			chkDashboard.Checked = Config.EnableDashboard;
			chkChecklistSpell.Checked = Config.EnableDashboardSpellCheck;
			chkChecklistValidation.Checked = Config.EnableDashboardValidation;
			chkStartDashboard.Checked = Config.StartOnDashboard;
			chkHideImages.Checked = !Config.GetBoolean(Settings.HideImages);
			chkPreviewBubble.Checked = Config.GetBoolean(Settings.ShowPreviewText);
			chkPreviewFormatting.Checked = !Config.GetBoolean(Settings.DisablePreviewFormatting);
			valFranchise.Value = Config.MaxFranchisePartners;
			chkAutoFill.Checked = Config.AutoPopulateStageImages;
			chkWarnIncomplete.Checked = Config.WarnAboutIncompleteStatus;
			chkSafeMode.Checked = Config.SafeMode;
			chkLegacyPoses.Checked = Config.ShowLegacyPoseTabs;

			recAutoOpen.RecordType = typeof(Character);
			recAutoOpen.RecordFilter = CharacterDatabase.FilterHuman;
			recAutoOpen.RecordKey = Config.GetString(Settings.AutoOpenCharacter);

			HashSet<string> pauses = Config.AutoPauseDirectives;
			foreach (DirectiveDefinition def in Definitions.Instance.Get<DirectiveDefinition>())
			{
				if (def.Key == "pause" || def.Key == "wait" || def.Key == "prompt")
				{
					continue;
				}
				lstPauses.Items.Add(def.Key, pauses.Contains(def.Key));
			}
			lstPauses.Sorted = true;

			HashSet<string> statusFilters = Config.StatusFilters;
			foreach (string status in new string[] {
				OpponentStatus.Testing,
				OpponentStatus.Offline,
				OpponentStatus.Incomplete,
				OpponentStatus.Duplicate,
				OpponentStatus.Event,
			})
			{
				chkStatuses.Items.Add(status, statusFilters.Contains(status));
			}
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
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

		public static bool VerifyApplicationDirectory(string path, bool noisy = false)
		{
			if (!Directory.Exists(path))
			{
				if (noisy)
				{
					ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find directory: {0}", path));
				}
				return false;
			}
			string opponentsDir = Path.Combine(path, "opponents");
			if (!Directory.Exists(opponentsDir))
			{
				if (noisy)
				{
					ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find opponents directory: {0}", path));
				}
				return false;
			}
			if (!File.Exists(Path.Combine(opponentsDir, "listing.xml")))
			{
				if (noisy)
				{
					ErrorLog.LogError(string.Format("When trying to verify SPNATI directory, could not find listing.mxl: {0}", path));
				}
				return false;
			}
			return true; //Pretty stupid validation, but how thorough do we really need to be?
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			string dir = txtApplicationDirectory.Text;
			if (!VerifyApplicationDirectory(dir, true))
			{
				MessageBox.Show("The provided directory does not appear to contain SPNATI! This application cannot start without a valid directory.");
				return;
			}
			Config.Set(Settings.GameDirectory, dir);
			Config.AutoSaveInterval = (int)valAutoSave.Value;
			Config.UserName = txtUserName.Text;
			Config.UseIntellisense = chkIntellisense.Checked;
			Config.UsePrefixlessImages = chkHidePrefixlessImages.Checked;
			Config.PrefixFilter = txtFilter.Text;
			Config.BackupEnabled = chkAutoBackup.Checked;
			Config.AutoOpenConditions = chkInitialAdd.Checked;
			Config.KisekaeDirectory = txtKisekae.Text;
			Config.SuppressDefaults = !chkDefaults.Checked;
			Config.UseSimpleTree = !chkCaseTree.Checked;
			Config.ColorTargetedLines = chkColorTargets.Checked;
			Config.DisableWorkflowTracer = !chkWorkflowTracer.Checked;
			Config.HideEmptyCases = !this.chkEmptyCases.Checked;
			Config.ImportMethod = cboImportMethod.SelectedIndex;
			CharacterGenerator.SetConverter(Config.ImportMethod);
			Config.BackupPeriod = (int)valFrequency.Value;
			Config.BackupLifeTime = (int)valLifetime.Value;
			Config.TinifyKey = txtTinify.Text;
			Config.EnableDashboard = chkDashboard.Checked;
			Config.StartOnDashboard = chkStartDashboard.Checked;
			Config.EnableDashboardSpellCheck = chkChecklistSpell.Checked;
			Config.EnableDashboardValidation = chkChecklistValidation.Checked;
			Config.Set(Settings.AutoOpenCharacter, recAutoOpen.RecordKey);
			Config.MaxFranchisePartners = (int)valFranchise.Value;
			Config.AutoPopulateStageImages = chkAutoFill.Checked;
			Config.WarnAboutIncompleteStatus = chkWarnIncomplete.Checked;

			bool oldSafeMode = Config.SafeMode;
			Config.SafeMode = chkSafeMode.Checked;
			Config.ShowLegacyPoseTabs = chkLegacyPoses.Checked;

			HashSet<string> pauses = new HashSet<string>();
			foreach (string item in lstPauses.CheckedItems)
			{
				pauses.Add(item);
			}
			Config.AutoPauseDirectives = pauses;

			HashSet<string> statusFilters = new HashSet<string>();
			foreach (string status in chkStatuses.CheckedItems)
			{
				statusFilters.Add(status);
			}
			Config.StatusFilters = statusFilters;

			DialogResult = DialogResult.OK;
			Config.Save();
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.SettingsUpdated);

			if (!oldSafeMode && Config.SafeMode)
			{
				Shell.Instance.RunChecks();
			}

			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void chkHideImages_CheckedChanged(object sender, EventArgs e)
		{
			txtFilter.Enabled = chkHidePrefixlessImages.Checked;
		}

		private void cmdBrowseKisekae_Click(object sender, EventArgs e)
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
			if (!VerifyApplicationDirectory(dir))
			{
				//try going up a level
				dir = Path.GetDirectoryName(original);
			}
			if (!VerifyApplicationDirectory(dir))
			{
				//Nope. How about down a level?
				bool succeed = false;
				foreach (string subfolder in Directory.EnumerateDirectories(original))
				{
					if (VerifyApplicationDirectory(subfolder))
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

		private void chkHideImages_CheckedChanged_1(object sender, EventArgs e)
		{
			Config.Set(Settings.HideImages, !chkHideImages.Checked);
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.ToggleImages);
		}

		private void cmdVerify_Click(object sender, EventArgs e)
		{
			string key = txtTinify.Text;
			Cursor = Cursors.WaitCursor;
			try
			{
				Tinify.Key = key;
				Tinify.Validate().Wait();
				MessageBox.Show($"API key is valid. Remaining compressions this month: {500 - Tinify.CompressionCount}", "Verify Tinify API Key", MessageBoxButtons.OK);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Verify Tinify API Key", MessageBoxButtons.OK);
			}
			Cursor = Cursors.Default;
		}

		private void chkDashboard_CheckedChanged(object sender, EventArgs e)
		{
			chkStartDashboard.Enabled = grpChecklist.Enabled = chkDashboard.Checked;
		}

		private void chkPreviewBubble_CheckedChanged(object sender, EventArgs e)
		{
			Config.Set(Settings.ShowPreviewText, chkPreviewBubble.Checked);
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.ToggleImages);
		}

		private void chkPreviewFormatting_CheckedChanged(object sender, EventArgs e)
		{
			Config.Set(Settings.DisablePreviewFormatting, !chkPreviewFormatting.Checked);
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.ToggleImages);
		}

		private void chkSafeMode_CheckedChanged(object sender, EventArgs e)
		{
			chkHideImages.Checked = !chkSafeMode.Checked;
		}
	}
}
