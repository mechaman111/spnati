using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data for a single layer of clothing
	/// </summary>
	[Serializable]
	public class Clothing
	{
		public const int ExtraStages = 3;

		[XmlAttribute("lowercase")]
		public string Lowercase;

		[XmlAttribute("position")]
		public string Position;

		[XmlAttribute("proper-name")]
		public string Name;

		[XmlAttribute("type")]
		public string Type;

		public Clothing()
		{
			Position = "upper";
			Type = "major";
			Name = "New item";
			Lowercase = "new item";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
