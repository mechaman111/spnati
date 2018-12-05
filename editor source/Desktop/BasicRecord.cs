namespace Desktop
{
	/// <summary>
	/// Base class for records that just launch activities
	/// </summary>
	public class BasicRecord : IRecord
	{
		public string Name { get; set; }

		public string Key { get; set; }

		public string Group { get; }

		public string ToLookupString()
		{
			return $"{Name} [{Key}]";
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}
