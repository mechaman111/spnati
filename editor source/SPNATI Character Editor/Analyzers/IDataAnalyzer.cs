using System;

namespace SPNATI_Character_Editor.Analyzers
{
	public interface IDataAnalyzer
	{
		/// <summary>
		/// Unique key
		/// </summary>
		string Key { get; }
		/// <summary>
		/// Unique key of parent property, if any
		/// </summary>
		string ParentKey { get; }
		/// <summary>
		/// Display name as it appears in the tree
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Name as it appears in the grid
		/// </summary>
		string FullName { get; }
		/// <summary>
		/// Gets a list of all possible values for this property
		/// </summary>
		/// <returns></returns>
		string[] GetValues();
		/// <summary>
		/// Gets what type of values this analyzer works with
		/// </summary>
		/// <returns></returns>
		Type GetValueType();
		/// <summary>
		/// Gets whether a character meets the given criteria
		/// </summary>
		bool MeetsCriteria(Character character, string op, string value);
	}

	public static class StringOperations
	{
		public static bool Matches(string actual, string op, string expected)
		{
			switch (op)
			{
				case "!=":
					return actual != expected;
				case "Contains":
					return actual.Contains(expected);
				case "Does not contain":
					return !actual.Contains(expected);
				default:
					return actual == expected;
			}
		}
	}
}
