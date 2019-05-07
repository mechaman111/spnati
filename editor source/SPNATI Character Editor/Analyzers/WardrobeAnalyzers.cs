using System;

namespace SPNATI_Character_Editor.Analyzers
{
	public class LayersAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "Layers"; }
		}

		public override string Name
		{
			get { return "Layers"; }
		}

		public override string FullName
		{
			get { return "Wardrobe - Layer Count"; }
		}

		public override string ParentKey
		{
			get { return "Wardrobe"; }
		}

		public override string[] GetValues()
		{
			return null;
		}

		public override int GetValue(Character character)
		{
			return character.Layers;
		}
	}

	public class LayerPositionAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "LayerPosition"; }
		}

		public string Name
		{
			get { return "Position"; }
		}

		public string FullName
		{
			get { return "Layer - Position"; }
		}

		public string ParentKey
		{
			get { return "Wardrobe"; }
		}

		public string[] GetValues()
		{
			return new string[] { "upper", "lower", "both", "head", "neck", "hands", "arms", "feet", "legs", "waist", "other" };
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (Clothing c in character.Wardrobe)
			{
				if (StringOperations.Matches(c.Position, op, value))
				{
					return true;
				}
			}
			return false;
		}
	}

	public class LayerNameAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "LayerName"; }
		}

		public string Name
		{
			get { return "Name"; }
		}

		public string FullName
		{
			get { return "Layer - Name"; }
		}

		public string ParentKey
		{
			get { return "Wardrobe"; }
		}

		public string[] GetValues()
		{
			return null;
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (Clothing c in character.Wardrobe)
			{
				if (StringOperations.Matches(c.GenericName, op, value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
