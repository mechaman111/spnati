using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class WardrobeEditor : UserControl
	{
		private Character _character;

		public WardrobeEditor()
		{
			InitializeComponent();
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			PopulateGrid();
		}

		private void PopulateGrid()
		{
			gridWardrobe.Rows.Clear();
			if (_character == null)
				return;
			for(int i = _character.Layers - 1; i >= 0; i--)
			{
				Clothing c = _character.Wardrobe[i];
				DataGridViewRow row = gridWardrobe.Rows[gridWardrobe.Rows.Add()];
				try
				{
					row.Cells["ColName"].Value = c.Name;
					row.Cells["ColLower"].Value = c.Lowercase;
					row.Cells["ColType"].Value = c.Type;
					row.Cells["ColPosition"].Value = c.Position;
				}
				catch { }
			}
		}

		public void Save()
		{
			if (_character == null)
				return;
			//TODO: Track the exact changes (maybe with a tag indicating their original order, or move history?)

			_character.Wardrobe.Clear();
			foreach (DataGridViewRow row in gridWardrobe.Rows)
			{
				string name = row.Cells["ColName"].Value?.ToString();
				string lower = row.Cells["ColLower"].Value?.ToString();
				string type = row.Cells["ColType"].Value?.ToString();
				string position = row.Cells["ColPosition"].Value?.ToString();
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lower))
					continue;
				Clothing c = new Clothing()
				{
					Name = name,
					Lowercase = lower,
					Type = type,
					Position = position
				};
				_character.Wardrobe.Insert(0, c);
			}
		}

		private void cmdClothesUp_Click(object sender, EventArgs e)
		{

		}

		private void cmdClothesDown_Click(object sender, EventArgs e)
		{

		}
	}
}
