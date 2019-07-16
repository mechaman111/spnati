using System;
using System.Reflection;

namespace Desktop
{
	/// <summary>
	/// Caches type information for public properties in a BindableObject
	/// </summary>
	public static class PropertyTypeInfo
	{
		public static DualKeyDictionary<Type, string, Type> _mapping = new DualKeyDictionary<Type, string, Type>();
		public static DualKeyDictionary<Type, string, FieldInfo> _fieldMapping = new DualKeyDictionary<Type, string, FieldInfo>();
		public static DualKeyDictionary<Type, string, MemberInfo> _memberMapping = new DualKeyDictionary<Type, string, MemberInfo>();

		public static Type GetType(Type type, string property)
		{
			Type cachedType = _mapping.Get(type, property);
			if (cachedType == null)
			{
				PropertyInfo pi = type.GetProperty(property);
				if (pi == null)
				{
					throw new Exception($"Could not find any property named {property} on type {type.Name}");
				}
				_mapping.Set(type, property, pi.PropertyType);
				cachedType = pi.PropertyType;
			}
			return cachedType;
		}

		public static FieldInfo GetFieldInfo(Type type, string property)
		{
			FieldInfo fi = _fieldMapping.Get(type, property);
			if (fi == null)
			{
				fi = type.GetField(property);
				_fieldMapping.Set(type, property, fi);
			}
			return fi;
		}

		public static MemberInfo GetMemberInfo(Type type, string property)
		{
			MemberInfo mi = _memberMapping.Get(type, property);
			if (mi == null)
			{
				MemberInfo[] mis = type.GetMember(property, BindingFlags.Public | BindingFlags.Instance);
				if (mis.Length > 0)
				{
					mi = mis[0];
					_memberMapping.Set(type, property, mi);
				}
			}
			return mi;
		}
	}
}
