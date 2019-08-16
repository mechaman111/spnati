using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Trigger
	{
		[XmlAttribute("id")]
		public string Id;

		[XmlElement("case")]
		public List<Case> Cases = new List<Case>();

		public Trigger()
		{
		}

		public Trigger(string tag)
		{
			Id = tag;
		}
	}
}
