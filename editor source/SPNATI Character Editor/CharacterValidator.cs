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
		public static bool Validate(Character character, out List<ValidationError> warnings)
		{
			warnings = new List<ValidationError>();
			string[] hands = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight",
				"Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush"};
			HashSet<string> validHands = new HashSet<string>();
			foreach (string hand in hands)
				validHands.Add(hand.ToLowerInvariant());

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
				if (!File.Exists(Path.Combine(Config.GetRootDirectory(character), img)))
				{
					warnings.Add(new ValidationError(ValidationFilterLevel.MissingImages, string.Format("{1} does not exist. {0}", "start", img)));
				}
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
						if (!ValidateCharacterExists(stageCase.Target))
						{
							if (CheckIfOfflineCharacter(stageCase.Target))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is located in the saves folder and may be obsolete. {0}", caseLabel, stageCase.Target)));
							}
							else if (CheckIfIncompleteCharacter(stageCase.Target))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("target \"{1}\" is an incomplete character. {0}", caseLabel, stageCase.Target)));
							}
							else
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("target \"{1}\" does not exist. {0}", caseLabel, stageCase.Target)));
							}
						}
						else
						{
							Character target = CharacterDatabase.Get(stageCase.Target);
							if (target != null)
							{
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
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TargetStage))
					{
						if (!trigger.HasTarget)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("\"targetStage\" is not allowed for case {0}", caseLabel)));
						}
						int targetStage;
						if (!int.TryParse(stageCase.TargetStage, out targetStage))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("targetStage \"{1}\" should be numeric. {0}", caseLabel, stageCase.TargetStage)));
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
					if (!string.IsNullOrEmpty(stageCase.AlsoPlaying))
					{
						if (!ValidateCharacterExists(stageCase.AlsoPlaying))
						{
							if (CheckIfOfflineCharacter(stageCase.AlsoPlaying))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" is located in the saves folder and may be obsolete. {0}", caseLabel, stageCase.AlsoPlaying)));
							}
							else if (CheckIfIncompleteCharacter(stageCase.AlsoPlaying))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("alsoPlaying target \"{1}\" is an incomplete character. {0}", caseLabel, stageCase.AlsoPlaying)));
							}
							else
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not exist. {0}", caseLabel, stageCase.AlsoPlaying)));
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
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlayingStage \"{1}\" should be numeric. {0}", caseLabel, stageCase.AlsoPlayingStage)));
						}
						else
						{
							Character other = CharacterDatabase.Get(stageCase.AlsoPlaying);
							if (other != null)
							{
								if (other.Layers + Clothing.ExtraStages <= alsoPlayingStage)
								{
									warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("alsoPlaying target \"{1}\" does not have {2} stages. {0}", caseLabel, stageCase.AlsoPlaying, stageCase.AlsoPlayingStage)));
								}
							}
						}
					}
					#endregion

					#region Misc
					if (!string.IsNullOrEmpty(stageCase.HasHand) && !validHands.Contains(stageCase.HasHand.ToLowerInvariant()))
					{
						warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("hasHand \"{1}\" is unrecognized. {0}", caseLabel, stageCase.HasHand)));
					}
					int total;
					if (!string.IsNullOrEmpty(stageCase.TotalFemales))
					{
						if (!int.TryParse(stageCase.TotalFemales, out total))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("totalFemales \"{1}\" must be numeric.", caseLabel, total)));
						}
						else if (total < 0 || total > 5)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("totalFemales \"{1}\" must be between 0-5.", caseLabel, total)));
						}
					}
					if (!string.IsNullOrEmpty(stageCase.TotalMales))
					{
						if (!int.TryParse(stageCase.TotalMales, out total))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("totalMales \"{1}\" must be numeric.", caseLabel, total)));
						}
						else if (total < 0 || total > 5)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("totalMales \"{1}\" must be between 0-5.", caseLabel, total)));
						}
					}
					#endregion

					#region Filters
					foreach (var condition in stageCase.Conditions)
					{
						if (condition.Count > 5 || condition.Count < 0)
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.TargetedDialogue, string.Format("Filtering tag {1} for {2} characters, but must be between 0-5. {0}", caseLabel, condition.Filter, condition.Count)));
						}
						if (!TagDatabase.TagExists(condition.Filter))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Minor, string.Format("Filtering on tag {1} which is not used by any characters. {0}", caseLabel, condition.Filter)));
						}
					}
					#endregion

					Tuple<string, string> template = DialogueDatabase.GetTemplate(stageCase.Tag);
					string defaultLine = template.Item2;
					foreach (DialogueLine line in stageCase.Lines)
					{
						//Validate image
						string img = line.Image;
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
						//Variables can only appear once. Why? Who knows
						Regex varRegex = new Regex(@"~\w*~", RegexOptions.IgnoreCase);
						HashSet<string> usedVars = new HashSet<string>();
						HashSet<string> reportedVars = new HashSet<string>();
						MatchCollection matches = varRegex.Matches(line.Text);
						foreach (var match in matches)
						{
							string variable = match.ToString();
							if (usedVars.Contains(variable) && !reportedVars.Contains(variable))
							{
								warnings.Add(new ValidationError(ValidationFilterLevel.Variables, string.Format("Variable {1} can only be used once in a line: {0}", caseLabel, variable)));
								reportedVars.Add(variable);
							}
							else
							{
								usedVars.Add(variable);
							}
						}

						//Make sure it's not a placeholder
						if (defaultLine.Equals(line.Text))
						{
							warnings.Add(new ValidationError(ValidationFilterLevel.Case, string.Format("Case is still using placeholder text: {0}", caseLabel)));
						}
					}
				}
			}
			if (warnings.Count == 0)
				return true;

			return false;
		}

		/// <summary>
		/// Verifies that a character's folder exists
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static bool ValidateCharacterExists(string name)
		{
			if (name == "human")
				return true;
			return Directory.Exists(Config.GetRootDirectory(name));
		}

		/// <summary>
		/// Checks if a character exists in the saves folder instead of opponents
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static bool CheckIfOfflineCharacter(string name)
		{
			string dir = Path.Combine(Config.GameDirectory, "saves", name);
			return Directory.Exists(dir);
		}

		/// <summary>
		/// Checks if a character exists in the saves folder instead of opponents
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static bool CheckIfIncompleteCharacter(string name)
		{
			string dir = Path.Combine(Config.GameDirectory, "saves", "incomplete_opponents", name);
			return Directory.Exists(dir);
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
