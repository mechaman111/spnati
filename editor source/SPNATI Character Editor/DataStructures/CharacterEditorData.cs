﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("metadata")]
	/// <summary>
	/// Tracks information the editor wants to know about characters but is not directly used by the game
	/// </summary>
	public class CharacterEditorData : IHookSerialization
	{
		private Character _character;
		private bool _initialized = false;

		[XmlElement("owner")]
		public string Owner;

		[XmlArray("noteworthy")]
		[XmlArrayItem("case")]
		/// <summary>
		/// Cases that have been called out for targeting.
		/// </summary>
		public List<Situation> NoteworthySituations = new List<Situation>();

		[XmlArray("hidden")]
		[XmlArrayItem("id")]
		/// <summary>
		/// List of IDs of hidden cases
		/// </summary>
		public List<int> HiddenCases = new List<int>();

		[XmlArray("responses")]
		[XmlArrayItem("response")]
		/// <summary>
		/// Lines this character has responded to already
		/// </summary>
		public List<SituationResponse> Responses = new List<SituationResponse>();

		[XmlArray("notes")]
		[XmlArrayItem("note")]
		public List<CaseNote> Notes = new List<CaseNote>();
		private Dictionary<int, string> _notes = new Dictionary<int, string>();

		[XmlArray("labels")]
		[XmlArrayItem("label")]
		public List<CaseLabel> Labels = new List<CaseLabel>();
		private Dictionary<int, CaseLabel> _labels = new Dictionary<int, CaseLabel>();

		[XmlArray("prefixes")]
		[XmlArrayItem("prefix")]
		public List<string> IgnoredPrefixes = new List<string>();

		[XmlElement("nextId")]
		/// <summary>
		/// Next unique ID to assign
		/// </summary>
		public int NextId;

		[XmlElement("markers")]
		/// <summary>
		/// Cached information about what markers are set in this character's dialog
		/// </summary>
		public MarkerData Markers;

		private HashSet<string> _usedFolders = new HashSet<string>();
		[XmlIgnore]
		public AutoCompleteStringCollection Folders
		{
			get
			{
				AutoCompleteStringCollection col = new AutoCompleteStringCollection();
				foreach (string folder in _usedFolders)
				{
					col.Add(folder);
				}
				return col;
			}
		}

		/// <summary>
		/// Deferred initialization of things that aren't part of serialization and don't need to exist until the character's lines are being worked on
		/// </summary>
		public void Initialize()
		{
			if (_initialized) { return; }
			_initialized = true;
			if (_character == null) { return; }
			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (workingCase.Id > 0)
				{
					NextId = Math.Max(workingCase.Id, NextId);
					Situation situation = NoteworthySituations.Find(s => s.Id == workingCase.Id);
					if (situation != null)
					{
						situation.LinkedCase = workingCase;
					}
				}
			}

			//for any situations that didn't get linked (because they were created before IDs were a thing), use their copy of a case
			foreach (Situation s in NoteworthySituations)
			{
				if (s.Id == 0)
				{
					s.LinkedCase = s.LegacyCase;
				}
			}
		}

		public void LinkOwner(Character character)
		{
			Owner = character.FolderName;
			_character = character;
			_character.Behavior.CaseRemoved += Behavior_CaseRemoved;
		}

		/// <summary>
		/// Copies instances of editor data on a copied case
		/// </summary>
		/// <param name="copiedCase"></param>
		public void Copy(Case copiedCase)
		{
			if (copiedCase.Id > 0)
			{
				string note = GetNote(copiedCase);
				CaseLabel label = GetLabel(copiedCase);
				copiedCase.Id = 0;
				AssignId(copiedCase);
				if (!string.IsNullOrEmpty(note))
				{
					SetNote(copiedCase, note);
				}
				if (label != null)
				{
					SetLabel(copiedCase, label.Text, label.ColorCode, label.Folder);
				}
			}
		}

		private void Behavior_CaseRemoved(object sender, Case deletedCase)
		{
			//delete anything using this case
			if (deletedCase.Id > 0)
			{
				HiddenCases.Remove(deletedCase.Id);
				for (int i = Responses.Count - 1; i >= 0; i--)
				{
					if (Responses[i].Id == deletedCase.Id)
					{
						Responses.RemoveAt(i);
						break;
					}
				}
				for (int i = NoteworthySituations.Count - 1; i >= 0; i--)
				{
					if (NoteworthySituations[i].Id == deletedCase.Id)
					{
						NoteworthySituations.RemoveAt(i);
						break;
					}
				}

				_notes.Remove(deletedCase.Id);
				_labels.Remove(deletedCase.Id);
			}
		}

		public Situation MarkNoteworthy(Case c)
		{
			if (c.Id == 0)
			{
				AssignId(c);
			}
			Situation line = new Situation(c);
			NoteworthySituations.Add(line);
			return line;
		}

		public void OnBeforeSerialize()
		{
			Markers = _character.Markers;
			Markers.OnBeforeSerialize();

			Notes.Clear();
			foreach (KeyValuePair<int, string> kvp in _notes)
			{
				CaseNote note = new CaseNote() { Id = kvp.Key, Text = kvp.Value };
				Notes.Add(note);
			}

			Labels.Clear();
			foreach (KeyValuePair<int, CaseLabel> kvp in _labels)
			{
				Labels.Add(kvp.Value);
			}
		}

		public void OnAfterDeserialize()
		{
			foreach (Situation c in NoteworthySituations)
			{
				c.OnAfterDeserialize();
			}

			foreach (CaseNote note in Notes)
			{
				_notes[note.Id] = note.Text;
			}

			foreach (CaseLabel label in Labels)
			{
				_labels[label.Id] = label;
				if (!string.IsNullOrEmpty(label.Folder))
				{
					_usedFolders.Add(label.Folder);
				}
			}

			Markers?.OnAfterDeserialize();
		}

		public bool IsCalledOut(Case c)
		{
			if (c.Id == 0) { return false; }
			return NoteworthySituations.Find(s => s.Id == c.Id) != null;
		}

		/// <summary>
		/// Gets whether a case is hidden from the GUI
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public bool IsHidden(Case c)
		{
			if (c.Id == 0) { return false; }
			return HiddenCases.Contains(c.Id);
		}

		/// <summary>
		/// Hides a case from the dialogue editor
		/// </summary>
		/// <param name="c"></param>
		/// <param name="hide"></param>
		public void HideCase(Case c, bool hide)
		{
			bool hidden = IsHidden(c);
			if (hide && hidden || !hide && !hidden) { return; }

			if (c.Id == 0)
			{
				AssignId(c);
			}
			if (hide)
			{
				HiddenCases.Add(c.Id);
			}
			else
			{
				HiddenCases.Remove(c.Id);
			}
		}

		/// <summary>
		/// Gives a case a unique ID
		/// </summary>
		/// <param name="c"></param>
		public void AssignId(Case c)
		{
			if (c.Id > 0) { return; }
			c.Id = ++NextId;
		}

		/// <summary>
		/// Gets whether the character has responded to a particular case before
		/// </summary>
		/// <param name="opponent"></param>
		/// <param name="opponentCase"></param>
		/// <returns></returns>
		public bool HasResponse(Character opponent, Case opponentCase)
		{
			if (opponentCase.Id == 0)
			{
				return false;
			}
			SituationResponse response = Responses.Find(r => r.Opponent == opponent.FolderName && r.OpponentId == opponentCase.Id);
			return response != null;
		}

		public Case GetResponse(Character opponent, Case opponentCase)
		{
			if (opponentCase.Id == 0)
			{
				return null;
			}
			SituationResponse response = Responses.Find(r => r.Opponent == opponent.FolderName && r.OpponentId == opponentCase.Id);
			if (response != null)
			{
				foreach (Case c in _character.Behavior.GetWorkingCases())
				{
					if (c.Id == response.Id)
					{
						return c;
					}
				}
			}
			return null;
		}

		public void MarkResponse(Character opponent, Case opponentCase, Case response)
		{
			if (opponentCase.Id == 0 || HasResponse(opponent, opponentCase))
			{
				return;
			}

			AssignId(response);
			SituationResponse situationResponse = new SituationResponse(response, opponent, opponentCase);
			Responses.Add(situationResponse);
		}

		public void SetNote(Case workingCase, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				_notes.Remove(workingCase.Id);
				return;
			}
			AssignId(workingCase);
			_notes[workingCase.Id] = text;
		}

		public string GetNote(Case workingCase)
		{
			if (workingCase.Id == 0)
			{
				return "";
			}
			string text = "";
			_notes.TryGetValue(workingCase.Id, out text);
			return text;
		}

		public void SetLabel(Case workingCase, string text, string colorCode, string folder)
		{
			if (string.IsNullOrEmpty(text) && (colorCode == null || colorCode == "0") && string.IsNullOrEmpty(folder))
			{
				_labels.Remove(workingCase.Id);
				return;
			}
			AssignId(workingCase);
			CaseLabel label = new CaseLabel()
			{
				Id = workingCase.Id,
				ColorCode = colorCode,
				Text = text,
				Folder = folder,
			};
			_labels[workingCase.Id] = label;
			if (!string.IsNullOrEmpty(folder))
			{
				_usedFolders.Add(folder);
			}
		}

		public CaseLabel GetLabel(Case workingCase)
		{
			if (workingCase.Id == 0)
			{
				return null;
			}
			CaseLabel label = null;
			_labels.TryGetValue(workingCase.Id, out label);
			return label;
		}
	}

	public class Situation
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlElement("description")]
		public string Description;

		/// <summary>
		/// Id of the case this situation corresponds to
		/// </summary>
		[XmlAttribute("id")]
		[DefaultValue(0)]
		public int Id;

		/// <summary>
		/// Targeting priority
		/// </summary>
		[XmlAttribute("priority")]
		[DefaultValue(SituationPriority.None)]
		public SituationPriority Priority;

		[XmlElement("trigger")]
		public Case LegacyCase;

		[XmlIgnore]
		public Case LinkedCase;

		[XmlAttribute("min")]
		public int MinStage;

		[XmlAttribute("max")]
		public int MaxStage;

		public string GetStageString()
		{
			if (MinStage != MaxStage)
			{
				return $"{MinStage}-{MaxStage}";
			}
			else return MinStage.ToString();
		}

		public Situation() { }

		public Situation(Case realCase)
		{
			Id = realCase.Id;
			LinkedCase = realCase;
			MinStage = realCase.Stages.Min(stage => stage);
			MaxStage = realCase.Stages.Max(stage => stage);

			Name = $"Identifying name (ex. {TriggerDatabase.GetLabel(realCase.Tag)})";
			Description = "Description about what's interesting happening with the character (i.e. why should others target this?)";
		}

		public void OnAfterDeserialize()
		{
			if (LegacyCase != null)
			{
				LegacyCase.Stages.Clear();
				for (int i = MinStage; i <= MaxStage; i++)
				{
					LegacyCase.Stages.Add(i);
				}

				if (LegacyCase.AlsoPlayingStage == "")
				{
					LegacyCase.AlsoPlayingStage = null;
				}
			}
		}
	}

	public enum SituationPriority
	{
		None = 0,
		MustTarget = 1,
		Noteworthy = 2,
		FYI = 3
	}

	public class SituationResponse
	{
		[XmlAttribute("id")]
		public int Id;

		[XmlAttribute("opponent")]
		public string Opponent;

		[XmlAttribute("opponentId")]
		public int OpponentId;

		public SituationResponse() { }
		public SituationResponse(Case response, Character opponent, Case opponentCase)
		{
			Id = response.Id;
			Opponent = opponent.FolderName;
			OpponentId = opponentCase.Id;
		}

		public override string ToString()
		{
			return $"{Opponent}-{OpponentId}";
		}
	}
}
