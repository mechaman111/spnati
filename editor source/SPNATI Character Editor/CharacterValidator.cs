using SPNATI_Character_Editor.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor
{
	public static class CharacterValidator
	{
		static String[] falsePlurals = { "pants", "shorts", "panties", "boxers", "tights", "leggings", "spats", "glasses", "sunglasses", "shades" };

		/// <summary>
		/// Validates the character's dialogue and returns a list of warnings (bad images, targets, etc.)
		/// </summary>
		/// <returns></returns>
		public static bool Validate(Character character, out List<ValidationError> warnings)
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
			unusedImages.Remove(character.Metadata.Portrait.Image);

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
			ValidateWardrobe(character, warnings);

			HashSet<string> usedCollectibles = new HashSet<string>();

			Dictionary<int, bool> stageHasTieLines = new Dictionary<int, bool>();
			bool hasAfterFinished = false;
			bool hasCustomSmall = false;

			// put this here so it only needs to be checked once
			if (character.Styles != null)
			{
				foreach (StyleRule rule in character.Styles.Rules)
				{
					if (rule.ClassName == "small")
					{
						hasCustomSmall = true;
						break;
					}
				}
			}


			//dialogue
			foreach (Case stageCase in character.Behavior.GetWorkingCases())
			{
				if (stageCase.Stages.Count > 0)
				{
					ValidationContext context = new ValidationContext(new Stage(stageCase.Stages[0]), stageCase, null);
					TriggerDefinition trigger = TriggerDatabase.GetTrigger(stageCase.Tag);
					string caseLabel = string.Format("({0})", stageCase.Tag);
					string caseTag = stageCase.Tag;

                    if (stageCase.Tag == "tie")
                    {
						foreach (int stage in stageCase.Stages)
                        {
							stageHasTieLines[stage] = true;
						}
                    }
					else if (stageCase.Tag == "after_masturbating")
                    {
						hasAfterFinished = true;
                    }

                    warnings = ValidateCase(character, stageCase, context, trigger, caseLabel, caseTag, warnings, validHands, targetRange);

					foreach (Case subcase in stageCase.AlternativeConditions)
                    {
						warnings = ValidateCase(character, subcase, context, trigger, caseLabel, caseTag, warnings, validHands, targetRange);
					}

					if (!string.IsNullOrEmpty(stageCase.CustomPriority))
					{
						int priority;
						if (!int.TryParse(stageCase.CustomPriority, out priority))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Custom priority {1} must be numeric. {0}", caseLabel, stageCase.CustomPriority)));
						}
					}

					if (stageCase.AlternativeConditions.Count == 1)
                    {
						warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("Case contains only one OR tab. It would be more readable if you condensed these conditions onto the default tab. {0}", caseLabel), context));
					}

					foreach (Case alt in stageCase.AlternativeConditions)
					{
						if (!alt.HasConditions)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case contains an empty OR tab, which will reduce it to a generic line. {0}", caseLabel), context));
							break;
						}
					}

					if (stageCase.Lines.Count == 0 && string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
                    {
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case has no lines assigned. {0}", caseLabel), context));
					}

					Tuple<string, string> template = DialogueDatabase.GetTemplate(stageCase.Tag);
					string defaultLine = template.Item2;
					Regex regex = new Regex(@"\<\/i\>");

					foreach (DialogueLine line in stageCase.Lines)
					{
						//Make sure it's not a placeholder
						if (defaultLine.Equals(line.Text) && string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Case is still using placeholder text. {0}", caseLabel), context));
						}

						//Make sure it's not blank
						if (!Config.DisableEmptyValidation && line.Text.Equals("") && string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Line has no text. If this was intentional, the correct way to create a blank line is to use ~blank~. {0}", caseLabel), context));
						}

						//Make sure it doesn't have weight 0
						if (line.Weight == 0.001f && string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Line has a weight of 0. This does not actually prevent the line from playing. {0}", caseLabel), context));
						}

						//Make sure it doesn't contain invalid variables (~name~ or ~target~ in untargeted cases)
						if (!trigger.HasTarget)
						{
							if (line.Text.ToLower().Contains("~name~"))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Line contains ~name~, but is in a case with no target. {0}", caseLabel), context));
							}
							else if (line.Text.ToLower().Contains("~target~"))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Line contains ~target~, but is in a case with no target. {0}", caseLabel), context));
							}
						}

						// check if line is entirely covered by {small}
						if (!hasCustomSmall)
						{
							var trimLine = line.Text.Trim();
							if (trimLine.StartsWith("{small}"))
							{
								if (!trimLine.Substring(1).Contains("{") || (trimLine.EndsWith("{!reset}") && !trimLine.Substring(1, trimLine.Length - 9).Contains("{")))
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.Lines, "Line uses \"{small}\" " + string.Format("to shrink the entire line. Instead, please use the built-in \"Text size\" option. {0}", caseLabel), context));
								}
							}
						}

						// check for undefined custom styles
						var temptext = line.Text;

						if ((character.Styles == null || !character.Styles.AdvancedMode) && !temptext.Contains("<script>"))
						{
							while (temptext.Contains("{"))
							{
								temptext = temptext.Substring(temptext.IndexOf("{") + 1);

								var style = temptext.Split('}')[0];
								if (style == "!reset") continue;

								var customstyles = style.Split(' ');

								foreach (var cs in customstyles)
								{
									bool foundStyle = false;

									foreach (StyleRule rule in StyleDatabase.GlobalStyles)
									{
										if (rule.ClassName == cs)
										{
											foundStyle = true;
											break;
										}
									}

									if (!foundStyle && character.Styles != null)
									{
										foreach (StyleRule rule in character.Styles.Rules)
										{
											if (rule.ClassName == cs)
											{
												foundStyle = true;
												break;
											}
										}
									}

									if (!foundStyle)
									{
										warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Custom style \"{0}\" is undefined. {1}", cs, caseLabel), context));
									}
								}
							}
						}

						//check for pointless ifMales
						if (!string.IsNullOrEmpty(trigger.Gender))
                        {
							if (line.Text.ToLower().Contains("target.ifMale"))
                            {
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("\"target.ifMale\" is redundant in a gendered case. {0}", caseLabel), context));
							}
                        }
						else if (trigger.Name.Contains("human_must_strip"))
                        {
							if (line.Text.ToLower().Contains("player.ifMale"))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("\"player.ifMale\" is redundant in a case that already defines the human player's gender. {0}", caseLabel), context));
							}
							else if (line.Text.ToLower().Contains("human.ifMale"))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("\"human.ifMale\" is redundant in a case that already defines the human player's gender. {0}", caseLabel), context));
							}
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
							warnings.Add(new ValidationError(ValidationFilterLevel.Lines, $"Line has mismatched <i> </i> tags. {caseLabel}", context));
						}

						//validate collectibles
						if (!string.IsNullOrEmpty(line.CollectibleId))
						{
							usedCollectibles.Add(line.CollectibleId);
							if (character.Collectibles.Get(line.CollectibleId) == null)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Collectibles, string.Format("Unknown collectible: {1} {0}", caseLabel, line.CollectibleId), context));
							}
						}
					}
				}

				foreach (int stageIndex in stageCase.Stages)
				{
					Stage stage = new Stage(stageIndex);
					HashSet<string> stageImages = usedPoses.GetOrAddDefault(stage.Id, () => new HashSet<string>());
					ValidationContext context = new ValidationContext(stage, stageCase, null);

					TriggerDefinition trigger = TriggerDatabase.GetTrigger(stageCase.Tag);
					if (trigger == null || trigger.Unrecognized)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is an unknown case. (stage {1})", stageCase.Tag, stage.Id), context));
						continue;
					}

					if (!TriggerDatabase.UsedInStage(stageCase.Tag, character, stage.Id))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case \"{0}\" is invalid for stage {1}.", stageCase.Tag, stage.Id), context));
						continue;
					}
					string caseLabel = string.Format("(Stage {0}, {1})", stage.Id, stageCase.Tag);

					foreach (DialogueLine line in stageCase.Lines)
					{
						context = new ValidationContext(stage, stageCase, line);

						DialogueLine stageLine = line;

						List<string> imagesInStage = new List<string>();
						foreach (StageImage si in line.Images)
						{
							if (si.Stages.Contains(stage.Id))
							{
								imagesInStage.Add(si.Pose?.GetStageKey(stage.Id, true));
							}
						}
						if (imagesInStage.Count == 0)
						{
							imagesInStage.Add(stageLine.Pose?.GetStageKey(stage.Id, true));
						}

						//Validate image
						foreach (string img in imagesInStage)
						{
							if (!string.IsNullOrEmpty(img))
							{
								unusedImages.Remove(img);
								if (img.StartsWith("custom:"))
								{
									string id = img.Substring(7);
									Pose pose = character.Poses.Find(p => p.Id == id);
									if (pose == null)
									{
										warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("Pose {1} does not exist. {0}", caseLabel, img), context));
									}
								}
								else
								{
									if (!File.Exists(Path.Combine(Config.GetRootDirectory(character), img)))
									{
										warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("{1} does not exist. {0}", caseLabel, img), context));
									}
								}
								stageImages.Add(img);
							}
							else if (!string.IsNullOrEmpty(line.Text) && line.Text.Trim() != "~blank~" && string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Lines, string.Format("Line has no pose assigned. {0}", caseLabel), context));
							}
						}
					}
				}
			}

			List<int> noTieStages = new List<int>();

			for (int i = 0; i <= character.Layers + 2; i++)
            {
				if (!stageHasTieLines.ContainsKey(i) || !stageHasTieLines[i])
                {
					noTieStages.Add(i);
                }
            }

			if (noTieStages.Count == character.Layers + 3)
            {
				warnings.Add(new ValidationError(ValidationFilterLevel.Case, "Character has no Absolute Tie lines."));
			}
			else if (noTieStages.Count == 1)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("Character has no Absolute Tie lines for stage {0}.", noTieStages[0])));
			}
			else if (noTieStages.Count > 0)
            {
				warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("Character has no Absolute Tie lines for stages {0}.", joinNumbers(noTieStages, ", "))));
			}

			if (!hasAfterFinished)
            {
				warnings.Add(new ValidationError(ValidationFilterLevel.Case, "Character has no After Finished lines. Even if the Finished lines purposefully handle this case, it is advised to separate the Finished and After Finished lines into separate cases."));
			}

			//endings
			foreach (Epilogue ending in character.Endings)
			{
				ValidateEpilogue(ending, warnings, unusedImages);
			}

			foreach (Pose pose in character.Poses)
			{
				ValidatePose(character, pose, unusedImages);
			}

			foreach (Collectible collectible in character.Collectibles.Collectibles)
			{
				ValidateCollectible(character, collectible, warnings, unusedImages, usedCollectibles);
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

		private static List<ValidationError> ValidateCase(Character character, Case stageCase, ValidationContext context, TriggerDefinition trigger, string caseLabel, string caseTag, List<ValidationError> warnings, HashSet<string> validHands, Regex targetRange)
		{
			foreach (TargetCondition condition in stageCase.Conditions)
			{
				if (!string.IsNullOrEmpty(condition.Saying))
				{
					ValidateSaying(condition.Character, condition.Saying, warnings, caseTag, context, "Saying Text");
				}

				if (!string.IsNullOrEmpty(condition.Said))
				{
					ValidateSaying(condition.Character, condition.Said, warnings, caseTag, context, "Said Text");
				}

				if (condition.Role == "target" && !trigger.HasTarget)
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"target\" is not allowed for case \"{0}\".", caseTag), context));
				}

				if (!String.IsNullOrEmpty(condition.Character))
				{
					Character target = CharacterDatabase.Get(condition.Character);

					if (target == null)
					{
						if (condition.Character != "human")
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.MissingTargets, string.Format("Character \"{1}\" does not exist. {0}", caseLabel, condition.Character), context));
						}
					}
					else
					{
						if (condition.Role == "target" && target.FolderName != "human")
						{
							if (!string.IsNullOrEmpty(trigger.Gender) && target.Gender != trigger.Gender)
							{
								if (!target.Metadata.CrossGender)
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Target \"{1}\" is {2}, so this case will never trigger. {0}", caseLabel, condition.Character, target.Gender), context));
								}
							}
							if (!string.IsNullOrEmpty(trigger.Size) && target.Size != trigger.Size)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Target \"{1}\" has a size of {2}, so this case will never trigger. {0}", caseLabel, condition.Character, target.Size), context));
							}
							if (!string.IsNullOrEmpty(condition.Stage))
							{
								int targetStage;
								if (int.TryParse(condition.Stage, out targetStage))
								{
									if (target.Layers + Clothing.ExtraStages <= targetStage)
									{
										warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Target \"{1}\" does not have {2} stages. {0}", caseLabel, condition.Character, condition.Stage), context));
									}
									Clothing clothing;
									if (!ValidateStageWithTag(target, targetStage, caseTag, out clothing))
									{
										if (clothing == null)
											warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Using the first stage as a target stage for a removed_item case. Removed cases should use the stage following the removing stage. {0}", caseLabel), context));
										else warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Targeting {1} at stage {2} ({3}), which will never happen because {3} is of type {4}. {0}", caseLabel, target, targetStage, clothing.Name, clothing.Type), context));
									}
								}
							}
						}

						if(string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
                        {
							ValidateMarker(warnings, target, caseLabel, condition.SaidMarker, condition.Stage, context);
							ValidateMarker(warnings, target, caseLabel, condition.SayingMarker, condition.Stage, context);
						}
					}
				}

				if (condition.Role == "self")
				{
					if (string.IsNullOrEmpty(stageCase.Hidden) && string.IsNullOrEmpty(stageCase.Disabled))
					{
						ValidateMarker(warnings, character, caseLabel, condition.SaidMarker, context);
					}
					
					// these next ones are not warnable if the case is hidden AND negative-priority
					int caseCustomPriority = 0;
					int.TryParse(stageCase.CustomPriority, out caseCustomPriority);
					bool isPostDialogueCase = !string.IsNullOrEmpty(stageCase.Hidden) && (caseCustomPriority < 0);

					if (!String.IsNullOrEmpty(condition.SayingMarker) && !isPostDialogueCase)
                    {
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Trying to use a Saying Marker condition on Self, which will always fail. {0}", caseLabel), context));
					}
					if (!String.IsNullOrEmpty(condition.Saying) && !isPostDialogueCase)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Trying to use a Saying Text condition on Self, which will always fail. {0}", caseLabel), context));
					}
					if (!String.IsNullOrEmpty(condition.Pose) && !isPostDialogueCase)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Trying to use a Pose condition on Self, which will always fail. {0}", caseLabel), context));
					}

					if (!string.IsNullOrEmpty(condition.Said))
					{
						ValidateSaying(character.FolderName, condition.Said, warnings, caseTag, context, "Said Text");
					}
				}

				if (!string.IsNullOrEmpty(condition.Stage) && !targetRange.IsMatch(condition.Stage))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Stage condition \"{1}\" should be numeric or a range. {0}", caseLabel, condition.Stage), context));
				}

				if (!string.IsNullOrEmpty(condition.Hand) && !validHands.Contains(condition.Hand.ToLowerInvariant()))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Hand condition \"{1}\" is unrecognized. {0}", caseLabel, condition.Hand), context));
				}

				if (condition.FilterTag != "human" && condition.FilterTag != "human_male" && condition.FilterTag != "human_female" && !string.IsNullOrEmpty(condition.FilterTag) && !TagDatabase.TagExists(condition.FilterTag))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("Filtering on tag \"{1}\" which is not used by any characters. {0}", caseLabel, condition.FilterTag), context));
				}

				warnings.AddRange(ValidateRangeField(condition.ConsecutiveLosses, "Consecutive Losses condition", caseLabel, -1, -1, context));
				warnings.AddRange(ValidateRangeField(condition.TimeInStage, "Time in Stage condition", caseLabel, -1, -1, context));
				warnings.AddRange(ValidateRangeField(condition.Count, string.Format("\"{0}\" tag count", condition.FilterTag), caseLabel, 0, 5, context));

				if (condition.Count == "0-")
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Condition has a count of \"0+\", which is meaningless. If you meant a count of zero, you also need to fill in the \"to:\" field. {0}", caseLabel), context));
				}
			}

			warnings.AddRange(ValidateRangeField(stageCase.TotalRounds, "totalRounds", caseLabel, -1, -1, context));

			ValidateExpressions(warnings, stageCase, caseLabel, context);

			return warnings;
		}

		private static string joinNumbers(List<int> list, string separator)
        {
			string s = "";

			for (int i = 0; i < list.Count; i++)
            {
				s += list[i];

				int curIndex = i;
				for (int j = i + 1; j < list.Count; j++)
                {
					if (list[j] == list[curIndex] + 1)
                    {
						curIndex++;
                    }
					else
                    {
						if (curIndex != i)
                        {
							s += "-" + list[curIndex];
                        }

						if (curIndex != list.Count - 1)
                        {
							s += separator;
                        }

						break;
                    }
                }

				if (curIndex == list.Count - 1 && curIndex != i)
                {
					s += "-" + list[curIndex];
                }

				i = curIndex;
            }

			return s;
        }

		private static bool IsUncountable(string name)
		{
			return name == "armor" || name == "jewelry" || name == "jewellery";
		}

		private static void ValidateWardrobe(Character character, List<ValidationError> warnings)
		{
			bool foundPlural = false;
			bool foundGeneric = false;
			string pluralGuess = null;
			string upper = null;
			string lower = null;
			string importantUpper = null;
			string importantLower = null;
			bool foundBoth = false;
			string otherMajor = null;
			for (int i = 0; i < character.Layers; i++)
			{
				Clothing c = character.GetClothing(i);
				foundPlural = c.Plural || foundPlural;
				if (pluralGuess == null && !c.Plural && c.Name.EndsWith("s"))
				{
					pluralGuess = c.Name;
				}
				foundGeneric = !String.IsNullOrEmpty(c.GenericName) || foundGeneric;

				if (c.Position == "upper" && c.Type == "major")
					upper = c.Name;
				if (c.Position == "lower" && c.Type == "major")
					lower = c.Name;
				if (c.Position == "both" && c.Type == "major")
					foundBoth = true;
				if (c.Position == "upper" && c.Type == "important")
					importantUpper = c.Name;
				if (c.Position == "lower" && c.Type == "important")
					importantLower = c.Name;
				if (c.Position == "other" && c.Type == "major")
					otherMajor = c.Name;

				if (IsUncountable(c.Name))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{c.Name}\" uses an uncountable noun with no plural form, which makes incoming generic dialogue awkward (ex. \"I've seen many {c.Name} in my day\"). Consider renaming this layer (ex. \"armor\" to \"breastplate\")."));
				}

				if (!c.Plural && falsePlurals.Contains(c.Name))
                {
					warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{c.Name}\" should be set to plural. Even though it's only one item, it's still a grammatically plural noun (ex. \"Those are some nice {c.Name}\")."));
				}

				if (!String.IsNullOrEmpty(c.GenericName))
				{
					bool validCategory = false;
					bool lowercaseCategory = false;
					foreach (ClothingCategoryItem cc in ClothingDefinitions.Instance.Categories)
					{
						if (cc.Key == c.GenericName)
						{
							validCategory = true;
							break;
						}
						else if (cc.Key == c.GenericName.ToLower())
                        {
							lowercaseCategory = true;
							break;
						}
					}

					if (!validCategory)
					{
						if (lowercaseCategory)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{c.Name}\" has classification \"{c.GenericName}\", which is inappropriately capitalized. Opening the Wardrobe tab will fix this."));
						}
						else
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{c.Name}\" has an invalid classification: \"{c.GenericName}\". Opening the Wardrobe tab will delete this classification, at which point you can enter a valid one if possible."));
						}
					}
				}
			}
			if (!foundBoth)
			{
				if (string.IsNullOrEmpty(upper))
				{
					if (!string.IsNullOrEmpty(importantUpper))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{importantUpper}\" has no major article covering it, meaning the character will be considered as having exposed underwear. Either an article{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantUpper} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Character has no clothing of type: major, position: upper, meaning the character will be considered as having an exposed chest. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}
				}
				if (string.IsNullOrEmpty(lower))
				{
					if (!string.IsNullOrEmpty(importantLower))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Clothing layer \"{importantLower}\" has no major article covering it, meaning the character will be considered as having exposed underwear. Either an article{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantLower} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Character has no clothing of type: major, position: lower, meaning the character will be considered as having an exposed crotch. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}
				}
			}

			if (!foundPlural && pluralGuess != null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"No clothing items are marked as plural. Should they be (ex. {pluralGuess})?"));
			}

			if (!foundGeneric)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"No clothing items have a classification. Are you sure this is correct?"));
			}
		}

		// terrible duplication, fix later
		private static void ValidateSkinWardrobe(Costume skin, List<ValidationError> warnings)
		{
			bool foundPlural = false;
			bool foundGeneric = false;
			string pluralGuess = null;
			string upper = null;
			string lower = null;
			string importantUpper = null;
			string importantLower = null;
			bool foundBoth = false;
			string otherMajor = null;
			for (int i = 0; i < skin.Layers; i++)
			{
				Clothing c = skin.GetClothing(i);
				foundPlural = c.Plural || foundPlural;
				if (pluralGuess == null && !c.Plural && c.Name.EndsWith("s"))
				{
					pluralGuess = c.Name;
				}
				foundGeneric = !String.IsNullOrEmpty(c.GenericName) || foundGeneric;

				if (c.Position == "upper" && c.Type == "major")
					upper = c.Name;
				if (c.Position == "lower" && c.Type == "major")
					lower = c.Name;
				if (c.Position == "both" && c.Type == "major")
					foundBoth = true;
				if (c.Position == "upper" && c.Type == "important")
					importantUpper = c.Name;
				if (c.Position == "lower" && c.Type == "important")
					importantLower = c.Name;
				if (c.Position == "other" && c.Type == "major")
					otherMajor = c.Name;

				if (IsUncountable(c.Name))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{c.Name}\" of alternate costume \"{skin.Name}\" uses an uncountable noun with no plural form, which makes incoming generic dialogue awkward (ex. \"I've seen many {c.Name} in my day\"). Consider renaming this layer (ex. \"armor\" to \"breastplate\")."));
				}

				if (!c.Plural && falsePlurals.Contains(c.Name))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{c.Name}\" of alternate costume \"{skin.Name}\" should be set to plural. Even though it's only one item, it's still a grammatically plural noun (ex. \"Those are some nice {c.Name}\")."));
				}

				if (!String.IsNullOrEmpty(c.GenericName))
				{
					bool validCategory = false;
					bool lowercaseCategory = false;
					foreach (ClothingCategoryItem cc in ClothingDefinitions.Instance.Categories)
					{
						if (cc.Key == c.GenericName)
						{
							validCategory = true;
							break;
						}
						else if (cc.Key == c.GenericName.ToLower())
						{
							lowercaseCategory = true;
							break;
						}
					}

					if (!validCategory)
					{
						if (lowercaseCategory)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{c.Name}\" of alternate costume \"{skin.Name}\" has classification \"{c.GenericName}\", which is inappropriately capitalized. Opening the alternate costume's Wardrobe tab will fix this."));
						}
						else
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{c.Name}\" of alternate costume \"{skin.Name}\" has an invalid classification: \"{c.GenericName}\". Opening the alternate costume's Wardrobe tab will delete this classification, at which point you can enter a valid one if possible."));
						}
					}
				}
			}
			if (!foundBoth)
			{
				if (string.IsNullOrEmpty(upper))
				{
					if (!string.IsNullOrEmpty(importantUpper))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{importantUpper}\" of alternate costume \"{skin.Name}\" has no major article covering it, meaning the character will be considered as having exposed underwear. Either an article{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantUpper} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Alternate costume \"{skin.Name}\" has no clothing of type: major, position: upper, meaning the character will be considered as having an exposed chest. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(lower) ? $" ({lower}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}
				}
				if (string.IsNullOrEmpty(lower))
				{
					if (!string.IsNullOrEmpty(importantLower))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Clothing layer \"{importantLower}\" of alternate costume \"{skin.Name}\" has no major article covering it, meaning the character will be considered as having exposed underwear. Either an article{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")} should be given position: both if it covers both the chest and crotch, or {importantLower} should use type: major instead of important."));
					}
					else
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"Alternate costume \"{skin.Name}\" has no clothing of type: major, position: lower, meaning the character will be considered as having an exposed crotch. If an item covers underwear over both the chest and crotch{(!string.IsNullOrEmpty(upper) ? $" ({upper}?)" : !string.IsNullOrEmpty(otherMajor) ? $" ({otherMajor}?)" : "")}, it should be given a position: both"));
					}
				}
			}

			if (!foundPlural && pluralGuess != null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"No clothing items on alternate costume \"{skin.Name}\" are marked as plural. Should they be (ex. {pluralGuess})?"));
			}

			if (!foundGeneric)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"No clothing items on alternate costume \"{skin.Name}\" have a classification. Are you sure this is correct?"));
			}
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
				if (minStr != "")
				{
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
					if (maxStr != "")
					{
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

		private static void ValidateExpressions(List<ValidationError> warnings, Case stageCase, string caseLabel, ValidationContext context)
		{
			foreach (ExpressionTest test in stageCase.Expressions)
			{
				if (string.IsNullOrEmpty(test.Expression))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Case, $"Variable test has no expression: {test} {caseLabel}", context));
				}
				else if (test.Expression.Contains("~_."))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Case, $"Variable test has no subject: {test} {caseLabel}", context));
				}
				if (string.IsNullOrEmpty(test.Value))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Case, $"Variable test has no value: {test} {caseLabel}", context));
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

			// Monika and Sayori do scripting that results in false positives
			if (character.FolderName == "monika" || character.FolderName == "sayori")
				return;

			if (character == null)
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.MissingTargets, string.Format("Missing character for {1}. {0}", caseLabel, name), context));
			}
			else
			{
				string value;
				MarkerOperator op;
				bool perTarget;

				name = Marker.ExtractConditionPieces(name, out op, out value, out perTarget);
				if (character.Markers.IsValueCreated && !character.Markers.Value.Contains(name))
				{
					//Count could be 0 for characters who have no editor data, so unless we decide to duplicate MarkerData in CachedCharacter, just ignore it for unloaded characters
					warnings.Add(new ValidationError(ValidationFilterLevel.Markers, string.Format("{1} has no dialogue that sets marker {2}, so this case will never trigger. {0}", caseLabel, character.Name, name), context));
				}
				else
				{
					if (character.IsFullyLoaded)
					{
						if (!string.IsNullOrEmpty(stageRange))
						{
							//verify that a marker can even be set prior to this point
							bool setsAtAll = false;

							int min, max;
							Case.ToRange(stageRange, out min, out max);
							if (character.Behavior.Triggers.Count > 0)
							{
								foreach (Trigger t in character.Behavior.Triggers)
								{
									foreach (Case c in t.Cases)
									{
										foreach (var line in c.Lines)
										{
											if (string.IsNullOrEmpty(line.Marker)) { continue; }
											string val;
											bool pt;
											string markerOp;
											string markerName = Marker.ExtractPieces(line.Marker, out val, out pt, out markerOp);
											if (markerName == name)
											{
												// persistent markers could always have been set
												if (character.Behavior.PersistentMarkers.Contains(name))
													return;

												for (int i = 0; i < c.Stages.Count; i++)
												{
													if (c.Stages[i] <= max)
													{
														return;
													}
												}

												setsAtAll = true;
											}

											if (line.Markers != null)
											{
												foreach (MarkerOperation m in line.Markers)
												{
													if (m.Name == name)
													{
														// persistent markers could always have been set
														if (character.Behavior.PersistentMarkers.Contains(name))
															return;

														for (int i = 0; i < c.Stages.Count; i++)
														{
															if (c.Stages[i] <= max)
															{
																return;
															}
														}

														setsAtAll = true;
													}
												}
											}
										}

									}
								}
							}
							else
							{
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
							}

							if (setsAtAll)
                            {
								warnings.Add(new ValidationError(ValidationFilterLevel.Markers, string.Format("{1} has no dialogue before or during stage {2} that sets marker {3}, so this case will never trigger. {0}", caseLabel, character.Name, max, name), context));
							}
							else
                            {
								warnings.Add(new ValidationError(ValidationFilterLevel.Markers, string.Format("{1} has no dialogue that sets marker {2}, so this case will never trigger. {0}", caseLabel, character.Name, name), context));
							}
						}

						if (!string.IsNullOrEmpty(value) && value != "1" && value != "0")
						{
							bool used = false;
							Marker m = character.Markers.Value.Values.FirstOrDefault(marker => marker.Name == name);
							if (m != null)
							{
								used = m.Values.Contains(value);
								if (!used)
                                {
									// if value is set to a variable, it could be any value
									foreach (String val in m.Values)
                                    {
										if (val.Contains("~"))
                                        {
											used = true;
											break;
                                        }
                                    }

									if (!used)
									{
										int test;
										//they never set the value directly, but if it's numeric, then they might be able to increment or decrement to it
										if (int.TryParse(value, out test))
										{
											used = (m.Values.Contains("+") || m.Values.Contains("-") || m.Values.Contains("*") || m.Values.Contains("/") || m.Values.Contains("%"));
										}
									}
								}
							}
							if (!used)
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Markers, $"{character.Name} has no dialogue that sets marker {name} to {value}, so this case will never trigger. {caseLabel}", context));
							}
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
					clothing = character.GetConvertedWardrobe()[character.Layers - stage - 1];
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

			bool lastTransition = false;
			foreach (Scene scene in ending.Scenes)
			{
				if (scene.Transition)
				{
					if (lastTransition)
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a scene with more than one transition. Only one transition is allowed.", new ValidationContext(ending, scene, null)));
					}
					lastTransition = true;
					continue;
				}
				lastTransition = false;

				HashSet<string> usedObjectIds = new HashSet<string>();
				if (!string.IsNullOrEmpty(scene.Background))
				{
					unusedImages.Remove(scene.Background);
				}

				foreach (Directive directive in scene.Directives)
				{
					if (directive.DirectiveType == "sprite" || directive.DirectiveType == "emitter")
					{
						string id = directive.Id;
						if (string.IsNullOrEmpty(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a {directive.DirectiveType} directive ({directive}) with no ID.", new ValidationContext(ending, scene, directive)));
						}
						else if (usedObjectIds.Contains(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} uses the ID \"{id}\" for more than one object in the same scene.", new ValidationContext(ending, scene, directive)));
						}
						else
						{
							usedObjectIds.Add(id);
						}

						if (!string.IsNullOrEmpty(directive.Src))
						{
							unusedImages.Remove(directive.Src);
						}
					}
					else if (directive.DirectiveType == "move" || directive.DirectiveType == "remove")
					{
						string id = directive.Id;
						if (string.IsNullOrEmpty(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a {directive.DirectiveType} directive ({directive}) with no ID.", new ValidationContext(ending, scene, directive)));
						}
						else if (!usedObjectIds.Contains(id))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Epilogue, $"Ending {ending.Title} has a {directive.DirectiveType} directive with ID \"{id}\" which does not correspond to any object in that scene.", new ValidationContext(ending, scene, directive)));
						}
					}
				}
			}
		}

		/// <summary>
		/// Validates a custom pose
		/// </summary>
		/// <param name="pose"></param>
		/// <param name="warnings"></param>
		/// <param name="baseImages"></param>
		private static void ValidatePose(Character character, Pose pose, HashSet<string> unusedImages)
		{
			unusedImages.Remove("custom:" + pose.Id);
			foreach (Sprite sprite in pose.Sprites)
			{
				string path = GetRelativeImagePath(character, sprite.Src);
				if (!string.IsNullOrEmpty(path))
				{
					if (path.Substring(0, 1) != "#")
					{
						unusedImages.Remove(path);
					}
					else
                    {
						// mark as used for all layers if cross-stage
						for (int i = 0; i <= character.Layers + 2; i++)
                        {
							unusedImages.Remove(i + path.Substring(1));
						}
                    }
				}
			}

			foreach (PoseDirective directive in pose.Directives)
			{
				foreach (Keyframe kf in directive.Keyframes)
				{
					if (!string.IsNullOrEmpty(kf.Src))
					{
						string path = GetRelativeImagePath(character, kf.Src);
						if (!string.IsNullOrEmpty(path))
						{
							if (path.Substring(0, 1) != "#")
							{
								unusedImages.Remove(path);
							}
							else
							{
								// mark as used for all layers if cross-stage
								for (int i = 0; i <= character.Layers + 2; i++)
								{
									unusedImages.Remove(i + path.Substring(1));
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Validates a collectible
		/// </summary>
		private static void ValidateCollectible(Character character, Collectible collectible, List<ValidationError> warnings, HashSet<string> unusedImages, HashSet<string> usedCollectibles)
		{
			ValidationContext context = new ValidationContext(collectible);
			if (!usedCollectibles.Contains(collectible.Id))
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Collectibles, $"Collectible {collectible.Id} is never unlocked by any dialogue.", context));
			}
			if (!string.IsNullOrEmpty(collectible.Thumbnail))
			{
				string path = GetRelativeImagePath(character, collectible.Thumbnail);
				if (!string.IsNullOrEmpty(path))
				{
					unusedImages.Remove(path);
				}
				string fullpath = Path.Combine(Config.SpnatiDirectory, collectible.Thumbnail);
				if (!File.Exists(fullpath))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Collectibles, $"The thumbnail \"{collectible.Thumbnail}\" for collectible \"{collectible.Id}\" does not exist.", context));
				}
			}
			if (!string.IsNullOrEmpty(collectible.Image))
			{
				string path = GetRelativeImagePath(character, collectible.Image);
				if (!string.IsNullOrEmpty(path))
				{
					unusedImages.Remove(path);
				}
				string fullpath = Path.Combine(Config.SpnatiDirectory, collectible.Image);
				if (!File.Exists(fullpath))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.Collectibles, $"The image \"{collectible.Image}\" for collectible \"{collectible.Id}\" does not exist.", context));
				}
			}

			if (collectible.Wearable && !collectible.ClothingIsPlural && falsePlurals.Contains(collectible.ClothingName))
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Metadata, $"Collectible \"{collectible.Id}\" has associated clothing, \"{collectible.ClothingName}\", which should be set to plural. Even though it's only one item, it's still a grammatically plural noun (ex. \"Those are some nice {collectible.ClothingName}\")."));
			}
		}

		private static string GetRelativeImagePath(Character character, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			string characterRoot = character.GetDirectory();
			string fullPath = Path.Combine(Config.SpnatiDirectory, path.StartsWith("opponents") ? path : Path.Combine("opponents", path));
			fullPath = fullPath.Replace("/", "\\");
			if (fullPath.StartsWith(characterRoot))
			{
				string relPath = fullPath.Substring(characterRoot.Length + 1);
				return relPath;
			}
			else if (path.StartsWith("reskins"))
			{
				string relPath = Path.GetFileName(path);
				return relPath;
			}
			return path;
		}

		private static void ValidateSaying(string target, string text, List<ValidationError> warnings, string caseLabel, ValidationContext context, string conditionName)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}

			if (text.Contains("##"))
            {
				text = text.Substring(0, text.IndexOf("##"));
			}

			Character other = CharacterDatabase.Get(target);
			if (other != null && other.IsFullyLoaded)
			{
				bool found = false;
				foreach (Case stageCase in other.Behavior.EnumerateSourceCases())
				{
					foreach (DialogueLine line in stageCase.Lines)
					{
						if (line.Text.Contains(text))
						{
							found = true;
							break;
						}
					}
					if (found)
					{
						break;
					}
				}
				if (!found)
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, $"Using a {conditionName} condition but {other.Name} never says this text: \"{text}\" ({caseLabel})", context));
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

			if (string.IsNullOrEmpty(link.PreviewImage))
			{
				warnings.Add(new ValidationError(ValidationFilterLevel.Reskins, $"{link.Name} has no preview portait set."));
			}
			else
			{
				unusedImages.Remove(link.PreviewImage);
			}

			ValidateSkinWardrobe(skin, warnings);

			//go through each stage using the alt skin and make sure the images are all present
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

			foreach (Pose pose in skin.Poses)
			{
				ValidatePose(character, pose, unusedImages);
				missingImages.Remove("custom:" + pose.Id);
			}

			// custom poses from the base skin can never be missing in an alt skin
			// because alt skins inherit all poses from the base skin
			foreach (Pose pose in character.Poses)
			{
				missingImages.Remove("custom:" + pose.Id);
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
		public Collectible Collectible;

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

		public ValidationContext(Collectible collectible)
		{
			Collectible = collectible;
			ContextArea = Area.Collectible;
		}

		public enum Area
		{
			Dialogue,
			Epilogue,
			Skin,
			Collectible,
		}
	}

	[Flags]
	public enum ValidationFilterLevel
	{
		None = 0,
		Minor = 1 << 0,
		Markers = 1 << 1,
		MissingTargets = 1 << 2,
		MissingImages = 1 << 3,
		Metadata = 1 << 4,
		Lines = 1 << 5,
		TargetedDialogue = 1 << 6,
		Case = 1 << 7,
		Stage = 1 << 8,
		Epilogue = 1 << 9,
		Reskins = 1 << 10,
		Collectibles = 1 << 11,
	}
}
