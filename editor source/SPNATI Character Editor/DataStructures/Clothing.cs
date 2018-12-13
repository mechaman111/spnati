using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Globalization;

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
		public string GenericName;

		[XmlAttribute("position")]
		public string Position;

		[XmlAttribute("formalName")]
		public string FormalName;

		/// <summary>
		/// Deprecated
		/// </summary>
		[XmlAttribute("proper-name")]
		public string ProperName;

		[XmlAttribute("type")]
		public string Type;

		[XmlAttribute("plural")]
		[DefaultValue(false)]
		public bool Plural;

		public Clothing()
		{
			Position = "upper";
			Type = "major";
			FormalName = "New item";
			GenericName = "new item";
			Plural = false;
		}

		public void OnAfterDeserialize()
		{
			if (FormalName == null || FormalName == "New item")
			{
				FormalName = ProperName;
				ProperName = null;
			}
		}

		public override string ToString()
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GenericName);
		}
	}
}
