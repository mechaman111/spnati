using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	/// <summary>
	/// THIS ISN'T READY FOR PRIMETIME YET
	/// </summary>
	public partial class SituationLinker : Form
	{
		private Situation _situation;
		private Character _character;

		public SituationLinker()
		{
			InitializeComponent();
		}

		public void SetData(Character character, Situation situation)
		{
			_situation = situation;
			_character = character;

			//find a case that looks like the situation
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				if (c.MatchesConditions(situation.LegacyCase))
				{
					HashSet<int> stages = new HashSet<int>();
					stages.Add(c.Stages[0]);
					gridLines.SetData(character, new Stage(c.Stages[0]), c, stages, ImageLibrary.Get(_character));
					break;
				}
			}

			_situation = situation;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
