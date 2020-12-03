using Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	public interface IPipelineNode : IRecord
	{
		/// <summary>
		/// Inputs into the node
		/// </summary>
		PortDefinition[] Inputs { get; }
		/// <summary>
		/// Outputs from the node
		/// </summary>
		PortDefinition[] Outputs { get; }
		/// <summary>
		/// User properties
		/// </summary>
		NodeProperty[] Properties { get; }
		/// <summary>
		/// Process the inputs to produce an output
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		Task<PipelineResult> Process(PipelineArgs args);
		/// <summary>
		/// Node description
		/// </summary>
		string Description { get; }
	}

	public class PipelineContext
	{
		public PipelineSettings Settings;
		public ISkin Character;
		public PoseEntry Cell;
	}

	public class PipelineArgs
	{
		public PipelineContext Context;
		public object[] Inputs;
		public IList<object> Properties;

		public bool HasInput()
		{
			return Inputs != null && Inputs.Length > 0;
		}

		public T GetInput<T>(int index)
		{
			if (index < 0 || index >= Inputs.Length)
			{
				return default(T);
			}
			return (T)Inputs[index];
		}

		public bool HasProperties()
		{
			return Properties != null;
		}

		public T GetProperty<T>(int index)
		{
			if (index < 0 || index >= Properties.Count)
			{
				return default(T);
			}
			return (T)Properties[index];
		}
	}

	public class PipelineResult
	{
		public List<object> Results = new List<object>();

		public PipelineResult(params object[] results)
		{
			if (results != null)
			{
				Results = new List<object>(results);
			}
			else
			{
				Results.Add(null);
			}
		}
	}

	public struct PortDefinition
	{
		public string Name;
		public PortType Type;

		public PortDefinition(PortType type, string name)
		{
			Name = name;
			Type = type;
		}
	}

	public enum NodePropertyType
	{
		ImageFile,
		String,
		Float,
		Integer,
		Boolean,
		CellReference,
		Point,
		Color
	}

	public struct NodeProperty
	{
		public string Name;
		public NodePropertyType Type;
		public Type DataType;
		public object DefaultValue;
		public float MinValue;
		public float MaxValue;

		public NodeProperty(NodePropertyType type, string name) : this(type, name, null, null) { }
		public NodeProperty(NodePropertyType type, string name, object defaultValue) : this(type, name, defaultValue, null) { }
		public NodeProperty(NodePropertyType type, string name, object defaultValue, Type dataType)
		{
			Name = name;
			Type = type;
			DefaultValue = defaultValue ?? GetDefaultProperty(type);
			DataType = dataType;
			MinValue = 0;
			MaxValue = 0;
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Gets the default value for a property type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private static object GetDefaultProperty(NodePropertyType type)
		{
			switch (type)
			{
				case NodePropertyType.Float:
					return 0.0f;
				case NodePropertyType.Integer:
					return 0;
				case NodePropertyType.Boolean:
					return false;
				case NodePropertyType.Point:
					return new Point(0, 0);
				default:
					return null;
			}
		}
	}

	public enum PortType
	{
		/// <summary>
		/// An image
		/// </summary>
		Bitmap,
		/// <summary>
		/// Floating point value
		/// </summary>
		Float,
		/// <summary>
		/// An integer
		/// </summary>
		Integer,
		/// <summary>
		/// A string
		/// </summary>
		String,
		/// <summary>
		/// A color
		/// </summary>
		Color
	}

	public interface IFloatNodeInput
	{
		/// <summary>
		/// Returns a value from 0 to 1 for a pixel
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <returns></returns>
		float Get(int x, int y);
	}
}
