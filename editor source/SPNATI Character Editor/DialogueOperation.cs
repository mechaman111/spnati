using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Represents embedded dialogue operations.
	/// </summary>
	public abstract class DialogueOperation
	{
		public DialogueOperation() { }
	}

	[Serializable]
	/// <summary>
	/// Represents a player attribute dialogue operation.
	/// </summary>
	public class PlayerAttributeOperation : DialogueOperation
	{
		[XmlAttribute("attr")]
		public string Attribute;
	
		[XmlAttribute("value")]
		public string Value;

		public PlayerAttributeOperation() { }

		public PlayerAttributeOperation Copy()
		{
			PlayerAttributeOperation clone = MemberwiseClone() as PlayerAttributeOperation;
			return clone;
		}

		public override int GetHashCode()
		{
			int hash = (Attribute ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Value ?? string.Empty).GetHashCode();
			return hash;
		}
	}

	[Serializable]
	/// <summary>
	/// Represents a forfeit attribute dialogue operation.
	/// </summary>
	public class ForfeitOperation : DialogueOperation
	{
		[XmlAttribute("attr")]
		public string Attribute;

		[XmlAttribute("op")]
		public string Operator;

		[XmlAttribute("value")]
		public string Value;

		[XmlAttribute("heavy")]
		public string HeavyOperation;

		public ForfeitOperation() { }

		public ForfeitOperation Copy()
		{
			ForfeitOperation clone = MemberwiseClone() as ForfeitOperation;
			return clone;
		}

		public override int GetHashCode()
		{
			int hash = (Attribute ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Operator ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Value ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (HeavyOperation ?? string.Empty).GetHashCode();
			return hash;
		}
	}

	[Serializable]
	/// <summary>
	/// Represents a nickname change dialogue operation.
	/// </summary>
	public class NicknameOperation : DialogueOperation
	{
		[XmlAttribute("character")]
		public string Character;

		[XmlAttribute("op")]
		public string Operator;

		[XmlAttribute("name")]
		public string Name;

		[DefaultValue(1)]
		[XmlAttribute("weight")]
		public int Weight;

		public NicknameOperation() { }

		public NicknameOperation Copy()
		{
			NicknameOperation clone = MemberwiseClone() as NicknameOperation;
			return clone;
		}

		public override int GetHashCode()
		{
			int hash = (Character ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Operator ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Name ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ Weight.GetHashCode();
			return hash;
		}
	}

	[Serializable]
	/// <summary>
	/// Represents a collection of embedded dialogue operations.
	/// </summary>
	public class DialogueOperations
	{
		[XmlElement("player", IsNullable = false)]
		public List<PlayerAttributeOperation> PlayerOps;

		[XmlElement("forfeit", IsNullable = false)]
		public List<ForfeitOperation> ForfeitOps;

		[XmlElement("nickname", IsNullable = false)]
		public List<NicknameOperation> NicknameOps;

		public DialogueOperations() {
			PlayerOps = new List<PlayerAttributeOperation>();
			ForfeitOps = new List<ForfeitOperation>();
			NicknameOps = new List<NicknameOperation>();
		}

		public DialogueOperations Copy()
		{
			DialogueOperations clone = new DialogueOperations();

			clone.PlayerOps = new List<PlayerAttributeOperation>();
			clone.ForfeitOps = new List<ForfeitOperation>();
			clone.NicknameOps = new List<NicknameOperation>();

			foreach (PlayerAttributeOperation op in PlayerOps)
			{
				clone.PlayerOps.Add(op.Copy());
			}

			foreach (ForfeitOperation op in ForfeitOps)
			{
				clone.ForfeitOps.Add(op.Copy());
			}

			foreach (NicknameOperation op in NicknameOps)
			{
				clone.NicknameOps.Add(op.Copy());
			}

			return clone;
		}

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (PlayerAttributeOperation op in PlayerOps)
			{
				hash = (hash * 397) ^ op.GetHashCode();
			}

			foreach (ForfeitOperation op in ForfeitOps)
			{
				hash = (hash * 397) ^ op.GetHashCode();
			}

			foreach (NicknameOperation op in NicknameOps)
			{
				hash = (hash * 397) ^ op.GetHashCode();
			}

			return hash;
		}

		public bool IsEmpty()
		{
			return (PlayerOps.Count == 0) && (ForfeitOps.Count == 0) && (NicknameOps.Count == 0);
		}
	}
}