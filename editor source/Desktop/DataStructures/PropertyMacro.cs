using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Desktop
{
	/// <summary>
	/// Pre-filled property edit control
	/// </summary>
	public sealed class Macro : IRecord
	{
		public string Name;

		public List<PropertyMacro> Properties = new List<PropertyMacro>();

		public string Group
		{
			get { return ""; }
		}

		public string Key
		{
			get { return Name; }
			set { Name = value; }
		}

		string IRecord.Name
		{
			get { return Name; }
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		/// <summary>
		/// Copies one macro's properties into this one
		/// </summary>
		/// <param name="other"></param>
		public void CopyFrom(Macro other)
		{
			Properties.Clear();
			Properties.AddRange(other.Properties);
		}

		public void AddProperty(string property, int index, List<string> values)
		{
			PropertyMacro propMacro = null;
			if (index == -1)
			{
				propMacro = Properties.Find(p => p.Property == property);
				if (propMacro == null)
				{
					propMacro = new PropertyMacro();
					Properties.Add(propMacro);
				}
			}
			else
			{
				propMacro = new PropertyMacro();
				Properties.Add(propMacro);
			}
			propMacro.Property = property;
			propMacro.Values = values;
		}

		public string Serialize()
		{
			List<string> applications = new List<string>();
			foreach (PropertyMacro prop in Properties)
			{
				applications.Add(prop.Serialize());
			}
			return $"{Name}$#{string.Join("#$", applications)}";
		}

		public static Macro Deserialize(string data)
		{
			string[] pieces = data.Split(new string[] { "$#" }, StringSplitOptions.None);
			if (pieces.Length > 1)
			{
				string name = pieces[0];
				Macro macro = new Macro();
				macro.Name = name;
				foreach (string piece in pieces[1].Split(new string[] { "#$" }, StringSplitOptions.None))
				{
					PropertyMacro property = PropertyMacro.Deserialize(piece);
					macro.Properties.Add(property);
				}
				return macro;
			}
			return null;
		}

		public HashSet<string> GetVariables()
		{
			HashSet<string> set = new HashSet<string>();
			foreach (PropertyMacro prop in Properties)
			{
				foreach (string variable in prop.GetVariables())
				{
					if (!string.IsNullOrEmpty(variable))
					{
						set.Add(variable);
					}
				}
			}
			return set;
		}

		public string ToLookupString()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class PropertyMacro
	{
		public string Property;

		public List<string> Values = new List<string>();

		public Dictionary<string, string> VariableMap;

		public string Serialize()
		{
			return $"{Property}%^{string.Join("|*", Values)}";
		}

		public IEnumerable<string> GetVariables()
		{
			HashSet<string> variables = new HashSet<string>();
			string pattern = @"(\$\w+)";
			foreach (string value in Values)
			{
				if (value.Contains("$"))
				{
					MatchCollection matches = Regex.Matches(value, pattern);
					foreach (Match match in matches)
					{
						if (match.Success)
						{
							yield return match.Groups[1].Value;
						}
					}
				}
			}
		}

		public static PropertyMacro Deserialize(string data)
		{
			PropertyMacro application = new PropertyMacro();
			string[] pieces = data.Split(new string[] { "%^" }, StringSplitOptions.None);
			if (pieces.Length > 1)
			{
				application.Property = pieces[0];
				foreach (string value in pieces[1].Split(new string[] { "|*" }, StringSplitOptions.None))
				{
					application.Values.Add(value);
				}
			}
			return application;
		}

		public override string ToString()
		{
			return $"{Property}: {string.Join(",", Values)}";
		}
	}
}
