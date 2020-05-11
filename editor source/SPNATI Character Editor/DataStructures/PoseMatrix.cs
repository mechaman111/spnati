using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.Controls.EditControls;
using System;
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
		/// <param name="name">Sheet name. Will be made unique if necessary</param>
		public PoseSheet AddSheet(string name)
		{
			name = GetUniqueSheetName(name);
			PoseSheet sheet = new PoseSheet()
			{
				Name = name
			};
			Sheets.Add(sheet);

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
		/// Moves a column to the specified index
		/// </summary>
		/// <param name="key"></param>
		/// <param name="newIndex"></param>
		public void MoveColumn(string key, int newIndex)
		{
			foreach (PoseStage stage in Stages)
			{
				stage.MoveCell(key, newIndex);
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
		/// Moves a cell to a new index
		/// </summary>
		/// <param name="key"></param>
		/// <param name="newIndex"></param>
		public void MoveCell(string key, int newIndex)
		{
			for (int i = 0; i < Poses.Count; i++)
			{
				PoseEntry pose = Poses[i];
				if (pose.Key == key)
				{
					Poses.RemoveAt(i);
					if (newIndex >= Poses.Count)
					{
						Poses.Add(pose);
					}
					else
					{
						Poses.Insert(newIndex, pose);
					}
					break;
				}
			}
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
	}
}
