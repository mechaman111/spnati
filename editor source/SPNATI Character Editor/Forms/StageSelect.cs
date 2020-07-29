using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class StageSelect : SkinnedForm
	{
		private List<int> _stages = new List<int>();

		public int Stage { get; private set; }

		public StageSelect()
		{
			InitializeComponent();
		}

		public void SetData(Character character, Case selectedCase, string caption, string instructions)
		{
			Text = caption;
			lblInstructions.Text = instructions;

			if (selectedCase == null)
			{
				for (int stage = 0; stage < character.Layers + Clothing.ExtraStages; stage++)
				{
					lstStages.Items.Add(character.LayerToStageName(stage));
					_stages.Add(stage);
				}
			}
			else
			{
				foreach (int stage in selectedCase.Stages)
				{
					lstStages.Items.Add(character.LayerToStageName(stage));
					_stages.Add(stage);
				}
			}
			lstStages.SelectedIndex = -1;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (lstStages.SelectedIndex == -1)
			{
				DialogResult = DialogResult.Cancel;
			}
			else
			{
				DialogResult = DialogResult.OK;
				Stage = _stages[lstStages.SelectedIndex];
			}
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
