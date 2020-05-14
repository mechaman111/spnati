using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.Controls.EditControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[Serializable]
	[XmlRoot("posegrid", Namespace = "")]
	public class PoseMatrix : BindableObject
	{
		[XmlArray("sheets")]
		[XmlArrayItem("sheet")]
		public ObservableCollection<PoseSheet> Sheets
		{
			get { return Get<ObservableCollection<PoseSheet>>(); }
			set { Set(value); }
		}

		public PoseMatrix()
		{
			Sheets = new ObservableCollection<PoseSheet>();
		}

		/// <summary>
		/// Creates a default matrix
		/// </summary>
		/// <param name="skin"></param>
		public PoseMatrix(ISkin skin) : this()
		{
			Character character = skin.Character;
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

		/// <summary>
		/// Adds a new sheet
		/// </summary>
		/// <param name="name"></param>
		/// <param name="name">Sheet name. Will be made unique if necessary</param>
		public PoseSheet AddSheet(string name)
		{
			return AddSheet(name, null);
		}

		/// <summary>
		/// Adds a new sheet
		/// </summary>
		/// <param name="name">Sheet name. Will be made unique if necessary</param>
		/// <param name="basis">Sheet to duplicate</param>
		public PoseSheet AddSheet(string name, PoseSheet basis)
		{
			name = GetUniqueSheetName(name);
			PoseSheet sheet = new PoseSheet()
			{
				Name = name
			};
			Sheets.Add(sheet);

			if (basis != null)
			{
				foreach (PoseStage basisStage in basis.Stages)
				{
					PoseStage stage = basisStage.Clone() as PoseStage;
					sheet.Stages.Add(stage);
					stage.Poses = new ObservableCollection<PoseEntry>();
					foreach (PoseEntry pose in basisStage.Poses)
					{
						stage.Poses.Add(pose.Clone() as PoseEntry);
					}
				}
			}

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
	}

	[Serializable]
	public class PoseSheet : BindableObject
	{
		public PoseSheet()
		{
			Stages = new ObservableCollection<PoseStage>();
		}

		[Text(DisplayName = "Sheet Name")]
		[XmlElement("name")]
		public string Name
		{
			get { return Get<string>(); }
			set { if (!string.IsNullOrEmpty(value)) { Set(value); } }
		}

		[Text(DisplayName = "Base Appearance Code", RowHeight = 130, Multiline = true)]
		[XmlElement("model")]
		public string BaseCode
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

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Creates a sheet out of a pose list
		/// </summary>
		/// <param name="list"></param>
		public void FillFromPoseList(Character character, PoseList list)
		{
			//create stages
			for (int stage = 0; stage < character.Layers + Clothing.ExtraStages; stage++)
			{
				Stages.Add(new PoseStage(stage));
			}

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
							if (stage >= 0 && stage < Stages.Count)
							{
								PoseStage s = Stages[stage];
								s.Poses.Add(cell);
							}
						}
					}
					else
					{
						//Currently can't handle stageless images
					}
				}				
			}
		}

		/// <summary>
		/// Populates a sheet from a template
		/// </summary>
		/// <param name="template"></param>
		public void FillFromTemplate(Character character, PoseTemplate template)
		{
			BaseCode = template.BaseCode.ToString();

			//create stages
			for (int stage = 0; stage < character.Layers + Clothing.ExtraStages; stage++)
			{
				PoseStage poseStage = new PoseStage(stage);
				Stages.Add(poseStage);

				if (stage < template.Stages.Count)
				{
					StageTemplate templateStage = template.Stages[stage];
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
			int expectedLayers = character.Layers + Clothing.ExtraStages;
			if (expectedLayers != Stages.Count)
			{
				while (expectedLayers > Stages.Count)
				{
					Stages.Add(new PoseStage(Stages.Count));
				}
				while (expectedLayers < Stages.Count)
				{
					Stages.RemoveAt(Stages.Count - 1);
				}
			}
		}
	}

	[Serializable]
	public class PoseStage : BindableObject
	{
		[XmlAttribute("id")]
		public int Stage
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Text(DisplayName = "Wardrobe Code", RowHeight = 130, Multiline = true)]
		[XmlElement("clothing")]
		public string Code
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlArray("poses")]
		[XmlArrayItem("pose")]
		public ObservableCollection<PoseEntry> Poses
		{
			get { return Get<ObservableCollection<PoseEntry>>(); }
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

		public PoseEntry GetCell(string key)
		{
			return Poses.Find(p => p.Key == key);
		}

		/// <summary>
		/// Inserts a cell into the stage
		/// </summary>
		/// <param name="cell">Cell to insert</param>
		/// <param name="order">Expected order cells to help determine where to place it (otherwise order won't be preserved in the grid next time opening this)</param>
		public void InsertCell(PoseEntry cell, List<string> order)
		{
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
	public class PoseEntry : BindableObject
	{
		public PoseEntry()
		{
			Crop = new Rect(0, 0, 600, 1400);
		}

		public PoseEntry(ImageMetadata metadata)
		{
			Key = metadata.ImageKey;
			Code = metadata.Data;
			Crop = metadata.CropInfo;
			SkipPreProcessing = metadata.SkipPreprocessing;
			//TODO: extradata
		}

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

		public override string ToString()
		{
			return Key;
		}
	}
}
