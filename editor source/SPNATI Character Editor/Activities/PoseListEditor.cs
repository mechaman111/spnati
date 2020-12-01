using Desktop;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 200, DelayRun = true, Caption = "Pose Lists")]
	[Activity(typeof(Costume), 200, DelayRun = true, Caption = "Pose Lists")]
	public partial class PoseListEditor : Activity
	{
		private const int MiniHeight = 200;
		private ImageMetadata _previewImageMetadata;
		private ISkin _character;
		private PoseList _poseList = new PoseList();
		private string _lastPoseFile;
		private DataGridViewColumn _sortedColumn;
		private bool _sortAscending;
		private Dictionary<string, Image> _thumbnails = new Dictionary<string, Image>();

		private ImageMetadata _clipboard;

		private readonly Image EmptyImage = new Bitmap(1, 1);

		public override string Caption
		{
			get { return "Pose Lists"; }
		}

		public PoseListEditor()
		{
			InitializeComponent();
			ColImport.Flat = ColAdvanced.Flat = true;
			ColImage.DefaultCellStyle.NullValue = null;
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
			if (_character is Costume)
			{
				chkRequired.Checked = true;
				chkRequired.CheckedChanged += chkRequired_CheckedChanged;
			}
			else
			{
				chkRequired.Visible = false;
			}
		}

		protected override void OnFirstActivate()
		{
			string defaultFileName = Path.Combine(_character.GetDirectory(), "poses.txt");
			if (File.Exists(defaultFileName))
			{
				ImportPoseList(defaultFileName);
			}
			PopulatePoseGrid();
		}

		public override void Quit()
		{
			CleanUpGrid();
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length >= 1)
			{
				PoseList list = parameters[0] as PoseList;
				_poseList = list;
				PopulatePoseGrid();
				lblCurrentPoseFile.Text = "From template";
			}
		}

		/// <summary>
		/// Clears all poses in the grid
		/// </summary>
		private void ClearPoses()
		{
			CleanUpGrid();
			_poseList.Poses.Clear();
		}

		/// <summary>
		/// Imports a pose list from a file
		/// </summary>
		/// <param name="filename"></param>
		private void ImportPoseList(string filename)
		{
			_poseList = new PoseList();
			string[] lines = File.ReadAllLines(filename);

			Rect cropInfo = new Rect(0, 0, 600, 1400);

			foreach (string line in lines)
			{
				string trimmedLine = line.Trim();
				if (trimmedLine.StartsWith("#") || string.IsNullOrEmpty(trimmedLine))
					continue;

				string[] values = trimmedLine.Split('|');
				trimmedLine = values[0];
				string[] pieces = trimmedLine.Split('=');

				if (pieces.Length < 2)
					continue;

				if (pieces[0] == "crop_pixels")
				{
					//Update global crop info
					string[] pixels = pieces[1].Split(',');
					if (pixels.Length != 4)
						continue;
					int l = 0;
					int t = 0;
					int r = 0;
					int b = 0;
					if (!int.TryParse(pixels[0], out l) || !int.TryParse(pixels[1], out t) || !int.TryParse(pixels[2], out r) || !int.TryParse(pixels[3], out b))
						continue;
					cropInfo = new Rect(l, t, r, b);
					continue;
				}

				string key = pieces[0];
				//TODO: check for crop_images
				string data = pieces[1];

				ImageMetadata metadata = new ImageMetadata(key, data);
				metadata.CropInfo = cropInfo;

				if (values.Length > 1)
				{
					string[] extraPieces = values[1].Split(',');
					foreach (string extra in extraPieces)
					{
						string[] kvp = extra.Split('=');
						string partKey = kvp[0];
						int part;
						if (int.TryParse(partKey, out part))
						{
							KisekaePart kkPart = (KisekaePart)part;
							string partValue = kvp[1];
							metadata.ExtraData[kkPart.Serialize()] = partValue;
						}
					}
				}
				if (values.Length > 2)
				{
					string skipPreprocessing = values[2];
					metadata.SkipPreprocessing = (skipPreprocessing == "1");
				}

				_poseList.Poses.Add(metadata);
			}

			lblCurrentPoseFile.Text = Path.GetFileName(filename);
		}

		/// <summary>
		/// Exports the pose grid to a file
		/// </summary>
		/// <param name="filename">Full file path of the file to create/replace</param>
		private void ExportPoseList(string filename)
		{
			//First create the pose list from the grid
			MakePoseList();
			List<string> lines = new List<string>();

			Rect currentCrop = new Rect(0, 0, 600, 1400);
			foreach (ImageMetadata metadata in _poseList.Poses)
			{
				Rect crop = metadata.CropInfo;
				if (currentCrop != crop)
				{
					//If new crop values are found, put them in
					lines.Add(string.Format("crop_pixels={0}", crop.Serialize()));
					currentCrop = crop;
				}
				lines.Add(metadata.Serialize());
			}
			File.WriteAllLines(filename, lines);
			lblCurrentPoseFile.Text = Path.GetFileName(filename);
		}

		/// <summary>
		/// Builds the PoseList data structure from the info in the grid
		/// </summary>
		private void MakePoseList()
		{
			_poseList = new PoseList();
			foreach (DataGridViewRow row in gridPoses.Rows)
			{
				ImageMetadata metadata = ReadRow(row);
				if (metadata != null)
				{
					_poseList.Poses.Add(metadata);
				}
			}
		}

		private ImageMetadata ReadRow(DataGridViewRow row)
		{
			string stage = row.Cells["ColStage"].Value?.ToString();
			string pose = row.Cells["ColPose"].Value?.ToString();
			if (string.IsNullOrEmpty(pose))
				return null;
			int l = 0;
			int r = 0;
			int t = 0;
			int b = 0;
			int.TryParse(row.Cells["ColL"].Value?.ToString(), out l);
			if (!int.TryParse(row.Cells["ColR"].Value?.ToString(), out r))
				r = 600;
			int.TryParse(row.Cells["ColT"].Value?.ToString(), out t);
			if (!int.TryParse(row.Cells["ColB"].Value?.ToString(), out b))
				b = 1400;

			string data = row.Cells["ColData"].Value?.ToString();
			if (string.IsNullOrEmpty(data))
				return null;

			Dictionary<string, string> extraData = row.Cells["ColAdvanced"].Tag as Dictionary<string, string>;

			string key = GetKey(stage, pose);
			ImageMetadata metadata = new ImageMetadata(key, data);
			metadata.ExtraData = extraData;
			metadata.CropInfo = new Rect(l, t, r, b);
			metadata.SkipPreprocessing = row.Cells["ColData"].Tag?.ToString() == "1";
			return metadata;
		}

		/// <summary>
		/// Builds a filename from a stage and pose/emotion
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="pose"></param>
		/// <returns></returns>
		private static string GetKey(string stage, string pose)
		{
			if (string.IsNullOrEmpty(stage))
			{
				return pose;
			}
			return string.Format("{0}-{1}", stage, pose);
		}

		private void CleanUpGrid()
		{
			gridPoses.Rows.Clear();
			foreach (Image img in _thumbnails.Values)
			{
				img.Dispose();
			}
			_thumbnails.Clear();
		}

		/// <summary>
		/// Populates the pose grid
		/// </summary>
		private void PopulatePoseGrid()
		{
			//pull in required images
			HashSet<string> requiredImages = _character.GetRequiredPoses(false);
			if (requiredImages == null)
			{
				requiredImages = new HashSet<string>();
			}

			DataGridViewImageColumn imageCol = gridPoses.Columns["ColImage"] as DataGridViewImageColumn;
			imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

			const int InitialImageThreshold = 20; //number of images to initially display before incrementally loading the rest
			int imageIndex = 0;

			try
			{
				CleanUpGrid();
				foreach (ImageMetadata pose in _poseList.Poses)
				{
					requiredImages.Remove(pose.ImageKey);
					DataGridViewRow row = gridPoses.Rows[gridPoses.Rows.Add()];
					WriteRow(pose, row);
					if (imageIndex < InitialImageThreshold)
					{
						imageIndex++;
						UpdateImageCell(pose.ImageKey, row);
					}
				}
			}
			catch
			{
				MessageBox.Show("Error importing pose list. Is this actually a poses file?", "Import Pose List", MessageBoxButtons.OK, MessageBoxIcon.Error);
				gridPoses.Rows.Clear();
			}

			//add rows for required poses
			if (chkRequired.Checked)
			{
				List<string> remaining = new List<string>();
				remaining.AddRange(requiredImages);
				remaining.Sort();
				foreach (string pose in remaining)
				{
					bool imageExists = File.Exists(Path.Combine(_character.GetDirectory(), pose + ".png")) || File.Exists(Path.Combine(_character.GetDirectory(), pose + ".gif"));
					if (imageExists)
					{
						continue;
					}
					DataGridViewRow row = gridPoses.Rows[gridPoses.Rows.Add()];
					WriteRow(new ImageMetadata(pose, ""), row);
					if (imageIndex < InitialImageThreshold)
					{
						imageIndex++;
						UpdateImageCell(pose, row);
					}
				}
			}
		}

		private static void WriteRow(ImageMetadata pose, DataGridViewRow row)
		{
			string[] piecedKey = pose.ImageKey.Split(new char[] { '-' }, 2);
			if (piecedKey.Length == 0)
			{
				row.Cells["ColStage"].Value = "0";
				row.Cells["ColPose"].Value = pose.ImageKey;
			}
			else if (piecedKey.Length == 1)
			{
				row.Cells["ColStage"].Value = "";
				row.Cells["ColPose"].Value = piecedKey[0];
			}
			else
			{
				row.Cells["ColStage"].Value = piecedKey[0];
				row.Cells["ColPose"].Value = piecedKey[1];
			}

			row.Cells["ColL"].Value = pose.CropInfo.Left;
			row.Cells["ColR"].Value = pose.CropInfo.Right;
			row.Cells["ColT"].Value = pose.CropInfo.Top;
			row.Cells["ColB"].Value = pose.CropInfo.Bottom;

			row.Cells["ColData"].Value = pose.Data;
			row.Cells["ColAdvanced"].Tag = pose.ExtraData;

			row.Cells["ColData"].Tag = (pose.SkipPreprocessing ? "1" : null);
		}

		private Image GetThumbnail(string key)
		{
			Image thumbnail;
			if (!_thumbnails.TryGetValue(key, out thumbnail))
			{
				string file = Path.Combine(_character.GetDirectory(), key + ".png");
				if (File.Exists(file))
				{
					using (Image fullSize = new Bitmap(file))
					{
						float aspect = (float)fullSize.Width / fullSize.Height;
						int width = (int)(aspect * MiniHeight);
						thumbnail = new Bitmap(fullSize, new Size(width, MiniHeight));
						_thumbnails[key] = thumbnail;
					}
				}
			}
			return thumbnail;
		}

		private void UpdateImageCell(string key, DataGridViewRow row)
		{
			Image thumbnail = GetThumbnail(key);
			DataGridViewImageCell imageCell = row.Cells["ColImage"] as DataGridViewImageCell;
			if (thumbnail != null)
			{
				imageCell.Value = thumbnail;
				row.Cells["ColImport"].Value = "Reimport";
			}
			else
			{
				imageCell.Value = EmptyImage;
				row.Cells["ColImport"].Value = "Import";
			}
		}

		/// <summary>
		/// Handles the Import buttons within the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewButtonColumn col = gridPoses.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
			if (col != null && e.RowIndex >= 0)
			{
				if (col == ColImport)
				{
					ImportImageForCropping(e.RowIndex);
				}
				else if (col == ColAdvanced)
				{
					ShowAdvancedSettings(e.RowIndex);
				}
			}
		}

		/// <summary>
		/// Shows the form for setting things like transparencies
		/// </summary>
		/// <param name="index">Selected row index</param>
		private void ShowAdvancedSettings(int index)
		{
			PoseSettingsForm form = new PoseSettingsForm();
			DataGridViewRow row = gridPoses.Rows[index];
			Dictionary<string, string> extraData = row.Cells["ColAdvanced"].Tag as Dictionary<string, string>;
			bool manual = row.Cells["ColData"].Tag?.ToString() == "1";
			form.SetData(extraData);
			form.UseManualCode = manual;
			if (form.ShowDialog() == DialogResult.OK)
			{
				row.Cells["ColAdvanced"].Tag = form.GetData();
				row.Cells["ColData"].Tag = (form.UseManualCode ? "1" : "");
			}
		}

		/// <summary>
		/// Imports an image from Kisekae and brings up the cropper
		/// </summary>
		/// <param name="index">Pose grid row index containing the metadata of the pose to import</param>
		private void ImportImageForCropping(int index)
		{
			DataGridViewRow row = gridPoses.Rows[index];

			string stage = row.Cells["ColStage"].Value?.ToString();
			string pose = row.Cells["ColPose"].Value?.ToString();
			if (string.IsNullOrEmpty(pose))
			{
				MessageBox.Show("Pose must be filled out.", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string data = row.Cells["ColData"].Value?.ToString();
			if (string.IsNullOrEmpty(data))
			{
				MessageBox.Show("Code must be filled out.", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string key = GetKey(stage, pose);

			_previewImageMetadata = new ImageMetadata(key, data);
			int l = 0;
			int r = 0;
			int t = 0;
			int b = 0;
			int.TryParse(row.Cells["ColL"].Value?.ToString(), out l);
			if (!int.TryParse(row.Cells["ColR"].Value?.ToString(), out r))
				r = 600;
			int.TryParse(row.Cells["ColT"].Value?.ToString(), out t);
			if (!int.TryParse(row.Cells["ColB"].Value?.ToString(), out b))
				b = 1400;
			_previewImageMetadata.CropInfo = new Rect(l, t, r, b);
			_previewImageMetadata.ExtraData = row.Cells["ColAdvanced"].Tag as Dictionary<string, string> ?? new Dictionary<string, string>();

			bool manual = row.Cells["ColData"].Tag?.ToString() == "1";
			_previewImageMetadata.SkipPreprocessing = manual;
			ImageCropper cropper = new ImageCropper();
			cropper.Import(_previewImageMetadata, _character, false);
			if (cropper.ShowDialog() == DialogResult.OK)
			{
				Image importedImage = cropper.CroppedImage;
				if (importedImage != null)
				{
					SaveImage(key, importedImage);
					UpdateImageCell(key, row);
					Rect crop = cropper.CroppingRegion;
					row.Cells["ColL"].Value = crop.Left;
					row.Cells["ColR"].Value = crop.Right;
					row.Cells["ColT"].Value = crop.Top;
					row.Cells["ColB"].Value = crop.Bottom;
				}
			}
		}

		/// <summary>
		/// Clears out red Xs from rows that have no image yet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			DataGridViewRow row = gridPoses.Rows[e.RowIndex];
			if (row.IsNewRow)
			{
				row.Cells["ColImage"].Value = EmptyImage;
			}
		}

		/// <summary>
		/// Defaults values for new poses in the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			for (int i = 0; i < e.RowCount; i++)
			{
				int index = e.RowIndex + i;
				DataGridViewRow row = gridPoses.Rows[index];
				row.Cells["ColImport"].Value = "Import";
				row.Cells["ColL"].Value = "0";
				row.Cells["ColT"].Value = "0";
				row.Cells["ColR"].Value = "600";
				row.Cells["ColB"].Value = "1400";
				row.Cells["ColAdvanced"].Tag = new Dictionary<string, string>();
			}
		}

		/// <summary>
		/// Wrapper around a file dialog to force the file selection to be within the character's folder
		/// </summary>
		/// <param name="dialog">Dialog to open</param>
		/// <returns></returns>
		private DialogResult ChooseFileInDirectory(FileDialog dialog, ref string file)
		{
			string dir = _character.GetDirectory();
			dir = dir.Replace('/', '\\');
			if (dir.EndsWith("\\"))
			{
				dir = dir.Substring(0, dir.Length - 1);
			}
			dialog.InitialDirectory = dir;
			dialog.FileName = Path.GetFileName(file);
			DialogResult result = DialogResult.OK;
			bool invalid;
			do
			{
				invalid = false;
				result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (Path.GetDirectoryName(dialog.FileName) != dir)
					{
						MessageBox.Show("Images need to be in the character's folder.", "Import Images", MessageBoxButtons.OK, MessageBoxIcon.Error);
						invalid = true;
					}
				}
			}
			while (invalid);
			if (result == DialogResult.OK)
				file = dialog.FileName;
			return result;
		}

		/// <summary>
		/// Click event for the Load Pose List button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImport_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(openFileDialog1, ref _lastPoseFile) == DialogResult.OK)
			{
				ImportPoseList(openFileDialog1.FileName);
				PopulatePoseGrid();
			}
		}

		/// <summary>
		/// Click event for the Save Pose List button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdExport_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(saveFileDialog1, ref _lastPoseFile) == DialogResult.OK)
			{
				ExportPoseList(saveFileDialog1.FileName);
			}
		}

		/// <summary>
		/// Click event for the Clear Poses button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will clear all poses and lose any unsaved changes. Are you sure you want to continue?", "Clear Pose List", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				ClearPoses();
			}
		}

		/// <summary>
		/// Click event for the Import New button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImportNew_Click(object sender, EventArgs e)
		{
			ImportUnloadedPoses();
		}

		/// <summary>
		/// Click event for the Import All button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImportAll_Click(object sender, EventArgs e)
		{
			ImportAllPoses();
		}

		/// <summary>
		/// Imports all pose data that doesn't have an image yet
		/// </summary>
		private void ImportUnloadedPoses()
		{
			MakePoseList();

			//Figure out which images need importing
			List<ImageMetadata> toImport = new List<ImageMetadata>();
			foreach (ImageMetadata metadata in _poseList.Poses)
			{
				string filename = metadata.ImageKey + ".png";
				string fullPath = Path.Combine(_character.GetDirectory(), filename);
				if (File.Exists(fullPath))
				{
					continue;
				}
				toImport.Add(metadata);
			}

			ImportPosesAsync(toImport);
		}

		/// <summary>
		/// Imports all poses, replacing existing images
		/// </summary>
		private void ImportAllPoses()
		{
			MakePoseList();
			ImportPosesAsync(_poseList.Poses);
		}

		/// <summary>
		/// Imports images asynchronously with a progress form
		/// </summary>
		/// <param name="list"></param>
		private void ImportPosesAsync(List<ImageMetadata> list)
		{
			//find all unrecognized props and assign them all up front
			List<KisekaeCode> codes = new List<KisekaeCode>();
			foreach (ImageMetadata metadata in list)
			{
				KisekaeCode code = new KisekaeCode(metadata.Data, true);
				if (code.TotalAssets > 0)
				{
					codes.Add(code);
				}
			}
			if (!CharacterGenerator.ImportUnrecognizedAssets(_character, codes))
			{
				return;
			}

			ProgressForm progressForm = new ProgressForm();
			progressForm.Text = "Import New Poses";
			progressForm.Show(this);

			int count = list.Count;
			var progressUpdate = new Progress<int>(value => progressForm.SetProgress(string.Format("Importing {0}...", list[value].ImageKey), value, count));

			progressForm.Shown += async (s, args) =>
			{
				var cts = new CancellationTokenSource();
				progressForm.SetCancellationSource(cts);
				try
				{
					int result = await ImportPoses(progressUpdate, list, cts.Token);
					if (result < 0)
					{
						MessageBox.Show("Imported with errors. See errorlog.txt for more information.", "Import Poses", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				finally
				{
					progressForm.Close();
				}
			};
		}

		/// <summary>
		/// Imports the provided pose data into images
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="importList"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		private Task<int> ImportPoses(IProgress<int> progress, List<ImageMetadata> importList, CancellationToken ct)
		{
			return Task.Run(async () =>
			{
				try
				{
					int current = 0;
					bool hasErrors = false;
					List<string> filesToCompress = new List<string>();
					foreach (ImageMetadata metadata in importList)
					{
						progress.Report(current++);
						try
						{
							Image img = await CharacterGenerator.GetCroppedImage(new KisekaeCode(metadata.Data), metadata.CropInfo, _character, metadata.ExtraData, metadata.SkipPreprocessing);
							if (img == null)
							{
								//Something went wrong. Stop here.
								FailedImport import = new FailedImport();
								import.ShowDialog();
								//MessageBox.Show("Couldn't import " + metadata.ImageKey + ". Is Kisekae running?", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return -1;
							}

							string savePath = SaveImage(metadata.ImageKey, img);

							//Update the relevant grid row
							MethodInvoker invoker = delegate ()
							{
								foreach (DataGridViewRow row in gridPoses.Rows)
								{
									string stage = row.Cells["ColStage"].Value?.ToString();
									string pose = row.Cells["ColPose"].Value?.ToString();
									if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
										continue;
									if (GetKey(stage, pose) == metadata.ImageKey)
									{
										UpdateImageCell(metadata.ImageKey, row);
										row.Cells["ColImport"].Value = "Reimport";
										break;
									}
								}
							};
							gridPoses.Invoke(invoker);
						}
						catch (Exception e)
						{
							hasErrors = true;
							ErrorLog.LogError(string.Format("Error importing from kisekae: {0}, {1}", metadata.ImageKey, e.Message));
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

		/// <summary>
		/// Saves an image to disk
		/// </summary>
		/// <param name="imageKey">Name of image (stage+pose)</param>
		/// <param name="image">Image to save</param>
		private string SaveImage(string imageKey, Image image)
		{
			string filename = imageKey + ".png";
			string fullPath = Path.Combine(_character.GetDirectory(), filename);

			try
			{
				image.Save(fullPath);

				_thumbnails.Remove(imageKey);
				_character.Character.PoseLibrary.Add(fullPath);

				string backup = Path.Combine(_character.GetBackupDirectory(), "images", filename);
				if (File.Exists(backup))
				{
					File.Delete(backup);
				}
			}
			catch { }

			return fullPath;
		}

		private async void gridPoses_Scroll(object sender, ScrollEventArgs e)
		{
			await Task.Delay(100);
			DoIncrementalLoad();
		}

		/// <summary>
		/// Loads in poses for all rows currently visible
		/// </summary>
		private void DoIncrementalLoad()
		{
			bool startedDisplay = false;
			int start = gridPoses.FirstDisplayedScrollingRowIndex;
			for (int i = start; i < gridPoses.Rows.Count; i++)
			{
				DataGridViewRow row = gridPoses.Rows[i];
				if (row.Displayed || startedDisplay)
				{
					Image current = row.Cells["ColImage"].Value as Image;
					if (current != null)
					{
						continue;
					}

					startedDisplay = true;
					string stage = row.Cells["ColStage"].Value?.ToString();
					string poseKey = row.Cells["ColPose"].Value?.ToString();
					if (string.IsNullOrEmpty(poseKey)) { continue; }

					string imageKey = string.IsNullOrEmpty(stage) ? poseKey : $"{stage}-{poseKey}";
					//Try to link this up with an existing image. Use thumbnails to save on memory since this activity could load hundreds at once
					UpdateImageCell(imageKey, row);

					if (!row.Displayed) { return; } //Displayed is false for a row that is partially visible, so we do one extra iteration to load it
				}
			}
		}

		private void CopySelectedLine()
		{
			if (gridPoses.SelectedRows.Count == 0) { return; }
			DataGridViewRow row = gridPoses.SelectedRows[0];
			_clipboard = ReadRow(row);
			Shell.Instance.SetStatus("Pose copied to clipboard.");
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gridPoses.SelectedRows.Count == 0)
			{
				if (gridPoses.SelectedCells.Count > 0)
				{
					DataGridViewCell cell = gridPoses.SelectedCells[0];
					if (cell is DataGridViewTextBoxCell)
					{
						string text = cell.Value?.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							Clipboard.Clear();
							Clipboard.SetText(text);
							TextBox box = gridPoses.EditingControl as TextBox;
							if (box != null)
							{
								box.Text = "";
							}
						}
					}
				}
				return;
			}

			CopySelectedLine();
			//remove the line
			int index = gridPoses.SelectedRows[0].Index;
			gridPoses.Rows.RemoveAt(index);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var count = gridPoses.SelectedCells.Count;
			if (count == 1)
			{
				//if cursor is in a cell, do standard clipboard action
				TextBox box = gridPoses.EditingControl as TextBox;
				if (box != null)
				{
					Clipboard.Clear();
					Clipboard.SetText(box.Text);
				}
			}
			else
			{
				//otherwise, copy the whole row
				CopySelectedLine();
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_clipboard == null) { return; }
			if (gridPoses.SelectedRows.Count == 0)
			{
				if (gridPoses.SelectedCells.Count > 0)
				{
					DataGridViewCell cell = gridPoses.SelectedCells[0];
					if (cell is DataGridViewTextBoxCell)
					{
						TextBox box = gridPoses.EditingControl as TextBox;
						if (box != null)
						{
							box.Text = Clipboard.GetText();
						}
					}
				}
				return;
			}

			DataGridViewRow row = gridPoses.SelectedRows[0];
			if (row.Index == gridPoses.Rows.Count - 1)
			{
				gridPoses.Rows.Add();
				row = gridPoses.Rows[gridPoses.Rows.Count - 2];
			}

			WriteRow(_clipboard, row);
			UpdateImageCell(_clipboard.ImageKey, row);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gridPoses.SelectedRows.Count == 0) { return; }

			DataGridViewRow row = gridPoses.SelectedRows[0];
			ImageMetadata data = ReadRow(row);
			DataGridViewRow newRow = gridPoses.Rows[gridPoses.Rows.Add()];
			WriteRow(data, newRow);
			gridPoses.Rows.Remove(newRow);
			gridPoses.Rows.Insert(gridPoses.SelectedRows[0].Index + 1, newRow);
			UpdateImageCell(data.ImageKey, newRow);
		}

		private void gridPoses_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var hit = gridPoses.HitTest(e.X, e.Y);
				gridPoses.ClearSelection();
				if (hit.RowIndex >= 0 && hit.RowIndex < gridPoses.Rows.Count)
				{
					if (hit.ColumnIndex >= 0 && hit.ColumnIndex <= gridPoses.Columns.Count)
					{
						gridPoses.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
						gridPoses.BeginEdit(true);
					}
					else
					{
						gridPoses.Rows[hit.RowIndex].Selected = true;
					}
				}
			}
		}

		private void chkRequired_CheckedChanged(object sender, EventArgs e)
		{
			PopulatePoseGrid();
		}

		private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (gridPoses.SelectedRows.Count == 0)
			{
				if (gridPoses.SelectedCells.Count > 0)
				{
					duplicateToolStripMenuItem.Visible = false;
					pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
					return;
				}
			}
			duplicateToolStripMenuItem.Visible = true;
			pasteToolStripMenuItem.Enabled = _clipboard != null;
		}

		private void gridPoses_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataGridViewColumn col = gridPoses.Columns[e.ColumnIndex];
			if (col.SortMode != DataGridViewColumnSortMode.Programmatic)
			{
				return;
			}

			bool ascending = false;
			if (_sortedColumn == col)
			{
				if (_sortAscending)
				{
					ascending = false;
				}
				else
				{
					ascending = true;
				}
			}
			else
			{
				ascending = true;
			}

			DataGridViewColumn secondaryCol = (col == ColStage ? ColPose : ColStage);
			_sortedColumn = col;
			_sortAscending = ascending;
			gridPoses.Sort(new PoseSorter(col, secondaryCol, ascending, (col == ColStage)));
		}

		private class PoseSorter : IComparer
		{
			private DataGridViewColumn _primary;
			private DataGridViewColumn _secondary;
			private bool _primaryNumeric;
			private bool _ascending;

			public PoseSorter(DataGridViewColumn primary, DataGridViewColumn secondary, bool ascending, bool useNumericPrimary)
			{
				_primary = primary;
				_secondary = secondary;
				_ascending = ascending;
				_primaryNumeric = useNumericPrimary;
			}

			public int Compare(object x, object y)
			{
				DataGridViewRow row1 = x as DataGridViewRow;
				DataGridViewRow row2 = y as DataGridViewRow;
				string primary1 = row1.Cells[_primary.Name].Value?.ToString() ?? "";
				string primary2 = row2.Cells[_primary.Name].Value?.ToString() ?? "";
				string secondary1 = row1.Cells[_secondary.Name].Value?.ToString() ?? "";
				string secondary2 = row2.Cells[_secondary.Name].Value?.ToString() ?? "";

				int val1;
				int val2;
				if (_primaryNumeric)
				{
					if (!int.TryParse(primary1, out val1))
					{
						val1 = -1;
					}
					if (!int.TryParse(primary2, out val2))
					{
						val2 = -1;
					}
				}
				else
				{
					if (!int.TryParse(secondary1, out val1))
					{
						val1 = -1;
					}
					if (!int.TryParse(secondary2, out val2))
					{
						val2 = -1;
					}
				}
				int compare = 0;
				if (_primaryNumeric)
				{
					compare = val1.CompareTo(val2);
				}
				else
				{
					compare = primary1.CompareTo(primary2);
				}
				if (compare == 0)
				{
					if (!_primaryNumeric)
					{
						compare = val1.CompareTo(val2);
					}
					else
					{
						compare = secondary1.CompareTo(secondary2);
					}
				}
				if (!_ascending)
				{
					compare *= -1;
				}
				return compare;
			}
		}

		private void gridPoses_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = gridPoses.Rows[e.RowIndex];
			string stage = row.Cells["ColStage"].Value?.ToString();
			string pose = row.Cells["ColPose"].Value?.ToString();
			if (string.IsNullOrEmpty(pose))
			{
				return;
			}
			string name = pose;
			int numStage = -1;
			if (!string.IsNullOrEmpty(stage))
			{
				name = stage + "-" + pose;
				int.TryParse(stage, out numStage);
			}

			Character c = _character.Character;
			PoseMapping img = c.PoseLibrary.GetPose(name + ".png");
			if (img != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, img, numStage));
			}
		}

		private void cmdCopyCrop_Click(object sender, EventArgs e)
		{
			MakePoseList();
			ImageMetadata selected = null;
			if (gridPoses.SelectedCells.Count > 0)
			{
				int index = gridPoses.SelectedCells[0].RowIndex;
				selected = index < _poseList.Poses.Count ? _poseList.Poses[index] : null;
			}
			CropCopier copier = new CropCopier(_poseList, selected);
			if (copier.ShowDialog() == DialogResult.OK)
			{
				PopulatePoseGrid();
			}
		}

		private void cmdFolder_Click(object sender, EventArgs e)
		{
			string directory = _character.GetDirectory();
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					Arguments = directory,
					FileName = "explorer.exe"
				};

				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void cmdToMatrix_Click(object sender, EventArgs e)
		{
			string name = _lastPoseFile;
			if (string.IsNullOrEmpty(name))
			{
				name = "Main";
			}
			else
			{
				name = Path.GetFileNameWithoutExtension(name);
			}
			AddSheetForm form = new AddSheetForm(name);
			if (form.ShowDialog() == DialogResult.OK)
			{
				MakePoseList();
				if (_poseList == null || _poseList.Poses.Count == 0)
				{
					MessageBox.Show("Pose list is empty.");
					return;
				}

				PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(_character);
				bool isEmpty = matrix.IsEmpty();
				PoseSheet sheet = matrix.AddSheet(form.SheetName, _character.Character);
				if (isEmpty && matrix.Sheets.Count > 1)
				{
					matrix.RemoveSheet(matrix.Sheets[0]);
					sheet.Name = form.SheetName;
				}
				matrix.FillFromPoseList(_character.Character, _poseList, sheet);
				Shell.Instance.Launch(_character as IRecord, typeof(PoseMatrixEditor), sheet);
			}
		}
	}

}
