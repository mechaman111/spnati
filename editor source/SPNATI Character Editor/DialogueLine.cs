using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.ComponentModel;

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

		[XmlAttribute("marker")]
		public string Marker;

		[DefaultValue("down")]
		[XmlAttribute("direction")]
		public string Direction;

		[DefaultValue("")]
		[XmlAttribute("location")]
		public string Location;

		[DefaultValue("")]
		[XmlAttribute("set-gender")]
		public string Gender;

		[DefaultValue("")]
		[XmlAttribute("set-intelligence")]
		public string Intelligence;

		[DefaultValue("")]
		[XmlAttribute("set-size")]
		public string Size;

		[DefaultValue("")]
		[XmlAttribute("set-label")]
		public string Label;

		[XmlIgnore]
		public string ImageExtension;

		public DialogueLine()
		{
			Image = "";
			Text = "";
			Direction = "down";
			Marker = null;
		}

		public DialogueLine(string image, string text)
		{
			Image = image;
			string extension = Path.GetExtension(image);
			ImageExtension = extension;
			Text = text ?? "";
		}

		public DialogueLine Copy()
		{
			DialogueLine copy = MemberwiseClone() as DialogueLine;
			return copy;
		}

		public override int GetHashCode()
		{
			int hash = (Image ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Text ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Marker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Direction ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Location ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Gender ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Intelligence ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Size ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Label ?? string.Empty).GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return Text;
		}

		/// <summary>
		/// Converts an image name (ex. 0-shy.png) to a generic name (shy)
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static string GetDefaultImage(string image)
		{
			if (string.IsNullOrEmpty(image))
				return image;
			bool custom = image.StartsWith("custom:");
			if (custom)
			{
				image = image.Substring(7);
			}
			int hyphen = image.IndexOf('-');
			if (hyphen > 0)
			{
				string prefix = image.Substring(0, hyphen);
				int value;
				if (int.TryParse(prefix, out value))
				{
					string reduced = Path.GetFileNameWithoutExtension(image.Substring(hyphen + 1));
					if (custom)
					{
						reduced = "custom:" + reduced;
					}
					return reduced;
				}
			}
			string path = Path.GetFileNameWithoutExtension(image);
			if (custom)
			{
				path = "custom:" + path;
			}
			return path;
		}

		/// <summary>
		/// Converts a generic image name into a stage-specific one (ex. shy to 0-shy)
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetStageImage(int stage, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			if (name.StartsWith("custom:"))
			{
				return $"custom:{stage}-{name.Substring(7)}";
			}
			else
			{
				return $"{stage}-{name}";
			}
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
			if (text == "~silent~")
			{
				return invalidVars;
			}
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