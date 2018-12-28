using System;
using System.IO;
using System.Windows.Forms;
using Desktop;
using Desktop.CommonControls;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ImageFileSelectControl : PropertyEditControl
	{
		private Character _character;

		public ImageFileSelectControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			FileSelectAttribute p = parameters as FileSelectAttribute;
			openFileDialog1.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files|*.*";
		}

		protected override void OnBoundData()
		{
			EpilogueContext context = Context as EpilogueContext;
			if (context == null || context.Character == null)
			{
				throw new Exception("ImageFileSelectControl requires a Character context!");
			}
			_character = context.Character;

			txtValue.Text = GetValue()?.ToString();
		}

		private void CmdBrowse_Click(object sender, EventArgs e)
		{
			string filename = txtValue.Text;
			filename = filename.Replace("/", "\\");

			string root = Path.GetDirectoryName(Application.ExecutablePath);
			string localPath = Path.Combine(root, "opponents", _character.FolderName);

			if (!string.IsNullOrEmpty(filename))
			{
				if (filename.StartsWith("/"))
				{
					//absolute path
					openFileDialog1.InitialDirectory = root;
				}
				else
				{
					//relative path
					openFileDialog1.InitialDirectory = localPath;
				}
			}
			else
			{
				openFileDialog1.InitialDirectory = localPath;
			}
			openFileDialog1.FileName = filename;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				filename = openFileDialog1.FileName;
				if (filename.StartsWith(localPath))
				{
					//file is in character's folder
					filename = filename.Substring(localPath.Length + 1);
				}
				else if (filename.StartsWith(root))
				{
					//file is in another game folder
					filename = filename.Substring(root.Length);
				}
				else
				{
					//file is outside the game, so copy it in (otherwise it won't be available on the website)
					File.Copy(filename, Path.Combine(localPath, Path.GetFileName(filename)));
					filename = Path.GetFileName(filename);
				}
				filename = filename.Replace("\\", "/");
				txtValue.Text = filename;
				Save();
			}
		}

		public override void Clear()
		{
			txtValue.Text = "";
			Save();
		}

		public override void Save()
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

	public class FileSelectAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ImageFileSelectControl); }
		}
	}
}
