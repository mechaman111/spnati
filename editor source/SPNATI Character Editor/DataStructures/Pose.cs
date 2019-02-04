using Desktop.CommonControls.PropertyControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Pose composed of sprites and animations
	/// </summary>
	public class Pose : ICloneable
	{
		[Text(DisplayName = "ID", GroupOrder = 0)]
		[XmlAttribute("id")]
		public string Id;

		[Float(DisplayName = "Base Height", Key = "baseHeight", GroupOrder = 10, Minimum = 0, Maximum = 50000, DecimalPlaces = 0)]
		[DefaultValue("1400")]
		[XmlAttribute("baseHeight")]
		public string BaseHeight = "1400";

		[XmlElement("sprite")]
		public List<Sprite> Sprites = new List<Sprite>();

		[XmlElement("directive")]
		public List<PoseDirective> Directives = new List<PoseDirective>();

		public override string ToString()
		{
			return Id;
		}

		public object Clone()
		{
			Pose pose = MemberwiseClone() as Pose;
			pose.Sprites = new List<Sprite>();
			foreach (Sprite sprite in Sprites)
			{
				Sprite clonedSprite = sprite.Clone() as Sprite;
				pose.Sprites.Add(clonedSprite);
			}

			pose.Directives = new List<PoseDirective>();
			foreach (PoseDirective directive in Directives)
			{
				PoseDirective clonedDirective = directive.Clone() as PoseDirective;
				pose.Directives.Add(clonedDirective);
			}
			return pose;
		}

		public void OnAfterDeserialize()
		{
			foreach (Sprite sprite in Sprites)
			{
				sprite.ScaleX = sprite.ScaleX ?? sprite.Scale;
				sprite.ScaleY = sprite.ScaleY ?? sprite.Scale;
			}
		}
	}
}
