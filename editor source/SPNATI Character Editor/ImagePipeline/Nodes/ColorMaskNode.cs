using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Creates a mask from values in the input equal to the mask color.
	/// </summary>
	public class ColorMaskNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Creates a mask from an image and color"; }
		}

		public override string Name
		{
			get { return "Color Mask"; }
		}

		public override string Key
		{
			get { return "colormask"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "in"),
				new PortDefinition(PortType.Color, "mask color"),
				new PortDefinition(PortType.Float, "range"),
				new PortDefinition(PortType.Float, "fuzziness"),
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
			return null;
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			Color maskColor = args.GetInput<Color>(1);
			float maskR = maskColor.R / 255.0f;
			float maskG = maskColor.G / 255.0f;
			float maskB = maskColor.B / 255.0f;
			IFloatNodeInput rangeReader = args.GetInput<IFloatNodeInput>(2);
			IFloatNodeInput fuzzinessReader = args.GetInput<IFloatNodeInput>(3);

			DirectBitmap result = new DirectBitmap(bmp.Width, bmp.Height);

			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					float range = rangeReader.Get(x, y);
					float fuzziness = fuzzinessReader.Get(x, y);
					if (fuzziness == 0)
					{
						fuzziness = 0.00001f;
					}

					Color inC = bmp.GetPixel(x, y);
					if (inC.A == 0)
					{
						continue;
					}
					float r = inC.R / 255.0f;
					float g = inC.G / 255.0f;
					float b = inC.B / 255.0f;

					float dist = (float)Math.Sqrt((r - maskR) * (r - maskR) + (g - maskG) * (g - maskG) + (b - maskB) * (b - maskB));
					float value = Math.Max(0, Math.Min(1, 1 - (dist - range) / fuzziness));
					int c = (int)(value * 255);
					result.SetPixel(x, y, Color.FromArgb(inC.A, c, c, c));
				}
			}

			return Task.FromResult(new PipelineResult(result));
		}
	}
}

