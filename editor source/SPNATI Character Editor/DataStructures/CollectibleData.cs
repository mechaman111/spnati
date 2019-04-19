using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[XmlRoot("collectibles")]
	public class CollectibleData
	{
		[XmlElement("collectible")]
		public List<Collectible> Collectibles = new List<Collectible>();

		public int Count
		{
			get { return Collectibles.Count; }
		}

		public void Add(Collectible collectible)
		{
			Collectibles.Add(collectible);
			Collectibles.Sort();
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
