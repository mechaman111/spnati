using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

		[XmlElement("nextId")]
		/// <summary>
		/// Next unique ID to assign
		/// </summary>
		public int NextId;

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
		}

		public void OnAfterDeserialize()
		{
			foreach (Situation c in NoteworthySituations)
			{
				c.OnAfterDeserialize();
			}
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
		private void AssignId(Case c)
		{
			c.Id = ++NextId;
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
}
