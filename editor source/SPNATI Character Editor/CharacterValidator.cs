﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using SPNATI_Character_Editor.Controls;

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

			Dictionary<int, HashSet<string>> usedPoses = new Dictionary<int, HashSet<string>>();
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

			//wardrobe
			string upper = null;
			string lower = null;
			string importantUpper = null;
			string importantLower = null;
			bool foundBoth = false;
			string otherMajor = null;
			for (int i = 0; i < character.Layers; i++)
			{
				Clothing c = character.GetClothing(i);
				if (c.Position == "upper" && c.Type == "major")
					upper = c.GenericName;
				if (c.Position == "lower" && c.Type == "major")
					lower = c.GenericName;
				if (c.Position == "lower" && c.Type == "major")
					foundBoth = true;
				if (c.Position == "upper" && c.Type == "important")
					importantUpper = c.GenericName;
				if (c.Position == "lower" && c.Type == "important")
					importantLower = c.GenericName;
				if (c.Position == "other" && c.Type == "major")
					otherMajor = c.GenericName;
			}
			if (!foundBoth)
			{
				if (string.IsNullOrEmpty(upper))
				{
					if (!string.IsNullOrEmpty(importantUpper))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"{importantUpper} has no major article covering it. Either an article{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantUpper} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Character has no clothing of type: major, position: upper. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}					
				}
				if (string.IsNullOrEmpty(lower))
				{
					if (!string.IsNullOrEmpty(importantLower))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"{importantLower} has no major article covering it. Either an article{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantLower} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Character has no clothing of type: major, position: upper. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}
				}
			}

			//dialogue
			foreach (Stage stage in character.Behavior.Stages)
			{
				HashSet<string> stageImages = usedPoses.GetOrAddDefault(stage.Id, () => new HashSet<string>());

				foreach (Case stageCase in stage.Cases)
				{
					ValidationContext context = new ValidationContext(stage, stageCase, null);

					Trigger trigger = TriggerDatabase.GetTrigger(stageCase.Tag);
					if (trigger == null || trigger.Unrecognized)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is an unknown case. (stage {1})", stageCase.Tag, stage.Id), context));
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

						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is invalid for stage {1}", stageCase.Tag, stage.Id), context));
						continue;
					}
					string caseLabel = string.Format("(Stage {0}, {1})", stage.Id, stageCase.Tag);

					#region Target
					if (!string.IsNullOrEmpty(stageCase.Target))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"target\" is not allowed for case {0}", caseLabel), context));
						}
						Character target = CharacterDatabase.Get(stageCase.Target);
						if (target == null)
						{
							if (stageCase.Target != "human")
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" does not exist. {0}", caseLabel, stageCase.Target), context));
							}
						}
						else
						{
							OpponentStatus status = listing.GetCharacterStatus(target.FolderName);
							if (status == OpponentStatus.Incomplete)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is flagged as incomplete. {0}", caseLabel, stageCase.Target), context));
							}
							else if (status == OpponentStatus.Offline)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is offline only. {0}", caseLabel, stageCase.Target), context));
							}

							if (target.FolderName != "human")
							{
								if (!string.IsNullOrEmpty(trigger.Gender) && target.Gender != trigger.Gender)
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" is {2}, so this case will never trigger. {0}", caseLabel, stageCase.Target, target.Gender), context));
								}
								if (!string.IsNullOrEmpty(trigger.Size) && target.Size != trigger.Size)
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" has a size of {2}, so this case will never trigger. {0}", caseLabel, stageCase.Target, target.Size), context));
								}
								if (!string.IsNullOrEmpty(stageCase.TargetStage))
								{
									int targetStage;
									if (int.TryParse(stageCase.TargetStage, out targetStage))
									{
										if (target.Layers + Clothing.ExtraStages <= targetStage)
										{
											warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" does not have {2} stages. {0}", caseLabel, stageCase.Target, stageCase.TargetStage), context));
										}
										Clothing clothing;
										if (!ValidateStageWithTag(target, targetStage, stageCase.Tag, out clothing))
										{
											if (clothing == null)
												warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("using the first stage as a target stage for a removed_item case. Removed cases should use the stage following the removing stage. {0}", caseLabel), context));
											else warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("targeting \"{1}\" at stage {2} ({3}), which will never happen because {3} is of type {4}. {0}", caseLabel, target, targetStage, clothing.GenericName, clothing.Type), context));
										}
									}
								}
							}
							ValidateMarker(warnings, target, caseLabel, stageCase.TargetSaidMarker, stageCase.TargetStage, context);
							ValidateMarker(warnings, target, caseLabel, stageCase.TargetSayingMarker, stageCase.TargetStage, context);
							ValidateMarker(warnings, target, caseLabel, stageCase.TargetNotSaidMarker, context);
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TargetStage))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"targetStage\" is not allowed for case {0}", caseLabel), context));
						}
						if (!targetRange.IsMatch(stageCase.TargetStage))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("targetStage \"{1}\" should be numeric or a range. {0}", caseLabel, stageCase.TargetStage), context));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TargetHand))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"oppHand\" is not allowed for case {0}", caseLabel), context));
						}
						if (!validHands.Contains(stageCase.TargetHand.ToLowerInvariant()))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("oppHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.TargetHand), context));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.Filter))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"filter\" is not allowed for case {0}", caseLabel), context));
						}
						if (!TagDatabase.TagExists(stageCase.Filter))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("No character has the tag \"{1}\". {0}", caseLabel, stageCase.Filter), context));
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
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not exist. {0}", caseLabel, stageCase.AlsoPlaying), context));
							}
						}
						else
						{
							OpponentStatus status = listing.GetCharacterStatus(other.FolderName);
							if (status == OpponentStatus.Incomplete)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("alsoPlaying target \"{1}\" is flagged as incomplete. {0}", caseLabel, stageCase.AlsoPlaying), context));
							}
							else if (status == OpponentStatus.Offline)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("alsoPlaying target \"{1}\" is offline only. {0}", caseLabel, stageCase.AlsoPlaying), context));
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingHand))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						if (!validHands.Contains(stageCase.AlsoPlayingHand.ToLowerInvariant()))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.AlsoPlayingHand), context));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingStage))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingStage must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						int alsoPlayingStage;
						if (!int.TryParse(stageCase.AlsoPlayingStage, out alsoPlayingStage))
						{
							if (!targetRange.IsMatch(stageCase.AlsoPlayingStage))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingStage \"{1}\" should be numeric or a range. {0}", caseLabel, stageCase.AlsoPlayingStage), context));
							}
						}
						else
						{
							if (other != null)
							{
								if (other.Layers + Clothing.ExtraStages <= alsoPlayingStage && other.FolderName != "human")
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not have {2} stages. {0}", caseLabel, stageCase.AlsoPlaying, stageCase.AlsoPlayingStage), context));
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingTimeInStage))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingTimeInStage must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						warnings.AddRange(ValidateRangeField(stageCase.AlsoPlayingTimeInStage, "alsoPlayingTimeInStage", caseLabel, -1, -1, context));
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSaidMarker))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingSaidMarker must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						else ValidateMarker(warnings, other, caseLabel, stageCase.AlsoPlayingSaidMarker, stageCase.AlsoPlayingStage, context);
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSayingMarker))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingSayingMarker must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						else ValidateMarker(warnings, other, caseLabel, stageCase.AlsoPlayingSayingMarker, stageCase.AlsoPlayingStage, context);
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlayingNotSaidMarker))
					{
						if (string.IsNullOrEmpty(stageCase.AlsoPlaying))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingHand must be accompanied with alsoPlaying. {0}", caseLabel), context));
						}
						else ValidateMarker(warnings, other, caseLabel, stageCase.AlsoPlayingNotSaidMarker, context);
					}
					#endregion

					#region Misc
					if (!string.IsNullOrEmpty(stageCase.HasHand) && !validHands.Contains(stageCase.HasHand.ToLowerInvariant()))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("hasHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.HasHand)));
					}

					warnings.AddRange(ValidateRangeField(stageCase.TotalFemales, "totalFemales", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalMales, "totalMales", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalRounds, "totalRounds", caseLabel, -1, -1, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalPlaying, "totalAlive", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalExposed, "totalExposed", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalNaked, "totalNaked", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalMasturbating, "totalMasturbating", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.TotalFinished, "totalFinished", caseLabel, 0, 5, context));
					warnings.AddRange(ValidateRangeField(stageCase.ConsecutiveLosses, "consecutiveLosses", caseLabel, -1, -1, context));
					warnings.AddRange(ValidateRangeField(stageCase.TargetTimeInStage, "targetTimeInStage", caseLabel, -1, -1, context));
					warnings.AddRange(ValidateRangeField(stageCase.TimeInStage, "timeInStage", caseLabel, -1, -1, context));
					if (!string.IsNullOrEmpty(stageCase.CustomPriority))
					{
						int priority;
						if (!int.TryParse(stageCase.CustomPriority, out priority))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("customPriority {1} must be numeric. {0}", caseLabel, stageCase.CustomPriority)));
						}
					}
					ValidateMarker(warnings, character, caseLabel, stageCase.SaidMarker, context);
					ValidateMarker(warnings, character, caseLabel, stageCase.NotSaidMarker, context);

					#endregion

					#region Filters
					foreach (var condition in stageCase.Conditions)
					{
						warnings.AddRange(ValidateRangeField(condition.Count, string.Format("\"{0}\" tag count", condition.Filter), caseLabel, 0, 5, context));
						if (condition.Filter != "human" && condition.Filter != "human_male" && condition.Filter != "human_female" && !string.IsNullOrEmpty(condition.Filter) && !TagDatabase.TagExists(condition.Filter))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("Filtering on tag \"{1}\" which is not used by any characters. {0}", caseLabel, condition.Filter), context));
						}
					}
					#endregion

					Tuple<string, string> template = DialogueDatabase.GetTemplate(stageCase.Tag);
					string defaultLine = template.Item2;
					Regex regex = new Regex(@"\<\/i\>");

					foreach (DialogueLine line in stageCase.Lines)
					{
						context = new ValidationContext(stage, stageCase, line);

						//Validate image
						string img = line.Image;
						if (!string.IsNullOrEmpty(img))
						{
							unusedImages.Remove(img);
							if (!File.Exists(Path.Combine(Config.GetRootDirectory(character), img)))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("{1} does not exist. {0}", caseLabel, img), context));
							}
							stageImages.Add(img);	
						}

						//Validate variables
						List<string> invalidVars = DialogueLine.GetInvalidVariables(stageCase.Tag, line.Text);
						if (invalidVars.Count > 0)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Invalid variables for case {0}: {1}", caseLabel, string.Join(",", invalidVars)), context));
						}

						//Make sure it's not a placeholder
						if (defaultLine.Equals(line.Text))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case is still using placeholder text: {0}", caseLabel), context));
						}

						//check for mismatched italics
						string[] pieces = line.Text.ToLower().Split(new string[] { "<i>" }, StringSplitOptions.None);
						int count = 0;
						for (int i = 0; i < pieces.Length; i++)
						{
							if (i > 0)
							{
								count++;
							}
							count -= regex.Matches(pieces[i]).Count;
						}
						if (count != 0)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, $"Line has mismatched <i> </i> tags: {line.Text}", context));
						}
					}
				}
			}

			//endings
			foreach (Epilogue ending in character.Endings)
			{
				ValidateEpilogue(ending, warnings, unusedImages);
			}

			if (unusedImages.Count > 0)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("The following images are never used: {0}", string.Join(", ", unusedImages))));
			}

			foreach (AlternateSkin alt in character.Metadata.AlternateSkins)
			{
				foreach (SkinLink link in alt.Skins)
				{
					ValidateSkin(character, link, warnings, usedPoses);
				}
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

		private static IEnumerable<ValidationError> ValidateRangeField(string value, string fieldName, string caseLabel, int min, int max, ValidationContext context)
		{
			int lower = 0, upper;
			if (!string.IsNullOrEmpty(value))
			{
				string[] pieces = value.Split('-');
				if (pieces.Length > 2)
				{
					yield return new ValidationError(ValidationFilterLevel.Case, string.Format("{2} \"{1}\" must be numeric or a range. {0}", caseLabel, value, fieldName), context);
					yield break;
				}
				string minStr = pieces[0];
				if (minStr != "") {
					if (!int.TryParse(minStr, out lower))
					{
						yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" must be numeric or empty. {0}", caseLabel, value, fieldName), context);
					}
					else if ((min >= 0 && lower < min) || (max >= 0 && lower > max))
					{
						yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" should be between {3} and {4}. {0}", caseLabel, value, fieldName, min, max), context);
					}
				}

				if (pieces.Length > 1)
				{
					string maxStr = pieces[1];
					if (maxStr != "") {
						if (!int.TryParse(maxStr, out upper))
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Upper bound of {2} \"{1}\" must be numeric or empty. {0}", caseLabel, value, fieldName), context);
						}
						else if (lower > upper)
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Lower bound of {2} \"{1}\" must not be greater than upper bound. {0}", caseLabel, value, fieldName, min, max), context);
						}
						else if ((min >= 0 && upper < min) || (max >= 0 && upper > max))
						{
							yield return new ValidationError(ValidationFilterLevel.Case, string.Format("Upper bound of {2} \"{1}\" should be between {3} and {4}. {0}", caseLabel, value, fieldName, min, max), context);
						}
						
					}
				}
			}
		}

		private static void ValidateMarker(List<ValidationError> warnings, Character character, string caseLabel, string value, ValidationContext context)
		{
			ValidateMarker(warnings, character, caseLabel, value, "", context);
		}

		private static void ValidateMarker(List<ValidationError> warnings, Character character, string caseLabel, string name, string stageRange, ValidationContext context)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (character == null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Missing character for {1}. {0}", caseLabel, name), context));
			}
			else
			{
				string value;
				MarkerOperator op;
				bool perTarget;
				name = Marker.ExtractConditionPieces(name, out op, out value, out perTarget);
				if (!character.Markers.Contains(name))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("{1} has no dialogue that sets marker {2}. {0}", caseLabel, character.FolderName, name), context));
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
									if (line.Marker == name)
									{
										return;
									}
								}
							}
						}
						warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("{1} has no dialogue prior to stage {2} that sets marker {3}, so this case will never trigger. {0}", caseLabel, character.FolderName, min, name), context));
					}

					if (!string.IsNullOrEmpty(value))
					{
						bool used = false;
						Marker m = character.Markers.Values.FirstOrDefault(marker => marker.Name == name);
						if (m != null)
						{
							used = m.Values.Contains(value);
							if (!used)
							{
								int test;
								//they never set the value directly, but if it's numeric, then they might be able to increment or decrement to it
								if (int.TryParse(value, out test))
								{
									used = (m.Values.Contains("+") || m.Values.Contains("-"));
								}
							}
						}
						if (!used)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, $"{character.FolderName} has no dialogue that sets marker {name} to {value}, so this case will never trigger. {caseLabel}", context));
						}
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

		private static void ValidateEpilogue(Epilogue ending, List<ValidationError> warnings, HashSet<string> unusedImages)
		{
			if (string.IsNullOrEmpty(ending.Hint) && ending.HasSpecialConditions)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has special unlock conditions. Consider adding a hint to help the player know how to unlock it.", new ValidationContext(ending, null, null)));
			}

			if (string.IsNullOrEmpty(ending.GalleryImage))
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has no gallery image and will appear blank in the epilogue gallery.", new ValidationContext(ending, null, null)));
			}
			else
			{
				unusedImages.Remove(ending.GalleryImage);
			}

			foreach (Scene scene in ending.Scenes)
			{
				HashSet<string> usedSpriteIds = new HashSet<string>();
				if (!string.IsNullOrEmpty(scene.Background))
				{
					unusedImages.Remove(scene.Background);
				}

				foreach (Directive directive in scene.Directives)
				{
					if (directive.DirectiveType == "sprite")
					{
						string id = directive.Id;
						if (string.IsNullOrEmpty(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a sprite directive ({directive}) with no ID.", new ValidationContext(ending, scene, directive)));
						}
						else if (usedSpriteIds.Contains(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} uses the ID \"{id}\" for more than one sprite in the same scene.", new ValidationContext(ending, scene, directive)));
						}
						else
						{
							usedSpriteIds.Add(id);
						}

						if (!string.IsNullOrEmpty(directive.Src))
						{
							unusedImages.Remove(directive.Src);
						}
					}
					else if (directive.DirectiveType == "move")
					{
						string id = directive.Id;
						if (string.IsNullOrEmpty(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a move directive ({directive}) with no ID.", new ValidationContext(ending, scene, directive)));
						}
						else if (!usedSpriteIds.Contains(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a move directive with ID \"{id}\" which does not correspond to any sprite in that scene.", new ValidationContext(ending, scene, directive)));
						}
					}
				}
			}
		}

		/// <summary>
		/// Validates an alternative skin
		/// </summary>
		/// <param name="character">Character</param>
		/// <param name="link">Link to reskin</param>
		/// <param name="warnings">All current warnings</param>
		/// <param name="baseImages">List of images used per stage</param>
		private static void ValidateSkin(Character character, SkinLink link, List<ValidationError> warnings, Dictionary<int, HashSet<string>> baseImages)
		{
			Costume skin = link.Costume;
			if (skin == null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"No reskin information found for {link.Name}"));
				return;
			}

			if (string.IsNullOrEmpty(link.PreviewImage))
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"{link.Name} has no preview portait set."));
			}

			//gather list of images in this skin
			HashSet<string> existingImages = new HashSet<string>();
			HashSet<string> unusedImages = new HashSet<string>();
			string folder = Path.Combine(Config.SpnatiDirectory, skin.Folder);
			foreach (string filename in Directory.EnumerateFiles(folder))
			{
				string ext = Path.GetExtension(filename).ToLower();
				if (ext.EndsWith(".png") || ext.EndsWith(".gif"))
				{
					string path = Path.GetFileName(filename);
					existingImages.Add(path);
					unusedImages.Add(path);
				}
			}

			//go through each stage using the alt skin and make sure the images are all present
			List<int> skinnedStages = new List<int>();
			Dictionary<int, bool> stageUsingSkin = new Dictionary<int, bool>();
			foreach (StageSpecificValue stageInfo in skin.Folders)
			{
				stageUsingSkin[stageInfo.Stage] = stageInfo.Value == skin.Folder;
			}
			bool inUse = false;
			for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
			{
				if (stageUsingSkin.ContainsKey(i))
				{
					inUse = stageUsingSkin[i];
				}
				else
				{
					stageUsingSkin[i] = inUse;
				}
			}

			HashSet<string> missingImages = new HashSet<string>();
			foreach (KeyValuePair<int, HashSet<string>> kvp in baseImages)
			{
				if (stageUsingSkin.Get(kvp.Key))
				{
					foreach (string image in kvp.Value)
					{
						if (existingImages.Contains(image))
						{
							unusedImages.Remove(image);
						}
						else
						{
							missingImages.Add(image);	
						}
					}
				}
			}

			if (missingImages.Count > 0)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"{skin.Folder} is missing the following images: {string.Join(",", missingImages)}"));
			}
			if (unusedImages.Count > 0)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"{skin.Folder} contains images unused by any dialogue: {string.Join(",", unusedImages)}"));
			}
		}
	}

	public class ValidationError
	{
		public ValidationFilterLevel Level;
		public string Text;
		public ValidationContext Context;

		public ValidationError(ValidationFilterLevel level, string text)
		{
			Level = level;
			Text = text;
		}

		public ValidationError(ValidationFilterLevel level, string text, ValidationContext context)
		{
			Level = level;
			Text = text;
			Context = context;
		}

		public override string ToString()
		{
			return Text;
		}
	}

	public class ValidationContext
	{
		public Area ContextArea;
		public Stage Stage;
		public Case Case;
		public DialogueLine Line;
		public Epilogue Epilogue;
		public Scene Scene;
		public Directive Directive;

		public ValidationContext() { }
		public ValidationContext(Stage stage, Case stageCase, DialogueLine line)
		{
			ContextArea = Area.Dialogue;
			Stage = stage;
			Case = stageCase;
			Line = line;
		}

		public ValidationContext(Epilogue epilogue, Scene scene, Directive directive)
		{
			ContextArea = Area.Epilogue;
			Epilogue = epilogue;
			Scene = scene;
			Directive = directive;
		}

		public enum Area
		{
			Dialogue,
			Epilogue,
			Skin,
		}
	}

	[Flags]
	public enum ValidationFilterLevel
	{
		None = 0,
		Minor = 1,
		MissingImages = 2,
		Metadata = 4,
		Lines = 8,
		TargetedDialogue = 16,
		Case = 32,
		Stage = 64,
		Epilogue = 128,
		Reskins = 256
	}
}
