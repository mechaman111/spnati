using System;
using System.Xml.Serialization;
using System.ComponentModel;

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

		[XmlAttribute("plural")]
		[DefaultValue(false)]
		public bool Plural;

		public Clothing()
		{
			Position = "upper";
			Type = "major";
			Name = "New item";
			Lowercase = "new item";
			Plural = false;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
