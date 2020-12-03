using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ApplyMaskNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Apply Mask"; }
		}

		public override string Key { get { return "mask"; } set { } }

		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Keeps only parts of the image that are on a mask"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "image"),
				new PortDefinition(PortType.Bitmap, "mask"),
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
				new NodeProperty(NodePropertyType.Boolean, "Use Alpha", false),
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
			bool useAlpha = args.GetProperty<bool>(0);
			if (img1 == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			if (img2 == null)
			{
				return Task.FromResult(new PipelineResult(new DirectBitmap(img1)));
			}
			ImageWrapMode wrapMode = args.GetProperty<ImageWrapMode>(1);
			Point offset = args.GetProperty<Point>(2);
			int w1 = img1.Width;
			int h1 = img1.Height;
			int w2 = img2.Width;
			int h2 = img2.Height;

			DirectBitmap output = new DirectBitmap(w1, h1);

			for (int x = 0; x < w1; x++)
			{
				for (int y = 0; y < h1; y++)
				{
					Color c1 = img1.GetPixel(x, y);
					float a = c1.A / 255.0f;
					int x2 = ShaderFunctions.OffsetAndWrap(x, w2, wrapMode, offset.X);
					int y2 = ShaderFunctions.OffsetAndWrap(y, h2, wrapMode, offset.Y);

					Color c2 = Color.Empty;
					if (x2 >= 0 && x2 < w2 && y2 >= 0 && y2 < h2)
					{
						c2 = img2.GetPixel(x2, y2);
					}
					float maskA = useAlpha ? c2.A / 255.0f : Math.Max(c2.R, Math.Max(c2.G, c2.B)) / 255.0f;
					a *= maskA;

					output.SetPixel(x, y, Color.FromArgb((int)(255 * a), c1));
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}
	}
}
