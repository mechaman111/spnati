using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class BlendNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Blends two images together"; }
		}

		public override string Name
		{
			get { return "Blend"; }
		}

		public override string Key
		{
			get { return "blend"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
					new PortDefinition(PortType.Bitmap, "base"),
					new PortDefinition(PortType.Bitmap, "blend"),
					new PortDefinition(PortType.Float, "amount"),
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
					new NodeProperty(NodePropertyType.Integer, "Mode", BlendMode.Normal, typeof(BlendMode)),
					new NodeProperty(NodePropertyType.Integer, "Clamp", ImageWrapMode.Clamp, typeof(ImageWrapMode)),
					new NodeProperty(NodePropertyType.Point, "Offset", new Point(0, 0)),
				};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			if (!args.HasInput())
			{
				return Task.FromResult(new PipelineResult(null));
			}
			DirectBitmap img1 = args.GetInput<DirectBitmap>(0);
			DirectBitmap img2 = args.GetInput<DirectBitmap>(1);
			IFloatNodeInput amountReader = args.GetInput<IFloatNodeInput>(2) ?? new ConstantFloat(0);
			if (img1 == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			BlendMode mode = args.GetProperty<BlendMode>(0);
			ImageWrapMode wrapMode = args.GetProperty<ImageWrapMode>(1);
			Point offset = args.GetProperty<Point>(2);
			int w1 = img1.Width;
			int h1 = img1.Height;
			int w2 = img2?.Width ?? 1;
			int h2 = img2?.Height ?? 1;

			DirectBitmap output = new DirectBitmap(w1, h1);

			for (int x = 0; x < w1; x++)
			{
				for (int y = 0; y < h1; y++)
				{
					Color c1 = img1.GetPixel(x, y);
					int x2 = ShaderFunctions.OffsetAndWrap(x, w2, wrapMode, offset.X);
					int y2 = ShaderFunctions.OffsetAndWrap(y, h2, wrapMode, offset.Y);

					Color c2 = Color.Empty;
					if (x2 >= 0 && x2 < w2 && y2 >= 0 && y2 < h2)
					{
						c2 = img2?.GetPixel(x2, y2) ?? Color.Empty;
					}
					BlendMode blendMode = mode;
					float blendAmount = amountReader.Get(x2, y2);
					if (mode == BlendMode.Extract)
					{
						if (c1 == c2)
						{
							output.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0));
							continue;
						}
						blendMode = BlendMode.Normal;
					}

					int a = MixAlpha(c1.A, c2.A, blendAmount, blendMode);
					int r = Mix(c1.R, c2.R, blendAmount, blendMode, c1.A, c2.A);
					int g = Mix(c1.G, c2.G, blendAmount, blendMode, c1.A, c2.A);
					int b = Mix(c1.B, c2.B, blendAmount, blendMode, c1.A, c2.A);
					output.SetPixel(x, y, Color.FromArgb(a, r, g, b));
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}

		private int MixAlpha(byte a1, byte a2, float m, BlendMode mode)
		{
			float src = a1 / 255.0f;
			float blend = a2 / 255.0f;
			float output = blend;

			switch (mode)
			{
				case BlendMode.Overlay:
					output = blend + src * (1 - blend);
					output = ShaderFunctions.Saturate(src * (1 - m) + output * m);
					break;
				case BlendMode.Multiply:
					output = src;
					break;
				case BlendMode.Additive:
					output = src + blend;
					break;

				default:
					return Mix(a1, a2, m, BlendMode.Normal, a1, a2);
			}

			return (int)(ShaderFunctions.Saturate(output) * 255);
		}

		private int Mix(byte c1, byte c2, float m, BlendMode mode, byte a1, byte a2)
		{
			float src = (float)c1 / 255;
			float blend = (float)c2 / 255;
			float srcAlpha = a1 / 255.0f;
			float blendAlpha = a2 / 255.0f;

			switch (mode)
			{
				case BlendMode.Multiply:
					blend = src * blend;
					blend = src * (1 - blendAlpha) + blend * blendAlpha;
					break;
				case BlendMode.Difference:
					blend = Math.Abs(src * srcAlpha - blend * blendAlpha);
					break;
				case BlendMode.Screen:
					blend = 1 - (1 - blend * blendAlpha) * (1 - src * srcAlpha);
					break;
				case BlendMode.Additive:
					blend = src * srcAlpha + blend * blendAlpha;
					break;
				case BlendMode.Lighten:
					blend = Math.Max(src * srcAlpha, blend * blendAlpha);
					break;
				case BlendMode.Darken:
					blend = Math.Min(src * srcAlpha, blend * blendAlpha);
					break;
				case BlendMode.Overlay:
					blendAlpha *= m;
					float d = blendAlpha + srcAlpha * (1 - blendAlpha);
					if (blendAlpha == 0)
					{
						blend = src;
					}
					else
					{
						blend = ShaderFunctions.Saturate((blend * blendAlpha + src * srcAlpha * (1 - blendAlpha)) / d);
					}
					return (int)(blend * 255);
			}

			//interpolation
			double a = ShaderFunctions.Saturate(src * (1 - m) + blend * m);
			return (int)(a * 255);
		}
	}

	public enum BlendMode
	{
		Normal = 0,
		Multiply = 1,
		Screen = 2,
		Additive = 3,
		Difference = 4,
		Lighten = 5,
		Darken = 6,
		Extract = 7,
		Overlay = 8,
	}

	public enum ImageWrapMode
	{
		Clamp = 0,
		Repeat = 1,
		Mirror = 2,
	}
}
