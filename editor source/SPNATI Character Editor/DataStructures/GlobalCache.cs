using Desktop;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Caches information across characters
	/// </summary>
	public static class GlobalCache
	{
		/// <summary>
		/// key1: character being targeted, key2: character doing the targeting, value: # of lines
		/// </summary>
		private static DualKeyDictionary<string, string, int> _targetedLineChange = new DualKeyDictionary<string, string, int>();

		public static IEnumerable<string> EnumerateChangeInTargets(string folderName)
		{
			Dictionary<string, int> innerDict;
			if (_targetedLineChange.TryGetValue(folderName, out innerDict))
			{
				foreach (string source in innerDict.Keys)
				{
					yield return source;
				}
			}
		}

		/// <summary>
		/// Gets the change in targets from the source to the destination character
		/// </summary>
		/// <param name="source"></param>
		/// <param name="dest"></param>
		/// <returns></returns>
		public static int GetChangeInTargets(string source, string destination)
		{
			return _targetedLineChange.Get(destination, source);
		}

		public static void CreateDiff(CachedCharacter old, CachedCharacter current)
		{
			if (old == null || current == null)
			{
				return;
			}
			string source = current.FolderName;
			foreach (TargetingInformation ti in current.Targets)
			{
				string target = ti.Target;
				int lines = ti.LineCount;
				int oldLines = old.GetTargetedCountTowards(target);
				int diff = lines - oldLines;
				if (diff > 0)
				{
					_targetedLineChange.Set(target, source, diff);
				}
			}
		}

		/// <summary>
		/// Gets whether there are new targeted lines towards a character
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static bool HasChanges(string target)
		{
			return _targetedLineChange.ContainsPrimaryKey(target);
		}
	}
}
