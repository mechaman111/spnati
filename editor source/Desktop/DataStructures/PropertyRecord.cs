using System;
using System.Reflection;

namespace Desktop
{
	/// <summary>
	/// Links a class member to an edit control
	/// </summary>
	public class PropertyRecord : IRecord, IComparable<PropertyRecord>
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public int GroupOrder { get; set; }
		public string Description { get; set; }
		public string Property { get; set; }

		public EditControlAttribute Attribute;
		public Type DataType;
		public Type EditControlType;
		public MemberInfo Member;

		public PropertyRecord() { }

		public PropertyRecord(Type dataType, MemberInfo member, EditControlAttribute attr)
		{
			Attribute = attr;
			DataType = dataType;
			EditControlType = attr.EditControlType;
			Member = member;
			Name = Key = attr.DisplayName;
			Group = attr.GroupName;
			GroupOrder = attr.GroupOrder;
			Description = attr.Description;
			Property = member.Name;
		}

		public string ToLookupString()
		{
			return Name;
		}
		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public int CompareTo(PropertyRecord other)
		{
			int compare = GroupOrder.CompareTo(other.GroupOrder);
			if (compare == 0)
			{
				compare = Name.CompareTo(other.Name);
			}
			return compare;
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
	public abstract class EditControlAttribute : Attribute
	{
		/// <summary>
		/// Type of edit control to use
		/// </summary>
		public abstract Type EditControlType { get; }

		/// <summary>
		/// Type of data this is used with
		/// </summary>
		public Type DataType;

		/// <summary>
		/// Name of record to associate to the control
		/// </summary>
		public string DisplayName;

		/// <summary>
		/// Group this should appear under in record lookup
		/// </summary>
		public string GroupName;

		/// <summary>
		/// Order this record appears in the group menu
		/// </summary>
		public int GroupOrder;

		/// <summary>
		/// Description of what this edits
		/// </summary>
		public string Description;

		/// <summary>
		/// Properties on the same Data object that should trigger PropertyChanged updates (only works with those properties being in other edit controls)
		/// </summary>
		public string[] BoundProperties;

		public EditControlAttribute()
		{
		}
	}
}
