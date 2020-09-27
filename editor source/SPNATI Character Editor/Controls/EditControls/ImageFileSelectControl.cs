using Desktop;
using Desktop.CommonControls;
using System;
using System.Windows.Forms;

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
			openFileDialog1.UseAbsolutePaths = (context.Context == CharacterContext.Pose || context.Context == CharacterContext.Collectible);
			openFileDialog1.IncludeOpponents = context.Context == CharacterContext.Collectible;
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

			if (openFileDialog1.ShowDialog(_character, filename) == DialogResult.OK)
			{
				txtValue.Text = openFileDialog1.FileName;
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
				string newValue = GetValue() as string;
				if (newValue != txtValue.Text)
				{
					txtValue.Text = newValue;
				}
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
		Pose,
		Collectible
	}
}
