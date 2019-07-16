using Desktop;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Skin), 0)]
	public partial class ThemeEditor : Activity
	{
		private Skin _skin;

		public ThemeEditor()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_skin = Record as Skin;
		}

		protected override void OnActivate()
		{
			SkinManager.Instance.SetSkin(_skin);
		}

		protected override void OnFirstActivate()
		{
			table.Data = _skin;
			PopulateGrid();
		}

		public override void Save()
		{
			ReadGrid();
			string json = Json.Serialize(_skin);
			string file = Path.Combine("Resources", "Skins", _skin.Name + ".skin");
			File.WriteAllText(file, json);
			SkinManager.Instance.SetSkin(_skin);
		}

		private void table_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Name" && e.PropertyName != "Group")
			{
				SkinManager.Instance.SetSkin(table.Data as Skin);
			}
		}

		private void PopulateGrid()
		{
			tmrRow.Start();
			gridCustom.Rows.Clear();
			foreach (KeyValuePair<string, Color> kvp in _skin.AppColors)
			{
				DataGridViewRow row = gridCustom.Rows[gridCustom.Rows.Add()];
				row.Cells[ColName.Index].Value = kvp.Key;
				DataGridViewButtonCell button = row.Cells[ColColor.Index] as DataGridViewButtonCell;
				button.Style.BackColor = kvp.Value;
			}
		}

		private void ReadGrid()
		{
			_skin.AppColors.Clear();
			foreach (DataGridViewRow row in gridCustom.Rows)
			{
				string key = row.Cells[ColName.Index].Value?.ToString();
				if (string.IsNullOrEmpty(key))
				{
					continue;
				}
				DataGridViewButtonCell button = row.Cells[ColColor.Index] as DataGridViewButtonCell;
				Color color = button.Style.BackColor;
				_skin.AppColors[key] = color;
			}
		}

		private void gridCustom_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == ColColor.Index)
			{
				DataGridViewButtonCell button = gridCustom.Rows[e.RowIndex].Cells[ColColor.Index] as DataGridViewButtonCell;
				colorDialog1.Color = button.Style.BackColor;
				if (colorDialog1.ShowDialog() == DialogResult.OK)
				{
					button.Style.BackColor = colorDialog1.Color;
				}
			}
		}

		private void tmrRow_Tick(object sender, EventArgs e)
		{
			tmrRow.Stop();
			RefreshColors();
		}

		private void RefreshColors()
		{
			foreach (DataGridViewRow row in gridCustom.Rows)
			{
				string key = row.Cells[ColName.Index].Value?.ToString();
				if (string.IsNullOrEmpty(key))
				{
					continue;
				}
				DataGridViewButtonCell button = row.Cells[ColColor.Index] as DataGridViewButtonCell;
				button.Style.BackColor = _skin.GetAppColor(key);
			}
		}

		protected override void OnSkinChanged(Skin skin)
		{
			base.OnSkinChanged(skin);
			tmrRow.Start();
		}
	}
}
