using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Tracks default dialogue (i.e. lines for required cases)
	/// </summary>
	public static class DialogueDatabase
	{
		private static Dictionary<string, Tuple<string, string>> _templates = new Dictionary<string, Tuple<string, string>>();

		public static Tuple<string, string> GetTemplate(string tag)
		{
			Tuple<string, string> tuple;
			if (_templates.TryGetValue(tag, out tuple))
			{
				return tuple;
			}
			return null;
		}

		/// <summary>
		/// Creates a default dialogue line for a case tag
		/// </summary>
		/// <param name="tag">Case tag</param>
		/// <returns></returns>
		public static DialogueLine CreateDefault(string tag)
		{
			Tuple<string, string> template = GetTemplate(tag);
			if (template == null)
				return new DialogueLine();
			DialogueLine line = new DialogueLine(template.Item1, template.Item2);
			return line;
		}

		/// <summary>
		/// Loads default dialogue information
		/// </summary>
		public static void Load()
		{
			foreach(string tag in TriggerDatabase.GetTags())
			{
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
				Tuple<string, string> tuple = new Tuple<string, string>(trigger.DefaultImage, trigger.DefaultText);
				_templates[tag] = tuple;
			}
		}
	}
}
