using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Selection form for choosing a destination to save a character
	/// </summary>
	public partial class FileNameSelect : Form
	{
		private string _folderName;
		public string FolderName
		{
			get { return _folderName; }
			set
			{
				_folderName = value;
				txtFile.Text = value;
			}
		}

		public FileNameSelect()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			string name = txtFile.Text;
			string folder = Config.GetRootDirectory(name);

			if (name != FolderName && Directory.Exists(folder))
			{
				string text = string.Format("{0} already exists. Do you really want to overwrite this character?", name);
				if (MessageBox.Show(text, "Save As", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					return;
				}
			}
			FolderName = name;

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
