using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ThresholdNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Converts an image into a binary mask"; }
		}

		public override string Name
		{
			get { return "Threshold"; }
		}

		public override string Key
		{
			get { return "threshold"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "src"),
				new PortDefinition(PortType.Float, "threshold"),
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
				new NodeProperty(NodePropertyType.Boolean, "Use Alpha", false)
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			if (!args.HasInput())
			{
				return Task.FromResult(new PipelineResult(null));
			}
			DirectBitmap img = args.GetInput<DirectBitmap>(0);
			if (img == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			DirectBitmap output = new DirectBitmap(img.Width, img.Height);
			IFloatNodeInput floatReader = args.GetInput<IFloatNodeInput>(1) ?? new ConstantFloat(0);
			bool useAlpha = args.GetProperty<bool>(0);

			for (int x = 0; x < img.Width; x++)
			{
				for (int y = 0; y < img.Height; y++)
				{
					Color color = ShaderFunctions.Desaturate(img.GetPixel(x, y), 1.0f);
					float threshold = ShaderFunctions.Saturate(floatReader.Get(x, y));
					float gray = color.R / 255.0f;
					if (useAlpha)
					{
						if (gray < threshold)
						{
							color = Color.FromArgb(color.A, 0, 0, 0);
						}
						else
						{
							color = Color.FromArgb(0, 0, 0, 0);
						}
					}
					else
					{
						if (gray < threshold)
						{
							color = Color.FromArgb(color.A, 0, 0, 0);
						}
						else
						{
							color = Color.FromArgb(color.A, 255, 255, 255);
						}
					}

					output.SetPixel(x, y, color);
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}

	}
}
