using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	//TODO: Turn the Stage field into a dropdown with character-specific stage names instead of raw numbers

	public partial class StageSpecificGrid : UserControl
	{
		public string Label
		{
			get { return ColValue.HeaderText; }
			set { ColValue.HeaderText = value; }
		}

		public StageSpecificGrid()
		{
			InitializeComponent();
		}

		public void Set(List<StageSpecificValue> values)
		{
			grid.Rows.Clear();
			foreach (StageSpecificValue value in values)
			{
				grid.Rows.Add(new string[] { value.Value, value.Stage.ToString() });
			}
		}

		public List<StageSpecificValue> Values
		{
			get
			{
				List<StageSpecificValue> list = new List<StageSpecificValue>();
				foreach (DataGridViewRow row in grid.Rows)
				{
					string name = row.Cells[0].Value?.ToString();
					if (string.IsNullOrEmpty(name))
					{
						continue;
					}
					int stage;
					string stageString = row.Cells[1].Value?.ToString() ?? "0";
					int.TryParse(stageString, out stage);
					StageSpecificValue value = new StageSpecificValue(stage, name);
					list.Add(value);
				}
				return list;
			}
		}
	}
}
