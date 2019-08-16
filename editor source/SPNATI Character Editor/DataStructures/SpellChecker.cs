using NHunspell;
using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Provides access to spell checking, user dictionaries, etc.
	/// </summary>
	public class SpellChecker : ISpellChecker
	{
		private static ISpellChecker _instance;
		public static ISpellChecker Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new SpellChecker();
				}
				return _instance;
			}
		}

		private Hunspell _spellchecker;
		private HashSet<string> _ignoredWords = new HashSet<string>();
		private HashSet<string> _userWords = new HashSet<string>();
		private bool _dirty;

		private SpellChecker()
		{
			string path = Config.ExecutableDirectory;
			_spellchecker = new Hunspell(Path.Combine(path, "Resources/en_US.aff"), Path.Combine(path, "Resources/en_US.dic"));
			LoadDictionary(Path.Combine(path, "Resources/words.txt"), false);
			LoadDictionary(GetUserDictionaryPath(), true);
		}

		private string GetUserDictionaryPath()
		{
			return Path.Combine(Config.AppDataDirectory, "user_dict.txt");
		}

		private void LoadDictionary(string path, bool isUserDictionary)
		{
			if (File.Exists(path))
			{
				foreach (string word in File.ReadAllLines(path))
				{
					_spellchecker.Add(word);
					if (isUserDictionary)
					{
						_userWords.Add(word);
					}
				}
			}
		}

		public bool CheckWord(string word)
		{
			if (_ignoredWords.Contains(word))
			{
				return true;
			}
			return _spellchecker.Spell(word);
		}

		public List<string> GetSuggestions(string word, int max)
		{
			List<string> suggestions = new List<string>();
			List<string> results = _spellchecker.Suggest(word);
			for (int i = 0; i < max && i < results.Count; i++)
			{
				suggestions.Add(results[i]);
			}
			return suggestions;
		}

		public void IgnoreWord(string word)
		{
			_ignoredWords.Add(word);
		}

		public void AddWord(string word, bool addToUserDictionary)
		{
			if (_userWords.Contains(word))
			{
				return;
			}
			_dirty = true;
			if (addToUserDictionary)
			{
				_userWords.Add(word);
			}
			_spellchecker.Add(word);
		}

		public void RemoveWord(string word)
		{
			if (_userWords.Contains(word))
			{
				_dirty = true;
				_userWords.Remove(word);
				_spellchecker.Remove(word);
			}
		}

		public IEnumerable<string> GetWords()
		{
			foreach (string word in _userWords)
			{
				yield return word;
			}
		}

		public void SaveUserDictionary()
		{
			if (!_dirty) { return; }
			try
			{
				File.WriteAllLines(GetUserDictionaryPath(), _userWords);
				_dirty = false;
			}
			catch { }
		}
	}

	public interface ISpellChecker
	{
		/// <summary>
		/// Checks if a word is spelled correctly
		/// </summary>
		/// <param name="word">Word to check</param>
		/// <returns>True if correct</returns>
		bool CheckWord(string word);
		/// <summary>
		/// Gets a list of suggested words for a misspelling
		/// </summary>
		/// <param name="word">Misspelled word</param>
		/// <param name="max">Maximium suggestions</param>
		/// <returns></returns>
		List<string> GetSuggestions(string word, int max);
		/// <summary>
		/// Adds a word to the ignore list for this session
		/// </summary>
		/// <param name="word">Word to ignore</param>
		void IgnoreWord(string word);
		/// <summary>
		/// Adds a word to the list of properly spelled words
		/// </summary>
		/// <param name="word">Word to add</param>
		/// <param name="addToUser">True to add to the user dictionary. False to just add to the session.</param>
		void AddWord(string word, bool addToUser);
		/// <summary>
		/// Enumerates through user dictionary words
		/// </summary>
		/// <returns></returns>
		IEnumerable<string> GetWords();
		/// <summary>
		/// Removes a word from the user dictionary
		/// </summary>
		/// <param name="word"></param>
		void RemoveWord(string word);
		/// <summary>
		/// Saves the user dictionary to disk
		/// </summary>
		void SaveUserDictionary();
	}
}
