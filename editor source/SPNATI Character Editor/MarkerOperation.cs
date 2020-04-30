using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[Serializable]
	/// <summary>
	/// Represents a change to marker
	/// </summary>
	public class MarkerOperation
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("op")]
		public string Operator;

		[XmlAttribute("value")]
		public string Value;

		[XmlAttribute("when")]
		public string When;

		public MarkerOperation()
		{
		}

		public MarkerOperation(string expression)
		{
			string op;
			string value;
			bool perTarget;
			string marker = Marker.ExtractPieces(expression, out value, out perTarget, out op);
			Name = marker;
			if (perTarget)
			{
				Name += "*";
			}
			Operator = op;
			Value = value;
		}

		public MarkerOperation Copy()
		{
			MarkerOperation clone = MemberwiseClone() as MarkerOperation;
			return clone;
		}

		public override int GetHashCode()
		{
			int hash = Name.GetHashCode();
			hash = (hash * 397) ^ (Operator ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Value ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (When ?? string.Empty).GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(Operator))
			{
				return Name;
			}
			string value = Value;
			if (string.IsNullOrEmpty(value))
			{
				value = "1";
			}
			if (Operator == "=")
			{
				return $"{Name}={value}";
			}
			else if (Operator == "+" && value == "1")
			{
				return $"+{Name}";
			}
			else if (Operator == "-" && value == "1")
			{
				return $"-{Name}";
			}
			else
			{
				return $"{Name} {Operator}= {Value}";
			}
		}
	}
}