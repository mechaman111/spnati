using System;
using System.Collections.Generic;
using System.Linq;

namespace SPNATI_Character_Editor.Services
{
	public class SpellCheckerService : IDisposable
	{
		private bool _activated;
		private Character _character;

		private ISpellChecker _spellchecker;
		private List<Case> _unprocessedCases = new List<Case>();
		private Queue<Misspelling> _misspellings = new Queue<Misspelling>();

		private const int MaxSuggestions = 5;


		public SpellCheckerService(Character character)
		{
			_character = character;
		}

		public void Activate()
		{
			if (_activated) { return; }

			_spellchecker = SpellChecker.Instance;

			_character.Behavior.CaseAdded += Behavior_CaseAdded;
			_character.Behavior.CaseRemoved += Behavior_CaseRemoved;
			_character.Behavior.CaseModified += Behavior_CaseModified;
			_unprocessedCases.AddRange(_character.Behavior.GetWorkingCases());
			_activated = true;
		}

		public void Dispose()
		{
			if (_activated)
			{
				_character.Behavior.CaseAdded -= Behavior_CaseAdded;
				_character.Behavior.CaseRemoved -= Behavior_CaseRemoved;
				_character.Behavior.CaseModified -= Behavior_CaseModified;
			}
		}

		/// <summary>
		/// Runs the spellcheck on any unprocessed cases
		/// </summary>
		public void Run()
		{
			if (!_activated)
			{
				Activate();
			}
			foreach (Case workingCase in _unprocessedCases)
			{
				ProcessCase(workingCase);
			}
			_unprocessedCases.Clear();
		}

		public int RemainingMisspellings
		{
			get
			{
				return _misspellings.Count;
			}
		}

		public Misspelling GetNextMispelling()
		{
			return _misspellings.Peek();
		}

		public void Advance()
		{
			_misspellings.Dequeue();
		}

		public void IgnoreWord(string word)
		{
			_spellchecker.IgnoreWord(word);
			_misspellings.Dequeue();
			FilterQueue(m => m.Word != word);
		}

		public void AddWord(string word)
		{
			_spellchecker.AddWord(word, true);
			FilterQueue(m => m.Word != word);
		}

		public List<string> GetSuggestions(string word)
		{
			return _spellchecker.GetSuggestions(word, MaxSuggestions);
		}

		public void Replace(string word, string replacement)
		{
			FilterQueue(m =>
			{
				if (m.Word == word)
				{
					m.Replace(replacement);
				}
				return m.Word != word;
			});
		}

		private void Behavior_CaseRemoved(object sender, Case theCase)
		{
			if (_unprocessedCases.Contains(theCase))
			{
				return;
			}
			_unprocessedCases.Remove(theCase);
			FilterQueue(c => c.Case != theCase);
		}

		private void Behavior_CaseAdded(object sender, Case theCase)
		{
			if (_unprocessedCases.Contains(theCase))
			{
				return;
			}
			_unprocessedCases.Add(theCase);
		}

		private void Behavior_CaseModified(object sender, Case theCase)
		{
			if (_unprocessedCases.Contains(theCase))
			{
				return;
			}
			FilterQueue(c => c.Case != theCase);
			_unprocessedCases.Add(theCase);
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
	}

	public class Misspelling
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
