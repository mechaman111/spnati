using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Node that takes an image and returns a copy of it
	/// </summary>
	public class ResultNode : NodeDefinition
	{
		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
					new PortDefinition(PortType.Bitmap, "in")
				};
		}

		public override PortDefinition[] GetOutputs()
		{
			return null;
		}

		public override NodeProperty[] GetProperties()
		{
			return null;
		}

		public override string Name
		{
			get { return "Result"; }
		}
		public override string Key
		{
			get { return "root"; }
			set { }
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap input = args.GetInput<DirectBitmap>(0);
			if (input == null)
			{
				return Task.FromResult(new PipelineResult(new Bitmap(1, 1)));
			}
			return Task.FromResult(new PipelineResult(new Bitmap(input.Bitmap)));
		}
	}
}
