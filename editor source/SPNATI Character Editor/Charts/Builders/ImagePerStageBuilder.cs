using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 11)]
	public class ImagePerStageBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, double>> _data;
		private Regex _regex = new Regex(@"^[0-9]*-");

		public string GetLabel()
		{
			return "Images per Stage (Average)";
		}

		private bool Filter(string filename)
		{
			string ext = Path.GetExtension(filename);
			if (ext != ".png" && ext != ".gif")
				return false;
			//exclude epilogue images. Note these might not actually be epilogue images, but it's a best guess
			string name = Path.GetFileNameWithoutExtension(filename);
			if (!_regex.IsMatch(name))
				return false;
			return true;
		}

		public void GenerateData()
		{
			_data = new List<Tuple<Character, double>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				HashSet<string> usedNames = new HashSet<string>();
				Dictionary<string, int> linesPerStage = new Dictionary<string, int>();
				string folder = Config.GetRootDirectory(c);
				foreach (string filename in Directory.EnumerateFiles(folder)
					.Where(Filter))
				{
					string[] pieces = Path.GetFileNameWithoutExtension(filename).Split('-');
					string stage = pieces[0];
					if (pieces.Length > 1)
					{
						usedNames.Add(pieces[1]);
					}
					else
					{
						usedNames.Add(pieces[0]);
					}
					int count;
					if (!linesPerStage.TryGetValue(stage, out count))
					{
						count = 1;
					}
					linesPerStage[stage] = ++count;
				}

				foreach (Pose pose in c.CustomPoses)
				{
					string[] pieces = Path.GetFileNameWithoutExtension(pose.Id).Split('-');
					string name = pieces.Length > 1 ? pieces[1] : pieces[0];
					string stage = pieces[0];
					if (!usedNames.Contains(name))
					{
						int count;
						if (!linesPerStage.TryGetValue(stage, out count))
						{
							count = 1;
						}
						linesPerStage[stage] = ++count;
					}
				}

				if (linesPerStage.Count == 0) continue;
				//Average the stages
				double average = linesPerStage.Values.Average();
				_data.Add(new Tuple<Character, double>(c, average));
			}

			_data.Sort((t1, t2) =>
			{
				return (t2.Item2).CompareTo(t1.Item2);
			});
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			List<List<ChartData>> data = new List<List<ChartData>>();
			List<ChartData> series0 = new List<ChartData>();

			data.Add(series0);
			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];
				series0.Add(new ChartData(item.Item1.Label, item.Item2));
			}

			return data;
		}

		public string GetTitle()
		{
			return "Images Per Stage";
		}

		public string[] GetViews()
		{
			return new string[] { "All" };
		}
	}
}
