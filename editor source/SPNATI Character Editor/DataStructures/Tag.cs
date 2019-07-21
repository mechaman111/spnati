using Desktop;
using Desktop.CommonControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Tag : IRecord, IComparable<Tag>, IGroupedItem, INotifyPropertyChanged
	{
		[XmlIgnore]
		public string Name { get { return DisplayName; } }
		[XmlIgnore]
		public string Key { get { return Value; } set { Value = value; } }
		[XmlIgnore]
		public string Group { get; set; }

		[XmlText]
		public string Value;

		[XmlAttribute("display")]
		public string DisplayName;

		[XmlAttribute("description")]
		public string Description;

		[XmlAttribute("paired")]
		public string RawPairings;

		[XmlIgnore]
		public List<string> PairedTags = new List<string>();

		[XmlIgnore]
		public List<string> ChildrenTags = new List<string>();

		[XmlAttribute("gender")]
		public string Gender;

		[XmlIgnore]
		public int Count;

		public event PropertyChangedEventHandler PropertyChanged
		{
			add { }
			remove { }
		}

		public string ToLookupString()
		{
			return Value;
		}

		public override string ToString()
		{
			//return string.Format("{0} ({1})", DisplayName ?? Value, Count);
			return DisplayName ?? Value;
		}

		public int CompareTo(Tag other)
		{
			return Value.CompareTo(other.Value);
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public string GetGroupKey()
		{
			return Group ?? "Misc";
		}
	}
}
