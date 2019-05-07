using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	[Activity(typeof(Character), 1)]
	[Activity(typeof(Costume), 5)]
	public partial class WardrobeEditor : Activity
	{
		private IWardrobe _wardrobe;
		private bool _populatingWardrobe;
		private Queue<WardrobeChange> _wardrobeChanges = new Queue<WardrobeChange>();
		private WardrobeRestrictions _restrictions;

		public WardrobeEditor()
		{
			InitializeComponent();
			ColPlural.TrueValue = true;
		}

		public override string Caption
		{
			get { return "Wardrobe"; }
		}

		protected override void OnInitialize()
		{
			_wardrobe = Record as IWardrobe;

			_restrictions = _wardrobe.GetWardrobeRestrictions();
			
			if (_restrictions.HasFlag(WardrobeRestrictions.LayerCount))
			{
				_populatingWardrobe = true;
				gridWardrobe.AllowUserToAddRows = false;
				gridWardrobe.AllowUserToDeleteRows = false;
				cmdClothesDown.Enabled = false;
				cmdClothesUp.Enabled = false;
				_populatingWardrobe = false;
			}
		}

		protected override void OnFirstActivate()
		{
			PopulateGrid();
		}

		private void PopulateGrid()
		{
			_populatingWardrobe = true;
			gridWardrobe.Rows.Clear();
			for (int i = _wardrobe.Layers - 1; i >= 0; i--)
			{
				Clothing c = _wardrobe.GetClothing(i);
				try
				{
					DataGridViewRow row = gridWardrobe.Rows[gridWardrobe.Rows.Add(c.FormalName, c.GenericName, c.Plural, c.Type, c.Position)];
					row.Tag = c;
					if (_restrictions.HasFlag(WardrobeRestrictions.LayerTypes))
					{
						row.Cells["ColType"].ReadOnly = true;
					}
				}
				catch { }
			}
			_populatingWardrobe = false;
		}

		public override void Save()
		{
			if (gridWardrobe.SelectedCells.Count > 0)
			{
				SaveLayer(gridWardrobe.SelectedCells[0].OwningRow.Index);
			}
			ApplyWardrobeChanges();
		}

		private void SaveLayer(int rowIndex)
		{
			DataGridViewRow row = gridWardrobe.Rows[rowIndex];
			string name = row.Cells[1].Value?.ToString();
			if (string.IsNullOrEmpty(name)) { return; }
			string lowercase = row.Cells[1].Value?.ToString();
			bool plural = row.Cells[2].Value != null ? (bool)row.Cells[2].Value : false;
			string type = row.Cells[3].Value?.ToString();
			string position = row.Cells[4].Value?.ToString();
			Clothing layer = row.Tag as Clothing;
			if (layer != null)
			{
				layer.FormalName = name;
				layer.GenericName = lowercase;
				layer.Plural = plural;
				layer.Type = type;
				layer.Position = position;
			}
			else
			{
				
			}
		}

		private void ApplyWardrobeChanges()
		{
			if (_wardrobeChanges.Count > 0)
			{
				_wardrobe.ApplyWardrobeChanges(_wardrobeChanges);
				_wardrobeChanges.Clear();
				Workspace.SendMessage(WorkspaceMessages.WardrobeUpdated);
			}
		}

		private void cmdClothesUp_Click(object sender, EventArgs e)
		{
			if (gridWardrobe.SelectedCells.Count == 0) { return; }
			int rowIndex = gridWardrobe.SelectedCells[0].OwningRow.Index;
			if (rowIndex == 0) { return; }

			int colIndex = gridWardrobe.SelectedCells[0].OwningColumn.Index;
			DataGridViewRow row = gridWardrobe.Rows[rowIndex];
			if (row.IsNewRow) { return; }

			Clothing layer = row.Tag as Clothing;
			int index = _wardrobe.MoveUp(layer);
			_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.MoveUp, index));

			_populatingWardrobe = true;
			gridWardrobe.Rows.Remove(row);
			gridWardrobe.Rows.Insert(rowIndex - 1, row);
			gridWardrobe.ClearSelection();
			gridWardrobe.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
			_populatingWardrobe = false;
		}

		private void cmdClothesDown_Click(object sender, EventArgs e)
		{
			if (gridWardrobe.SelectedCells.Count == 0) { return; }
			int rowIndex = gridWardrobe.SelectedCells[0].OwningRow.Index;
			if (rowIndex >= gridWardrobe.Rows.Count - 2) { return; }

			int colIndex = gridWardrobe.SelectedCells[0].OwningColumn.Index;
			DataGridViewRow row = gridWardrobe.Rows[rowIndex];
			if (row.IsNewRow) { return; }

			Clothing layer = row.Tag as Clothing;
			int index = _wardrobe.MoveDown(layer);
			_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.MoveDown, index));

			_populatingWardrobe = true;
			gridWardrobe.Rows.Remove(row);
			gridWardrobe.Rows.Insert(rowIndex + 1, row);
			gridWardrobe.ClearSelection();
			gridWardrobe.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
			_populatingWardrobe = false;
		}

		private void gridWardrobe_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (_populatingWardrobe) { return; }
			Clothing layer = _wardrobe.GetClothing(_wardrobe.Layers - e.RowCount - 1);
			if (layer != null)
			{
				int index = _wardrobe.RemoveLayer(layer);
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.Remove, index));
			}
		}

		private void gridWardrobe_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (_populatingWardrobe) { return; }

			Clothing layer = new Clothing();
			int index = _wardrobe.AddLayer(layer);
			DataGridViewRow row = gridWardrobe.Rows[index];
			row.Tag = layer;
			_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.Add, index));
		}

		private void gridWardrobe_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			SaveLayer(e.RowIndex);
		}

		private void gridWardrobe_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			DataGridViewRow row = gridWardrobe.Rows[e.RowIndex];
			if (row.IsNewRow) { return; }
			if (e.ColumnIndex == 0 && string.IsNullOrEmpty(e.FormattedValue?.ToString()))
			{
				MessageBox.Show("Clothing cannot have an empty name.");
				e.Cancel = true;
			}
			if (e.ColumnIndex == ColPosition.Index && _restrictions.HasFlag(WardrobeRestrictions.LayerTypes))
			{
				string type = row.Cells["ColType"].Value?.ToString();
				string position = row.Cells["ColPosition"].Value?.ToString();
				if (type == "important" && position != e.FormattedValue?.ToString() && (position == "upper" || position == "lower"))
				{
					MessageBox.Show("Cannot change position for an important layer.");
					e.Cancel = true;
				}
			}
		}
	}
}
