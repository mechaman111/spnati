using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("tagdictionary", Namespace = "")]
	public class TagDictionary
	{
		[XmlElement("group")]
		public List<TagGroup> Groups = new List<TagGroup>();

		private Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
		private Dictionary<string, List<string>> _pairedTags = new Dictionary<string, List<string>>();

		public void CacheData()
		{
			List<Tag> tags = new List<Tag>();
			foreach (TagGroup group in Groups)
			{
				foreach (Tag tag in group.Tags)
				{
					tag.Group = group.Label;
					_tags[tag.Value] = tag;

					if (!string.IsNullOrEmpty(tag.RawPairings))
					{
						tags.Add(tag);
						string[] pairs = tag.RawPairings.Split(',');
						foreach (string pair in pairs)
						{
							tag.PairedTags.Add(pair);
							List<string> children = _pairedTags.GetOrAddDefault(pair, () => new List<string>());
							children.Add(tag.Value);
						}
					}
				}
			}

			foreach (Tag tag in tags)
			{
				foreach (string pair in tag.PairedTags)
				{
					Tag pairedTag = GetTag(pair);
					if (pairedTag == null)
					{
						pairedTag = new Tag() { DisplayName = pair, Value = pair };
						_tags[pair] = pairedTag;
					}
					pairedTag.ChildrenTags.Add(tag.Value);
				}
			}
		}

		public IEnumerable<Tag> Tags
		{
			get { return _tags.Values; }
		}

		public void AddIfNew(string value)
		{
			if (_tags.ContainsKey(value)) { return; }
			Tag tag = new Tag() { Key = value, DisplayName = value };
			_tags.Add(value, tag);
		}

		public Tag AddTag(string value)
		{
			Tag tag = new Tag() { Key = value, DisplayName = value };
			_tags.Add(value, tag);
			return tag;
		}

		public Tag GetTag(string value)
		{
			return _tags.Get(value);
		}

		public TagGroup GetGroup(string name)
		{
			return Groups.Find(g => g.Label == name);
		}

		public bool IsPairedTag(string value)
		{
			return _pairedTags.ContainsKey(value);
		}
	}

	public class TagGroup
	{
		[XmlAttribute("label")]
		public string Label;

		[XmlAttribute("multiselect")]
		public bool MultiSelect;

		[XmlElement("tag")]
		public List<Tag> Tags = new List<Tag>();

		[XmlAttribute("gender")]
		public string Gender;

		[XmlAttribute("hidden")]
		public bool Hidden;

		public override string ToString()
		{
			return Label;
		}
	}
}
