using Desktop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SPNATI_Character_Editor.DataStructures
{
	[JsonObject("definition")]
	public class CaseDefinition
	{
		[JsonProperty("id")]
		public int Id;

		/// <summary>
		/// Conditions and tag to create when building a case out of this
		/// </summary>
		[JsonProperty("case")]
		public Case Case;

		[JsonProperty("label")]
		public string Label;

		/// <summary>
		/// Label to display in safe mode
		/// </summary>
		[JsonProperty("safe")]
		public string SafeLabel;

		/// <summary>
		/// Group ID
		/// </summary>
		[JsonProperty("groupId")]
		[DefaultValue(0)]
		public int Group;

		/// <summary>
		/// Default lines
		/// </summary>
		[JsonProperty("lines")]
		public List<string> Lines = new List<string>();

		/// <summary>
		/// Whether this is a core definition
		/// </summary>
		[JsonIgnore]
		public bool IsCore;

		public override int GetHashCode()
		{
			return Case.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Case.Equals((obj as CaseDefinition)?.Case);
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public string DisplayName
		{
			get
			{
				return Config.SafeMode ? SafeLabel ?? Label : Label;
			}
		}

		public string Key
		{
			get
			{
				return IsCore ? Case.Tag : Case.Tag + "-" + Id.ToString();
			}
		}

		public TriggerDefinition GetTrigger()
		{
			string tag = Case.Tag;
			return TriggerDatabase.GetTrigger(tag);
		}

		/// <summary>
		/// Creates a new case using this template
		/// </summary>
		/// <returns></returns>
		public Case CreateCase()
		{
			Case copy = Case.Copy();
			foreach (string line in Lines)
			{
				copy.Lines.Add(new DialogueLine(null, line));
			}
			return copy;
		}

		/// <summary>
		/// Gets the number of matching conditions between cases
		/// </summary>
		/// <param name="c"></param>
		/// <returns>Matches, or 0 if something didn't match</returns>
		public int GetMatchCount(Case c)
		{
			if (c.Tag != Case.Tag)
			{
				return 0;
			}
			int count = 1;

			//All new cases should use only conditions and tests, so only look at those
			foreach (ExpressionTest test in Case.Expressions)
			{
				bool found = c.Expressions.Any(e => test.Equals(e));
				if (!found)
				{
					return 0;
				}
				count++;
			}

			foreach (TargetCondition condition in Case.Conditions)
			{
				bool foundMatch = false;
				int condCount = 0;
				foreach (TargetCondition other in c.Conditions)
				{
					if (other.Character == "catria")
					{
					}
					condCount = 0;
					foreach (KeyValuePair<string, object> kvp in condition.DataStore)
					{
						string key = kvp.Key;
						object value = kvp.Value ?? "";

						object otherValue = other.Get<object>(key) ?? "";
						if (!value.Equals(otherValue))
						{
							condCount = 0;
							break;
						}
						condCount++;
					}

					if (condCount > 0)
					{
						foundMatch = true;
						break;
					}
				}
				if (foundMatch)
				{
					count += condCount;
				}
				else
				{
					return 0;
				}
			}

			return count;
		}
	}

	[JsonObject("group")]
	public class CaseGroup : IRecord
	{
		[JsonProperty("id")]
		public int Id;
		[JsonProperty("label")]
		public string Label;
		[JsonProperty("safe")]
		public string SafeLabel;

		[JsonIgnore]
		public bool IsCore;

		public CaseGroup()
		{
		}

		public CaseGroup(int id, string label, string safeLabel = null)
		{
			Id = id;
			Label = label;
			SafeLabel = safeLabel ?? Label;
		}

		public override string ToString()
		{
			return Label;
		}

		public string DisplayName
		{
			get
			{
				return Config.SafeMode ? SafeLabel ?? Label : Label;
			}
		}

		public string Name
		{
			get { return Label; }
		}

		public string Key
		{
			get { return Id.ToString(); }
			set
			{
				int id;
				if (int.TryParse(value, out id))
				{
					Id = id;
				}
			}
		}

		public string Group
		{
			get { return null; }
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public string ToLookupString()
		{
			return Name;
		}
	}

	[JsonObject("cases")]
	public class CaseDefinitions
	{
		[JsonProperty("groups")]
		public List<CaseGroup> Groups = new List<CaseGroup>();

		[JsonProperty("cases")]
		public List<CaseDefinition> Cases = new List<CaseDefinition>();
	}

	public static class CaseDefinitionDatabase
	{
		private static int _nextId;
		private static int _nextGroupId;
		private static Dictionary<int, CaseGroup> _groups = new Dictionary<int, CaseGroup>();
		private static Dictionary<string, List<CaseDefinition>> _casesByTag = new Dictionary<string, List<CaseDefinition>>();
		private static Dictionary<CaseDefinition, string> _tagsByCase = new Dictionary<CaseDefinition, string>();
		private static List<CaseDefinition> _cases = null;
		private static Dictionary<string, CaseDefinition> _casesByKey = new Dictionary<string, CaseDefinition>();
		private static Dictionary<CaseDefinition, string> _keysByCase = new Dictionary<CaseDefinition, string>();

		public static Action Loader = DefaultLoad;

		public static void Reset()
		{
			Loader = DefaultLoad;
			_cases = null;
			_groups.Clear();
			_casesByKey.Clear();
			_keysByCase.Clear();
			_tagsByCase.Clear();
			_casesByTag.Clear();
		}

		/// <summary>
		/// Loads the group definitions
		/// </summary>
		private static void Load()
		{
			_cases = new List<CaseDefinition>();
			Loader();
			foreach (CaseDefinition def in _cases)
			{
				_nextId = Math.Max(def.Id, _nextId);
			}
			foreach (CaseGroup group in _groups.Values)
			{
				_nextGroupId = Math.Max(group.Id, _nextGroupId);
			}
		}

		private static void EnsureLoaded()
		{
			if (_cases == null)
			{
				Load();
			}
		}

		private static void DefaultLoad()
		{
			//Create the core definitions
			_groups.Add(-1, new CaseGroup(-1, "Misc") { IsCore = true });
			foreach (TextGroup group in TriggerDatabase.Groups)
			{
				CaseGroup grp = AddGroup(group.Id, group.Name, group.SafeName);
				grp.IsCore = true;
			}
			foreach (TriggerDefinition trigger in TriggerDatabase.Triggers)
			{
				Case baseCase = new Case(trigger.Tag);
				CaseDefinition def = AddDefinition(baseCase, trigger.Label, trigger.SafeLabel);
				def.IsCore = true;
				UpdateDefinition(def);
				SetGroup(def, trigger.Group);
			}

			_nextId = 1000;

			//Custom definitions
			string filepath = Path.Combine(Config.AppDataDirectory, "cases.json");
			if (File.Exists(filepath))
			{
				try
				{
					string json = File.ReadAllText(filepath);
					CaseDefinitions data = Json.Deserialize<CaseDefinitions>(json);
					foreach (CaseGroup group in data.Groups)
					{
						_groups.Add(group.Id, group);
					}
					foreach (CaseDefinition definition in data.Cases)
					{
						AddDefinition(definition);
					}
				}
				catch { }
			}
		}


		public static void Save()
		{
			EnsureLoaded();

			string filepath = Path.Combine(Config.AppDataDirectory, "cases.json");
			CaseDefinitions defs = new CaseDefinitions();
			foreach (CaseGroup group in Groups)
			{
				if (group.IsCore)
				{
					continue;
				}
				defs.Groups.Add(group);
			}
			defs.Groups.Sort((a, b) => a.Id.CompareTo(b.Id));

			foreach (CaseDefinition def in Definitions)
			{
				if (def.IsCore)
				{
					continue;
				}
				defs.Cases.Add(def);
			}
			defs.Cases.Sort((a, b) => a.Id.CompareTo(b.Id));

			string json = Json.Serialize(defs);
			try
			{
				File.WriteAllText(filepath, json);
			}
			catch { }
		}

		public static IEnumerable<CaseGroup> Groups
		{
			get
			{
				EnsureLoaded();
				return _groups.Values;
			}
		}

		public static List<CaseDefinition> Definitions
		{
			get
			{
				EnsureLoaded();
				return _cases;
			}
		}

		public static CaseGroup GetGroup(int id)
		{
			CaseGroup group;
			_groups.TryGetValue(id, out group);
			return group;
		}

		/// <summary>
		/// Gets the closest definition match for a case
		/// </summary>
		/// <param name="c">Case to match</param>
		/// <returns></returns>
		public static CaseDefinition GetDefinition(Case c)
		{
			EnsureLoaded();
			string tag = c.Tag;
			List<CaseDefinition> list;
			if (_casesByTag.TryGetValue(tag, out list))
			{
				CaseDefinition closestMatch = null;
				int bestMatchCount = 0;
				foreach (CaseDefinition definition in list)
				{
					int matchCount = definition.GetMatchCount(c);
					if (matchCount > bestMatchCount)
					{
						bestMatchCount = matchCount;
						closestMatch = definition;
					}
				}
				return closestMatch;
			}
			return null;
		}

		/// <summary>
		/// Gets a definition by its key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static CaseDefinition GetDefinition(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			EnsureLoaded();
			CaseDefinition definition;
			_casesByKey.TryGetValue(key, out definition);
			return definition;
		}

		public static CaseGroup AddGroup(string label)
		{
			EnsureLoaded();
			CaseGroup group = new CaseGroup(++_nextGroupId, label);
			_groups.Add(group.Id, group);
			return group;
		}

		public static void RemoveGroup(CaseGroup group)
		{
			_groups.Remove(group.Id);
		}

		/// <summary>
		/// Adds a new group
		/// </summary>
		/// <param name="id"></param>
		/// <param name="label"></param>
		/// <param name="safeLabel"></param>
		/// <returns></returns>
		public static CaseGroup AddGroup(int id, string label, string safeLabel = null)
		{
			EnsureLoaded();
			CaseGroup group = new CaseGroup(id, label, safeLabel);
			_groups.Add(group.Id, group);
			return group;
		}

		/// <summary>
		/// Adds a brand new definition
		/// </summary>
		/// <returns></returns>
		public static CaseDefinition AddDefinition()
		{
			Case c = new Case("hand");
			string label = "New Template";

			CaseDefinition def = AddDefinition(c, label);
			SetGroup(def, -1);
			return def;
		}

		/// <summary>
		/// Adds a new definition
		/// </summary>
		/// <param name="baseCase"></param>
		/// <param name="label"></param>
		/// <param name="safeLabel"></param>
		/// <returns></returns>
		public static CaseDefinition AddDefinition(Case baseCase, string label, string safeLabel = null)
		{
			EnsureLoaded();
			CaseDefinition definition = new CaseDefinition()
			{
				Case = baseCase,
				Label = label,
				SafeLabel = safeLabel ?? label,
				Id = ++_nextId,
			};
			AddDefinition(definition);
			return definition;
		}
		public static void AddDefinition(CaseDefinition definition)
		{
			EnsureLoaded();
			_nextId = Math.Max(_nextId, definition.Id);
			_cases.Add(definition);
			CacheDefinition(definition);
		}

		/// <summary>
		/// Removes a definition
		/// </summary>
		/// <param name="def"></param>
		public static void RemoveDefinition(CaseDefinition def)
		{
			EnsureLoaded();
			string key;
			if (_keysByCase.TryGetValue(def, out key))
			{
				_keysByCase.Remove(def);
				_casesByKey.Remove(key);
			}

			string tag;
			if (_tagsByCase.TryGetValue(def, out tag))
			{
				_tagsByCase.Remove(def);
				List<CaseDefinition> list;
				if (_casesByTag.TryGetValue(tag, out list))
				{
					list.Remove(def);
				}
			}

			_cases.Remove(def);
		}

		private static void CacheDefinition(CaseDefinition def)
		{
			string tag = def.Case.Tag;
			string currentTag;
			if (_tagsByCase.TryGetValue(def, out currentTag) && tag != currentTag)
			{
				//new tag, remove from the previous
				List<CaseDefinition> currentList;
				if (_casesByTag.TryGetValue(currentTag, out currentList))
				{
					currentList.Remove(def);
				}
			}

			if (currentTag != tag)
			{
				List<CaseDefinition> list;
				if (!_casesByTag.TryGetValue(tag, out list))
				{
					list = new List<CaseDefinition>();
					_casesByTag[tag] = list;
				}
				list.Add(def);
				_tagsByCase[def] = tag;
			}

			//keys
			string key = def.Key;
			string oldKey;
			if (_keysByCase.TryGetValue(def, out oldKey) && oldKey != key)
			{
				//new key, remove the previous
				_casesByKey.Remove(oldKey);
			}
			_keysByCase[def] = key;
			_casesByKey[key] = def;
		}

		public static void UpdateDefinition(CaseDefinition definition)
		{
			EnsureLoaded();
			CacheDefinition(definition);
		}

		public static void SetGroup(CaseDefinition definition, int group)
		{
			definition.Group = group;
		}

		public static int Compare(string key1, string key2)
		{
			CaseDefinition def1 = GetDefinition(key1);
			CaseDefinition def2 = GetDefinition(key2);
			string tag1 = def1.Case.Tag;
			string tag2 = def2.Case.Tag;
			int compare = TriggerDatabase.Compare(tag1, tag2);
			if (compare == 0)
			{
				compare = key1.CompareTo(key2);
			}
			return compare;
		}

	}
}
