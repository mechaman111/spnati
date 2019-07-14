using Desktop;
using Newtonsoft.Json;

namespace SPNATI_Character_Editor
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Definition : IRecord
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("group")]
		public string Group { get; set; }
		[JsonProperty("description")]
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
