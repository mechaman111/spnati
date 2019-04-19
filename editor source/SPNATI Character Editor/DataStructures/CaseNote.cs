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
}
