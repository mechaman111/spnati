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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
		private bool _building;
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
			tsApplyCrop.Enabled = tsApplyCode.Enabled = false;

			tsAddMain.Visible = _skin is Costume;
			sepSkin.Visible = _skin is Costume;
		}

		protected override void OnFirstActivate()
		{
			_matrix = CharacterDatabase.GetPoseMatrix(_skin);
			_matrix.PropertyChanged += _matrix_PropertyChanged;
			if (_matrix.Sheets.Count == 0)
			{
				//make a default sheet
				_matrix.AddSheet("Main", _character);
			}

			Rebuild();
		}

		private void OnWardrobeChanged()
		{
			_pendingWardrobeChange = true;
		}

		private void _matrix_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_skin.IsDirty = true;
			if (!this.IsActive)
			{
				_dirty = true;
			}
		}

		protected override void OnDestroy()
		{
			if (_sheet != null)
			{
				_sheet.PropertyChanged -= _sheet_PropertyChanged;
			}
			if (_currentColumn != null)
			{
				_currentColumn.PropertyChanged -= ColumnChanged;
				_currentColumn = null;
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

			if (tabControl.TabPages.Count > 0)
			{
				tabControl.SelectedIndex = 0;
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
			_building = true;
			_columns.Clear();
			_cells.Clear();
			_currentPose = null;
			_currentColumn = null;
			skinnedSplitContainer1.Panel2Collapsed = true;
			grid.Rows.Clear();
			grid.Columns.Clear();

			grid.TopLeftHeaderCell.Value = "Sheet";


			if (_sheet.IsEmpty)
			{
				_sheet.ReconcileStages(_character);

				for (int stage = 0; stage < _sheet.Stages.Count; stage++)
				{
					//make a sample column
					PoseStage row = _sheet.Stages[stage];
					PoseEntry calm = new PoseEntry()
					{
						Key = "calm"
					};
					row.Poses.Add(calm);
				}
			}

			foreach (PoseStage stage in _sheet.Stages)
			{
				//create new columns as necessary
				foreach (PoseEntry pose in stage.Poses)
				{
					if (!_columns.ContainsKey(pose.Key))
					{
						AddColumn(pose.Key);
					}
				}

				DataGridViewRow row = grid.Rows[grid.Rows.Add()];
				row.Tag = stage;

				//give every cell a valid default
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Value = EmptyImage;
				}

				if (_sheet.IsGlobal)
				{
					row.HeaderCell.Value = "Global";
				}
				else
				{
					StageName stageName = _character.LayerToStageName(stage.Stage);
					row.HeaderCell.Value = $"{stageName.Id} - {stageName.DisplayName}";
				}

				foreach (PoseEntry pose in stage.Poses)
				{
					DataGridViewCell cell = row.Cells[pose.Key];
					cell.Tag = pose;
					_cells[pose] = cell;
					UpdateCell(cell, pose);
					pose.PropertyChanged += Pose_PropertyChanged;
				}
			}
			_building = false;

			//manually trigger selection
			grid_SelectionChanged(grid, EventArgs.Empty);
		}

		private void AddColumn(string key)
		{
			AddColumn(key, -1);
		}
		/// <summary>
		/// Creates a new column
		/// </summary>
		/// <param name="key">Pose key</param>
		/// <param name="index">Where to insert in the sheet. -1 to put at the end.</param>
		private DataGridViewImageColumn AddColumn(string key, int index)
		{
			DataGridViewImageColumn col = new DataGridViewImageColumn();
			col.Name = key;
			col.HeaderText = key;
			if (index == -1)
			{
				grid.Columns.Add(col);
			}
			else
			{
				grid.Columns.Insert(index, col);
			}
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
			return col;
		}

		private void UpdateCell(DataGridViewCell cell, PoseEntry pose)
		{
			if (cell == null && !_cells.TryGetValue(pose, out cell))
			{
				return;
			}
			int stage = cell.RowIndex;
			string stageKey = _sheet.IsGlobal ? "" : stage.ToString();
			string key = GetKey(stageKey, pose.Key);
			string filePath = Path.Combine(_skin.GetDirectory(), key + ".png");

			if (!string.IsNullOrEmpty(pose.Code))
			{
				//see if physical file exists
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
				if (_skin is Costume)
				{
					string mainPath = Path.Combine(_skin.Character.GetDirectory(), key + ".png");
					if (File.Exists(mainPath) && !File.Exists(filePath))
					{
						cell.Value = Properties.Resources.Missing;
					}
					else
					{
						cell.Value = EmptyImage;
					}
				}
				else
				{
					cell.Value = EmptyImage;
				}
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
			form.SetMatrix(_matrix);
			if (form.ShowDialog() == DialogResult.OK)
			{
				PoseSheet sheet = _matrix.AddSheet(form.SheetName, _character, form.SelectedSheet, form.Global);
				AddTab(sheet);
				tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
			}
		}


		private void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex == -1)
			{
				//Row
				grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

				if (e.Button == MouseButtons.Right)
				{
					grid.ClearSelection();
					grid.Rows[e.RowIndex].Selected = true;
				}
			}
			else if (e.RowIndex == -1)
			{
				//Column
				grid.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;

				if (e.Button == MouseButtons.Right)
				{
					grid.ClearSelection();
					grid.Columns[e.ColumnIndex].Selected = true;
				}
			}
			else
			{
				//Cell

				//don't need to change SelectionMode because the others already allow selecting cells

				if (e.Button == MouseButtons.Right)
				{
					grid.ClearSelection();
					grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
				}
			}
		}

		private void grid_TopLeftHeaderMouseDown(object sender, EventArgs e)
		{
			grid.ClearSelection();

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

		private void grid_SelectionChanged(object sender, EventArgs e)
		{
			if (_building)
			{
				return;
			}
			if (_currentColumn != null)
			{
				_currentColumn.PropertyChanged -= ColumnChanged;
				_currentColumn = null;
			}

			tsRemovePose.Enabled = false;
			tsApplyCrop.Enabled = tsApplyCode.Enabled = false;

			if (grid.SelectedRows.Count > 0)
			{
				//selecting a stage
				tsPaste.Enabled = Clipboards.Has<RowClipboardData>();

				int rowIndex = grid.SelectedRows[0].Index;
				DataGridViewRow row = grid.Rows[rowIndex];
				row.Selected = true;
				_currentPose = null;
				_currentStage = rowIndex;
				if (rowIndex >= 0 && rowIndex < _sheet.Stages.Count)
				{
					lblHeader.Text = $"{row.HeaderCell.Value?.ToString()}";
					skinnedSplitContainer1.Panel2Collapsed = false;
					panelSingle.Visible = false;
					panelPose.Visible = false;
					panelStage.Visible = true;
					sptMode.Panel2Collapsed = false;
					picHelp.Image = new Bitmap("Resources/Images/StageCode.png");
					table.SetDataAsync(_sheet.Stages[rowIndex], null);
				}
			}
			else if (grid.SelectedColumns.Count > 0)
			{
				//selecting a pose key
				DataGridViewColumn column = grid.SelectedColumns[0];
				column.Selected = true;
				_currentPose = null;
				_currentStage = -1;
				_currentColumn = new Column(column);

				tsPaste.Enabled = Clipboards.Has<ColumnClipboardData>();

				if (_currentColumn != null)
				{
					_currentColumn.PropertyChanged += ColumnChanged;
					tsRemovePose.Enabled = grid.SelectedColumns.Count < grid.ColumnCount;
					skinnedSplitContainer1.Panel2Collapsed = false;
					sptMode.Panel2Collapsed = true;
					panelSingle.Visible = false;
					panelPose.Visible = true;
					panelStage.Visible = false;
					lblHeader.Text = "Pose: " + _currentColumn.Key;
					table.SetDataAsync(_currentColumn, null);
				}
			}
			else if (grid.SelectedCells.Count > 0)
			{
				DataGridViewCell gridCell = grid.SelectedCells[0];
				if (gridCell == null)
				{
					return;
				}

				tsPaste.Enabled = Clipboards.Has<CellClipboardData>();

				tsApplyCrop.Enabled = tsApplyCode.Enabled = true;
				_currentStage = gridCell.RowIndex;

				PoseEntry cell = gridCell?.Tag as PoseEntry;
				if (cell == null)
				{
					DataGridViewColumn col = gridCell.OwningColumn;
					cell = CreatePoseEntry(gridCell, col.Name);
				}
				if (cell != null)
				{
					_currentPose = cell;
					lblHeader.Text = $"{grid.Rows[gridCell.RowIndex].HeaderCell.Value?.ToString()} - {cell.Key}";
					skinnedSplitContainer1.Panel2Collapsed = false;
					panelSingle.Visible = true;
					panelPose.Visible = false;
					panelStage.Visible = false;
				}

				sptMode.Panel2Collapsed = string.IsNullOrEmpty(_sheet.Stages[_currentStage].Code);
				if (!sptMode.Panel2Collapsed)
				{
					picHelp.Image = new Bitmap("Resources/Images/PoseCode.png");
				}

				table.SetDataAsync(cell, null);
				ShowPreview(cell);
			}
		}

		private List<string> GetColumnOrder()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			foreach (DataGridViewColumn col in grid.Columns)
			{
				cols.Add(col);
			}
			return cols.OrderBy(c => c.DisplayIndex).Select(c => c.Name).ToList();
		}

		/// <summary>
		/// Creates and attaches a PoseEntry to a grid cell
		/// </summary>
		/// <param name="cell"></param>
		private PoseEntry CreatePoseEntry(DataGridViewCell gridCell, string key)
		{
			PoseEntry cell = new PoseEntry()
			{
				Key = key
			};
			cell.PropertyChanged += Pose_PropertyChanged;
			gridCell.Tag = cell;
			_cells[cell] = gridCell;
			_sheet.Stages[gridCell.RowIndex].InsertCell(cell, GetColumnOrder());
			return cell;
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

			string stage = _sheet.IsGlobal ? "" : _currentStage.ToString();
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
			preview.ExtraData = _currentPose.ExtraMetadata;

			preview.SkipPreprocessing = _currentPose.SkipPreProcessing;
			ImageCropper cropper = new ImageCropper();
			cropper.Import(preview, _skin, false);
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
			string fullPath = Path.Combine(_skin.GetDirectory(), filename);

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
			string key = cell.Key + ".png";
			if (!_sheet.IsGlobal)
			{
				key = "#-" + key;
			}
			PoseMapping img = _character.PoseLibrary.GetPose(key);
			if (img != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_skin, img, _currentStage));
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
			StageTemplate lastStage = new StageTemplate("");
			int max = Math.Min(_sheet.Stages.Count, 9);
			max = Math.Min(max, _character.Layers < 7 ? _character.Layers + Clothing.ExtraStages: 9);
			for (int i = 0; i < max; i++)
			{
				PoseStage stage = _sheet.Stages[i];
				PoseEntry cell = stage.GetCell(key) ?? lastCell;
				if (cell != null && string.IsNullOrEmpty(cell.Code))
				{
					cell = lastCell;
				}
				if (cell != null && !string.IsNullOrEmpty(cell.Code))
				{
					lastCell = cell;
					isEmpty = false;
					KisekaeCode modelCode = null;
					if (baseCode != null || !string.IsNullOrEmpty(stage.Code))
					{
						Emotion emotion = new Emotion(cell.Key, cell.Code, "", "", "", "");
						StageTemplate stageTemplate = string.IsNullOrEmpty(stage.Code) ? lastStage : new StageTemplate(stage.Code);
						lastStage = stageTemplate;
						modelCode = PoseTemplate.CreatePose(baseCode, stageTemplate, emotion);
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
								pose = CreatePoseEntry(cell, key);
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
			bool usingWardrobe = false;
			foreach (PoseStage stage in _sheet.Stages)
			{
				usingWardrobe = usingWardrobe || !string.IsNullOrEmpty(stage.Code);
				if (usingWardrobe && string.IsNullOrEmpty(stage.Code))
				{
					continue;
				}
				foreach (PoseEntry pose in stage.Poses)
				{
					if (string.IsNullOrEmpty(pose.Code))
					{
						continue;
					}
					string stageKey = _sheet.IsGlobal ? "" : stage.Stage.ToString();
					string key = GetKey(stageKey, pose.Key);
					string filename = key + ".png";
					string fullPath = Path.Combine(_skin.GetDirectory(), filename);
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
			bool usingWardrobe = false;
			foreach (PoseStage stage in _sheet.Stages)
			{
				usingWardrobe = usingWardrobe || !string.IsNullOrEmpty(stage.Code);
				if (usingWardrobe && string.IsNullOrEmpty(stage.Code))
				{
					continue;
				}
				foreach (PoseEntry pose in stage.Poses)
				{
					if (string.IsNullOrEmpty(pose.Code))
					{
						continue;
					}
					ImageMetadata metadata = CreateMetadata(stage, pose);
					toImport.Add(metadata);
				}
			}

			ImportPosesAsync(toImport);
		}

		private ImageMetadata CreateMetadata(PoseStage stage, PoseEntry pose)
		{
			string stageKey = _sheet.IsGlobal ? "" : stage.Stage.ToString();
			string key = GetKey(stageKey, pose.Key);
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
			metadata.ExtraData = pose.ExtraMetadata;
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
			if (!CharacterGenerator.ImportUnrecognizedAssets(_skin, codes))
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
							Image img = await CharacterGenerator.GetCroppedImage(new KisekaeCode(metadata.Data), metadata.CropInfo, _skin, metadata.ExtraData, metadata.SkipPreprocessing);
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
										string stageKey = _sheet.IsGlobal ? "" : row.Index.ToString();
										if (entry != null && GetKey(stageKey, entry.Key) == metadata.ImageKey)
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
			if (RemoveColumn(_currentColumn.GridColumn))
			{
				_currentColumn = null;
			}
		}

		private void tsAddPose_Click(object sender, EventArgs e)
		{
			AddNewColumn(-1);
		}

		private void AddNewColumn(int index)
		{
			if (_sheet == null)
			{
				return;
			}

			AddPoseForm form = new AddPoseForm("happy");
			if (form.ShowDialog() == DialogResult.OK)
			{
				//create a column but no data structure yet. That'll happen when clicking in cells
				string key = _sheet.GetUniqueColumnName(form.PoseName);
				DataGridViewColumn col = AddColumn(key, index);

				//select the first cell
				grid.ClearSelection();
				grid.Rows[0].Cells[col.Index].Selected = true;
			}
		}

		private void InsertColumn()
		{
			if (grid.SelectedCells.Count > 0)
			{
				AddNewColumn(grid.SelectedCells[0].OwningColumn.DisplayIndex);
			}
			else
			{
				AddNewColumn(-1);
			}
		}

		private bool RemoveColumn(params DataGridViewColumn[] columns)
		{
			if (grid.Columns.Count == columns.Length)
			{
				MessageBox.Show("You cannot remove all columns.");
				return false;
			}
			if (MessageBox.Show($"Are you sure you want to remove all poses for the selected columns? This cannot be undone.", "Remove Pose", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				List<string> names = columns.Select(c => c.Name).ToList();
				foreach (string key in names)
				{
					DataGridViewColumn column = grid.Columns[key];
					if (column == null)
					{
						continue;
					}
					_columns.Remove(key);
					foreach (DataGridViewRow row in grid.Rows)
					{
						DataGridViewCell cell = row.Cells[key];
						ClearCell(cell);
					}
					_sheet.RemoveColumn(key);
					grid.Columns.Remove(column);
				}
				return true;
			}
			return false;
		}

		private void grid_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}
			if (e.Column.DisplayIndex != e.Column.Index)
			{
				_sheet.ReorderColumns(GetColumnOrder());
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
			for (int i = 0; i < _sheet.Stages.Count; i++)
			{
				if (i == _currentStage)
				{
					continue;
				}
				PoseStage stage = _sheet.Stages[i];
				PoseEntry cell = stage.GetCell(_currentPose.Key);
				if (cell == null)
				{
					continue;
				}
				cell.Crop = _currentPose.Crop;
			}
		}

		private void tsApplyCode_Click(object sender, EventArgs e)
		{
			if (_currentPose == null || _sheet == null)
			{
				return;
			}

			for (int i = 0; i < grid.Rows.Count; i++)
			{
				if (i == _currentStage)
				{
					continue;
				}
				DataGridViewRow row = grid.Rows[i];
				DataGridViewCell cell = row.Cells[_currentPose.Key];
				PoseEntry pose = cell.Tag as PoseEntry;
				if (pose == null)
				{
					pose = CreatePoseEntry(cell, _currentPose.Key);
				}
				_currentPose.CopyPropertiesInto(pose);
			}
		}

		private void gridMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (grid.SelectedRows.Count > 0)
			{
				FilterContextMenu("Row");
			}
			else if (grid.SelectedColumns.Count > 0)
			{
				FilterContextMenu("Column");
			}
			else if (grid.SelectedCells.Count > 0)
			{
				FilterContextMenu("Cell");
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void FilterContextMenu(string tag)
		{
			foreach (ToolStripItem item in gridMenu.Items)
			{
				item.Visible = (item.Tag?.ToString() == tag);
			}
		}

		#region Event Handlers
		private void tsCut_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void tsCopy_Click(object sender, EventArgs e)
		{
			Copy();
		}

		private void tsPaste_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void cutCellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void copyCellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Copy();
		}

		private void pasteCellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void deleteCellsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteSelection();
		}

		private void insertColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InsertColumn();
		}

		private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewColumn(-1);
		}
		#endregion

		private DataGridViewColumn GetColumnByDisplayIndex(int displayIndex)
		{
			foreach (DataGridViewColumn col in grid.Columns)
			{
				if (col.DisplayIndex == displayIndex)
				{
					return col;
				}
			}
			return null;
		}

		#region Clipboard
		private void Cut()
		{
			Copy();
			DeleteSelection();
		}

		private void Copy()
		{
			if (grid.SelectedRows.Count > 0)
			{
				Clipboards.Set<RowClipboardData>(new RowClipboardData(grid.SelectedRows));
				tsPaste.Enabled = pasteRowsToolStripMenuItem.Enabled = true;
			}
			else if (grid.SelectedColumns.Count > 0)
			{
				Clipboards.Set<ColumnClipboardData>(new ColumnClipboardData(grid.SelectedColumns));
				tsPaste.Enabled = pasteColumnsToolStripMenuItem.Enabled = true;
			}
			else if (grid.SelectedCells.Count > 0)
			{
				Clipboards.Set<CellClipboardData>(new CellClipboardData(grid.SelectedCells));
				tsPaste.Enabled = pasteCellToolStripMenuItem.Enabled = true;
			}
		}

		private void Paste()
		{
			if (_sheet == null)
			{
				return;
			}
			if (grid.SelectedRows.Count > 0)
			{
				RowClipboardData clipboard = Clipboards.Get<RowClipboardData, RowClipboardData>();
				if (clipboard != null)
				{
					int start = int.MaxValue;
					foreach (DataGridViewRow r in grid.SelectedRows)
					{
						start = Math.Min(start, r.Index);
					}
					grid.ClearSelection();

					foreach (PoseStage clipboardRow in clipboard.Rows)
					{
						int index = start + clipboardRow.Stage;
						if (index >= 0 && index < grid.Rows.Count)
						{
							DataGridViewRow row = grid.Rows[index];
							PoseStage stage = _sheet.Stages[index];
							stage.Code = clipboardRow.Code;
							for (int i = 0; i < row.Cells.Count; i++)
							{
								DataGridViewCell gridCell = row.Cells[i];
								PoseEntry pose = gridCell.Tag as PoseEntry;
								PoseEntry clipboardPose = clipboardRow.GetCell(grid.Columns[i].Name);
								if (clipboardPose == null || clipboardPose.Code == null)
								{
									if (pose != null)
									{
										ClearCell(gridCell);
									}
								}
								else
								{
									if (pose == null)
									{
										pose = CreatePoseEntry(gridCell, clipboardPose.Key);
									}
									clipboardPose.CopyPropertiesInto(pose);
								}
							}
							row.Selected = true;
						}
					}
				}
			}
			else if (grid.SelectedColumns.Count > 0)
			{
				ColumnClipboardData clipboard = Clipboards.Get<ColumnClipboardData, ColumnClipboardData>();
				if (clipboard != null)
				{
					int start = int.MaxValue;
					foreach (DataGridViewColumn c in grid.SelectedColumns)
					{
						start = Math.Min(start, c.DisplayIndex);
					}

					grid.ClearSelection();

					foreach (ColumnClipboardItem item in clipboard.Columns)
					{
						int col = start + item.Index;
						DataGridViewColumn column = GetColumnByDisplayIndex(col);
						if (column != null)
						{
							for (int i = 0; i < item.Rows.Count && i < grid.Rows.Count; i++)
							{
								PoseEntry clipboardRow = item.Rows[i];
								DataGridViewRow row = grid.Rows[i];
								DataGridViewCell cell = row.Cells[column.Index];
								PoseEntry pose = cell.Tag as PoseEntry;
								if (clipboardRow == null)
								{
									//nothing in this cell
									if (pose == null)
									{
										continue;
									}
									else
									{
										ClearCell(cell);
									}
								}
								else
								{
									if (pose == null)
									{
										pose = CreatePoseEntry(cell, column.Name);
									}
									else
									{
										pose.Clear();
									}
									clipboardRow.CopyPropertiesInto(pose);
									pose.Key = column.Name;
								}
							}
							column.Selected = true;
						}
					}
				}
			}
			else if (grid.SelectedCells.Count > 0)
			{
				CellClipboardData clipboard = Clipboards.Get<CellClipboardData, CellClipboardData>();

				if (clipboard != null)
				{
					CellClipboardData newSelection = new CellClipboardData(grid.SelectedCells);

					grid.ClearSelection();

					foreach (Tuple<Point, PoseEntry> item in clipboard.Cells)
					{
						int col = newSelection.Left + item.Item1.X;
						int row = newSelection.Top + item.Item1.Y;
						if (col >= 0 && row >= 0 && col < grid.Columns.Count && row < grid.Rows.Count)
						{
							DataGridViewColumn gridCol = GetColumnByDisplayIndex(col);
							DataGridViewCell gridCell = grid.Rows[row].Cells[gridCol.Name];
							PoseStage stage = _sheet.Stages[row];
							if (item.Item2 == null || string.IsNullOrEmpty(item.Item2.Code))
							{
								ClearCell(gridCell);
							}
							else
							{
								PoseEntry cell = stage.GetCell(gridCol.Name);
								if (cell == null)
								{
									cell = CreatePoseEntry(gridCell, gridCol.Name);
								}
								item.Item2.CopyPropertiesInto(cell);
								cell.Key = gridCol.Name; //set the key back since a column's key should never change
							}
							gridCell.Selected = true;
						}
					}
				}
			}
		}
		#endregion

		private void DeleteSelection()
		{
			if (_sheet == null)
			{
				return;
			}
			if (grid.SelectedRows.Count > 0)
			{
				foreach (DataGridViewRow row in grid.SelectedRows)
				{
					PoseStage stage = _sheet.Stages[row.Index];
					stage.Code = null;
					foreach (PoseEntry cell in stage.Poses)
					{
						cell.Clear();
					}
				}
			}
			else if (grid.SelectedColumns.Count > 0)
			{
				DataGridViewColumn[] toDelete = new DataGridViewColumn[grid.SelectedColumns.Count];
				for (int i = 0; i < grid.SelectedColumns.Count; i++)
				{
					toDelete[i] = grid.SelectedColumns[i];
				}

				RemoveColumn(toDelete);
			}
			else if (grid.SelectedCells.Count > 0)
			{
				foreach (DataGridViewCell cell in grid.SelectedCells)
				{
					ClearCell(cell);
				}
			}
		}

		private void ClearCell(DataGridViewCell cell)
		{
			PoseEntry pose = cell.Tag as PoseEntry;
			if (pose == null || _sheet == null)
			{
				return;
			}
			cell.Tag = null;
			_cells.Remove(pose);
			PoseStage stage = _sheet.Stages[cell.RowIndex];
			stage.RemoveCell(pose.Key);
			cell.Value = EmptyImage;
		}

		private void tsAddMain_Click(object sender, EventArgs e)
		{
			HashSet<string> requiredImages = _skin.GetRequiredPoses(true);
			foreach (string key in requiredImages.OrderBy(i => i))
			{
				if (!_columns.ContainsKey(key))
				{
					AddColumn(key);
				}
			}
		}

		private void cmdFolder_Click(object sender, EventArgs e)
		{
			string directory = _skin.GetDirectory();
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

		private void tsPoseList_Click(object sender, EventArgs e)
		{
			if (_sheet == null)
			{
				return;
			}
			PoseList list = new PoseList();
			bool usingTemplate = false;
			foreach (PoseStage stage in _sheet.Stages)
			{
				usingTemplate = usingTemplate || !string.IsNullOrEmpty(stage.Code);
				if (usingTemplate && string.IsNullOrEmpty(stage.Code))
				{
					continue;
				}
				foreach (PoseEntry entry in stage.Poses)
				{
					if (string.IsNullOrEmpty(entry.Code))
					{
						continue;
					}
					ImageMetadata metadata = CreateMetadata(stage, entry);
					list.Poses.Add(metadata);
				}
			}
			Shell.Instance.Launch(_skin as IRecord, typeof(PoseListEditor), list);
		}
	}

	public class CellClipboardData
	{
		private List<Tuple<Point, PoseEntry>> _cells = new List<Tuple<Point, PoseEntry>>();

		public int Left { get; private set; }
		public int Top { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public IEnumerable<Tuple<Point, PoseEntry>> Cells
		{
			get { return _cells; }
		}

		public int Count
		{
			get { return _cells.Count; }
		}

		public CellClipboardData(DataGridViewSelectedCellCollection selection)
		{
			int minRow = int.MaxValue;
			int minCol = int.MaxValue;
			int maxRow = int.MinValue;
			int maxCol = int.MaxValue;
			foreach (DataGridViewCell cell in selection)
			{
				PoseEntry copy = new PoseEntry() { Key = cell.OwningColumn.Name };
				PoseEntry pose = cell.Tag as PoseEntry;
				pose?.CopyPropertiesInto(copy);

				Tuple<Point, PoseEntry> item = new Tuple<Point, PoseEntry>(new Point(cell.OwningColumn.DisplayIndex, cell.RowIndex), copy);
				minRow = Math.Min(minRow, cell.RowIndex);
				maxRow = Math.Max(maxRow, cell.RowIndex);
				minCol = Math.Min(minCol, cell.OwningColumn.DisplayIndex);
				maxCol = Math.Max(maxCol, cell.OwningColumn.DisplayIndex);
				_cells.Add(item);
			}

			Left = minCol;
			Top = minRow;
			Height = maxRow - minRow;
			Width = maxCol - minCol;

			//adjust everything relative to 0,0
			for (int i = 0; i < _cells.Count; i++)
			{
				Tuple<Point, PoseEntry> item = _cells[i];
				Point pt = item.Item1;
				Point relPt = new Point(pt.X - minCol, pt.Y - minRow);
				_cells[i] = new Tuple<Point, PoseEntry>(relPt, item.Item2);
			}
		}
	}

	public class ColumnClipboardItem
	{
		public int Index;
		public string Name;
		public List<PoseEntry> Rows = new List<PoseEntry>();

		public ColumnClipboardItem(DataGridViewColumn column)
		{
			Name = column.Name;
			Index = column.DisplayIndex;
			DataGridView grid = column.DataGridView;
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				DataGridViewRow row = grid.Rows[i];
				DataGridViewCell cell = row.Cells[column.Name];
				PoseEntry pose = cell.Tag as PoseEntry;
				if (pose != null)
				{
					PoseEntry copy = new PoseEntry();
					pose.CopyPropertiesInto(copy);
					Rows.Add(copy);
				}
				else
				{
					Rows.Add(null);
				}
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class ColumnClipboardData
	{
		private List<ColumnClipboardItem> _columns = new List<ColumnClipboardItem>();

		public ColumnClipboardData(DataGridViewSelectedColumnCollection selection)
		{
			int left = int.MaxValue;
			foreach (DataGridViewColumn col in selection)
			{
				left = Math.Min(left, col.DisplayIndex);
				ColumnClipboardItem item = new ColumnClipboardItem(col);
				_columns.Add(item);
			}

			//make all columns relative to the leftmost
			foreach (ColumnClipboardItem item in _columns)
			{
				item.Index -= left;
			}
		}

		public IEnumerable<ColumnClipboardItem> Columns
		{
			get
			{
				return _columns;
			}
		}
	}

	public class RowClipboardData
	{
		private List<PoseStage> _rows = new List<PoseStage>();

		public IEnumerable<PoseStage> Rows
		{
			get { return _rows; }
		}

		public RowClipboardData(DataGridViewSelectedRowCollection selection)
		{
			int top = int.MaxValue;
			foreach (DataGridViewRow row in selection)
			{
				top = Math.Min(top, row.Index);
				PoseStage data = null;
				PoseStage stage = row.Tag as PoseStage;
				if (stage != null)
				{
					data = new PoseStage();
					stage.CopyPropertiesInto(data);
					data.Poses = new ObservableCollection<PoseEntry>();
					foreach (PoseEntry pose in stage.Poses)
					{
						data.Poses.Add(pose.Clone() as PoseEntry);
					}
				}
				_rows.Add(data);
			}

			foreach (PoseStage stage in _rows)
			{
				if (stage != null)
				{
					stage.Stage -= top;
				}
			}
		}
	}
}
