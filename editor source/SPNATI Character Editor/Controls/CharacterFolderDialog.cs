using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public class CharacterFolderDialog : Component
	{
		public CharacterFolderDialog()
		{
			InitializeComponent();
		}

		private FolderBrowserDialog folderBrowserDialog1;
		public string DirectoryName;

		private void InitializeComponent()
		{
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

		}

		public DialogResult ShowDialog(ISkin character, string subfolder)
		{
			string root = Config.SpnatiDirectory;
			string characterRoot = character.GetDirectory();
			string localPath = characterRoot;

			if (!string.IsNullOrEmpty(subfolder))
			{
				localPath = Path.Combine(localPath, subfolder);
			}
			folderBrowserDialog1.SelectedPath = localPath;

			DialogResult result = DialogResult.OK;
			bool valid = true;
			do
			{
				result = folderBrowserDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
					string newPath = folderBrowserDialog1.SelectedPath;
					valid = newPath.StartsWith(characterRoot);
					if (valid)
					{
						newPath = newPath.Substring(character.GetDirectory().Length);
						DirectoryName = subfolder.Replace("\\", "/");
					}
					else
					{
						MessageBox.Show("Invalid folder path for " + character.ToString());
					}
				}
			} while (result == DialogResult.OK && !valid);
			
			return result;
		}
	}
}
