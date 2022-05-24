using Desktop;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class AlternateSkin
	{
		[XmlElement("costume")]
		public List<SkinLink> Skins = new List<SkinLink>();

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;
	}

	[XmlRoot("costume", Namespace = "")]
	/// <summary>
	/// Alternate skin data stored in costume.xml
	/// </summary>
	public class Costume : IRecord, ISkin, IHookSerialization
	{
		public Costume()
		{
			Labels = new List<StageSpecificValue>();
			Tags = new List<CharacterTag>();
		}

		[XmlElement("id")]
		public string Id { get; set; }

		[XmlElement("label")]
		public List<StageSpecificValue> Labels { get; set; }

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<CharacterTag> Tags
		{
			get;
			set;
		}

		[XmlElement("folder")]
		public List<StageSpecificValue> Folders = new List<StageSpecificValue>();

		[XmlIgnore]
		public bool IsDirty { get; set; }

		[XmlIgnore]
		public ISkin Skin { get { return this; } }

		[XmlIgnore]
		public string Folder
		{
			get
			{
				if (Folders.Count == 0)
				{
					return null;
				}
				StageSpecificValue first = Folders.Find(f => f.Stage == 0);
				if (first != null)
				{
					return first.Value;
				}
				return Folders[0].Value;
			}
		}

		[XmlArray("wardrobe")]
		[XmlArrayItem("clothing")]
		public List<Clothing> Wardrobe = new List<Clothing>();

		[XmlNewLine]
		[XmlArray("poses")]
		[XmlArrayItem("pose")]
		public List<Pose> Poses = new List<Pose>();

		[XmlAnyElement]
		public List<XmlElement> ExtraXml;

		[XmlIgnore]
		public Character Character { get; set; }

		[XmlIgnore]
		public SkinLink Link { get; set; }

		public string Name
		{
			get
			{
				if (Link != null)
				{
					return Link.Name;
				}
				if (Labels.Count > 0) { return Labels[0].Value; }
				return Id;
			}
		}

		public string Key
		{
			get { return Id; }
			set { Id = value; }
		}

		public string Group
		{
			get { return ""; }
		}

		public int Layers
		{
			get { return Wardrobe.Count; }
		}

		public override string ToString()
		{
			return Id;
		}

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize(string source)
		{
			Wardrobe.ForEach(c => c.OnAfterDeserialize());

			//standardize folder format
			Folders.ForEach(f =>
			{
				string folder = f.Value;
				folder = folder.Replace("\\", "/");
				if (folder.StartsWith("/"))
				{
					folder = folder.Substring(1);
				}
				if (!folder.EndsWith("/"))
				{
					folder = folder + "/";
				}
				f.Value = folder;
			});
		}

		public string ToLookupString()
		{
			return $"{Name} [{Id}]";
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		/// <summary>
		/// Links or re-links a character whose link had been broken
		/// </summary>
		/// <param name="character"></param>
		public void LinkCharacter(Character owner)
		{
			AlternateSkin skin;
			if (owner.Metadata.AlternateSkins.Count == 0)
			{
				skin = new AlternateSkin();
				owner.Metadata.AlternateSkins.Add(skin);
			}
			else
			{
				skin = owner.Metadata.AlternateSkins[0];
			}
			SkinLink link = new SkinLink()
			{
				Folder = Folder,
				Name = Key,
			};
			skin.Skins.Add(link);

			Character = owner;
			link.Costume = this;
			Link = link;
		}

		public WardrobeRestrictions GetWardrobeRestrictions()
		{
			return WardrobeRestrictions.LayerCount;
		}

		public Clothing GetClothing(int index)
		{
			return Wardrobe[index];
		}

		public void ApplyWardrobeChanges(Queue<WardrobeChange> changes)
		{
		}

		public int AddLayer(Clothing layer)
		{
			return 0;
		}

		public int RemoveLayer(Clothing layer)
		{
			return 0;
		}

		public int MoveUp(Clothing layer)
		{
			return 0;
		}

		public int MoveDown(Clothing layer)
		{
			return 0;
		}

		public string FolderName
		{
			get { return Folder; }
		}

		public string GetDirectory()
		{
			string dir = Path.Combine(Config.SpnatiDirectory, Folder).Replace("/", "\\");
			if (dir.EndsWith("\\"))
			{
				return dir.Substring(0, dir.Length - 1);
			}
			return dir;
		}

		public string GetBackupDirectory()
		{
			string dir = Character.GetBackupDirectory();
			return Path.Combine(dir, Id);
		}

		public string GetAttachmentsDirectory()
		{
			return Path.Combine(Config.SpnatiDirectory, "attachments", "reskins", FolderName);
		}

		public HashSet<string> GetRequiredPoses(bool stageless)
		{
			if (Character == null)
			{
				return null;
			}
			HashSet<string> images = new HashSet<string>();
			int endStage = Layers + Clothing.ExtraStages;
			if (Folders.Count > 1)
			{
				endStage = Folders[1].Stage;
			}
			foreach (Case c in Character.Behavior.GetWorkingCases())
			{
				foreach (DialogueLine line in c.Lines)
				{
					foreach (int stage in c.Stages)
					{
						if (stage < endStage)
						{
							PoseMapping pose = line.Pose;
							if (pose == null || pose.Key.StartsWith("custom:") || (pose.IsGeneric && stageless))
							{
								continue;
							}
							string name = pose.DisplayName;
							if (!stageless)
							{
								name = pose.GetStageKey(stage, false);
							}
							images.Add(name);
						}
					}
				}
			}
			return images;
		}

		public string GetPosePath(string sheetName, string subfolder, string poseName, bool asset)
		{
			string root = asset ? Path.Combine(Config.AppDataDirectory, Folder, sheetName) : Path.Combine(GetDirectory());
			if (!string.IsNullOrEmpty(subfolder))
			{
				root = Path.Combine(root, subfolder);
			}
			return Path.Combine(root, poseName + ".png");
		}

		public List<Pose> CustomPoses
		{
			get { return Poses; }
			set { Poses = value; }
		}

		public string Gender
		{
			get { return Character.Gender; }
		}

		public List<CharacterTag> GetTags()
		{
			//Use the character's tags
			List<CharacterTag> list = new List<CharacterTag>();
			foreach (CharacterTag tag in Character.Tags)
			{
				CharacterTag copy = tag.Clone() as CharacterTag;
				list.Add(copy);
			}

			HashSet<string> removed = new HashSet<string>();

			//now override it with the skin changes
			foreach (CharacterTag tag in Tags)
			{
				//remove all previous entries
				if (!removed.Contains(tag.Tag))
				{
					list = list.Where(t => t.Tag != tag.Tag).ToList();
					removed.Add(tag.Tag);
				}

				if (!tag.Remove)
				{
					list.Add(tag);
				}
			}

			return list;
		}

		/// <summary>
		/// Combines tags with different intervals into one tag and an interval string per tag
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		private Dictionary<string, Tuple<string, List<CharacterTag>>> GetTagsWithInterval(List<CharacterTag> tags)
		{
			Dictionary<string, Tuple<List<int>, List<CharacterTag>>> mainTags = new Dictionary<string, Tuple<List<int>, List<CharacterTag>>>();
			foreach (CharacterTag tag in tags)
			{
				string key = tag.Tag;
				Tuple<List<int>, List<CharacterTag>> group;
				List<int> stages;
				List<CharacterTag> groupTags;
				if (mainTags.TryGetValue(key, out group))
				{
					stages = group.Item1;
					groupTags = group.Item2;
				}
				else
				{
					stages = new List<int>();
					groupTags = new List<CharacterTag>();
				}

				int from;
				int to;
				if (!string.IsNullOrEmpty(tag.From) && !string.IsNullOrEmpty(tag.To) && int.TryParse(tag.From, out from) && int.TryParse(tag.To, out to))
				{
					for (int s = from; s <= to; s++)
					{
						stages.Add(s);
					}
				}
				else
				{
					for (int s = 0; s < Character.Layers + Clothing.ExtraStages; s++)
					{
						stages.Add(s);
					}
				}
				groupTags.Add(tag);

				mainTags[key] = new Tuple<List<int>, List<CharacterTag>>(stages, groupTags);
			}

			Dictionary<string, Tuple<string, List<CharacterTag>>> output = new Dictionary<string, Tuple<string, List<CharacterTag>>>();
			foreach (KeyValuePair<string, Tuple<List<int>, List<CharacterTag>>> kvp in mainTags)
			{
				string interval = GUIHelper.ListToString(kvp.Value.Item1);
				output[kvp.Key] = new Tuple<string, List<CharacterTag>>(interval, kvp.Value.Item2);
			}

			return output;
		}

		public void AddTags(List<CharacterTag> tags)
		{
			Dictionary<string, Tuple<string, List<CharacterTag>>> mainTags = GetTagsWithInterval(Character.Tags);
			Dictionary<string, Tuple<string, List<CharacterTag>>> skinTags = GetTagsWithInterval(tags);

			foreach (KeyValuePair<string, Tuple<string, List<CharacterTag>>> kvp in skinTags)
			{
				string key = kvp.Key;
				string interval = kvp.Value.Item1;
				List<CharacterTag> tagList = kvp.Value.Item2;
				Tuple<string, List<CharacterTag>> source;
				if (mainTags.TryGetValue(key, out source) && source.Item1 == interval)
				{
					mainTags.Remove(key);
					continue; //same stages are present between main skin and this one, so ignore it
				}

				//different stages, so add the tags
				Tags.AddRange(tagList);

				mainTags.Remove(key);
			}

			//anything left in the main skin's dictionary is a tag that should be marked for removal
			foreach (string tag in mainTags.Keys)
			{
				if (tag == CharacterDatabase.GetId(Character))
				{
					continue; //older characters might have this tag - ignore it
				}
				CharacterTag remover = new CharacterTag(tag);
				remover.Remove = true;
				Tags.Add(remover);
			}

			Tags.Sort();
		}
	}

	/// <summary>
	/// Link to a reskin used in meta.xml
	/// </summary>
	public class SkinLink
	{
		[XmlAttribute("folder")]
		public string Folder;

		[XmlAttribute("img")]
		public string PreviewImage;

		[XmlAttribute("set")]
		public string Set;

		[XmlText]
		public string Name;

		[XmlAttribute("status")]
		public string Status;

		[XmlAttribute("gender")]
		public string Gender;

		[XmlAttribute("label")]
		public string Label;

		[XmlIgnore]
		public Costume Costume { get; set; }

		public bool IsDirty;

		public override string ToString()
		{
			return Name;
		}
	}
}
