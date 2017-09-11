using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Screen
	{
		[XmlAttribute("img")]
		public string Image;

		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("text")]
		public List<EndingText> Text = new List<EndingText>();

		public override string ToString()
		{
			return Image;
		}

		public bool IsEmpty
		{
			get
			{
				return Text.Count == 0 && string.IsNullOrEmpty(Image);
			}
		}
	}

	public class EndingText
	{
		[XmlElement("x")]
		public string X;
		[XmlElement("y")]
		public string Y;
		[XmlElement("width")]
		public string Width;
		[XmlElement("arrow")]
		public string Arrow;
		[XmlElement("content")]
		public string Content;

		public override string ToString()
		{
			return Content;
		}
	}
}
