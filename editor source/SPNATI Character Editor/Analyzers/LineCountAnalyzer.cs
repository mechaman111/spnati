namespace SPNATI_Character_Editor.Analyzers
{
	public class LineCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "LineCount"; }
		}

		public override string Name
		{
			get	{ return "Line Count"; }
		}

		public override string FullName
		{
			get { return "Unique Line Count"; }
		}

		public override string ParentKey
		{
			get { return "Dialogue"; }
		}

		public override int GetValue(Character character)
		{
			return character.GetUniqueLineCount();
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class UniquePoseCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "UniquePoseCount"; }
		}

		public override string Name
		{
			get { return "Unique Poses"; }
		}

		public override string FullName
		{
			get { return "Unique Pose Count"; }
		}

		public override string ParentKey
		{
			get { return "Dialogue"; }
		}

		public override int GetValue(Character character)
		{
			int lines, poses;
			character.GetUniqueLineAndPoseCount(out lines, out poses);
			return poses;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}
}
