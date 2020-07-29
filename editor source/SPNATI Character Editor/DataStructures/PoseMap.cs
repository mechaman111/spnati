using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor
{
	public class PoseMap
	{
		private bool _initialized;
		private Character _character;

		private static readonly Regex _regex = new Regex(@"(\d+)-(.*)");

		private Dictionary<string, PoseMapping> _poseMap = new Dictionary<string, PoseMapping>();
		private List<PoseMapping> _poses = new List<PoseMapping>();
		public List<PoseMapping> Poses
		{
			get
			{
				if (!_initialized)
				{
					Initialize();
					_initialized = true;
				}
				return _poses;
			}
		}

		public PoseMap(Character character)
		{
			_character = character;
		}

		/// <summary>
		/// Gets all poses that can be used in the given stage
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public List<PoseMapping> GetPoses(int stage)
		{
			List<PoseMapping> list = new List<PoseMapping>();
			foreach (PoseMapping pose in Poses)
			{
				if (!Filter(pose) && pose.ContainsStage(stage))
				{
					list.Add(pose);
				}
			}
			if (Config.UsePrefixlessImages && stage >= 0)
			{
				list.AddRange(GetPoses(-1));
			}
			return list;
		}

		private bool Filter(PoseMapping pose)
		{
			string prefix = Config.PrefixFilter;
			string key = pose.Key;
			if (!string.IsNullOrEmpty(prefix) && key.StartsWith(prefix))
			{
				return true;
			}

			CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
			if (editorData != null)
			{
				foreach (string p in editorData.IgnoredPrefixes)
				{
					if (key.StartsWith(p))
					{
						return true;
					}
				}
			}
			return false;
		}

		private void Initialize()
		{
			string dir = _character.GetDirectory();
			if (string.IsNullOrEmpty(dir))
			{
				return;
			}
			string[] extensions = { ".png", ".gif" };
			foreach (string file in Directory.EnumerateFiles(dir, "*.*")
				.Where(s => extensions.Any(ext => ext == Path.GetExtension(s))))
			{
				Add(file);
			}

			foreach (Pose pose in _character.CustomPoses)
			{
				Add(pose);
			}
		}

		public void Add(Pose pose)
		{
			int stage;
			string id;
			ParseImage(pose.Id, out stage, out id);
			string key = GetPoseKey(stage, id, "");
			PoseMapping mapping = _poseMap.GetOrAddDefault(key, () =>
			{
				PoseMapping m = new PoseMapping(key);
				_poses.Add(m);
				_poses.Sort((p1, p2) =>
				{
					bool custom1 = p1.Key.StartsWith("custom:");
					bool custom2 = p2.Key.StartsWith("custom:");
					bool generic1 = p1.IsGeneric;
					bool generic2 = p2.IsGeneric;
					int type1 = custom1 ? 1 : generic1 ? 2 : 0;
					int type2 = custom2 ? 1 : generic2 ? 2 : 0;
					int compare = type1.CompareTo(type2);
					if (compare == 0)
					{
						compare = p1.Key.CompareTo(p2.Key);
					}
					return compare;
				});
				return m;
			});
			mapping.SetPose(stage, pose);
		}

		public void Rename(Pose pose)
		{
			string key = "";
			PoseMapping mapping = null;
			foreach (KeyValuePair<string, PoseMapping> kvp in _poseMap)
			{
				if (kvp.Value.ContainsPose(pose))
				{
					mapping = kvp.Value;
					key = kvp.Key;
					break;
				}
			}
			if (!string.IsNullOrEmpty(key))
			{
				_poseMap.Remove(key);
				_poses.Remove(mapping);
			}
			Add(pose);
		}

		public void Remove(Pose pose)
		{
			int stage;
			string id;
			ParseImage(pose.Id, out stage, out id);
			string key = GetPoseKey(stage, id, "");
			PoseMapping mapping = _poseMap.Get(key);
			if (mapping != null)
			{
				_poseMap.Remove(key);
				_poses.Remove(mapping);
			}
		}

		public void Add(string filename)
		{
			string extension = Path.GetExtension(filename);
			string name = Path.GetFileNameWithoutExtension(filename);
			int stage;
			string id;
			ParseImage(name, out stage, out id);
			string key = GetPoseKey(stage, id, extension);
			PoseMapping mapping = _poseMap.GetOrAddDefault(key, () =>
			{
				PoseMapping m = new PoseMapping(key);
				_poses.Add(m);
				return m;
			});
			mapping.SetPose(stage, Path.GetFileName(filename));
		}

		public static void ParseImage(string name, out int stage, out string id)
		{
			Match m = _regex.Match(name);
			if (m.Success)
			{
				int.TryParse(m.Groups[1].Value, out stage);
				id = m.Groups[2].Value;
			}
			else
			{
				stage = -1;
				id = name;
			}
		}

		private string GetPoseKey(int stage, string id, string extension)
		{
			return $"{(string.IsNullOrEmpty(extension) && !id.StartsWith("custom:") ? "custom:" : "")}{(stage >= 0 ? "#-" : "")}{id}{(!string.IsNullOrEmpty(extension) ? extension : "")}";
		}

		public PoseMapping GetFlatFilePose(string name)
		{
			string file = Path.Combine(_character.GetDirectory(), name);
			if (File.Exists(file))
			{
				return GetPose(name);
			}
			else
			{
				string key = name;
				if (key.StartsWith("custom:"))
				{
					key = "custom:#-" + key.Substring("custom:".Length);
				}
				else
				{
					key = "#-" + key;
				}
				return GetPose(key);
			}
		}

		/// <summary>
		/// Gets a PoseMapping based on how it might appear in a stage element
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public PoseMapping GetPose(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!_initialized)
			{
				Initialize();
				_initialized = true;
			}
			int stage;
			string id;
			string ext = null;
			if (!name.StartsWith("custom:"))
			{
				ext = Path.GetExtension(name);
				if (!string.IsNullOrEmpty(ext))
				{
					name = name.Substring(0, name.Length - ext.Length);
				}
			}
			ParseImage(name, out stage, out id);
			string key = GetPoseKey(stage, id, ext);

			PoseMapping pose;
			_poseMap.TryGetValue(key, out pose);
			return pose;
		}
	}

	/// <summary>
	/// A mapping between a pose and its stage-specific variants
	/// </summary>
	public class PoseMapping : IComparable<PoseMapping>
	{
		/// <summary>
		/// How the pose gets stored in a state element
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		/// User-friendly display name
		/// </summary>
		public string DisplayName { get; set; }

		public string GetFlatFormat()
		{
			return Key.Replace("#-", "");
		}

		private Dictionary<int, PoseReference> _stages = new Dictionary<int, PoseReference>();

		public PoseMapping(string key)
		{
			Key = key;
			if (!key.StartsWith("custom:") && !key.EndsWith(".gif"))
			{
				int period = key.LastIndexOf('.');
				if (period > 0)
				{
					key = key.Substring(0, period);
				}
			}
			if (key.Contains("#-"))
			{
				key = key.Replace("#-", "");
			}
			else
			{
				key += "*";
			}

			DisplayName = key;
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public bool IsGeneric
		{
			get { return _stages.ContainsKey(-1); }
		}

		public void SetPose(int stage, Pose pose)
		{
			PoseReference def = new PoseReference(pose);
			_stages[stage] = def;
		}

		public void SetPose(int stage, string filename)
		{
			PoseReference def = new PoseReference(filename);
			_stages[stage] = def;
		}

		/// <summary>
		/// Gets whether a stage has its own image variant
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public bool ContainsStage(int stage)
		{
			return _stages.ContainsKey(stage);
		}

		/// <summary>
		/// Gets the pose for a stage
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public PoseReference GetPose(int stage)
		{
			PoseReference definition;
			if (!_stages.TryGetValue(stage, out definition) && stage != -1)
			{
				_stages.TryGetValue(-1, out definition);
			}
			return definition;
		}

		public string GetStageKey(int stage, bool includeExtension)
		{
			string key = Key;
			if (!key.StartsWith("custom:") && !includeExtension)
			{
				string extension = Path.GetExtension(key);
				if (!string.IsNullOrEmpty(extension))
				{
					key = key.Substring(0, key.Length - extension.Length);
				}
			}
			return key.Replace("#-", stage.ToString() + "-");
		}

		public bool ContainsPose(Pose pose)
		{
			foreach (PoseReference def in _stages.Values)
			{
				if (def.Pose == pose)
				{
					return true;
				}
			}
			return false;
		}

		public int CompareTo(PoseMapping other)
		{
			return DisplayName.CompareTo(other.DisplayName);
		}

		public string GetBasicName()
		{
			string name = Path.GetFileNameWithoutExtension(Key);
			if (name.StartsWith("#-"))
			{
				name = name.Substring(2);
			}
			return name;
		}
	}

	public class PoseReference
	{
		/// <summary>
		/// Filename
		/// </summary>
		public string FileName;

		/// <summary>
		/// Custom pose
		/// </summary>
		public Pose Pose;

		public PoseReference(Pose pose)
		{
			Pose = pose;
		}

		public PoseReference(string filename)
		{
			FileName = filename;
		}

		public override string ToString()
		{
			return Pose?.Id ?? FileName;
		}
	}
}
