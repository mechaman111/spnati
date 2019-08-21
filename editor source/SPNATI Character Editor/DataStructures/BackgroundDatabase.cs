using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public static class BackgroundDatabase
	{
		private static BackgroundList _list;

		public static void Load()
		{
			BackgroundDatabase._list = Serialization.ImportBackgrounds();
			BackgroundTag nameTag = new BackgroundTag("name");
			Definitions.Instance.Add(nameTag);
			if (_list != null)
			{
				foreach (Background bkg in _list.Backgrounds)
				{
					nameTag.AddValue(bkg.Id);
					foreach (XmlElement el in bkg.Elements)
					{
						string name = el.Name;
						if (!IsExcluded(name))
						{
							BackgroundTag tag = Definitions.Instance.Get<BackgroundTag>(name);
							if (tag == null)
							{
								tag = new BackgroundTag(name);
								Definitions.Instance.Add(tag);

								if (Definitions.Instance.Get<BackgroundTagValue>(name) == null)
								{
									BackgroundTagValue tagValue = new BackgroundTagValue(name);
									Definitions.Instance.Add(tagValue);
								}
							}
							tag.AddValue(el.InnerText);
						}
					}
				}
			}
		}

		private static bool IsExcluded(string value)
		{
			bool result;
			if (string.IsNullOrEmpty(value))
			{
				result = true;
			}
			else
			{
				result = value == "filter" || value == "src" || value == "author" || value == "name";
			}
			return result;
		}
	}

	public class Background
	{
		[XmlAttribute("id")]
		public string Id;

		[XmlAnyElement]
		public List<XmlElement> Elements = new List<XmlElement>();
	}

	[XmlRoot("backgrounds")]
	public class BackgroundList
	{
		[XmlArray("auto-tag-metadata")]
		[XmlArrayItem("key")]
		public List<BackgroundKey> AutoTagMetadata = new List<BackgroundKey>();

		[XmlElement("background")]
		public List<Background> Backgrounds = new List<Background>();
	}

	public class BackgroundKey
	{
		[XmlAttribute("boolean")]
		public string BooleanElement;

		[XmlText]
		public string Name;

		public bool IsBoolean
		{
			get	{ return BooleanElement != null; }
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class BackgroundTag : Definition
	{
		public HashSet<string> Values = new HashSet<string>();

		public BackgroundTag() { }
		public BackgroundTag(string name)
		{
			Key = name;
			Name = name;
		}

		public void AddValue(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Values.Add(value);
				BackgroundTagValue tagValue = Definitions.Instance.Get<BackgroundTagValue>(value);
				if (tagValue == null)
				{
					tagValue = new BackgroundTagValue(value);
					Definitions.Instance.Add(tagValue);
				}
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public override string ToLookupString()
		{
			return Key;
		}
	}

	public class BackgroundTagValue : Definition
	{
		public BackgroundTagValue() { }
		public BackgroundTagValue(string value)
		{
			Key = value;
			Name = value;
		}

		public override string ToString()
		{
			return Key;
		}

		public override string ToLookupString()
		{
			return Key;
		}
	}
}
