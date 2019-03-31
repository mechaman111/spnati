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
	}
}
