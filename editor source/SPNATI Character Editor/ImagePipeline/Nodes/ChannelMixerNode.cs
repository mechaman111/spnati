using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ChannelMixerNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Channel"; }
		}

		public override string Description
		{
			get { return "Controls the output of each RGBA channel in an image"; }
		}

		public override string Name
		{
			get { return "Channel Mixer"; }
		}

		public override string Key
		{
			get { return "mixer"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "in")
			};
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.Float, "R", 1.0f) { MaxValue = 2.0f },
				new NodeProperty(NodePropertyType.Float, "G", 1.0f) { MaxValue = 2.0f },
				new NodeProperty(NodePropertyType.Float, "B", 1.0f) { MaxValue = 2.0f }
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}

			float sourceR = args.GetProperty<float>(0);
			float sourceG = args.GetProperty<float>(1);
			float sourceB = args.GetProperty<float>(2);

			DirectBitmap copy = new DirectBitmap(bmp);
			for (int x = 0; x < copy.Width; x++)
			{
				for (int y = 0; y < copy.Height; y++)
				{
					Color c1 = copy.GetPixel(x, y);
					float r = ShaderFunctions.Saturate(c1.R / 255.0f * sourceR);
					float g = ShaderFunctions.Saturate(c1.G / 255.0f * sourceG);
					float b = ShaderFunctions.Saturate(c1.B / 255.0f * sourceB);

					Color result = Color.FromArgb(c1.A, (int)(r * 255), (int)(g * 255), (int)(b * 255));
					copy.SetPixel(x, y, result);
				}
			}
			return Task.FromResult(new PipelineResult(copy));
		}
	}
}
