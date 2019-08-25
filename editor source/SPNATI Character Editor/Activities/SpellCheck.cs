using Desktop;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 550)]
	public partial class SpellCheck : Activity
	{
		private const int MaxSuggestions = 5;

		private ISpellChecker _spellchecker;
		private Character _character;
		private List<Case> _unprocessedCases = new List<Case>();
		private Queue<Misspelling> _misspellings = new Queue<Misspelling>();
		private Misspelling _currentMisspelling;

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
		}

		private void Behavior_CaseRemoved(object sender, Case theCase)
		{
			_unprocessedCases.Remove(theCase);
			FilterQueue(c => c.Case != theCase);
		}

		private void Behavior_CaseAdded(object sender, Case theCase)
		{
			_unprocessedCases.Add(theCase);
		}

		private void Behavior_CaseModified(object sender, Case theCase)
		{
			FilterQueue(c => c.Case != theCase);
			_unprocessedCases.Add(theCase);
		}

		public override void Destroy()
		{
			_character.Behavior.CaseAdded -= Behavior_CaseAdded;
			_character.Behavior.CaseRemoved -= Behavior_CaseRemoved;
			_character.Behavior.CaseModified -= Behavior_CaseModified;
			base.Destroy();
		}

		protected override void OnFirstActivate()
		{
			_spellchecker = SpellChecker.Instance;
			_character.Behavior.CaseAdded += Behavior_CaseAdded;
			_character.Behavior.CaseRemoved += Behavior_CaseRemoved;
			_character.Behavior.CaseModified += Behavior_CaseModified;

			_unprocessedCases.AddRange(_character.Behavior.GetWorkingCases());
		}

		protected override void OnActivate()
		{
			cmdIgnore.Focus();
			foreach (Case workingCase in _unprocessedCases)
			{
				ProcessCase(workingCase);
			}
			_unprocessedCases.Clear();
			GetNextMisspelling();
		}

		protected override void OnSkinChanged(Skin skin)
		{
			txtLine.ForeColor = skin.Surface.ForeColor;
			txtLine.BackColor = skin.FieldDisabledBackColor;
		}

		private void ProcessCase(Case workingCase)
		{
			foreach (DialogueLine line in workingCase.Lines)
			{
				Dictionary<string, int> visitedWords = new Dictionary<string, int>();
				string text = line.Text;
				string[] words = text.Split(new string[] { " ", ",", ".", "?", "!", ";", ":", "=", "<i>", "</i>", "*", "\"", "(", ")", "[", "]", "~", "/", "|" }, StringSplitOptions.RemoveEmptyEntries);
				for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
				{
					string word = words[wordIndex];
					if (word == "-" || word.Contains("_"))
					{
						continue;
					}
					if (!_spellchecker.CheckWord(word))
					{
						int count = visitedWords.Get(word);
						if (count == 0)
						{
							for (int i = 0; i < words.Length; i++)
							{
								if (words[i] == word)
								{
									break;
								}
								if (words[i].Contains(word))
								{
									count++;
								}
							}
						}
						count++;
						visitedWords[word] = count;
						int start = -1;
						for (int i = 0; i < count; i++)
						{
							start = text.IndexOf(word, start + 1);
						}
						Misspelling misspelling = new Misspelling()
						{
							Word = word,
							Case = workingCase,
							Line = line,
							Index = start
						};
						_misspellings.Enqueue(misspelling);
					}
				}
			}
		}

		private void GetNextMisspelling()
		{
			_currentMisspelling = null;
			if (_misspellings.Count == 0)
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
			Misspelling ms = _misspellings.Peek();
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
			lblProgress.Text = $"Remaining: {_misspellings.Count}";
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
			foreach (string suggestion in _spellchecker.GetSuggestions(word, MaxSuggestions))
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
			_misspellings.Dequeue();
			GetNextMisspelling();
		}

		private void cmdIgnoreAll_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			_spellchecker.IgnoreWord(txtWord.Text);
			_misspellings.Dequeue();
			FilterQueue(m => m.Word != word);

			GetNextMisspelling();
		}

		private void FilterQueue(Func<Misspelling, bool> predicate)
		{
			Queue<Misspelling> reducedList = new Queue<Misspelling>();
			foreach (Misspelling ms in _misspellings.Where(predicate))
			{
				reducedList.Enqueue(ms);
			}
			_misspellings = reducedList;
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			_spellchecker.AddWord(word, true);
			FilterQueue(m => m.Word != word);
			GetNextMisspelling();
		}

		private void cmdChange_Click(object sender, EventArgs e)
		{
			string replacement = GetReplacement();
			_currentMisspelling.Replace(replacement);
			_misspellings.Dequeue();
			GetNextMisspelling();
		}

		private void cmdChangeAll_Click(object sender, EventArgs e)
		{
			string word = _currentMisspelling.Word;
			string replacement = GetReplacement();
			FilterQueue(m =>
			{
				if (m.Word == word)
				{
					m.Replace(replacement);
				}
				return m.Word != word;
			});
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

		private class Misspelling
		{
			/// <summary>
			/// Word that was misspelled
			/// </summary>
			public string Word;

			/// <summary>
			/// Case this word is found in
			/// </summary>
			public Case Case;

			/// <summary>
			/// Dialogue line this word came from
			/// </summary>
			public DialogueLine Line;

			/// <summary>
			/// Index within the line's text that this word appears
			/// </summary>
			public int Index;

			public override string ToString()
			{
				return Word;
			}

			public void Replace(string replacement)
			{
				string text = Line.Text;
				string newText = text.Substring(0, Index) + replacement + text.Substring(Index + Word.Length);
				Line.Text = newText;
			}
		}
	}
}
