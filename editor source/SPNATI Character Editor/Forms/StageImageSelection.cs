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
		private ImageLibrary _imageLibrary;

		public StageImageSelection()
		{
			InitializeComponent();
			ColClear.Flat = true;
		}
		public StageImageSelection(Character character, DialogueLine line, Case workingCase) : this()
		{
			_line = line;
			_character = character;
			_imageLibrary = ImageLibrary.Get(_character);

			foreach (int stage in workingCase.Stages)
			{
				DataGridViewRow row = grid.Rows[grid.Rows.Add()];
				row.Tag = stage;
				row.Cells[nameof(ColStage)].Value = _character.LayerToStageName(stage);

				UpdateAvailableImages(stage);

				if (_line.StageImages.ContainsKey(stage))
				{
					LineImage lineImage = _line.StageImages[stage];
					string imageKey = lineImage.Image;
					if (!lineImage.IsGenericImage)
					{
						SkinnedDataGridViewComboBoxCell cell = row.Cells[nameof(ColImage)] as SkinnedDataGridViewComboBoxCell;
						imageKey = DialogueLine.GetStageImage(stage, imageKey);
						SetImage(cell, imageKey);
					}
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
			cell.ValueType = typeof(CharacterImage);
			cell.Items.Clear();
			List<CharacterImage> images = new List<CharacterImage>();
			images.AddRange(_imageLibrary.GetImages(stageId));
			if (Config.UsePrefixlessImages)
			{
				foreach (CharacterImage img in _imageLibrary.GetImages(-1))
				{
					string file = img.Name;
					if (!_imageLibrary.FilterImage(_character, file))
					{
						images.Add(img);
					}
				}
			}
			foreach (CharacterImage img in images)
			{
				cell.Items.Add(img);
			}
		}

		private void SetImage(SkinnedDataGridViewComboBoxCell cell, string key)
		{
			string defaultKey = DialogueLine.GetDefaultImage(key);
			foreach (object item in cell.Items)
			{
				CharacterImage image = item as CharacterImage;
				if (image != null && image.DefaultName == defaultKey)
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
			string image = row.Cells[nameof(ColImage)].Value?.ToString();
			ShowImage(image, (int)row.Tag);
		}

		private void ShowImage(string image, int stage)
		{
			CharacterImage img = null;
			img = _imageLibrary.Find(image);
			if (img == null)
			{
				image = DialogueLine.GetStageImage(stage, DialogueLine.GetDefaultImage(image));
				img = _imageLibrary.Find(image);
			}
			if (img != null)
			{
				Desktop.IWorkspace ws = Desktop.Shell.Instance.GetWorkspace(_character);
				if (ws != null)
				{
					ws.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			_line.StageImages.Clear();
			foreach (DataGridViewRow row in grid.Rows)
			{
				CharacterImage img = row.Cells[ColImage.Index].Value as CharacterImage;
				if (img != null)
				{
					string name = img.Name;
					if (!img.IsGeneric)
					{
						name = DialogueLine.GetDefaultImage(name);
					}
					if (name == _line.Image)
					{
						continue;
					}
					LineImage li = new LineImage(img.Name, img.IsGeneric);
					_line.StageImages[(int)row.Tag] = li;
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
							CharacterImage img = item as CharacterImage;
							if (img != null && img.Name == name)
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
			ShowImage(comboBox.SelectedItem?.ToString(), stage);
		}
	}
}
