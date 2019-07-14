using Desktop;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 215)]
	[Activity(typeof(Costume), 215)]
	public partial class ScreenshotTaker : Activity
	{
		private ISkin _character;
		private Dictionary<string, string> _extraData = new Dictionary<string, string>();

		public ScreenshotTaker()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Screenshot"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
		}

		private void cmdImport_Click(object sender, EventArgs e)
		{
			string file = Path.GetFileNameWithoutExtension(txtName.Text);
			if (string.IsNullOrEmpty(file))
			{
				MessageBox.Show("File name is blank.", "Import Screenshot", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			ImageCropper cropper = new ImageCropper();
			cropper.ImportUnprocessed(_extraData);
			
			if (cropper.ShowDialog() == DialogResult.OK)
			{
				Image importedImage = cropper.CroppedImage;
				if (importedImage != null)
				{
					SaveImage(file, importedImage);
					Shell.Instance.SetStatus($"{Path.Combine(_character.GetDirectory(), file + ".png")} created.");
				}
			}
		}

		private void SaveImage(string imageKey, Image image)
		{
			string filename = imageKey + ".png";
			string fullPath = Path.Combine(_character.GetDirectory(), filename);

			image.Save(fullPath);
		}

		private void cmdAdvanced_Click(object sender, EventArgs e)
		{
			PoseSettingsForm form = new PoseSettingsForm();
			form.SetData(_extraData);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_extraData = form.GetData();
			}
		}
	}
}
