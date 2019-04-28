using Desktop;
using System.Linq;
using System.Reflection;

namespace SPNATI_Character_Editor.Analyzers
{
	public class PoseAnimationCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "PoseAnimatedCount"; }
		}

		public override string Name
		{
			get { return "Count (Animated)"; }
		}

		public override string FullName
		{
			get { return "Pose - Count (Animated)"; }
		}

		public override string ParentKey
		{
			get { return "Custom Pose"; }
		}

		public override int GetValue(Character character)
		{
			return character.CustomPoses.Count(p => p.Directives.Count > 0);
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public abstract class PoseDirectivePropertyAnalyzer : BooleanAnalyzer
	{
		public abstract string PropertyName { get; }

		public override string Key { get { return "PoseDirective" + PropertyName; } }
		public override string Name { get { return "Animates " + PropertyName; } }
		public override string FullName { get { return "Pose - Animates " + PropertyName; } }

		public override string ParentKey
		{
			get { return "Custom Pose>Directives"; }
		}

		public override bool GetValue(Character character)
		{
			FieldInfo fi = PropertyTypeInfo.GetFieldInfo(typeof(Keyframe), PropertyName);
			foreach (Pose pose in character.CustomPoses)
			{
				foreach (PoseDirective directive in pose.Directives)
				{
					foreach (Keyframe kf in directive.Keyframes)
					{
						string value = fi.GetValue(kf)?.ToString();
						if (!string.IsNullOrEmpty(value))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public class PoseConditionalAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "PoseMarker"; }
		}

		public override string Name
		{
			get { return "Marker Usage"; }
		}

		public override string FullName
		{
			get { return "Pose - Marker Count"; }
		}

		public override string ParentKey
		{
			get { return "Custom Pose"; }
		}

		public override int GetValue(Character character)
		{
			int count = 0;
			foreach (Pose pose in character.CustomPoses)
			{
				foreach (Sprite sprite in pose.Sprites)
				{
					if (!string.IsNullOrEmpty(sprite.Marker))
					{
						count++;
					}
				}
			}
			return count;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class PoseSourceAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "Src"; } }
	}
	public class PoseXAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "X"; } }
	}
	public class PoseYAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "Y"; } }
	}
	public class PoseScaleXAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "ScaleX"; } }
	}
	public class PoseScaleYAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "ScaleY"; } }
	}
	public class PoseRotationAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "Rotation"; } }
	}
	public class PoseSkewXAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "SkewX"; } }
	}
	public class PoseSkewYAnalyzer : PoseDirectivePropertyAnalyzer
	{
		public override string PropertyName { get { return "SkewY"; } }
	}
}
