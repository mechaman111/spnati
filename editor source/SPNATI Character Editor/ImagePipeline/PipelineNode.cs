using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.Providers;

namespace ImagePipeline
{
	[Serializable]
	[XmlRoot("node")]
	public class PipelineNode : BindableObject, IHookSerialization
	{
		[XmlIgnore]
		public PipelineGraph Graph;

		/// <summary>
		/// Key of this node's definition
		/// </summary>
		[XmlAttribute("type")]
		public string NodeType
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				ApplyDefinition();
			}
		}

		/// <summary>
		/// Unique node ID within the graph
		/// </summary>
		[XmlAttribute("id")]
		public int Id
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Node properties
		/// </summary>
		[XmlIgnore]
		public ObservableCollection<object> Properties
		{
			get { return Get<ObservableCollection<object>>(); }
			set { Set(value); }
		}
		[XmlArray("properties")]
		[XmlArrayItem("property")]
		public List<SerializedProperty> SerializedProperties;

		/// <summary>
		/// X position in graph editor
		/// </summary>
		[XmlAttribute("x")]
		public int X
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		/// <summary>
		/// X position in graph editor
		/// </summary>
		[XmlAttribute("y")]
		public int Y
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[XmlIgnore]
		public IPipelineNode Definition;

		public PipelineNode()
		{
			Properties = new ObservableCollection<object>();
		}

		public override string ToString()
		{
			return Definition?.Name ?? "Node " + Id;
		}

		public void OnBeforeSerialize()
		{
			if (Properties != null && Properties.Count > 0)
			{
				SerializedProperties = new List<SerializedProperty>();
				for (int i = 0; i < Properties.Count; i++)
				{
					SerializedProperties.Add(new SerializedProperty(Definition.Properties[i], Properties[i]));
				}
			}
			else
			{
				SerializedProperties = null;
			}
		}

		public void OnAfterDeserialize(string source)
		{
			ApplyDefinition();
			if (SerializedProperties != null)
			{
				for (int i = 0; i < SerializedProperties.Count; i++)
				{
					object value = SerializedProperties[i].Deserialize(Definition.Properties[i]);
					SetProperty(i, value);
				}
				SerializedProperties = null;
			}
		}

		private void ApplyDefinition()
		{
			Definition = PipelineProvider.GetDefinition(NodeType);
			if (Definition != null)
			{
				var props = Definition.Properties;
				if (props != null)
				{
					for (int i = Properties.Count; i < props.Length; i++)
					{
						Properties.Add(props[i].DefaultValue);
					}
				}
			}
		}

		public object GetProperty(int index)
		{
			if (Properties.Count > index && index >= 0)
			{
				return Properties[index];
			}
			return null;
		}

		/// <summary>
		/// Sets a property
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public void SetProperty(int index, object value)
		{
			if (Properties.Count > index && index >= 0)
			{
				Properties[index] = value;
			}
		}
	}

	//Helper class for serializing properties
	[Serializable]
	public class SerializedProperty
	{
		[XmlArray("values")]
		[XmlArrayItem("value")]
		public List<string> Values = new List<string>();

		public SerializedProperty() { }

		public SerializedProperty(NodeProperty property, object value)
		{
			if (value == null)
			{
				Values.Add("");
			}
			else
			{
				switch (property.Type)
				{
					case NodePropertyType.Integer:
						Values.Add(((int)value).ToString());
						break;
					case NodePropertyType.Boolean:
					case NodePropertyType.String:
					case NodePropertyType.ImageFile:
						Values.Add(value.ToString());
						break;
					case NodePropertyType.CellReference:
						PoseCellReference cellRef = value as PoseCellReference;
						Values.Add(string.Join("/", new string[] {
							cellRef.SheetName.Replace("/", "&sol;"),
							cellRef.Stage.ToString(),
							(cellRef.StageName ?? "").Replace("/", "&sol;"),
							cellRef.Key.Replace("/", "&sol;"),
							(cellRef.CharacterFolder ?? "").Replace("/", "&sol;")
						}));
						break;
					case NodePropertyType.Float:
						float floatValue = (float)value;
						Values.Add(floatValue.ToString(CultureInfo.InvariantCulture));
						break;
					case NodePropertyType.Point:
						Point pt = (Point)value;
						Values.Add(pt.X.ToString());
						Values.Add(pt.Y.ToString());
						break;
					case NodePropertyType.Color:
						int n = ((Color)value).ToArgb();
						Values.Add(n.ToString());
						break;
				}
			}
		}

		public object Deserialize(NodeProperty property)
		{
			if (Values.Count == 0 || Values[0] == "")
			{
				return property.DefaultValue;
			}
			switch (property.Type)
			{
				case NodePropertyType.Boolean:
					bool boolValue;
					bool.TryParse(Values[0], out boolValue);
					return boolValue;
				case NodePropertyType.Float:
					float floatValue;
					float.TryParse(Values[0], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out floatValue);
					return floatValue;
				case NodePropertyType.Integer:
					int intValue;
					int.TryParse(Values[0], out intValue);
					return intValue;
				case NodePropertyType.ImageFile:
				case NodePropertyType.String:
					return Values[0];
				case NodePropertyType.CellReference:
					string[] pieces = Values[0].Split('/');
					int stage;
					if (!int.TryParse(pieces[1], out stage))
					{
						stage = -1;
					}
					PoseCellReference cellRef = new PoseCellReference()
					{
						SheetName = pieces[0].Replace("&sol;", "/"),
						Stage = stage,
						Key = pieces[3].Replace("&sol;", "/")
					};
					if (!string.IsNullOrEmpty(pieces[2]))
					{
						cellRef.StageName = pieces[2].Replace("&sol;", "/");
					}
					if (pieces.Length > 4 && !string.IsNullOrEmpty(pieces[4]))
					{
						cellRef.CharacterFolder = pieces[4].Replace("&sol;", "/");
					}
					return cellRef;
				case NodePropertyType.Point:
					int x;
					int y;
					int.TryParse(Values[0], out x);
					int.TryParse(Values[1], out y);
					return new Point(x, y);
				case NodePropertyType.Color:
					int c;
					int.TryParse(Values[0], out c);
					return Color.FromArgb(c);
				default:
					return property.DefaultValue;
			}
		}
	}
}
