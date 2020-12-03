using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ExtractNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Cookie Cutter"; }
		}

		public override string Key { get { return "extract"; } set { } }

		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Cuts part of an image out based on which parts are identical to a comparison image"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "source"),
				new PortDefinition(PortType.Bitmap, "compare"),
				new PortDefinition(PortType.Float, "range"),
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
				new NodeProperty(NodePropertyType.Integer, "Keep", ExtractOperation.Different, typeof(ExtractOperation)),
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
			IFloatNodeInput rangeReader = args.GetInput<IFloatNodeInput>(2) ?? new ConstantFloat(0);
			if (img1 == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			ExtractOperation mode = args.GetProperty<ExtractOperation>(0);
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
						c2 = img2?.GetPixel(x2, y2) ?? Color.FromArgb(0, 0, 0, 0);
					}
					float range = rangeReader.Get(x2, y2);
					float dist = ShaderFunctions.Distance(c1, c2, true);
					bool keep = true;
					switch (mode)
					{
						case ExtractOperation.Same:
							if (dist > range)
							{
								keep = false;
							}
							break;
						case ExtractOperation.Different:
							if (dist <= range)
							{
								keep = false;
							}
							break;
					}
					if (keep)
					{
						output.SetPixel(x, y, c2);
					}
					else
					{
						output.SetPixel(x, y, Color.Empty);
					}
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}
	}

	public enum ExtractOperation
	{
		Same,
		Different
	}
}
