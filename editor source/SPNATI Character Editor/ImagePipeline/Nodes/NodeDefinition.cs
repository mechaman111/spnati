using Desktop;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public abstract class NodeDefinition : IPipelineNode
	{
		private PortDefinition[] _inputs;
		private PortDefinition[] _outputs;
		private NodeProperty[] _properties;

		public PortDefinition[] Inputs
		{
			get
			{
				if (_inputs == null)
				{
					_inputs = GetInputs();
				}
				return _inputs;
			}
		}

		public PortDefinition[] Outputs
		{
		get

			{
				if (_outputs == null)
				{
					_outputs = GetOutputs();
				}
				return _outputs;
			}
		}

		public NodeProperty[] Properties
		{
			get
			{
				if (_properties == null)
				{
					_properties = GetProperties();
				}
				return _properties;
			}
		}

		public abstract string Name { get; }
		public abstract string Key { get; set; }

		public virtual string Group { get; }
		public virtual string Description { get; }

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public abstract Task<PipelineResult> Process(PipelineArgs args);
		
		public string ToLookupString()
		{
			return Name;
		}

		public abstract PortDefinition[] GetInputs();
		public abstract PortDefinition[] GetOutputs();
		public abstract NodeProperty[] GetProperties();
	}
}
