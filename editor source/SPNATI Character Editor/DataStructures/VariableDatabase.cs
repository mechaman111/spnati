using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public static class VariableDatabase
	{
		private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
		private static List<Variable> _globalVariables = new List<Variable>();

		public static void Load()
		{
			string filename = "variables.xml";
			if (File.Exists(filename))
			{
				TextReader reader = null;
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(VariableList), "");
					reader = new StreamReader(filename);
					VariableList list = serializer.Deserialize(reader) as VariableList;

					foreach (Variable variable in list.Variables)
					{
						_variables[variable.Name] = variable;
						if (variable.IsGlobal)
						{
							_globalVariables.Add(variable);
						}
						SpellChecker.Instance.AddWord(variable.Name, false);
						foreach (VariableFunction func in variable.Functions)
						{
							SpellChecker.Instance.AddWord(func.Name, false);
						}
					}
				}
				finally
				{
					if (reader != null)
						reader.Close();
				}
			}
		}

		public static IEnumerable<Variable> GlobalVariables
		{
			get { return _globalVariables; }
		}

		/// <summary>
		/// Gets a variable by its name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Variable Get(string name)
		{
			return _variables.Get(name);
		}
	}

	[XmlRoot("variables", Namespace = "")]
	public class VariableList
	{
		[XmlElement("variable")]
		public List<Variable> Variables;
	}
}
