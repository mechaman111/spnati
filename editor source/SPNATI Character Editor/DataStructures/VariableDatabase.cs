using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public static class VariableDatabase
	{
		private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
		private static Dictionary<string, Variable> _tempVariables = new Dictionary<string, Variable>();
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
			Variable v = _variables.Get(name);
			if (v == null)
			{
				v = _tempVariables.Get(name);
				if (v == null)
				{
					v = new Variable();
					v.Description = "Case-specific temporary variable.";
					v.Name = name;
					_tempVariables[name] = v;
				}
			}
			return v;
		}
	}

	[XmlRoot("variables", Namespace = "")]
	public class VariableList
	{
		[XmlElement("variable")]
		public List<Variable> Variables;
	}

	public static class StyleDatabase
	{
		private static Dictionary<string, StyleRule> _styles = new Dictionary<string, StyleRule>();

		static StyleDatabase()
		{
			_styles.Add("!reset", new StyleRule() { ClassName = "!reset", Description = "Resets style to default" });
			_styles.Add("b", new StyleRule() { ClassName = "b", Description = "Bold text" });
			_styles.Add("i", new StyleRule() { ClassName = "i", Description = "Italics text" });
			_styles.Add("u", new StyleRule() { ClassName = "u", Description = "Underline" });
			_styles.Add("s", new StyleRule() { ClassName = "s", Description = "Strikethrough" });
			_styles.Add("highlight", new StyleRule() { ClassName = "highlight", Description = "Highlighted text" });
			_styles.Add("mono", new StyleRule() { ClassName = "mono", Description = "Monospace font" });
			_styles.Add("small", new StyleRule() { ClassName = "small", Description = "Smaller text" });
			_styles.Add("big", new StyleRule() { ClassName = "big", Description = "Bigger text" });
		}

		public static IEnumerable<StyleRule> GlobalStyles
		{
			get { return _styles.Values; }
		}

		public static StyleRule Get(string className, Character character)
		{
			StyleRule rule = _styles.Get(className);
			if (rule == null && character.Styles != null)
			{
				rule = character.Styles.Rules.Find(r => r.ClassName == className);
			}
			return rule;			
		}
	}
}
