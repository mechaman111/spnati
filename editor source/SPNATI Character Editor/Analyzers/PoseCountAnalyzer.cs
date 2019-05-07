namespace SPNATI_Character_Editor.Analyzers
{
	public class PoseCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "PoseCount"; }
		}

		public override string Name
		{
			get	{ return "Count (All Custom)"; }
		}

		public override string FullName
		{
			get { return "Pose - Count (All)"; }
		}

		public override string ParentKey
		{
			get { return "Custom Pose"; }
		}

		public override int GetValue(Character character)
		{
			return character.CustomPoses.Count;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}
}
