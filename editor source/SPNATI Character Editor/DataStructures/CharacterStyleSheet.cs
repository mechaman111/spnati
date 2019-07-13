using Desktop.DataStructures;
using SPNATI_Character_Editor.Controls.StyleControls;
using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// CSS stylesheet that follows a very strict format
	/// </summary>
	public class CharacterStyleSheet : BindableObject
	{
		public string Name;

		public ObservableCollection<StyleRule> Rules
		{
			get { return Get<ObservableCollection<StyleRule>>(); }
			set { Set(value); }
		}

		public bool AdvancedMode
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public string FullText
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public CharacterStyleSheet()
		{
			Rules = new ObservableCollection<StyleRule>();
		}
	}

	public class StyleRule : BindableObject
	{
		public string ClassName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public string Description
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[StyleAttributeEditControl(DisplayName = "Attribute")]
		public ObservableCollection<StyleAttribute> Attributes
		{
			get { return Get<ObservableCollection<StyleAttribute>>(); }
			set { Set(value); }
		}

		public StyleRule()
		{
			Attributes = new ObservableCollection<StyleAttribute>();
		}

		public string Serialize(string characterId)
		{
			List<string> output = new List<string>();
			output.Add($".dialogue .{ClassName}[data-character=\"{characterId}\"] {{");
			output.Add("  /*" + CharacterStyleSheetSerializer.CssEncode(Description) + "*/");
			foreach (StyleAttribute attribute in Attributes)
			{
				output.Add($"  {attribute.Name}: {attribute.Value};");
			}
			output.Add("}");
			return string.Join("\r\n", output);
		}

		public override string ToString()
		{
			return ClassName;
		}
	}

	public class StyleAttribute : BindableObject
	{
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); }
		}
		public string Value
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public StyleAttribute() { }
		public StyleAttribute(string attribute, string value)
		{
			Name = attribute;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Name}: {Value}";
		}
	}
}
