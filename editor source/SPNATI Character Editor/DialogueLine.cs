using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.ComponentModel;
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

		/// <summary>
		/// Stage-specific images
		/// </summary>
		[XmlElement("alt-img")]
		public List<StageImage> Images { get; set; }

		/// <summary>
		/// Text will be read from the XmlText but written to the &lt;text&gt; element
		/// </summary>
		[XmlText]
		public string LegacyText
		{
			get { return null; } //XmlSerializer won't read this without a getter, so use a dummy to prevent writing it back out 
			set { Text = value; }
		}

		private string _text;
		[XmlConditionalText("UseXmlText")]
		[XmlElement("text")]
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

		private List<MarkerOperation> _markers;
		[XmlArray("markers")]
		[XmlArrayItem("marker")]
		public List<MarkerOperation> Markers
		{
			get { return _markers; }
			set { if (_markers != value) { _markers = value; NotifyPropertyChanged(); } }
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

		private string _layer;
		[DefaultValue("")]
		[XmlAttribute("dialogue-layer")]
		public string Layer
		{
			get { return _layer; }
			set { if (_layer != value) { _layer = value; NotifyPropertyChanged(); } }
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
			set { if (_collectibleValue != value) { _collectibleValue = value; NotifyPropertyChanged(); } }
		}

		private bool _persistent;
		[DefaultValue(false)]
		[XmlAttribute("persist-marker")]
		public bool IsMarkerPersistent
		{
			get { return _persistent; }
			set { if (_persistent != value) { _persistent = value; NotifyPropertyChanged(); } }
		}

		public static readonly string[] ArrowDirections = new string[] { "", "down", "left", "right", "up", "none" };
		public static readonly string[] AILevels = new string[] { "", "throw", "bad", "average", "good", "best" };

		public DialogueLine()
		{
			Image = "";
			Text = "";
			Direction = "down";
			Weight = 1;
			Marker = null;
			Images = new List<StageImage>();
			Markers = new List<MarkerOperation>();
		}

		public DialogueLine(string image, string text) : this()
		{
			Image = image;
			Text = text ?? "";
		}

		public DialogueLine Copy()
		{
			DialogueLine copy = new DialogueLine();
			//previously this used MemberwiseClone, but that clones registered event handlers too which is bad
			copy._image = this._image;
			copy._pose = this._pose;
			copy.LegacyText = this.LegacyText;
			copy._text = this._text;
			copy._oneShotId = this._oneShotId;
			copy._marker = this._marker;
			copy._direction = this._direction;
			copy._location = this._location;
			copy._gender = this._gender;
			copy._intelligence = this._intelligence;
			copy._layer = this._layer;
			copy._size = this._size;
			copy._label = this._label;
			copy._weight = this._weight;
			copy.IsGenericImage = this.IsGenericImage;
			copy._collectibleId = this._collectibleId;
			copy._collectibleValue = this._collectibleValue;
			copy._persistent = this._persistent;

			copy.Images = new List<StageImage>();
			foreach (StageImage img in Images)
			{
				copy.Images.Add(img.Copy());
			}
			copy.Markers = new List<MarkerOperation>();
			foreach (MarkerOperation marker in Markers)
			{
				copy.Markers.Add(marker.Copy());
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
			hash = (hash * 397) ^ (Layer ?? string.Empty).GetHashCode();
			foreach (MarkerOperation op in Markers)
			{
				hash = (hash * 397) ^ op.GetHashCode();
			}
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

		public bool HasAdvancedMarker
		{
			get
			{
				return IsMarkerPersistent || (Marker != null && (Marker.Contains("=") || Marker.Contains("+") || Marker.Contains("-") || Marker.Contains("*"))) || Markers.Count > 0;
			}
		}

		public bool HasAdvancedData
		{
			get
			{
				return !string.IsNullOrEmpty(Gender) || !string.IsNullOrEmpty(Size) || Intelligence != null || (!string.IsNullOrEmpty(Direction) && Direction != "down") ||
					Label != null || !string.IsNullOrEmpty(Location) || !string.IsNullOrEmpty(Layer) || Weight != 1;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private bool UseXmlText()
		{
			return Images.Count == 0 && Markers.Count == 0;
		}
	}
}