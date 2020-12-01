using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ColorNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Provides a constant color value"; }
		}

		public override string Name
		{
			get { return "Color"; }
		}

		public override string Key
		{
			get { return "color"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Color, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.Color, "Value", Color.White)
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			Color value = args.GetProperty<Color>(0);
			return Task.FromResult(new PipelineResult(value));
		}
	}
}

