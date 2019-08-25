using Desktop;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// A single line of dialogue and its pose
	/// </summary>
	/// <remarks>
	/// Overhead of using a BindableObject is too big for this since it's used thousands of times.
	/// </remarks>
	public class DialogueLine : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private static Regex _stageRegex = new Regex(@"^(custom:)?\d+-.*");

		public void NotifyPropertyChanged([CallerMemberName] string propName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>
		/// Image as it's serialized
		/// </summary>
		private string _image;
		[XmlAttribute("img")]
		public string Image
		{
			get { return _image; }
			set { if (_image != value) { _image = value; NotifyPropertyChanged(); } }
		}

		private PoseMapping _pose;
		/// <summary>
		/// Working pose image
		/// </summary>
		[XmlIgnore]
		public PoseMapping Pose
		{
			get { return _pose; }
			set { if (_pose != value) { _pose = value; NotifyPropertyChanged(); } }
		}

		[XmlIgnore]
		private ObservableDictionary<int, PoseMapping> _stageImages = new ObservableDictionary<int, PoseMapping>();
		public ObservableDictionary<int, PoseMapping> StageImages
		{
			get { return _stageImages; }
			set
			{
				if (_stageImages != value)
				{
					if (_stageImages != null)
					{
						_stageImages.CollectionChanged -= _stageImages_CollectionChanged;
					}
					_stageImages = value;
					if (_stageImages != null)
					{
						_stageImages.CollectionChanged += _stageImages_CollectionChanged;
					}
					NotifyPropertyChanged();
				}
			}
		}

		private void _stageImages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyPropertyChanged(nameof(StageImages));
		}

		private string _text;
		[XmlText]
		public string Text
		{
			get { return _text; }
			set { if (_text != value) { _text = value; NotifyPropertyChanged(); } }
		}

		private int _oneShotId;
		/// <summary>
		/// Line will only play once
		/// </summary>
		[DefaultValue(0)]
		[XmlAttribute("oneShotId")]
		public int OneShotId
		{
			get { return _oneShotId; }
			set { if (_oneShotId != value) { _oneShotId = value; NotifyPropertyChanged(); } }
		}

		private string _marker;
		[XmlAttribute("marker")]
		public string Marker
		{
			get { return _marker; }
			set { if (_marker != value) { _marker = value; NotifyPropertyChanged(); } }
		}

		private string _direction;
		[DefaultValue("down")]
		[XmlAttribute("direction")]
		public string Direction
		{
			get { return _direction; }
			set { if (_direction != value) { _direction = value; NotifyPropertyChanged(); } }
		}

		private string _location;
		[DefaultValue("")]
		[XmlAttribute("location")]
		public string Location
		{
			get { return _location; }
			set { if (_location != value) { _location = value; NotifyPropertyChanged(); } }
		}

		private string _gender;
		[DefaultValue("")]
		[XmlAttribute("set-gender")]
		public string Gender
		{
			get { return _gender; }
			set { if (_gender != value) { _gender = value; NotifyPropertyChanged(); } }
		}

		private string _intelligence;
		[DefaultValue("")]
		[XmlAttribute("set-intelligence")]
		public string Intelligence
		{
			get { return _intelligence; }
			set { if (_intelligence != value) { _intelligence = value; NotifyPropertyChanged(); } }
		}

		private string _size;
		[DefaultValue("")]
		[XmlAttribute("set-size")]
		public string Size
		{
			get { return _size; }
			set { if (_size != value) { _size = value; NotifyPropertyChanged(); } }
		}

		private string _label;
		[DefaultValue("")]
		[XmlAttribute("set-label")]
		public string Label
		{
			get { return _label; }
			set { if (_label != value) { _label = value; NotifyPropertyChanged(); } }
		}

		private float _weight = 1.0f;
		[DefaultValue(1.0f)]
		[XmlAttribute("weight")]
		public float Weight
		{
			get { return _weight; }
			set { if (_weight != value) { _weight = value; NotifyPropertyChanged(); } }
		}

		[XmlIgnore]
		public string ImageExtension;

		[XmlIgnore]
		public bool IsGenericImage;

		private string _collectibleId;
		[DefaultValue("")]
		[XmlAttribute("collectible")]
		public string CollectibleId
		{
			get { return _collectibleId; }
			set { if (_collectibleId != value) { _collectibleId = value; NotifyPropertyChanged(); } }
		}

		private string _collectibleValue;
		[DefaultValue("")]
		[XmlAttribute("collectible-value")]
		public string CollectibleValue
		{
			get { return _collectibleValue; }
			set	{ if (_collectibleValue != value) { _collectibleValue = value; NotifyPropertyChanged(); } }
		}

		private bool _persistent;
		[DefaultValue(false)]
		[XmlAttribute("persist-marker")]
		public bool IsMarkerPersistent
		{
			get { return _persistent; }
			set { if (_persistent != value) { _persistent = value; NotifyPropertyChanged(); } }
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
			DialogueLine copy = MemberwiseClone() as DialogueLine;
			copy.StageImages = new ObservableDictionary<int, PoseMapping>();
			foreach (KeyValuePair<int, PoseMapping> kvp in StageImages)
			{
				copy.StageImages[kvp.Key] = kvp.Value;
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
}