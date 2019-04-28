using System;

namespace SPNATI_Character_Editor.Analyzers
{
	public abstract class BooleanAnalyzer : IDataAnalyzer
	{
		public abstract string Key { get; }
		public abstract string Name { get; }
		public abstract string ParentKey { get; }
		public abstract string FullName { get; }

		public string[] GetValues()
		{
			return new string[] { "true", "false" };
		}

		public Type GetValueType()
		{
			return typeof(bool);
		}

		public abstract bool GetValue(Character character);

		public bool MeetsCriteria(Character character, string op, string value)
		{
			op = op ?? "==";
			bool actual = GetValue(character);
			bool expected = (value != "false" && op == "==") || (value == "false" && op == "!=");
			return actual == expected;
		}
	}
}
