using Desktop.DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data represenation of meta.xml
	/// </summary>
	/// <remarks>
	/// PROPERTY ORDER IS IMPORTANT - Order determines attribute order in generated XML files
	/// </remarks>
	[XmlRoot("opponent")]
	public class Metadata : BindableObject, IHookSerialization
	{
		[XmlElement("enabled")]
		public bool Enabled
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[XmlElement("first")]
		public string FirstName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("last")]
		public string LastName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("label")]
		public string Label
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("pic")]
		public string Portrait
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("gender")]
		public string Gender
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("height")]
		public string Height
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("from")]
		public string Source
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("writer")]
		public string Writer
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("artist")]
		public string Artist
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("description")]
		public string Description
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue(false)]
		[XmlElement("crossGender")]
		public bool CrossGender
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[XmlElement("scale")]
		[DefaultValue(100.0f)]
		public float Scale
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		[XmlElement("epilogue")]
		public List<EpilogueMeta> Endings { get; set; }

		[XmlElement("layers")]
		public int Layers
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<CharacterTag> Tags { get; set; }

		[XmlElement("alternates")]
		public List<AlternateSkin> AlternateSkins { get; set; }

		[XmlElement("has_collectibles")]
		public bool HasCollectibles
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Count of unique text across all lines
		/// </summary>
		[XmlElement("lines")]
		public int Lines { get; set; }

		/// <summary>
		/// Count of unique poses used across all lines
		/// </summary>
		[XmlElement("poses")]
		public int Poses { get; set; }

		/// <summary>
		/// Custom z-ordering
		/// </summary>
		[DefaultValue(0)]
		[XmlElement("z-index")]
		public int Z
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Speech bubble position relative to image
		/// </summary>
		[DefaultValue(DialogueLayer.over)]
		[XmlElement("dialogue-layer")]
		public DialogueLayer BubblePosition
		{
			get { return Get<DialogueLayer>(); }
			set { Set(value); }
		}

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml { get; set; }

		public Metadata()
		{
			Scale = 100.0f;
			AlternateSkins = new List<AlternateSkin>();
		}

		public Metadata(Character c) : this()
		{
			PopulateFromCharacter(c);
		}

		/// <summary>
		/// Builds the meta data from a character instance
		/// </summary>
		/// <param name="c"></param>
		public void PopulateFromCharacter(Character c)
		{
			FirstName = c.FirstName;
			LastName = c.LastName;
			Label = c.Label;
			Gender = c.Gender;
			Layers = c.Layers;
			Endings = c.Endings.ConvertAll(e => new EpilogueMeta
			{
				Status = e.Status,
				Title = e.Title,
				Gender = e.Gender,
				GalleryImage = e.GalleryImage ?? (e.Scenes.Count > 0 ? e.Scenes[0].Background : null),
				AlsoPlaying = e.AlsoPlaying,
				PlayerStartingLayers = e.PlayerStartingLayers,
				Hint = e.Hint,
				HasMarkerConditions = !string.IsNullOrWhiteSpace(e.AllMarkers)
					|| !string.IsNullOrWhiteSpace(e.AnyMarkers)
					|| !string.IsNullOrWhiteSpace(e.NotMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingAllMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingAnyMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingNotMarkers)
			});
			Tags = c.Tags;
			HasCollectibles = c.Collectibles.Count > 0;
			int lines, poses;
			c.GetUniqueLineAndPoseCount(out lines, out poses);
			Lines = lines;
			Poses = poses;
		}

		public void OnBeforeSerialize()
		{

		}

		public void OnAfterDeserialize(string source)
		{
			//Encoding these doesn't need to be done in OnBeforeSerialize because the serializer does it automatically
			Description = XMLHelper.DecodeEntityReferences(Description);
		}
	}

	public class EpilogueMeta
	{
		[DefaultValue("")]
		[XmlAttribute("status")]
		public string Status;

		[XmlAttribute("gender")]
		public string Gender;

		[XmlAttribute("playerStartingLayers")]
		public string PlayerStartingLayers;

		[DefaultValue(false)]
		[XmlAttribute("markers")]
		public bool HasMarkerConditions;

		[XmlAttribute("img")]
		public string GalleryImage;

		[XmlAttribute("alsoPlaying")]
		public string AlsoPlaying;

		[XmlAttribute("hint")]
		public string Hint;

		[XmlText]
		public string Title;
	}

	public enum DialogueLayer
	{
		over,
		under
	}
}
