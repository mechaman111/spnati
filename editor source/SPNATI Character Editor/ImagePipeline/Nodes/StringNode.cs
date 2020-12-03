using System.Threading.Tasks;

namespace ImagePipeline
{
	public class StringNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Provides a constant string value"; }
		}

		public override string Name
		{
			get { return "Text"; }
		}

		public override string Key
		{
			get { return "string"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.String, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.String, "Value", "")
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			string value = args.GetProperty<string>(0);
			return Task.FromResult(new PipelineResult(value));
		}
	}
}

