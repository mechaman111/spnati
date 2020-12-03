using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Replaces colors that fall within a certain range
	/// </summary>
	public class ReplaceColorNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Replaces colors that fall within a certain range"; }
		}

		public override string Name
		{
			get { return "Replace Color"; }
		}

		public override string Key
		{
			get { return "replace"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "in"),
				new PortDefinition(PortType.Color, "from"),
				new PortDefinition(PortType.Color, "to"),
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
			Color fromColor = args.GetInput<Color>(1);
			float[] from = fromColor.ToFloatArray();
			Color toColor = args.GetInput<Color>(2);
			float[] to = toColor.ToFloatArray();

			IFloatNodeInput rangeReader = args.GetInput<IFloatNodeInput>(3);
			IFloatNodeInput fuzzinessReader = args.GetInput<IFloatNodeInput>(4);

			DirectBitmap result = new DirectBitmap(bmp.Width, bmp.Height);

			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					float range = rangeReader.Get(x, y);
					float fuzziness = fuzzinessReader.Get(x, y);
					if (fuzziness <= 0)
					{
						fuzziness = 0.00001f;
					}

					Color inC = bmp.GetPixel(x, y);
					float[] c = inC.ToFloatArray();

					float distance = ShaderFunctions.Distance(fromColor, inC, false);
					float t = ShaderFunctions.Saturate((distance - range) / fuzziness);
					float[] outValues = ShaderFunctions.Lerp(to, c, t);
					Color final = ShaderFunctions.ToColor(outValues);
					result.SetPixel(x, y, Color.FromArgb(inC.A, final.R, final.G, final.B));
				}
			}

			return Task.FromResult(new PipelineResult(result));
		}
	}
}

