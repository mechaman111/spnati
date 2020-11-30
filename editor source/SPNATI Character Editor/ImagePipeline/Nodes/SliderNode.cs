using System.Threading.Tasks;

namespace ImagePipeline
{
	public class SliderNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Slider"; }
		}

		public override string Key
		{
			get { return "slider"; }
			set { }
		}

		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Provides a value between 0 and 1"; }
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
				new NodeProperty(NodePropertyType.Float, "Value", 0.5f)
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			float value = args.GetProperty<float>(0);
			return Task.FromResult(new PipelineResult(new ConstantFloat(value)));
		}
	}
}
