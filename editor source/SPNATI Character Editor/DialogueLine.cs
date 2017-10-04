using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// A single ine of dialogue and its pose
	/// </summary>
	public class DialogueLine
	{
		[XmlAttribute("img")]
		public string Image;

		[XmlText]
		public string Text;

		/// <summary>
		/// Whether the dialogue should be suppressed entirely. Should be null or ""
		/// </summary>
		[XmlAttribute("silent")]
		public string IsSilent;

		[XmlIgnore]
		public string ImageExtension;

		public DialogueLine()
		{
			Image = "";
			Text = "";
		}

		public DialogueLine(string image, string text)
		{
			Image = image;
			string extension = Path.GetExtension(image);
			ImageExtension = extension;
			Text = text;
		}

		public DialogueLine Copy()
		{
			DialogueLine copy = MemberwiseClone() as DialogueLine;
			return copy;
		}

		public override int GetHashCode()
		{
			int hash = (Image ?? string.Empty).GetHashCode() * 397 + (Text ?? string.Empty).GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return Text;
		}

		public static string GetDefaultImage(string image)
		{
			if (string.IsNullOrEmpty(image))
				return image;
			int hyphen = image.IndexOf('-');
			if (hyphen > 0)
			{
				string prefix = image.Substring(0, hyphen);
				int value;
				if (int.TryParse(prefix, out value))
				{
					return Path.GetFileNameWithoutExtension(image.Substring(hyphen + 1));
				}
			}
			return Path.GetFileNameWithoutExtension(image);
		}

		/// <summary>
		/// Gets a list of variables in a block of text that are invalid
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static List<string> GetInvalidVariables(string tag, string text)
		{
			Regex varRegex = new Regex(@"~\w*~", RegexOptions.IgnoreCase);
			List<string> invalidVars = new List<string>();
			MatchCollection matches = varRegex.Matches(text);
			foreach (var match in matches)
			{
				string variable = match.ToString().Trim('~');
				if (!TriggerDatabase.IsVariableAvailable(tag, variable))
				{
					invalidVars.Add(variable);
				}
			}
			return invalidVars;
		}
	}
}