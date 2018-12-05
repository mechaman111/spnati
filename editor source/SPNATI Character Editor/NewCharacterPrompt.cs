using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class NewCharacterPrompt : Form
	{
		private HashSet<string> _existing = new HashSet<string>();

		public string FolderName;

		public NewCharacterPrompt()
		{
			InitializeComponent();

			PopulateBox();
		}

		private void PopulateBox()
		{
			string dir = Path.Combine(Config.GetString(Settings.GameDirectory), "opponents");
			List<string> folders = new List<string>();
			foreach (var folder in Directory.EnumerateDirectories(dir))
			{
				string shortName = Path.GetFileName(folder);
				if (!File.Exists(Path.Combine(folder, "behaviour.xml")) && shortName != "human")
				{
					folders.Add(shortName);
				}
				else
				{
					_existing.Add(shortName);
				}
			}
			cboCharacters.DataSource = folders;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			string folder = cboCharacters.Text;
			if (cboCharacters.SelectedItem != null)
			{
				folder = cboCharacters.SelectedItem.ToString();
				if (_existing.Contains(folder))
				{
					MessageBox.Show("A character with this name already exists. Use the Open menu rather than New to edit them.");
					return;
				}
			}
			FolderName = folder.ToLower();
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
