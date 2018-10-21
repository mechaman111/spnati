using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor
{
	public static class CharacterValidator
	{
		/// <summary>
		/// Validates the character's dialogue and returns a list of warnings (bad images, targets, etc.)
		/// </summary>
		/// <returns></returns>
		public static bool Validate(Character character, Listing listing, out List<ValidationError> warnings)
		{
			warnings = new List<ValidationError>();
			string[] hands = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight",
				"Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush"};
			HashSet<string> validHands = new HashSet<string>();
			foreach (string hand in hands)
			{
				validHands.Add(hand.ToLowerInvariant());
			}

			HashSet<string> unusedImages = new HashSet<string>();
			string folder = Config.GetRootDirectory(character);
			foreach (string filename in Directory.EnumerateFiles(folder))
			{
				string ext = Path.GetExtension(filename).ToLower();
				if (ext.EndsWith(".png") || ext.EndsWith(".gif"))
				{
					unusedImages.Add(Path.GetFileName(filename));
				}
			}
			unusedImages.Remove(character.Metadata.Portrait);

			Regex targetRange = new Regex(@"^\d+(-\d+)?$");

			int layers = character.Layers + Clothing.ExtraStages;
			if (character.Behavior.Stages.Count > layers)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, string.Format("There are too many stages. Expected amount based on clothing: {0}, Actual: {1}", layers, character.Behavior.Stages.Count)));
			}

			foreach (var ai in character.Intelligence)
			{
				int stage = ai.Stage;
				if (stage < 0 || stage >= layers)
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, string.Format("Intelligence level starting at stage {0}, but character has no stage {0}", stage)));
				}
			}


			foreach (var line in character.StartingLines)
			{
				string img = line.Image;
				unusedImages.Remove(img);
				//TODO: This isn't working correctly at the moment
				//if (!File.Exists(Path.Combine(Config.GetRootDirectory(character), img)))
				//{
				//	warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("{1} does not exist. {0}", "start", img)));
				//}
			}

			foreach (Stage stage in character.Behavior.Stages)
			{
				foreach (Case stageCase in stage.Cases)
				{
					Trigger trigger = TriggerDatabase.GetTrigger(stageCase.Tag);
					if (trigger == null || trigger.Unrecognized)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is an unknown case. (stage {1})", stageCase.Tag, stage.Id)));
						continue;
					}

					if (!TriggerDatabase.UsedInStage(stageCase.Tag, character, stage.Id))
					{
						#region hardcoded exclusions
						if (stageCase.Tag == "stripped" && stage.Id == 0 || stageCase.Tag == "game_over_victory" && (stage.Id == character.Behavior.Stages.Count - 1 || stage.Id == character.Behavior.Stages.Count - 2))
						{
							//Pretend these cases don't even exist. make_xml.py mistakenly generates them even though the game will never use them
							continue;
						}
						#endregion

						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is invalid for stage {1}", stageCase.Tag, stage.Id)));
						continue;
					}
					bool hasTarget = trigger.HasTarget;
					string caseLabel = string.Format("(Stage {0}, {1})", stage.Id, stageCase.Tag);

					#region Target
					if (!string.IsNullOrEmpty(stageCase.Target))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"target\" is not allowed for case {0}", caseLabel)));
						}
						Character target = CharacterDatabase.Get(stageCase.Target);
						if (target == null)
						{
							if (stageCase.Target != "human")
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" does not exist. {0}", caseLabel, stageCase.Target)));
							}
						}
						else
						{
							OpponentStatus status = listing.GetCharacterStatus(target.FolderName);
							if (status == OpponentStatus.Incomplete)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is flagged as incomplete. {0}", caseLabel, stageCase.Target)));
							}
							else if (status == OpponentStatus.Offline)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is offline only. {0}", caseLabel, stageCase.Target)));
							}

							if (!string.IsNullOrEmpty(trigger.Gender) && target.Gender != trigger.Gender)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" is {2}, so this case will never trigger. {0}", caseLabel, stageCase.Target, target.Gender)));
							}
							if (!string.IsNullOrEmpty(trigger.Size) && target.Size != trigger.Size)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" has a size of {2}, so this case will never trigger. {0}", caseLabel, stageCase.Target, target.Size)));
							}
							if (!string.IsNullOrEmpty(stageCase.TargetStage))
							{
								int targetStage;
								if (int.TryParse(stageCase.TargetStage, out targetStage))
								{
									if (target.Layers + Clothing.ExtraStages <= targetStage)
									{
										warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" does not have {2} stages. {0}", caseLabel, stageCase.Target, stageCase.TargetStage)));
									}
									Clothing clothing;
									if (!ValidateStageWithTag(target, targetStage, stageCase.Tag, out clothing))
									{
										if (clothing == null)
											warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("using the first stage as a target stage for a removed_item case. Removed cases should use the stage following the removing stage. {0}", caseLabel)));
										else warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("targeting \"{1}\" at stage {2} ({3}), which will never happen because {3} is of type {4}. {0}", caseLabel, target, targetStage, clothing.Lowercase, clothing.Type)));
									}
								}
							}
							ValidateMarker(warnings, target, caseLabel, "targetSeenMarker", stageCase.TargetSaidMarker, stageCase.TargetStage);
							ValidateMarker(warnings, target, caseLabel, "targetNotSeenMarker", stageCase.TargetNotSaidMarker);
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TargetStage))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"targetStage\" is not allowed for case {0}", caseLabel)));
						}
						if (!targetRange.IsMatch(stageCase.TargetStage))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("targetStage \"{1}\" should be numeric or a range. {0}", caseLabel, stageCase.TargetStage)));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TargetHand))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"oppHand\" is not allowed for case {0}", caseLabel)));
						}
						if (!validHands.Contains(stageCase.TargetHand.ToLowerInvariant()))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("oppHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.TargetHand)));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.Filter))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"filter\" is not allowed for case {0}", caseLabel)));
						}
						if (!TagDatabase.TagExists(stageCase.Filter))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("No character has the tag \"{1}\". {0}", caseLabel, stageCase.Filter)));
						}
					}
					#endregion

					#region Also Playing
					Character other = CharacterDatabase.Get(stageCase.AlsoPlaying);
					if (!string.IsNullOrEmpty(stageCase.AlsoPlaying))
					{
						if (other == null)
						{
							if (stageCase.AlsoPlaying != "human")
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not exist. {0}", caseLabel, stageCase.AlsoPlaying)));
							}
						}
						else
						{
							OpponentStatus status = listing.GetCharacterStatus(other.FolderName);
							if (status == OpponentStatus.Incomplete)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("alsoPlaying target \"{1}\" is flagged as incomplete. {0}", caseLabel, stageCase.AlsoPlaying)));
							}
							else if (status == OpponentStatus.Offline)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("alsoPlaying target \"{1}\" is offline only. {0}", caseLabel, stageCase.AlsoPlaying)));
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingHand))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand must be accompanied with alsoPlaying. {0}", caseLabel)));
						}
						if (!validHands.Contains(stageCase.AlsoPlayingHand.ToLowerInvariant()))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.AlsoPlayingHand)));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingStage))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingStage must be accompanied with alsoPlaying. {0}", caseLabel)));
						}
						int alsoPlayingStage;
						if (!int.TryParse(stageCase.AlsoPlayingStage, out alsoPlayingStage))
						{
							if (!targetRange.IsMatch(stageCase.AlsoPlayingStage))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingStage \"{1}\" should be numeric or a range. {0}", caseLabel, stageCase.AlsoPlayingStage)));
							}
						}
						else
						{
							if (other != null)
							{
								if (other.Layers + Clothing.ExtraStages <= alsoPlayingStage)
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not have {2} stages. {0}", caseLabel, stageCase.AlsoPlaying, stageCase.AlsoPlayingStage)));
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingTimeInStage))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingTimeInStage must be accompanied with alsoPlaying. {0}", caseLabel)));
						}
						warnings.AddRange(ValidateRangeField(stageCase.AlsoPlayingTimeInStage, "alsoPlayingTimeInStage", caseLabel, -1, -1));
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSaidMarker))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand must be accompanied with alsoPlaying. {0}", caseLabel)));
						}
						else ValidateMarker(warnings, other, caseLabel, "alsoPlayingSeenMarker", stageCase.AlsoPlayingSaidMarker, stageCase.AlsoPlayingStage);
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingNotSaidMarker))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand must be accompanied with alsoPlaying. {0}", caseLabel)));
						}
						else ValidateMarker(warnings, other, caseLabel, "alsoPlayingNotSeenMarker", stageCase.AlsoPlayingNotSaidMarker);
					}
					#endregion

					#region Misc
					if (!string.IsNullOrEmpty(stageCase.HasHand) && !validHands.Contains(stageCase.HasHand.ToLowerInvariant()))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("hasHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.HasHand)));
					}

					warnings.AddRange(ValidateRangeField(stageCase.TotalFemales, "totalFemales", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalMales, "totalMales", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalRounds, "totalRounds", caseLabel, -1, -1));
					warnings.AddRange(ValidateRangeField(stageCase.TotalPlaying, "totalAlive", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalExposed, "totalExposed", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalNaked, "totalNaked", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalFinishing, "totalMasturbating", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.TotalFinished, "totalFinished", caseLabel, 0, 5));
					warnings.AddRange(ValidateRangeField(stageCase.ConsecutiveLosses, "consecutiveLosses", caseLabel, -1, -1));
					warnings.AddRange(ValidateRangeField(stageCase.TargetTimeInStage, "targetTimeInStage", caseLabel, -1, -1));
					warnings.AddRange(ValidateRangeField(stageCase.TimeInStage, "timeInStage", caseLabel, -1, -1));
					if (!string.IsNullOrEmpty(stageCase.CustomPriority))
					{
						int priority;
						if (!int.TryParse(stageCase.CustomPriority, out priority))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("customPriority {1} must be numeric. {0}", caseLabel, stageCase.CustomPriority)));
						}
					}
					ValidateMarker(warnings, character, caseLabel, "seenMarker", stageCase.SaidMarker);
					ValidateMarker(warnings, character, caseLabel, "notSeenMarker", stageCase.NotSaidMarker);

					#endregion

					#region Filters
					foreach (var condition in stageCase.Conditions)
					{
						warnings.AddRange(ValidateRangeField(condition.Count, string.Format("\"{0}\" tag count", condition.Filter), caseLabel, 0, 5));
						if (!TagDatabase.TagExists(condition.Filter))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("Filtering on tag \"{1}\" which is not used by any characters. {0}", caseLabel, condition.Filter)));
						}
					}
					#endregion

					Tuple<string, string> template = DialogueDatabase.GetTemplate(stageCase.Tag);
					string defaultLine = template.Item2;
					foreach (DialogueLine line in stageCase.Lines)
					{
						//Validate image
						string img = line.Image;
						unusedImages.Remove(img);
						if (!File.Exists(Path.Combine(Config.GetRootDirectory(character), img)))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("{1} does not exist. {0}", caseLabel, img)));
						}

						//Validate variables
						List<string> invalidVars = DialogueLine.GetInvalidVariables(stageCase.Tag, line.Text);
						if (invalidVars.Count > 0)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Variables, string.Format("Invalid variables for case {0}: {1}", caseLabel, string.Join(",", invalidVars))));
						}

						//Make sure it's not a placeholder
						if (defaultLine.Equals(line.Text))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case is still using placeholder text: {0}", caseLabel)));
						}
					}
				}
			}

			//endings
			foreach (Epilogue ending in character.Endings)
			{
				foreach (Screen screen in ending.Screens)
				{
					unusedImages.Remove(screen.Image);
				}
			}

			if (unusedImages.Count > 0)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("The following images are never used: {0}", string.Join(", ", unusedImages))));
			}

			if (warnings.Count == 0)
				return true;

			return false;
		}

		/// <summary>
		/// Gets whether a validation level is part of the filters (i.e. should NOT be excluded)
		/// </summary>
		/// <param name="filters"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public static bool IsInFilter(ValidationFilterLevel filters, ValidationFilterLevel level)
		{
			return (filters & level) > 0;
		}

		private static IEnumerable<ValidationError> ValidateRangeField(string value, string fieldName, string caseLabel, int min, int max)
		{
			int lower = 0, upper;
			if (!string.IsNullOrEmpty(value))
			{
				string[] pieces = value.Split('-');
				if (pieces.Length > 2)
				{
					yield return new ValidationError(ValidationFilterLevel.Case, string.Format("{2} \"{1}\" must be numeric or a range. {0}", caseLabel, value, fieldName));
					yield break;
				}
				string minStr = pieces[0];
				if (minStr != "") {
					if (!int.TryParse(minStr, out lower))
					{
						yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" must be numeric or empty. {0}", caseLabel, value, fieldName));
					}
					else if ((min >= 0 && lower < min) || (max >= 0 && lower > max))
					{
						yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" should be between {3} and {4}. {0}", caseLabel, value, fieldName, min, max));
					}
				}

				if (pieces.Length > 1)
				{
					string maxStr = pieces[1];
					if (maxStr != "") {
						if (!int.TryParse(maxStr, out upper))
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Upper bound of {2} \"{1}\" must be numeric or empty. {0}", caseLabel, value, fieldName));
						}
						else if (lower > upper)
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" must not be greater than upper bound. {0}", caseLabel, value, fieldName, min, max));
						}
						else if ((min >= 0 && upper < min) || (max >= 0 && upper > max))
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Upper bound of {2} \"{1}\" should be between {3} and {4}. {0}", caseLabel, value, fieldName, min, max));
						}
						
					}
				}
			}
		}

		private static void ValidateMarker(List<ValidationError> warnings, Character character, string caseLabel, string label, string value)
		{
			ValidateMarker(warnings, character, caseLabel, label, value, "");
		}

		private static void ValidateMarker(List<ValidationError> warnings, Character character, string caseLabel, string label, string value, string stageRange)
		{
			if (string.IsNullOrEmpty(value))
				return;

			if (character == null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Missing character for {1}. {0}", caseLabel, value)));
			}
			else
			{
				if (!character.Markers.Contains(value))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("{1} has no dialogue that sets marker {2}. {0}", caseLabel, character.FolderName, value)));
				}
				else
				{
					if (!string.IsNullOrEmpty(stageRange))
					{
						//verify that a marker can even be set prior to this point

						int min, max;
						Case.ToRange(stageRange, out min, out max);
						for (int i = 0; i <= min && i < character.Behavior.Stages.Count; i++)
						{
							Stage stage = character.Behavior.Stages[i];
							foreach (var c in stage.Cases)
							{
								foreach (var line in c.Lines)
								{
									if (line.Marker == value)
									{
										return;
									}
								}
							}
						}
						warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("{1} has no dialogue prior to stage {2} that sets marker {3}, so this case will never trigger. {0}", caseLabel, character.FolderName, min, value)));
					}
				}
			}
		}

		/// <summary>
		/// Verifies that a stage works in conjuction with a tag
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		private static bool ValidateStageWithTag(Character character, int stage, string tag, out Clothing clothing)
		{
			clothing = null;
			if (stage < 0 || stage >= character.Layers)
				return true; //Not valid, but this problem gets picked up in a different validation
			bool removing = tag.Contains("removing");
			bool removed = tag.Contains("removed");
			if (removing || removed)
			{
				int index = tag.LastIndexOf('_');
				if (index >= 0 && index < tag.Length - 1)
				{
					if (removed)
					{
						stage--; //removed is in the next stage so back up one to get the stage of the item removed
					}
					if (stage < 0)
						return false;
					string type = tag.Substring(index + 1);
					if (type == "accessory")
						type = "extra";
					clothing = character.Wardrobe[character.Layers - stage - 1];
					string realType = clothing.Type;
					if (type != realType.ToLower())
					{
						return false;
					}
				}
			}
			return true;
		}
	}

	public class ValidationError
	{
		public ValidationFilterLevel Level;
		public string Text;

		public ValidationError(ValidationFilterLevel level, string text)
		{
			Level = level;
			Text = text;
		}

		public override string ToString()
		{
			return Text;
		}
	}

	[Flags]
	public enum ValidationFilterLevel
	{
		None = 0,
		Minor = 1,
		MissingImages = 2,
		Metadata = 4,
		Variables = 8,
		TargetedDialogue = 16,
		Case = 32,
		Stage = 64
	}
}
