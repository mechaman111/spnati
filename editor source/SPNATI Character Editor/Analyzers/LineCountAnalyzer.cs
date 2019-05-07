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
			get { return "Line Count - Unique"; }
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

	public class TargetedLineCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "TargetedLineCount"; }
		}

		public override string Name
		{
			get { return "Line Count - Targeted"; }
		}

		public override string FullName
		{
			get { return "Targeted Line Count"; }
		}

		public override string ParentKey
		{
			get { return "Dialogue"; }
		}

		public override int GetValue(Character character)
		{
			return character.GetTargetedLineCount();
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class SpecialLineCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "SpecialLineCount"; }
		}

		public override string Name
		{
			get { return "Line Count - Special"; }
		}

		public override string FullName
		{
			get { return "Special Line Count"; }
		}

		public override string ParentKey
		{
			get { return "Dialogue"; }
		}

		public override int GetValue(Character character)
		{
			return character.GetSpecialLineCount();
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class FilterLineCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "FilterLineCount"; }
		}

		public override string Name
		{
			get { return "Line Count - Filtered"; }
		}

		public override string FullName
		{
			get { return "Filtered Line Count"; }
		}

		public override string ParentKey
		{
			get { return "Dialogue"; }
		}

		public override int GetValue(Character character)
		{
			return character.GetFilterLineCount();
		}

		public override string[] GetValues()
		{
			return null;
		}
	}
}
