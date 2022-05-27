using Desktop;
using SPNATI_Character_Editor.Controls;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public static class VariableDatabase
	{
		private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
		private static Dictionary<string, Variable> _tempVariables = new Dictionary<string, Variable>();
		private static List<Variable> _globalVariables = new List<Variable>();

		public static void Clear()
		{
			_variables.Clear();
			_tempVariables.Clear();
			_globalVariables.Clear();
		}

		public static void Load()
		{
			string path = Path.Combine(Config.SpnatiDirectory, "opponents");
			string filename = Path.Combine(path, "variables.xml");
			if (!File.Exists(filename))
			{
				//use the old location for backwards compatibility
				filename = Path.Combine(Config.ExecutableDirectory, "variables.xml");
			}

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
						Add(variable);
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

		public static void Add(Variable variable)
		{
			_variables[variable.Name] = variable;
			bool isGlobal = variable.IsGlobal;
			if (isGlobal)
			{
				_globalVariables.Add(variable);
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
		public static Variable Get(string name, bool allowTemporary = true)
		{
			Variable v = _variables.Get(name);
			if (v == null && allowTemporary)
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

		public static IEnumerable<Variable> GetVariablesForTag(string tag)
		{
			foreach (Variable v in GetVariablesForTag(tag, _variables.Values))
			{
				yield return v;
			}
			foreach (Variable v in GetVariablesForTag(tag, _tempVariables.Values))
			{
				yield return v;
			}
		}

		private static IEnumerable<Variable> GetVariablesForTag(string tag, Dictionary<string, Variable>.ValueCollection values)
		{
			foreach (Variable v in values)
			{
				if (TriggerDatabase.IsVariableAvailable(tag, v.Name))
				{
					yield return v;
				}
			}
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

	public class VariableContext
	{
		public Case Case;
		public Line Line;

		public VariableContext(Case c, Line line)
		{
			Case = c;
			Line = line;
		}
	}

	public class VariableProvider : IRecordProvider<Variable>
	{
		private VariableContext _context;

		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }
		public bool TrackRecent { get { return false; } }

		public IRecord Create(string key)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new System.NotImplementedException();
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name", "Description" };
			info.Caption = "Choose a Variable";
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Variable v = record as Variable;
			return new ListViewItem(new string[] { v.Name, v.Description });
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			string tag = _context.Case.Tag ?? "";
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (Variable v in VariableDatabase.GetVariablesForTag(tag))
			{
				if (v.Name.ToLower().Contains(text))
				{
					list.Add(v);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_context = context as VariableContext;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}

	public class VariableFunctionProvider : IRecordProvider<VariableFunction>
	{
		private Variable _variable;

		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }
		public bool TrackRecent { get { return false; } }

		public IRecord Create(string key)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new System.NotImplementedException();
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name", "Description" };
			info.Caption = "Choose a Function";
		}

		public ListViewItem FormatItem(IRecord record)
		{
			VariableFunction func = record as VariableFunction;
			return new ListViewItem(new string[] { func.Name, func.Description });
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (VariableFunction func in _variable.Functions)
			{
				if (func.Name.ToLower().Contains(text))
				{
					list.Add(func);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_variable = context as Variable;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}
