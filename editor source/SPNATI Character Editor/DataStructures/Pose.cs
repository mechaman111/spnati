using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Pose composed of sprites and animations
	/// </summary>
	public class Pose
	{
		[XmlAttribute("id")]
		public string Id;

		[XmlAttribute("baseHeight")]
		public string BaseHeight;

		[XmlElement("sprite")]
		public List<Sprite> Sprites = new List<Sprite>();

		[XmlElement("directive")]
		public List<PoseDirective> Directives = new List<PoseDirective>();
	}
}
