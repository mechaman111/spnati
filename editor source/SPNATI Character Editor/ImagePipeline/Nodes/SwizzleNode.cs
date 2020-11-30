using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Creates a new color that flips channels around
	/// </summary>
	public class SwizzleNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Swizzle"; }
		}

		public override string Key
		{
			get { return "swizzle"; }
			set { }
		}

		public override string Group
		{
			get { return "Channel"; }
		}

		public override string Description
		{
			get { return "Swaps RGBA channels around"; }
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
				new NodeProperty(NodePropertyType.Integer, "Red", ColorChannel.R, typeof(ColorChannel)),
				new NodeProperty(NodePropertyType.Integer, "Green", ColorChannel.G, typeof(ColorChannel)),
				new NodeProperty(NodePropertyType.Integer, "Blue", ColorChannel.B, typeof(ColorChannel)),
				new NodeProperty(NodePropertyType.Integer, "Alpha", ColorChannel.A, typeof(ColorChannel)),
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}

			ColorChannel rSwizzle = args.GetProperty<ColorChannel>(0);
			ColorChannel gSwizzle = args.GetProperty<ColorChannel>(1);
			ColorChannel bSwizzle = args.GetProperty<ColorChannel>(2);
			ColorChannel aSwizzle = args.GetProperty<ColorChannel>(3);

			DirectBitmap result = new DirectBitmap(bmp.Width, bmp.Height);

			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					Color inC = bmp.GetPixel(x, y);
					int r = GetChannel(inC, rSwizzle);
					int g = GetChannel(inC, gSwizzle);
					int b = GetChannel(inC, bSwizzle);
					int a = GetChannel(inC, aSwizzle);

					result.SetPixel(x, y, Color.FromArgb(a, r, g, b));
				}
			}

			return Task.FromResult(new PipelineResult(result));
		}

		private int GetChannel(Color color, ColorChannel channel)
		{
			switch (channel)
			{
				case ColorChannel.G:
					return color.G;
				case ColorChannel.B:
					return color.B;
				case ColorChannel.A:
					return color.A;
				default:
					return color.R;
			}
		}
	}
}

