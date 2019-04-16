using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// SPNATI config.xml
	/// </summary>
	[XmlRoot("config")]
	public class SpnatiConfig
	{
		[XmlIgnore]
		private static SpnatiConfig _instance;

		public static SpnatiConfig Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Serialization.ImportConfig();
				}
				return _instance;
			}
		}

		[XmlElement("debug")]
		public bool Debug;
		[XmlElement("epilogues")]
		public bool Epilogues;
		[XmlElement("epilogues-unlocked")]
		public bool EpiloguesUnlocked;
		[XmlElement("epilogue_badges")]
		public bool EpilogueBadges;
		[XmlElement("alternate-costumes")]
		public bool AlternateCostumes;
		[XmlElement("alternate-costume-sets")]
		public string AlternateCostumeSets;
		[XmlElement("include-status")]
		public List<string> IncludeStatus;
	}
}
