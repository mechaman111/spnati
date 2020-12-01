using Desktop.Skinning;
using ImagePipeline;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class PoseSelectionForm : SkinnedForm
	{
		private ISkin _original;
		private ISkin _character;
		private PoseMatrix _matrix;
		private PoseSheet _sheet;
		private bool _building;

		public PoseEntry Cell { get; private set; }

		private static readonly Bitmap EmptyImage = new Bitmap(1, 1);

		public PoseSelectionForm()
		{
			InitializeComponent();
		}

		public void SetData(PoseMatrix matrix, PoseSheet initialSheet, PoseEntry pose)
		{
			_original = _character = matrix.Character;
			_matrix = matrix;
			_sheet = initialSheet;

			//recCharacter.RecordType = typeof(Character);
			//recCharacter.Record = _character;
			//recCharacter.RecordChanged += RecCharacter_RecordChanged;

			BuildControls();

			//Select the current sheet and cell
			SelectSheetAndCell(_sheet, pose);
		}

		private void SelectSheetAndCell(PoseSheet sheet, PoseEntry pose)
		{
			for (int i = 0; i < tabControl.TabPages.Count; i++)
			{
				TabPage page = tabControl.TabPages[i];
				if (page.Tag == _sheet)
				{
					tabControl.SelectedTab = page;
					if (pose != null)
					{
						//select the cell too
						foreach (DataGridViewRow row in grid.Rows)
						{
							foreach (DataGridViewCell cell in row.Cells)
							{
								if (cell.Tag == pose)
								{
									cell.Selected = true;
									return;
								}
							}
						}
					}
					return;
				}
			}
		}

		private void RecCharacter_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			ISkin character = recCharacter.Record as ISkin ?? _original;
			if (_original != character)
			{
				_matrix = CharacterDatabase.GetPoseMatrix(character);
				if (_matrix.Sheets.Count > 0)
				{
					_sheet = _matrix.Sheets[0];
				}
				else
				{
					_sheet = null;
				}
				_character = character;

				BuildControls();
			}
		}

		private void BuildControls()
		{
			if (_building) { return; }
			_building = true;
			BuildTabStrip();
			BuildGrid();
			_building = false;
		}

		private void BuildTabStrip()
		{
			tabControl.TabPages.Clear();
			foreach (PoseSheet sheet in _matrix.Sheets)
			{
				AddTab(sheet);
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
			grid.Rows.Clear();
			grid.Columns.Clear();

			if (_sheet == null)
			{
				lblMissingMatrix.Visible = true;
				grid.Visible = false;
				return;
			}
			grid.Visible = true;
			lblMissingMatrix.Visible = false;

			grid.TopLeftHeaderCell.Value = "Sheet";
			grid.RowHeadersWidth = 80;
			grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			bool columnsBuilt = false;

			foreach (PoseStage stage in _sheet.Stages)
			{
				if (!columnsBuilt)
				{
					foreach (PoseEntry pose in stage.Poses)
					{
						if (!string.IsNullOrEmpty(pose.Key))
						{
							AddColumn(pose.Key);
						}
					}
					columnsBuilt = true;
				}
				DataGridViewRow row = grid.Rows[grid.Rows.Add()];
				row.Tag = stage;

				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Value = EmptyImage;
				}

				if (_sheet.IsGlobal)
				{
					row.HeaderCell.Value = stage.Name ?? stage.Stage.ToString();
				}
				else
				{
					row.HeaderCell.Value = $"{stage.Stage}";
				}

				foreach (PoseEntry pose in stage.Poses)
				{
					if (string.IsNullOrEmpty(pose.Key))
					{
						continue;
					}
					DataGridViewCell cell = row.Cells[pose.Key];
					cell.Tag = pose;
					UpdateCell(cell, pose);
				}
			}
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

			//give a default empty value to any rows that were previously added
			foreach (DataGridViewRow existingRow in grid.Rows)
			{
				DataGridViewCell existingCell = existingRow.Cells[col.Name];
				existingCell.Value = EmptyImage;
			}
			return col;
		}

		private void cmdCreate_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			if (grid.SelectedCells.Count > 0)
			{
				PoseEntry cell = grid.SelectedCells[0].Tag as PoseEntry;
				if (cell != null)
				{
					DialogResult = DialogResult.OK;
					Cell = cell;
				}
			}
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_building) { return; }
			_sheet = tabControl.SelectedTab.Tag as PoseSheet;
			BuildGrid();
		}

		private void UpdateCell(DataGridViewCell cell, PoseEntry pose)
		{
			if (cell == null)
			{
				return;
			}

			if (!string.IsNullOrEmpty(pose.Code))
			{
				FileStatus status = _matrix.GetStatus(pose);
				switch (status)
				{
					case FileStatus.Imported:
						cell.Value = Properties.Resources.Checkmark;
						break;
					case FileStatus.Missing:
						cell.Value = Properties.Resources.FileMissing;
						break;
					case FileStatus.OutOfDate:
						cell.Value = Properties.Resources.FileOutdated;
						break;
				}
			}
			else
			{
				cell.Value = EmptyImage;
			};
		}

		private void grid_SelectionChanged(object sender, EventArgs e)
		{
			if (_building)
			{
				return;
			}
			if (grid.SelectedCells.Count > 0)
			{
				DataGridViewCell cell = grid.SelectedCells[0];
				PoseEntry pose = cell.Tag as PoseEntry;
				if (pose != null)
				{
					string path = _matrix.GetFilePath(pose);
					if (File.Exists(path))
					{
						using (Bitmap img = new Bitmap(path))
						{
							SetPreview(img);
							return;
						}
					}
				}
				SetPreview(null);
			}
		}

		private void SetPreview(Image image)
		{
			if (image != null)
			{
				image = new Bitmap(image);
			}
			preview.SetImage(image);
		}

		private void PoseSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void cmdGenerate_Click(object sender, EventArgs e)
		{
			if (grid.SelectedCells.Count > 0)
			{
				DataGridViewCell cell = grid.SelectedCells[0];
				PoseEntry pose = cell.Tag as PoseEntry;
				if (pose != null)
				{
					ImportPose(pose);
				}
			}
		}

		private async void ImportPose(PoseEntry pose)
		{
			Enabled = false;
			Cursor = Cursors.WaitCursor;
			try
			{
				Image img = await PipelineImporter.Import(pose, true, false);
				if (img == null)
				{
					//Something went wrong. Stop here.
					FailedImport import = new FailedImport();
					import.ShowDialog();
					Enabled = true;
					Cursor = Cursors.Default;
					return;
				}

				//Update the relevant grid row
				MethodInvoker invoker = delegate ()
				{
					foreach (DataGridViewRow row in grid.Rows)
					{
						foreach (DataGridViewCell cell in row.Cells)
						{
							PoseEntry entry = cell.Tag as PoseEntry;
							if (entry != null && entry == pose)
							{
								SetPreview(img);
								UpdateCell(cell, entry);
							}
						}
					}
				};
				grid.Invoke(invoker);
			}
			catch (Exception e)
			{
				ErrorLog.LogError(string.Format("Error in import pipeline: {0}, {1}", pose.GetFullKey(), e.Message));
			}
			Cursor = Cursors.Default;
			Enabled = true;
		}
	}
}
