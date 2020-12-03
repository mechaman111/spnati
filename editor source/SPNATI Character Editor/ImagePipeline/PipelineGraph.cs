using Desktop;
using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	/// <summary>
	/// A graph describing an imaging pipeline
	/// </summary>
	[Serializable]
	[XmlRoot("graph", Namespace = "")]
	public class PipelineGraph : BindableObject, IHookSerialization, IRecord
	{
		[XmlIgnore]
		public ISkin Character;
		private Random _rand = new Random();

		[XmlAttribute("key")]
		public string Key { get; set; }
		[XmlIgnore]
		public string Group => null;

		private Dictionary<int, PipelineResult> _processedNodes = new Dictionary<int, PipelineResult>();

		/// <summary>
		/// Starting nodes
		/// </summary>
		[XmlElement("root")]
		public PipelineNode MasterNode
		{
			get { return Get<PipelineNode>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Flat list of other nodes
		/// </summary>
		[XmlArray("nodes")]
		[XmlArrayItem("node")]
		public ObservableCollection<PipelineNode> Nodes
		{
			get { return Get<ObservableCollection<PipelineNode>>(); }
			set { Set(value); }
		}

		[XmlIgnore]
		public Dictionary<int, PipelineNode> NodeMap = new Dictionary<int, PipelineNode>();

		[XmlArray("connections")]
		[XmlArrayItem("connection")]
		public ObservableCollection<Connection> Connections
		{
			get { return Get<ObservableCollection<Connection>>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Map of nodes connected to a Node ID + input index
		/// </summary>
		private DualKeyDictionary<int, int, PortConnection> _inputMap = new DualKeyDictionary<int, int, PortConnection>();
		/// <summary>
		/// Map of nodes connected to a node ID output + index
		/// </summary>
		private DualKeyDictionary<int, int, List<PortConnection>> _outputMap = new DualKeyDictionary<int, int, List<PortConnection>>();

		[XmlElement("name")]
		/// <summary>
		/// Display name for the graph
		/// </summary>
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[XmlIgnore]
		public int NextId
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public PipelineGraph()
		{
			Nodes = new ObservableCollection<PipelineNode>();
			Connections = new ObservableCollection<Connection>();
			NextId = 1;
			MasterNode = new PipelineNode()
			{
				Id = GetId(),
				NodeType = "root",
				X = 600,
				Y = 200,
				Graph = this
			};
			NodeMap[MasterNode.Id] = MasterNode;
		}

		public override string ToString()
		{
			return Name;
		}
		public string ToLookupString()
		{
			return Name;
		}
		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public int GetId()
		{
			return NextId++;
		}

		public void OnBeforeSerialize()
		{
			Nodes.Clear();
			foreach (PipelineNode node in NodeMap.Values)
			{
				node.OnBeforeSerialize();
				if (node != MasterNode)
				{
					Nodes.Add(node);
				}
			}
			Nodes.Sort((n1, n2) => n1.Id.CompareTo(n2.Id));
		}

		public void OnAfterDeserialize(string source)
		{
			NodeMap.Clear();
			NodeMap[MasterNode.Id] = MasterNode;
			int maxId = MasterNode.Id;
			MasterNode.Graph = this;
			MasterNode.OnAfterDeserialize(source);
			foreach (PipelineNode node in Nodes)
			{
				maxId = Math.Max(node.Id, maxId);
				node.Graph = this;
				NodeMap[node.Id] = node;
				node.OnAfterDeserialize(source);
			}
			NextId = maxId + 1;

			foreach (Connection connection in Connections)
			{
				CacheConnection(connection);
			}
		}

		/// <summary>
		/// Adds a node of the given type
		/// </summary>
		/// <param name="type"></param>
		public PipelineNode AddNode(string type)
		{
			PipelineNode node = new PipelineNode();
			node.X = _rand.Next(400);
			node.Y = _rand.Next(350);
			node.NodeType = type;
			node.Id = GetId();
			node.Graph = this;
			NodeMap[node.Id] = node;
			Nodes.Add(node);
			return node;
		}

		/// <summary>
		/// Removes a node from the graph
		/// </summary>
		/// <param name="node"></param>
		public void RemoveNode(PipelineNode node)
		{
			//remove all connections to and from the node
			for (int i = Connections.Count - 1; i >= 0; i--)
			{
				Connection c = Connections[i];
				if (c.From == node.Id || c.To == node.Id)
				{
					Disconnect(c);
				}
			}
			NodeMap.Remove(node.Id);
			Nodes.Remove(node);
		}

		private void CacheConnection(Connection connection)
		{
			PipelineNode from;
			PipelineNode to;
			NodeMap.TryGetValue(connection.From, out from);
			NodeMap.TryGetValue(connection.To, out to);
			if (from != null && to != null)
			{
				_inputMap.Set(connection.To, connection.ToIndex, new PortConnection(from, connection.FromIndex));
				List<PortConnection> nodes = _outputMap.Get(connection.From, connection.FromIndex);
				if (nodes == null)
				{
					nodes = new List<PortConnection>();
					_outputMap.Set(connection.From, connection.FromIndex, nodes);
				}
				nodes.Add(new PortConnection(to, connection.ToIndex));
			}
		}

		/// <summary>
		/// Iterates over all inputs into a node and their ancestors
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private IEnumerable<PipelineNode> GetInputs(PipelineNode node, HashSet<PipelineNode> visited)
		{
			if (visited.Contains(node))
			{
				yield break;
			}
			visited.Add(node);
			if (node.Definition.Inputs != null)
			{
				for (int i = 0; i < node.Definition.Inputs.Length; i++)
				{
					PortConnection connection = GetInput(node, i);
					if (connection != null)
					{
						PipelineNode input = connection.Node;
						yield return input;
						foreach (PipelineNode child in GetInputs(input, visited))
						{
							yield return child;
						}
					}
				}
			}
		}

		/// <summary>
		/// Verifies that a connection is valid
		/// </summary>
		/// <param name="from">Node whose output this connection starts from</param>
		/// <param name="to">Node whose input this connectino ends at</param>
		/// <param name="input">Index of the input in the To node</param>
		public bool ValidateConnection(PipelineNode from, int fromInput, PipelineNode to, int input)
		{
			if (from == to)
			{
				return false;
			}
			//make sure the data types match
			if (from.Definition.Outputs[fromInput].Type != to.Definition.Inputs[input].Type)
			{
				return false;
			}

			//check for loops
			foreach (PipelineNode parent in GetInputs(from, new HashSet<PipelineNode>()))
			{
				if (parent == to)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Connects two nodes together
		/// </summary>
		/// <param name="from">Node whose output this connection starts from</param>
		/// <param name="to">Node whose input this connectino ends at</param>
		/// <param name="input">Index of the input in the To node</param>
		public bool Connect(PipelineNode from, int output, PipelineNode to, int input)
		{
			//first validate that this doesn't produce a cycle
			if (!ValidateConnection(from, output, to, input))
			{
				return false;
			}

			//if there's already a connection here, kill it
			Disconnect(to, input);

			Connection connection = new Connection()
			{
				From = from.Id,
				FromIndex = output,
				To = to.Id,
				ToIndex = input
			};
			CacheConnection(connection);
			Connections.Add(connection);
			return true;
		}

		/// <summary>
		/// Removes a connection
		/// </summary>
		/// <param name="connection"></param>
		public void Disconnect(Connection connection)
		{
			_inputMap.Remove(connection.To, connection.ToIndex);
			List<PortConnection> outputs = _outputMap.Get(connection.From, connection.FromIndex);
			if (outputs != null)
			{
				int connectionIndex = outputs.FindIndex(c => c.Node.Id == connection.To && c.Index == connection.ToIndex);
				if (connectionIndex >= 0)
				{
					outputs.RemoveAt(connectionIndex);
					if (outputs.Count == 0)
					{
						_outputMap.Remove(connection.From, connection.FromIndex);
					}
				}
			}
			Connections.Remove(connection);
		}

		/// <summary>
		/// Disconnects a node's input
		/// </summary>
		/// <param name="node"></param>
		/// <param name="input"></param>
		public void Disconnect(PipelineNode node, int input)
		{
			for (int i = 0; i < Connections.Count; i++)
			{
				Connection c = Connections[i];
				if (c.To == node.Id && c.ToIndex == input)
				{
					Disconnect(c);
					return;
				}
			}
		}

		public void DisposeResults()
		{
			if (_processedNodes != null)
			{
				//dispose all intermediate results
				foreach (KeyValuePair<int, PipelineResult> kvp in _processedNodes)
				{
					if (kvp.Key != MasterNode.Id)
					{
						foreach (object value in kvp.Value.Results)
						{
							if (value is IDisposable)
							{
								((IDisposable)value).Dispose();
							}
						}
					}
				}
				_processedNodes.Clear();
			}
		}

		public bool HasNodeOutput(int id)
		{
			return _processedNodes.ContainsKey(id);
		}

		/// <summary>
		/// Gets the output for a node
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public PipelineResult GetNodeOutput(int id)
		{
			PipelineResult result;
			_processedNodes.TryGetValue(id, out result);
			return result;
		}

		public PipelineNode GetNode(int id)
		{
			PipelineNode node;
			NodeMap.TryGetValue(id, out node);
			return node;
		}

		/// <summary>
		/// Gets the node connected to an input
		/// </summary>
		/// <param name="node"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public PortConnection GetInput(PipelineNode node, int index)
		{
			return _inputMap.Get(node.Id, index);
		}

		/// <summary>
		/// Gets the nodes connected to a node's output
		/// </summary>
		/// <param name="node">Node to query</param>
		/// <param name="index">Output index</param>
		/// <returns></returns>
		public List<PortConnection> GetOutputs(PipelineNode node, int index)
		{
			List<PortConnection> list = _outputMap.Get(node.Id, index);
			if (list == null)
			{
				list = new List<PortConnection>();
			}
			return list;
		}

		/// <summary>
		/// Runs through the whole pipeline
		/// </summary>
		/// <returns></returns>
		public async Task<Bitmap> Process(PoseEntry cell, PipelineSettings settings)
		{
			DisposeResults();
			PipelineResult result = await Process(cell, settings, MasterNode);
			return result.Results[0] as Bitmap;
		}
		public async Task<PipelineResult> Process(PoseEntry cell, PipelineSettings settings, PipelineNode node)
		{
			PipelineContext context = new PipelineContext()
			{
				Settings = settings,
				Cell = cell,
				Character = Character
			};

			Dictionary<int, PipelineResult> processedNodes = new Dictionary<int, PipelineResult>();
			LinkedList<PipelineNode> nodesToProcess = new LinkedList<PipelineNode>();
			nodesToProcess.AddLast(node);
			while (nodesToProcess.Count > 0)
			{
				await ProcessNode(context, nodesToProcess, processedNodes);
			}

			if (settings.DisallowCache)
			{
				settings.Cache.Clear();
			}

			PipelineResult result;
			processedNodes.TryGetValue(node.Id, out result);
			_processedNodes = processedNodes;
			return result;
		}

		private async Task ProcessNode(PipelineContext context, LinkedList<PipelineNode> nodesToProcess, Dictionary<int, PipelineResult> processedNodes)
		{
			LinkedListNode<PipelineNode> linkNode = nodesToProcess.First;
			PipelineNode node = linkNode.Value;
			if (processedNodes.ContainsKey(node.Id))
			{
				//this was already processed
				nodesToProcess.RemoveFirst();
				return;
			}

			bool allProcessed = true;
			PortDefinition[] inputNodes = node.Definition.Inputs;
			if (inputNodes != null)
			{
				//see if all the inputs are satisfied, and if not, add them to the processing queue
				for (int i = 0; i < inputNodes.Length; i++)
				{
					PortConnection connection = GetInput(node, i);
					if (connection != null)
					{
						PipelineNode connectedNode = connection.Node;
						if (!processedNodes.ContainsKey(connectedNode.Id))
						{
							nodesToProcess.AddBefore(linkNode, connectedNode);
							allProcessed = false;
						}
					}
				}
				if (!allProcessed)
				{
					return;
				}
			}

			//gather the inputs and process the node
			PortDefinition[] pipes = node.Definition.Inputs;
			object[] inputs = pipes != null ? new object[pipes.Length] : null;
			if (inputs != null)
			{
				for (int i = 0; i < node.Definition.Inputs.Length; i++)
				{
					PortConnection connection = GetInput(node, i);
					if (connection == null)
					{
						inputs[i] = GetDefaultInput(pipes[i].Type);
					}
					else
					{
						PipelineResult result = processedNodes.Get(connection.Node.Id);
						inputs[i] = result.Results[connection.Index];
					}
				}
			}

			PipelineArgs args = new PipelineArgs()
			{
				Context = context,
				Inputs = inputs,
				Properties = node.Properties
			};
			PipelineResult output = await node.Definition.Process(args);
			processedNodes[node.Id] = output;

			nodesToProcess.RemoveFirst(); //done with this node
		}

		private object GetDefaultInput(PortType type)
		{
			switch (type)
			{
				case PortType.Float:
					return new ConstantFloat(0);
				case PortType.Integer:
					return 0;
				case PortType.Color:
					return Color.White;
				default:
					return null;
			}
		}
	}

	public class PipelineSettings
	{
		/// <summary>
		/// If true, forces cells to be reimported from KKL
		/// </summary>
		public bool DisallowCache { get; set; }
		/// <summary>
		/// True when in the graph editor
		/// </summary>
		public bool PreviewMode { get; set; }
		/// <summary>
		/// Cache object for processors to cache things between runs
		/// </summary>
		public Dictionary<string, object> Cache { get; set; }
		/// <summary>
		/// An image to override the cell node's image
		/// </summary>
		public DirectBitmap CellOverride { get; set; }
		/// <summary>
		/// A key to use for the cell node's override
		/// </summary>
		public string CellOverrideKey { get; set; }

		public PipelineSettings()
		{
			Cache = new Dictionary<string, object>();
		}

		public PipelineSettings(PipelineSettings original)
		{
			DisallowCache = original.DisallowCache;
			PreviewMode = original.PreviewMode;
			Cache = original.Cache;
			CellOverride = original.CellOverride;
		}
	}

	[Serializable]
	public class Connection : ICloneable
	{
		[XmlAttribute("from")]
		/// <summary>
		/// Node whose output this connection comes from
		/// </summary>
		public int From;
		/// <summary>
		/// Index of output port
		/// </summary>
		[XmlAttribute("fromN")]
		public int FromIndex;
		[XmlAttribute("to")]
		/// <summary>
		/// Node that this connection is plugged into an Input
		/// </summary>
		public int To;
		[XmlAttribute("n")]
		/// <summary>
		/// Index of the input in the To node
		/// </summary>
		public int ToIndex;

		/// <summary>
		/// User-vertices
		/// </summary>
		[XmlArray("vertices")]
		[XmlArrayItem("pt")]
		public List<Point2D> Vertices = new List<Point2D>();

		public event EventHandler VerticesChanged;

		public object Clone()
		{
			Connection copy = MemberwiseClone() as Connection;
			copy.Vertices = new List<Point2D>();
			for (int j = 0; j < Vertices.Count; j++)
			{
				Point2D pt = Vertices[j];
				Point2D newPt = new Point2D(pt.X, pt.Y);
				copy.Vertices.Add(newPt);
			}
			return copy;
		}

		/// <summary>
		/// Adds a new vertex
		/// </summary>
		/// <param name="position">Position in workspace</param>
		/// <returns>The index of the vertex</returns>
		public int InsertVertex(int index, Point2D position)
		{
			if (index < 0)
			{
				Vertices.Add(position);
			}
			else
			{
				Vertices.Insert(index, position);
			}
			OnVerticesChanged();
			return index >= 0 ? index : Vertices.Count - 1;
		}

		public void RemoveVertex(int index)
		{
			Vertices.RemoveAt(index);
			OnVerticesChanged();
		}

		public void ReplaceVertex(int index, Point2D newVertex)
		{
			Vertices[index] = newVertex;
			OnVerticesChanged();
		}

		private void OnVerticesChanged()
		{
			VerticesChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	[Serializable]
	public class PoseCellReference
	{
		[XmlAttribute("sheet")]
		public string SheetName;
		[XmlAttribute("stage")]
		[DefaultValue(-1)]
		public int Stage = -1;
		[XmlAttribute("stageName")]
		public string StageName;
		[XmlAttribute("key")]
		public string Key;
		[XmlAttribute("folder")]
		public string CharacterFolder;

		public override string ToString()
		{
			return $"{SheetName} > {StageName ?? Stage.ToString()} > {Key}";
		}

		public PoseCellReference() { }

		public PoseCellReference(PoseEntry cell)
		{
			SheetName = cell.Stage.Sheet.Name;
			if (string.IsNullOrEmpty(cell.Stage.Name))
			{
				Stage = cell.Stage.Stage;
			}
			else
			{
				StageName = cell.Stage.Name;
			}
			Key = cell.Key;
			CharacterFolder = cell.Stage.Sheet.Matrix.Character.FolderName;
		}

		public PoseCellReference(string sheet, int stage, string key)
		{
			SheetName = sheet;
			Stage = stage;
			Key = key;
		}

		public PoseCellReference(string sheet, string stage, string key)
		{
			SheetName = sheet;
			StageName = stage;
			Key = key;
		}

		public PoseEntry GetCell(PoseMatrix matrix)
		{
			if (!string.IsNullOrEmpty(CharacterFolder) && CharacterFolder != matrix.Character.FolderName)
			{
				Character character = CharacterDatabase.Get(CharacterFolder);
				if (character != null)
				{
					matrix = CharacterDatabase.GetPoseMatrix(character);
				}
			}
			PoseSheet sheet = matrix.Sheets.Find(s => s.Name == SheetName);
			if (sheet == null)
			{
				return null;
			}
			PoseStage stage = sheet.Stages.Find(s => (!string.IsNullOrEmpty(StageName) && s.Name == StageName) || (Stage >= 0 && s.Stage == Stage));
			if (stage == null)
			{
				return null;
			}
			PoseEntry entry = stage.GetCell(Key);
			return entry;
		}
	}

	public class PortConnection
	{
		public PipelineNode Node;
		public int Index;

		public PortConnection(PipelineNode node, int index)
		{
			Node = node;
			Index = index;
		}

		public override string ToString()
		{
			return Node.ToString() + ": " + Index;
		}
	}
}
