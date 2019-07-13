using SPNATI_Character_Editor.Controls.StyleControls;
using System;
using System.Reflection;

namespace SPNATI_Character_Editor.Providers
{
	public class StyleControlProvider : DefinitionProvider<StyleAttributeRecord>
	{
		static StyleControlProvider()
		{
			Definitions.Instance.Add(new StyleAttributeRecord(null, "other", "Free text attribute"));
			Assembly assembly = typeof(StyleControlProvider).Assembly;
			foreach (Type type in assembly.GetTypes())
			{
				foreach (SubAttributeAttribute attrib in type.GetCustomAttributes<SubAttributeAttribute>())
				{
					StyleAttributeRecord definition = new StyleAttributeRecord(type, attrib.Attribute, attrib.Description);
					Definitions.Instance.Add(definition);
				}
			}
		}

		public override void ApplyDefaults(StyleAttributeRecord definition)
		{
			
		}

		public override string GetLookupCaption()
		{
			return "Choose an Attribute to Add";
		}
	}

	public class StyleAttributeRecord : Definition
	{
		public Type ControlType;

		public StyleAttributeRecord() { }

		public StyleAttributeRecord(Type type, string attribute, string description)
		{
			ControlType = type;
			Key = Name = attribute;
			Description = description;
		}
	}
}
