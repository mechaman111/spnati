using Desktop;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("marker")]
	[Serializable]
	/// <summary>
	/// Contains descriptive information about a marker. This doesn't actually control anything in game; it's just for documentation
	/// </summary>
	public class Marker : IComparable<Marker>, IRecord
	{
		[XmlAttribute("scope")]
		/// <summary>
		/// Controls whether the marker will be visible to other charaters in the editor. Does not control anything in game
		/// </summary>
		public MarkerScope Scope;

		[XmlAttribute("name")]
		/// <summary>
		/// Marker name, what's used in game
		/// </summary>
		public string Name;

		[XmlText]
		/// <summary>
		/// Description about what this marker controls
		/// </summary>
		public string Description;

		[XmlIgnore]
		/// <summary>
		/// Values that get set into this marker
		/// </summary>
		private CountedSet<string> _values = new CountedSet<string>();

		public Marker()
		{
			Scope = MarkerScope.Public;
		}

		public Marker(string name) : this()
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}

		public int CompareTo(Marker other)
		{
			return Name.CompareTo(other.Name);
		}

		public IEnumerable<string> Values
		{
			get { return _values.Values; }
		}

		public int ValueCount
		{
			get { return _values.Count; }
		}

		string IRecord.Name
		{
			get
			{
				return Name;
			}
		}

		public string Key
		{
			get
			{
				return Name;
			}

			set
			{
				Name = value;
			}
		}

		public string Group { get; }

		public void AddValue(string value)
		{
			if (string.IsNullOrEmpty(value)) { return; }
			_values.Add(value);
		}

		public void RemoveValue(string value)
		{
			if (string.IsNullOrEmpty(value)) { return; }
			_values.Remove(value);
		}

		/// <summary>
		/// Extracts the string from a marker equation
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		public static string ExtractConditionPieces(string marker, out MarkerOperator op, out string value, out bool perTarget)
		{
			if (string.IsNullOrEmpty(marker))
			{
				value = "";
				perTarget = false;
				op = MarkerOperator.Equals;
				return marker;
			}
			Match match = Regex.Match(marker, @"([\w\-]+)(\*?)(\s*((?:\>|\<|\=|\!)\=?)\s*(\-?\w+|~\w+~))?");
			op = ToOperator(match.Groups[4].ToString());
			value = match.Groups[5]?.ToString();
			if (value == "")
			{
				value = null;
			}
			perTarget = !string.IsNullOrEmpty(match.Groups[2]?.ToString());
			return match.Groups[1].ToString();
		}

		/// <summary>
		/// Extracts the string and value from a marker of the form "marker[*]", "[+|-]marker[*]", or "marker[*]=value"
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		public static string ExtractPieces(string marker, out string value, out bool perTarget, out string op)
		{
			perTarget = false;
			if (marker == null)
			{
				op = null;
				value = null;
				return null;
			}
			string pattern = @"^(?:(\+|-)([\w-]+\*?)|([\w-]+\*?)\s*([-+*%\/]?=)\s*(.*?)\s*)$";
			Match match = Regex.Match(marker, pattern);

			if (match.Success)
			{
				if (match.Groups[1].Success)
				{
					marker = match.Groups[2].Value;
					op = match.Groups[1].Value;
					value = "1";
				}
				else
				{
					marker = match.Groups[3].Value;
					op = match.Groups[4].Value[0].ToString();
					value = match.Groups[5].Value;
				}
			}
			else if (marker.Contains("="))
			{
				string[] pieces = marker.Split('=');
				marker = pieces[0];
				op = "=";
				value = pieces[1];
			}
			else
			{
				value = null;
				op = null;	
			}

			if (marker.EndsWith("*"))
			{
				perTarget = true;
				marker = marker.Substring(0, marker.Length - 1);
			}
			return marker;
		}

		/// <summary>
		/// Splits a marker of the form marker=5 into the pieces marker, = , and 5
		/// </summary>
		/// <param name="marker">Marker condition to split</param>
		/// <param name="op">Conditional operator</param>
		/// <param name="rhs">Right hand side of equation</param>
		/// <returns>Left side of equation</returns>
		public static string SplitConditional(string marker, out MarkerOperator op, out string rhs)
		{
			///Note: I originally converted *SaidMarker strings into a class instead of repeatedly piecing and unpiecing the parts, 
			///but it complicated a number of things with serialization, primarily performance and maintaining member order


			marker = Regex.Replace(marker, @"\s+", ""); //throw away any whitespace
			Match match = Regex.Match(marker, @"(\w+)((==|<|<=|>=|>|!=)(\w+))*");
			if (match.Groups.Count > 4)
			{
				op = ToOperator(match.Groups[3].ToString());
				rhs = match.Groups[4].ToString();
				return match.Groups[1].ToString();
			}
			else
			{
				op = MarkerOperator.Equals;
				rhs = null;
				return marker;
			}
		}

		private static MarkerOperator ToOperator(string value)
		{
			switch (value)
			{
				case "==":
					return MarkerOperator.Equals;
				case "!=":
					return MarkerOperator.NotEqual;
				case "<":
					return MarkerOperator.LessThan;
				case "<=":
					return MarkerOperator.LessThanOrEqual;
				case ">":
					return MarkerOperator.GreaterThan;
				case ">=":
					return MarkerOperator.GreaterThanOrEqual;
			}
			return MarkerOperator.Equals;
		}

		public string ToLookupString()
		{
			return Name;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public static bool CheckMarker(string condition, Dictionary<string, string> markers)
		{
			MarkerOperator op;
			string value;
			bool perTarget;
			string marker = ExtractConditionPieces(condition, out op, out value, out perTarget);
			int targetIntValue;
			if (string.IsNullOrEmpty(value))
			{
				value = "1";
			}
			int.TryParse(value, out targetIntValue);

			string setValue = markers.Get(marker);
			if (string.IsNullOrEmpty(setValue))
			{
				setValue = "0";
			}
			int intValue;
			int.TryParse(setValue, out intValue);
			switch (op)
			{
				case MarkerOperator.Equals:
					return setValue == value;
				case MarkerOperator.GreaterThan:
					return intValue > targetIntValue;
				case MarkerOperator.GreaterThanOrEqual:
					return intValue >= targetIntValue;
				case MarkerOperator.LessThan:
					return intValue < targetIntValue;
				case MarkerOperator.LessThanOrEqual:
					return intValue <= targetIntValue;
				case MarkerOperator.NotEqual:
					return setValue != value;
			}
			return true;
		}
	}

	public static class MarkerOperatorExtensions
	{
		public static string Serialize(this MarkerOperator op)
		{
			switch (op)
			{
				case MarkerOperator.Equals:
					return "==";
				case MarkerOperator.LessThan:
					return "<";
				case MarkerOperator.GreaterThan:
					return ">";
				case MarkerOperator.LessThanOrEqual:
					return "<=";
				case MarkerOperator.GreaterThanOrEqual:
					return ">=";
				case MarkerOperator.NotEqual:
					return "!=";
			}
			return "";
		}
	}

	public enum MarkerOperator
	{
		Equals,
		LessThan,
		LessThanOrEqual,
		GreaterThan,
		GreaterThanOrEqual,
		NotEqual
	}

	public enum MarkerScope
	{
		Private,
		Public
	}
}
