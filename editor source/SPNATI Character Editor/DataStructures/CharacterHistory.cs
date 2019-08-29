using Desktop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
		public static CharacterHistory Get(Character character)
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
					}
					catch
					{
						history = null;
					}
				}
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
			CharacterHistory history = Get(character);

			history.Update();

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

		[JsonProperty("work")]
		private List<LineWork> _work = new List<LineWork>();

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

			_workToday = GetWork(today);
		}

		public void Update()
		{
			_workToday.Update(_character);
		}

		/// <summary>
		/// Gets the line work performed on the given date
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public LineWork GetWork(DateTime date)
		{
			foreach (LineWork work in _work)
			{
				if (work.Time.Date == date.Date)
				{
					return work;
				}
			}
			LineWork newWork = new LineWork();
			_work.Add(newWork);
			newWork.Time = DateTime.UtcNow;
			return newWork;
		}
	}

	public class LineWork
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

		public void Update(Character character)
		{
			Dictionary<LineFilter, HashSet<string>> visitedLines = new Dictionary<LineFilter, HashSet<string>>();
			Dictionary<LineFilter, int> counts = new Dictionary<LineFilter, int>();
			counts[LineFilter.Generic] = 0;
			counts[LineFilter.Targeted] = 0;
			counts[LineFilter.Filter] = 0;
			counts[LineFilter.Conditional] = 0;
			foreach (Case c in character.Behavior.GetWorkingCases())
			{
				LineFilter type = (c.HasTargetedConditions ? LineFilter.Targeted : c.HasFilters ? LineFilter.Filter : c.HasConditions ? LineFilter.Conditional : LineFilter.Generic);
				HashSet<string> lines = visitedLines.GetOrAddDefault(type, () => new HashSet<string>());
				foreach (DialogueLine line in c.Lines)
				{
					if (lines.Contains(line.Text))
					{
						continue;
					}
					lines.Add(line.Text);

					counts[type]++;
				}
			}
			TargetedCount = counts[LineFilter.Targeted];
			FilterCount = counts[LineFilter.Filter];
			ConditionCount = counts[LineFilter.Conditional];
			GenericCount = counts[LineFilter.Generic];
		}
	}
}
