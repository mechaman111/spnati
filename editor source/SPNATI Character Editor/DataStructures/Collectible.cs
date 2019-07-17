using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	public class Collectible : IRecord, IComparable<Collectible>
	{
		[Text(DisplayName = "Id", Description = "Unique identifier", GroupOrder = 0)]
		[DefaultValue("")]
		[XmlAttribute("id")]
		public string Id;

		[FileSelect(DisplayName = "Image", Description = "Image when viewing the collectible", GroupOrder = 40)]
		[DefaultValue("")]
		[XmlAttribute("img")]
		public string Image;

		[FileSelect(DisplayName = "Thumbnail", Description = "Image when viewing the collectible", GroupOrder = 20)]
		[DefaultValue("")]
		[XmlAttribute("thumbnail")]
		public string Thumbnail;

		[Text(DisplayName = "Title", Description = "Display name", GroupOrder = 5)]
		[DefaultValue("")]
		[XmlElement("title")]
		public string Title;

		[Text(DisplayName = "Subtitle", Description = "Flavor text for the collectible on the unlock screen", GroupOrder = 10)]
		[DefaultValue("")]
		[XmlElement("subtitle")]
		public string Subtitle;

		[Text(DisplayName = "Text", Description = "Text to display when viewing the collectible", GroupOrder = 50, Multiline = true, RowHeight = 135)]
		[DefaultValue("")]
		[XmlElement("text")]
		public string Text;

		[Text(DisplayName = "Unlock Hint", Description = "Text to display when the collectible has not been unlocked yet", GroupOrder = 100, Multiline = true, RowHeight = 80)]
		[DefaultValue("")]
		[XmlElement("unlock")]
		public string UnlockHint;

		[Boolean(DisplayName = "Secret", GroupOrder = 110, Description = "If checked, collectible will not appear in the collectibles list at all until unlocked")]
		[DefaultValue(false)]
		[XmlElement("hidden")]
		public bool Hidden;

		[Boolean(DisplayName = "Hide Details", GroupOrder = 120, Description = "If checked, title and subtitle will not be displayed when viewing a locked collectible")]
		[DefaultValue(false)]
		[XmlElement("hide-details")]
		public bool HideDetails;

		[Numeric(DisplayName = "Counter", GroupOrder = 60, Description = "If checked, a progress bar will be displayed and the collectible will not be unlocked until reaching this value", Minimum = 0, Maximum = 1000)]
		[DefaultValue(0)]
		[XmlElement("counter")]
		public int Counter;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		[XmlIgnore]
		public Character Character;

		[XmlIgnore]
		public List<Case> LinkedCases = new List<Case>();

		public string Name
		{
			get
			{
				return Title;
			}
		}

		public string Key
		{
			get { return Id; }
			set { Id = value; }
		}

		public string Group
		{
			get
			{
				return "";
			}
		}

		public override string ToString()
		{
			return Title;
		}

		public int CompareTo(Collectible other)
		{
			return Id.CompareTo(other.Id);
		}

		public string ToLookupString()
		{
			return $"{Title} [{Id}]";
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}
