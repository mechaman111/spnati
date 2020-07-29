using Desktop;
using Desktop.CommonControls;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class FolderSelectControl : PropertyEditControl
	{
		private ISkin _character;

		public FolderSelectControl()
		{
			InitializeComponent();
		}
		
		protected override void OnBoundData()
		{
			ISkin context = Context as ISkin;
			if (context == null)
			{
				throw new Exception("FolderSelectControl requires a Character context!");
			}
			_character = context.Character;

			string value = GetValue()?.ToString();

			txtValue.PlaceholderText = GetPreviewValue()?.ToString();
			txtValue.Text = value;
		}

		private void CmdBrowse_Click(object sender, EventArgs e)
		{
			string filename = txtValue.Text;
			if (string.IsNullOrEmpty(filename))
			{
				filename = GetPreviewValue()?.ToString() ?? "";
			}
			filename = filename.Replace("/", "\\");

			if (characterFolderDialog1.ShowDialog(_character, filename) == DialogResult.OK)
			{
				txtValue.Text = characterFolderDialog1.DirectoryName;
				Save();
			}
		}

		protected override void OnClear()
		{
			txtValue.Text = "";
			Save();
		}

		protected override void OnSave()
		{
			if (txtValue.Text == "")
			{
				SetValue(null);
			}
			else
			{
				SetValue(txtValue.Text);
			}
		}
	}

	public class FolderSelectAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(FolderSelectControl); }
		}
	}
}
