using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using Desktop.Skinning;
using KisekaeImporter;
using KisekaeImporter.DataStructures.Kisekae;
using KisekaeImporter.ImageImport;
using KisekaeImporter.SubCodes;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 220)]
	[Activity(typeof(Costume), 220)]
	public partial class PoseMatrixEditor : Activity
	{
		private ISkin _skin;
		private Character _character;
		private PoseMatrix _matrix;
		private PoseSheet _sheet;
		private int _currentStage;
		private PoseEntry _currentPose;
		private bool _dirty;
		private Column _currentColumn;
		private bool _pendingWardrobeChange = false;

		private Dictionary<string, DataGridViewColumn> _columns = new Dictionary<string, DataGridViewColumn>();
		private Dictionary<PoseEntry, DataGridViewCell> _cells = new Dictionary<PoseEntry, DataGridViewCell>();

		private static readonly Bitmap EmptyImage = new Bitmap(1, 1);

		public override string Caption
		{
			get { return "Pose Matrix"; }
		}

		public PoseMatrixEditor()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_skin = Record as ISkin;
			_character = _skin.Character;

			SubscribeWorkspace(WorkspaceMessages.WardrobeUpdated, OnWardrobeChanged);

			skinnedSplitContainer1.Panel2Collapsed = true;
			tsRemovePose.Enabled = false;
			tsApplyCrop.Enabled = false;
		}

		protected override void OnFirstActivate()
		{
			_matrix = CharacterDatabase.GetPoseMatrix(_skin);
			_matrix.PropertyChanged += _matrix_PropertyChanged;
			if (_matrix.Sheets.Count == 0)
			{
				_matrix.AddSheet("Main");
			}

			Rebuild();
		}

		private void OnWardrobeChanged()
		{
			_pendingWardrobeChange = true;
		}

		private void _matrix_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_character.IsDirty = true;
			if (!this.IsActive)
			{
				_dirty = true;
			}
		}

		protected override void OnActivate()
		{
			if (_pendingWardrobeChange)
			{
				_matrix?.ReconcileStages(_character);
				_dirty = true;
			}
			if (_dirty)
			{
				Rebuild();
				_dirty = false;
			}
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length > 0)
			{
				PoseSheet targetSheet = parameters[0] as PoseSheet;
				if (targetSheet != null)
				{
					int index = _matrix.Sheets.IndexOf(targetSheet);
					if (index >= 0)
					{
						tabControl.SelectedIndex = index;
					}
				}
			}
		}

		private void Rebuild()
		{
			tabControl.TabPages.Clear();
			foreach (PoseSheet sheet in _matrix.Sheets)
			{
				AddTab(sheet);
			}

			if (tabControl.TabPages.Count == 1)
			{
				//need to manually raise the event for the first tab
				tabControl_SelectedIndexChanged(tabControl, new System.EventArgs());
			}
		}

		private void AddTab(PoseSheet sheet)
		{
			TabPage page = new TabPage(sheet.Name);
			page.Tag = sheet;
			tabControl.TabPages.Add(page);
		}

		private void BuildGrid()
		{
			_columns.Clear();
			_cells.Clear();
			_currentPose = null;
			_currentColumn = null;
			skinnedSplitContainer1.Panel2Collapsed = true;
			grid.Rows.Clear();
			grid.Columns.Clear();

			if (_sheet.Stages.Count == 0)
			{
				//Add empty stages
				for (int stage = 0; stage < _character.Layers + Clothing.ExtraStages; stage++)
				{
					PoseStage row = new PoseStage(stage);

					//make a sample column
					if (stage == 0)
					{
						PoseEntry calm = new PoseEntry()
						{
							Key = "calm"
						};
						row.Poses.Add(calm);
					}

					_sheet.Stages.Add(row);
				}
			}

			foreach (PoseStage stage in _sheet.Stages)
			{
				if (stage.Poses.Count == 0)
				{
					continue;
				}
				//create new columns as necessary
				foreach (PoseEntry pose in stage.Poses)
				{
					if (!_columns.ContainsKey(pose.Key))
					{
						AddColumn(pose.Key);
					}
				}

				DataGridViewRow row = grid.Rows[grid.Rows.Add()];

				//give every cell a valid default
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Value = EmptyImage;
				}

				StageName stageName = _character.LayerToStageName(stage.Stage);
				row.HeaderCell.Value = $"{stageName.Id} - {stageName.DisplayName}";

				foreach (PoseEntry pose in stage.Poses)
				{
					DataGridViewCell cell = row.Cells[pose.Key];
					cell.Tag = pose;
					_cells[pose] = cell;
					UpdateCell(cell, pose);
					pose.PropertyChanged += Pose_PropertyChanged;
				}
			}
		}

		private void AddColumn(string key)
		{
			DataGridViewImageColumn col = new DataGridViewImageColumn();
			col.Name = key;
			col.HeaderText = key;
			grid.Columns.Add(col);
			col.SortMode = DataGridViewColumnSortMode.NotSortable;
			col.MinimumWidth = 40;
			col.DefaultCellStyle = new DataGridViewCellStyle()
			{
				Alignment = DataGridViewContentAlignment.MiddleCenter,
			};
			_columns[key] = col;

			//give a default empty value to any rows that were previously added
			foreach (DataGridViewRow existingRow in grid.Rows)
			{
				DataGridViewCell existingCell = existingRow.Cells[col.Name];
				existingCell.Value = EmptyImage;
			}
		}

		private void UpdateCell(DataGridViewCell cell, PoseEntry pose)
		{
			if (!string.IsNullOrEmpty(pose.Code))
			{
				if (cell == null && !_cells.TryGetValue(pose, out cell))
				{
					return;
				}

				//see if physical file exists
				int stage = cell.RowIndex;
				string key = GetKey(stage.ToString(), pose.Key);
				string filePath = Path.Combine(_skin.GetDirectory(), key + ".png");
				if (File.Exists(filePath))
				{
					cell.Value = Properties.Resources.Checkmark;
				}
				else
				{
					cell.Value = Properties.Resources.FileMissing;
				}
			}
			else
			{
				cell.Value = EmptyImage;
			};
		}

		private void Pose_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			DataGridViewCell cell;
			PoseEntry pose = sender as PoseEntry;
			if (pose != null)
			{
				if (_cells.TryGetValue(pose, out cell))
				{
					UpdateCell(cell, pose);
				}
			}
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_sheet != null)
			{
				_sheet.PropertyChanged -= _sheet_PropertyChanged;
			}
			if (tabControl.SelectedIndex < 0)
			{
				return;
			}
			_sheet = tabControl.SelectedTab.Tag as PoseSheet;
			_sheet.PropertyChanged += _sheet_PropertyChanged;
			BuildGrid();
		}

		private void _sheet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Name")
			{
				for (int i = 0; i < tabControl.TabPages.Count; i++)
				{
					TabPage page = tabControl.TabPages[i];
					if (page.Tag == _sheet)
					{
						page.Text = _sheet.Name;
					}
				}
			}
		}

		private void tabStrip_AddButtonClicked(object sender, System.EventArgs e)
		{
			AddSheetForm form = new AddSheetForm("New Sheet");
			if (form.ShowDialog() == DialogResult.OK)
			{
				PoseSheet sheet = _matrix.AddSheet(form.SheetName);
				AddTab(sheet);
				tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
			}
		}

		private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (_currentColumn != null)
			{
				_currentColumn.PropertyChanged -= ColumnChanged;
			}

			tsRemovePose.Enabled = false;
			tsApplyCrop.Enabled = false;

			if (e.ColumnIndex == -1)
			{
				if (e.RowIndex == -1)
				{
					//selecting the whole sheet
					skinnedSplitContainer1.Panel2Collapsed = false;
					sptMode.Panel2Collapsed = false;
					picHelp.Image = new Bitmap("Resources/Images/BaseCode.png");
					panelSingle.Visible = false;
					panelStage.Visible = false;
					panelPose.Visible = true;
					_currentPose = null;
					_currentColumn = null;
					lblHeader.Text = "Sheet: " + _sheet.Name;
					table.SetDataAsync(_sheet, null);
					return;
				}

				//selecting a stage

				grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
				DataGridViewRow row = grid.Rows[e.RowIndex];
				row.Selected = true;
				_currentPose = null;
				_currentStage = e.RowIndex;
				if (e.RowIndex >= 0 && e.RowIndex < _sheet.Stages.Count)
				{
					lblHeader.Text = $"{row.HeaderCell.Value?.ToString()}";
					skinnedSplitContainer1.Panel2Collapsed = false;
					panelSingle.Visible = false;
					panelPose.Visible = false;
					panelStage.Visible = true;
					sptMode.Panel2Collapsed = false;
					picHelp.Image = new Bitmap("Resources/Images/StageCode.png");
					table.SetDataAsync(_sheet.Stages[e.RowIndex], null);
				}
			}
			else if (e.RowIndex == -1)
			{
				//selecting a pose key

				grid.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
				DataGridViewColumn column = grid.Columns[e.ColumnIndex];
				column.Selected = true;
				_currentPose = null;
				_currentStage = -1;
				_currentColumn = new Column(column);
				sptMode.Panel2Collapsed = true;
				if (_currentColumn != null)
				{
					_currentColumn.PropertyChanged += ColumnChanged;
					tsRemovePose.Enabled = true;
					skinnedSplitContainer1.Panel2Collapsed = false;
					panelSingle.Visible = false;
					panelPose.Visible = true;
					panelStage.Visible = false;
					lblHeader.Text = "Pose: " + _currentColumn.Key;
					table.SetDataAsync(_currentColumn, null);
				}
			}
			else
			{
				//selecting a pose+stage combo

				DataGridViewCell gridCell = grid.Rows[e.RowIndex]?.Cells[e.ColumnIndex];
				if (gridCell == null)
				{
					return;
				}
				sptMode.Panel2Collapsed = true;
				tsApplyCrop.Enabled = true;
				_currentStage = e.RowIndex;

				PoseEntry cell = gridCell?.Tag as PoseEntry;
				if (cell == null)
				{
					cell = new PoseEntry()
					{
						Key = grid.Columns[e.ColumnIndex].HeaderText
					};
					cell.PropertyChanged += Pose_PropertyChanged;
					gridCell.Tag = cell;
					_cells[cell] = gridCell;
					_sheet.Stages[_currentStage].Poses.Add(cell);
				}
				if (cell != null)
				{
					_currentPose = cell;
					lblHeader.Text = $"{grid.Rows[e.RowIndex].HeaderCell.Value?.ToString()} - {cell.Key}";
					skinnedSplitContainer1.Panel2Collapsed = false;
					panelSingle.Visible = true;
					panelPose.Visible = false;
					panelStage.Visible = false;

					sptMode.Panel2Collapsed = string.IsNullOrEmpty(_sheet.Stages[_currentStage].Code);
					if (!sptMode.Panel2Collapsed)
					{
						picHelp.Image = new Bitmap("Resources/Images/PoseCode.png");
					}

					table.SetDataAsync(cell, null);

					ShowPreview(cell);
				}
			}
		}

		private void ColumnChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Column col = sender as Column;
			if (col != null && e.PropertyName == "Key")
			{
				string oldName = col.GridColumn.Name;
				col.GridColumn.HeaderText = col.Key;
				col.GridColumn.Name = col.Key;

				bool unique = true;
				//check for uniqueness
				for (int i = 0; i < grid.Columns.Count; i++)
				{
					DataGridViewColumn other = grid.Columns[i];
					if (other != col.GridColumn && other.Name == col.GridColumn.Name)
					{
						unique = false;
						Skin skin = SkinManager.Instance.CurrentSkin;
						col.GridColumn.HeaderCell.Style.ForeColor = skin.BadForeColor;
						col.GridColumn.HeaderCell.ToolTipText = "This pose name is already listed!";
						break;
					}
				}

				if (unique)
				{
					Skin skin = SkinManager.Instance.CurrentSkin;
					col.GridColumn.HeaderCell.Style.ForeColor = skin.Surface.ForeColor;
					col.GridColumn.HeaderCell.ToolTipText = "";

					//update all the cells's names
					foreach (PoseStage stage in _sheet.Stages)
					{
						PoseEntry pose = stage.GetCell(oldName);
						if (pose != null)
						{
							pose.Key = col.Key;
							DataGridViewCell cell;
							if (_cells.TryGetValue(pose, out cell))
							{
								UpdateCell(cell, pose);
							}
						}
					}
				}

			}
		}

		private void cmdImport_Click(object sender, System.EventArgs e)
		{
			ImportAndCropPose();
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

		private void ImportAndCropPose()
		{
			if (_currentPose == null)
			{
				return;
			}

			string stage = _currentStage.ToString();
			string name = _currentPose.Key;
			string code = _currentPose.Code;
			if (string.IsNullOrEmpty(code))
			{
				MessageBox.Show("Code must be filled out.", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string key = GetKey(stage, name);

			string baseCode = _sheet.BaseCode;
			string stageCode = _sheet.Stages[_currentStage].Code;
			ImageMetadata preview = null;
			if (!string.IsNullOrEmpty(baseCode) || !string.IsNullOrEmpty(stageCode))
			{
				StageTemplate stageTemplate = new StageTemplate(stageCode ?? "");
				Emotion emotion = new Emotion(_currentPose.Key, _currentPose.Code ?? "", _currentPose.Crop.Left.ToString(), _currentPose.Crop.Top.ToString(), _currentPose.Crop.Right.ToString(), _currentPose.Crop.Bottom.ToString());
				KisekaeCode templatedCode = PoseTemplate.CreatePose(new KisekaeCode(baseCode ?? "", true), stageTemplate, emotion);
				preview = new ImageMetadata(key, templatedCode.ToString());
			}
			else
			{
				preview = new ImageMetadata(key, code);
			}
			if (_currentPose.Crop.Right > 0 && _currentPose.Crop.Bottom > 0)
			{
				preview.CropInfo = _currentPose.Crop;
			}
			//preview.ExtraData = row.Cells["ColAdvanced"].Tag as Dictionary<string, string> ?? new Dictionary<string, string>(); //TODO

			preview.SkipPreprocessing = _currentPose.SkipPreProcessing;
			ImageCropper cropper = new ImageCropper();
			cropper.Import(preview, _character, false);
			if (cropper.ShowDialog() == DialogResult.OK)
			{
				Image importedImage = cropper.CroppedImage;
				if (importedImage != null)
				{
					SaveImage(key, importedImage);
					_currentPose.Crop = cropper.CroppingRegion;
					ShowPreview(_currentPose);
					UpdateCell(null, _currentPose);
				}
			}
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

				_character.PoseLibrary.Add(fullPath);
			}
			catch { }

			return fullPath;
		}

		private void tabStrip_CloseButtonClicked(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove this sheet?", "Remove Sheet", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_matrix.RemoveSheet(_sheet);
				tabControl.TabPages.Remove(tabControl.SelectedTab);
			}
		}

		/// <summary>
		/// Sends this pose's image to the sidebar preview if it exists
		/// </summary>
		/// <param name="cell"></param>
		private void ShowPreview(PoseEntry cell)
		{
			PoseMapping img = _character.PoseLibrary.GetPose("#-" + cell.Key + ".png");
			if (img != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, img, _currentStage));
			}
		}

		private async void cmdLineup_Click(object sender, System.EventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}

			string key = _currentColumn?.Key;
			if (string.IsNullOrEmpty(key))
			{
				//grab the first found
				bool found = false;
				foreach (PoseStage stage in _sheet.Stages)
				{
					foreach (PoseEntry entry in stage.Poses)
					{
						key = entry.Key;
						found = true;
						break;
					}
					if (found)
					{
						break;
					}
				}
			}

			//turn this into a lineup
			KisekaeCode code = new KisekaeCode("", true);
			code.Scene = new KisekaeChunk("");
			int version = 0;
			bool isEmpty = true;
			string appearance = _sheet.BaseCode;
			KisekaeCode baseCode = null;
			if (!string.IsNullOrEmpty(appearance))
			{
				baseCode = new KisekaeCode(appearance, true);
			}
			HashSet<int> globalData = new HashSet<int>();
			PoseEntry lastCell = null;
			for (int i = 0; i < _sheet.Stages.Count && i <= _character.Layers && i < 9; i++)
			{
				PoseStage stage = _sheet.Stages[i];
				PoseEntry cell = stage.GetCell(key) ?? lastCell;
				if (cell != null && !string.IsNullOrEmpty(cell.Code))
				{
					lastCell = cell;
					isEmpty = false;
					KisekaeCode modelCode = null;
					if (baseCode != null || !string.IsNullOrEmpty(stage.Code))
					{
						Emotion emotion = new Emotion(cell.Key, cell.Code, "", "", "", "");
						modelCode = PoseTemplate.CreatePose(baseCode, new StageTemplate(stage.Code ?? ""), emotion);
					}
					else
					{
						modelCode = new KisekaeCode(cell.Code, false);
					}

					string[] pieces = cell.Code.Split('*');
					string versionNo = pieces[0];
					int modelVersion;
					if (int.TryParse(versionNo, out modelVersion) && modelVersion > version)
					{
						version = modelVersion;
					}
					if (modelCode.Scene != null)
					{
						globalData.Add(i);
						modelCode.Scene = null;
					}

					KisekaeModel model = modelCode.Models[0];
					if (model == null)
					{
						continue;
					}

					code.Models[i] = model;
					int x = 150 + 87 * i;
					KisekaePose kklPose = model.GetOrAddComponent<KisekaePose>();
					kklPose.Placement.X = x;
				}
			}
			code.Version = version.ToString();

			if (isEmpty)
			{
				return;
			}

			//load in this code
			KisekaeCamera camera = code.Scene.GetOrAddComponent<KisekaeScene>().Camera;
			camera.X = 0;
			camera.Y = 0;
			camera.Zoom = 0;
			ImageMetadata md = new ImageMetadata("lineup", code.ToString());
			md.SkipPreprocessing = true;
			ImageLoader loader = new ImageLoader();
			bool success = await loader.Import(md, _skin);
			if (success && globalData.Count > 0)
			{
				MessageBox.Show("One or more stage codes contained scene data (ex. global image attachments, speech bubbles, etc.). This data was discarded while creating the lineup.\r\nImpacted stage(s): " + string.Join(", ", globalData));
			}
		}

		private async void cmdLoadToKKL_Click(object sender, System.EventArgs e)
		{
			if (_sheet == null || _currentStage < 0 || _currentStage >= _sheet.Stages.Count)
			{
				return;
			}

			string appearance = _sheet.BaseCode;
			KisekaeCode baseCode = null;
			if (!string.IsNullOrEmpty(appearance))
			{
				baseCode = new KisekaeCode(appearance, true);
			}

			PoseStage stage = _sheet.Stages[_currentStage];
			PoseEntry cell = stage.Poses.Find(p => !string.IsNullOrEmpty(p.Code));
			KisekaeCode modelCode = null;
			if (baseCode != null || !string.IsNullOrEmpty(stage.Code))
			{
				Emotion emotion = new Emotion("temp", cell?.Code ?? "", "", "", "", "");
				modelCode = PoseTemplate.CreatePose(baseCode, new StageTemplate(stage.Code ?? ""), emotion);
			}
			else
			{
				modelCode = new KisekaeCode(cell?.Code ?? "", false);
			}

			//load in this code
			ImageMetadata md = new ImageMetadata("lineup", modelCode.ToString());
			ImageLoader loader = new ImageLoader();
			await loader.Import(md, _skin);
		}

		private void cmdImportLineup_Click(object sender, System.EventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}
			bool hasClothingCode = (_sheet.Stages.Find(s => !string.IsNullOrEmpty(s.Code)) != null);
			ImportLineupMode mode = (_currentColumn == null ? ImportLineupMode.Wardrobe : hasClothingCode ? ImportLineupMode.Pose : ImportLineupMode.All);
			ImportLineupForm form = new ImportLineupForm(mode);
			if (form.ShowDialog() == DialogResult.OK)
			{
				string key = _currentColumn?.Key;

				KisekaeCode lineup = form.Code;
				for (int i = 0; i < lineup.Models.Length; i++)
				{
					if (i >= _sheet.Stages.Count)
					{
						break;
					}

					KisekaeModel model = lineup.Models[i];
					if (model != null)
					{
						PoseStage stage = _sheet.Stages[i];
						DataGridViewRow row = grid.Rows[i];

						if (mode == ImportLineupMode.Wardrobe)
						{
							stage.Code = $"{lineup.Version}**{model.Serialize()}";
						}
						else
						{
							//find the corresponding PoseEntry, or make a new one
							DataGridViewCell cell = row.Cells[key];
							PoseEntry pose = cell.Tag as PoseEntry;
							if (pose == null)
							{
								pose = new PoseEntry();
								pose.Key = key;
								stage.Poses.Add(pose);
								cell.Tag = pose;
								_cells[pose] = cell;
								pose.PropertyChanged += Pose_PropertyChanged;
							}

							pose.Code = $"{lineup.Version}**{model.Serialize()}";

							UpdateCell(cell, pose);
						}
					}
				}
			}
		}

		private class Column : BindableObject
		{
			[Text(DisplayName = "Pose name")]
			public string Key
			{
				get { return Get<string>(); }
				set { Set(value); }
			}

			public DataGridViewColumn GridColumn { get; private set; }

			public Column(DataGridViewColumn column)
			{
				GridColumn = column;
				Key = column.Name;
			}

			public override string ToString()
			{
				return Key;
			}
		}

		private void cmdImportNew_Click(object sender, System.EventArgs e)
		{
			ImportUnloadedPoses();
		}

		private void cmdImportAll_Click(object sender, System.EventArgs e)
		{
			ImportAllPoses();
		}

		/// <summary>
		/// Imports all pose data that doesn't have an image yet
		/// </summary>
		private void ImportUnloadedPoses()
		{
			//Figure out which images need importing
			List<ImageMetadata> toImport = new List<ImageMetadata>();
			foreach (PoseStage stage in _sheet.Stages)
			{
				foreach (PoseEntry pose in stage.Poses)
				{
					string key = GetKey(stage.Stage.ToString(), pose.Key);
					string filename = key + ".png";
					string fullPath = Path.Combine(_character.GetDirectory(), filename);
					if (File.Exists(fullPath))
					{
						continue;
					}
					ImageMetadata metadata = CreateMetadata(stage, pose);
					toImport.Add(metadata);
				}
			}

			ImportPosesAsync(toImport);
		}

		/// <summary>
		/// Imports all poses, replacing existing images
		/// </summary>
		private void ImportAllPoses()
		{
			List<ImageMetadata> toImport = new List<ImageMetadata>();
			foreach (PoseStage stage in _sheet.Stages)
			{
				foreach (PoseEntry pose in stage.Poses)
				{
					ImageMetadata metadata = CreateMetadata(stage, pose);
					toImport.Add(metadata);
				}
			}

			ImportPosesAsync(toImport);
		}

		private ImageMetadata CreateMetadata(PoseStage stage, PoseEntry pose)
		{
			string key = GetKey(stage.Stage.ToString(), pose.Key);
			ImageMetadata metadata = new ImageMetadata(key, "");
			string appearance = _sheet.BaseCode;
			KisekaeCode baseCode = null;
			if (!string.IsNullOrEmpty(appearance))
			{
				baseCode = new KisekaeCode(appearance, true);
			}

			KisekaeCode modelCode = null;
			if (baseCode != null || !string.IsNullOrEmpty(stage.Code))
			{
				Emotion emotion = new Emotion(pose.Key, pose.Code, "", "", "", "");
				modelCode = PoseTemplate.CreatePose(baseCode, new StageTemplate(stage.Code ?? ""), emotion);
			}
			else
			{
				modelCode = new KisekaeCode(pose.Code, false);
			}
			metadata.SkipPreprocessing = pose.SkipPreProcessing;
			metadata.CropInfo = pose.Crop;
			metadata.Data = modelCode.ToString();
			return metadata;
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
								foreach (DataGridViewRow row in grid.Rows)
								{
									foreach (DataGridViewCell cell in row.Cells)
									{
										PoseEntry entry = cell.Tag as PoseEntry;
										if (entry != null && GetKey(row.Index.ToString(), entry.Key) == metadata.ImageKey)
										{
											UpdateCell(cell, entry);
										}
									}
								}
							};
							grid.Invoke(invoker);
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

		private void tsRemovePose_Click(object sender, EventArgs e)
		{
			if (_currentColumn == null)
			{
				return;
			}
			if (grid.Columns.Count == 1)
			{
				MessageBox.Show("You cannot remove the last column.");
				return;
			}
			if (MessageBox.Show($"Are you sure you want to remove all poses for {_currentColumn.Key}? This cannot be undone.", "Remove Pose", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_sheet.RemoveColumn(_currentColumn.Key);
				_columns.Remove(_currentColumn.Key);
				grid.Columns.Remove(_currentColumn.GridColumn);
				_currentColumn = null;
			}
		}

		private void tsAddPose_Click(object sender, EventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}

			AddSheetForm form = new AddSheetForm("happy");
			form.Text = "Add Pose";
			if (form.ShowDialog() == DialogResult.OK)
			{
				//create a column but no data structure yet. That'll happen when clicking in cells
				string key = _sheet.GetUniqueColumnName(form.SheetName);
				AddColumn(key);
			}
		}

		private void grid_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}
			if (e.Column.DisplayIndex != e.Column.Index)
			{
				string name = e.Column.Name;
				_sheet.MoveColumn(name, e.Column.DisplayIndex);
			}
		}

		private void tsSort_Click(object sender, EventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}
			_sheet.SortColumns();
			BuildGrid();
		}

		private void tsApplyCrop_Click(object sender, EventArgs e)
		{
			if (_currentPose == null || _sheet == null)
			{
				return;
			}
			foreach (PoseStage stage in _sheet.Stages)
			{
				PoseEntry cell = stage.GetCell(_currentPose.Key);
				if (cell == null || cell == _currentPose)
				{
					continue;
				}
				cell.Crop = _currentPose.Crop;
			}
		}
	}
}
