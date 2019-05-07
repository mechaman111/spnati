using System;

namespace SPNATI_Character_Editor.Analyzers
{
	public abstract class NumericAnalyzer : IDataAnalyzer
	{
		public abstract string Key { get; }
		public abstract string Name { get; }
		public abstract string FullName { get; }
		public abstract string ParentKey { get; }
		public abstract string[] GetValues();

		public Type GetValueType()
		{
			return typeof(int);
		}

		public abstract int GetValue(Character character);

		public bool MeetsCriteria(Character character, string op, string value)
		{
			int actual = GetValue(character);
			if (string.IsNullOrEmpty(value))
			{
				return actual > 0;
			}

			string minStr = value?.ToString();
			string maxStr = null;
			if (!string.IsNullOrEmpty(minStr))
			{
				string[] pieces = minStr.Split('-');
				minStr = pieces[0];
				if (pieces.Length > 1)
				{
					maxStr = pieces[1];
				}
			}
			int min, max;
			if (!int.TryParse(minStr, out min))
			{
				min = int.MinValue;
			}
			if (int.TryParse(maxStr, out max))
			{
				max = int.MaxValue;
			}
			switch (op)
			{
				case "!=":
					return actual != min;
				case ">":
					return actual > min;
				case ">=":
					return actual >= min;
				case "<":
					return actual < min;
				case "<=":
					return actual <= min;
				case "In range":
					return actual >= min && actual <= max;
				case "Not in range":
					return actual < min || actual > max;
				default:
					return actual == min;
			}
		}
	}
}
