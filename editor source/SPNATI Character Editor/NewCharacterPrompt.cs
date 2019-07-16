using Desktop.Skinning;
using SPNATI_Character_Editor.Providers;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class NewCharacterPrompt : SkinnedForm
	{
		public Character Character { get; private set; }

		public NewCharacterPrompt()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			string name = txtName.Text;
			string[] names = name.Split(new char[] { ' ' }, 2);
			string firstName = names[0];
			string surNames = "";
			if (names.Length > 1)
			{
				surNames = names[1];
			}

			string folderName = firstName.ToLower();

			Character existing = CharacterDatabase.Get(folderName);
			if (existing != null && !string.IsNullOrEmpty(surNames))
			{
				folderName = $"{folderName}_{surNames.ToLower()}";
				existing = CharacterDatabase.Get(folderName);
			}
			if (existing != null)
			{
				int suffix = 1;
				do
				{
					folderName = $"{firstName.ToLower()}{suffix++}";
					existing = CharacterDatabase.Get(folderName);
				}
				while (existing != null);
			}

			CharacterProvider provider = new CharacterProvider();
			Character = provider.Create(folderName) as Character;
			Character.Label = firstName;
			Character.FirstName = firstName;
			if (!string.IsNullOrEmpty(surNames))
			{
				Character.LastName = surNames;
			}

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
