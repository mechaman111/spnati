using Desktop;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class AlternateSkin
	{
		[XmlElement("costume")]
		public List<SkinLink> Skins = new List<SkinLink>();
	}

	[XmlRoot("costume", Namespace = "")]
	/// <summary>
	/// Alternate skin data stored in costume.xml
	/// </summary>
	public class Costume : IRecord, IWardrobe
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

		[XmlIgnore]
		public Character Character { get; set; }

		[XmlIgnore]
		public SkinLink Link { get; set; }

		public string Name
		{
			get
			{
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

		public void OnAfterDeserialize()
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
	}

	/// <summary>
	/// For added realism on character models?
	/// </summary>
	public class SkinTag
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

		[XmlText]
		public string Name;

		[XmlIgnore]
		public Costume Costume { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
