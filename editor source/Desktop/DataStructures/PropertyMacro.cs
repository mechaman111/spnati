using System.Collections.Generic;

namespace Desktop
{
	/// <summary>
	/// Pre-filled property edit control
	/// </summary>
	public class PropertyMacro
	{
		public string Name;

		public string Property;

		public List<string> Values = new List<string>();

		public string Serialize()
		{
			return string.Join("|*", Values);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
