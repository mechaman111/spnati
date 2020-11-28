using Desktop;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 215, DelayRun = true, Caption = "Images")]
	[Activity(typeof(Costume), 215, DelayRun = true, Caption = "Images")]
	public partial class ScreenshotTaker : Activity
	{
		private ISkin _character;
		private Dictionary<string, string> _extraData = new Dictionary<string, string>();

		private readonly Image EmptyImage = new Bitmap(1, 1);

		public ScreenshotTaker()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Images"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
		}

		protected override void OnActivate()
		{
			PopulateFileList();
		}

		private void PopulateFileList()
		{
			gridFiles.Rows.Clear();
			string dir = _character.GetDirectory();
			DirectoryInfo directory = new DirectoryInfo(dir);
			foreach (FileInfo file in directory.EnumerateFiles()
				.Where(f => f.Extension == ".png"))
			{
				DataGridViewRow row = gridFiles.Rows[gridFiles.Rows.Add()];
				row.Cells[0].Value = file.Name;
				row.Cells[1].Value = ToKB(file.Length);
				row.Cells[2].Value = IsCompressed(file.Name) ? Properties.Resources.Checkmark : EmptyImage;
			}
		}

		private string ToKB(long length)
		{
			double size = length / 1024.0;
			return $"{Math.Round(size, 2)} KB";
		}

		private bool IsCompressed(string filename)
		{
			string dir = Path.Combine(_character.GetBackupDirectory(), "images");
			if (!Directory.Exists(dir))
			{
				return false;
			}
			string path = Path.Combine(dir, filename);
			return File.Exists(path);
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

			try
			{
				string compressionPath = Path.Combine(_character.GetBackupDirectory(), "images", filename);
				if (File.Exists(compressionPath))
				{
					File.Delete(compressionPath);
				}

				image.Save(fullPath);
			}
			catch (Exception ex)
			{
				ErrorLog.LogError(ex.ToString());
			}
			PopulateFileList();
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

		private void cmdCompressAll_Click(object sender, EventArgs e)
		{
			List<string> images = CompileCompressionList((file, row) =>
			{
				return !IsCompressed(file);
			});
			Compress(images);
		}

		private void cmdCompressSelected_Click(object sender, EventArgs e)
		{
			List<string> images = CompileCompressionList((file, row) =>
			{
				return row.Selected;
			});
			Compress(images);
		}

		private void cmdMarkCompressed_Click(object sender, EventArgs e)
		{
			List<string> images = CompileCompressionList((file, row) =>
			{
				return row.Selected;
			});
			string srcDir = _character.GetDirectory();
			string dir = Path.Combine(_character.GetBackupDirectory(), "images");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			foreach (string file in images)
			{
				string sourcePath = Path.Combine(srcDir, file);
				string compressedPath = Path.Combine(dir, file);
				try
				{
					if (!File.Exists(compressedPath))
					{
						File.Copy(sourcePath, compressedPath);
					}
				}
				catch { }
			}
			PopulateFileList();
		}

		private List<string> CompileCompressionList(Func<string, DataGridViewRow, bool> filter)
		{
			List<string> output = new List<string>();
			for (int i = 0; i < gridFiles.Rows.Count; i++)
			{
				DataGridViewRow row = gridFiles.Rows[i];
				string file = row.Cells[0].Value?.ToString();
				if (string.IsNullOrEmpty(file) || !filter(file, row))
				{
					continue;
				}
				output.Add(file);
			}
			return output;
		}

		private void Compress(List<string> files)
		{
			ProgressForm progressForm = new ProgressForm();
			progressForm.Text = "Compress Images";
			progressForm.Show(this);

			int maxCount = files.Count;
			var progressUpdate = new Progress<int>(value => progressForm.SetProgress(string.Format("Compressing {0}...", files[value]), value, maxCount));

			progressForm.Shown += async (s, args) =>
			{
				var cts = new CancellationTokenSource();
				progressForm.SetCancellationSource(cts);
				try
				{
					int result = await CompressAsync(progressUpdate, files, cts.Token);
					if (result < 0)
					{
						MessageBox.Show($"An error occurred during compression.", "Compress Images", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						Shell.Instance.SetStatus($"Compressed {maxCount} images.");
					}
				}
				finally
				{
					progressForm.Close();
				}

				PopulateFileList();
			};
		}

		private Task<int> CompressAsync(IProgress<int> progress, List<string> files, CancellationToken ct)
		{
			return Task.Run(() =>
			{
				try
				{
					IImageCompressor compressor = new TinifyCompressor();
					string dir = _character.GetDirectory();
					int current = 0;
					bool hasErrors = false;
					foreach (string file in files)
					{
						progress.Report(current++);

						string fullPath = Path.Combine(dir, file);
						if (!compressor.Compress(fullPath, _character))
						{
							break;
						}

						ct.ThrowIfCancellationRequested();
					}

					return hasErrors ? -1 : 1;
				}
				catch (OperationCanceledException)
				{
					return 0;
				}
			}, ct);
		}
	}
}
