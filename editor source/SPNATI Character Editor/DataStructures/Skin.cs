using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// I don't know why there is this wrapper layer around costume
	/// </summary>
	public class Alternate
	{
		[XmlElement("costume")]
		public Costume Skin;
	}

	/// <summary>
	/// Alternate skin data stored in costume.xml
	/// </summary>
	public class Costume
	{
	}

	/// <summary>
	/// Link to a reskin used in meta.xml
	/// </summary>
	public class SkinLink
	{
		[XmlAttribute("folder")]
		public string Folder;

		[XmlAttribute("img")]
		public string PreviewImage;

		[XmlText]
		public string Name;
	}
}
