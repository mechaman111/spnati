using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("requirements")]
	public class TestRequirements
	{
		private static TestRequirements _instance;

		public static TestRequirements Instance
		{
			get
			{
				TextReader reader = null;
				string path = Path.Combine(Config.SpnatiDirectory, "opponents", "new_character_requirements.xml");
				if (File.Exists(path))
				{
					try
					{
						XmlSerializer serializer = new XmlSerializer(typeof(TestRequirements), "");
						reader = new StreamReader(path);
						_instance = serializer.Deserialize(reader) as TestRequirements;
					}
					finally
					{
						if (reader != null)
							reader.Close();
					}
				}
				else
				{
					_instance = new TestRequirements();
				}
				return _instance;
			}
		}

		[XmlElement("lines")]
		public int Lines;

		[XmlElement("targeted")]
		public int Targeted;

		[XmlElement("unique")]
		public int UniqueTargets;

		[XmlElement("filtered")]
		public int Filtered;

		[XmlElement("sizelimit")]
		public int SizeLimit;

		[XmlElement("collectibleLines")]
		public int LinesPerCollectible;

		public int GetAllowedCollectibles(int lineCount)
		{
			if (lineCount < 1)
			{
				return 1;
			}
			return (lineCount - 1) / LinesPerCollectible + 2;
		}
	}
}
