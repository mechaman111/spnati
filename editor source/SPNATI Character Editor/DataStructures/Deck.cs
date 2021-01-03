using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[Serializable]
	public class Deck : BindableObject, IRecord, IDirtiable
	{
		public Deck()
		{
			Fronts = new ObservableCollection<CardFront>();
			Backs = new ObservableCollection<CardBack>();
		}

		[XmlAttribute("id")]
		public string Key
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("title")]
		[Text(DisplayName = "Title", Description = "Name of set as it appears in the game")]
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public string Group { get { return null; } }

		[XmlElement("subtitle")]
		[Text(DisplayName = "Subtitle", Description = "An optional subtitle displayed in game")]
		public string SubTitle
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("description")]
		[Text(DisplayName = "Description", Multiline = true, RowHeight = 50, Description = "Description about this set")]
		public string Description
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("credits")]
		[Text(DisplayName = "Credits", Description = "Who made the art")]
		public string Credits
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("front")]
		public ObservableCollection<CardFront> Fronts
		{
			get { return Get<ObservableCollection<CardFront>>(); }
			set { Set(value); }
		}

		[XmlElement("back")]
		public ObservableCollection<CardBack> Backs
		{
			get { return Get<ObservableCollection<CardBack>>(); }
			set { Set(value); }
		}

		[XmlElement("unlockChar")]
		[RecordSelect(DisplayName = "Character", RecordType = typeof(Character),
			Description = "If a collectible is required to unlock this deck, the character the collectible belongs to")]
		public string Character
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlElement("unlockCollectible")]
		[RecordSelect(DisplayName = "Collectible", RecordType = typeof(Collectible), BoundProperties = new string[] { "Character" },
			Description = "Collectible that is required in order to unlock this deck")]
		public string Collectible
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("online")]
		[ComboBox(DisplayName = "Status", Description = "Where the epilogue is available", GroupOrder = 1, Options = new string[] {
			"event",
			"online",
			"offline",
			"testing",
			"unlisted",
		})]
		[XmlElement("status")]
		public string Status;

		private bool _dirty;
		[XmlIgnore]
		public bool IsDirty
		{
			get { return _dirty; }
			set
			{
				_dirty = value;
				OnDirtyChanged?.Invoke(this, _dirty);
			}
		}

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml { get; set; }

		public event EventHandler<bool> OnDirtyChanged;

		public int CompareTo(IRecord other)
		{
			return Key.CompareTo(other.Key);
		}

		public string ToLookupString()
		{
			return Key;
		}

		public CardFront AddFront()
		{
			CardFront front = new CardFront();
			Fronts.Add(front);
			return front;
		}

		public void RemoveFront(CardFront front)
		{
			Fronts.Remove(front);
		}

		public CardBack AddBack()
		{
			string prefix = Key;
			HashSet<string> usedIds = new HashSet<string>();
			foreach (CardBack back in Backs)
			{
				usedIds.Add(back.Id);
			}
			int id = 1;
			string key = $"{prefix}{id}";
			while (usedIds.Contains(key))
			{
				id++;
				key = $"{prefix}{id}";
			}
			CardBack newBack = new CardBack()
			{
				Id = key
			};
			if (Backs.Count > 0)
			{
				newBack.Src = Backs[Backs.Count - 1].Src;
			}
			Backs.Add(newBack);
			return newBack;
		}

		public void RemoveBack(CardBack back)
		{
			Backs.Remove(back);
		}
	}

	public class CardFront : BindableObject
	{
		[XmlAttribute("src")]
		public string Src
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlAttribute("rank")]
		public string Ranks
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlAttribute("suit")]
		public string Suits
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml { get; set; }

		public override string ToString()
		{
			string name = (Src ?? "").Replace("%s", "").Replace("%i", "");
			if (name.StartsWith(Config.SpnatiDirectory.Replace('\\', '/')))
			{
				name = name.Substring(Config.SpnatiDirectory.Length + 1);
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "New Front";
			}
			else
			{
				name = Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)).Replace('\\', '/');
			}
			return name;
		}
	}

	public class CardBack : BindableObject
	{
		[XmlAttribute("id")]
		public string Id
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlAttribute("src")]
		public string Src
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlAnyElement]
		public List<System.Xml.XmlElement> ExtraXml { get; set; }
	}
}
