namespace SPNATI_Character_Editor.Analyzers
{
	public interface IDataCriterion
	{
		bool IsMet(Character character);
	}

	public class DataCriterion : IDataCriterion
	{
		public IDataAnalyzer Analyzer;
		public string Operator = "==";
		public string Value;

		public bool IsMet(Character character)
		{
			if (Analyzer == null)
			{
				return false;
			}

			return Analyzer.MeetsCriteria(character, Operator, Value);
		}

		public override string ToString()
		{
			return Analyzer.ToString();
		}
	}
}
