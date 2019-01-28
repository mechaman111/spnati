using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// In-game (non-epilogue) sprite
	/// </summary>
	public class Sprite
	{
		[XmlAttribute("src")]
		public string Src;

		[XmlAttribute("x")]
		public string X;

		[XmlAttribute("y")]
		public string Y;

		[XmlAttribute("z")]
		public string Z;

		[XmlAttribute("width")]
		public string Width;

		[XmlAttribute("height")]
		public string Height;

		[XmlAttribute("scalex")]
		public string ScaleX;

		[XmlAttribute("scaley")]
		public string ScaleY;

		[XmlAttribute("rotation")]
		public string Rotation;

		[XmlAttribute("alpha")]
		public string Alpha;

		[XmlAttribute("pivotx")]
		public string PivotX;

		[XmlAttribute("pivoty")]
		public string PivotY;
	}
}