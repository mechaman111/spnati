using System.Linq;

namespace SPNATI_Character_Editor.Analyzers
{
	public class StyleAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "Styles"; }
		}

		public override string Name
		{
			get { return "Count"; }
		}

		public override string FullName
		{
			get { return "Custom Styles - Count"; }
		}

		public override string ParentKey
		{
			get { return "Custom Styles"; }
		}

		public override int GetValue(Character character)
		{
			if (!string.IsNullOrEmpty(character.StyleSheetName))
			{
				return character.Styles?.Rules?.Count ?? 0;
			}
			return 0;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}
}
