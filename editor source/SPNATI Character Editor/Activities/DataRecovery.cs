using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	public partial class DataRecovery : SkinnedForm
	{
		private Character _character;
		public Character RecoveredCharacter { get; private set; }

		public DataRecovery()
		{
			InitializeComponent();
			recCharacter.RecordType = typeof(Character);
			recCharacter.RecordFilter = Filter;
		}

		private bool Filter(IRecord record)
		{
			Character character = record as Character;
			if (character.FolderName == "human")
			{
				return false;
			}
			return true;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public void SetCharacter(Character character)
		{
			recCharacter.Record = character;
		}

		public void SetCharacter(string name)
		{
			lblCharacter.Visible = false;
			recCharacter.Visible = false;
			lblCorrupt.Text = string.Format(lblCorrupt.Text, name);
			lblCorrupt.Visible = true;
			_character = new Character();
			_character.FolderName = name;
			pnlRecovery.Enabled = true;
			LoadSnapshots();
		}

		private void recCharacter_RecordChanged(object sender, RecordEventArgs e)
		{
			_character = recCharacter.Record as Character;
			pnlRecovery.Enabled = _character != null;
			LoadSnapshots();
		}

		private void LoadSnapshots()
		{
			cmdRecover.Enabled = false;
			lstSnapshots.Items.Clear();
			if (_character == null) { return; }

			//Files needed for a full snapshot
			string[] files = new string[] { "behaviour", "meta" };
			string root = files[0];
			string dir = Config.GetBackupDirectory(_character);
			if (!Directory.Exists(dir)) { return; }

			List<string> recoveryFiles = new List<string>();

			foreach (string file in Directory.EnumerateFiles(dir))
			{
				string filename = Path.GetFileNameWithoutExtension(file);
				string extension = Path.GetExtension(file);
				if (extension != ".bak")
				{
					continue;
				}
				string timestamp = filename.Substring(root.Length + 1);
				if (filename.StartsWith(root))
				{
					bool valid = true;
					//make sure the other required files exist
					for (int i = 1; i < files.Length; i++)
					{
						if (!File.Exists(Path.Combine(dir, $"{files[i]}-{timestamp}{extension}")))
						{
							valid = false;
							break;
						}
					}
					if (valid)
					{
						recoveryFiles.Add(file);
					}
				}
			}

			recoveryFiles.Sort((string f1, string f2) => File.GetLastWriteTime(f2).CompareTo(File.GetLastWriteTime(f1)));
			foreach (string file in recoveryFiles)
			{
				string filename = Path.GetFileNameWithoutExtension(file);
				string timestamp = filename.Substring(root.Length + 1);
				string lineCount = "CORRUPTED";
				string endingCount = "CORRUPTED";
				string poseCount = "CORRUPTED";
				Character c = Serialization.ImportXml<Character>(file);
				if (c != null)
				{
					lineCount = c.GetUniqueLineCount().ToString();
					endingCount = c.Endings.Count.ToString();
					poseCount = c.Poses.Count.ToString();
				}

				ListViewItem item = new ListViewItem(new string[] {
						DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", null).ToString(),
						lineCount,
						endingCount,
						poseCount,
					});
				item.Tag = timestamp;
				if (lineCount == "CORRUPTED")
				{
					item.Tag = null;
					item.ForeColor = System.Drawing.Color.Red;
				}
				lstSnapshots.Items.Add(item);
			}
		}

		private void lstSnapshots_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdRecover.Enabled = false;
			if (lstSnapshots.SelectedItems.Count != 1)
			{
				return;
			}
			string timestamp = lstSnapshots.SelectedItems[0]?.Tag?.ToString();
			if (string.IsNullOrEmpty(timestamp))
			{
				return;
			}
			cmdRecover.Enabled = true;

		}

		private void cmdRecover_Click(object sender, EventArgs e)
		{
			if (_character == null) { return; }
			string timestamp = lstSnapshots.SelectedItems[0]?.Tag?.ToString();
			Character recovered = Serialization.RecoverCharacter(_character, timestamp);
			RecoveredCharacter = recovered;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
