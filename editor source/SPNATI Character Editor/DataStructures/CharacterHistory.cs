using Desktop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using static SPNATI_Character_Editor.Character;

namespace SPNATI_Character_Editor
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	/// <summary>
	/// Contains editing history for a character
	/// </summary>
	public class CharacterHistory
	{
		#region Static stuff
		private static Dictionary<string, CharacterHistory> _histories = new Dictionary<string, CharacterHistory>();

		/// <summary>
		/// Gets where a character's history is stored on disk
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		private static string GetFilePath(Character character)
		{
			return Path.Combine(Config.GetBackupDirectory(character), "history.json");
		}

		/// <summary>
		/// Gets a character's history, loading it from disk if necessary
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public static CharacterHistory Get(Character character, bool update)
		{
			CharacterHistory history = null;
			if (!_histories.TryGetValue(character.FolderName, out history))
			{
				string file = GetFilePath(character);
				if (File.Exists(file))
				{
					try
					{
						string json = File.ReadAllText(file);
						history = Json.Deserialize<CharacterHistory>(json);
						_histories[character.FolderName] = history;
					}
					catch
					{
						history = null;
					}
				}
			}
			else
			{
				if (update)
				{
					history.Update();
				}
				return history;
			}
			if (history == null)
			{
				history = new CharacterHistory();
				_histories[character.FolderName] = history;
			}
			history.Initialize(character);
			return history;
		}

		/// <summary>
		/// Saves a character's history to disk
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public static bool Save(Character character)
		{
			CharacterHistory history = Get(character, true);

			string json = Json.Serialize(history);
			try
			{
				string file = GetFilePath(character);
				File.WriteAllText(file, json);
			}
			catch
			{
				return false;
			}
			return true;
		}
		#endregion

		private Character _character;

		private LineWork _workToday;
		private float _fileSize = 0;

		[JsonProperty("lineGoal")]
		public int DailyGoal = 0;

		[JsonProperty("goalBanner")]
		public DateTime LastGoalBanner;

		[JsonProperty("work")]
		private List<LineWork> _work = new List<LineWork>();

		public LineWork Previous
		{
			get { return _work.Count > 1 ? _work[_work.Count - 2] : _workToday; }
		}

		public LineWork Current
		{
			get { return _workToday; }
		}

		/// <summary>
		/// Initialization for a new(ly loaded) history
		/// </summary>
		/// <param name="character"></param>
		public void Initialize(Character character)
		{
			_character = character;

			//clear out any work older than a month
			DateTime today = DateTime.UtcNow;
			for (int i = _work.Count - 1; i >= 0; i--)
			{
				if ((today - _work[i].Time).Days > 30)
				{
					_work.RemoveAt(i);
				}
			}

			_workToday = GetWork(today, true);

			Update();

			if (_work.Count == 1)
			{
				//if today is the only logged day, create yesterday with the current values in order to get accurate diffs going forward
				LineWork yesterday = _workToday.Clone() as LineWork;
				yesterday.Time = today - new TimeSpan(1, 0, 0, 0);
				_work.Insert(0, yesterday);
			}
		}

		public void Update()
		{
			_workToday.Update(_character);
		}

		public bool BannerDisplayedToday
		{
			get { return DateTime.Now.Date == LastGoalBanner.ToLocalTime().Date; }
		}
		public void MarkBannerAsDisplayed()
		{
			LastGoalBanner = DateTime.UtcNow.Date;
		}

		/// <summary>
		/// Gets the line work performed most recently before the given date
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public LineWork GetMostRecentWorkBefore(DateTime date)
		{
			DateTime inputDate = date.ToLocalTime().Date;
			for (int i = _work.Count - 1; i >= 0; i--)
			{
				DateTime work = _work[i].Time;
				if (work.ToLocalTime().Date < inputDate)
				{
					return _work[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the line work performed on the given date
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public LineWork GetWork(DateTime date, bool addIfNew)
		{
			DateTime localDate = date.ToLocalTime().Date;
			foreach (LineWork work in _work)
			{
				if (work.Time.ToLocalTime().Date == localDate)
				{
					return work;
				}
			}
			if (addIfNew)
			{
				LineWork newWork = new LineWork();
				_work.Add(newWork);
				newWork.Time = DateTime.UtcNow;
				return newWork;
			}
			else
			{
				return null;
			}
		}

		public float GetTotalFileSize(bool forceCompute)
		{
			if (_fileSize == 0 || forceCompute)
			{
				HashSet<string> customPoseAssets = new HashSet<string>();
				foreach (Pose pose in _character.Poses)
				{
					foreach (Sprite sprite in pose.Sprites)
					{
						string path = sprite.Src;
						if (path.StartsWith(_character.FolderName + "/"))
						{
							path = path.Substring(_character.FolderName.Length + 1);
							customPoseAssets.Add(path);
						}
					}
				}

				long size = 0;
				string dir = _character.GetDirectory();
				DirectoryInfo directory = new DirectoryInfo(dir);
				foreach (FileInfo file in directory.EnumerateFiles()
					.Where(f => f.Extension == ".png" || f.Extension == ".gif"))
				{
					if (char.IsNumber(file.Name[0]) || customPoseAssets.Contains(file.Name)) //only include images that start with a number. Assume others are for epilogues and shouldn't count towards the requirements
					{
						size += file.Length;
					}
				}
				_fileSize = (float)Math.Round(size / 1048576f, 2);
			}
			return _fileSize;
		}
	}

	public class LineWork : ICloneable
	{
		public DateTime Time;
		/// <summary>
		/// Total targeted lines
		/// </summary>
		public int TargetedCount;
		/// <summary>
		/// Total conditioned lines
		/// </summary>
		public int ConditionCount;
		/// <summary>
		/// Total generic lines
		/// </summary>
		public int GenericCount;
		/// <summary>
		/// Filtered lines
		/// </summary>
		public int FilterCount;

		[JsonIgnore]
		public int TotalLines
		{
			get
			{
				return TargetedCount + ConditionCount + GenericCount + FilterCount;
			}
		}

		[JsonIgnore]
		public List<TargetingInformation> Targets = new List<TargetingInformation>();

		[JsonIgnore]
		public Dictionary<int, int> LinesPerStage = new Dictionary<int, int>();

		public void Update(Character character)
		{
			Dictionary<string, TargetingInformation> targetInfo = new Dictionary<string, TargetingInformation>();
			HashSet<string> lines = new HashSet<string>();
			Dictionary<string, HashSet<string>> targetLines = new Dictionary<string, HashSet<string>>();
			Dictionary<LineFilter, int> counts = new Dictionary<LineFilter, int>();
			LinesPerStage.Clear();
			counts[LineFilter.Generic] = 0;
			counts[LineFilter.Targeted] = 0;
			counts[LineFilter.Filter] = 0;
			counts[LineFilter.Conditional] = 0;
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				if (!string.IsNullOrEmpty(c.Hidden))
				{
					continue;
				}
				LineFilter type = (c.HasTargetedConditions ? LineFilter.Targeted : c.HasFilters ? LineFilter.Filter : c.HasConditions ? LineFilter.Conditional : LineFilter.Generic);

				int caseCount = 0;
				foreach (DialogueLine line in c.Lines)
				{
					if (lines.Contains(line.Text))
					{
						continue;
					}
					lines.Add(line.Text);

					counts[type]++;
					caseCount++;
				}

				foreach (int stage in c.Stages)
				{
					LinesPerStage[stage] = LinesPerStage.Get(stage) + caseCount;
				}

				if (type == LineFilter.Targeted)
				{
					HashSet<string> targets = c.GetTargets();
					foreach (string target in targets)
					{
						int targetCount = 0;
						HashSet<string> targetedLines = targetLines.GetOrAddDefault(target, () => new HashSet<string>());
						foreach (DialogueLine line in c.Lines)
						{
							if (targetedLines.Contains(line.Text))
							{
								continue;
							}
							targetedLines.Add(line.Text);
							targetCount++;
						}

						TargetingInformation info = targetInfo.GetOrAddDefault(target, () => new TargetingInformation(target));
						info.LineCount += targetCount;
					}
				}
			}
			TargetedCount = counts[LineFilter.Targeted];
			FilterCount = counts[LineFilter.Filter];
			ConditionCount = counts[LineFilter.Conditional];
			GenericCount = counts[LineFilter.Generic];
			Targets = new List<TargetingInformation>();
			foreach (TargetingInformation info in targetInfo.Values)
			{
				bool added = false;
				int count = info.LineCount;
				for (int i = 0; i < Targets.Count; i++)
				{
					if (count > Targets[i].LineCount)
					{
						Targets.Insert(i, info);
						added = true;
						break;
					}
				}
				if (!added)
				{
					Targets.Add(info);
				}
			}
		}

		public object Clone()
		{
			LineWork copy = MemberwiseClone() as LineWork;
			return copy;
		}
	}

	public class TargetingInformation
	{
		[XmlAttribute("target")]
		public string Target;
		[XmlAttribute("count")]
		public int LineCount;

		public TargetingInformation() { }

		public TargetingInformation(string target)
		{
			Target = target;
		}

		public override string ToString()
		{
			return Target + ": " + LineCount;
		}
	}
}
