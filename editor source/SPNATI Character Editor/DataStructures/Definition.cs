using Desktop;

namespace SPNATI_Character_Editor
{
	public class Definition : IRecord
	{
		public string Name { get; set; }
		public string Key { get; set; }
		public string Group { get; set; }
		public string Description { get; set; }

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
		public string ToLookupString()
		{
			return $"{Name} ({Key})";
		}
	}
}
