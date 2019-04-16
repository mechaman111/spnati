using System;

namespace SPNATI_Character_Editor
{
	public class PropertyDefinition : Definition, IComparable<PropertyDefinition>
	{
		public int SortOrder;

		public Type PropertyType;

		public PropertyDefinition() { }

		public PropertyDefinition(string name, string displayName, Type propertyType, int order)
		{
			Key = name;
			Name = displayName;
			PropertyType = propertyType;
			SortOrder = order;
		}

		public int CompareTo(PropertyDefinition other)
		{
			return SortOrder.CompareTo(other.SortOrder);
		}
	}
}
