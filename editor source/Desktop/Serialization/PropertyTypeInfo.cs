using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Desktop
{
	/// <summary>
	/// Caches type information for public properties in a BindableObject
	/// </summary>
	public static class PropertyTypeInfo
	{
		private static DualKeyDictionary<Type, string, Type> _mapping = new DualKeyDictionary<Type, string, Type>();
		private static DualKeyDictionary<Type, string, FieldInfo> _fieldMapping = new DualKeyDictionary<Type, string, FieldInfo>();
		private static DualKeyDictionary<Type, string, MemberInfo> _memberMapping = new DualKeyDictionary<Type, string, MemberInfo>();
		private static DualKeyDictionary<Type, string, string> _enumNames = new DualKeyDictionary<Type, string, string>();

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

		public static string GetSerializedEnumValue(Type type, string value)
		{
			string name = _enumNames.Get(type, value);
			if (name == null)
			{
				MemberInfo[] mi = type.GetMember(value);
				if (mi.Length > 0)
				{
					XmlEnumAttribute enumAttribute = mi[0].GetCustomAttribute<XmlEnumAttribute>();
					if (enumAttribute != null)
					{
						value = enumAttribute.Name;
						_enumNames.Set(type, value, value);
						return value;
					}
				}
				_enumNames.Set(type, value, value);
				return value;
			}
			else
			{
				return name;
			}
		}
	}
}
