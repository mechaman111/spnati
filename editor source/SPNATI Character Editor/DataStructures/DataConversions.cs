using System;
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
				TargetCondition cond = GetCondition(workingCase, "target");

				if (!string.IsNullOrEmpty(workingCase.Target))
				{
					cond.Character = workingCase.Target;
					workingCase.Target = null;
				}
				if (!string.IsNullOrEmpty(workingCase.Filter))
				{
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
					TargetCondition cond = GetCondition(workingCase, "target");
					cond.ConsecutiveLosses = workingCase.ConsecutiveLosses;
					workingCase.ConsecutiveLosses = null;
				}
				else
				{
					TargetCondition cond = GetCondition(workingCase, "self");
					cond.ConsecutiveLosses = workingCase.ConsecutiveLosses;
					workingCase.ConsecutiveLosses = null;
				}
			}

			if (!string.IsNullOrEmpty(workingCase.HasHand) ||
				!string.IsNullOrEmpty(workingCase.SaidMarker) ||
				!string.IsNullOrEmpty(workingCase.NotSaidMarker) ||
				!string.IsNullOrEmpty(workingCase.TimeInStage))
			{
				TargetCondition cond = GetCondition(workingCase, "self");
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
				TargetCondition cond = GetCondition(workingCase, "other");

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

		private static TargetCondition GetCondition(Case workingCase, string role)
		{
			TargetCondition cond = workingCase.Conditions.Find(c => c.Role == role);
			if (cond == null)
			{
				cond = new TargetCondition();
				cond.Role = role;
				workingCase.Conditions.Add(cond);
			}
			return cond;
		}
	}
}
