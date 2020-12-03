using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ContrastNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Adjusts an image's brightness and contrast"; }
		}

		public override string Name
		{
			get { return "Brightness/Contrast"; }
		}

		public override string Key
		{
			get { return "contrast"; }
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
				new NodeProperty(NodePropertyType.Float, "Brightness", 0.0f) { MinValue = -1.0f, MaxValue = 1.0f },
				new NodeProperty(NodePropertyType.Float, "Contrast", 0.0f) { MinValue = -1.0f, MaxValue = 1.0f }
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}

			float brightness = (float)Math.Max(-1.0f, Math.Min(1.0f, args.GetProperty<float>(0))) * 0.5f;
			float contrast = (float)Math.Max(-1.0f, Math.Min(1.0f, args.GetProperty<float>(1)));
			float factor = (1.0156863f * (contrast + 1)) / (1.0156863f - contrast);

			DirectBitmap output = new DirectBitmap(bmp.Width, bmp.Height);

			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					Color color = bmp.GetPixel(x, y);
					float[] c = color.ToFloatArray();
					for (int i = 0; i < 3; i++)
					{
						c[i] = ShaderFunctions.Saturate(factor * (c[i] + brightness - 0.5f) + 0.5f);
					}
					output.SetPixel(x, y, ShaderFunctions.ToColor(c));
				}
			}

			return Task.FromResult(new PipelineResult(output));
		}
	}
}
