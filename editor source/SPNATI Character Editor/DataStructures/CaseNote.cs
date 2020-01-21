using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class CaseNote
	{
		[XmlAttribute("id")]
		public int Id;
		[XmlText]
		public string Text;
	}

	public class CaseRecipe
	{
		[XmlAttribute("key")]
		public string Key;
		[XmlArray("cases")]
		[XmlArrayItem("id")]
		public List<int> CaseIds = new List<int>();
	}
}
