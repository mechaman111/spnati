using Desktop.DataStructures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[XmlRoot("collectibles")]
	public class CollectibleData : BindableObject
	{
		[XmlElement("collectible")]
		public ObservableCollection<Collectible> Collectibles
		{
			get { return Get<ObservableCollection<Collectible>>(); }
			set { Set(value); }
		}

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public CollectibleData()
		{
			Collectibles = new ObservableCollection<Collectible>();
		}

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
