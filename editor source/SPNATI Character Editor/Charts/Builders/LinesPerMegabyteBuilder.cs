using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 16)]
	public class LinesPerMegabyteBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, double>> _data;

		public string GetLabel()
		{
			return "Lines per MB";
		}

		public void GenerateData()
		{
			List<Tuple<Character, double>> counts = new List<Tuple<Character, double>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				long filesize = 0;
				string folder = Config.GetRootDirectory(c);
				DirectoryInfo directory = new DirectoryInfo(folder);
				foreach (FileInfo file in directory.GetFiles().Where(FilterPaths))
				{
					filesize += file.Length;
				}
				int lineCount = c.GetUniqueLineCount();
				if (filesize > 0)
				{
					double megabytes = filesize / 1000000.0f;
					double average = lineCount / megabytes;
					counts.Add(new Tuple<Character, double>(c, average));
				}
			}

			_data = counts;
			_data.Sort((t1, t2) =>
			{
				return (t2.Item2).CompareTo(t1.Item2);
			});
		}

		public static bool FilterPaths(FileInfo f)
		{
			string extension = f.Extension.ToLower();
			if (extension == ".png" ||
				extension == ".gif" ||
				extension == ".jpg" ||
				extension == ".css" ||
				extension == ".js")
			{
				return true;
			}
			if (extension == ".xml")
			{
				if (f.Name == "behaviour.xml" || f.Name == "meta.xml")
				{
					return true;
				}
			}
			return false;
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
			return "Lines per MB";
		}

		public string[] GetViews()
		{
			return new string[] { "All" };
		}
	}
}
