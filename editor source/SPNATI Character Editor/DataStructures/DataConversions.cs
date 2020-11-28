using Desktop;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace SPNATI_Character_Editor
{
	public static class DataConversions
	{
		public static void ConvertVersion(Character character)
		{
			string version = character.Version;
			if (Config.VersionPredates(version, "v3.2"))
			{
				Convert3_2(character);
			}
			if (version == "v4.2")
			{
				//fix weight bug from 4.2
				foreach (Case workingCase in character.Behavior.GetWorkingCases())
				{
					foreach (DialogueLine line in workingCase.Lines)
					{
						if (line.Weight == 0)
						{
							line.Weight = 1;
						}
					}
				}
			}
			if (Config.VersionPredates(version, "v5.2"))
			{
				Convert5_1(character);
				Convert5_2(character);
			}
			if (Config.VersionPredates(version, "v5.2.7"))
			{
				Convert5_2_7(character);
			}
			if (Config.VersionPredates(version, "v5.8"))
			{
				Convert5_8(character);
			}
			if (Config.VersionPredates(version, "v6.1"))
			{
				Convert6_1(character);
			}
		}

		private static void Convert3_2(Character character)
		{
			//convert old-style epilogues
			foreach (Epilogue ending in character.Endings)
			{
				if (ending.Screens.Count > 0)
				{
					ConvertScreenBased(character, ending);
				}
				else if (ending.Backgrounds.Count > 0)
				{
					ConvertBackgroundSceneBased(character, ending);
				}
			}
		}

		/// <summary>
		/// Converts old screen-based epilogues to modern format
		/// </summary>
		private static void ConvertScreenBased(Character character, Epilogue ending)
		{
			ending.Scenes.Clear();
			foreach (Screen screen in ending.Screens)
			{
				string image = screen.Image;

				string sceneWidth = null;
				string sceneHeight = null;
				if (!string.IsNullOrEmpty(image))
				{
					if (string.IsNullOrEmpty(ending.GalleryImage))
					{
						ending.GalleryImage = image;
					}
					try
					{
						Image img = new Bitmap(Path.Combine(Config.GetRootDirectory(character), image));
						sceneWidth = img.Width.ToString();
						sceneHeight = img.Height.ToString();
						img.Dispose();
					}
					catch { }
				}

				Scene scene = new Scene()
				{
					Background = image,
					Width = sceneWidth,
					Height = sceneHeight,
				};
				ending.Scenes.Add(scene);

				foreach (EndingText text in screen.Text)
				{
					Directive dir = new Directive("text");
					dir.Width = text.Width;
					dir.X = text.X;
					dir.Y = text.Y;
					dir.Arrow = text.Arrow;
					dir.Text = text.Content;
					scene.Directives.Add(dir);

					scene.Directives.Add(new Directive("pause"));
				}
				if (scene.Directives.Count == 0)
				{
					scene.Directives.Add(new Directive("pause"));
				}
			}

			ending.Backgrounds.Clear();
			ending.Screens.Clear();
		}

		/// <summary>
		/// Converts background/scene-based epilogues to modern format
		/// </summary>
		private static void ConvertBackgroundSceneBased(Character character, Epilogue ending)
		{
			ending.Scenes.Clear();
			foreach (LegacyBackground bg in ending.Backgrounds)
			{
				string image = bg.Image;

				string sceneWidth = "";
				string sceneHeight = "";
				if (!string.IsNullOrEmpty(image))
				{
					try
					{
						Image img = new Bitmap(Path.Combine(Config.GetRootDirectory(character), image));
						sceneWidth = img.Width.ToString();
						sceneHeight = img.Height.ToString();
						img.Dispose();
					}
					catch { }
				}

				foreach (Scene oldScene in bg.Scenes)
				{
					Scene scene = new Scene()
					{
						Background = image,
						Width = sceneWidth,
						Height = sceneHeight,
					};

					scene.X = oldScene.LegacyX;
					scene.Y = oldScene.LegacyY;
					float zoom;
					if (float.TryParse(oldScene.LegacyZoom.Substring(0, oldScene.LegacyZoom.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture, out zoom))
					{
						scene.Zoom = (zoom / 100).ToString(CultureInfo.InvariantCulture);
					}

					ending.Scenes.Add(scene);

					foreach (EndingSprite sprite in oldScene.LegacySprites)
					{
						Directive dir = new Directive("sprite");
						dir.Width = sprite.Width;
						dir.X = sprite.X;
						dir.Y = sprite.Y;
						dir.Src = sprite.Src;
						dir.Width = sprite.Width;
						dir.Css = sprite.Css;
						scene.Directives.Add(dir);
					}

					foreach (EndingText text in oldScene.LegacyText)
					{
						Directive dir = new Directive("text");
						dir.Width = text.Width;
						dir.X = text.X;
						dir.Y = text.Y;
						dir.Arrow = text.Arrow;
						dir.Text = text.Content;
						dir.Css = text.Css;
						scene.Directives.Add(dir);

						scene.Directives.Add(new Directive("pause"));
					}
					if (scene.Directives.Count == 0)
					{
						scene.Directives.Add(new Directive("pause"));
					}
				}
			}

			ending.Backgrounds.Clear();
			ending.Screens.Clear();
		}

		private static void Convert5_1(Character character)
		{
			//try to link up any old-style situations
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(character);
			foreach (Situation s in editorData.NoteworthySituations)
			{
				if (s.LegacyCase != null)
				{
					//find a matching case
					bool foundLink = false;
					foreach (Case workingCase in character.Behavior.GetWorkingCases())
					{
						if (workingCase.MatchesConditions(s.LegacyCase, false) && workingCase.MatchesStages(s.LegacyCase, true))
						{
							editorData.LinkSituation(s, workingCase);
							foundLink = true;
							break;
						}
					}
					if (!foundLink)
					{

					}
				}
			}
		}

		private static void Convert5_2(Character character)
		{
			HashSet<int> usedOneShots = new HashSet<int>();
			//de-duplicate oneshot IDs, since every case should be unique now
			foreach (Case workingCase in character.Behavior.GetWorkingCases())
			{
				if (workingCase.OneShotId > 0)
				{
					if (usedOneShots.Contains(workingCase.OneShotId))
					{
						workingCase.OneShotId = ++character.Behavior.MaxCaseId;
					}
					else
					{
						usedOneShots.Add(workingCase.OneShotId);
					}
				}
			}
		}

		private static void Convert5_2_7(Character character)
		{
			HashSet<int> usedOneShots = new HashSet<int>();
			//de-duplicate oneshot IDs on lines too
			foreach (Case workingCase in character.Behavior.GetWorkingCases())
			{
				foreach (DialogueLine line in workingCase.Lines)
				{
					if (line.OneShotId > 0)
					{
						if (usedOneShots.Contains(workingCase.OneShotId))
						{
							line.OneShotId = ++character.Behavior.MaxStateId;
						}
						else
						{
							usedOneShots.Add(workingCase.OneShotId);
						}
					}
				}
			}
		}

		/// <summary>
		/// 5.8 conversion: remove Nothing hands and convert old conditions to the 5.2 format
		/// </summary>
		/// <param name="character"></param>
		private static void Convert5_8(Character character)
		{
			int count = 0;
			foreach (Case wc in character.Behavior.GetWorkingCases())
			{
				if (wc.HasLegacyConditions())
				{
					ConvertCase5_2(wc);
					count++;
				}
				foreach (TargetCondition condition in wc.Conditions)
				{
					if (condition.Hand == "Nothing")
					{
						condition.Hand = "High Card";
					}
				}
				ConvertCase5_8(wc, character);
			}
			if (count > 0 && Shell.Instance != null)
			{
				Shell.Instance.SetStatus("Auto-converted conditions for " + count + " cases.");
			}
		}

		/// <summary>
		/// Converts a case to use TargetConditions where it previously used direct properties
		/// </summary>
		/// <param name="workingCase"></param>
		public static void ConvertCase5_2(Case workingCase)
		{
			if (workingCase == null || !workingCase.HasLegacyConditions()) { return; }
			int priority = workingCase.GetPriority();
			if (!string.IsNullOrEmpty(workingCase.Target) ||
				!string.IsNullOrEmpty(workingCase.TargetStage) ||
				!string.IsNullOrEmpty(workingCase.Filter) ||
				!string.IsNullOrEmpty(workingCase.TargetHand) ||
				!string.IsNullOrEmpty(workingCase.TargetLayers) ||
				!string.IsNullOrEmpty(workingCase.TargetStatus) ||
				!string.IsNullOrEmpty(workingCase.TargetSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.TargetNotSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.TargetSayingMarker) ||
				!string.IsNullOrEmpty(workingCase.TargetSaying) ||
				!string.IsNullOrEmpty(workingCase.TargetStartingLayers) ||
				!string.IsNullOrEmpty(workingCase.TargetTimeInStage))
			{
				TargetCondition cond = GetCondition(workingCase, "target", workingCase.Target);

				if (!string.IsNullOrEmpty(workingCase.Target))
				{
					cond.Character = workingCase.Target;
					workingCase.Target = null;
				}
				if (!string.IsNullOrEmpty(workingCase.Filter))
				{
					if (!string.IsNullOrEmpty(cond.FilterTag))
					{
						//already have a filter tag, need a new conditoin
						cond = new TargetCondition();
						cond.Role = "target";
						workingCase.Conditions.Add(cond);
					}
					cond.FilterTag = workingCase.Filter;
					workingCase.Filter = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetStage))
				{
					cond.Stage = workingCase.TargetStage;
					workingCase.TargetStage = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetHand))
				{
					cond.Hand = workingCase.TargetHand;
					workingCase.TargetHand = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetLayers))
				{
					cond.Layers = workingCase.TargetLayers;
					workingCase.TargetLayers = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetStatus))
				{
					cond.Status = workingCase.TargetStatus;
					workingCase.TargetStatus = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetSaidMarker))
				{
					cond.SaidMarker = workingCase.TargetSaidMarker;
					workingCase.TargetSaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetNotSaidMarker))
				{
					cond.NotSaidMarker = workingCase.TargetNotSaidMarker;
					workingCase.TargetNotSaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetSayingMarker))
				{
					cond.SayingMarker = workingCase.TargetSayingMarker;
					workingCase.TargetSayingMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetSaying))
				{
					cond.Saying = workingCase.TargetSaying;
					workingCase.TargetSaying = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetStartingLayers))
				{
					cond.StartingLayers = workingCase.TargetStartingLayers;
					workingCase.TargetStartingLayers = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TargetTimeInStage))
				{
					cond.TimeInStage = workingCase.TargetTimeInStage;
					workingCase.TargetTimeInStage = null;
				}
			}
			if (!string.IsNullOrEmpty(workingCase.ConsecutiveLosses))
			{
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(workingCase.Tag);
				if (trigger != null && trigger.HasTarget)
				{
					TargetCondition cond = GetCondition(workingCase, "target", null);
					cond.ConsecutiveLosses = workingCase.ConsecutiveLosses;
					workingCase.ConsecutiveLosses = null;
				}
				else
				{
					TargetCondition cond = GetCondition(workingCase, "self", null);
					cond.ConsecutiveLosses = workingCase.ConsecutiveLosses;
					workingCase.ConsecutiveLosses = null;
				}
			}

			if (!string.IsNullOrEmpty(workingCase.HasHand) ||
				!string.IsNullOrEmpty(workingCase.SaidMarker) ||
				!string.IsNullOrEmpty(workingCase.NotSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.TimeInStage))
			{
				TargetCondition cond = GetCondition(workingCase, "self", null);
				if (!string.IsNullOrEmpty(workingCase.HasHand))
				{
					cond.Hand = workingCase.HasHand;
					workingCase.HasHand = null;
				}
				if (!string.IsNullOrEmpty(workingCase.SaidMarker))
				{
					cond.SaidMarker = workingCase.SaidMarker;
					workingCase.SaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.NotSaidMarker))
				{
					cond.NotSaidMarker = workingCase.NotSaidMarker;
					workingCase.NotSaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.TimeInStage))
				{
					cond.TimeInStage = workingCase.TimeInStage;
					workingCase.TimeInStage = null;
				}
			}

			if (!string.IsNullOrEmpty(workingCase.AlsoPlaying) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingStage) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingHand) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingNotSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingSayingMarker) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingSaying) ||
				!string.IsNullOrEmpty(workingCase.AlsoPlayingTimeInStage))
			{
				TargetCondition cond = GetCondition(workingCase, "other", workingCase.AlsoPlaying);

				if (!string.IsNullOrEmpty(workingCase.AlsoPlaying))
				{
					cond.Character = workingCase.AlsoPlaying;
					workingCase.AlsoPlaying = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingStage))
				{
					cond.Stage = workingCase.AlsoPlayingStage;
					workingCase.AlsoPlayingStage = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingHand))
				{
					cond.Hand = workingCase.AlsoPlayingHand;
					workingCase.AlsoPlayingHand = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingSaidMarker))
				{
					cond.SaidMarker = workingCase.AlsoPlayingSaidMarker;
					workingCase.AlsoPlayingSaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingNotSaidMarker))
				{
					cond.NotSaidMarker = workingCase.AlsoPlayingNotSaidMarker;
					workingCase.AlsoPlayingNotSaidMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingSayingMarker))
				{
					cond.SayingMarker = workingCase.AlsoPlayingSayingMarker;
					workingCase.AlsoPlayingSayingMarker = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingSaying))
				{
					cond.Saying = workingCase.AlsoPlayingSaying;
					workingCase.AlsoPlayingSaying = null;
				}
				if (!string.IsNullOrEmpty(workingCase.AlsoPlayingTimeInStage))
				{
					cond.TimeInStage = workingCase.AlsoPlayingTimeInStage;
					workingCase.AlsoPlayingTimeInStage = null;
				}
			}

			if (!string.IsNullOrEmpty(workingCase.TotalFemales))
			{
				TargetCondition filter = new TargetCondition();
				filter.Gender = "female";
				filter.Count = workingCase.TotalFemales;
				workingCase.Conditions.Add(filter);
				workingCase.TotalFemales = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalMales))
			{
				TargetCondition filter = new TargetCondition();
				filter.Gender = "male";
				filter.Count = workingCase.TotalMales;
				workingCase.Conditions.Add(filter);
				workingCase.TotalMales = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalExposed))
			{
				TargetCondition filter = new TargetCondition();
				filter.Status = "exposed";
				filter.Count = workingCase.TotalExposed;
				workingCase.Conditions.Add(filter);
				workingCase.TotalExposed = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalNaked))
			{
				TargetCondition filter = new TargetCondition();
				filter.Status = "naked";
				filter.Count = workingCase.TotalNaked;
				workingCase.Conditions.Add(filter);
				workingCase.TotalNaked = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalMasturbating))
			{
				TargetCondition filter = new TargetCondition();
				filter.Status = "masturbating";
				filter.Count = workingCase.TotalMasturbating;
				workingCase.Conditions.Add(filter);
				workingCase.TotalMasturbating = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalFinished))
			{
				TargetCondition filter = new TargetCondition();
				filter.Status = "finished";
				filter.Count = workingCase.TotalFinished;
				workingCase.Conditions.Add(filter);
				workingCase.TotalFinished = null;
			}
			if (!string.IsNullOrEmpty(workingCase.TotalPlaying))
			{
				TargetCondition filter = new TargetCondition();
				filter.Status = "alive";
				filter.Count = workingCase.TotalPlaying;
				workingCase.Conditions.Add(filter);
				workingCase.TotalPlaying = null;
			}

			int newPriority = workingCase.GetPriority();
			if (priority != newPriority && string.IsNullOrEmpty(workingCase.CustomPriority))
			{
				workingCase.CustomPriority = newPriority.ToString();
			}
		}

		private static TargetCondition GetCondition(Case workingCase, string role, string character)
		{
			TargetCondition cond = workingCase.Conditions.Find(c => c.Role == role && (c.Character == character || string.IsNullOrEmpty(character)) && string.IsNullOrEmpty(c.Count));
			if (cond == null)
			{
				cond = new TargetCondition();
				cond.Role = role;
				cond.Character = character;
				workingCase.Conditions.Add(cond);
			}
			return cond;
		}

		/// <summary>
		/// 5.8 conversion from line-level persistent marker to character-wide definitions
		/// </summary>
		/// <param name="workingCase"></param>
		public static void ConvertCase5_8(Case workingCase, Character character)
		{
			foreach (DialogueLine line in workingCase.Lines)
			{
				if (!string.IsNullOrEmpty(line.Marker) && line.IsMarkerPersistent)
				{
					MarkerOperator op;
					bool perTarget;
					string value;
					string name = Marker.ExtractConditionPieces(line.Marker, out op, out value, out perTarget);
					character.Behavior.PersistentMarkers.Add(name);
					line.IsMarkerPersistent = false;
				}
			}
		}

		/// <summary>
		/// 6.1 conversion: convert conditions using tag checks for a character into checking the character directly
		/// </summary>
		/// <param name="character"></param>
		private static void Convert6_1(Character character)
		{
			foreach (Case wc in character.Behavior.GetWorkingCases())
			{
				ConvertCase6_1(wc, character);
			}
		}

		private static void ConvertCase6_1(Case workingCase, Character character)
		{
			for (int i = 0; i < workingCase.Conditions.Count; i++)
			{
				TargetCondition condition = workingCase.Conditions[i];
				if (!string.IsNullOrEmpty(condition.FilterTag))
				{
					if (CharacterDatabase.Get(condition.FilterTag) != null && string.IsNullOrEmpty(condition.Role) && string.IsNullOrEmpty(condition.Character))
					{
						condition.Role = "opp";
						condition.Character = condition.FilterTag;
						condition.FilterTag = null;
						if (condition.Count == "1")
						{
							condition.Count = null;
						}
					}
				}
			}

			foreach (Case alternate in workingCase.AlternativeConditions)
			{
				ConvertCase6_1(alternate, character);
			}
		}
	}
}
