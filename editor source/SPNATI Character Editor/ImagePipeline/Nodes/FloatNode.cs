using System.Threading.Tasks;

namespace ImagePipeline
{
	public class FloatNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Decimal"; }
		}

		public override string Key
		{
			get { return "float"; }
			set { }
		}

		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Provides a constant floating point number"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Float, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.Float, "Value", 1.0f, typeof(bool))
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			float value = args.GetProperty<float>(0);
			return Task.FromResult(new PipelineResult(new ConstantFloat(value)));
		}
	}
}
