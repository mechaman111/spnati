using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Helper functions for setting and reading from input controls
	/// </summary>
	public static class GUIHelper
	{
		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(ComboBox minBox, ComboBox maxBox, string value)
		{
			if (value == null)
			{
				minBox.Text = "";
				maxBox.Text = "";
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			if (string.IsNullOrEmpty(min))
			{
				minBox.Text = "";
			}
			else
			{
				minBox.Text = min;
			}
			if (string.IsNullOrEmpty(max))
			{
				maxBox.Text = "";
			}
			else
			{
				maxBox.Text = max;
			}
		}

		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(NumericUpDown minBox, NumericUpDown maxBox, string value)
		{
			if (value == null)
			{
				SetNumericBox(minBox, null);
				SetNumericBox(maxBox, null);
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			SetNumericBox(minBox, min);
			SetNumericBox(maxBox, max);
		}

		public static string ReadRange(ComboBox minBox, ComboBox maxBox)
		{
			string min = minBox.Text;
			if (string.IsNullOrEmpty(min))
				return null;
			string max = maxBox.Text;
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		public static string ReadRange(NumericUpDown minBox, NumericUpDown maxBox)
		{
			string min = ReadNumericBox(minBox);
			if (string.IsNullOrEmpty(min))
				return null;
			string max = ReadNumericBox(maxBox);
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		public static void SetNumericBox(NumericUpDown box, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				box.Text = "";
			}
			else
			{
				int v;
				if (int.TryParse(value, out v) && v >= box.Minimum && v <= box.Maximum)
				{
					box.Value = v;
					box.Text = v.ToString();
				}
			}
		}

		public static string ReadNumericBox(NumericUpDown box)
		{
			if (string.IsNullOrEmpty(box.Text))
				return null;
			return box.Value.ToString();
		}

		/// <summary>
		/// Converts a range string to a display-friendly format
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static string RangeToString(string range)
		{
			if (range == null)
			{
				return "";
			}
			string[] pieces = range.Split('-');
			if (pieces.Length == 1 || pieces[0] == pieces[1])
			{
				return pieces[0];
			}
			if (pieces.Length == 2 && string.IsNullOrEmpty(pieces[0]))
			{
				return $"0-{pieces[1]}";
			}
			if (pieces.Length == 2 && string.IsNullOrEmpty(pieces[1]))
			{
				return $"{pieces[0]}+";
			}
			return range;
		}

		/// <summary>
		/// Converts a min and max to a range string
		/// </summary>
		/// <param name="min">Min bound, or -1 if no min bound</param>
		/// <param name="max">Max bound, or -1 if no max bound</param>
		/// <returns></returns>
		public static string ToRange(int min, int max)
		{
			if (min == -1 && max == -1)
			{
				return null;
			}
			else if (min == -1)
			{
				//open-ended upper bound
				return $"-{max}";
			}
			else if (max == -1)
			{
				//open-ended lower bound
				return $"{min}-";
			}
			else if (max < min)
			{
				return $"{min}-{min}";
			}
			else if (max == min)
			{
				return $"{min}";
			}
			else
			{
				return $"{min}-{max}";
			}
		}

		/// <summary>
		/// Converts a range string to an interval tuple
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static Tuple<int, int> ToInterval(string range)
		{
			int min;
			int max;
			string[] pieces = range.Split('-');
			if (!int.TryParse(pieces[0], out min))
			{
				min = 0;
			}
			if (pieces.Length > 1)
			{
				if (!int.TryParse(pieces[1], out max))
				{
					max = min;
				}
			}
			else
			{
				max = min;
			}
			max = Math.Max(min, max);
			return new Tuple<int, int>(min, max);
		}

		public static Tuple<int, int> ToOpenInterval(string range)
		{
			int min;
			int max;
			string[] pieces = range.Split('-');
			if (!int.TryParse(pieces[0], out min))
			{
				min = 0;
			}
			if (pieces.Length > 1)
			{
				if (!int.TryParse(pieces[1], out max))
				{
					max = -1;
				}
			}
			else
			{
				max = -1;
			}
			return new Tuple<int, int>(min, max);
		}

		public static string ListToString(List<int> list)
		{
			string result;
			if (list.Count == 0)
			{
				result = "";
			}
			else
			{
				list.Sort();
				StringBuilder sb = new StringBuilder();
				int last = list[0];
				int startRange = last;
				for (int i = 1; i < list.Count; i++)
				{
					int stage = list[i];
					if (stage - 1 > last)
					{
						if (startRange == last)
						{
							sb.Append(startRange.ToString() + " ");
						}
						else
						{
							sb.Append(string.Format("{0}-{1} ", startRange, last));
						}
						startRange = stage;
					}
					last = stage;
				}
				if (startRange == last)
				{
					sb.Append(startRange.ToString());
				}
				else
				{
					sb.Append(string.Format("{0}-{1}", startRange, last));
				}
				result = sb.ToString();
			}
			return result;
		}

		public static List<int> StringToList(string input)
		{
			List<int> list = new List<int>();
			string[] ranges = input.Split(' ');
			foreach (string range in ranges)
			{
				string[] bounds = range.Split('-');
				int min;
				if (int.TryParse(bounds[0], out min))
				{
					int max;
					if (bounds.Length < 2 || !int.TryParse(bounds[1], out max))
					{
						max = min;
					}
					for (int i = min; i <= max; i++)
					{
						list.Add(i);
					}
				}
			}
			list.Sort();
			return list;
		}

		public static List<Word> ParseWords(string sentence)
		{
			Tuple<string, FormatMarker>[] markers = new Tuple<string, FormatMarker>[] {
				new Tuple<string, FormatMarker>("<i>", FormatMarker.ItalicOn),
				new Tuple<string, FormatMarker>("</i>", FormatMarker.ItalicOff),
				new Tuple<string, FormatMarker>("<br>", FormatMarker.LineBreak),
				new Tuple<string, FormatMarker>("<br/>", FormatMarker.LineBreak),
			};
			List<Word> words = new List<Word>();
			string[] rawWords = sentence.Split(' ');
			for (int i = 0; i < rawWords.Length; i++)
			{
				string word = rawWords[i];
				ParseForMarker(word, 0, markers, words);
			}
			return words;
		}

		private static void ParseForMarker(string text, int markerIndex, Tuple<string, FormatMarker>[] markers, List<Word> words)
		{
			string tag = markers[markerIndex].Item1;
			FormatMarker marker = markers[markerIndex].Item2;
			string[] segments = text.Split(new string[] { tag }, StringSplitOptions.None);
			Regex regex = new Regex(@"^[!?,\.]+$");
			for (int i = 0; i < segments.Length; i++)
			{
				string segment = segments[i];
				if (i > 0)
				{
					words.Add(new Word(marker));
				}
				if (markerIndex < markers.Length - 1)
				{
					ParseForMarker(segment, markerIndex + 1, markers, words);
				}
				else if(!string.IsNullOrEmpty(segment))
				{
					//if punctuation only, add it to the previous word
					if (words.Count > 0 && regex.IsMatch(segment))
					{
						bool found = false;
						for (int j = words.Count - 1; j >= 0; j--)
						{
							if (words[j].Formatter == FormatMarker.None)
							{
								words[j].Text += segment;
								found = true;
								break;
							}
						}
						if (found)
						{
							continue;
						}
					}
					
					words.Add(new Word(segment));
				}
			}
		}
	}

	public class Word
	{
		public string Text;
		public float Width;
		public FormatMarker Formatter;

		public Word(string text)
		{
			Text = text;
		}

		public Word(FormatMarker formatter)
		{
			Formatter = formatter;
		}

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(Text))
			{
				return Text;
			}
			return Formatter.ToString();
		}
	}

	public enum FormatMarker
	{
		None = 0,
		ItalicOn = 1,
		ItalicOff = 2,
		LineBreak = 3,
	}
}
