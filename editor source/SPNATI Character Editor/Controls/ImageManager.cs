using KisekaeImporter;
using KisekaeImporter.ImageImport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ImageManager : UserControl
	{
		private ImageImporter _importer = new ImageImporter();
		private Image _previewImage;
		private ImageMetadata _previewImageMetadata;
		private Character _character;
		private PoseList _poseList = new PoseList();
		private string _listFileName;
		private ImageLibrary _imageLibrary;
		private string _lastPoseFile;
		private string _lastTemplateFile;

		private const int DefaultImageHeight = 1500;
		private RectangleF _cropBounds = new Rectangle(0, 0, 10, 10);
		private DragState _dragState = DragState.None;
		private bool _autoUpdate = false;
		private ImportState _state = ImportState.Ready;
		private int _importRow;
		private PointF _downPoint;

		private readonly Image EmptyImage = new Bitmap(1, 1);

		public event EventHandler<Tuple<string, Image>> GeneratedImage;
		public event EventHandler<Image> PreviewImage;

		public ImageManager()
		{
			InitializeComponent();
			splitPreviewer.Panel2Collapsed = true;
			PopulatePoseGrid();
		}

		/// <summary>
		/// Clears all poses in the grid
		/// </summary>
		private void ClearPoses()
		{
			gridPoses.Rows.Clear();
			_poseList.Poses.Clear();
		}

		/// <summary>
		/// Sets up the manager to work with a particular character
		/// </summary>
		/// <param name="character">Character to import poses for</param>
		public void SetCharacter(Character character)
		{
			if (_character == character)
			{
				UpdateTemplate();
				return;
			}
			ClearPoses();
			_character = character;
			UpdateTemplate();
			if (_character == null)
				return;
			_imageLibrary = new ImageLibrary();
			_imageLibrary.Load(character);

			string defaultFileName = Path.Combine(Config.GetRootDirectory(character), "poses.txt");
			if (File.Exists(defaultFileName))
			{
				ImportPoseList(defaultFileName);
			}
			PopulatePoseGrid();
		}

		/// <summary>
		/// Imports a pose list from a file
		/// </summary>
		/// <param name="filename"></param>
		private void ImportPoseList(string filename)
		{
			_poseList = new PoseList();
			_listFileName = filename;
			string[] lines = File.ReadAllLines(filename);

			Rect cropInfo = new Rect(0, 0, 600, 1400);

			foreach (string line in lines)
			{
				string trimmedLine = line.Trim();
				if (trimmedLine.StartsWith("#") || string.IsNullOrEmpty(trimmedLine))
					continue;
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
				string stage = row.Cells["ColStage"].Value?.ToString();
				string pose = row.Cells["ColPose"].Value?.ToString();
				if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
					continue;
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
				if (data == null)
					data = string.Empty;

				string key = GetKey(stage, pose);
				ImageMetadata metadata = new ImageMetadata(key, data);
				metadata.CropInfo = new Rect(l, t, r, b);
				_poseList.Poses.Add(metadata);
			}
		}

		/// <summary>
		/// Populates the pose grid
		/// </summary>
		private void PopulatePoseGrid()
		{
			DataGridViewImageColumn imageCol = gridPoses.Columns["ColImage"] as DataGridViewImageColumn;
			imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

			try
			{
				gridPoses.Rows.Clear();
				foreach (ImageMetadata pose in _poseList.Poses)
				{
					DataGridViewRow row = gridPoses.Rows[gridPoses.Rows.Add()];
					string[] piecedKey = pose.ImageKey.Split(new char[] { '-' }, 2);
					if (piecedKey.Length == 0)
					{
						row.Cells["ColStage"].Value = "0";
						row.Cells["ColPose"].Value = pose.ImageKey;
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

					//Try to link this up with an existing image
					CharacterImage existingImage = _imageLibrary.Find(pose.ImageKey);
					DataGridViewImageCell imageCell = row.Cells["ColImage"] as DataGridViewImageCell;
					if (existingImage != null)
					{
						imageCell.Value = existingImage.Image;
						row.Cells["ColImport"].Value = "Reimport";
					}
					else
					{
						imageCell.Value = EmptyImage;
						row.Cells["ColImport"].Value = "Import";
					}

					row.Cells["ColData"].Value = pose.Data;
				}
			}
			catch
			{
				MessageBox.Show("Error importing pose list. Is this actually a poses file?");
				gridPoses.Rows.Clear();
			}
		}

		/// <summary>
		/// Imports an image from Kisekae and brings up the cropper
		/// </summary>
		/// <param name="index">Pose grid row index containing the metadata of the pose to import</param>
		private void ImportImageForCropping(int index)
		{
			DataGridViewRow row = gridPoses.Rows[index];
			_importRow = index;

			string stage = row.Cells["ColStage"].Value?.ToString();
			string pose = row.Cells["ColPose"].Value?.ToString();
			if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
			{
				MessageBox.Show("Stage and Pose must be filled out.");
				return;
			}
			string data = row.Cells["ColData"].Value?.ToString();
			if (string.IsNullOrEmpty(data))
			{
				//Dummy values for faster testing
				data = "47**aa17.100.0.14.62.17.100.0.2.62_ab_ac_ba54_bb7.1_bc185.500.0.0.1_bd17_be180_ca79.0.6.63.50.31.44.49.30_cb0_daF9C9BD.0.0.100_db_dd9.3.16.0.12.29_dhF38484.20.50.43.3_di5_qa_qb_dc7.4.EEBABE.D68B8B.C96262_eh_ea26.33.33.56.0.0_ec10.29.33.33.56.42.63.1_ed0.24.0.1.33.56_ef_eg_r00_fa11.50.65.54.50.36.56_fb7_fh5_fc3.19.55.3.19.55.42.61.61.42.50.50_fd1.0.20.893A1A.893A1A_fe48.77_ff0000000000_fg1.58_pa0.0.0.0.10.50.85.78.0.0_t00_pb_pc_pd_pe_ga0_gb0_gc1.0_ge0000000000_gh_gf_gg_gd10000000_ha86.86_hb50.1.50.100_hc0.50.48.0.50.48_hd0.1.50.50_ia_if0.55.55.0.1.8.0.0.8.0.0.0.0.3_ib_id9.55.55.44.0.0.1.1.0.0.1.0.0.3_ic_jc_ie1.56.56.0.10.22.22.0.10.22.22.0.0_ja13.55.2.0_jb13.55.2.0_jd6.48.50.50_je6.48.50.50_jf_jg_ka4.18.18.18.0_kb10.A5C0F1.42.42_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_m00_n00_s00_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_ad0.0.0.0.0.0.0.0.0.0";
			}
			if (_state == ImportState.Cropping)
			{
				CloseImportPreview(); //Reset
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

			Cursor.Current = Cursors.WaitCursor;
			_previewImage = _importer.ImportSingleImage(_previewImageMetadata);
			Cursor.Current = Cursors.Default;
			if (_previewImage == null)
			{
				MessageBox.Show("Failed to import. Is Kisekae running?");
				return;
			}

			splitPreviewer.Panel2Collapsed = false;
			splitPreviewer.Panel1Collapsed = true;
			_state = ImportState.Cropping;
			float zoom = ZoomRatio;
			_cropBounds = _previewImageMetadata.CropInfo.ToRectangle(zoom);
			_cropBounds.X = _cropBounds.X + ImageImporter.ImageXOffset * zoom;
			UpdateCropValues();

			previewPanel.Invalidate();
		}

		/// <summary>
		/// Builds a filename from a stage and pose/emotion
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="pose"></param>
		/// <returns></returns>
		private static string GetKey(string stage, string pose)
		{
			return string.Format("{0}-{1}", stage, pose);
		}

		/// <summary>
		/// Cropping preview paint code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int screenWidth = previewPanel.Width;
			int screenHeight = previewPanel.Height;
			if (_previewImage != null)
			{
				int width = (int)(screenHeight * (float)_previewImage.Width / _previewImage.Height);
				g.DrawImage(_previewImage, 0, 0, width, screenHeight);

				//Crop boundary
				g.DrawRectangle(_dragState == DragState.None ? Pens.Red : Pens.Blue, _cropBounds.X, _cropBounds.Y, _cropBounds.Width, _cropBounds.Height);
			}
		}

		/// <summary>
		/// Handles moving the cropping bounds
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (_previewImage == null)
				return;
			if (_dragState == DragState.None)
			{
				DragState hoverState = GetDragState(e);
				SetCursor(hoverState);
			}
			else
			{
				//Pull the appropriate bounds
				int MinBuffer = (int)valWidth.Minimum;
				float top, left, right, diff;
				if ((_dragState & DragState.Horizontal) > 0)
				{
					right = _cropBounds.Right;
					left = _cropBounds.X;
					float midLine = (right + left) * 0.5f;
					if ((_dragState & DragState.Left) > 0)
					{
						float maxLeft = midLine - MinBuffer * 0.5f;
						left = Math.Max(0, Math.Min(maxLeft, e.X));
						float dist = midLine - left;
						right = midLine + dist;
						_cropBounds.Width = right - left;
						_cropBounds.X = left;
					}
					else if ((_dragState & DragState.Right) > 0)
					{
						float maxRight = midLine + MinBuffer * 0.5f;
						right = Math.Min(previewPanel.Width, Math.Max(maxRight, e.X));
						float dist = right - midLine;
						left = midLine - dist;
						_cropBounds.Width = right - left;
						_cropBounds.X = left;
					}
				}
				else if ((_dragState & DragState.Left) > 0)
				{
					right = _cropBounds.Right;
					left = Math.Max(0, Math.Min(right - MinBuffer, e.X));
					_cropBounds.Width = right - left;
					_cropBounds.X = left;
				}
				else if ((_dragState & DragState.Right) > 0)
				{
					left = _cropBounds.Left;
					right = Math.Min(previewPanel.Width, Math.Max(left + MinBuffer, e.X));
					_cropBounds.Width = right - left;
				}
				else if ((_dragState & DragState.Move) > 0)
				{
					left = _cropBounds.Left;
					top = _cropBounds.Top;
					diff = e.X - _downPoint.X;
					left = Math.Max(0, Math.Min(previewPanel.Width - _cropBounds.Width, left + diff));
					diff = e.Y - _downPoint.Y;
					top = Math.Max(0, Math.Min(previewPanel.Height - _cropBounds.Height, top + diff));
					_cropBounds.X = left;
					_cropBounds.Y = top;
					_downPoint = new PointF(e.X, e.Y);
				}
				else
				{
					//Not yet implemented
				}

				UpdateCropValues();

				previewPanel.Invalidate();
			}
		}

		/// <summary>
		/// Updates the crop bounds text boxes
		/// </summary>
		private void UpdateCropValues()
		{
			_autoUpdate = true;
			float zoom = ZoomRatio;
			valWidth.Value = (decimal)(_cropBounds.Width / zoom);
			valHeight.Value = (decimal)(_cropBounds.Height / zoom);
			_autoUpdate = false;
		}

		/// <summary>
		/// Sets the mouse cursor based on the drag state
		/// </summary>
		/// <param name="state">Current dragging state</param>
		private void SetCursor(DragState state)
		{
			switch (state)
			{
				case DragState.Left:
				case DragState.Right:
				case DragState.Horizontal:
					Cursor.Current = Cursors.SizeWE;
					break;
				case DragState.Top:
				case DragState.Bottom:
				case DragState.Vertical:
					Cursor.Current = Cursors.SizeNS;
					break;
				case DragState.Move:
					Cursor.Current = Cursors.SizeAll;
					break;
				default:
					Cursor.Current = Cursors.Default;
					break;
			}
		}

		/// <summary>
		/// Gets the appropriate drag operation based on a mouse position
		/// </summary>
		/// <param name="e">Mouse event args</param>
		/// <returns></returns>
		private DragState GetDragState(MouseEventArgs e)
		{
			const int GrabThreshold = 5;
			//See if we're on the cropping box
			if (e.Y >= _cropBounds.Y && e.Y <= _cropBounds.Y + _cropBounds.Height)
			{
				if (Math.Abs(_cropBounds.X - e.X) <= GrabThreshold)
				{
					return DragState.Left;
				}
				else if (Math.Abs(_cropBounds.X + _cropBounds.Width - e.X) <= GrabThreshold)
				{
					return DragState.Right;
				}
			}
			if (e.X >= _cropBounds.X && e.X <= _cropBounds.X + _cropBounds.Width)
			{
				if (Math.Abs(_cropBounds.Y - e.Y) <= GrabThreshold)
				{
					return DragState.Top;
				}
				else if (Math.Abs(_cropBounds.Y + _cropBounds.Height - e.Y) <= GrabThreshold)
				{
					return DragState.Bottom;
				}
			}
			if (e.Y >= _cropBounds.Y && e.Y <= _cropBounds.Y + _cropBounds.Height && e.X >= _cropBounds.X && e.X <= _cropBounds.X + _cropBounds.Width)
			{
				return DragState.Move;
			}
			return DragState.None;
		}

		private void previewPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (_previewImage == null || e.Button == MouseButtons.Middle)
				return;
			DragState hoverState = GetDragState(e);
			if (hoverState != DragState.None)
			{
				if (hoverState != DragState.Move && e.Button == MouseButtons.Right)
				{
					if (hoverState == DragState.Left || hoverState == DragState.Right)
						hoverState |= DragState.Horizontal;
					else if (hoverState == DragState.Top || hoverState == DragState.Bottom)
						hoverState |= DragState.Vertical;
				}
				SetCursor(hoverState);
				_downPoint = new PointF(e.X, e.Y);
				_dragState = hoverState;
				previewPanel.Invalidate();
			}
		}

		private void previewPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (_previewImage == null || _dragState == DragState.None)
				return;
			_dragState = DragState.None;
			Cursor.Current = Cursors.Default;
			previewPanel.Invalidate();
		}

		private void valWidth_ValueChanged(object sender, EventArgs e)
		{
			if (_autoUpdate)
				return;
			_cropBounds.Width = (int)valWidth.Value;
		}

		private void valHeight_ValueChanged(object sender, EventArgs e)
		{
			if (_autoUpdate)
				return;
			_cropBounds.Height = (int)valHeight.Value;
		}

		private enum ImportState
		{
			Ready,
			Cropping,
			Importing
		}

		private float ZoomRatio
		{
			get
			{
				return (float)previewPanel.Height / DefaultImageHeight;
			}
		}

		/// <summary>
		/// Crops the image and closes the previewer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdCrop_Click(object sender, EventArgs e)
		{
			if (_state != ImportState.Cropping)
				return;

			_previewImageMetadata.CropInfo = _cropBounds.ToRect(ZoomRatio);
			using (Image croppedImage = _importer.Crop(_previewImage, _previewImageMetadata.CropInfo))
			{
				if (croppedImage != null)
				{
					SaveImage(_previewImageMetadata.ImageKey, croppedImage);
					if (_importRow >= 0 && _importRow < gridPoses.Rows.Count)
					{
						DataGridViewRow row = gridPoses.Rows[_importRow];
						CharacterImage cachedImage = _imageLibrary.Find(_previewImageMetadata.ImageKey);
						row.Cells["ColImage"].Value = cachedImage.Image;
						row.Cells["ColImport"].Value = "Reimport";
						row.Cells["ColL"].Value = _previewImageMetadata.CropInfo.Left - ImageImporter.ImageXOffset;
						row.Cells["ColR"].Value = _previewImageMetadata.CropInfo.Right - ImageImporter.ImageXOffset;
						row.Cells["ColT"].Value = _previewImageMetadata.CropInfo.Top;
						row.Cells["ColB"].Value = _previewImageMetadata.CropInfo.Bottom;
					}
				}
			}

			CloseImportPreview();
		}

		/// <summary>
		/// Saves an image to disk
		/// </summary>
		/// <param name="imageKey">Name of image (stage+pose)</param>
		/// <param name="image">Image to save</param>
		private void SaveImage(string imageKey, Image image)
		{
			string filename = imageKey + ".png";
			string fullPath = Path.Combine(Config.GetRootDirectory(_character), filename);

			//Back up the existing image if it hasn't been backed up yet
			if (File.Exists(fullPath))
			{
				string backupDir = Path.Combine(Config.AppDataDirectory, _character.FolderName);
				if (!Directory.Exists(backupDir))
				{
					Directory.CreateDirectory(backupDir);
				}
				string backupPath = Path.Combine(backupDir, imageKey + ".png");
				if (!File.Exists(backupPath))
				{
					File.Copy(fullPath, backupPath);
				}
			}

			//Removes the previous image from the cache so the next time the image is looked up, it will pull the new one from disk
			ImageCache.Release(fullPath);
			_imageLibrary.Add(fullPath, imageKey);

			image.Save(fullPath);
		}

		/// <summary>
		/// Handles the Import buttons within the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (_state != ImportState.Ready)
				return;
			if (gridPoses.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
			{
				ImportImageForCropping(e.RowIndex);
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
		/// Click event for the Crop Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			CloseImportPreview();
		}

		/// <summary>
		/// Closes the cropping preview without saving the image
		/// </summary>
		private void CloseImportPreview()
		{
			if (_previewImage != null)
			{
				_previewImage.Dispose();
				_previewImage = null;
			}
			_state = ImportState.Ready;
			splitPreviewer.Panel2Collapsed = true;
		}

		/// <summary>
		/// Wrapper around a file dialog to force the file selection to be within the character's folder
		/// </summary>
		/// <param name="dialog">Dialog to open</param>
		/// <returns></returns>
		private DialogResult ChooseFileInDirectory(FileDialog dialog, ref string file)
		{
			string dir = Config.GetRootDirectory(_character);
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
						MessageBox.Show("Images need to be in the character's folder.");
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
			if (_state != ImportState.Ready)
				return;
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
			if (_state != ImportState.Ready)
				return;
			if (ChooseFileInDirectory(saveFileDialog1, ref _lastPoseFile) == DialogResult.OK)
			{
				ExportPoseList(saveFileDialog1.FileName);
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
			}
		}

		/// <summary>
		/// Click event for the Clear Poses button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (_state != ImportState.Ready)
				return;
			if (MessageBox.Show("This will clear all poses and lose any unsaved changes. Are you sure you want to continue?", "Clear Pose List", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
			if (_state != ImportState.Ready)
				return;
			MakePoseList();

			//Figure out which images need importing
			List<ImageMetadata> toImport = new List<ImageMetadata>();
			foreach (var metadata in _poseList.Poses)
			{
				CharacterImage existingImage = _imageLibrary.Find(metadata.ImageKey);
				if (existingImage?.Image != null)
					continue;
				toImport.Add(metadata);
			}

			ImportPosesAsync(toImport);
		}

		/// <summary>
		/// Imports all poses, replacing existing images
		/// </summary>
		private void ImportAllPoses()
		{
			if (_state != ImportState.Ready)
				return;
			MakePoseList();
			ImportPosesAsync(_poseList.Poses);
		}

		/// <summary>
		/// Imports images asynchronously with a progress form
		/// </summary>
		/// <param name="list"></param>
		private void ImportPosesAsync(List<ImageMetadata> list)
		{
			_state = ImportState.Importing;

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
						MessageBox.Show("Imported with errors. See errorlog.txt for more information.");
					}
				}
				finally
				{
					progressForm.Close();
					_state = ImportState.Ready;
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
			return Task.Run(() =>
			{
				try
				{
					int current = 0;
					bool hasErrors = false;
					foreach (ImageMetadata metadata in importList)
					{
						progress.Report(current++);
						try
						{
							Image img = _importer.ImportSingleImage(metadata);
							if (img == null)
							{
								//Something went wrong. Stop here.
								MessageBox.Show("Couldn't import " + metadata.ImageKey + ". Is Kisekae running?");
								return -1;
							}

							Rect cropInfo = metadata.CropInfo;
							cropInfo.Left += ImageImporter.ImageXOffset;
							cropInfo.Right += ImageImporter.ImageXOffset;
							using (Image croppedImage = _importer.Crop(img, cropInfo))
							{
								SaveImage(metadata.ImageKey, croppedImage);
							}
							img.Dispose();
							img = null;

							//Update the relevant grid row
							foreach (DataGridViewRow row in gridPoses.Rows)
							{
								string stage = row.Cells["ColStage"].Value?.ToString();
								string pose = row.Cells["ColPose"].Value?.ToString();
								if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
									continue;
								if (GetKey(stage, pose) == metadata.ImageKey)
								{
									CharacterImage cachedImage = _imageLibrary.Find(metadata.ImageKey);
									row.Cells["ColImage"].Value = cachedImage.Image;
									row.Cells["ColImport"].Value = "Reimport";
									break;
								}
							}
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

		[Flags]
		private enum DragState
		{
			None = 0,
			Left = 1,
			Right = 2,
			Top = 4,
			Bottom = 8,
			Horizontal = 16,
			Vertical = 32,
			Move = 64
		}

		private void txtBaseCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
				txtBaseCode.SelectAll();
		}

		private void UpdateTemplate()
		{
			if (_character == null)
				return;
			gridLayers.Rows.Clear();
			for (int layer = 0; layer < _character.Layers + Clothing.ExtraStages; layer++)
			{
				gridLayers.Rows.Add();
			}
			RestoreLabels();
		}

		private void RestoreLabels()
		{
			for (int layer = 0; layer < _character.Layers + Clothing.ExtraStages; layer++)
			{
				StageName name = _character.LayerToStageName(layer);
				string label = name?.DisplayName ?? "Unknown";
				DataGridViewRow row = gridLayers.Rows[layer];
				row.Cells[0].Value = label;
			}
		}

		private void cmdLoadTemplate_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(openFileDialog1, ref _lastTemplateFile) == DialogResult.OK)
			{
				PoseTemplate template = PoseTemplate.LoadFromFile(openFileDialog1.FileName);
				if (template == null)
				{
					MessageBox.Show("Failed to read " + openFileDialog1.FileName + " as a pose template.");
				}
				LoadTemplate(template);
			}
		}

		/// <summary>
		/// Loads a template into the template tab
		/// </summary>
		/// <param name="template"></param>
		private void LoadTemplate(PoseTemplate template)
		{
			txtBaseCode.Text = template.BaseCode.Serialize();
			gridLayers.Rows.Clear();
			gridEmotions.Rows.Clear();
			foreach (StageTemplate stageCode in template.Stages)
			{
				DataGridViewRow row = gridLayers.Rows[gridLayers.Rows.Add()];
				row.Cells[1].Value = stageCode.Code.Serialize();
				row.Cells[2].Value = stageCode.ExtraBlush;
				row.Cells[3].Value = stageCode.ExtraAnger;
				row.Cells[4].Value = stageCode.ExtraJuice;
			}
			foreach (var emotion in template.Emotions)
			{
				DataGridViewRow row = gridEmotions.Rows[gridEmotions.Rows.Add()];
				row.Cells[0].Value = emotion.Key;
				row.Cells[1].Value = emotion.Code.Serialize();
			}
			RestoreLabels();
		}

		private void cmdSaveTemplate_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(saveFileDialog1, ref _lastTemplateFile) == DialogResult.OK)
			{
				PoseTemplate template = CreateTemplate();
				template?.SaveToFile(saveFileDialog1.FileName);
			}
		}

		private int GetIntValue(object value)
		{
			if (value == null)
				return 0;
			int result;
			int.TryParse(value.ToString(), out result);
			return result;
		}

		/// <summary>
		/// Creates a pose template from the template tab fields
		/// </summary>
		/// <returns></returns>
		private PoseTemplate CreateTemplate()
		{
			PoseTemplate template = new PoseTemplate();
			template.BaseCode = new KisekaeCode(txtBaseCode.Text);
			if (string.IsNullOrWhiteSpace(txtBaseCode.Text))
			{
				MessageBox.Show("You must supply a base code.", "Generating Template");
				return null;
			}
			foreach (DataGridViewRow row in gridLayers.Rows)
			{
				StageTemplate stageTemplate = GetStageTemplateFromRow(row);
				if (stageTemplate == null)
					continue;
				template.Stages.Add(stageTemplate);
			}
			foreach (DataGridViewRow row in gridEmotions.Rows)
			{
				Emotion emotion = GetEmotionFromRow(row);
				if (emotion == null)
					continue;
				template.Emotions.Add(emotion);
			}
			return template;
		}

		private StageTemplate GetStageTemplateFromRow(DataGridViewRow row)
		{
			if (row == null)
				return null;
			string key = row.Cells[0].Value?.ToString();
			string value = row.Cells[1].Value?.ToString();
			if (string.IsNullOrEmpty(key))
				return null;
			int blush = GetIntValue(row.Cells[2].Value);
			int anger = GetIntValue(row.Cells[3].Value);
			int juice = GetIntValue(row.Cells[4].Value);
			StageTemplate stageTemplate = new StageTemplate();
			stageTemplate.ExtraBlush = blush;
			stageTemplate.ExtraAnger = anger;
			stageTemplate.ExtraJuice = juice;
			stageTemplate.Code = new KisekaeCode(value);
			return stageTemplate;
		}

		private Emotion GetEmotionFromRow(DataGridViewRow row)
		{
			if (row == null)
				return null;
			string key = row.Cells[0].Value?.ToString();
			string value = row.Cells[1].Value?.ToString();
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
				return null;
			Emotion emotion = new Emotion(key, new KisekaeCode(value));
			return emotion;
		}

		private void cmdGenerate_Click(object sender, EventArgs e)
		{
			PoseTemplate template = CreateTemplate();
			if (template == null)
			{
				return;
			}
			_poseList = template.GeneratePoseList();
			PopulatePoseGrid();
			lblCurrentPoseFile.Text = "From template";
			tabControl.SelectedTab = tabPoses;
		}

		private void cmdPreviewPose_Click(object sender, EventArgs e)
		{
			KisekaeCode baseCode = new KisekaeCode(txtBaseCode.Text, true);
			DataGridViewRow stageRow = null;
			DataGridViewRow emotionRow = null;
			if (gridLayers.Rows.Count == 0 || gridEmotions.Rows.Count == 0)
				return;
			if (gridLayers.SelectedRows != null && gridLayers.SelectedRows.Count > 0)
				stageRow = gridLayers.SelectedRows[0];
			else stageRow = gridLayers.Rows[0];

			if (gridEmotions.SelectedRows != null && gridEmotions.SelectedRows.Count > 0)
				emotionRow = gridEmotions.SelectedRows[0];
			else emotionRow = gridEmotions.Rows[0];

			StageTemplate stage = GetStageTemplateFromRow(stageRow);
			Emotion emotion = GetEmotionFromRow(emotionRow);
			if (stage == null)
			{
				MessageBox.Show("No clothing data has been defined yet.", "Preview");
				return;
			}
			if (emotion == null)
			{
				MessageBox.Show("No pose has been defined yet.", "Preview");
				return;
			}

			KisekaeCode code = PoseTemplate.CreatePose(baseCode, stage, emotion.Code);
			ImageMetadata metadata = new ImageMetadata("_zzPreview", code.Serialize());
			Rect cropInfo = metadata.CropInfo;
			cropInfo.Left += ImageImporter.ImageXOffset;
			cropInfo.Right += ImageImporter.ImageXOffset;
			Cursor.Current = Cursors.WaitCursor;
			using (Image img = _importer.ImportSingleImage(metadata))
			{
				if (img != null && PreviewImage != null)
				{
					PreviewImage(this, _importer.Crop(img, cropInfo));
				}
			}
			Cursor.Current = Cursors.Default;
		}
	}
}
