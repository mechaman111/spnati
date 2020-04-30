using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml;

namespace SPNATI_Character_Editor
{
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
					_instance = Serialization.ImportListing();
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

		public OpponentStatus GetCharacterStatus(string name)
		{
			var opponent = Characters.Find(opp => opp.Name == name);
			if (opponent != null)
			{
				return opponent.Status;
			}
			else
			{
				return OpponentStatus.Unlisted;
			}
		}
	}

	public enum OpponentStatus
	{
		[XmlEnum()]
		Main,
		[XmlEnum("testing")]
		Testing,
		[XmlEnum("offline")]
		Offline,
		[XmlEnum("incomplete")]
		Incomplete,
		[XmlEnum("duplicate")]
		Duplicate,
		[XmlEnum("event")]
		Event,
		[XmlEnum()]
		Unlisted
	}

	public class Opponent
	{
		[XmlAttribute("release")]
		public string ReleaseNumber;

		[XmlAttribute("status")]
		[DefaultValue(OpponentStatus.Main)]
		public OpponentStatus Status;

		[XmlText]
		public string Name;

		[XmlAnyAttribute]
		public List<XmlAttribute> ExtraAttributes;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public Opponent()
		{

		}

		public Opponent(string name, OpponentStatus status)
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
