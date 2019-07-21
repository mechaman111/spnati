using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.IO
{
	public static class CharacterStyleSheetSerializer
	{
		public static CharacterStyleSheet Load(Character character, string sheetName)
		{
			string path = Path.Combine(character.GetDirectory(), sheetName);
			if (!File.Exists(path))
			{
				CharacterStyleSheet defaultSheet = new CharacterStyleSheet();
				defaultSheet.Name = "styles.css";

				StyleRule selector = new StyleRule();
				selector.ClassName = "example";
				selector.Attributes.Add(new StyleAttribute("color", "blue"));
				selector.Description = "This is an example style.";
				defaultSheet.Rules.Add(selector);
				return defaultSheet;
			}

			CharacterStyleSheet sheet = new CharacterStyleSheet();
			sheet.Name = sheetName;

			//I don't feel like making a robust CSS parser, so this is only going for the very basics
			bool requireAdvanced = false;
			try
			{
				string[] lines = File.ReadAllLines(path);
				StringBuilder working = new StringBuilder();
				StyleRule rule = null;
				foreach (string line in lines)
				{
					string text = line.Trim();
					if (string.IsNullOrEmpty(text))
					{
						continue;
					}
					if (rule == null)
					{
						if (text.Length > 1 && text.EndsWith("{"))
						{
							working.Append(text.Substring(0, text.Length - 1));
						}
						if (text.EndsWith("{"))
						{
							//parse out the relevant class name
							string selectorText = working.ToString().Trim();
							Regex regex = new Regex(@"\.dialogue \.(\w+)\[data-character=""\w+""\]");
							Match match = regex.Match(selectorText);
							if (match.Success)
							{
								string className = match.Groups[1].Value;
								rule = new StyleRule();
								rule.ClassName = className;
								sheet.Rules.Add(rule);
								working.Clear();
							}
							else
							{
								requireAdvanced = true;
								break;
							}
						}
						else
						{
							working.Append(text);
						}
					}
					else
					{
						//building the rule
						if (text == "}")
						{
							working.Clear();
							rule = null;
						}
						else if (text.StartsWith("/*"))
						{
							//description
							rule.Description = CssDecode(text.Replace("/*", "").Replace("*/", ""));
						}
						else
						{
							string style = text;
							if (text.EndsWith("}") && text.Length > 1)
							{
								style = text.Substring(0, text.Length - 1);
							}
							string[] pieces = text.Split(new char[] { ':' }, 2);
							if (pieces.Length == 2)
							{
								string attribute = pieces[0].Trim();
								string value = pieces[1].Trim(new char[] { ' ', ';' });
								StyleAttribute attr = new StyleAttribute(attribute, value);
								rule.Attributes.Add(attr);
							}
							else
							{
								requireAdvanced = true;
								break;
							}
							if (text.EndsWith("}"))
							{
								working.Clear();
								rule = null;
							}
						}
					}
				}


				if (requireAdvanced)
				{
					//we're too dumb to understand this file. Just store the whole thing and force advanced mode
					sheet.Rules.Clear();
					sheet.FullText = File.ReadAllText(path);
					sheet.AdvancedMode = true;
				}
			}
			catch { }

			return sheet;
		}

		public static void Save(Character character, CharacterStyleSheet sheet)
		{
			if (sheet == null) { return; }
			string path = Path.Combine(character.GetDirectory(), sheet.Name);
			List<string> selectors = new List<string>();
			if (sheet.AdvancedMode)
			{
				File.WriteAllText(path, sheet.FullText);
			}
			else
			{
				string id = CharacterDatabase.GetId(character);
				foreach (StyleRule selector in sheet.Rules)
				{
					selectors.Add(selector.Serialize(id));
				}
				string output = string.Join("\r\n\r\n", selectors);
				File.WriteAllText(path, output);
			}
		}

		public static string CssEncode(string input)
		{
			input = input.Replace("{", "\\00007B");
			input = input.Replace("}", "\\00007D");
			input = input.Replace("*", "\\00002A");
			return input;
		}

		public static string CssDecode(string input)
		{
			//AntiXssEncoder lacks a CssDecode, so make up a cheapo one
			input = input.Replace("\\00007B", "{");
			input = input.Replace("\\00007D", "}");
			input = input.Replace("\\00002A", "*");
			return input;
		}
	}
}
