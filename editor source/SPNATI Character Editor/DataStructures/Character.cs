using Desktop;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data representation for behaviour.xml and meta.xml
	/// </summary>
	/// <remarks>
	/// PROPERTY ORDER IS IMPORTANT - Order determines the order attributes are placed in the xml files
	/// </remarks>
	[XmlRoot("opponent", Namespace = "")]
	[XmlHeader("This file was machine generated using the Character Editor {Version} at {Time} on {Date}. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.")]
	public class Character : IHookSerialization, IRecord, IWardrobe
	{
		[XmlIgnore]
		public string Group { get; }

		/// <summary>
		/// Where did this character come from?
		/// </summary>
		[XmlIgnore]
		public Metadata Metadata;

		/// <summary>
		/// Cached information about what markers are set in this character's dialog
		/// </summary>
		[XmlIgnore]
		public MarkerData Markers;

		[XmlIgnore]
		public string FolderName;

		[XmlElement("first")]
		public string FirstName;

		[XmlElement("last")]
		public string LastName;

		[XmlElement("label")]
		public List<StageSpecificValue> Labels;

		[XmlIgnore]
		public string Label // Compatibility property
		{
			get {
				return Labels.Find(l => l.Stage == 0)?.Value;
			}
			set
			{
				StageSpecificValue s0lbl = Labels.Find(l => l.Stage == 0);
				if (s0lbl != null)
				{
					s0lbl.Value = value;
				}
				else
				{
					Labels.Add(new StageSpecificValue(0, value));
				}
			}
		}

		[XmlElement("gender")]
		public string Gender;

		[XmlElement("size")]
		public string Size;

		[XmlElement("timer")]
		public int Stamina;

		[XmlNewLine(Position = XmlNewLinePosition.After)]
		[XmlElement("intelligence")]
		public List<StageSpecificValue> Intelligence;

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<string> Tags;

		[XmlNewLine]
		[XmlArray("start")]
		[XmlArrayItem("state")]
		public List<DialogueLine> StartingLines;

		[XmlNewLine]
		[XmlArray("wardrobe")]
		[XmlArrayItem("clothing")]
		public List<Clothing> Wardrobe = new List<Clothing>();

		[XmlNewLine(XmlNewLinePosition.Both)]
		[XmlElement("behaviour")]
		public Behaviour Behavior = new Behaviour();

		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("epilogue")]
		public List<Epilogue> Endings;

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml;

		private bool _built;

		[XmlIgnore]
		public string Name
		{
			get { return FirstName; }
		}

		[XmlIgnore]
		public string Key
		{
			get { return FolderName; }
			set { FolderName = value; }
		}

		/// <summary>
		/// Current skin in play
		/// </summary>
		[XmlIgnore]
		public Costume CurrentSkin { get; set; }

		public string ToLookupString()
		{
			return $"{Name} [{Key}]";
		}

		public int CompareTo(IRecord other)
		{
			return Label.CompareTo((other as Character).Label);
		}

		public Character()
		{
			FirstName = "New";
			LastName = "Character";
			Labels = new List<StageSpecificValue>();
			Gender = "female";
			Size = "medium";
			Intelligence = new List<StageSpecificValue>();
			Stamina = 15;
			Tags = new List<string>();
			Metadata = new Metadata();
			Markers = new MarkerData();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
		}

		/// <summary>
		/// Clears all data from this character
		/// </summary>
		public void Clear()
		{
			FirstName = "";
			LastName = "";
			Labels.Clear();
			Gender = "";
			Size = "";
			Behavior = new Behaviour();
			Intelligence = new List<StageSpecificValue>();
			Stamina = 15;
			Tags.Clear();
			Metadata = new Metadata();
			Markers = new MarkerData();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
		}

		public override string ToString()
		{
			return Label;
		}

		/// <summary>
		/// DisplayMember only works with properties, so this is for what to display in the LoadCharacterPrompt
		/// </summary>
		public string DisplayName { get { return FolderName; } }

		#region Outfit
		public int Layers
		{
			get { return Wardrobe.Count; }
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToStageName(int layer)
		{
			return LayerToStageName(layer, false, this);
		}

		public StageName LayerToStageName(int layer, bool advancingStage)
		{
			return LayerToStageName(layer, advancingStage, this);
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToStageName(int layer, IWardrobe list)
		{
			return LayerToStageName(layer, false, list);
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer">Layer to name</param>
		/// <param name="advancingStage">True if the name should be in relation to advancing to the next stage, rather than what happened in the previous stage</param>
		public StageName LayerToStageName(int layer, bool advancingStage, IWardrobe list)
		{
			int count = list.Layers;
			if (layer < 0 || layer >= count + Clothing.ExtraStages)
				return null;
			string label = layer.ToString();
			if (advancingStage)
			{
				if (layer < count)
				{
					Clothing clothes = list.GetClothing(Layers - 1 - layer);
					label = "Losing " + clothes.ToString();
				}
			}
			else
			{
				if (layer == 0)
					label = "Fully Clothed";
				else if (layer < count)
				{
					int index = layer - 1;
					Clothing lastClothes = list.GetClothing(Layers - 1 - index);
					label = "Lost " + lastClothes.ToString();
				}
			}
			if (layer == count)
			{
				label = "Naked";
			}
			else if (layer == count + 1)
			{
				label = "Masturbating";
			}
			else if (layer == count + 2)
			{
				label = "Finished";
			}
			return new StageName(layer.ToString(), label);
		}

		/// <summary>
		/// Converts a layer to a user friendly name for the txt flat file
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToFlatFileName(int layer, bool advancingStage)
		{
			string label = layer.ToString();
			if (layer < 0 || layer >= Wardrobe.Count + Clothing.ExtraStages)
			{
				if (layer == -3)
				{
					label = "naked";
				}
				else if (layer == -2)
				{
					label = "masturbating";
				}
				else if (layer == -1)
				{
					label = "finished";
				}
				else
				{
					return null;
				}
			}
			else
			{
				if (advancingStage)
				{
					layer++;
					if (layer <= Wardrobe.Count)
					{
						Clothing clothes = Wardrobe[Layers - layer];
						label = "losing " + clothes.ToString();
					}
					else
					{
						label = "lost all clothing";
					}
				}
				else
				{
					if (layer == 0)
						label = "Fully Clothed";
					else if (layer < Wardrobe.Count)
					{
						int index = layer - 1;
						Clothing lastClothes = Wardrobe[Layers - 1 - index];
						label = "Lost " + lastClothes.ToString();
					}
					else if (layer == Wardrobe.Count)
					{
						label = "Naked";
					}
					else if (layer == Wardrobe.Count + 1)
					{
						label = "Masturbating";
					}
					else if (layer == Wardrobe.Count + 2)
					{
						label = "Finished";
					}
				}
			}
			return new StageName(layer.ToString(), label);
		}

		/// <summary>
		/// Adds a new layer
		/// </summary>
		/// <param name="layer">Layer to add</param>
		/// <returns>Stage of added layer</returns>
		public int AddLayer(Clothing layer)
		{
			Wardrobe.Insert(0, layer);
			return Wardrobe.Count - 1;
		}

		/// <summary>
		/// Removes a layer
		/// </summary>
		/// <param name="layer">Layer to remove</param>
		/// <returns>Stage of removed layer</returns>
		public int RemoveLayer(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if (index >= 0)
				Wardrobe.RemoveAt(index);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Moves the clothing item at the given index down
		/// </summary>
		/// <param name="layer">Layer to move</param>
		/// <returns>Stage of layer before it was moved</returns>
		public int MoveDown(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if (index < 1 || index >= Wardrobe.Count)
				return -1;
			Clothing item = Wardrobe[index];
			Wardrobe.RemoveAt(index);
			Wardrobe.Insert(index - 1, item);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Moves the clothing at the given index up
		/// </summary>
		/// <param name="layer">Layer to move</param>
		/// <returns>Stage of layer before it was moved</returns>
		public int MoveUp(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if (index < 0 || index >= Wardrobe.Count - 1)
				return -1;
			Clothing item = Wardrobe[index];
			Wardrobe.RemoveAt(index);
			Wardrobe.Insert(index + 1, item);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Applies wardrobe changes to the dialogue tree
		/// </summary>
		/// <param name="changes"></param>
		public void ApplyWardrobeChanges(Queue<WardrobeChange> changes)
		{
			Behavior.ApplyWardrobeChanges(this, changes);
		}
		#endregion

		#region Serialization
		/// <summary>
		/// Gets the full path to this character's attachments
		/// </summary>
		/// <returns></returns>
		public string GetAttachmentsDirectory()
		{
			string root = Config.GetString(Settings.GameDirectory);
			return Path.Combine(root, "attachments", FolderName);
		}

		public void OnBeforeSerialize()
		{
			Gender = Gender.ToLower();

			string dir = Config.GetRootDirectory(this);
			foreach (var line in StartingLines)
			{
				string image = Path.GetFileNameWithoutExtension(line.Image) + line.ImageExtension;
				if (!string.IsNullOrEmpty(line.Image) && !char.IsNumber(line.Image[0]) && !File.Exists(Path.Combine(dir, image)))
				{
					line.Image = "0-" + line.Image;
				}
			}
			Behavior.OnBeforeSerialize(this);
			Metadata.PopulateFromCharacter(this);
		}

		public void OnAfterDeserialize()
		{
			Wardrobe.ForEach(c => c.OnAfterDeserialize());
			foreach (var line in StartingLines)
			{
				line.Text = XMLHelper.DecodeEntityReferences(line.Text);
				CacheMarker(line.Marker);
			}
			Behavior.OnAfterDeserialize(this);
		}
		#endregion

		/// <summary>
		/// Called when editing a character in the editor to make sure working fields are built properly.
		/// Working fields are set up lazily so as to not inflict the performance cost on every single character during startup
		/// </summary>
		public void PrepareForEdit()
		{
			if (_built)
				return;
			Behavior.PrepareForEdit(this);
			_built = true;
		}

		/// <summary>
		/// Gets a count of the number of unique, non-targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetGenericLineCount()
		{
			return GetLineCount(LineFilter.Generic);
		}

		/// <summary>
		/// Gets a count of the number of unique targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetTargetedLineCount()
		{
			return GetLineCount(LineFilter.Targeted);
		}

		/// <summary>
		/// Gets a count of the number of unique targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetSpecialLineCount()
		{
			return GetLineCount(LineFilter.Special);
		}

		/// <summary>
		/// Gets a count of unique lines
		/// </summary>
		/// <returns></returns>
		public int GetUniqueLineCount()
		{
			return GetLineCount(LineFilter.Generic | LineFilter.Targeted | LineFilter.Special);
		}

		private int GetLineCount(LineFilter filters)
		{
			int count = 0;
			HashSet<string> lines = new HashSet<string>();
			List<Stage> stages = Behavior.Stages;
			foreach (var stage in stages)
			{
				foreach (var stageCase in stage.Cases)
				{
					bool targeted = stageCase.HasTargetedConditions;
					bool special = stageCase.HasStageConditions;
					bool generic = !stageCase.HasFilters;

					if ((filters & LineFilter.Generic) > 0 && generic ||
						(filters & LineFilter.Targeted) > 0 && targeted ||
						(filters & LineFilter.Special) > 0 && special)
					{
						foreach (var line in stageCase.Lines)
						{
							if (lines.Contains(line.Text))
								continue;
							count++;
							lines.Add(line.Text);
						}
					}
				}
			}
			return count;
		}

		[Flags]
		public enum LineFilter
		{
			None = 0,
			Generic = 1,
			Targeted = 2,
			Special = 4
		}

		public IEnumerable<Case> GetWorkingCasesTargetedAtCharacter(Character character, TargetType targetTypes)
		{
			foreach (var workingCase in Behavior.GetWorkingCases())
			{
				if (IsCaseTargetedAtCharacter(workingCase, character, targetTypes))
				{
					yield return workingCase;
				}
			}
		}

		/// <summary>
		/// Iterates through dialogue that targets another particular character
		/// </summary>
		/// <param name="character">The character being targeted</param>
		/// <returns></returns>
		public IEnumerable<Case> GetCasesTargetedAtCharacter(Character character, TargetType targetTypes)
		{
			List<Stage> stages = Behavior.Stages;
			foreach (var stage in stages)
			{
				foreach (var stageCase in stage.Cases)
				{
					if (IsCaseTargetedAtCharacter(stageCase, character, targetTypes))
					{
						bool addStage = !stageCase.Stages.Contains(stage.Id);
						if (addStage)
							stageCase.Stages.Add(stage.Id);
						yield return stageCase;
						if (addStage)
							stageCase.Stages.Remove(stage.Id);
					}
				}
			}
		}

		private bool IsCaseTargetedAtCharacter(Case stageCase, Character character, TargetType allowedTargetTypes)
		{
			if (allowedTargetTypes == TargetType.None)
				return false;
			bool targeted = false;
			bool targetedByTag = false;
			targeted = stageCase.Target == character.FolderName || stageCase.AlsoPlaying == character.FolderName;
			if (!targeted && (allowedTargetTypes & TargetType.Filter) > 0)
			{
				string gender = stageCase.Tag.StartsWith("male_") ? "male" : stageCase.Tag.StartsWith("female_") ? "female" : null;
				if (gender != null && gender != character.Gender)
					return false;
				string size = stageCase.Tag.Contains("_large_") ? "large" : stageCase.Tag.Contains("_medium_") ? "medium" : stageCase.Tag.Contains("_small_") ? "small" : null;
				if (size != null && character.Size != size)
					return false;

				if (stageCase.Filter != null && character.Tags.Contains(stageCase.Filter))
				{
					targetedByTag = true;
				}
			}
			if ((targeted && (allowedTargetTypes & TargetType.DirectTarget) > 0) || targetedByTag)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets a count of lines targeted towards another character
		/// </summary>
		/// <param name="folderName"></param>
		/// <returns></returns>
		public int GetCharacterUsage(Character character, out int tagCount)
		{
			tagCount = 0;
			int count = 0;
			HashSet<string> lines = new HashSet<string>();
			foreach (var stageCase in GetCasesTargetedAtCharacter(character, TargetType.All))
			{
				foreach (var line in stageCase.Lines)
				{
					if (lines.Contains(line.Text))
						continue;
					if (stageCase.Target != character.FolderName && stageCase.AlsoPlaying != character.FolderName)
						tagCount++;
					else count++;
					lines.Add(line.Text);
				}
			}

			return count;
		}

		/// <summary>
		/// Gets a count of lines targeting a specific tag
		/// </summary>
		/// <returns></returns>
		public int GetTagUsage(string tag, string targetGender)
		{
			int count = 0;
			HashSet<string> lines = new HashSet<string>();
			List<Stage> stages = Behavior.Stages;
			foreach (var stage in stages)
			{
				foreach (var stageCase in stage.Cases)
				{
					if (targetGender != "" && !stageCase.Tag.StartsWith(targetGender))
						continue;
					bool usesTag = (stageCase.Filter == tag);
					if (!usesTag)
					{
						usesTag = stageCase.Conditions.Find(c => c.Filter == tag) != null;
					}
					if (usesTag)
					{
						foreach (var line in stageCase.Lines)
						{
							if (lines.Contains(line.Text))
								continue;
							count++;
							lines.Add(line.Text);
						}
					}
				}
			}
			return count;
		}

		public void RemoveMarkerReference(string marker)
		{
			Markers.RemoveReference(marker);
		}

		public void CacheMarker(string marker)
		{
			Markers.Cache(marker);
		}

		public WardrobeRestrictions GetWardrobeRestrictions()
		{
			//For established characters, lock down changing the layer amount and order since it's hugely disruptive
			OpponentStatus status = Listing.Instance.GetCharacterStatus(FolderName);
			if (status != OpponentStatus.Testing && status != OpponentStatus.Unlisted)
			{
				return WardrobeRestrictions.LayerCount;
			}
			return WardrobeRestrictions.None;
		}

		public Clothing GetClothing(int index)
		{
			return Wardrobe[index];
		}
	}

	/// <summary>
	/// Change to wardrobe, used for updating dialogue stages
	/// </summary>
	public class WardrobeChange
	{
		/// <summary>
		/// Type of change performed
		/// </summary>
		public WardrobeChangeType Change;
		/// <summary>
		/// Index of item being changed
		/// </summary>
		public int Index;

		public WardrobeChange(WardrobeChangeType type, int index)
		{
			Change = type;
			Index = index;
		}
	}

	public enum WardrobeChangeType
	{
		/// <summary>
		/// Item as added at the given index
		/// </summary>
		Add,
		/// <summary>
		/// Item was removed from the given index
		/// </summary>
		Remove,
		/// <summary>
		/// Item was moved up, originally located at the given index
		/// </summary>
		MoveUp,
		/// <summary>
		/// Item was moved down, originally located at the given index
		/// </summary>
		MoveDown
	}

	public class StageSpecificValue
	{
		/// <summary>
		/// Stages this intelligence begins at
		/// </summary>
		[XmlAttribute("stage")]
		[DefaultValue(0)]
		public int Stage;

		/// <summary>
		/// Intelligence level
		/// </summary>
		[XmlText]
		public string Value;

		public StageSpecificValue()
		{
		}

		public StageSpecificValue(int stage, string value)
		{
			Stage = stage;
			Value = value;
		}
	}

	public enum CharacterSource
	{
		/// <summary>
		/// characters that are in the main game or testing tables
		/// </summary>
		Main,
		/// <summary>
		/// finished characters that were moved offline to conserve space
		/// </summary>
		Offline,
		/// <summary>
		/// characters that were never completed
		/// </summary>
		Incomplete
	}
}
