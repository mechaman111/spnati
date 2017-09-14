using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class LoadCharacterPrompt : Form
	{
		private string _folder;
		public string FolderName
		{
			get { return _folder; }
			set
			{
				_folder = value;
				int index = CharacterDatabase.Characters.FindIndex(c => c.FolderName == value);
				cboCharacters.SelectedIndex = index;
			}
		}

		public LoadCharacterPrompt()
		{
			InitializeComponent();

			PopulateBox();
		}

		private void PopulateBox()
		{
			cboCharacters.DataSource = CharacterDatabase.Characters;
			cboCharacters.DisplayMember = "DisplayName";
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Character c = cboCharacters.SelectedItem as Character;
			if(c != null)
				FolderName = c.FolderName;
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
