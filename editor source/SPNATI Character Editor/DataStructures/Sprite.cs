using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// In-game (non-epilogue) sprite
	/// </summary>
	public class Sprite
	{
		/// <summary>
		/// personal notes
		/// x,y,width,height are all % of baseHeight
		/// ex. true width = width * display.height / baseHeight
		/// 
		/// width and height are basically required, since otherwise it doesn't scale with display
		/// 
		/// keyframes need a starting frame - can't just rely on the previous state
		/// 
		/// x is centered (i.e. x = container.width * 0.5 + x * display.height / baseHeight
		/// </summary>


		[XmlAttribute("id")]
		public string Id;

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