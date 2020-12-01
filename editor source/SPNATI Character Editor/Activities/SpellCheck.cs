using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Services;
using SPNATI_Character_Editor.Workspaces;
using System;
using System.Drawing;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 550, DelayRun = true, Caption = "Spell Check")]
	public partial class SpellCheck : Activity
	{
		private Misspelling _currentMisspelling;
		private SpellCheckerService _spellchecker;

		private Character _character;		

		public SpellCheck()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Spell Check"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_spellchecker = Workspace.GetData<SpellCheckerService>(CharacterWorkspace.SpellCheckerService);
		}

		protected override void OnActivate()
		{
			cmdIgnore.Focus();
			_spellchecker.Run();
			GetNextMisspelling();
		}

		protected override void OnSkinChanged(Skin skin)
		{
			txtLine.ForeColor = skin.Surface.ForeColor;
			txtLine.BackColor = skin.FieldDisabledBackColor;
		}

		private void GetNextMisspelling()
		{
			_currentMisspelling = null;
			if (_spellchecker.RemainingMisspellings == 0)
			{
				txtWord.Text = "";
				lstSuggestions.Items.Clear();
				txtLine.Clear();
				panelFix.Enabled = false;
				lblGood.Visible = true;
				return;
			}
			panelFix.Enabled = true;
			lblGood.Visible = false;
			Misspelling ms = _spellchecker.GetNextMispelling();
			DisplayWord(ms);
		}

		private void DisplayImage(Case workingCase, PoseMapping pose)
		{
			if (pose != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, pose, workingCase.Stages[0]));
			}
		}

		private void DisplayWord(Misspelling misspelling)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			_currentMisspelling = misspelling;
			lblProgress.Text = $"Remaining: {_spellchecker.RemainingMisspellings}";
			DisplayImage(misspelling.Case, misspelling.Line.Pose);

			int start = misspelling.Index;
			string word = misspelling.Word;
			string text = misspelling.Line.Text;

			txtLine.Clear();
			if (start >= 0)
			{
				txtLine.SelectionStart = start;
				txtLine.SelectionLength = word.Length;
				txtLine.AppendText(text.Substring(0, start));
				txtLine.SelectionFont = new Font(txtLine.Font, FontStyle.Bold);
				txtLine.SelectionColor = skin.BadForeColor;
				txtLine.AppendText(word);
				txtLine.SelectionFont = new Font(txtLine.Font, FontStyle.Regular);
				txtLine.SelectionColor = txtLine.ForeColor;
				txtLine.AppendText(text.Substring(start + word.Length));
			}
			else
			{
				txtLine.AppendText(text);
			}

			txtWord.Text = word;

			lstSuggestions.Items.Clear();
			foreach (string suggestion in _spellchecker.GetSuggestions(word))
			{
				lstSuggestions.Items.Add(suggestion);
			}
		}

		private void txtWord_Enter(object sender, EventArgs e)
		{
			lstSuggestions.SelectedIndex = -1;
		}

		private void cmdIgnore_Click(object sender, EventArgs e)
		{
			_spellchecker.Advance();
			GetNextMisspelling();
		}

		private void cmdIgnoreAll_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			_spellchecker.IgnoreWord(word);
			GetNextMisspelling();
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			_spellchecker.AddWord(word);
			GetNextMisspelling();
		}

		private void cmdChange_Click(object sender, EventArgs e)
		{
			string replacement = GetReplacement();
			_currentMisspelling.Replace(replacement);
			_spellchecker.Advance();
			GetNextMisspelling();
		}

		private void cmdChangeAll_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			string replacement = GetReplacement();
			_spellchecker.Replace(word, replacement);
			GetNextMisspelling();
		}

		private string GetReplacement()
		{
			string replacement = null;
			if (lstSuggestions.SelectedItem != null)
			{
				replacement = lstSuggestions.SelectedItem.ToString();
			}
			else
			{
				replacement = txtWord.Text;
			}
			return replacement;
		}

		private void cmdGoto_Click(object sender, EventArgs e)
		{
			Shell.Instance.Launch<Character, DialogueEditor>(_character, new ValidationContext(new Stage(_currentMisspelling.Case.Stages[0]), _currentMisspelling.Case, _currentMisspelling.Line));
		}
	}
}
