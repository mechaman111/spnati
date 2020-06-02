using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.StackedBar, 4)]
	public class TargetedByBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, int, int>> _data;
		private const string ViewAll = "All";
		private const string ViewDirect = "Direct";
		private const string ViewTags = "Tags";

		public string GetLabel()
		{
			return "Targets (Incoming)"; ;
		}

		public void GenerateData()
		{
			List<Tuple<Character, int, int>> counts = new List<Tuple<Character, int, int>>();
			Dictionary<string, Tuple<int, int>> targetMap = new Dictionary<string, Tuple<int, int>>();
			Dictionary<string, HashSet<Character>> tagMap = new Dictionary<string, HashSet<Character>>();

			foreach (Character c in CharacterDatabase.Characters)
			{
				foreach (CharacterTag tag in c.Tags)
				{
					HashSet<Character> tagSet;
					if (!tagMap.TryGetValue(tag.Tag.ToLower(), out tagSet))
					{
						tagSet = new HashSet<Character>();
						tagMap[tag.Tag.ToLower()] = tagSet;
					}
					tagSet.Add(c);
				}

				HashSet<string> targets = new HashSet<string>();
				HashSet<string> tagTargets = new HashSet<string>();
				foreach (var stageCase in c.Behavior.EnumerateSourceCases())
				{
					foreach (string target in stageCase.GetTargets())
					{
						if (!targets.Contains(target) && CharacterDatabase.Exists(target))
						{
							Tuple<int, int> current;
							if (!targetMap.TryGetValue(target, out current))
								current = new Tuple<int, int>(0, 0);
							targetMap[target] = new Tuple<int, int>(current.Item1 + 1, current.Item2);
							targets.Add(target);
						}
					}

					if (!string.IsNullOrEmpty(stageCase.Target))
					{
						if (!targets.Contains(stageCase.Target) && CharacterDatabase.Exists(stageCase.Target))
						{
							Tuple<int, int> current;
							if (!targetMap.TryGetValue(stageCase.Target, out current))
								current = new Tuple<int, int>(0, 0);
							targetMap[stageCase.Target] = new Tuple<int, int>(current.Item1 + 1, current.Item2);
							targets.Add(stageCase.Target);
						}
					}
					else if (!string.IsNullOrEmpty(stageCase.Filter))
					{
						HashSet<Character> taggedCharacters;
						if (tagMap.TryGetValue(stageCase.Filter.ToLower(), out taggedCharacters))
						{
							foreach (Character character in taggedCharacters)
							{
								if (!tagTargets.Contains(character.FolderName))
								{
									Tuple<int, int> current;
									if (!targetMap.TryGetValue(character.FolderName, out current))
										current = new Tuple<int, int>(0, 0);
									targetMap[character.FolderName] = new Tuple<int, int>(current.Item1, current.Item2 + 1);
									tagTargets.Add(character.FolderName);
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(stageCase.AlsoPlaying) && CharacterDatabase.Exists(stageCase.AlsoPlaying))
					{
						if (!targets.Contains(stageCase.AlsoPlaying))
						{
							Tuple<int, int> current;
							if (!targetMap.TryGetValue(stageCase.AlsoPlaying, out current))
								current = new Tuple<int, int>(0, 0);
							targetMap[stageCase.AlsoPlaying] = new Tuple<int, int>(current.Item1 + 1, current.Item2);
							targets.Add(stageCase.AlsoPlaying);
						}
					}
				}
			}

			foreach (var kvp in targetMap)
			{
				int count = kvp.Value.Item1;
				int tagCount = kvp.Value.Item2;
				if (count + tagCount > 0)
				{
					counts.Add(new Tuple<Character, int, int>(CharacterDatabase.Get(kvp.Key), count, tagCount));
				}
			}

			_data = counts;
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			List<List<ChartData>> data = new List<List<ChartData>>();
			List<ChartData> series0 = new List<ChartData>();
			List<ChartData> series1 = new List<ChartData>();

			data.Add(series0);
			data.Add(series1);

			switch (view)
			{
				case ViewAll:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2 + t2.Item3).CompareTo(t1.Item2 + t1.Item3);
					});
					break;
				case ViewDirect:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2).CompareTo(t1.Item2);
					});
					break;
				case ViewTags:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item3).CompareTo(t1.Item3);
					});
					break;
			}

			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];
				switch (view)
				{
					case ViewAll:
						series0.Add(new ChartData(item.Item1.Label, item.Item2));
						series1.Add(new ChartData(item.Item1.Label, item.Item3));
						break;
					case ViewDirect:
						series0.Add(new ChartData(item.Item1.Label, item.Item2));
						break;
					case ViewTags:
						series1.Add(new ChartData(item.Item1.Label, item.Item3));
						break;
				}
			}

			return data;
		}

		public string GetTitle()
		{
			return "Unique Players Targeting this Character";
		}

		public string[] GetViews()
		{
			return new string[] { ViewAll, ViewDirect, ViewTags };
		}
	}
}
