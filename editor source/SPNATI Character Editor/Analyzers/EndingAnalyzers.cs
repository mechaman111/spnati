using Desktop;
using Desktop.CommonControls.PropertyControls;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SPNATI_Character_Editor.Analyzers
{
	public class EndingCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "EpilogueCount"; }
		}

		public override string Name
		{
			get { return "Count"; }
		}

		public override string FullName
		{
			get { return "Epilogue - (Count)"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override int GetValue(Character character)
		{
			return character.Endings.Count;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class EndingCountGenderAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "EpilogueGenderCount"; }
		}

		public string Name
		{
			get { return "Gender"; }
		}

		public string FullName
		{
			get { return "Epilogue - (Gender)"; }
		}

		public string ParentKey
		{
			get { return "Epilogues"; }
		}

		public string[] GetValues()
		{
			return new string[] { "any", "female", "male" };
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (Epilogue ending in character.Endings)
			{
				if (StringOperations.Matches(ending.Gender, op, value))
				{
					return true;
				}
			}
			return false;
		}
	}

	public class EndingAnimatedAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "EpiloguesAnimated"; }
		}

		public override string Name
		{
			get { return "Animated"; }
		}

		public override string FullName
		{
			get { return "Epilogue - (Has Animations)"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Scene scene in ending.Scenes)
				{
					foreach (Directive dir in scene.Directives)
					{
						string type = dir.DirectiveType;
						if (type != "text" && type != "pause" && type != "sprite")
						{
							//Consider any directive that doesn't come from the old-school conversion to be animated
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public class EndingMarkerUnlockAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "EpiloguesMarkerUnlock"; }
		}

		public override string Name
		{
			get { return "Unlocked by Markers"; }
		}

		public override string FullName
		{
			get { return "Epilogue - (Unlocked by Markers)"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Epilogue ending in character.Endings)
			{
				if (!string.IsNullOrEmpty(ending.AlsoPlayingAllMarkers) ||
					!string.IsNullOrEmpty(ending.AlsoPlayingAnyMarkers) ||
					!string.IsNullOrEmpty(ending.AlsoPlayingNotMarkers) ||
					!string.IsNullOrEmpty(ending.AllMarkers) ||
					!string.IsNullOrEmpty(ending.AnyMarkers) ||
					!string.IsNullOrEmpty(ending.NotMarkers))
				{
					return true;
				}
			}
			return false;
		}
	}

	public class EndingStartingLayerUnlockAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "EpiloguesStartingLayersUnlock"; }
		}

		public override string Name
		{
			get { return "Unlocked w/ Starting Layers"; }
		}

		public override string FullName
		{
			get { return "Epilogue - (Unlocked w/ Starting Layers)"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Epilogue ending in character.Endings)
			{
				if (!string.IsNullOrEmpty(ending.PlayerStartingLayers))
				{
					return true;
				}
			}
			return false;
		}
	}

	public class EndingAlsoPlayingAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "EndingAlsoPlaying"; }
		}

		public string Name
		{
			get { return "Also Playing"; }
		}

		public string FullName
		{
			get { return "Epilogue - Also Playing"; }
		}

		public string ParentKey
		{
			get { return "Epilogues"; }
		}

		public string[] GetValues()
		{
			List<string> options = new List<string>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c.FolderName == "human")
				{
					continue;
				}
				options.Add(c.FolderName);
			}
			return options.ToArray();
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (Epilogue ending in character.Endings)
			{
				if (!string.IsNullOrEmpty(ending.AlsoPlaying))
				{
					if (StringOperations.Matches(ending.AlsoPlaying, op, value))
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public class EndingTransitionTypeAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "EndingTransition"; }
		}

		public string Name
		{
			get { return "Transition"; }
		}

		public string FullName
		{
			get { return "Epilogue - Transition"; }
		}

		public string ParentKey
		{
			get { return "Epilogues"; }
		}

		public string[] GetValues()
		{
			FieldInfo fi = PropertyTypeInfo.GetFieldInfo(typeof(Scene), "Effect");
			ComboBoxAttribute attr = fi.GetCustomAttribute<ComboBoxAttribute>();
			if (attr != null)
			{
				return attr.Options;
			}
			return null;
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Scene scene in ending.Scenes)
				{
					if (scene.Transition)
					{
						if (string.IsNullOrEmpty(value))
						{
							return true;
						}
						if (StringOperations.Matches(scene.Effect, op, value))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public class EndingParticleAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "Particles"; }
		}

		public override string Name
		{
			get { return "Emitters"; }
		}

		public override string FullName
		{
			get { return "Epilogue - Uses Emitters"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Scene scene in ending.Scenes)
				{
					foreach (Directive dir in scene.Directives)
					{
						if (dir.DirectiveType == "emitter")
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public class EndingBurstAnalyzer : BooleanAnalyzer
	{
		public override string Key
		{
			get { return "BurstParticles"; }
		}

		public override string Name
		{
			get { return "Burst Effects"; }
		}

		public override string FullName
		{
			get { return "Epilogue - Uses Burst Emitters"; }
		}

		public override string ParentKey
		{
			get { return "Particles"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Scene scene in ending.Scenes)
				{
					foreach (Directive dir in scene.Directives)
					{
						if (dir.DirectiveType == "emit")
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public class EndingConditionalAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "EpilogueMarker"; }
		}

		public override string Name
		{
			get { return "Marker Usage"; }
		}

		public override string FullName
		{
			get { return "Epilogue - Marker Count"; }
		}

		public override string ParentKey
		{
			get { return "Epilogues"; }
		}

		public override int GetValue(Character character)
		{
			int count = 0;
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Scene scene in ending.Scenes)
				{
					foreach (Directive directive in scene.Directives)
					{
						if (!string.IsNullOrEmpty(directive.Marker))
						{
							count++;
						}
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
}
