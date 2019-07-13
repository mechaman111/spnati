using Desktop;

namespace SPNATI_Character_Editor
{
	public class TargetId : IRecord
	{
		public string Group { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public TargetId(string key, string name, string group, string description)
		{
			Group = group;
			Key = key;
			Name = name;
			Description = description;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public string ToLookupString()
		{
			return Name;
		}
	}
}
