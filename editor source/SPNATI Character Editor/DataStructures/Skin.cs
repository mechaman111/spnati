using Desktop;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
	public class Costume : IRecord, IWardrobe, ISkin, IHookSerialization
	{
		[XmlElement("id")]
		public string Id;

		[XmlElement("label")]
		public List<StageSpecificValue> Labels = new List<StageSpecificValue>();

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<SkinTag> Tags = new List<SkinTag>();

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
			return WardrobeRestrictions.LayerTypes | WardrobeRestrictions.LayerCount;
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
	}

	/// <summary>
	/// For added realism on character models?
	/// </summary>
	public class SkinTag : IComparable<SkinTag>
	{
		[XmlText]
		public string Name;

		[DefaultValue(false)]
		[XmlAttribute("remove")]
		public bool Remove;

		public override string ToString()
		{
			return $"{Name}{(Remove ? "(-)" : "")}";
		}

		public int CompareTo(SkinTag other)
		{
			return Name.CompareTo(other.Name);
		}

		public SkinTag() { }
		public SkinTag(string name)
		{
			Name = name;
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
