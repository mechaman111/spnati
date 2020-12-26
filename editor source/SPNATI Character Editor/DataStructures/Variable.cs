using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Variable : IRecord
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("description")]
		public string Description;

		[XmlAttribute("example")]
		public string Example;

		/// <summary>
		/// If true, this variable is available in all contextx
		/// </summary>
		[XmlAttribute("global")]
		public bool IsGlobal;

		/// <summary>
		/// If true, values that this character's markers can be set to will be used as valid function names
		/// </summary>
		[XmlAttribute("useMarkers")]
		public bool UseMarkers;

		[XmlElement("function")]
		public List<VariableFunction> Functions = new List<VariableFunction>();

		[XmlElement("param")]
		public List<VariableParameter> Parameters = new List<VariableParameter>();

		public string Key
		{
			get { return Name; }
			set { }
		}

		public string Group
		{
			get { return null; }
		}

		public bool HasFunctions()
		{
			return Functions.Count > 0 || UseMarkers;
		}

		/// <summary>
		/// Gets the function with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public VariableFunction GetFunction(Character character, string name)
		{
			foreach (VariableFunction func in GetFunctions(character))
			{
				if (func.Name == name)
				{
					return func;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets functions available for a particular character
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public IEnumerable<VariableFunction> GetFunctions(Character character)
		{
			foreach (VariableFunction func in Functions)
			{
				yield return func;
			}
			if (UseMarkers && character != null)
			{
				foreach (Marker marker in character.Markers.Value.Values)
				{
					if (marker.ValueCount > 0)
					{
						yield return new VariableFunction(marker);
					}
				}
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public string ToLookupString()
		{
			return Name;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
	}

	public class VariableFunction : IRecord
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("label")]
		public string Label { get; set; }

		[XmlAttribute("example")]
		public string Example { get; set; }

		[XmlElement("param")]
		public List<VariableParameter> Parameters = new List<VariableParameter>();

		[XmlAttribute("description")]
		public string Description;

		public string Key
		{
			get { return Name; }
			set { }
		}

		public string Group { get { return null; } }

		public override string ToString()
		{
			return Name;
		}

		public string ToLookupString()
		{
			if (Label != null)
			{
				return $"${Label} [{Name}]";
			}
			return Name;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public VariableFunction() { }
		public VariableFunction(Marker marker)
		{
			Name = marker.Name;
			Description = $"Gets the value currently stored in marker '{Name}'";
		}
	}

	public class VariableParameter
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("description")]
		public string Description;

		[XmlAttribute("label")]
		public string Label;

		[XmlAttribute("example")]
		public string Example;

		public override string ToString()
		{
			return Name;
		}
	}
}