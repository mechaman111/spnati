using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	[Activity(typeof(Character), 5)]
	public partial class WardrobeEditor : Activity
	{
		private Character _character;
		private bool _populatingWardrobe;
		private Queue<WardrobeChange> _wardrobeChanges = new Queue<WardrobeChange>();

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
			_character = Record as Character;
			OpponentStatus status = Listing.Instance.GetCharacterStatus(_character.FolderName);
			if (status != OpponentStatus.Testing && status != OpponentStatus.Unlisted)
			{
				//For established characters, lock down changing the layer amount and order since it's hugely disruptive
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
			for (int i = _character.Layers - 1; i >= 0; i--)
			{
				Clothing c = _character.Wardrobe[i];
				try
				{
					DataGridViewRow row = gridWardrobe.Rows[gridWardrobe.Rows.Add(c.FormalName, c.GenericName, c.Plural, c.Type, c.Position)];
					row.Tag = c;
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
			string name = row.Cells[0].Value?.ToString();
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
		}

		private void ApplyWardrobeChanges()
		{
			if (_wardrobeChanges.Count > 0)
			{
				_character.ApplyWardrobeChanges(_wardrobeChanges);
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
			int index = _character.MoveUp(layer);
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
			int index = _character.MoveDown(layer);
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
			Clothing layer = _character.Wardrobe[_character.Layers -  e.RowIndex - 1];
			if (layer != null)
			{
				int index = _character.RemoveLayer(layer);
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.Remove, index));
			}
		}

		private void gridWardrobe_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (_populatingWardrobe) { return; }

			Clothing layer = new Clothing();
			DataGridViewRow row = gridWardrobe.Rows[e.RowIndex];
			row.Tag = layer;
			int index = _character.AddLayer(layer);
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
		}
	}
}
