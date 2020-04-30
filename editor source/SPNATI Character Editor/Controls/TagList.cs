using SPNATI_Character_Editor.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class TagList : UserControl
	{
		public TagList()
		{
			InitializeComponent();
		}

		private AutoCompleteStringCollection _tagAutoComplete = new AutoCompleteStringCollection();
		private BindableTagList _binding;
		private Character _character;
		private bool _adding;

		public void SetData(BindableTagList dataSource, Character character)
		{
			if (_binding != null)
			{
				_binding.TagAdded -= _binding_TagAdded;
				_binding.TagRemoved -= _binding_TagRemoved;
				_binding.TagModified -= _binding_TagModified;
			}

			_binding = dataSource;
			_character = character;
			BuildGrid();

			_binding.TagAdded += _binding_TagAdded;
			_binding.TagRemoved += _binding_TagRemoved;
			_binding.TagModified += _binding_TagModified;
		}

		private void _binding_TagRemoved(object sender, BindableTag tag)
		{
			RemoveRow(tag);
		}

		private void _binding_TagAdded(object sender, BindableTag tag)
		{
			foreach (DataGridViewRow row in grid.Rows)
			{
				if (row.Tag == tag)
				{
					return;
				}
			}
			AddRow(tag);
		}

		private void _binding_TagModified(object sender, BindableTag tag)
		{
			UpdateRow(tag);
		}

		private void BuildGrid()
		{
			_tagAutoComplete.Clear();
			foreach (Tag tag in TagDatabase.Tags)
			{
				_tagAutoComplete.Add(tag.Value);
			}

			grid.Rows.Clear();
			if (_binding == null) { return; }
			foreach (BindableTag tag in _binding.GetPopulated())
			{
				AddRow(tag);
			}
		}

		private void AddRow(BindableTag tag)
		{
			if (_adding) { return; }
			DataGridViewRow row = grid.Rows[grid.Rows.Add()];
			row.Tag = tag;
			row.Cells[0].Value = tag.Tag;
			row.Cells[1].Value = RangeToString(tag.Stages);
		}

		private void RemoveRow(BindableTag tag)
		{
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				if (grid.Rows[i].Tag == tag)
				{
					grid.Rows.RemoveAt(i);
					break;
				}
			}
		}

		private void UpdateRow(BindableTag tag)
		{
			for (int i = 0; i < grid.Rows.Count; i++)
			{
				if (grid.Rows[i].Tag == tag)
				{
					grid.Rows[i].Cells[1].Value = RangeToString(tag.Stages);
				}
			}
		}

		private string RangeToString(IList<int> stages)
		{
			List<int> sorted = new List<int>();
			sorted.AddRange(stages);
			sorted.Sort();

			if (sorted.Count == 0)
			{
				return "None";
			}
			if (sorted.Count == _character.Layers + Clothing.ExtraStages)
			{
				return "All";
			}
			List<string> blocks = new List<string>();
			int from = sorted[0];
			if (sorted.Count == 1)
			{
				return from.ToString();
			}
			for (int i = 1; i < sorted.Count; i++)
			{
				int stage = sorted[i];
				int last = sorted[i - 1];
				if (stage - last > 1)
				{
					if (last == from)
					{
						blocks.Add(from.ToString());
					}
					else
					{
						blocks.Add($"{from}-{last}");
					}
					from = stage;
				}
				if (i == sorted.Count - 1)
				{
					if (stage == from)
					{
						blocks.Add(from.ToString());
					}
					else
					{
						blocks.Add($"{from}-{stage}");
					}
				}
			}
			return string.Join(",", blocks);
		}

		private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (grid.CurrentCell.ColumnIndex == 0)
			{
				TextBox box = e.Control as TextBox;
				if (box != null)
				{
					box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
					box.AutoCompleteCustomSource = _tagAutoComplete;
					box.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}
			}
		}

		private void grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				string value = e.FormattedValue?.ToString();
				//make sure no other row has this value
				foreach (DataGridViewRow row in grid.Rows)
				{
					if (row.Index == e.RowIndex)
					{
						continue;
					}
					if (row.Cells[0].Value?.ToString() == value)
					{
						grid.EditingControl.ForeColor = System.Drawing.Color.Red;
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void grid_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				return;
			}
			DataGridViewRow row = grid.Rows[e.RowIndex];
			string key = row.Cells[0].Value?.ToString();
			BindableTag tag = row.Tag as BindableTag;
			if (tag == null)
			{
				if (!string.IsNullOrEmpty(key))
				{
					//added a tag
					tag = _binding.Get(key);
					if (tag == null)
					{
						tag = _binding.Add(key);
					}
					row.Tag = tag;

					//default stages to All
					_adding = true;
					for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
					{
						tag.Stages.Add(i);
					}
					_adding = false;
				}
			}
			else
			{
				if (string.IsNullOrWhiteSpace(key))
				{
					//removed a tag
					row.Tag = null;
					for (int i = tag.Stages.Count - 1; i >= 0; i--)
					{
						tag.Stages.RemoveAt(i);
					}
				}
				else if (key == tag.Tag)
				{
					//modified a tag
					return;
				}
			}
		}

		private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				DataGridViewRow row = grid.Rows[e.RowIndex];
				BindableTag tag = row.Tag as BindableTag;
				if (tag != null)
				{
					TagStageSelect form = new TagStageSelect();
					form.SetData(tag, _character);
					if (form.ShowDialog() == DialogResult.OK)
					{
						row.Cells[1].Value = RangeToString(tag.Stages);
					}
				}
			}
		}
	}
}
