using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class PoseDirective
	{
		[XmlAttribute("type")]
		public string Type;

		[XmlAttribute("looped")]
		public string Looped;

		[XmlAttribute("frameTime")]
		public string FrameTime;

		[XmlElement("keyframe")]
		public List<PoseKeyframe> Keyframes = new List<PoseKeyframe>();

		[XmlElement("animFrame")]
		public List<PoseAnimFrame> AnimFrames = new List<PoseAnimFrame>();
	}

	public class PoseKeyframe
	{
		[XmlAttribute("x")]
		public string X;

		[XmlAttribute("y")]
		public string Y;

		[XmlAttribute("rotation")]
		public string Rotation;

		[XmlAttribute("scalex")]
		public string ScaleX;

		[XmlAttribute("scaley")]
		public string ScaleY;

		[XmlAttribute("alpha")]
		public string Alpha;

		[XmlAttribute("time")]
		public string Time;

		[XmlAttribute("delay")]
		public string Delay;
	}

	public class PoseAnimFrame
	{
		[XmlAttribute("id")]
		public string Id;
	}
}
