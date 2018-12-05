using System;
using System.Collections.Generic;
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
		[XmlElement("owner")]
		public string Owner;

		[XmlArray("noteworthy")]
		[XmlArrayItem("case")]
		/// <summary>
		/// Cases that have been called out for targeting.
		/// </summary>
		public List<Situation> NoteworthySituations = new List<Situation>();

		public Situation MarkNoteworthy(Case c)
		{
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
	}

	public class Situation
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlElement("description")]
		public string Description;

		[XmlElement("trigger")]
		public Case Case;

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
			Case = realCase.Copy();
			MinStage = realCase.Stages.Min(stage => stage);
			MaxStage = realCase.Stages.Max(stage => stage);
			OnAfterDeserialize();

			Name = $"Identifying name (ex. {TriggerDatabase.GetLabel(realCase.Tag)})";
			Description = "Description about what's interesting happening with the character (i.e. why should others target this?)";
		}

		public void OnAfterDeserialize()
		{
			Case.Stages.Clear();
			for (int i = MinStage; i <= MaxStage; i++)
			{
				Case.Stages.Add(i);
			}
		}
	}
}
