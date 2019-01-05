using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data for a single layer of clothing
	/// </summary>
	[Serializable]
	public class Clothing
	{
		public const int ExtraStages = 3;

		[XmlAttribute("lowercase")]
		public string GenericName;

		[XmlAttribute("position")]
		public string Position;

		[XmlAttribute("formalName")]
		public string FormalName;

		/// <summary>
		/// Deprecated
		/// </summary>
		[XmlAttribute("proper-name")]
		public string ProperName;

		[XmlAttribute("type")]
		public string Type;

		[XmlAttribute("plural")]
		[DefaultValue(false)]
		public bool Plural;

		public Clothing()
		{
			Position = "upper";
			Type = "major";
			FormalName = "New item";
			GenericName = "new item";
			Plural = false;
		}

		public void OnAfterDeserialize()
		{
			if (FormalName == null || FormalName == "New item")
			{
				FormalName = ProperName;
				ProperName = null;
			}
		}

		public Clothing Copy()
		{
			Clothing copy = new Clothing()
			{
				Position = Position,
				Type = Type,
				FormalName = FormalName,
				GenericName = GenericName,
				ProperName = ProperName,
				Plural = Plural,
			};
			return copy;
		}

		public override string ToString()
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GenericName);
		}
	}

	/// <summary>
	/// Class that has a wardrobe
	/// </summary>
	public interface IWardrobe
	{
		WardrobeRestrictions GetWardrobeRestrictions();
		int Layers { get; }
		Clothing GetClothing(int index);
		void ApplyWardrobeChanges(Queue<WardrobeChange> changes);
		int AddLayer(Clothing layer);
		int RemoveLayer(Clothing layer);
		int MoveUp(Clothing layer);
		int MoveDown(Clothing layer);
	}

	[Flags]
	public enum WardrobeRestrictions
	{
		None = 0,
		/// <summary>
		/// Cannot add, remove, or rearrange layers
		/// </summary>
		LayerCount = 1,
		/// <summary>
		/// Cannot change layer types
		/// </summary>
		LayerTypes = 2,
	}
}
