using Desktop;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(DictionaryRecord), 0)]
	public partial class DictionaryEditor : Activity
	{
		public DictionaryEditor()
		{
			InitializeComponent();
		}

		protected override void OnFirstActivate()
		{
			List<string> words = new List<string>();
			words.AddRange(SpellChecker.Instance.GetWords());
			words.Sort();
			foreach (string word in words)
			{
				lstWords.AddItem(word);
			}
		}

		private void lstWords_ItemAdded(object sender, object item)
		{
			string word = item?.ToString();
			if (!string.IsNullOrEmpty(word))
			{
				SpellChecker.Instance.AddWord(word, true);
			}
		}

		private void lstWords_ItemRemoved(object sender, object item)
		{
			string word = item?.ToString();
			if (!string.IsNullOrEmpty(word))
			{
				SpellChecker.Instance.RemoveWord(word);
			}
		}
	}

	public class DictionaryRecord : BasicRecord
	{
		public DictionaryRecord()
		{
			Name = "Dictionary";
		}
	}

	public class DictionaryProvider : BasicProvider<DictionaryRecord>
	{
	}
}
