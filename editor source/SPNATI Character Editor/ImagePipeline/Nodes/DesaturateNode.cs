using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using SPNATI_Character_Editor;

namespace ImagePipeline
{
	/// <summary>
	/// Desaturates an image
	/// </summary>
	public class DesaturateNode : NodeDefinition
	{
		public override string Name => "Desaturate";

		public override string Key
		{
			get { return "grayscale"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "src"),
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
				new NodeProperty(NodePropertyType.Float, "amount")
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
			float amount = args.GetProperty<float>(0);
			if (amount == 0)
			{
				return Task.FromResult(new PipelineResult(new DirectBitmap(img)));
			}
			DirectBitmap output = new DirectBitmap(img.Width, img.Height);

			for (int x = 0; x < img.Width; x++)
			{
				for (int y = 0; y < img.Height; y++)
				{
					Color color = img.GetPixel(x, y);
					float r = color.R / 255.0f;
					float g = color.G / 255.0f;
					float b = color.B / 255.0f;
					//float bw = (g * 0.59f + r * 0.3f + b * 0.11f);
					float bw = (Math.Min(r, Math.Min(g, b)) + Math.Max(r, Math.Max(g, b))) * 0.5f;

					float fr = r * (1 - amount) + bw * amount;
					float fg = g * (1 - amount) + bw * amount;
					float fb = b * (1 - amount) + bw * amount;

					int vr = (int)(fr * 255);
					int vg = (int)(fg * 255);
					int vb = (int)(fb * 255);

					output.SetPixel(x, y, Color.FromArgb(color.A, vr, vg, vb));
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}

	}
}
