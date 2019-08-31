﻿using Desktop;
using Desktop.DataStructures;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml;
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
	public class Character : BindableObject, IHookSerialization, IRecord, IWardrobe, ISkin, IDirtiable
	{
		[XmlElement("version")]
		/// <summary>
		/// What version of the editor this was last saved under. Used for performing one-time data conversions when necessary.
		/// </summary>
		public string Version { get; set; }
		[XmlIgnore]
		public EditorSource Source;

		private bool _dirty;
		[XmlIgnore]
		public bool IsDirty
		{
			get { return _dirty; }
			set
			{
				_dirty = value;
				OnDirtyChanged?.Invoke(this, _dirty);
			}
		}

		[XmlIgnore]
		public PoseMap PoseLibrary;

		[XmlIgnore]
		public string Group { get; }

		[XmlIgnore]
		public DateTime LastUpdate;

		/// <summary>
		/// Where did this character come from?
		/// </summary>
		[XmlIgnore]
		public Metadata Metadata
		{
			get { return Get<Metadata>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Cached information about what markers are set in this character's dialog
		/// </summary>
		[XmlIgnore]
		public MarkerData Markers;

		[XmlIgnore]
		public string FolderName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("first")]
		public string FirstName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("last")]
		public string LastName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("label")]
		public ObservableCollection<StageSpecificValue> Labels
		{
			get { return Get<ObservableCollection<StageSpecificValue>>(); }
			set { Set(value); }
		}

		[XmlIgnore]
		public string Label // Compatibility property
		{
			get
			{
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
		public string Gender
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("size")]
		public string Size
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("timer")]
		public int Stamina
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[XmlNewLine(Position = XmlNewLinePosition.After)]
		[XmlElement("intelligence")]
		public ObservableCollection<StageSpecificValue> Intelligence
		{
			get { return Get<ObservableCollection<StageSpecificValue>>(); }
			set { Set(value); }
		}

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<CharacterTag> Tags
		{
			get;
			set;
		}

		[XmlArray("nicknames")]
		[XmlArrayItem("nickname")]
		public ObservableCollection<Nickname> Nicknames
		{
			get { return Get<ObservableCollection<Nickname>>(); }
			set { Set(value); }
		}

		[XmlElement("stylesheet")]
		public string StyleSheetName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		private CharacterStyleSheet _styles;
		[XmlIgnore]
		public CharacterStyleSheet Styles
		{
			get
			{
				if (_styles == null && !string.IsNullOrEmpty(StyleSheetName))
				{
					_styles = CharacterStyleSheetSerializer.Load(this, StyleSheetName);
					_styles.PropertyChanged += _styles_PropertyChanged;
				}
				return _styles;
			}
		}

		private void _styles_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IsDirty = true;
		}

		[XmlNewLine]
		[XmlArray("start")]
		[XmlArrayItem("state")]
		public List<DialogueLine> StartingLines { get; set; }

		[XmlNewLine]
		[XmlArray("wardrobe")]
		[XmlArrayItem("clothing")]
		public List<Clothing> Wardrobe { get; set; }

		[XmlNewLine]
		[XmlArray("poses")]
		[XmlArrayItem("pose")]
		public List<Pose> Poses { get; set; }

		[XmlNewLine(XmlNewLinePosition.Both)]
		[XmlElement("behaviour")]
		public Behaviour Behavior
		{
			get { return Get<Behaviour>(); }
			set { Set(value); }
		}

		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("epilogue")]
		public List<Epilogue> Endings { get; set; }

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml;

		[XmlIgnore]
		public CollectibleData Collectibles
		{
			get { return Get<CollectibleData>(); }
			set { Set(value); }
		}

		private bool _built;

		public event EventHandler<bool> OnDirtyChanged;

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

		private Costume _currentSkin;
		/// <summary>
		/// Current skin in play
		/// </summary>
		[XmlIgnore]
		public Costume CurrentSkin
		{
			get { return _currentSkin; }
			set
			{
				if (_currentSkin != value)
				{
					_currentSkin = value;
					NotifyPropertyChanged();
				}
			}
		}

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
			Labels = new ObservableCollection<StageSpecificValue>();
			Gender = "female";
			Size = "medium";
			Intelligence = new ObservableCollection<StageSpecificValue>();
			Stamina = 15;
			Tags = new List<CharacterTag>();
			Metadata = new Metadata();
			Markers = new MarkerData();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
			Tags = new List<CharacterTag>();
			Nicknames = new ObservableCollection<Nickname>();
			Behavior = new Behaviour();
			Poses = new List<Pose>();
			Wardrobe = new List<Clothing>();
			Collectibles = new CollectibleData();
			PoseLibrary = new PoseMap(this);
		}

		/// <summary>
		/// Clears all data from this character
		/// </summary>
		public new void Clear()
		{
			FirstName = "";
			LastName = "";
			Labels.Clear();
			Gender = "";
			Size = "";
			Behavior = new Behaviour();
			Intelligence = new ObservableCollection<StageSpecificValue>();
			Stamina = 15;
			Tags.Clear();
			Metadata = new Metadata();
			Markers = new MarkerData();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
			Poses = new List<Pose>();
			Version = "";
			Nicknames = new ObservableCollection<Nickname>();
			Collectibles = new CollectibleData();
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

		string ISkin.FolderName
		{
			get
			{
				return FolderName;
			}
		}

		Character ISkin.Character
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToStageName(int layer)
		{
			return LayerToStageName(layer, false, CurrentSkin ?? (IWardrobe)this);
		}

		public StageName LayerToStageName(int layer, bool advancingStage)
		{
			return LayerToStageName(layer, advancingStage, CurrentSkin ?? (IWardrobe)this);
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
			Behavior.OnBeforeSerialize(this);
			Metadata.PopulateFromCharacter(this);
			Version = Config.Version;
			foreach (Epilogue ending in Endings)
			{
				ending.OnBeforeSerialize();
			}
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
			foreach (Epilogue ending in Endings)
			{
				ending.OnAfterDeserialize();
			}
			Poses.Sort();
			foreach (Pose pose in Poses)
			{
				pose.OnAfterDeserialize();
			}

			PoseLibrary = new PoseMap(this);
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
			IsDirty = false;
			PropertyChanged += Character_PropertyChanged;
			_built = true;
		}

		private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "CurrentSkin")
			{
				IsDirty = true;
			}
		}

		/// <summary>
		/// Gets a count of the number of unique, non-targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetGenericLineCount()
		{
			int poses;
			return GetLineCount(LineFilter.Generic, out poses);
		}

		/// <summary>
		/// Gets a count of the number of unique targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetTargetedLineCount()
		{
			int poses;
			return GetLineCount(LineFilter.Targeted, out poses);
		}

		/// <summary>
		/// Gets a count of the number of unique targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetFilterLineCount()
		{
			int poses;
			return GetLineCount(LineFilter.Filter, out poses);
		}

		/// <summary>
		/// Gets a count of the number of unique targeted lines
		/// </summary>
		/// <returns></returns>
		public int GetSpecialLineCount()
		{
			int poses;
			return GetLineCount(LineFilter.Special, out poses);
		}

		public void GetUniqueLineAndPoseCount(out int lines, out int poses)
		{
			lines = GetLineCount(LineFilter.None, out poses);
		}

		/// <summary>
		/// Gets a count of unique lines
		/// </summary>
		/// <returns></returns>
		public int GetUniqueLineCount()
		{
			int poses;
			return GetLineCount(LineFilter.None, out poses);
		}

		private int GetLineCount(LineFilter filters, out int poseCount)
		{
			poseCount = 0;
			int count = 0;
			HashSet<string> poses = new HashSet<string>();
			HashSet<string> lines = new HashSet<string>();
			foreach (Case stageCase in Behavior.EnumerateSourceCases())
			{
				AddLines(poses, lines, stageCase, filters, ref poseCount, ref count);
			}
			return count;
		}

		private void AddLines(HashSet<string> poses, HashSet<string> lines, Case theCase, LineFilter filters, ref int poseCount, ref int count)
		{
			if (!string.IsNullOrEmpty(theCase.Hidden))
			{
				return;
			}
			bool targeted = theCase.HasTargetedConditions;
			bool special = theCase.HasStageConditions;
			bool generic = !theCase.HasConditions;
			bool filter = theCase.HasFilters;

			if ((filters == LineFilter.None) ||
				(filters & LineFilter.Generic) > 0 && generic ||
				(filters & LineFilter.Targeted) > 0 && targeted ||
				(filters & LineFilter.Special) > 0 && special ||
				(filters & LineFilter.Filter) > 0 && filter)
			{
				foreach (DialogueLine line in theCase.Lines)
				{
					List<string> images = new List<string>();
					if (line.Image != null)
					{
						if (line.Image.Contains("#"))
						{
							foreach (int stage in theCase.Stages)
							{
								images.Add(line.Image.Replace("#", stage.ToString()));
							}
						}
						else
						{
							images.Add(line.Image);
						}
					}
					foreach (string img in images)
					{
						if (!poses.Contains(img))
						{
							poses.Add(img);
							poseCount++;
						}
					}
					if (lines.Contains(line.Text))
						continue;
					count++;
					lines.Add(line.Text);
				}
			}
		}

		[Flags]
		public enum LineFilter
		{
			None = 0,
			Generic = 1,
			/// <summary>
			/// Target or AlsoPlaying
			/// </summary>
			Targeted = 2,
			/// <summary>
			/// Game state conditions
			/// </summary>
			Special = 4,
			/// <summary>
			/// Filter
			/// </summary>
			Filter = 8,
			/// <summary>
			/// Any conditions
			/// </summary>
			Conditional = Targeted | Special | Filter
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
			foreach (var stageCase in Behavior.EnumerateSourceCases())
			{
				if (IsCaseTargetedAtCharacter(stageCase, character, targetTypes))
				{
					yield return stageCase;
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

				if (stageCase.Filter != null && character.Tags.Find(t => t.Tag == stageCase.Filter) != null)
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
			foreach (var stageCase in Behavior.EnumerateSourceCases())
			{
				if (targetGender != "" && !stageCase.Tag.StartsWith(targetGender))
					continue;
				bool usesTag = (stageCase.Filter == tag);
				if (!usesTag)
				{
					usesTag = stageCase.Conditions.Find(c => c.FilterTag == tag) != null;
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
			if (status != OpponentStatus.Testing && status != OpponentStatus.Unlisted && status != OpponentStatus.Incomplete)
			{
				return WardrobeRestrictions.LayerCount;
			}
			return WardrobeRestrictions.None;
		}

		public Clothing GetClothing(int index)
		{
			return Wardrobe[index];
		}

		public string GetDirectory()
		{
			return Config.GetRootDirectory(this);
		}

		public string GetBackupDirectory()
		{
			return Config.GetBackupDirectory(this);
		}

		public ISkin Skin
		{
			get
			{
				ISkin skin = CurrentSkin;
				if (skin == null)
				{
					skin = this;
				}
				return skin;
			}
		}

		public HashSet<string> GetRequiredPoses()
		{
			return null;
		}

		public List<Pose> CustomPoses
		{
			get { return Poses; }
			set { Poses = value; }
		}

		/// <summary>
		/// Enumerates through all tags belonging to a certain group
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		public IEnumerable<CharacterTag> EnumerateTags(string group)
		{
			foreach (CharacterTag tag in Tags)
			{
				Tag t = TagDatabase.GetTag(tag.Tag);
				if (t.Group == group)
				{
					yield return tag;
				}
			}
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

	public class StageSpecificValue : BindableObject
	{
		/// <summary>
		/// Stages this intelligence begins at
		/// </summary>
		[XmlAttribute("stage")]
		[DefaultValue(0)]
		public int Stage
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Intelligence level
		/// </summary>
		[XmlText]
		public string Value
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public StageSpecificValue()
		{
		}

		public StageSpecificValue(int stage, string value)
		{
			Stage = stage;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Stage} - {Value}";
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

	public class CharacterTag
	{
		[XmlText]
		public string Tag;

		[XmlAttribute("from")]
		public string From;

		[XmlAttribute("to")]
		public string To;

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		public CharacterTag() { }
		public CharacterTag(string tag)
		{
			Tag = tag;
		}

		public override string ToString()
		{
			return Tag;
		}
	}

	public enum EditorSource
	{
		CharacterEditor,
		MakeXml,
		Other
	}
}
