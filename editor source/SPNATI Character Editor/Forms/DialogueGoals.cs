using Desktop.Skinning;

namespace SPNATI_Character_Editor.Forms
{
	public partial class DialogueGoals : SkinnedForm
	{
		private CharacterHistory _history;

		public DialogueGoals()
		{
			InitializeComponent();
		}

		public void SetData(Character character)
		{
			_history = CharacterHistory.Get(character, false);
			valDaily.Value = _history.DailyGoal;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			_history.DailyGoal = (int)valDaily.Value;
			Close();
		}
	}
}
