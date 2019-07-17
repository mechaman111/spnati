using System;
using System.Collections.Generic;
using System.Reflection;

namespace Desktop.Reporting
{
	public static class DataSlicerControlMap
	{
		private static Dictionary<Type, Type> _map;

		public static Type GetControlType(Type slicerType)
		{
			if (_map == null)
			{
				_map = new Dictionary<Type, Type>();
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					string name = assembly.FullName.ToLower();
					if (name.StartsWith("system") ||
						name.StartsWith("microsoft") ||
						name.StartsWith("newtonsoft") ||
						name.StartsWith("mscorlib") ||
						name.StartsWith("vshost") ||
						name.StartsWith("windows"))
					{
						continue;
					}
					foreach (Type type in assembly.GetTypes())
					{
						DataSlicerControlAttribute attrib = type.GetCustomAttribute<DataSlicerControlAttribute>();
						if (attrib != null)
						{
							_map[attrib.SlicerType] = type;
						}
					}
				}
			}

			Type controlType;
			_map.TryGetValue(slicerType, out controlType);
			return controlType;
		}
	}

	public class DataSlicerControlAttribute : Attribute
	{
		public Type SlicerType;

		public DataSlicerControlAttribute(Type slicerType)
		{
			SlicerType = slicerType;
		}
	}

	public interface ISlicerControl
	{
		void SetSlicer(IDataSlicer slicer);
	}
}
