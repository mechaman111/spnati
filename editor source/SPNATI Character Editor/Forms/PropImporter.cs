using Desktop.Skinning;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class PropImporter : SkinnedForm
	{
		private string _imagesDirectory;

		public PropImporter()
		{
			InitializeComponent();
		}

		public void SetData(List<string> missingImages, string imagesDir)
		{
			_imagesDirectory = imagesDir;
			foreach (string image in missingImages)
			{
				gridMissingImages.Rows.Add(image, null, "Browse...");
			}
		}

		private void gridMissingImages_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 2 && e.RowIndex >= 0)
			{
				DataGridViewRow row = gridMissingImages.Rows[e.RowIndex];
				string image = row.Cells[0].Value?.ToString();
				if (string.IsNullOrEmpty(image))
				{
					return;
				}

				string extension = Path.GetExtension(image);
				openFileDialog1.Filter = $"{extension} files|*{extension}";
				openFileDialog1.FileName = image;
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					string sourceFile = openFileDialog1.FileName;

					string destFile = Path.Combine(_imagesDirectory, image);

					//Need to throw away an existing image so we can replace it
					Image currentImage = row.Tag as Image;
					if (currentImage != null)
					{
						row.Cells[1].Value = null;
						currentImage.Dispose();
						File.Delete(destFile);
					}

					try
					{
						File.Copy(sourceFile, destFile);
					}
					catch { }
					try
					{
						Image thumbnail = new Bitmap(destFile);
						UpdateThumbnails(image, thumbnail);
					}
					catch {
						//use some placeholder if it failed
						Image thumbnail = new Bitmap(50, 50);
						using (Graphics g = Graphics.FromImage(thumbnail))
						{
							using (Pen pen = new Pen(Brushes.DarkGreen, 5))
							{
								g.DrawLine(pen, 5, 30, 15, 42);
								g.DrawLine(pen, 15, 38, 43, 10);
							}
						}
						UpdateThumbnails(image, thumbnail);
					}

					EnableOK();
				}
			}
		}

		private void UpdateThumbnails(string image, Image thumbnail)
		{
			foreach (DataGridViewRow row in gridMissingImages.Rows)
			{
				string rowImage = row.Cells[0].Value?.ToString();
				if (rowImage == image)
				{
					row.Cells[1].Value = thumbnail;
					row.Tag = thumbnail;
				}
			}

			EnableOK();
		}

		private void EnableOK()
		{
			foreach (DataGridViewRow row in gridMissingImages.Rows)
			{
				Image img = row.Cells[1].Value as Image;
				if (img == null)
				{
					cmdOK.Enabled = false;
					lblReady.Visible = false;
					return;
				}
			}
			lblReady.Visible = true;
			cmdOK.Enabled = true;
		}

		private void PropImporter_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!cmdOK.Enabled && DialogResult != DialogResult.Cancel)
			{
				e.Cancel = true;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void PropImporter_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (DataGridViewRow row in gridMissingImages.Rows)
			{
				Image img = row.Cells[1]?.Value as Image;
				if (img != null)
				{
					row.Cells[1].Value = null;
					img.Dispose();
				}
			}
		}

		
	}
}
