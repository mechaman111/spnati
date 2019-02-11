using System;
using System.IO;
using System.Windows.Forms;
using Desktop;
using Desktop.CommonControls;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ImageFileSelectControl : PropertyEditControl
	{
		private ISkin _character;

		public ImageFileSelectControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			openFileDialog1.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files|*.*";
		}

		protected override void OnBoundData()
		{
			ICharacterContext context = Context as ICharacterContext;
			if (context == null || context.Character == null)
			{
				throw new Exception("ImageFileSelectControl requires a Character context!");
			}
			openFileDialog1.UseAbsolutePaths = (context.Context == CharacterContext.Pose);
			_character = context.Character;

			txtValue.Text = GetValue()?.ToString();
		}

		private void CmdBrowse_Click(object sender, EventArgs e)
		{
			string filename = txtValue.Text;
			filename = filename.Replace("/", "\\");

			if (openFileDialog1.ShowDialog(_character, filename) == DialogResult.OK)
			{
				txtValue.Text = openFileDialog1.FileName;
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

	public interface ICharacterContext
	{
		ISkin Character { get; }
		CharacterContext Context { get; }
	}

	public enum CharacterContext
	{
		Epilogue,
		Pose
	}
}
