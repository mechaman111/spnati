using SPNATI_Character_Editor.DataStructures;
using System.IO;

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
			get { return "Count (All Custom)"; }
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

	public class PoseMatrixAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "PoseMatrix"; }
		}

		public override string Name
		{
			get { return "Used"; }
		}

		public override string FullName
		{
			get { return "Uses Pose Matrix"; }
		}

		public override string ParentKey
		{
			get { return "Pose Matrix"; }
		}

		public override bool GetValue(Character character)
		{
			string file = Path.Combine(character.GetDirectory(), "poses.xml");
			return File.Exists(file);
		}
	}

	public class PipelineAnalzyer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "PipelineUsage"; }
		}

		public override string Name
		{
			get { return "Pipelines"; }
		}

		public override string FullName
		{
			get { return "Uses Pipelines"; }
		}

		public override string ParentKey
		{
			get { return "Pose Matrix"; }
		}

		public override bool GetValue(Character character)
		{
			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(character);
			if (matrix.Pipelines.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

}
