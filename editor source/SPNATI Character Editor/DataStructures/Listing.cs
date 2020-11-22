using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml;

namespace SPNATI_Character_Editor
{
	public static class Listings
	{
		public static Listing Core;
		public static Listing Test;
	}

	/// <summary>
	/// Data representation of listing.xml
	/// </summary>
	[XmlRoot("catalog")]
	public class Listing
	{
		[XmlIgnore]
		private static Listing _instance;
		
		public static Listing Instance
		{
			get
			{
				if (_instance == null)
				{
					Listing combined = new Listing();
					Listings.Core = Serialization.ImportListing("listing.xml");
					Listings.Test = Serialization.ImportListing("listing-test.xml");
					foreach (Listing listing in new Listing[] { Listings.Core, Listings.Test })
					{
						combined.Characters.AddRange(listing.Characters);
						combined.Groups.AddRange(listing.Groups);
					}
					_instance = combined;
				}
				return _instance;
			}
		}

		[XmlArray("individuals")]
		[XmlArrayItem("opponent")]
		public List<Opponent> Characters = new List<Opponent>();

		[XmlArray("groups")]
		[XmlArrayItem("group")]
		public List<Group> Groups = new List<Group>();

		[XmlAnyAttribute]
		public List<XmlAttribute> ExtraAttributes;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public string GetCharacterStatus(string name)
		{
			var opponent = Characters.Find(opp => opp.Name == name);
			if (opponent != null)
			{
				return opponent.Status ?? OpponentStatus.Main;
			}
			else
			{
				return OpponentStatus.Unlisted;
			}
		}
	}

	public static class OpponentStatus
	{
		public static readonly string Main = "";
		public static readonly string Testing = "testing";
		public static readonly string Offline = "offline";
		public static readonly string Incomplete = "incomplete";
		public static readonly string Duplicate = "duplicate";
		public static readonly string Event = "event";
		public static readonly string Unlisted = null;
	}

	public class Opponent
	{
		[XmlAttribute("release")]
		public string ReleaseNumber;

		[XmlAttribute("status")]
		[DefaultValue(null)]
		public string Status;

		[XmlText]
		public string Name;

		[XmlAnyAttribute]
		public List<XmlAttribute> ExtraAttributes;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public Opponent()
		{

		}

		public Opponent(string name, string status)
		{
			Name = name;
			Status = status;
		}
	}

	public class Group
	{
		[XmlAttribute("testing")]
		[DefaultValue(false)]
		public bool Test;
		[XmlAttribute("title")]
		public string Name;
		[XmlAttribute("opp1")]
		public string Opponent1;
		[XmlAttribute("opp2")]
		public string Opponent2;
		[XmlAttribute("opp3")]
		public string Opponent3;
		[XmlAttribute("opp4")]
		public string Opponent4;
		[XmlAttribute("background")]
		public string Background;

		[XmlAnyAttribute]
		public List<XmlAttribute> ExtraAttributes;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public Group()
		{
		}

		public Group(string title, params string[] players)
		{
			Name = title;
			if (players.Length >= 1)
				Opponent1 = players[0];
			if (players.Length >= 2)
				Opponent2 = players[1];
			if (players.Length >= 3)
				Opponent3 = players[2];
			if (players.Length >= 4)
				Opponent4 = players[3];
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
