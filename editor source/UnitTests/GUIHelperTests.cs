using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using System.Collections.Generic;

namespace UnitTests
{
	[TestClass]
	public class GUIHelperTests
	{
		[TestMethod]
		public void EmptyList()
		{
			List<int> list = new List<int>();
			string output = GUIHelper.ListToString(list);
			Assert.AreEqual("", output);
		}

		[TestMethod]
		public void SingleStage()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				5
			});
			Assert.AreEqual("5", output);
		}

		[TestMethod]
		public void SingleRange()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				5,
				6,
				7
			});
			Assert.AreEqual("5-7", output);
		}

		[TestMethod]
		public void MultipleRange()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				1,
				2,
				4,
				5,
				6
			});
			Assert.AreEqual("1-2 4-6", output);
		}

		[TestMethod]
		public void RangeStage()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				1,
				2,
				3,
				6
			});
			Assert.AreEqual("1-3 6", output);
		}

		[TestMethod]
		public void StageRange()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				1,
				4,
				5,
				6
			});
			Assert.AreEqual("1 4-6", output);
		}

		[TestMethod]
		public void SortedRange()
		{
			string output = GUIHelper.ListToString(new List<int>
			{
				6,
				5,
				2,
				7
			});
			Assert.AreEqual("2 5-7", output);
		}

		[TestMethod]
		public void StageRange_ToList()
		{
			List<int> list = GUIHelper.StringToList("1 4-6");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(4, list[1]);
			Assert.AreEqual(5, list[2]);
			Assert.AreEqual(6, list[3]);
		}

		[TestMethod]
		public void RangeStage_ToList()
		{
			List<int> list = GUIHelper.StringToList("1-3 6");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(3, list[2]);
			Assert.AreEqual(6, list[3]);
		}

		[TestMethod]
		public void MultipleRange_ToList()
		{
			List<int> list = GUIHelper.StringToList("1-2 4-6");
			Assert.AreEqual(5, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(4, list[2]);
			Assert.AreEqual(5, list[3]);
			Assert.AreEqual(6, list[4]);
		}

		[TestMethod]
		public void SingleRange_ToList()
		{
			List<int> list = GUIHelper.StringToList("5-7");
			Assert.AreEqual(3, list.Count);
			Assert.AreEqual(5, list[0]);
			Assert.AreEqual(6, list[1]);
			Assert.AreEqual(7, list[2]);
		}

		[TestMethod]
		public void SingleStage_ToList()
		{
			List<int> list = GUIHelper.StringToList("5");
			Assert.AreEqual(1, list.Count);
			Assert.AreEqual(5, list[0]);
		}

		[TestMethod]
		public void Empty_ToList()
		{
			List<int> list = GUIHelper.StringToList("");
			Assert.AreEqual(0, list.Count);
		}

		[TestMethod]
		public void UnsortedRange_ToList()
		{
			List<int> list = GUIHelper.StringToList("4-6 2");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(2, list[0]);
			Assert.AreEqual(4, list[1]);
			Assert.AreEqual(5, list[2]);
			Assert.AreEqual(6, list[3]);
		}

		[TestMethod]
		public void ParseSentence_Break_Spaced()
		{
			string sentence = "A <br> break.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(3, words.Count);
			Assert.AreEqual("A", words[0].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[1].Formatter);
			Assert.AreEqual("break.", words[2].Text);
		}

		[TestMethod]
		public void ParseSentence_Punctuation()
		{
			string sentence = "...";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(1, words.Count);
			Assert.AreEqual("...", words[0].Text);
		}

		[TestMethod]
		public void ParseSentence_Punctuation_Italics()
		{
			string sentence = "<i>...</i>";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(3, words.Count);
			Assert.AreEqual(FormatMarker.ItalicOn, words[0].Formatter);
			Assert.AreEqual("...", words[1].Text);
			Assert.AreEqual(FormatMarker.ItalicOff, words[2].Formatter);
		}


		[TestMethod]
		public void ParseSentence_Break_Single()
		{
			string sentence = "A<br>break.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(3, words.Count);
			Assert.AreEqual("A", words[0].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[1].Formatter);
			Assert.AreEqual("break.", words[2].Text);
		}

		[TestMethod]
		public void ParseSentence_Break_SelfClosing()
		{
			string sentence = "A <br/> break.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(3, words.Count);
			Assert.AreEqual("A", words[0].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[1].Formatter);
			Assert.AreEqual("break.", words[2].Text);
		}

		[TestMethod]
		public void ParseSentence_Break_Multiple()
		{
			string sentence = "A<br>few<br>breaks.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(5, words.Count);
			Assert.AreEqual("A", words[0].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[1].Formatter);
			Assert.AreEqual("few", words[2].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[3].Formatter);
			Assert.AreEqual("breaks.", words[4].Text);
		}

		[TestMethod]
		public void ParseSentence_Break_Punctuation()
		{
			string sentence = "Hey<i>, some punctuation .";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(4, words.Count);
			int i = 0;
			Assert.AreEqual("Hey,", words[i++].Text);
			Assert.AreEqual(FormatMarker.ItalicOn, words[i++].Formatter);
			Assert.AreEqual("some", words[i++].Text);
			Assert.AreEqual("punctuation.", words[i++].Text);			
		}

		[TestMethod]
		public void ParseSentence_Italic_Spaced()
		{
			string sentence = "Now <i> italics </i> are on.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(6, words.Count);
			Assert.AreEqual("Now", words[0].Text);
			Assert.AreEqual(FormatMarker.ItalicOn, words[1].Formatter);
			Assert.AreEqual("italics", words[2].Text);
			Assert.AreEqual(FormatMarker.ItalicOff, words[3].Formatter);
			Assert.AreEqual("are", words[4].Text);
			Assert.AreEqual("on.", words[5].Text);
		}

		[TestMethod]
		public void ParseSentence_Italic_Joined()
		{
			string sentence = "Now <i>italics</i> are on.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(6, words.Count);
			Assert.AreEqual("Now", words[0].Text);
			Assert.AreEqual(FormatMarker.ItalicOn, words[1].Formatter);
			Assert.AreEqual("italics", words[2].Text);
			Assert.AreEqual(FormatMarker.ItalicOff, words[3].Formatter);
			Assert.AreEqual("are", words[4].Text);
			Assert.AreEqual("on.", words[5].Text);
		}

		[TestMethod]
		public void ParseSentence_Italic_Reverse()
		{
			string sentence = "Now </i>italics <i>are on.";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(6, words.Count);
			Assert.AreEqual("Now", words[0].Text);
			Assert.AreEqual(FormatMarker.ItalicOff, words[1].Formatter);
			Assert.AreEqual("italics", words[2].Text);
			Assert.AreEqual(FormatMarker.ItalicOn, words[3].Formatter);
			Assert.AreEqual("are", words[4].Text);
			Assert.AreEqual("on.", words[5].Text);
		}

		[TestMethod]
		public void ParseSentence_Multiple()
		{
			string sentence = "<i>This sentence<br></i> has it <br/><i>all!</i>";
			List<Word> words = GUIHelper.ParseWords(sentence);
			Assert.AreEqual(11, words.Count);
			int i = 0;
			Assert.AreEqual(FormatMarker.ItalicOn, words[i++].Formatter);
			Assert.AreEqual("This", words[i++].Text);
			Assert.AreEqual("sentence", words[i++].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[i++].Formatter);
			Assert.AreEqual(FormatMarker.ItalicOff, words[i++].Formatter);
			Assert.AreEqual("has", words[i++].Text);
			Assert.AreEqual("it", words[i++].Text);
			Assert.AreEqual(FormatMarker.LineBreak, words[i++].Formatter);
			Assert.AreEqual(FormatMarker.ItalicOn, words[i++].Formatter);
			Assert.AreEqual("all!", words[i++].Text);
			Assert.AreEqual(FormatMarker.ItalicOff, words[i++].Formatter);
		}
	}
}
