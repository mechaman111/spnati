namespace Desktop
{
	public class Category : IRecord
	{
		public Category(string key, string value)
		{
			Key = key;
			Name = value;
		}

		public string Group
		{
			get { return ""; }
		}

		public string Key { get; set; }
		public string Name { get; set; }

		public int CompareTo(IRecord other)
		{
			return Key.CompareTo(other.Key);
		}

		public virtual string ToLookupString()
		{
			return $"{Name} [{Key}]";
		}
	}
}
