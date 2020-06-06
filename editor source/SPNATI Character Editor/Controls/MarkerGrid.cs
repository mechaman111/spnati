using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class MarkerGrid : UserControl
	{
		private Character _character;

		public event EventHandler<Marker> SelectionChanged;
		public bool AllowPrivate { get; set; }

		public bool ReadOnly
		{
			get { return gridMarkers.ReadOnly; }
			set
			{
				if (value)
				{
					gridMarkers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					gridMarkers.AllowUserToDeleteRows = false;
					ColDelete.Visible = false;
				}
				else
				{
					gridMarkers.SelectionMode = DataGridViewSelectionMode.CellSelect;
					gridMarkers.AllowUserToDeleteRows = false;
					ColDelete.Visible = true;
				}
				gridMarkers.ReadOnly = value;
			}
		}

		public MarkerGrid()
		{
			InitializeComponent();
			ColScope.Items.AddRange(new object[] { "Private", "Public" });
			ColPersistent.TrueValue = true;
			ColDelete.Flat = true;
		}

		public void SetCharacter(Character character)
		{
			_character = character;

			DataGridViewColumn col = gridMarkers.Columns["ColScope"];
			col.Visible = AllowPrivate;
			
			gridMarkers.Rows.Clear();
			if (_character == null)
				return;
			List<Marker> markers = new List<Marker>();
			foreach (var marker in _character.Markers.Value.Values)
			{
				markers.Add(marker);
			}
			markers.Sort();
			foreach (var marker in markers)
			{
				if (!AllowPrivate && marker.Scope == MarkerScope.Private)
					continue;
				AddMarkerToGrid(marker);
			}
			if (markers.Count > 0)
			{
				SelectionChanged?.Invoke(this, markers[0]);
			}
		}

		public void Save()
		{
			if (_character == null)
				return;
			gridMarkers.EndEdit();
			List<Marker> oldMarkers = _character.Markers.Value.Values.ToList();
			_character.Markers.Value.Clear();
			foreach (DataGridViewRow row in gridMarkers.Rows)
			{
				string name = row.Cells["ColName"].Value?.ToString();
				string scopeStr = row.Cells["ColScope"].Value?.ToString();
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(scopeStr))
					continue;
				string desc = row.Cells["ColDescription"].Value?.ToString();
				bool persistent = row.Cells[nameof(ColPersistent)].Value != null && (bool)row.Cells[nameof(ColPersistent)].Value;
				if (persistent)
				{
					_character.Behavior.PersistentMarkers.Add(name);
				}
				else
				{
					_character.Behavior.PersistentMarkers.Remove(name);
				}
				Marker marker = oldMarkers.Find(m => m.Name == name) ?? new Marker(name);
				marker.Description = desc;
				MarkerScope scope;
				Enum.TryParse(scopeStr, out scope);
				marker.Scope = scope;
				_character.Markers.Value.Add(marker);
			}
		}

		private void AddMarkerToGrid(Marker marker)
		{
			DataGridViewRow row = gridMarkers.Rows[gridMarkers.Rows.Add()];
			row.Tag = marker;
			row.Cells["ColName"].Value = marker.Name;
			row.Cells["ColDescription"].Value = marker.Description?.ToString();
			row.Cells[nameof(ColPersistent)].Value = _character.Behavior.PersistentMarkers.Contains(marker.Name);
			try
			{
				row.Cells["ColScope"].Value = marker.Scope.ToString();
			}
			catch
			{
				ErrorLog.LogError(string.Format("Marker report found a marker with invalid scope for {0}: {1}. Scope must be Private or Public.", _character, marker));
			}
		}

		private void gridMarkers_SelectionChanged(object sender, EventArgs e)
		{
			Marker marker = null;
			if (gridMarkers.SelectedRows.Count > 0)
			{
				DataGridViewRow row = gridMarkers.SelectedRows[0];
				marker = row.Tag as Marker;
			}
			SelectionChanged?.Invoke(this, marker);
		}

		private void gridMarkers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColDelete.Index)
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

		private void gridMarkers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.ColumnIndex >= gridMarkers.Columns.Count || e.RowIndex == gridMarkers.NewRowIndex || ReadOnly)
			{
				return;
			}
			DataGridViewColumn col = gridMarkers.Columns[e.ColumnIndex];
			if (col == ColDelete)
			{
				gridMarkers.Rows.RemoveAt(e.RowIndex);
			}
		}
	}
}
