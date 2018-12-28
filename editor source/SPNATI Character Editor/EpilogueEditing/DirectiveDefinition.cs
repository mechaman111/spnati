using System.Collections.Generic;
using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	public class DirectiveDefinition : Definition
	{
		/// <summary>
		/// Which properties can be set in this directive
		/// </summary>
		public HashSet<string> AllowedProperties = new HashSet<string>();
		public HashSet<string> RequiredProperties = new HashSet<string>();
		public HashSet<string> RequiredAnimatedProperties = new HashSet<string>();

		public bool IsAnimatable;

		/// <summary>
		/// Gets whether a particular property is allowed for this directive
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public bool AllowsProperty(string prop)
		{
			return AllowedProperties.Contains(prop);
		}

		/// <summary>
		/// Gets whether a particular property is required
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public bool RequiresProperty(string prop)
		{
			return RequiredProperties.Contains(prop);
		}

		/// <summary>
		/// Gets whether a particular property is required for keyframes
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public bool RequiresAnimatedProperty(string prop)
		{
			return RequiredAnimatedProperties.Contains(prop);
		}
	}

	public class DirectiveProvider : DefinitionProvider<DirectiveDefinition>
	{
		public override void ApplyDefaults(DirectiveDefinition definition)
		{

		}

		public override string GetLookupCaption()
		{
			return "Choose a directive type";
		}
	}
}
