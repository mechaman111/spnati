using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[XmlRoot("collectibles")]
	public class CollectibleData
	{
		[XmlElement("collectible")]
		public List<Collectible> Collectibles = new List<Collectible>();

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public int Count
		{
			get { return Collectibles.Count; }
		}

		public void Add(Collectible collectible)
		{
			Collectibles.Add(collectible);
		}

		public void Remove(Collectible collectible)
		{
			Collectibles.Remove(collectible);
		}

		public Collectible Get(string id)
		{
			return Collectibles.Find(c => c.Id == id);
		}
	}
}
