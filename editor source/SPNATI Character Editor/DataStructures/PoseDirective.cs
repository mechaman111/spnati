using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class PoseDirective : Directive
	{
		[XmlAttribute("frameTime")]
		public string FrameTime;

		[XmlElement("animFrame")]
		public List<PoseAnimFrame> AnimFrames = new List<PoseAnimFrame>();

		public override string ToString()
		{
			switch (DirectiveType)
			{
				case "animation":
					return $"Animate: {Id}";
				case "sequence":
					return $"Sequence: {Id}";
			}
			return "New Directive";
		}

		public override object Clone()
		{
			PoseDirective clone = MemberwiseClone() as PoseDirective;
			clone.Keyframes = new List<Keyframe>();
			foreach (Keyframe kf in Keyframes)
			{
				Keyframe clonedFrame = kf.Clone() as Keyframe;
				clonedFrame.Directive = clone;
				clone.Keyframes.Add(clonedFrame);
			}
			clone.AnimFrames = new List<PoseAnimFrame>();
			foreach (PoseAnimFrame pf in AnimFrames)
			{
				PoseAnimFrame clonedFrame = pf.Clone() as PoseAnimFrame;
				clonedFrame.Directive = clone;
				clone.AnimFrames.Add(clonedFrame);
			}
			return clone;
		}
	}

	public class PoseAnimFrame : ICloneable
	{
		/// <summary>
		/// Parent directive
		/// </summary>
		[XmlIgnore]
		public Directive Directive { get; set; }

		[XmlAttribute("id")]
		public string Id;

		public object Clone()
		{
			PoseAnimFrame frame = MemberwiseClone() as PoseAnimFrame;
			return frame;
		}
	}
}
