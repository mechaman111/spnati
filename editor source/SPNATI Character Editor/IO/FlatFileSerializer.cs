using Desktop.CommonControls;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
			foreach (StageSpecificValue label in character.Labels)
			{
				lines.Add("label=" + label.Value + (label.Stage != 0 ? "," + label.Stage : ""));
			}
			lines.Add("gender=" + character.Gender);
			lines.Add("size=" + character.Size);
			foreach (StageSpecificValue intelligence in character.Intelligence)
			{
				lines.Add("intelligence=" + intelligence.Value + (intelligence.Stage != 0 ? "," + intelligence.Stage : ""));
			}
			lines.Add("");
			lines.Add("#Number of phases to \"finish\" masturbating");
			lines.Add("timer=" + character.Stamina);
			lines.Add("");
			lines.Add("#Tags describe characters and allow dialogue directed to only characters with these tags, such as: confident, blonde, and british. All tags should be lower case. See tag_list.txt for a list of tags.");
			foreach (CharacterTag tag in character.Tags)
			{
				List<string> attributes = new List<string>();
				if (!string.IsNullOrEmpty(tag.From))
				{
					attributes.Add("from:" + tag.From);
				}
				if (!string.IsNullOrEmpty(tag.To))
				{
					attributes.Add("to:" + tag.To);
				}
				string lineData = tag.Tag;
				if (attributes.Count > 0)
				{
					lineData += "," + string.Join(",", attributes);
				}
				lines.Add("tag=" + lineData);
			}
			lines.Add("");
			lines.Add("#required for meta.xml");
			lines.Add("#select screen image");
			lines.Add("pic=" + Path.GetFileNameWithoutExtension(metadata.Portrait));
			lines.Add("height=" + metadata.Height);
			lines.Add("from=" + metadata.Source);
			lines.Add("writer=" + metadata.Writer);
			lines.Add("artist=" + metadata.Artist);
			lines.Add("description=" + metadata.Description);
			lines.Add("z-layer=" + metadata.Z);
			lines.Add("dialogue-layer=" + metadata.BubblePosition);

			lines.Add("");

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
				lines.Add(string.Format("clothes={0},{1},{2},{3}{4}", clothes.GenericName, clothes.Name, clothes.Type, clothes.Position, clothes.Plural ? ",plural" : ""));
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
			lines.Add("#0-female_must_strip,target:hermione=happy,Looks like your magic doesn't help with poker!");
			#endregion
			List<Case> cases = new List<Case>();

			//Pass 1 - build a mapping of how many non-targeted cases belong to each stage
			Dictionary<Tuple<string, int>, HashSet<Case>> _stageMap = new Dictionary<Tuple<string, int>, HashSet<Case>>();
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				if (c.HasConditions)
					continue;
				foreach (int stage in c.Stages)
				{
					Tuple<string, int> key = new Tuple<string, int>(c.Tag, stage);
					HashSet<Case> set;
					if (!_stageMap.TryGetValue(key, out set))
					{
						set = new HashSet<Case>();
						_stageMap[key] = set;
					}
					_stageMap[key].Add(c);
				}
			}

			//Pass 2 - Mark default cases - those that are the only case for the pertinent key in the map 
			Dictionary<string, Case> defaultCases = new Dictionary<string, Case>();
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				if (c.Stages.Count <= 1 || c.HasConditions)
					continue;

				bool candidate = true;
				foreach (int stage in c.Stages)
				{
					Tuple<string, int> key = new Tuple<string, int>(c.Tag, stage);
					HashSet<Case> set;
					if (!_stageMap.TryGetValue(key, out set) || set.Count != 1 || !set.Contains(c))
					{
						candidate = false;
						break;
					}
				}
				if (candidate)
				{
					//There could be multiple potential defaults (ex one case covers stages 1-3, another covers 4-6), so just use the one that covers the most stages
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
			}

			//Pass 3 - Separate non-default cases into individual cases per stage, and actually mark the default cases as defaults
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				if (c.Stages.Count == 0)
					continue;
				if (defaultCases.ContainsKey(c.Tag) && defaultCases[c.Tag] == c)
				{
					c.IsDefault = true;
					cases.Add(c);
				}
				else
				{
					foreach (int stage in c.Stages)
					{
						foreach (Case set in c.GetConditionSets())
						{
							set.Tag = c.Tag;
							Case stageCase = set.CopyConditions();
							stageCase.Stages.Add(stage);
							stageCase.Id = c.Id;
							stageCase.TriggerSet = c.TriggerSet;
							foreach (var line in c.Lines)
							{
								DialogueLine l = line.Copy();
								stageCase.Lines.Add(l);
							}
							cases.Add(stageCase);
						}
					}
				}
			}

			//Sort cases by stage+group order
			cases.Sort((c1, c2) =>
			{
				//1st key: group
				TriggerDefinition t1 = TriggerDatabase.GetTrigger(c1.Tag);
				TriggerDefinition t2 = TriggerDatabase.GetTrigger(c2.Tag);
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
							int filters1 = c1.HasConditions ? 1 : 0;
							int filters2 = c2.HasConditions ? 1 : 0;
							compare = filters1.CompareTo(filters2);
							if (compare == 0)
							{
								//5th key: actual filters
								compare = Stage.GetSortKey(c1).CompareTo(Stage.GetSortKey(c2));

								if (compare == 0 && c1.Lines.Count > 0 && c2.Lines.Count > 0)
								{
									compare = c1.Lines[0].Text.CompareTo(c2.Lines[0].Text.ToString());
								}
							}
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
					stageId = -9;
				string tag = outputCase.Tag;
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
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
						lines.Add("#stage-generic lines that will be used for every individual stage that doesn't have a line written");
					}
					else
					{
						if (lastStage == -9)
						{
							lines.Add("");
							lines.Add("#stage-specific lines that override the stage-generic ones");
						}
						StageName label = character.LayerToFlatFileName(stageId, appliesToNext);
						if (label?.DisplayName != null)
						{
							lines.Add("");
							lines.Add("#" + label.DisplayName.ToLower());
						}
					}
					lastStage = stageId;
				}
				string stagePrefix = outputCase.IsDefault ? "" : stageId + "-";
				if (outputCase.Stages.Count == 1 && outputCase.IsDefault)
				{
					//If the default case only has one stage, there is effectively no default case
					stagePrefix = outputCase.Stages[0] + "-";
				}
				if (trigger.NoPrefix)
				{
					stagePrefix = "";
				}
				List<string> code = new List<string>();
				code.Add(string.Format("{0}{1}", stagePrefix, outputCase.Tag));
				code.AddRange(GetFilters(outputCase));
				if (outputCase.Id > 0)
				{
					code.Add($"id:{outputCase.Id}");
				}
				string caseCode = string.Join(",", code);

				foreach (var line in outputCase.Lines)
				{
					var defaultLine = line;
					string lineCode = caseCode;
					if (!string.IsNullOrEmpty(defaultLine.Marker))
					{
						lineCode += string.Format(",marker:{0}", defaultLine.Marker);
					}
					if (!string.IsNullOrEmpty(defaultLine.Direction) && defaultLine.Direction != "down")
					{
						lineCode += $",direction:{defaultLine.Direction}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Location))
					{
						lineCode += $",location:{defaultLine.Location}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Layer))
					{
						lineCode += $",dialogue-layer:{defaultLine.Location}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Gender))
					{
						lineCode += $",set-gender:{defaultLine.Gender}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Intelligence))
					{
						lineCode += $",set-intelligence:{defaultLine.Intelligence}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Label))
					{
						lineCode += $",set-label:{defaultLine.Label}";
					}
					if (!string.IsNullOrEmpty(defaultLine.Size))
					{
						lineCode += $",set-size:{defaultLine.Size}";
					}
					if (!string.IsNullOrEmpty(defaultLine.CollectibleId))
					{
						lineCode += $",collectible:{defaultLine.CollectibleId}";
					}
					if (!string.IsNullOrEmpty(defaultLine.CollectibleValue))
					{
						lineCode += $",collectible-value:{defaultLine.CollectibleValue}";
					}
					if (defaultLine.IsMarkerPersistent)
					{
						lineCode += $",persist-marker:1";
					}
					if (defaultLine.OneShotId > 0)
					{
						lineCode += $",one-shot-id:{defaultLine.OneShotId}";
					}
					if (defaultLine.Weight != 1)
					{
						lineCode += $",weight:{defaultLine.Weight.ToString(CultureInfo.InvariantCulture)}";
					}
					string text = String.IsNullOrEmpty(defaultLine.Text) ? "~silent~" : defaultLine.Text;
					lines.Add(string.Format("{0}={1},{2}", lineCode, defaultLine.Pose?.GetFlatFormat() ?? "", text));
				}
			}

			// endings
			lines.Add("\r\n#EPILOGUE/ENDING");
			foreach (Epilogue ending in character.Endings)
			{
				SerializeEpilogue(lines, ending);
			}

			//poses
			lines.Add("\r\n#CUSTOM POSES");
			foreach (Pose pose in character.Poses)
			{
				SerializePose(lines, pose);
			}

			string filename = Path.Combine(Config.GetRootDirectory(character), "edit-dialogue.txt");
			File.WriteAllLines(filename, lines);
			return true;
		}

		private static void SerializeEpilogue(List<string> lines, Epilogue ending)
		{
			lines.Add("");
			lines.Add(string.Format("ending={0}", ending.Title));
			lines.Add(string.Format("\tending_gender={0}", ending.Gender));
			if (ending.HasSpecialConditions)
			{
				List<string> conditions = new List<string>();
				if (!string.IsNullOrEmpty(ending.AlsoPlaying))
				{
					conditions.Add("alsoPlaying:" + ending.AlsoPlaying);
				}
				if (!string.IsNullOrEmpty(ending.PlayerStartingLayers))
				{
					conditions.Add("playerStartingLayers:" + ending.PlayerStartingLayers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingAllMarkers))
				{
					conditions.Add("markers:" + ending.AllMarkers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingNotMarkers))
				{
					conditions.Add("not-markers:" + ending.NotMarkers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingAnyMarkers))
				{
					conditions.Add("any-markers:" + ending.AnyMarkers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingAllMarkers))
				{
					conditions.Add("alsoplaying-markers:" + ending.AlsoPlayingAllMarkers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingNotMarkers))
				{
					conditions.Add("alsoplaying-not-markers:" + ending.AlsoPlayingNotMarkers);
				}
				if (!string.IsNullOrEmpty(ending.AlsoPlayingAnyMarkers))
				{
					conditions.Add("alsoplaying-any-markers:" + ending.AlsoPlayingAnyMarkers);
				}
				lines.Add($"\tending_conditions={string.Join(",", conditions)}");
			}
			if (ending.GalleryImage != null)
			{
				lines.Add(string.Format("\tgallery_image={0}", ending.GalleryImage));
			}
			if (!string.IsNullOrEmpty(ending.Hint))
			{
				lines.Add("\thint=" + ending.Hint);
			}

			foreach (Scene scene in ending.Scenes)
			{
				lines.Add("");
				List<string> sceneAttributes = new List<string>();
				ElementInformation memberInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Scene));
				foreach (FieldInformation fieldInfo in memberInfo.Fields)
				{
					if (fieldInfo.Attribute == null) { continue; }
					string value = fieldInfo.GetValue(scene)?.ToString();
					if (fieldInfo.FieldType == typeof(bool))
					{
						value = (bool)fieldInfo.GetValue(scene) ? "1" : null;
					}
					if (string.IsNullOrEmpty(value))
					{
						continue;
					}
					sceneAttributes.Add($"{fieldInfo.Attribute.AttributeName}:{value}");
				}
				lines.Add($"\tscene={string.Join(",", sceneAttributes)}");

				foreach (Directive directive in scene.Directives)
				{
					List<string> attributes = new List<string>();
					ElementInformation directiveInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Directive));
					foreach (FieldInformation fieldInfo in directiveInfo.Fields)
					{
						if (fieldInfo.Attribute == null) { continue; }
						string value = fieldInfo.GetValue(directive)?.ToString();
						if (fieldInfo.FieldType == typeof(bool))
						{
							value = (bool)fieldInfo.GetValue(directive) ? "1" : null;
						}
						if (string.IsNullOrEmpty(value))
						{
							continue;
						}
						attributes.Add($"{fieldInfo.Attribute.AttributeName}:{value}");
					}
					lines.Add($"\t\tdirective={string.Join(",", attributes)}");
					if (!string.IsNullOrEmpty(directive.Text))
					{
						lines.Add($"\t\t\ttext={directive.Text}");
					}
					else if (!string.IsNullOrEmpty(directive.Title))
					{
						lines.Add($"\t\t\ttext={directive.Title}");
					}

					foreach (Keyframe keyframe in directive.Keyframes)
					{
						List<string> keyAttributes = new List<string>();
						ElementInformation keyframeInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Keyframe));
						foreach (FieldInformation info in keyframeInfo.Fields)
						{
							if (info.Attribute == null) { continue; }
							string value = info.GetValue(keyframe)?.ToString();
							if (info.FieldType == typeof(bool))
							{
								value = (bool)info.GetValue(keyframe) ? "1" : null;
							}
							if (string.IsNullOrEmpty(value))
							{
								continue;
							}
							keyAttributes.Add($"{info.Attribute.AttributeName}:{value}");
						}
						lines.Add($"\t\t\tkeyframe={string.Join(",", keyAttributes)}");
					}

					foreach (Choice choice in directive.Choices)
					{
						List<string> choiceAttributes = new List<string>();
						ElementInformation choiceInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Choice));
						foreach (FieldInformation info in choiceInfo.Fields)
						{
							if (info.Attribute == null) { continue; }
							string value = info.GetValue(choice)?.ToString();
							if (info.FieldType == typeof(bool))
							{
								value = (bool)info.GetValue(choice) ? "1" : null;
							}
							if (string.IsNullOrEmpty(value))
							{
								continue;
							}
							choiceAttributes.Add($"{info.Attribute.AttributeName}:{value}");
						}
						lines.Add($"\t\t\tchoice={string.Join(",", choiceAttributes)}");
						if (!string.IsNullOrEmpty(choice.Caption))
						{
							lines.Add($"\t\t\t\ttext={choice.Caption}");
						}
					}
				}
			}
		}

		private static void SerializePose(List<string> lines, Pose pose)
		{
			lines.Add("");
			lines.Add($"pose={pose.Id}");
			if (!string.IsNullOrEmpty(pose.BaseHeight))
			{
				lines.Add($"\tbase_height={pose.BaseHeight}");
			}
			foreach (Sprite sprite in pose.Sprites)
			{
				List<string> attributes = new List<string>();
				ElementInformation spriteInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Sprite));
				foreach (FieldInformation fieldInfo in spriteInfo.Fields)
				{
					if (fieldInfo.Attribute == null) { continue; }
					string value = fieldInfo.GetValue(sprite)?.ToString();
					if (fieldInfo.FieldType == typeof(bool))
					{
						value = (bool)fieldInfo.GetValue(sprite) ? "1" : null;
					}
					if (string.IsNullOrEmpty(value))
					{
						continue;
					}
					attributes.Add($"{fieldInfo.Attribute.AttributeName}:{value}");
				}
				lines.Add($"\tsprite={string.Join(",", attributes)}");
			}
			foreach (PoseDirective directive in pose.Directives)
			{
				List<string> attributes = new List<string>();
				ElementInformation directiveInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Directive));
				foreach (FieldInformation fieldInfo in directiveInfo.Fields)
				{
					if (fieldInfo.Attribute == null) { continue; }
					string value = fieldInfo.GetValue(directive)?.ToString();
					if (fieldInfo.FieldType == typeof(bool))
					{
						value = (bool)fieldInfo.GetValue(directive) ? "1" : null;
					}
					if (string.IsNullOrEmpty(value))
					{
						continue;
					}
					attributes.Add($"{fieldInfo.Attribute.AttributeName}:{value}");
				}
				lines.Add($"\tdirective={string.Join(",", attributes)}");
				foreach (Keyframe keyframe in directive.Keyframes)
				{
					List<string> keyAttributes = new List<string>();
					ElementInformation keyframeInfo = SpnatiXmlSerializer.GetSerializationInformation(typeof(Keyframe));
					foreach (FieldInformation info in keyframeInfo.Fields)
					{
						if (info.Attribute == null) { continue; }
						string value = info.GetValue(keyframe)?.ToString();
						if (info.FieldType == typeof(bool))
						{
							value = (bool)info.GetValue(keyframe) ? "1" : null;
						}
						if (string.IsNullOrEmpty(value))
						{
							continue;
						}
						keyAttributes.Add($"{info.Attribute.AttributeName}:{value}");
					}
					lines.Add($"\t\tkeyframe={string.Join(",", keyAttributes)}");
				}
			}
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
			if (!string.IsNullOrEmpty(stageCase.TargetStatus))
			{
				filters.Add("targetStatus:" + stageCase.TargetStatus);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetLayers))
			{
				filters.Add("targetLayers:" + stageCase.TargetLayers);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetStartingLayers))
			{
				filters.Add("targetStartingLayers:" + stageCase.TargetStartingLayers);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetTimeInStage))
			{
				filters.Add("targetTimeInStage:" + stageCase.TargetTimeInStage);
			}
			if (!string.IsNullOrEmpty(stageCase.ConsecutiveLosses))
			{
				filters.Add("consecutiveLosses:" + stageCase.ConsecutiveLosses);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetHand))
			{
				filters.Add("oppHand:" + stageCase.TargetHand);
			}
			if (!string.IsNullOrEmpty(stageCase.Filter))
			{
				filters.Add("filter:" + stageCase.Filter);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetSaidMarker))
			{
				filters.Add("targetSaidMarker:" + stageCase.TargetSaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetNotSaidMarker))
			{
				filters.Add("targetNotSaidMarker:" + stageCase.TargetNotSaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetSayingMarker))
			{
				filters.Add("targetSayingMarker:" + stageCase.TargetSayingMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.TargetSaying))
			{
				filters.Add("targetSaying:" + stageCase.TargetSaying.Replace(",", "&comma;"));
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlaying))
			{
				filters.Add("alsoPlaying:" + stageCase.AlsoPlaying);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingStage))
			{
				filters.Add("alsoPlayingStage:" + stageCase.AlsoPlayingStage);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingTimeInStage))
			{
				filters.Add("alsoPlayingTimeInStage:" + stageCase.AlsoPlayingTimeInStage);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingHand))
			{
				filters.Add("alsoPlayingHand:" + stageCase.AlsoPlayingHand);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSaidMarker))
			{
				filters.Add("alsoPlayingSaidMarker:" + stageCase.AlsoPlayingSaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingNotSaidMarker))
			{
				filters.Add("alsoPlayingNotSaidMarker:" + stageCase.AlsoPlayingNotSaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSayingMarker))
			{
				filters.Add("alsoPlayingSayingMarker:" + stageCase.AlsoPlayingSayingMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.AlsoPlayingSaying))
			{
				filters.Add("alsoPlayingSaying:" + stageCase.AlsoPlayingSaying.Replace(",", "&comma;"));
			}
			if (!string.IsNullOrEmpty(stageCase.HasHand))
			{
				filters.Add("hasHand:" + stageCase.HasHand);
			}
			if (!string.IsNullOrEmpty(stageCase.TimeInStage))
			{
				filters.Add("timeInStage:" + stageCase.TimeInStage);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalMales))
			{
				filters.Add("totalMales:" + stageCase.TotalMales);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalFemales))
			{
				filters.Add("totalFemales:" + stageCase.TotalFemales);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalRounds))
			{
				filters.Add("totalRounds:" + stageCase.TotalRounds);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalPlaying))
			{
				filters.Add("totalAlive:" + stageCase.TotalPlaying);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalExposed))
			{
				filters.Add("totalExposed:" + stageCase.TotalExposed);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalNaked))
			{
				filters.Add("totalNaked:" + stageCase.TotalNaked);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalMasturbating))
			{
				filters.Add("totalMasturbating:" + stageCase.TotalMasturbating);
			}
			if (!string.IsNullOrEmpty(stageCase.TotalFinished))
			{
				filters.Add("totalFinished:" + stageCase.TotalFinished);
			}
			if (!string.IsNullOrEmpty(stageCase.SaidMarker))
			{
				filters.Add("saidMarker:" + stageCase.SaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.NotSaidMarker))
			{
				filters.Add("notSaidMarker:" + stageCase.NotSaidMarker);
			}
			if (!string.IsNullOrEmpty(stageCase.AddCharacterTags))
			{
				filters.Add("addCharacterTags:" + stageCase.AddCharacterTags.Replace(',', ':'));
			}
			if (!string.IsNullOrEmpty(stageCase.RemoveCharacterTags))
			{
				filters.Add("removeCharacterTags:" + stageCase.RemoveCharacterTags.Replace(',', ':'));
			}
			if (!string.IsNullOrEmpty(stageCase.Hidden))
			{
				filters.Add("hidden:1");
			}
			if (stageCase.OneShotId > 0)
			{
				filters.Add("oneShotId:" + stageCase.OneShotId);
			}
			if (!string.IsNullOrEmpty(stageCase.CustomPriority))
			{
				filters.Add("priority:" + stageCase.CustomPriority);
			}
			if (stageCase.Conditions != null)
			{
				foreach (var condition in stageCase.Conditions)
				{
					filters.Add(condition.Serialize());
				}
			}
			if (stageCase.Expressions != null)
			{
				foreach (ExpressionTest test in stageCase.Expressions)
				{
					filters.Add($"test:{test.Serialize()}");
				}
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
			CollectibleData collectibles = character.Collectibles;
			ObservableCollection<Nickname> nicknames = character.Nicknames;
			character.Clear();
			List<Case> genericCases = new List<Case>();

			Screen currentScreen = null;
			Epilogue currentEnding = null;
			EndingText currentText = null;
			Scene currentScene = null;
			Directive currentDirective = null;
			Choice currentChoice = null;
			Pose currentPose = null;
			PoseDirective currentPoseDirective = null;

			foreach (string line in lines)
			{
				string data = line.Trim();
				if (data.StartsWith("#") || string.IsNullOrEmpty(data))
					continue;
				int splitIndex = data.LastIndexOf('='); //split on last occurrence of =. This will prevent characters from using = in dialogue, but allow conditional markers to parse properly
				if (splitIndex < 0)
					continue;

				string key = data.Substring(0, splitIndex);
				string value = data.Substring(splitIndex + 1);
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
						if (value.Contains(","))
						{
							string[] pieces = value.Split(',');
							int intStage;
							var intelligence = new StageSpecificValue();
							if (int.TryParse(pieces[1], out intStage))
							{
								intelligence.Value = pieces[0];
								intelligence.Stage = intStage;
								character.Intelligence.Add(intelligence);
							}
						}
						else
						{
							character.Intelligence.Add(new StageSpecificValue(0, value));
						}
						break;
					case "timer":
						if (int.TryParse(value, out intValue))
							character.Stamina = intValue;
						break;
					case "tag":
						value = value.ToLower();
						string[] tagPieces = value.Split(',');
						CharacterTag tag = new CharacterTag(tagPieces[0]);
						for (int i = 1; i < tagPieces.Length; i++)
						{
							string[] attrData = tagPieces[i].Split(new char[] { ':' }, 2);
							if (attrData.Length == 2)
							{
								string attrKey = attrData[0];
								string attrValue = attrData[1];
								switch (attrKey)
								{
									case "from":
										tag.From = attrValue;
										break;
									case "to":
										tag.To = attrValue;
										break;
								}
							}
						}
						character.Tags.Add(tag);
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
					case "z-layer":
						int layer;
						if (int.TryParse(value, out layer))
						{
							character.Metadata.Z = layer;
						}
						break;
					case "dialogue-layer":
						DialogueLayer pos;
						if (Enum.TryParse(value, out pos))
						{
							character.Metadata.BubblePosition = pos;
						}
						break;
					case "start":
						Case temp = MakeLine(key, value, character);
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
						currentScene = null;
						currentDirective = null;
						currentChoice = null;
						currentPose = null;
						currentPoseDirective = null;
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
						else if (currentChoice != null)
						{
							currentChoice.Caption = value;
						}
						else if (currentDirective != null)
						{
							if (currentDirective.DirectiveType == "prompt")
							{
								currentDirective.Title = value;
							}
							else
							{
								currentDirective.Text = value;
							}
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
					case "hint":
						if (currentEnding != null)
						{
							currentEnding.Hint = value;
						}
						break;
					case "gallery_image":
						if (currentEnding != null)
						{
							currentEnding.GalleryImage = value;
						}
						break;
					case "ending_conditions":
						if (currentEnding != null)
						{
							ParseAttributes(currentEnding, value);
						}
						break;
					case "scene":
						if (currentEnding != null)
						{
							currentScene = new Scene();
							currentDirective = null;
							currentChoice = null;
							ParseAttributes(currentScene, value);
							currentEnding.Scenes.Add(currentScene);
						}
						break;
					case "directive":
						if (currentScene != null)
						{
							currentDirective = new Directive();
							currentChoice = null;
							ParseAttributes(currentDirective, value);
							currentScene.Directives.Add(currentDirective);
						}
						else if (currentPose != null)
						{
							currentPoseDirective = new PoseDirective();
							ParseAttributes(currentPoseDirective, value);
							currentPose.Directives.Add(currentPoseDirective);
						}
						break;
					case "keyframe":
						if (currentDirective != null)
						{
							Keyframe keyframe = new Keyframe();
							ParseAttributes(keyframe, value);
							currentDirective.Keyframes.Add(keyframe);
						}
						else if (currentPoseDirective != null)
						{
							Keyframe keyframe = new Keyframe();
							ParseAttributes(keyframe, value);
							currentPoseDirective.Keyframes.Add(keyframe);
						}
						break;
					case "choice":
						if (currentDirective != null)
						{
							Choice choice = new Choice();
							currentChoice = choice;
							ParseAttributes(choice, value);
							currentDirective.Choices.Add(choice);
						}
						break;
					case "pose":
						currentScene = null;
						currentDirective = null;
						currentEnding = null;
						currentChoice = null;
						currentPose = new Pose();
						currentPose.Id = value;
						currentPoseDirective = null;
						character.Poses.Add(currentPose);
						break;
					case "base_height":
						if (currentPose != null)
						{
							currentPose.BaseHeight = value;
						}
						break;
					case "sprite":
						if (currentPose != null)
						{
							Sprite sprite = new Sprite();
							ParseAttributes(sprite, value);
							currentPose.Sprites.Add(sprite);
						}
						break;
					default:
						//Dialogue
						Case newCase = MakeLine(key, value, character);
						if (newCase != null && newCase.Lines.Count > 0)
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
				List<Stage> stages = character.Behavior.Stages;
				while (character.Behavior.Stages.Count < character.Layers + Clothing.ExtraStages)
				{
					stages.Add(new Stage(stages.Count));
				}
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
			character.OnAfterDeserialize(filename);
			character.Collectibles = collectibles;
			character.Nicknames = nicknames;
			character.Behavior.PrepareForEdit(character);
		}

		private static void ParseAttributes<T>(T instance, string attributes)
		{
			ElementInformation info = SpnatiXmlSerializer.GetSerializationInformation(typeof(T));
			string[] pieces = attributes.Split(',');
			foreach (string pair in pieces)
			{
				string[] kvp = pair.Split(new char[] { ':' }, 2);
				if (kvp.Length == 2)
				{
					string key = kvp[0];
					string value = kvp[1];
					FieldInformation field = info.Fields.Find((f) =>
					{
						return f.Attribute?.AttributeName == key;
					});
					if (field != null)
					{
						object objValue = value;
						if (field.FieldType == typeof(bool))
						{
							objValue = (value == "1" ? true : false);
						}
						else if (field.FieldType == typeof(int))
						{
							int v;
							int.TryParse(value, out v);
							objValue = v;
						}

						field.Info.SetValue(instance, objValue);
					}
				}
			}
		}

		private static Clothing MakeClothing(string value)
		{
			string[] pieces = value.Split(',');
			if (pieces.Length >= 4)
			{
				Clothing c = new Clothing();
				c.GenericName = pieces[0];
				c.Name = pieces[1];
				c.Type = pieces[2];
				c.Position = pieces[3];
				if (pieces.Length >= 5)
				{
					c.Plural = pieces[4] == "plural";
				}
				return c;
			}
			else
			{
				return new Clothing();
			}
		}

		private static Case MakeLine(string key, string value, Character character)
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
				AddTarget(targets[i], lineCase, line);
			}
			if (lineCase.Id > 0)
			{
				lineCase.StageId = $"{stage}-{lineCase.Id}";
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

			if (text == "~silent~")
			{
				text = "";
			}

			PoseMapping mapping = character.PoseLibrary.GetFlatFilePose(img);
			if (mapping != null)
			{
				line.Image = mapping.Key;
				line.Pose = mapping;
			}
			line.Text = text;

			lineCase.Lines.Add(line);
			return lineCase;
		}

		private static void AddTarget(string data, Case lineCase, DialogueLine line)
		{
			string[] kvp = data.Split(new char[] { ':' }, 2);
			if (kvp.Length == 2)
			{
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(lineCase.Tag);
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
						case "targetstatus":
							lineCase.TargetStatus = value;
							break;
						case "targetlayers":
							lineCase.TargetLayers = value;
							break;
						case "targetstartinglayers":
							lineCase.TargetStartingLayers = value;
							break;
						case "targettimeinstage":
							lineCase.TargetTimeInStage = value;
							break;
						case "targetsaidmarker":
							lineCase.TargetSaidMarker = value;
							break;
						case "targetnotsaidmarker":
							lineCase.TargetNotSaidMarker = value;
							break;
						case "targetsayingmarker":
							lineCase.TargetSayingMarker = value;
							break;
						case "targetsaying":
							lineCase.TargetSaying = value.Replace("&comma;", ",");
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
					case "alsoplayingtimeinstage":
						lineCase.AlsoPlayingTimeInStage = value;
						break;
					case "alsoplayingsaidmarker":
						lineCase.AlsoPlayingSaidMarker = value;
						break;
					case "alsoplayingsayingmarker":
						lineCase.AlsoPlayingSayingMarker = value;
						break;
					case "alsoplayingnotsaidmarker":
						lineCase.AlsoPlayingNotSaidMarker = value;
						break;
					case "alsoplayingsaying":
						lineCase.AlsoPlayingSaying = value.Replace("&comma;", ",");
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
					case "totalrounds":
						lineCase.TotalRounds = value;
						break;
					case "totalalive":
						lineCase.TotalPlaying = value;
						break;
					case "totalexposed":
						lineCase.TotalExposed = value;
						break;
					case "totalnaked":
						lineCase.TotalNaked = value;
						break;
					case "totalmasturbating":
						lineCase.TotalMasturbating = value;
						break;
					case "totalfinished":
						lineCase.TotalFinished = value;
						break;
					case "consecutivelosses":
						lineCase.ConsecutiveLosses = value;
						break;
					case "timeinstage":
						lineCase.TimeInStage = value;
						break;
					case "saidmarker":
						lineCase.SaidMarker = value;
						break;
					case "notsaidmarker":
						lineCase.NotSaidMarker = value;
						break;
					case "marker":
						line.Marker = value;
						break;
					case "addcharactertags":
						lineCase.AddCharacterTags = value.Replace(':', ',');
						break;
					case "removecharactertags":
						lineCase.RemoveCharacterTags = value.Replace(':', ',');
						break;
					case "hidden":
						lineCase.Hidden = (value == "1" ? "1" : null);
						break;
					case "priority":
						lineCase.CustomPriority = value;
						break;
					case "test":
						ExpressionTest test = new ExpressionTest(value);
						lineCase.Expressions.Add(test);
						break;
					case "location":
						line.Location = value;
						break;
					case "dialogue-layer":
						line.Layer = value;
						break;
					case "direction":
						line.Direction = value;
						break;
					case "oneshotid":
						int oneShotId;
						if (int.TryParse(value, out oneShotId))
						{
							lineCase.OneShotId = oneShotId;
						}
						break;
					case "one-shot-id":
						if (int.TryParse(value, out oneShotId))
						{
							line.OneShotId = oneShotId;
						}
						break;
					case "set-gender":
						line.Gender = value;
						break;
					case "set-intelligence":
						line.Intelligence = value;
						break;
					case "set-label":
						line.Label = value;
						break;
					case "set-size":
						line.Size = value;
						break;
					case "collectible":
						line.CollectibleId = value;
						break;
					case "collectible-value":
						line.CollectibleValue = value;
						break;
					case "persist-marker":
						line.IsMarkerPersistent = (value == "1");
						break;
					case "weight":
						float fval;
						if (float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out fval))
						{
							line.Weight = fval;
						}
						break;
					case "id":
						int id;
						if (int.TryParse(value, out id))
						{
							lineCase.Id = id;
						}
						break;
					default:
						if (key.StartsWith("count-"))
						{
							string filter = key.Substring(6);
							TargetCondition condition = new TargetCondition(filter, value);
							lineCase.Conditions.Add(condition);
						}
						break;
				}
			}
		}
	}
}
