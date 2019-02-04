using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public class CharacterImageDialog : Component
	{
		public CharacterImageDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// If true, all paths in both character directory and others will be based on "opponents"
		/// </summary>
		public bool UseAbsolutePaths { get; set; }

		private OpenFileDialog openFileDialog1;

		public string FileName;

		public string Filter
		{
			get { return openFileDialog1.Filter; }
			set { openFileDialog1.Filter = value; }
		}

		private void InitializeComponent()
		{
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			// 
			// openFileDialog
			// 
			this.openFileDialog1.FileName = "openFileDialog";
		}

		public DialogResult ShowDialog(ISkin character, string filename)
		{
			string root = Config.SpnatiDirectory;
			string localPath = character.GetDirectory();

			if (!string.IsNullOrEmpty(filename))
			{
				if (UseAbsolutePaths)
				{
					openFileDialog1.InitialDirectory = Path.Combine(root, "opponents", Path.GetDirectoryName(filename));
				}
				else if (filename.StartsWith("\\"))
				{
					//absolute path
					openFileDialog1.InitialDirectory = Path.Combine(root, Path.GetDirectoryName(filename).Substring(1));
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
			openFileDialog1.FileName = Path.GetFileName(filename);

			DialogResult result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				filename = openFileDialog1.FileName;
				if (UseAbsolutePaths)
				{
					string opponentRoot = Path.Combine(root, "opponents") + "\\";
					filename = filename.Substring(opponentRoot.Length);
				}
				else
				{
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
				}
				FileName = filename.Replace("\\", "/");
			}
			return result;
		}
	}
}
