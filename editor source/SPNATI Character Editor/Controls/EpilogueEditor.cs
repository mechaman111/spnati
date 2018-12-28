using Desktop;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	[Activity(typeof(Character), 40)]
	public partial class EpilogueEditor : Activity
	{
		private Character _character;
		private Epilogue _ending;
		private bool _populatingEnding;

		public EpilogueEditor()
		{
			InitializeComponent();

			Enabled = _character != null;
			EnableFields(false);
		}

		public override string Caption
		{
			get
			{
				return "Epilogues";
			}
		}

		private void EnableFields(bool enabled)
		{
			txtTitle.Enabled = enabled;
			cboGender.Enabled = enabled;
			cmdDeleteEnding.Enabled = enabled;
			cmdAdvancedConditions.Enabled = enabled;
		}

		protected override void OnInitialize()
		{
			SetCharacter(Record as Character);
		}

		protected override void OnFirstActivate()
		{
			EnableForEdit();
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			_ending = null;
		}

		private void ClearFields()
		{
			txtTitle.Text = "";
		}

		private void PopulateEndingCombo()
		{
			_populatingEnding = true;
			//Endings combo
			cboEnding.Items.Clear();
			foreach (Epilogue ending in _character.Endings)
			{
				cboEnding.Items.Add(ending);
			}
			_populatingEnding = false;
		}

		private void cboEnding_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingEnding)
				return;
			Epilogue epilogue = cboEnding.SelectedItem as Epilogue;
			LoadEnding(epilogue);
		}

		private void cmdAddEnding_Click(object sender, EventArgs e)
		{
			CreateNewEnding();
		}

		private void cmdDeleteEnding_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete this ending?", "Delete Ending", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				RemoveEnding();
			}
		}

		private void CreateNewEnding()
		{
			Epilogue ending = new Epilogue();
			_character.Endings.Add(ending);
			PopulateEndingCombo();
			cboEnding.SelectedItem = ending;
		}

		private void LoadEnding(Epilogue ending)
		{
			SaveEnding();
			_ending = ending;

			if (_ending == null)
			{
				cboGender.SelectedIndex = -1;
				txtTitle.Text = "";
				EnableFields(false);
			}
			else
			{
				cboGender.Text = ending.Gender;
				txtTitle.Text = ending.Title;
				EnableFields(true);
			}
			canvas.SetEpilogue(_ending, _character);
		}

		public override void Save()
		{
			SaveEnding();
		}

		private void SaveEnding()
		{
			if (_ending == null)
				return;
			_ending.Title = txtTitle.Text;
			_ending.Gender = cboGender.Text;

			//Strip out any empty screens
			for (int i = _ending.Screens.Count - 1; i >= 0; i--)
			{
				Screen screen = _ending.Screens[i];
				if (screen.IsEmpty)
				{
					_ending.Screens.RemoveAt(i);
				}
			}
		}

		private void RemoveEnding()
		{
			if (_character == null || _ending == null)
				return;
			_character.Endings.Remove(_ending);
			_ending = null;
			LoadEnding(null);
			PopulateEndingCombo();
			if (_character.Endings.Count > 0)
			{
				cboEnding.SelectedIndex = 0;
			}
		}

		
		private void EnableForEdit()
		{
			Enabled = true;
			PopulateEndingCombo();

			if (_character.Endings.Count > 0)
			{
				cboEnding.SelectedIndex = 0;
			}
			else
			{
				ClearFields();
			}
		}

		private void cmdAdvancedConditions_Click(object sender, EventArgs e)
		{
			new AdvancedEpilogueConditions(_character, _ending).ShowDialog();
		}
	}
}
