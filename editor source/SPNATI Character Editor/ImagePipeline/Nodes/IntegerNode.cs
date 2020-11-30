using System.Threading.Tasks;

namespace ImagePipeline
{
	public class IntegerNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Integer"; }
		}

		public override string Key
		{
			get { return "int"; }
			set { }
		}

		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Provides a constant integer value"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Integer, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.Integer, "Value", 0)
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			int value = args.GetProperty<int>(0);
			return Task.FromResult(new PipelineResult(value));
		}
	}
}
