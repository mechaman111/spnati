using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using ImagePipeline;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.Controls.EditControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[Serializable]
	[XmlRoot("posegrid", Namespace = "")]
	public class PoseMatrix : BindableObject, IHookSerialization
	{
		private ISkin _character;
		[XmlIgnore]
		public ISkin Character
		{
			get { return _character; }
			set
			{
				_character = value;
				foreach (PipelineGraph pipeline in Pipelines)
				{
					pipeline.Character = value;
				}
			}
		}

		[XmlArray("sheets")]
		[XmlArrayItem("sheet")]
		public ObservableCollection<PoseSheet> Sheets
		{
			get { return Get<ObservableCollection<PoseSheet>>(); }
			set { Set(value); }
		}

		[XmlArray("pipelines")]
		[XmlArrayItem("pipeline")]
		public ObservableCollection<PipelineGraph> Pipelines
		{
			get { return Get<ObservableCollection<PipelineGraph>>(); }
			set { Set(value); }
		}

		public PoseMatrix()
		{
			Sheets = new ObservableCollection<PoseSheet>();
			Pipelines = new ObservableCollection<PipelineGraph>();
		}

		public bool IsEmpty()
		{
			if (Sheets.Count > 1)
			{
				return false;
			}
			if (Sheets.Count == 0)
			{
				return true;
			}
			//empty means no codes are defined
			PoseStage codedStage = Sheets[0].Stages.Find(s => s.Poses.Find(p => !string.IsNullOrEmpty(p.Code)) != null);
			return codedStage == null;
		}

		public PipelineGraph GetPipeline(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			return Pipelines.Find(p => p.Key == key);
		}

		/// <summary>
		/// Adds a new sheet
		/// </summary>
		/// <param name="name"></param>
		/// <param name="name">Sheet name. Will be made unique if necessary</param>
		public PoseSheet AddSheet(string name, Character character)
		{
			return AddSheet(name, character, null, false);
		}

		/// <summary>
		/// Adds a new sheet
		/// </summary>
		/// <param name="name">Sheet name. Will be made unique if necessary</param>
		/// <param name="basis">Sheet to duplicate</param>
		/// <param name="global">Whether poses are global</param>
		public PoseSheet AddSheet(string name, Character character, PoseSheet basis, bool global)
		{
			name = GetUniqueSheetName(name);
			PoseSheet sheet = new PoseSheet()
			{
				Name = name,
				IsGlobal = global,
				Matrix = this
			};
			Sheets.Add(sheet);

			if (basis != null)
			{
				sheet.BaseCode = basis.BaseCode;
				foreach (PoseStage basisStage in basis.Stages)
				{
					PoseStage stage = basisStage.Clone() as PoseStage;
					stage.Sheet = sheet;
					sheet.Stages.Add(stage);
					stage.Poses = new ObservableCollection<PoseEntry>();
					foreach (PoseEntry pose in basisStage.Poses)
					{
						PoseEntry clone = pose.Clone() as PoseEntry;
						clone.Stage = stage;
						stage.Poses.Add(clone);
					}

					if (global)
					{
						break; //only do one stage for globals
					}
				}
			}
			else if (global)
			{
				PoseStage stage = new PoseStage(0);
				stage.Sheet = sheet;
				sheet.Stages.Add(stage);
			}

			sheet.ReconcileStages(character);

			return sheet;
		}

		public void RemoveSheet(PoseSheet sheet)
		{
			Sheets.Remove(sheet);
		}

		private string GetUniqueSheetName(string name)
		{
			string sheetName = name;
			int suffix = 1;
			while (Sheets.Find(s => s.Name == sheetName) != null)
			{
				sheetName = $"{name}{++suffix}";
			}
			return sheetName;
		}

		/// <summary>
		/// Builds a filename from a stage and pose/emotion
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="pose"></param>
		/// <returns></returns>
		public static string GetKey(string stage, string pose)
		{
			if (string.IsNullOrEmpty(stage))
			{
				return pose;
			}
			if (stage.EndsWith("_"))
			{
				return string.Format("{0}{1}", stage, pose);
			}
			return string.Format("{0}-{1}", stage, pose);
		}

		/// <summary>
		/// Gets the file path for a pose
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		public string GetFilePath(PoseEntry entry, string prefix = "")
		{
			PoseStage poseStage = entry.Stage;
			PoseSheet sheet = poseStage.Sheet;
			string stageKey = sheet.IsGlobal ? (!string.IsNullOrEmpty(poseStage.Name) ? poseStage.Name + "_" : "") : poseStage.Stage.ToString();
			string key = prefix + GetKey(stageKey, entry.Key);
			string filePath = GetPosePath(sheet, poseStage, key, !string.IsNullOrEmpty(prefix));
			return filePath;
		}

		private string GetPosePath(PoseSheet sheet, PoseStage stage, string key, bool forceAsset)
		{
			bool asset = sheet.PipelineAsset || stage.PipelineAsset || forceAsset;
			string path = Character.GetPosePath(sheet.Name, sheet.SubFolder, key, asset);
			return path;
		}

		/// <summary>
		/// Gets whether a pose is out of date
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		public bool IsOutOfDate(PoseEntry entry, string prefix = "")
		{
			string path = GetFilePath(entry, prefix);
			if (File.Exists(path))
			{
				if (entry.LastUpdate == 0)
				{
					//if this predates the LastUpdate field, consider it always up-to-date
					return false;
				}
				DateTime updateTime = DateTimeOffset.FromUnixTimeMilliseconds(entry.LastUpdate).DateTime;
				DateTime fileTime = File.GetLastWriteTimeUtc(path);
				return fileTime < updateTime;
			}
			return false;
		}

		/// <summary>
		/// Gets the status of a pose's import
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		public FileStatus GetStatus(PoseEntry entry, string prefix = "")
		{
			string path = GetFilePath(entry, prefix);
			if (File.Exists(path))
			{
				return IsOutOfDate(entry, prefix) ? FileStatus.OutOfDate : FileStatus.Imported;
			}
			else
			{
				return FileStatus.Missing;
			}
		}

		public void OnBeforeSerialize()
		{
			foreach (PoseSheet sheet in Sheets)
			{
				sheet.OnBeforeSerialize();
			}
			foreach (PipelineGraph pipeline in Pipelines)
			{
				pipeline.OnBeforeSerialize();
			}
		}

		public void OnAfterDeserialize(string source)
		{
			foreach (PoseSheet sheet in Sheets)
			{
				sheet.Matrix = this;
				sheet.OnAfterDeserialize(source);
			}
			foreach (PipelineGraph pipeline in Pipelines)
			{
				pipeline.OnAfterDeserialize(source);
			}
		}

		/// <summary>
		/// Ensures the stages in the matrix match the character's wardrobe
		/// </summary>
		/// <param name="character"></param>
		public void ReconcileStages(Character character)
		{
			foreach (PoseSheet sheet in Sheets)
			{
				sheet.ReconcileStages(character);
			}
		}

		/// <summary>
		/// Moves a pose list into a sheet
		/// </summary>
		/// <param name="character">Character the sheet belongs to</param>
		/// <param name="list">List to convert</param>
		/// <param name="sheet">Sheet to put the list into</param>
		public void FillFromPoseList(Character character, PoseList list, PoseSheet sheet)
		{
			PoseSheet globalSheet = null;

			//make sure stages exist
			sheet.ReconcileStages(character);

			string pattern = @"^((\d+)-)*(.+)$";
			Regex regex = new Regex(pattern);

			//fill in cells
			foreach (ImageMetadata metadata in list.Poses)
			{
				Match match = regex.Match(metadata.ImageKey);
				if (match.Success)
				{
					string key = metadata.ImageKey;
					if (!string.IsNullOrEmpty(match.Groups[2].Value))
					{
						string stageValue = match.Groups[2].Value;
						key = match.Groups[3].Value;

						//find the right stage to put it into
						int stage;
						if (int.TryParse(stageValue, out stage))
						{
							PoseEntry cell = new PoseEntry(metadata);
							cell.Key = key;
							if (stage >= 0 && stage < sheet.Stages.Count)
							{
								PoseStage s = sheet.Stages[stage];
								s.Poses.Add(cell);
							}
						}
					}
					else
					{
						if (globalSheet == null)
						{
							globalSheet = AddSheet("Global", character, null, true);
						}
						key = metadata.ImageKey;
						PoseEntry cell = new PoseEntry(metadata);
						cell.Key = key;
						globalSheet.Stages[0].Poses.Add(cell);
					}
				}
			}
		}

		/// <summary>
		/// Populates from a template
		/// </summary>
		/// <param name="template"></param>
		public void FillFromTemplate(Character character, PoseTemplate template, PoseSheet sheet)
		{
			sheet.BaseCode = template.BaseCode.ToString();

			//make sure every row exists
			sheet.ReconcileStages(character);

			//create stages
			for (int stage = 0; stage < character.Layers + Clothing.ExtraStages; stage++)
			{
				PoseStage poseStage = sheet.Stages[stage];

				if (stage < template.Stages.Count)
				{
					StageTemplate templateStage = template.Stages[stage];
					if (string.IsNullOrEmpty(templateStage.Code))
					{
						continue;
					}
					poseStage.Code = templateStage.Code;

					foreach (Emotion emotion in template.Emotions)
					{
						PoseEntry entry = new PoseEntry();
						entry.Key = emotion.Key;
						entry.Code = emotion.Code;
						entry.Crop = emotion.Crop;
						poseStage.Poses.Add(entry);
					}
				}
			}
		}
	}

	[Serializable]
	public class PoseSheet : BindableObject, IPoseCode, IHookSerialization
	{
		public PoseSheet()
		{
			Stages = new ObservableCollection<PoseStage>();
		}

		[XmlIgnore]
		public PoseMatrix Matrix;

		[Text(DisplayName = "Sheet Name")]
		[XmlElement("name")]
		public string Name
		{
			get { return Get<string>(); }
			set { if (!string.IsNullOrEmpty(value)) { Set(value); } }
		}

		[Text(DisplayName = "Base Appearance Code", RowHeight = 110, Multiline = true)]
		[XmlElement("model")]
		public string BaseCode
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue(false)]
		[XmlAttribute("global")]
		public bool IsGlobal
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[Text(DisplayName = "Subfolder")]
		[DefaultValue("")]
		[XmlAttribute("folder")]
		public string SubFolder
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlArray("stages")]
		[XmlArrayItem("stage")]
		public ObservableCollection<PoseStage> Stages
		{
			get { return Get<ObservableCollection<PoseStage>>(); }
			set { Set(value); }
		}

		[Boolean(DisplayName = "Pipeline Asset", Description = "If checked, poses in this sheet will not be saved to the character's folder and are only meant for pipelines")]
		[XmlAttribute("asset")]
		[DefaultValue(false)]
		public bool PipelineAsset
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public override string ToString()
		{
			return Name;
		}

		public string GetCode()
		{
			return BaseCode;
		}
		public void SetCode(string value)
		{
			BaseCode = value;
		}

		/// <summary>
		/// Gets whether there are no codes defined in this sheet
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return Stages.Find(s => s.Poses.Count > 0) == null;
			}
		}

		public string GetUniqueColumnName(string name)
		{
			string colName = name;
			int suffix = 1;
			while (Stages.Find(s => s.Poses.Find(p => p.Key == colName) != null) != null)
			{
				colName = $"{name}{++suffix}";
			}
			return colName;
		}

		/// <summary>
		/// Removes all poses with the given key
		/// </summary>
		/// <param name="key"></param>
		public void RemoveColumn(string key)
		{
			foreach (PoseStage stage in Stages)
			{
				stage.RemoveCell(key);
			}
		}

		/// <summary>
		/// Sorts columns in alphabetical order
		/// </summary>
		public void SortColumns()
		{
			foreach (PoseStage stage in Stages)
			{
				stage.Sort();
			}
		}

		/// <summary>
		/// Reorders columns to match the order
		/// </summary>
		/// <param name="expectedOrder"></param>
		public void ReorderColumns(List<string> expectedOrder)
		{
			foreach (PoseStage stage in Stages)
			{
				stage.Reorder(expectedOrder);
			}
		}

		/// <summary>
		/// Ensures the stages in the matrix match the character's wardrobe
		/// </summary>
		/// <param name="character"></param>
		public void ReconcileStages(Character character)
		{
			int expectedLayers = IsGlobal ? 1 : character.Layers + Clothing.ExtraStages;
			if (expectedLayers != Stages.Count)
			{
				while (expectedLayers > Stages.Count)
				{
					PoseStage stage = new PoseStage(Stages.Count);
					stage.Sheet = this;
					Stages.Add(stage);
				}
				while (expectedLayers < Stages.Count)
				{
					Stages.RemoveAt(Stages.Count - 1);
				}
			}
		}

		public PoseStage AddRow()
		{
			PoseStage stage = new PoseStage(Stages.Count);
			stage.Sheet = this;

			//copy the poses from the first stage
			if (Stages.Count > 0)
			{
				PoseStage last = Stages[0];
				foreach (PoseEntry pose in last.Poses)
				{
					PoseEntry clone = pose.Clone() as PoseEntry;
					clone.Stage = stage;
					stage.Poses.Add(clone);
				}
			}

			Stages.Add(stage);

			return stage;
		}

		public void RemoveRow(PoseStage stage)
		{
			Stages.Remove(stage);
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize(string source)
		{
			foreach (PoseStage stage in Stages)
			{
				stage.Sheet = this;
				foreach (PoseEntry pose in stage.Poses)
				{
					pose.Stage = stage;
				}
			}
		}
	}

	[Serializable]
	public class PoseStage : BindableObject, IPoseCode
	{
		[XmlAttribute("id")]
		public int Stage
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Text(DisplayName = "Name")]
		[XmlAttribute("name")]
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Text(DisplayName = "Wardrobe Code", RowHeight = 130, Multiline = true)]
		[XmlElement("clothing")]
		public string Code
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[PipelineSelect(DisplayName = "Pipeline")]
		[XmlAttribute("pipeline")]
		public string Pipeline
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlArray("pipe-params")]
		[XmlArrayItem("parameter")]
		public List<string> PipelineParameters
		{
			get { return Get<List<string>>(); }
			set { Set(value); }
		}

		[XmlArray("poses")]
		[XmlArrayItem("pose")]
		public ObservableCollection<PoseEntry> Poses
		{
			get { return Get<ObservableCollection<PoseEntry>>(); }
			set { Set(value); }
		}

		[Boolean(DisplayName = "Pipeline Asset", Description = "If checked, poses in this row will not be saved to the character's folder and are only meant for pipelines")]
		[XmlAttribute("asset")]
		[DefaultValue(false)]
		public bool PipelineAsset
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public PoseStage()
		{
			Poses = new ObservableCollection<PoseEntry>();
		}
		public PoseStage(int stage) : this()
		{
			Stage = stage;
		}

		/// <summary>
		/// The sheet this stage belongs to
		/// </summary>
		public PoseSheet Sheet;

		public string GetCode()
		{
			return Code;
		}
		public void SetCode(string value)
		{
			Code = value;
		}

		public PoseEntry GetCell(string key)
		{
			return Poses.Find(p => p.Key == key);
		}

		public void AddCell(PoseEntry pose)
		{
			pose.Stage = this;
			Poses.Add(pose);
		}

		/// <summary>
		/// Inserts a cell into the stage
		/// </summary>
		/// <param name="cell">Cell to insert</param>
		/// <param name="order">Expected order cells to help determine where to place it (otherwise order won't be preserved in the grid next time opening this)</param>
		public void InsertCell(PoseEntry cell, List<string> order)
		{
			cell.Stage = this;

			//try to figure out the best index
			int index = order.IndexOf(cell.Key);
			if (index == -1)
			{
				//if it's not even in the order, just tack it on the end
				Poses.Add(cell);
				return;
			}

			//look for a previous item and insert after it
			for (int i = index - 1; i >= 0; i--)
			{
				string neighborKey = order[i];
				for (int j = 0; j < Poses.Count; j++)
				{
					if (Poses[j].Key == neighborKey)
					{
						//found a previous key. Assume it's the closest and just insert after it
						Poses.Insert(j + 1, cell);
						return;
					}
				}
			}

			//look for a next item and insert before it
			for (int i = index + 1; i < order.Count; i++)
			{
				string neighborKey = order[i];
				for (int j = Poses.Count - 1; j >= 0; j--)
				{
					if (Poses[j].Key == neighborKey)
					{
						//found a previous key. Assume it's the closest and just insert after it
						Poses.Insert(j, cell);
						return;
					}
				}
			}

			//no neighbors found, so just add it
			Poses.Add(cell);
		}

		public void RemoveCell(string key)
		{
			for (int i = 0; i < Poses.Count; i++)
			{
				if (Poses[i].Key == key)
				{
					Poses.RemoveAt(i);
					break;
				}
			}
		}

		/// <summary>
		/// Reorders cells to match the expected order
		/// </summary>
		/// <param name="expectedOrder"></param>
		public void Reorder(List<string> expectedOrder)
		{
			Poses.Sort((p1, p2) => expectedOrder.IndexOf(p1.Key).CompareTo(expectedOrder.IndexOf(p2.Key)));
		}

		/// <summary>
		/// Sorts cells in alphabetical order
		/// </summary>
		public void Sort()
		{
			Poses.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));
		}
	}

	[Serializable]
	public class PoseEntry : BindableObject, IPoseCode
	{
		public PoseEntry()
		{
			Crop = new Rect(0, 0, 600, 1400);
			ExtraMetadata = new Dictionary<string, string>();
		}

		public PoseEntry(ImageMetadata metadata)
		{
			Key = metadata.ImageKey;
			Code = metadata.Data;
			Crop = metadata.CropInfo;
			SkipPreProcessing = metadata.SkipPreprocessing;
			ExtraMetadata = metadata.ExtraData;
		}

		public string GetCode()
		{
			return Code;
		}
		public void SetCode(string value)
		{
			Code = value;
		}

		/// <summary>
		/// The stage this entry belongs to
		/// </summary>
		[XmlIgnore]
		public PoseStage Stage;

		[XmlAttribute("key")]
		public string Key
		{
			get { return Get<string>(); }
			set { if (!string.IsNullOrEmpty(value)) { Set(value); } }
		}

		[XmlElement("code")]
		[Text(DisplayName = "Code", RowHeight = 130, Multiline = true)]
		public string Code
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("crop")]
		[Rect(DisplayName = "Crop")]
		public Rect Crop
		{
			get { return Get<Rect>(); }
			set { Set(value); }
		}

		[DefaultValue(false)]
		[XmlAttribute("manual")]
		[Boolean(DisplayName = "Skip pre-processing")]
		public bool SkipPreProcessing
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlElement("extra")]
		[PoseMetadata(DisplayName = "Transparencies")]
		public string ExtraMetadataRaw
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				ExtraMetadata.Clear();
				if (value != null)
				{
					string[] pieces = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string raw in pieces)
					{
						string[] piece = raw.Split(new char[] { '=' }, 2);
						if (piece.Length == 2)
						{
							string key = piece[0];
							ExtraMetadata[key] = piece[1];
						}
					}
				}
			}
		}

		[PipelineSelect(DisplayName = "Pipeline")]
		[XmlAttribute("pipeline")]
		public string Pipeline
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlArray("pipe-params")]
		[XmlArrayItem("parameter")]
		public List<string> PipelineParameters
		{
			get { return Get<List<string>>(); }
			set { Set(value); }
		}

		[XmlElement("lastupdate")]
		[DefaultValue(0L)]
		public long LastUpdate { get; set; }

		private Dictionary<string, string> _data;
		[XmlIgnore]
		public Dictionary<string, string> ExtraMetadata
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
				if (value != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (KeyValuePair<string, string> kvp in value)
					{
						sb.Append($"{kvp.Key}={kvp.Value},");
					}
					Set(sb.ToString(), "ExtraMetadataRaw"); //not using the setter directly to avoid infinite recursion
				}
			}
		}

		public override string ToString()
		{
			return Key;
		}

		public void UpdateTimeStamp()
		{
			LastUpdate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}

		/// <summary>
		/// Gets the full key for a pose + stage
		/// </summary>
		/// <returns></returns>
		public string GetFullKey()
		{
			PoseSheet sheet = Stage.Sheet;
			string stageKey = sheet.IsGlobal ? (!string.IsNullOrEmpty(Stage.Name) ? Stage.Name + "_" : "") : Stage.Stage.ToString();
			string key = PoseMatrix.GetKey(stageKey, Key);
			return key;
		}

		/// <summary>
		/// Compiles metadata from the pose, stage, and base
		/// </summary>
		/// <returns></returns>
		public ImageMetadata CreateMetadata()
		{
			PoseSheet sheet = Stage.Sheet;
			string key = GetFullKey();
			ImageMetadata metadata = new ImageMetadata(key, "");
			string appearance = sheet.BaseCode;
			KisekaeCode baseCode = null;
			if (!string.IsNullOrEmpty(appearance))
			{
				baseCode = new KisekaeCode(appearance, true);
			}

			KisekaeCode modelCode = null;
			if (baseCode != null || !string.IsNullOrEmpty(Stage.Code))
			{
				Emotion emotion = new Emotion(Key, Code, "", "", "", "");
				modelCode = PoseTemplate.CreatePose(baseCode, new StageTemplate(Stage.Code ?? ""), emotion);
			}
			else
			{
				modelCode = new KisekaeCode(Code, false);
			}
			metadata.SkipPreprocessing = SkipPreProcessing;
			metadata.CropInfo = Crop;
			metadata.Data = modelCode.ToString();
			metadata.ExtraData = ExtraMetadata;
			return metadata;
		}
	}

	public interface IPoseCode
	{
		string GetCode();
		void SetCode(string value);
	}

	public enum FileStatus
	{
		Missing,
		OutOfDate,
		Imported
	}
}
