using System;

namespace Desktop
{
	public interface IRecord : IComparable<IRecord>
	{
		string Name { get; }
		string Key { get; set; }
		string Group { get; }

		string ToLookupString();
	}
}
