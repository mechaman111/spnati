using Desktop.DataStructures;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Behaviour node of xml file. Contains dialogue
	/// </summary>
	public class Behaviour : BindableObject
	{
		/// <summary>
		/// Raised when a new case is added to the working cases
		/// </summary>
		public event EventHandler<Case> CaseAdded;

		/// <summary>
		/// Raised when a case is removed from the working cases collection
		/// </summary>
		public event EventHandler<Case> CaseRemoved;

		/// <summary>
		/// Raised when the list stages that a working case appears in has been modified
		/// </summary>
		public event EventHandler<Case> CaseModified;

		/// <summary>
		/// Next ID for a case that needs one
		/// </summary>
		[XmlIgnore]
		public int NextId { get; set; }
		[XmlIgnore]
		public int MaxCaseId { get; set; }
		[XmlIgnore]
		public int MaxStageId { get; set; }

		/// <summary>
		/// Only used when serializing or deserializing XML. Cases that share text across stages are split into separate cases per stage here
		/// </summary>
		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("stage")]
		public List<Stage> Stages = new List<Stage>();

		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("trigger")]
		public List<Trigger> Triggers = new List<Trigger>();

		/// <summary>
		/// Flat structure of cases used when editing dialogue. When deserializing, this is constructed from Stages/Triggers. When serializing, Triggers is reconstructed using this info.
		/// </summary>
		/// <remarks>Unlike the Stages property, Case instances here can be shared across stages, ensuring that editing in one will update all applicable stages</remarks>
		[XmlIgnore]
		private List<Case> _workingCases = new List<Case>();

		/// <summary>
		/// Whether the working cases list has been built yet
		/// </summary>
		private bool _builtWorkingCases = false;

		/// <summary>
		/// Character to which this behavior belongs
		/// </summary>
		private Character _character;

		/// <summary>
		/// Called prior to serializing to XML
		/// </summary>
		/// <param name="character"></param>
		public void OnBeforeSerialize(Character character)
		{
			Stages.Clear();
			BuildTriggers(character);
		}

		/// <summary>
		/// Called after deserialization
		/// </summary>
		/// <param name="character"></param>
		public void OnAfterDeserialize(Character character)
		{
			_character = character;
			foreach (Stage stage in Stages)
			{
				PostProcessCases(stage.Cases);
			}
			foreach (Trigger trigger in Triggers)
			{
				PostProcessCases(trigger.Cases);
			}
		}

		public IEnumerable<Case> EnumerateSourceCases()
		{
			if (Triggers.Count > 0)
			{
				foreach (Trigger trigger in Triggers)
				{
					foreach (Case theCase in trigger.Cases)
					{
						yield return theCase;
					}
				}
			}
			else
			{
				foreach (Stage stage in Stages)
				{
					foreach (Case theCase in stage.Cases)
					{
						yield return theCase;
					}
				}
			}
		}

		private void PostProcessCases(List<Case> cases)
		{
			foreach (Case stageCase in cases)
			{
				foreach (DialogueLine line in stageCase.Lines)
				{
					line.Text = XMLHelper.DecodeEntityReferences(line.Text);
					if (string.IsNullOrEmpty(line.Marker))
					{
						line.Marker = null;
					}
					_character.CacheMarker(line.Marker);
				}
			}
		}

		/// <summary>
		/// Called when loading a character to edit
		/// </summary>
		/// <param name="character"></param>
		public void PrepareForEdit(Character character)
		{
			_character = character;
			_workingCases.Clear();
			_builtWorkingCases = false;

			EnsureWorkingCases();

			if (!Config.SuppressDefaults)
			{
				EnsureDefaults(character); //If the input file had any missing dialogue, add it in now
			}
		}

		/// <summary>
		/// Looks through the working cases to locate any Stage+Trigger combos that don't exist, and creates default cases for any missing combinations.
		/// Triggers apply to one or more stages, and a case with no targeted dialogue must exist for each applicable stage
		/// </summary>
		/// <param name="character">Character this behavior belongs to</param>
		public bool EnsureDefaults(Character character)
		{
			_character = character;
			bool modified = false;

			//Generate an index of expected stage+tag combos
			int layers = character.Layers + Clothing.ExtraStages;
			Dictionary<string, HashSet<int>> requiredLineIndex = new Dictionary<string, HashSet<int>>();
			foreach (TriggerDefinition t in TriggerDatabase.Triggers)
			{
				if (t.Optional || t.Tag == TriggerDefinition.StartTrigger)
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
			foreach (var workingCase in character.Behavior._workingCases)
			{
				if (workingCase.HasConditions)
					continue; //A filtered case can't possibly be a default
				string tag = workingCase.Tag;
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
				List<string> tags = new List<string>();
				tags.Add(tag);
				tags.AddRange(trigger.LinkedTriggers);
				foreach (string usedTag in tags)
				{
					HashSet<int> expectedStages;
					if (!requiredLineIndex.TryGetValue(usedTag, out expectedStages))
					{
						continue; //Tag has already been satisfied (or it's an invalid tag)
					}
					foreach (int stage in workingCase.Stages)
					{
						expectedStages.Remove(stage);
					}
					if (expectedStages.Count == 0)
					{
						requiredLineIndex.Remove(usedTag); //Tag's defaults have all been met
					}
				}
			}

			//Finally, add lines for whatever remains in the index
			foreach (var kvp in requiredLineIndex)
			{
				string tag = kvp.Key;
				HashSet<int> remainingStages = kvp.Value;
				Case genericCase = new Case(tag);
				DialogueLine line = DialogueDatabase.CreateDefault(tag);
				genericCase.Lines.Add(line);
				genericCase.AddStages(remainingStages);
				AddWorkingCase(genericCase);
				modified = true;
			}

			return modified;
		}

		/// <summary>
		/// Sorts the WorkingCases so that they appear in consistent order within the tree
		/// </summary>
		public void SortWorking()
		{
			_workingCases.Sort(CompareTags);
		}

		public static int CompareTags(Case c1, Case c2)
		{
			string tag1 = c1.Tag;
			string tag2 = c2.Tag;
			int comparison = 0;
			if (!string.IsNullOrEmpty(tag1) && !string.IsNullOrEmpty(tag2))
			{
				comparison = TriggerDatabase.Compare(tag1, tag2);
			}
			if (comparison == 0)
			{
				comparison = c1.CompareTo(c2);
			}
			return comparison;
		}

		/// <summary>
		/// Returns the number of unique lines of dialogue
		/// </summary>
		public int UniqueLines
		{
			get
			{
				HashSet<string> knownLines = new HashSet<string>();
				foreach (Case c in _workingCases)
				{
					if (!string.IsNullOrEmpty(c.Hidden))
					{
						continue;
					}
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

		public void BuildTriggers(Character character)
		{
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(character);
			Dictionary<string, Trigger> triggers = new Dictionary<string, Trigger>();
			Triggers.Clear();
			using (IEnumerator<Case> enumerator = GetWorkingCases().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Case workingCase = enumerator.Current;
					Trigger trigger = triggers.GetOrAddDefault(workingCase.Tag, () => new Trigger(workingCase.Tag));
					List<Case> sets = new List<Case>();
					sets.Add(workingCase);
					if (workingCase.AlternativeConditions != null)
					{
						sets.AddRange(workingCase.AlternativeConditions);
					}
					Dictionary<int, DialogueLine> lines = new Dictionary<int, DialogueLine>();
					Dictionary<int, List<int>> lineStages = new Dictionary<int, List<int>>();
					Dictionary<int, int> earliestStage = new Dictionary<int, int>();
					foreach (int stage in workingCase.Stages)
					{
						foreach (DialogueLine line in workingCase.Lines)
						{
							DialogueLine stageLine = line.Copy();
							if (stageLine.StageImages.ContainsKey(stage))
							{
								stageLine.Image = stageLine.StageImages[stage]?.Key;
							}
							else
							{
								stageLine.Image = stageLine.Pose?.Key;
							}
							
							int hash = stageLine.GetHashCode();
							if (!lines.ContainsKey(hash))
							{
								lines.Add(hash, stageLine);
							}
							List<int> usedStages = lineStages.GetOrAddDefault(hash, () => new List<int>());
							usedStages.Add(stage);
						}
					}
					Dictionary<int, Case> splitCases = new Dictionary<int, Case>();
					foreach (int hash in lines.Keys)
					{
						DialogueLine line = lines[hash];
						List<int> stages = lineStages[hash];
						int stageHash = ToHash(stages);
						Case c = splitCases.GetOrAddDefault(stageHash, delegate
						{
							Case newCase = new Case(workingCase.Tag);
							if (workingCase.Id > 0)
							{
								newCase.StageId = workingCase.Id.ToString();
							}
							else
							{
								editorData.AssignId(newCase);
								newCase.StageId = newCase.Id.ToString();
							}
							newCase.Stages.AddRange(stages);
							return newCase;
						});
						c.Lines.Add(line);
					}
					foreach (Case set in sets)
					{
						set.TriggerSet = 0;
						foreach (Case lineSet in splitCases.Values)
						{
							Case copy = set.CopyConditions();
							if (workingCase.Id > 0)
							{
								copy.StageId = workingCase.Id.ToString();
							}
							else
							{
								editorData.AssignId(copy);
								copy.StageId = copy.Id.ToString();
							}
							copy.Tag = null;
							copy.Lines = lineSet.Lines;
							copy.Stages = lineSet.Stages;
							trigger.Cases.Add(copy);
						}
					}
				}
			}
			foreach (Trigger trigger in triggers.Values)
			{
				Triggers.Add(trigger);
				trigger.Cases.Sort();
			}

			Triggers.Sort();
		}

		private int ToHash(List<int> stages)
		{
			int hash = 0;
			foreach (int stage in stages)
			{
				hash += 1 << stage;
			}
			return hash;
		}

		/// <summary>
		/// Rebuilds the stage tree from the WorkingCases list
		/// </summary>
		public void BuildStageTree(Character character)
		{
			foreach (var stageCase in _workingCases)
			{
				stageCase.ClearEmptyValues();
			}
			Stages.Clear();

			//Always build 1 stage per layer
			for (int s = 0; s < character.Layers + Clothing.ExtraStages; s++)
			{
				Stages.Add(new Stage(s));
			}

			character.Metadata.CrossGender = false;

			//Put each case into the appropriate stage(s)
			foreach (Case workingCase in _workingCases)
			{
				List<Case> alternativeCases = new List<Case>();
				alternativeCases.Add(workingCase);
				foreach (Case alternate in workingCase.AlternativeConditions)
				{
					//copy lines and stages into the alternate cases
					alternativeCases.Add(alternate);
				}

				foreach (int s in workingCase.Stages)
				{
					if (s >= Stages.Count) { continue; }

					string id = null;
					if (workingCase.Id > 0)
					{
						id = $"{s}-{workingCase.Id}";
					}

					Stage stage = Stages[s];

					//Find a case to merge into
					foreach (Case sourceCase in alternativeCases)
					{
						Case existingCase = stage.Cases.Find(c => c.MatchesConditions(sourceCase) && (c.StageId == id || (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(c.StageId))));
						if (existingCase == null)
						{
							//No case exists yet, so create one
							existingCase = sourceCase.CopyConditions();
							existingCase.StageId = id;
							existingCase.Stages.Add(s); //Not really necessary for serialization, since each case will have a single stage, and will be a child of that stage
							stage.Cases.Add(existingCase);
						}

						//Move the lines over, and make them stage-specific
						foreach (var line in workingCase.Lines)
						{
							DialogueLine stageLine = line.Copy();
							if (stageLine.StageImages.ContainsKey(stage.Id))
							{
								stageLine.Image = stageLine.StageImages[stage.Id]?.Key;
							}
							else
							{
								stageLine.Image = stageLine.Pose?.Key;
							}
							existingCase.Lines.Add(stageLine);

							if (!string.IsNullOrEmpty(line.Gender))
							{
								character.Metadata.CrossGender = true;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Builds the working cases list out of the Stages tree
		/// </summary>
		public void BuildWorkingCases()
		{
			_builtWorkingCases = true;
			_workingCases.Clear();

			if (Triggers.Count > 0)
			{
				BuildWorkingCasesFromTriggers();
			}
			else
			{
				BuildLegacyWorkingCases();
			}

			//Move the legacy Start lines into Selected/Game start cases
			if (_character != null && _character.StartingLines.Count > 0)
			{
				Case selected = new Case("selected");
				selected.AddStage(0);
				selected.Lines.Add(_character.StartingLines[0]);
				AddWorkingCase(selected);

				Case start = new Case("game_start");
				start.AddStage(0);
				if (_character.StartingLines.Count > 1)
				{
					for (int i = 1; i < _character.StartingLines.Count; i++)
					{
						start.Lines.Add(_character.StartingLines[i]);
					}
				}
				else
				{
					start.Lines.Add(_character.StartingLines[0]);
				}
				AddWorkingCase(start);
				_character.StartingLines.Clear();
			}

			if (_character == null) { return; }
			SortWorking();
		}

		private void PerformChecksum()
		{
			if (Stages.Count == 0) { return; }
			List<Case> original = new List<Case>();
			original.AddRange(_workingCases);
			_workingCases.Clear();
			BuildLegacyWorkingCases();
			int count = UniqueLines;
			SortWorking();
			List<Case> old = new List<Case>();
			old.AddRange(_workingCases);
			BuildTriggers(_character);
			_workingCases.Clear();
			BuildWorkingCasesFromTriggers();
			int newCount = UniqueLines;
			SortWorking();
			for (int i = 0; i < Math.Min(_workingCases.Count, old.Count); i++)
			{
				Case oldCase = old[i];
				Case newCase = _workingCases[i];
				if (oldCase.Tag != newCase.Tag || !oldCase.MatchesConditions(newCase) || !oldCase.MatchesNonConditions(newCase) || !oldCase.MatchesStages(newCase, true))
				{
					//cases don't match
				}
			}
			_workingCases = original;
		}

		private void BuildWorkingCasesFromTriggers()
		{
			Dictionary<int, int> setToIdMap = new Dictionary<int, int>();
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
			int nextId = editorData?.NextId ?? 0;
			foreach (Trigger trigger in Triggers)
			{
				Dictionary<int, List<Case>> setCases = new Dictionary<int, List<Case>>();
				Dictionary<int, HashSet<int>> setStages = new Dictionary<int, HashSet<int>>();
				foreach (Case triggerCase in trigger.Cases)
				{
					//set was a poor choice that is out there for early adopters, so convert it to ID
					if (string.IsNullOrEmpty(triggerCase.StageId) && triggerCase.TriggerSet > 0)
					{
						int uniqueId = ++nextId;
						setToIdMap[triggerCase.TriggerSet] = uniqueId;
						triggerCase.StageId = uniqueId.ToString();
					}
					int id;
					int.TryParse(triggerCase.StageId, out id);
					List<Case> cases = setCases.GetOrAddDefault(id, () => new List<Case>());
					HashSet<int> stages = setStages.GetOrAddDefault(id, () => new HashSet<int>());
					triggerCase.Tag = trigger.Id;
					Case rootCase = (cases.Count > 0) ? cases[0] : null;
					Case existingCase = null;
					foreach (Case setCase in cases)
					{
						if (setCase.MatchesConditions(triggerCase))
						{
							existingCase = setCase;
							break;
						}
					}
					if (existingCase == null)
					{
						existingCase = triggerCase.CopyConditions();
						cases.Add(existingCase);
						if (rootCase == null)
						{
							rootCase = existingCase;
							rootCase.OneShotId = existingCase.OneShotId;
							int stageId;
							if (int.TryParse(triggerCase.StageId, out stageId))
							{
								rootCase.Id = stageId;
							}
							existingCase.Tag = trigger.Id;
							_workingCases.Add(existingCase);
						}
						else
						{
							rootCase.AlternativeConditions.Add(existingCase);
						}
					}
					if (existingCase == rootCase)
					{
						foreach (int stage in triggerCase.Stages)
						{
							stages.Add(stage);
						}
						foreach (DialogueLine line in triggerCase.Lines)
						{
							DialogueLine defaultLine = line.Copy();
							int code = line.GetHashCodeWithoutImage();
							bool foundMatch = false;
							foreach (DialogueLine rootLine in rootCase.Lines)
							{
								//look to see if this is an alternative stage-specific image for an existing line
								if (rootLine.GetHashCodeWithoutImage() == code)
								{
									foreach (int lineStage in triggerCase.Stages)
									{
										PoseMapping pose = _character.PoseLibrary.GetPose(line.Image);
										rootLine.StageImages[lineStage] = pose;
									}
									foundMatch = true;
									break;
								}
							}
							if (!foundMatch)
							{
								//not a stage-specific image but a new line
								defaultLine.Pose = _character.PoseLibrary.GetPose(line.Image);
								rootCase.Lines.Add(defaultLine);
							}
						}
					}
				}
				foreach (KeyValuePair<int, List<Case>> kvp in setCases)
				{
					List<Case> list = kvp.Value;
					HashSet<int> stages2 = setStages[kvp.Key];
					list[0].AddStages(stages2);
				}
			}
		}

		private void BuildLegacyWorkingCases()
		{
			Dictionary<int, Case> map = new Dictionary<int, Case>();

			List<Case> buckets = new List<Case>();
			//Make case+line buckets to track which stages each combo appears in
			foreach (Stage stage in Stages)
			{
				foreach (Case stageCase in stage.Cases)
				{
					if (!TriggerDatabase.UsedInStage(stageCase.Tag, _character, stage.Id))
						continue;
					int code = stageCase.GetCode();

					if (stageCase.OneShotId > 0)
					{
						MaxCaseId = Math.Max(MaxCaseId, stageCase.OneShotId);
					}

					int id = 0;
					if (!string.IsNullOrEmpty(stageCase.StageId))
					{
						string[] idPieces = stageCase.StageId.Split('-');
						if (idPieces.Length > 1)
						{
							int.TryParse(idPieces[1], out id);
						}
					}

					HashSet<string> builtCases = new HashSet<string>();
					foreach (DialogueLine line in stageCase.Lines)
					{
						bool addedDuplicate = false;
						var defaultLine = line.Copy();

						if (defaultLine.OneShotId > 0)
						{
							MaxStageId = Math.Max(MaxStageId, defaultLine.OneShotId);
						}

						int lineHash = defaultLine.GetHashCodeWithoutImage();
						int hash = (code * 397) ^ lineHash;
						//See if there's a case that already contains this line, and make one if there isn't
						Case existing;
						if (!map.TryGetValue(hash, out existing))
						{
							existing = stageCase.CopyConditions();
							existing.Id = id;
							map[hash] = existing;
							existing.Lines.Add(defaultLine);
							buckets.Add(existing);
							if (!string.IsNullOrEmpty(defaultLine.Text))
							{
								builtCases.Add(defaultLine.Text);
							}
						}
						else if (builtCases.Contains(defaultLine.Text))
						{
							//If the same text appears multiple times in the same case, make sure to include them all
							existing.Lines.Add(defaultLine);
						}
						if (!existing.Stages.Contains(stage.Id))
						{
							existing.AddStage(stage.Id);

							if (!addedDuplicate)
							{
								//find the existing line if there is one
								foreach (DialogueLine existingLine in existing.Lines)
								{
									if (existingLine.GetHashCodeWithoutImage() == lineHash)
									{
										if (existingLine.Image != defaultLine.Image)
										{
											//if the images are different, remember that difference
											PoseMapping pose = _character.PoseLibrary.GetPose(defaultLine.Image);
											if (pose != null)
											{
												existingLine.StageImages[stage.Id] = pose;
											}
										}
										break;
									}
								}
							}
						}
					}
				}
			}

			//Sort each bucket's Stages set for easier equivalence checks
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
					caseMatchingStages.Id = bucket.Id;
					caseMatchingStages.AddStages(bucket.Stages);
					caseList.Add(caseMatchingStages);
				}
				foreach (DialogueLine line in bucket.Lines)
				{
					caseMatchingStages.Lines.Add(line);
				}
			}

			Dictionary<int, List<Case>> lineCodes = new Dictionary<int, List<Case>>();

			//Done grouping. Put the cases into the WorkingCase list
			foreach (List<Case> list in cases.Values)
			{
				foreach (Case c in list)
				{
					int lineCode = c.GetLineCode();
					List<Case> similarCases;
					bool newCase = true;
					if (lineCodes.TryGetValue(lineCode, out similarCases))
					{
						bool foundSimilar = false;
						foreach (Case similar in similarCases)
						{
							if (similar.MatchesNonConditions(c))
							{
								similar.AlternativeConditions.Add(c);
								foundSimilar = true;
								newCase = false;
								break;
							}
						}
						if (!foundSimilar)
						{
							similarCases.Add(c);
						}
					}
					else
					{
						similarCases = new List<Case>();
						lineCodes[lineCode] = similarCases;
						similarCases.Add(c);
					}
					if (newCase)
					{
						foreach (DialogueLine line in c.Lines)
						{
							line.Pose = _character.PoseLibrary.GetPose(line.Image);
						}
						_workingCases.Add(c);
					}
				}
			}
		}

		/// <summary>
		/// Ensures the working cases list has been built before trying to manipulate it
		/// </summary>
		private void EnsureWorkingCases()
		{
			if (_builtWorkingCases) { return; }
			BuildWorkingCases();

			CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
			editorData?.Initialize();
			DataConversions.ConvertVersion(_character);
		}

		public IEnumerable<Case> GetWorkingCases()
		{
			EnsureWorkingCases();
			foreach (Case c in _workingCases)
			{
				yield return c;
			}
		}

		/// <summary>
		/// Adds a new case to the working cases
		/// </summary>
		/// <param name="theCase">Case to add</param>
		public void AddWorkingCase(Case theCase)
		{
			EnsureWorkingCases();
			_workingCases.Add(theCase);
			CaseAdded?.Invoke(this, theCase);
			NotifyPropertyChanged(nameof(_workingCases));
		}

		/// <summary>
		/// Removes a case from the working cases
		/// </summary>
		/// <param name="theCase"></param>
		public void RemoveWorkingCase(Case theCase)
		{
			EnsureWorkingCases();
			_workingCases.Remove(theCase);
			CaseRemoved?.Invoke(this, theCase);
			NotifyPropertyChanged(nameof(_workingCases));
		}

		private void RemoveWorkingCaseAt(int index)
		{
			EnsureWorkingCases();
			Case theCase = _workingCases[index];
			_workingCases.RemoveAt(index);
			CaseRemoved?.Invoke(this, theCase);
			NotifyPropertyChanged(nameof(_workingCases));
		}

		/// <summary>
		/// Finalizes changes to a case's Stages list
		/// </summary>
		/// <param name="theCase"></param>
		public void ApplyChanges(Case theCase)
		{
			CaseModified?.Invoke(this, theCase);
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into multiple cases that apply to one stage each, one for each stage
		/// </summary>
		/// <param name="original">The case being split</param>
		/// <param name="retainStage">Which stage to keep in the original case object</param>
		public void DivideCaseIntoSeparateStages(Case original, int retainStage)
		{
			foreach (int stage in original.Stages)
			{
				if (stage != retainStage)
				{
					Case stageCase = DuplicateCase(original, false);
					stageCase.AddStage(stage);
					AddWorkingCase(stageCase);
				}
			}
			original.ClearStages();
			original.AddStage(retainStage);
			ApplyChanges(original);
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into two, one taking all the stages except the split stage, and one taking the split stage
		/// </summary>
		/// <param name="original">Case to split</param>
		/// <param name="splitPoint">Stage to split at</param>
		public void SplitCaseStage(Case original, int splitPoint)
		{
			Case beforeSplitCase = DuplicateCase(original, false);
			for (int s = original.Stages.Count - 1; s >= 0; s--)
			{
				if (original.Stages[s] != splitPoint)
				{
					beforeSplitCase.AddStage(original.Stages[s]);
					original.RemoveStage(s);
				}
			}
			ApplyChanges(original);
			beforeSplitCase.Stages.Sort();
			AddWorkingCase(beforeSplitCase);
		}

		/// <summary>
		/// Takes a case that spans multiple stages and splits it into two, one taking all the stages before the split point, and one taking all stages after
		/// </summary>
		/// <param name="original">Case to split</param>
		/// <param name="splitPoint">Stage to split at</param>
		public void SplitCaseAtStage(Case original, int splitPoint)
		{
			if (original.Stages.Count == 0 || original.Stages[0] == splitPoint)
			{
				return;
			}
			Case beforeSplitCase = DuplicateCase(original, false);
			for (int s = original.Stages.Count - 1; s >= 0; s--)
			{
				if (original.Stages[s] < splitPoint)
				{
					beforeSplitCase.AddStage(original.Stages[s]);
					original.RemoveStage(s);
				}
			}
			ApplyChanges(original);
			beforeSplitCase.Stages.Sort();
			AddWorkingCase(beforeSplitCase);
		}

		/// <summary>
		/// Duplicates a case and its applied stages
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public Case DuplicateCase(Case original, bool addToWorking)
		{
			Case copy = original.Copy();
			if (copy.Id != 0)
			{
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
				editorData.Copy(copy);
			}
			if (copy.OneShotId > 0)
			{
				copy.OneShotId = ++MaxCaseId;
			}
			foreach (DialogueLine line in copy.Lines)
			{
				if (line.OneShotId > 0)
				{
					line.OneShotId = ++MaxStageId;
				}
			}
			if (addToWorking)
			{
				copy.AddStages(original.Stages);
				AddWorkingCase(copy);
			}
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
			for (int i = _workingCases.Count - 1; i >= 0; i--)
			{
				Case workingCase = _workingCases[i];
				if (!workingCase.HasConditions && destinationTags.Contains(_workingCases[i].Tag))
				{
					RemoveWorkingCaseAt(i);
				}
			}

			//Step 2: Go through the source cases and duplicate them for each destination
			int end = _workingCases.Count; //caching this off since we'll be adding to this list whie iterating, but don't need to process the new cases
			for (int i = 0; i < end; i++)
			{
				Case sourceCase = _workingCases[i];
				if (!sourceCase.HasConditions && sourceCase.Tag == sourceTag)
				{
					foreach (string tag in destinationTags)
					{
						Case newCase = sourceCase.Copy();
						newCase.AddStages(sourceCase.Stages);
						newCase.Tag = tag;
						AddWorkingCase(newCase);
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
			foreach (Case workingCase in _workingCases)
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
			foreach (Case workingCase in _workingCases)
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
			foreach (Case workingCase in _workingCases)
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
