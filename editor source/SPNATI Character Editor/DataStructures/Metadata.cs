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
	public class Metadata : IHookSerialization
	{
		[XmlElement("enabled")]
		public bool Enabled;

		[XmlElement("first")]
		public string FirstName;

		[XmlElement("last")]
		public string LastName;

		[XmlElement("label")]
		public string Label;

		[XmlElement("pic")]
		public string Portrait;

		[XmlElement("gender")]
		public string Gender;

		[XmlElement("height")]
		public string Height;

		[XmlElement("from")]
		public string Source;

		[XmlElement("writer")]
		public string Writer;

		[XmlElement("artist")]
		public string Artist;

		[XmlElement("description")]
		public string Description;

		[XmlElement("scale")]
		[DefaultValue(100.0f)]
		public float Scale = 100.0f;

		[XmlElement("epilogue")]
		public List<EpilogueMeta> Endings;

		[XmlElement("layers")]
		public int Layers;

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<string> Tags;

		[XmlElement("alternates")]
		public List<Alternate> Skins = new List<Alternate>();

		public Metadata()
		{
		}

		public Metadata(Character c)
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
				Title = e.Title,
				Gender = e.Gender,
				GalleryImage = e.GalleryImage ?? (e.Screens.Count > 0 ? e.Screens[0].Image : null),
				AlsoPlaying = e.AlsoPlaying,
				PlayerStartingLayers = e.PlayerStartingLayers,
				HasMarkerConditions = !string.IsNullOrWhiteSpace(e.AllMarkers)
					|| !string.IsNullOrWhiteSpace(e.AnyMarkers)
					|| !string.IsNullOrWhiteSpace(e.NotMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingAllMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingAnyMarkers)
					|| !string.IsNullOrWhiteSpace(e.AlsoPlayingNotMarkers)
			});
			Tags = c.Tags;
		}

		public void OnBeforeSerialize()
		{

		}

		public void OnAfterDeserialize()
		{
			//Encoding these doesn't need to be done in OnBeforeSerialize because the serializer does it automatically
			Description = XMLHelper.DecodeEntityReferences(Description);
		}
	}

	public class EpilogueMeta
	{
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

		[XmlText]
		public string Title;
	}
}
