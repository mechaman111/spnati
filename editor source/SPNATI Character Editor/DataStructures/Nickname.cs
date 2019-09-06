using Desktop.DataStructures;
using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	public class Nickname : BindableObject, IComparable<Nickname>
	{
		[XmlAttribute("for")]
		public string Character
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlText]
		public string Label
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public Nickname()
		{
		}

		public Nickname(string character, string label)
		{
			Character = character;
			Label = label;
		}

		public int CompareTo(Nickname other)
		{
			int compare = Character.CompareTo(other.Character);
			if (compare == 0)
			{
				compare = Label.CompareTo(other.Label);
			}
			return compare;
		}

		public override string ToString()
		{
			return $"{Character}: {Label}";
		}
	}
}
