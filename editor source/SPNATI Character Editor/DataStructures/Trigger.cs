using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Trigger : IComparable<Trigger>
	{
		[XmlAttribute("id")]
		public string Id;

		[XmlElement("case")]
		public List<Case> Cases = new List<Case>();

		public Trigger()
		{
		}

		public Trigger(string tag)
		{
			Id = tag;
		}

		public int CompareTo(Trigger other)
		{
			string tag1 = Id;
			string tag2 = other.Id;
			int compare = TriggerDatabase.Compare(tag1, tag2);
			return compare;
		}

		public override string ToString()
		{
			return Id;
		}
	}
}
