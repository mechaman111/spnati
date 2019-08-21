using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.ComponentModel;
using Desktop.DataStructures;
using Desktop;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// A single line of dialogue and its pose
	/// </summary>
	public class DialogueLine : BindableObject
	{
		private string _image;
		[XmlAttribute("img")]
		public string Image
		{
			get { return _image; }
			set { if (_image != value) { _image = value; NotifyPropertyChanged(); } }
		}

		[XmlIgnore]
		public ObservableDictionary<int, LineImage> StageImages
		{
			get { return Get<ObservableDictionary<int, LineImage>>(); }
			set { Set(value); }
		}

		[XmlText]
		public string Text
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Line will only play once
		/// </summary>
		[DefaultValue(0)]
		[XmlAttribute("oneShotId")]
		public int OneShotId
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[XmlAttribute("marker")]
		public string Marker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("down")]
		[XmlAttribute("direction")]
		public string Direction
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("location")]
		public string Location
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("set-gender")]
		public string Gender
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("set-intelligence")]
		public string Intelligence
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("set-size")]
		public string Size
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("set-label")]
		public string Label
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue(1.0f)]
		[XmlAttribute("weight")]
		public float Weight
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		[XmlIgnore]
		public string ImageExtension;

		[XmlIgnore]
		public bool IsGenericImage;

		[DefaultValue("")]
		[XmlAttribute("collectible")]
		public string CollectibleId
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("collectible-value")]
		public string CollectibleValue
		{
			get { return Get<string>(); }
			set	{ Set(value); }
		}

		[DefaultValue(false)]
		[XmlAttribute("persist-marker")]
		public bool IsMarkerPersistent
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public static readonly string[] ArrowDirections = new string[] { "", "down", "left", "right", "up" };
		public static readonly string[] AILevels = new string[] { "", "throw", "bad", "average", "good", "best" };

		public DialogueLine()
		{
			Image = "";
			Text = "";
			Direction = "down";
			Weight = 1;
			Marker = null;
			StageImages = new ObservableDictionary<int, LineImage>();
		}

		public DialogueLine(string image, string text) : this()
		{
			Image = image;
			string extension = Path.GetExtension(image);
			ImageExtension = extension;
			Text = text ?? "";
		}

		public DialogueLine Copy()
		{
			DialogueLine copy = new DialogueLine();
			CopyPropertiesInto(copy);
			copy._image = _image;
			copy.StageImages = new ObservableDictionary<int, LineImage>();
			foreach (KeyValuePair<int, LineImage> kvp in StageImages)
			{
				copy.StageImages[kvp.Key] = new LineImage(kvp.Value.Image, kvp.Value.IsGenericImage);
			}
			return copy;
		}

		/// <summary>
		/// Gets a hash code not including the image
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public int GetHashCodeWithoutImage()
		{
			int hash = (Text ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Marker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Direction ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Location ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Gender ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Intelligence ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Size ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Label ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ Weight.GetHashCode();
			hash = (hash * 397) ^ (CollectibleId ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (CollectibleValue ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ IsMarkerPersistent.GetHashCode();
			hash = (hash * 397) ^ (OneShotId > 0 ? OneShotId : -1);
			return hash;
		}

		public override int GetHashCode()
		{
			int hash = GetHashCodeWithoutImage();
			hash = (hash * 397) ^ (Image ?? string.Empty).GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return Text;
		}

		private static Regex _stageRegex = new Regex(@"^(custom:)?\d+-.*");
		/// <summary>
		/// Gets whether an image name is in the format [custom:]#-abc
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static bool IsStageSpecificImage(string image)
		{
			if (string.IsNullOrEmpty(image)) { return true; }
			return _stageRegex.IsMatch(image);
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
				if (int.TryParse(prefix, out value) || prefix == "#")
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

		public static string GetPlaceholderImage(string name)
		{
			return GetStageImage("#", name);
		}

		public static string GetStageImage(int stage, string name)
		{
			return GetStageImage(stage.ToString(), name);
		}

		/// <summary>
		/// Converts a generic image name into a stage-specific one (ex. shy to 0-shy)
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetStageImage(string stage, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			if (name.StartsWith("custom:"))
			{
				if (name.StartsWith($"custom:{stage}-"))
				{
					return name;
				}
				return $"custom:{stage}-{name.Substring(7)}";
			}
			else
			{
				if (name.StartsWith($"{stage}-"))
				{
					return name;
				}
				return $"{stage}-{name}";
			}
		}

		/// <summary>
		/// Gets a list of variables in a block of text that are invalid
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static List<string> GetInvalidVariables(Case dialogueCase, string text)
		{
			string tag = dialogueCase.Tag;
			Regex varRegex = new Regex(@"~[^\s~]*~", RegexOptions.IgnoreCase);
			List<string> invalidVars = new List<string>();
			if (text == "~silent~")
			{
				return invalidVars;
			}
			MatchCollection matches = varRegex.Matches(text);
			foreach (var match in matches)
			{
				string variable = match.ToString().Trim('~');
				if (!TriggerDatabase.IsVariableAvailable(tag, variable) && variable != "~name~")
				{
					//check filters for variables
					TargetCondition filter = dialogueCase.Conditions.Find(c => c.Variable == variable);
					if (filter == null)
					{
						invalidVars.Add(variable);
					}
				}
			}
			return invalidVars;
		}

		/// <summary>
		/// Sets a dialogue line to use the generic version of a particular image
		/// </summary>
		public void GeneralizeImage(DialogueLine line)
		{
			string extension = line.ImageExtension ?? Path.GetExtension(line.Image);
			ImageExtension = extension;
			line.ImageExtension = extension;
			Image = GetDefaultImage(line.Image);
			IsGenericImage = line.IsGenericImage;
		}

		public bool HasAdvancedMarker
		{
			get
			{
				return IsMarkerPersistent || (Marker != null && (Marker.Contains("=") || Marker.Contains("+") || Marker.Contains("-") || Marker.Contains("*")));
			}
		}
	}

	public class LineImage
	{
		public string Image;
		public bool IsGenericImage;

		public LineImage(string img, bool generic)
		{
			Image = img;
			IsGenericImage = generic;
		}

		public override string ToString()
		{
			return Image;
		}
	}
}