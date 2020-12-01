using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class InvertColorsNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Invert Colors"; }
		}

		public override string Key
		{
			get { return "invert"; }
			set { }
		}

		public override string Group
		{
			get { return "Artistic"; }
		}

		public override string Description
		{
			get { return "Inverts the colors in an image per-channel"; }
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
				new NodeProperty(NodePropertyType.Boolean, "Red"),
				new NodeProperty(NodePropertyType.Boolean, "Green"),
				new NodeProperty(NodePropertyType.Boolean, "Blue"),
				new NodeProperty(NodePropertyType.Boolean, "Alpha"),
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}

			bool invertR = args.GetProperty<bool>(0);
			bool invertG = args.GetProperty<bool>(1);
			bool invertB = args.GetProperty<bool>(2);
			bool invertA = args.GetProperty<bool>(3);
			
			DirectBitmap copy = new DirectBitmap(bmp);
			for (int x = 0; x < copy.Width; x++)
			{
				for (int y = 0; y < copy.Height; y++)
				{
					Color c1 = copy.GetPixel(x, y);
					int r = c1.R;
					int g = c1.G;
					int b = c1.B;
					int a = c1.A;
					if (invertA)
					{
						a = 255 - a;
					}
					if (invertR)
					{
						r = 255 - r;
					}
					if (invertG)
					{
						g = 255 - g;
					}
					if (invertB)
					{
						b = 255 - b;
					}

					Color result = Color.FromArgb(a, r, g, b);
					copy.SetPixel(x, y, result);
				}
			}
			return Task.FromResult(new PipelineResult(copy));
		}
	}
}
