using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// Everything in this file is legacy and only for the sake of backwards compatibility.
	/// The editor will convert anything using these formats to the directive format and when saving, replace with the new format
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Legacy screen tag
	/// </summary>
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

	/// <summary>
	/// Legacy text tag
	/// </summary>
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
		[XmlAttribute("css")]
		public string Css;

		public override string ToString()
		{
			return Content;
		}
	}

	/// <summary>
	/// Legacy background tag
	/// </summary>
	public class Background
	{
		[XmlAttribute("img")]
		public string Image;

		[XmlElement("scene")]
		public List<Scene> Scenes = new List<Scene>();
	}

	/// <summary>
	/// Legacy sprite tag
	/// </summary>
	public class EndingSprite
	{
		[XmlElement("x")]
		public string X;
		[XmlElement("y")]
		public string Y;
		[XmlElement("width")]
		public string Width;
		[XmlElement("src")]
		public string Src;
		[XmlAttribute("css")]
		public string Css;

		public override string ToString()
		{
			return Src;
		}
	}
}
