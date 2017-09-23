using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	public static class FlatFileSerializer
	{
		/// <summary>
		/// Creates a dialogue.txt out of a character's data, which can be run through make-xml.py (which should generate the exact output this program already does)
		/// </summary>
		/// <param name="character"></param>
		public static bool ExportFlatFile(Character character)
		{
			Metadata metadata = character.Metadata;

			List<string> lines = new List<string>();
			lines.Add("#required for behaviour.xml");
			lines.Add("first=" + character.FirstName);
			lines.Add("last=" + character.LastName);
			lines.Add("label=" + character.Label);
			lines.Add("gender=" + character.Gender);
			lines.Add("size=" + character.Size);
			if(character.Intelligence.Count > 0)
				lines.Add("intelligence=" + character.Intelligence[0].Level); //make_xml.py doesn't support multi-stage intelligence yet
			lines.Add("");
			lines.Add("#Number of phases to \"finish\" masturbating");
			lines.Add("timer=" + character.Stamina);
			lines.Add("");
			lines.Add("#Tags describe characters and allow dialogue directed to only characters with these tags, such as: confident, blonde, and british. All tags should be lower case. See tag_list.txt for a list of tags.");
			foreach (string tag in character.Tags)
			{
				lines.Add("tag=" + tag);
			}
			lines.Add("");
			lines.Add("#required for meta.xml");
			lines.Add("#select screen image");
			lines.Add("pic=" + metadata.Portrait);
			lines.Add("height=" + metadata.Height);
			lines.Add("from=" + metadata.Source);
			lines.Add("writer=" + metadata.Writer);
			lines.Add("artist=" + metadata.Artist);
			lines.Add("description=" + metadata.Description);

			lines.Add("");
			lines.Add("#You can have more than one start line, but make_xml won't pick them up automatically and they must be added manually after the xml is generated.");
			lines.Add("#When selecting the characters to play the game, the first line will always play, then it randomly picks from any of the start lines after you commence the game but before you deal the first hand.");
			foreach (var line in character.StartingLines)
			{
				var defaultLine = Behaviour.CreateDefaultLine(line);
				lines.Add(string.Format("start=0-{0},{1}", defaultLine.Image, defaultLine.Text));
			}

			#region Clothing commentary
			lines.Add("");
			lines.Add("");
			lines.Add("#Items of clothing should be listed here in order of removal.");
			lines.Add("#The values are formal name, lower case name, how much they cover, what they cover");
			lines.Add("#Please do not put spaces around the commas.");
			lines.Add("#\"Important\" clothes cover genitals (lower) or chest/breasts (upper). For example: bras, panties, a skirt when going commando.");
			lines.Add("#\"Major\" clothes cover underwear. For example: skirts, pants, shirts, dresses.");
			lines.Add("#\"Minor\" clothes cover skin or are large pieces of clothing that do not cover skin. For example: jackets, socks, stockings, gloves.");
			lines.Add("#\"Extra\" clothes are small items that may or may not be clothing but do not cover anything interesting. For example: jewelry, shoes or boots with socks underneath, belts, hats. In the rest of the code, \"extra\" clothes are called \"accessory\".");
			lines.Add("#If for some reason you write another word for the type of clothes (e.g. \"accessory\"), other characters will not react at all when the clothing is removed.");
			lines.Add("#What they cover = upper (upper body), lower (lower body), other (neither).");
			lines.Add("#The game can support any number of entries, but typically we use 2-8, with at least one \"important\" layer for upper and lower (each).");
			#endregion
			for (int i = character.Wardrobe.Count - 1; i >= 0; i--)
			{
				Clothing clothes = character.Wardrobe[i];
				lines.Add(string.Format("clothes={0},{1},{2},{3}", clothes.Name, clothes.Lowercase, clothes.Type, clothes.Position));
			}

			#region Lines commentary
			lines.Add("");
			lines.Add("");
			lines.Add("");
			lines.Add("#Notes on dialogue");
			lines.Add("#All lines that start with a # symbol are comments and will be ignored by the tool that converts this file into a xml file for the game.");
			lines.Add("#Where more than one line has an identical type, like \"swap_cards\" and \"swap_cards\", the game will randomly select one of these lines each time the character is in that situation.");
			lines.Add("#You should try to include multiple lines for most stages, especially the final (finished) stage, -1. ");
			lines.Add("");
			lines.Add("#A character goes through multiple stages as they undress. The stage number starts at zero and indicates how many layers they have removed. Special stage numbers are used when they are nude (-3), masturbating (-2), and finished (-1).");
			lines.Add("#Line types that start with a number will only display during that stage. The will override any numberless stage-generic lines. For example, in stage 4 \"4-swap_cards\" will be used over \"swap_cards\" if it is not blank here. Giving a character unique dialogue for each stage is an effective way of showing their changing openness/shyness as the game progresses.");
			lines.Add("#You can combine the above points and make multiple lines for a particular situation in a particular stage, like \"4-swap_cards\" and \"4-swap_cards\".");
			lines.Add("");
			lines.Add("#Some special words can be used that will be substituted by the game for context-appropriate ones: ~name~ is the name of the character they're speaking to, but this only works if someone else is in focus. ~clothing~ is the type of clothing that is being removed by another player. ~Clothing~ is almost the same, but it starts with a capital letter in case you want to start a sentence with it.");
			lines.Add("#~name~ can be used any time a line targets an opponent (game_over_defeat, _must_strip, _removing_, _removed, _must_masturbate, etc).");
			lines.Add("#~clothing~ can be used only when clothing is being removed (_removing and _removed, but NOT _must_strip).");
			lines.Add("#~player~ can be used at any time and refers to the human player.");
			lines.Add("#~cards~ can be used only in the swap_cards lines.");
			lines.Add("#All wildcards can be used once per line only! If you use ~name~ twice, the code will show up the second time.");
			lines.Add("");
			lines.Add("#Lines can be written that are only spoken when specific other characters are present. For a detailed explanation, read this guide: https://www.reddit.com/r/spnati/comments/6nhaj0/the_easy_way_to_write_targeted_lines/");
			lines.Add("#Here is an example line (note that targeted lines must have a stage number):");
			lines.Add("#0-female_must_strip,target:hermioine=happy,Looks like your magic doesn't help with poker!");
			#endregion
			List<Case> cases = new List<Case>();

			//Make a best guess as to what the "default" case is for a tag by using the one that applies to the most stages
			Dictionary<string, Case> defaultCases = new Dictionary<string, Case>();
			foreach (Case c in character.Behavior.WorkingCases)
			{
				if (c.Stages.Count == 0 || c.HasFilters)
					continue;
				Case currentDefaultForTag;
				if (!defaultCases.TryGetValue(c.Tag, out currentDefaultForTag))
				{
					defaultCases[c.Tag] = c;
				}
				else
				{
					if (c.Stages.Count > currentDefaultForTag.Stages.Count)
					{
						defaultCases[c.Tag] = c;
					}
				}
			}

			//Separate non-default cases into individual cases per stage
			foreach (Case c in character.Behavior.WorkingCases)
			{
				if (c.Stages.Count == 0)
					continue;
				if (defaultCases[c.Tag] == c)
				{
					c.IsDefault = true;
					cases.Add(c);
				}
				else
				{
					foreach (int stage in c.Stages)
					{
						Case stageCase = c.CopyConditions();
						stageCase.Stages.Add(stage);
						foreach (var line in c.Lines)
						{
							stageCase.Lines.Add(Behaviour.CreateStageSpecificLine(line, stage));
						}
						cases.Add(stageCase);
					}
				}
			}

			//Sort cases by stage+group order
			cases.Sort((c1, c2) =>
			{
				//1st key: group
				Trigger t1 = TriggerDatabase.GetTrigger(c1.Tag);
				Trigger t2 = TriggerDatabase.GetTrigger(c2.Tag);
				int compare = t1.Group.CompareTo(t2.Group);
				if (compare == 0)
				{
					//2nd key: stage
					int stage1 = c1.IsDefault ? -1 : c1.Stages[0];
					int stage2 = c2.IsDefault ? -1 : c2.Stages[0];
					if (t1.GroupWithPreviousStage && stage1 >= 0)
						stage1--;
					if (t2.GroupWithPreviousStage && stage2 >= 0)
						stage2--;
					compare = stage1.CompareTo(stage2);
					if (compare == 0)
					{
						//3rd key: order
						compare = t1.GroupOrder.CompareTo(t2.GroupOrder);
						if (compare == 0)
						{
							//4th key: are there conditions?
							int filters1 = c1.HasFilters ? 1 : 0;
							int filters2 = c2.HasFilters ? 1 : 0;
							compare = filters1.CompareTo(filters2);
						}
					}
				}
				return compare;
			});

			//Output time!
			int lastGroup = -1;
			int lastStage = -10;
			bool appliesToNext = false;
			foreach (var outputCase in cases)
			{
				int stageId = TriggerDatabase.ToFlatFileStage(character, outputCase.Stages[0]);
				if (outputCase.IsDefault)
					stageId = -1;
				string tag = outputCase.Tag;
				Trigger trigger = TriggerDatabase.GetTrigger(tag);
				int group = trigger.Group;
				bool needSpacer = (lastGroup != group || lastStage != stageId);
				if (needSpacer)
				{
					lines.Add("");
				}
				if (lastGroup != group)
				{
					appliesToNext = TriggerDatabase.GroupAppliesToNextStage(group);
					string description = TriggerDatabase.GetGroupDescription(group);
					if (!string.IsNullOrEmpty(description))
					{
						lines.Add("");
						lines.Add("");
						lines.Add("");
						lines.Add(description.Trim('\n'));
					}
					lastGroup = group;
					lastStage = -10;
				}
				if (lastStage != stageId)
				{
					if (outputCase.IsDefault)
					{
						lines.Add("");
						lines.Add("#stage-specific lines that override the stage-generic ones");
					}
					else
					{
						StageName label = character.LayerToStageName(stageId, appliesToNext);
						if (label?.DisplayName != null)
						{
							lines.Add("");
							lines.Add("#" + label.DisplayName.ToLower());
						}
					}
					lastStage = stageId;
				}
				string stagePrefix = outputCase.IsDefault ? "" : stageId + "-";
				if (trigger.NoPrefix)
				{
					stagePrefix = "";
				}
				List<string> code = new List<string>();
				code.Add(string.Format("{0}{1}", stagePrefix, outputCase.Tag));
				code.AddRange(GetFilters(outputCase));
				string caseCode = string.Join(",", code);

				foreach (var line in outputCase.Lines)
				{
					var defaultLine = Behaviour.CreateDefaultLine(line);
					lines.Add(string.Format("{0}={1},{2}", caseCode, defaultLine.Image, defaultLine.Text));
				}
			}

			// endings
			lines.Add("\r\n#EPILOGUE/ENDING");
			foreach (Epilogue ending in character.Endings)
			{
				lines.Add("");
				lines.Add(string.Format("ending={0}", ending.Title));
				lines.Add(string.Format("\tending_gender={0}", ending.Gender));
				foreach (Screen screen in ending.Screens)
				{
					lines.Add("");
					lines.Add(string.Format("\t\tscreen={0}", screen.Image));
					foreach (EndingText text in screen.Text)
					{
						lines.Add("\t\t\t\t");
						lines.Add(string.Format("\t\t\t\ttext={0}", text.Content));
						lines.Add(string.Format("\t\t\t\tx={0}", text.X));
						lines.Add(string.Format("\t\t\t\ty={0}", text.Y));
						lines.Add(string.Format("\t\t\t\twidth={0}", text.Width));
						lines.Add(string.Format("\t\t\t\tarrow={0}", text.Arrow));
					}
				}
			}

			string filename = Path.Combine(Config.GetRootDirectory(character), "edit-dialogue.txt");
			File.WriteAllLines(filename, lines);
			return true;
		}

		private static List<string> GetFilters(Case stageCase)
		{
			List<string> filters = new List<string>();
			if (!string.IsNullOrEmpty(stageCase.Target))
			{
				filters.Add("target:" + stageCase.Target);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetStage))
			{
				filters.Add("targetStage:" + stageCase.TargetStage);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetHand))
			{
				filters.Add("oppHand:" + stageCase.TargetHand);
			}
			if (!string.IsNullOrEmpty(stageCase.Filter))
			{
				filters.Add("filter:" + stageCase.Filter);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlaying))
			{
				filters.Add("alsoPlaying:" + stageCase.AlsoPlaying);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingStage))
			{
				filters.Add("alsoPlayingStage:" + stageCase.AlsoPlayingStage);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingHand))
			{
				filters.Add("alsoPlayingHand:" + stageCase.AlsoPlayingHand);
			}
			if (!string.IsNullOrEmpty(stageCase.HasHand))
			{
				filters.Add("hasHand:" + stageCase.HasHand);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalMales))
			{
				filters.Add("totalMales:" + stageCase.TotalMales);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalFemales))
			{
				filters.Add("totalFemales:" + stageCase.TotalFemales);
			}
			return filters;
		}

		/// <summary>
		/// Imports a text file 
		/// </summary>
		/// <param name="filename">File name of the txt to import</param>
		/// <param name="character">Character to insert data into</param>
		public static void Import(string filename, Character character)
		{
			string[] lines = File.ReadAllLines(filename);
			character.Clear();
			List<Case> genericCases = new List<Case>();

			Screen currentScreen = null;
			Epilogue currentEnding = null;
			EndingText currentText = null;

			foreach (string line in lines)
			{
				string data = line.Trim();
				if (data.StartsWith("#") || string.IsNullOrEmpty(data))
					continue;
				string[] kvp = data.Split(new char[] { '=' }, 2);
				if (kvp.Length != 2)
					continue;

				string key = kvp[0].ToLower();
				string value = kvp[1];
				int intValue;
				switch (key)
				{
					case "first":
						character.FirstName = value;
						break;
					case "last":
						character.LastName = value;
						break;
					case "label":
						character.Label = value;
						break;
					case "gender":
						character.Gender = value;
						break;
					case "size":
						character.Size = value;
						break;
					case "intelligence":
						//TODO: Update this once multi-stage intelligence is supported in make_xml.py
						character.Intelligence.Add(new Intelligence(0, value));
						break;
					case "timer":
						if (int.TryParse(value, out intValue))
							character.Stamina = intValue;
						break;
					case "tag":
						character.Tags.Add(value.ToLower());
						break;
					case "pic":
						character.Metadata.Portrait = value;
						break;
					case "height":
						character.Metadata.Height = value;
						break;
					case "from":
						character.Metadata.Source = value;
						break;
					case "writer":
						character.Metadata.Writer = value;
						break;
					case "artist":
						character.Metadata.Artist = value;
						break;
					case "description":
						character.Metadata.Description = value;
						break;
					case "release":
						character.Metadata.ReleaseNumber = value;
						break;
					case "start":
						Case temp = MakeLine(key, value, character, true);
						if (temp != null)
							character.StartingLines.Add(temp.Lines[0]);
						break;
					case "clothes":
						character.Wardrobe.Insert(0, MakeClothing(value));
						break;
					case "ending":
						currentEnding = new Epilogue();
						currentEnding.Title = value;
						currentScreen = null;
						currentText = null;
						character.Endings.Add(currentEnding);
						break;
					case "ending_gender":
						if (currentEnding != null)
						{
							currentEnding.Gender = value;
						}
						break;
					case "screen":
						if (currentEnding != null)
						{
							currentScreen = new Screen();
							currentScreen.Image = value;
							currentEnding.Screens.Add(currentScreen);
							currentText = null;
						}
						break;
					case "text":
						if (currentScreen != null)
						{
							currentText = new EndingText();
							currentText.Content = value;
							currentScreen.Text.Add(currentText);
						}
						break;
					case "x":
						if (currentText != null)
							currentText.X = value;
						break;
					case "y":
						if (currentText != null)
							currentText.Y = value;
						break;
					case "width":
						if (currentText != null)
							currentText.Width = value;
						break;
					case "arrow":
						if (currentText != null)
							currentText.Arrow = value;
						break;
					default:
						//Dialogue
						Case newCase = MakeLine(key, value, character, false);
						if (newCase != null && newCase.Lines.Count > 0 && !string.IsNullOrEmpty(newCase.Lines[0].Text))
						{
							int stage = newCase.Stages[0];

							if (stage == -99)
							{
								//track generic lines separately and figure out what stages to apply to after everything is added

								//Look for an existing case to merge this into
								bool found = false;
								foreach (Case generic in genericCases)
								{
									if (generic.MatchesConditions(newCase))
									{
										generic.Lines.AddRange(newCase.Lines);
										found = true;
										break;
									}
								}

								if (!found)
									genericCases.Add(newCase);
							}
							else
							{
								List<Stage> stages = character.Behavior.Stages;
								while (stages.Count <= stage)
								{
									stages.Add(new Stage(stages.Count));
								}

								//Look for an existing case to merge this into
								bool found = false;
								foreach (Case existing in stages[stage].Cases)
								{
									if (existing.MatchesConditions(newCase))
									{
										existing.Lines.AddRange(newCase.Lines);
										found = true;
										break;
									}
								}
								if (!found)
									stages[stage].Cases.Add(newCase);
							}
						}
						break;
				}
			}

			foreach (Case generic in genericCases)
			{
				//Add a case to every stage that doesn't already have this case
				foreach (Stage stage in character.Behavior.Stages)
				{
					if (TriggerDatabase.UsedInStage(generic.Tag, character, stage.Id))
					{
						bool found = false;
						foreach (var existingCase in stage.Cases)
						{
							if (existingCase.Tag == generic.Tag && existingCase.MatchesConditions(generic))
							{
								found = true;
								break;
							}
						}
						if (!found)
						{
							Case stageInstance = generic.Copy();
							stageInstance.Stages.Add(stage.Id);
							stage.Cases.Add(stageInstance);
						}
					}
				}
			}
		}

		private static Clothing MakeClothing(string value)
		{
			string[] pieces = value.Split(',');
			if (pieces.Length == 4)
			{
				Clothing c = new Clothing();
				c.Name = pieces[0];
				c.Lowercase = pieces[1];
				c.Type = pieces[2];
				c.Position = pieces[3];
				return c;
			}
			else
			{
				return new Clothing();
			}
		}

		private static Case MakeLine(string key, string value, Character character, bool startingLine)
		{
			Case lineCase = new Case();
			DialogueLine line = new DialogueLine();
			bool negative = false;
			if (key.StartsWith("-"))
			{
				negative = true;
				key = key.Substring(1);
			}
			int stage = -99;
			string tag = "";
			int hyphen = key.IndexOf('-');
			if (hyphen >= 1 && hyphen <= 2)
			{
				if (int.TryParse(key.Substring(0, hyphen), out stage))
				{
					if (negative)
					{
						stage *= -1;
						stage += character.Layers + Clothing.ExtraStages;
					}
				}
				tag = key.Substring(hyphen + 1);
			}
			else
			{
				tag = key; //must be generic
			}
			lineCase.Stages.Add(stage);
			string[] targets = tag.Split(',');
			tag = targets[0];
			lineCase.Tag = tag;
			if (tag != "start" && stage != -99 && !TriggerDatabase.UsedInStage(tag, character, stage))
				return null;
			for (int i = 1; i < targets.Length; i++)
			{
				AddTarget(targets[i], lineCase);
			}

			//Image and dialogue
			string[] linePieces = value.Split(new char[] { ',' }, 2);
			string img = "";
			string text = "";
			if (linePieces.Length > 1)
			{
				img = linePieces[0];
				text = linePieces[1];
			}
			else
			{
				text = value;
			}

			line.Image = img;
			line.Text = text;

			if (!startingLine)
			{
				if (stage == -99)
					line = Behaviour.CreateDefaultLine(line);
				else line = Behaviour.CreateStageSpecificLine(line, stage);
			}

			lineCase.Lines.Add(line);
			return lineCase;
		}

		private static void AddTarget(string data, Case lineCase)
		{
			string[] kvp = data.Split(':');
			if (kvp.Length == 2)
			{
				Trigger trigger = TriggerDatabase.GetTrigger(lineCase.Tag);
				string key = kvp[0].ToLower();
				string value = kvp[1];
				//Targets
				if (trigger.HasTarget)
				{
					switch (key)
					{
						case "target":
							lineCase.Target = value;
							break;
						case "opphand":
							lineCase.TargetHand = value;
							break;
						case "filter":
							lineCase.Filter = value;
							break;
						case "targetstage":
							lineCase.TargetStage = value;
							break;
					}
				}

				//Also playing and misc
				switch (key)
				{
					case "alsoplaying":
						lineCase.AlsoPlaying = value;
						break;
					case "alsoplayinghand":
						lineCase.AlsoPlayingHand = value;
						break;
					case "alsoplayingstage":
						lineCase.AlsoPlayingStage = value;
						break;
					case "hashand":
						lineCase.HasHand = value;
						break;
					case "totalfemales":
						lineCase.TotalFemales = value;
						break;
					case "totalmales":
						lineCase.TotalMales = value;
						break;
				}
			}
		}
	}
}
