using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data representation of listing.xml
	/// </summary>
	[XmlRoot("catalog")]
	public class Listing
	{
		[XmlArray("individuals")]
		[XmlArrayItem("opponent")]
		public List<string> Characters = new List<string>();

		[XmlArray("groups")]
		[XmlArrayItem("group")]
		public List<Group> Groups = new List<Group>();
	}

	public class Group
	{
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
