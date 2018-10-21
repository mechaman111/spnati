using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using SPNATI_Character_Editor.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Behaviour node of xml file. Contains dialogue
	/// </summary>
	public class Behaviour
	{
		/// <summary>
		/// Only used when serializing or deserializings XML. Cases that share text across stages are split into separate cases per stage here
		/// </summary>
		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("stage")]
		public List<Stage> Stages = new List<Stage>();

		/// <summary>
		/// Flat structure of cases used when editing dialogue. When deserializing, this is constructed from Stages. When serializing, Stages is reconstructed using this info.
		/// </summary>
		/// <remarks>Unlike the Stages property, Case instances here can be shared across stages, ensuring that editing in one will update all applicable stages</remarks>
		[XmlIgnore]
		public List<Case> WorkingCases = new List<Case>();

		/// <summary>
		/// Called prior to serializing to XML
		/// </summary>
		/// <param name="character"></param>
		public void OnBeforeSerialize(Character character)
		{
			BuildStageTree(character);
		}

		/// <summary>
		/// Called after deserialization in order to build the WorkingCases structure
		/// </summary>
		/// <param name="character"></param>
		public void OnAfterDeserialize(Character character)
		{
			foreach (var stage in Stages)
			{
				foreach (var stageCase in stage.Cases)
				{
					foreach (var line in stageCase.Lines)
					{
						line.Text = XMLHelper.DecodeEntityReferences(line.Text);
						if (string.IsNullOrEmpty(line.Marker))
							line.Marker = null;
						character.CacheMarker(line.Marker);
					}
				}
			}
		}

		/// <summary>
		/// Called when loading a character to edit
		/// </summary>
		/// <param name="character"></param>
		public void PrepareForEdit(Character character)
		{
			WorkingCases = new List<Case>();

			BuildWorkingCases(character);

			EnsureDefaults(character); //If the input file had any missing dialogue, add it in now
		}

		/// <summary>
		/// Converts a generic line of dialogue into one specific for the given stage (i.e gives the image the applicable prefix)
		/// </summary>
		/// <param name="line">Line to convert</param>
		/// <param name="stage">Stage to convert to</param>
		/// <returns></returns>
		public static DialogueLine CreateStageSpecificLine(DialogueLine line, int stage, Character character)
		{
			DialogueLine copy = line.Copy();
			string extension = line.ImageExtension;
			if (string.IsNullOrEmpty(extension))
			{
				extension = ".png";
			}
			if (copy.Image != null)
			{
				copy.Image = Path.GetFileNameWithoutExtension(copy.Image);
			}
			string path = character != null ? Config.GetRootDirectory(character) : "";
			if (!copy.Image.StartsWith(stage + "-") && !File.Exists(Path.Combine(path, copy.Image + extension)))
			{
				copy.Image = stage + "-" + copy.Image;
			}
			copy.Image += extension;
			copy.ImageExtension = extension;
			copy.Text = line.Text?.Trim();
			return copy;
		}

		/// <summary>
		/// Trashes all dialogue and replaces it with the default template
		/// NB: This method is not in use. If used, should be checked if it needs
		/// modification to support optional tags.
		/// </summary>
		/// <param name="character"></param>
		public void RestoreToDefaults(Character character)
		{
			WorkingCases.Clear();
			int layerCount = character.Layers + Clothing.ExtraStages;

			character.StartingLines.Clear();
			Tuple<string, string> start = DialogueDatabase.GetTemplate(Trigger.StartTrigger);
			character.StartingLines.Add(new DialogueLine(start.Item1, start.Item2));

			foreach (var tag in TriggerDatabase.GetTags())
			{
				if (tag == Trigger.StartTrigger)
					continue;
				Case workingCase = new Case(tag);
				Tuple<string, string> template = DialogueDatabase.GetTemplate(tag);
				DialogueLine line = new DialogueLine(template.Item1, template.Item2);
				for (int i = 0; i < layerCount; i++)
				{
					if (TriggerDatabase.UsedInStage(tag, character, i))
						workingCase.Stages.Add(i);
				}
				workingCase.Lines.Add(line);
				WorkingCases.Add(workingCase);
			}
		}

		/// <summary>
		/// Looks through the working cases to locate any Stage+Trigger combos that don't exist, and creates default cases for any missing combinations.
		/// Triggers apply to one or more stages, and a case with no targeted dialogue must exist for each applicable stage
		/// </summary>
		/// <param name="character">Character this behavior belongs to</param>
		public bool EnsureDefaults(Character character)
		{
			bool modified = false;
			if (character.StartingLines.Count == 0)
			{
				Tuple<string, string> start = DialogueDatabase.GetTemplate(Trigger.StartTrigger);
				character.StartingLines.Add(new DialogueLine(start.Item1, start.Item2));
				modified = true;
			}

			//Generate an index of expecte stage+tag combos
			int layers = character.Layers + Clothing.ExtraStages;
			Dictionary<string, HashSet<int>> requiredLineIndex = new Dictionary<string, HashSet<int>>();
			foreach (Trigger t in TriggerDatabase.Triggers)
			{
				if (t.Optional || t.Tag == Trigger.StartTrigger)
					continue;
				HashSet<int> stages = new HashSet<int>();
				for (int stage = 0; stage < layers; stage++)
				{
					if (TriggerDatabase.UsedInStage(t.Tag, character, stage))
					{
						stages.Add(stage);
					}
				}
				requiredLineIndex[t.Tag] = stages;
			}

			//Loop through the cases and remove any satisfied tags from the index
			foreach (var workingCase in character.Behavior.WorkingCases)
			{
				if (workingCase.HasFilters)
					continue; //A filtered case can't possibly be a default
				string tag = workingCase.Tag;
				HashSet<int> expectedStages;
				if (!requiredLineIndex.TryGetValue(tag, out expectedStages))
					continue; //Tag has already been satisfied (or it's an invalid tag)
				foreach (int stage in workingCase.Stages)
				{
					expectedStages.Remove(stage);
				}
				if (expectedStages.Count == 0)
					requiredLineIndex.Remove(tag); //Tag's defaults have all been met
			}

			//Finally, add lines for whatever remains in the index
			foreach (var kvp in requiredLineIndex)
			{
				string tag = kvp.Key;
				HashSet<int> remainingStages = kvp.Value;
				Case genericCase = new Case(tag);
				DialogueLine line = DialogueDatabase.CreateDefault(tag);
				genericCase.Lines.Add(line);
				foreach (int stage in remainingStages)
				{
					genericCase.Stages.Add(stage);
				}
				WorkingCases.Add(genericCase);
				modified = true;
			}

			return modified;
		}

		/// <summary>
		/// Sorts the WorkingCases so that they appear in consistent order within the tree
		/// </summary>
		public void SortWorking()
		{
			WorkingCases.Sort((c1, c2) =>
			{
				string tag1 = c1.Tag;
				string tag2 = c2.Tag;
				int comparison = TriggerDatabase.Compare(tag1, tag2);
				if (comparison == 0)
				{
					comparison = c1.CompareTo(c2);
				}
				return comparison;
			});
		}

		/// <summary>
		/// Creates a generic line from a stage specific one (i.e. strips the stage prefix from the image)
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static DialogueLine CreateDefaultLine(DialogueLine line)
		{
			DialogueLine copy = line.Copy();
			string extension = line.ImageExtension ?? Path.GetExtension(line.Image);
			copy.ImageExtension = extension;
			line.ImageExtension = extension;
			copy.Image = Path.GetFileNameWithoutExtension(DialogueLine.GetDefaultImage(line.Image));
			copy.Text = line.Text.Trim();
			return copy;
		}

		/// <summary>
		/// Returns the number of unique lines of dialogue
		/// </summary>
		public int UniqueLines
		{
			get
			{
				HashSet<string> knownLines = new HashSet<string>();
				foreach (Case c in WorkingCases)
				{
					foreach (var line in c.Lines)
					{
						if (knownLines.Contains(line.Text))
							continue;
						knownLines.Add(line.Text);
					}
				}
				return knownLines.Count;
			}
		}

		/// <summary>
		/// Rebuilds the stage tree from the WorkingCases list
		/// </summary>
		public void BuildStageTree(Character character)
		{
			foreach (var stageCase in WorkingCases)
			{
				stageCase.ClearEmptyValues();
			}
			Stages.Clear();

			//Always build 1 stage per layer
			for (int s = 0; s < character.Layers + Clothing.ExtraStages; s++)
			{
				Stages.Add(new Stage(s));
			}

			//Put each case into the appropriate stage(s)
			foreach (var workingCase in WorkingCases)
			{
				foreach (int s in workingCase.Stages)
				{
					Stage stage = Stages[s];

					//Find a case to merge into
					Case existingCase = stage.Cases.Find(c => c.MatchesConditions(workingCase));
					if (existingCase == null)
					{
						//No case exists yet, so create one
						existingCase = workingCase.CopyConditions();
						existingCase.Stages.Add(s); //Not really necessary for serialization, since each case will have a single stage, and will be a child of that stage
						stage.Cases.Add(existingCase);
					}

					//Move the lines over, and make them stage-specific
					foreach (var line in workingCase.Lines)
					{
						existingCase.Lines.Add(CreateStageSpecificLine(line, s, character));
					}
				}
			}

			//Sort cases to try to match make_xml's output
			foreach (var stage in Stages)
			{
				stage.Cases.Sort(stage.SortCases);
			}
		}

		/// <summary>
		/// Builds the working cases list out of the Stages tree
		/// </summary>
		public void BuildWorkingCases(Character character)
		{
			WorkingCases.Clear();
			Dictionary<int, Case> map = new Dictionary<int, Case>();

			List<Case> buckets = new List<Case>();
			//Make case+line buckets to track which stages each combo appears in
			foreach (Stage stage in Stages)
			{
				foreach (Case stageCase in stage.Cases)
				{
					if (!TriggerDatabase.UsedInStage(stageCase.Tag, character, stage.Id))
						continue;
					int code = stageCase.GetCode();
					foreach (DialogueLine line in stageCase.Lines)
					{
						var defaultLine = CreateDefaultLine(line);
						int hash = defaultLine.GetHashCode();
						hash = code + hash;
						//See if there's a case that already contains this line, and make one if there isn't
						Case existing;
						if (!map.TryGetValue(hash, out existing))
						{
							existing = stageCase.CopyConditions();
							map[hash] = existing;
							existing.Lines.Add(defaultLine);
							buckets.Add(existing);
						}
						existing.Stages.Add(stage.Id);
					}
				}
			}

			//Sort each buckets's Stages set for easier equivalence checks
			foreach (Case c in buckets)
			{
				c.Stages.Sort();
			}

			//Merge buckets whose case+stages match
			Dictionary<int, List<Case>> cases = new Dictionary<int, List<Case>>();
			foreach (Case bucket in buckets)
			{
				int code = bucket.GetCode();
				List<Case> caseList;
				if (!cases.TryGetValue(code, out caseList))
				{
					caseList = new List<Case>();
					cases[code] = caseList;
				}
				Case caseMatchingStages = caseList.Find(c => c.Stages.SequenceEqual(bucket.Stages));
				if (caseMatchingStages == null)
				{
					caseMatchingStages = bucket.CopyConditions();
					caseMatchingStages.Stages.AddRange(bucket.Stages);
					caseList.Add(caseMatchingStages);
				}
				foreach (var line in bucket.Lines)
				{
					caseMatchingStages.Lines.Add(line);
				}
			}

			//Done grouping. Put the cases into the WorkingCase list
			foreach (List<Case> list in cases.Values)
			{
				WorkingCases.AddRange(list);
			}
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into multiple cases that apply to one stage each, one for each stage
		/// </summary>
		/// <param name="original"></param>
		/// <param name="retainStage"></param>
		public void DivideCaseIntoSeparateStages(Case original, int retainStage)
		{
			foreach (int stage in original.Stages)
			{
				if (stage != retainStage)
				{
					Case stageCase = original.Copy();
					stageCase.Stages.Add(stage);
					WorkingCases.Add(stageCase);
				}
			}
			original.Stages.Clear();
			original.Stages.Add(retainStage);
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into two, one taking all the stages except the split stage, and one taking the split stage
		/// </summary>
		/// <param name="original">Case to split</param>
		/// <param name="splitPoint">Stage to split at</param>
		public void SplitCaseStage(Case original, int splitPoint)
		{
			Case beforeSplitCase = original.Copy();
			for (int s = original.Stages.Count - 1; s >= 0; s--)
			{
				if (original.Stages[s] != splitPoint)
				{
					beforeSplitCase.Stages.Add(original.Stages[s]);
					original.Stages.RemoveAt(s);
				}
			}
			WorkingCases.Add(beforeSplitCase);
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into two, one taking all the stages before the split point, and one taking all stages after
		/// </summary>
		/// <param name="original">Case to split</param>
		/// <param name="splitPoint">Stage to split at</param>
		public void SplitCaseAtStage(Case original, int splitPoint)
		{
			Case beforeSplitCase = original.Copy();
			for (int s = original.Stages.Count - 1; s >= 0; s--)
			{
				if (original.Stages[s] < splitPoint)
				{
					beforeSplitCase.Stages.Add(original.Stages[s]);
					original.Stages.RemoveAt(s);
				}
			}
			WorkingCases.Add(beforeSplitCase);
		}

		/// <summary>
		/// Duplicates a case and its applied stages
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public Case DuplicateCase(Case original)
		{
			Case copy = original.Copy();
			copy.Stages.AddRange(original.Stages);
			WorkingCases.Add(copy);
			return copy;
		}

		/// <summary>
		/// Replaces all non-targeted dialogue in the destination cases with that from the source. Affects all stages
		/// </summary>
		/// <param name="sourceTag">Trigger tag for the case to copy from</param>
		/// <param name="destinationTags">Tags to replace</param>
		public void BulkReplace(string sourceTag, HashSet<string> destinationTags)
		{
			//Step 1: Throw away all non-targeted cases from the destinations
			for (int i = WorkingCases.Count - 1; i >= 0; i--)
			{
				Case workingCase = WorkingCases[i];
				if (!workingCase.HasFilters && destinationTags.Contains(WorkingCases[i].Tag))
				{
					WorkingCases.RemoveAt(i);
				}
			}

			//Step 2: Go through the source cases and duplicate them for each destination
			int end = WorkingCases.Count; //caching this off since we'll be adding to this list whie iterating, but don't need to process the new cases
			for (int i = 0; i < end; i++)
			{
				Case sourceCase = WorkingCases[i];
				if (!sourceCase.HasFilters && sourceCase.Tag == sourceTag)
				{
					foreach (string tag in destinationTags)
					{
						Case newCase = sourceCase.Copy();
						newCase.Stages.AddRange(sourceCase.Stages);
						newCase.Tag = tag;
						WorkingCases.Add(newCase);
					}
				}
			}
		}

		/// <summary>
		/// Applies wardrobe changes to the dialogue tree
		/// </summary>
		/// <param name="changes"></param>
		public void ApplyWardrobeChanges(Character character, Queue<WardrobeChange> changes)
		{
			while (changes.Count > 0)
			{
				WardrobeChange change = changes.Dequeue();
				switch (change.Change)
				{
					case WardrobeChangeType.Add:
						InsertStage(change.Index);
						break;
					case WardrobeChangeType.Remove:
						RemoveStage(change.Index);
						break;
					case WardrobeChangeType.MoveDown:
						SwapStages(change.Index, change.Index - 1);
						break;
					case WardrobeChangeType.MoveUp:
						SwapStages(change.Index, change.Index + 1);
						break;
				}
			}

			EnsureDefaults(character);
		}

		/// <summary>
		/// Inserts a stage at the given index, shifting everything after it up
		/// </summary>
		/// <param name="index"></param>
		private void InsertStage(int index)
		{
			foreach (Case workingCase in WorkingCases)
			{
				List<int> stages = workingCase.Stages;
				for (int i = 0; i < stages.Count; i++)
				{
					int stage = stages[i];
					if (stage >= index)
						stages[i] = stage + 1;
				}
			}
		}

		/// <summary>
		/// Removes a stage from the given index, shifting everything after it down
		/// </summary>
		/// <param name="index"></param>
		private void RemoveStage(int index)
		{
			foreach (Case workingCase in WorkingCases)
			{
				List<int> stages = workingCase.Stages;
				for (int i = stages.Count - 1; i >= 0; i--)
				{
					int stage = stages[i];
					if (stage > index)
						stages[i] = stage - 1;
					else if (stage == index)
						stages.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Swaps the position of two stages
		/// </summary>
		/// <param name="index1"></param>
		/// <param name="index2"></param>
		private void SwapStages(int index1, int index2)
		{
			foreach (Case workingCase in WorkingCases)
			{
				List<int> stages = workingCase.Stages;
				for (int i = 0; i < stages.Count; i++)
				{
					int stage = stages[i];
					if (stage == index1)
						stages[i] = index2;
					else if (stage == index2)
						stages[i] = index1;
				}
			}
		}
	}
}
