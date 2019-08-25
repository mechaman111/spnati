using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class StageImageSelection : SkinnedForm
	{
		private DialogueLine _line;
		private Character _character;

		public StageImageSelection()
		{
			InitializeComponent();
			ColClear.Flat = true;
		}
		public StageImageSelection(Character character, DialogueLine line, Case workingCase) : this()
		{
			_line = line;
			_character = character;

			foreach (int stage in workingCase.Stages)
			{
				DataGridViewRow row = grid.Rows[grid.Rows.Add()];
				row.Tag = stage;
				row.Cells[nameof(ColStage)].Value = _character.LayerToStageName(stage);

				UpdateAvailableImages(stage);

				if (_line.StageImages.ContainsKey(stage))
				{
					PoseMapping pose = _line.StageImages[stage];
					SkinnedDataGridViewComboBoxCell cell = row.Cells[nameof(ColImage)] as SkinnedDataGridViewComboBoxCell;
					SetImage(cell, pose);
				}
			}
		}

		private void UpdateAvailableImages(int stageId)
		{
			DataGridViewRow row = null;
			foreach (DataGridViewRow r in grid.Rows)
			{
				if ((int)r.Tag == stageId)
				{
					row = r;
					break;
				}
			}
			if (row == null) { return; }
			SkinnedDataGridViewComboBoxCell cell = row.Cells[ColImage.Index] as SkinnedDataGridViewComboBoxCell;
			cell.ValueType = typeof(PoseMapping);
			cell.Items.Clear();
			List<PoseMapping> images = new List<PoseMapping>();
			images.AddRange(_character.PoseLibrary.GetPoses(stageId));
			foreach (PoseMapping img in images)
			{
				cell.Items.Add(img);
			}
		}

		private void SetImage(SkinnedDataGridViewComboBoxCell cell, PoseMapping pose)
		{
			foreach (object item in cell.Items)
			{
				PoseMapping image = item as PoseMapping;
				if (image == pose)
				{
					cell.Value = image;
					return;
				}
			}
		}

		private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			DataGridViewRow row = grid.Rows[e.RowIndex];
			PoseMapping image = row.Cells[nameof(ColImage)].Value as PoseMapping;
			ShowImage(image, (int)row.Tag);
		}

		private void ShowImage(PoseMapping image, int stage)
		{
			if (image != null)
			{
				Desktop.IWorkspace ws = Desktop.Shell.Instance.GetWorkspace(_character);
				if (ws != null)
				{
					ws.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, stage));
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			_line.StageImages.Clear();
			foreach (DataGridViewRow row in grid.Rows)
			{
				PoseMapping img = row.Cells[ColImage.Index].Value as PoseMapping;
				if (img != null)
				{
					_line.StageImages[(int)row.Tag] = img;
				}
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private void grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			if (e.ColumnIndex == ColImage.Index)
			{
				SkinnedDataGridViewComboBoxCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex] as SkinnedDataGridViewComboBoxCell;
				if (cell != null)
				{
					object v = e.Value;
					if (v is string)
					{
						string name = v.ToString();
						foreach (object item in cell.Items)
						{
							PoseMapping img = item as PoseMapping;
							if (img != null && img.DisplayName == name)
							{
								e.Value = img;
								e.ParsingApplied = true;
								break;
							}
						}
					}
				}
			}
		}

		private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColClear.Index)
			{
				Image img = Properties.Resources.Delete;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
		}

		private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == ColClear.Index)
			{
				DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[ColImage.Index];
				cell.Value = null;
			}
		}

		private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			SkinnedComboBox cb = e.Control as SkinnedComboBox;
			if (cb != null)
			{
				cb.SelectedIndexChanged -= Cb_SelectedIndexChanged;
				cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
			}
		}

		private void Cb_SelectedIndexChanged(object sender, EventArgs e)
		{
			SkinnedComboBox comboBox = sender as SkinnedComboBox;
			if (grid.SelectedCells.Count == 0)
			{
				return;
			}
			int stage = (int)grid.SelectedCells[0].OwningRow.Tag;
			ShowImage(comboBox.SelectedItem as PoseMapping, stage);
		}
	}
}
