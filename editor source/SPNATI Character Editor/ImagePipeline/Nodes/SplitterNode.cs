using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Splits an image into separate RGBA channels
	/// </summary>
	public class SplitterNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Splitter"; }
		}

		public override string Key
		{
			get { return "split"; }
			set { }
		}

		public override string Group
		{
			get { return "Channel"; }
		}

		public override string Description
		{
			get { return "Splits an image into separate R, G, B, and A numbers"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "image")
			};
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Float, "R"),
				new PortDefinition(PortType.Float, "G"),
				new PortDefinition(PortType.Float, "B"),
				new PortDefinition(PortType.Float, "A"),
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return null;
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				ConstantFloat reader = new ConstantFloat(0);
				return Task.FromResult(new PipelineResult(reader, reader, reader, reader));
			}
			else
			{
				ChannelReader r = new ChannelReader(bmp, ColorChannel.R);
				ChannelReader g = new ChannelReader(bmp, ColorChannel.G);
				ChannelReader b = new ChannelReader(bmp, ColorChannel.B);
				ChannelReader a = new ChannelReader(bmp, ColorChannel.A);
				return Task.FromResult(new PipelineResult(r, g, b, a));
			}

		}
	}
}
