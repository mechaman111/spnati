using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using SPNATI_Character_Editor;

namespace ImagePipeline
{
	public class BlendNode : NodeDefinition
	{
		public override string Name => "Blend";

		public override string Key
		{
			get { return "blend"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
					new PortDefinition(PortType.Bitmap, "base"),
					new PortDefinition(PortType.Bitmap, "blend")
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
					new NodeProperty(NodePropertyType.Float, "Strength", 0.5f),
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
			BlendMode mode = args.GetProperty<BlendMode>(0);
			if (img1 == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			float amount = args.GetProperty<float>(1);
			if (amount == 0 || img2 == null)
			{
				return Task.FromResult(new PipelineResult(new DirectBitmap(img1.Bitmap)));
			}
			else if (amount == 1 && mode == BlendMode.Normal)
			{
				return Task.FromResult(new PipelineResult(new DirectBitmap(img2.Bitmap)));
			}
			ImageWrapMode wrapMode = args.GetProperty<ImageWrapMode>(2);
			Point offset = args.GetProperty<Point>(3);
			int w1 = img1.Width;
			int h1 = img1.Height;
			int w2 = img2.Width;
			int h2 = img2.Height;

			if (mode == BlendMode.Overlay)
			{
				return Task.FromResult(new PipelineResult(CombineImages(img1, img2, amount, wrapMode, offset.X, offset.Y)));
			}

			DirectBitmap output = new DirectBitmap(w1, h1);

			for (int x = 0; x < w1; x++)
			{
				for (int y = 0; y < h1; y++)
				{
					Color c1 = img1.GetPixel(x, y);
					int x2 = x - offset.X;
					int y2 = y - offset.Y;
					switch (wrapMode)
					{
						case ImageWrapMode.Repeat:
							x2 = (x2 + img2.Width) % img2.Width;
							y2 = (y2 + img2.Height) % img2.Height;
							break;
						case ImageWrapMode.Mirror:
							int xPattern = ((x2 + img2.Width) / img2.Width) % 2;
							int yPattern = ((y2 + img2.Height) / img2.Height) % 2;
							x2 = (x2 + img2.Width) % img2.Width;
							y2 = (y2 + img2.Height) % img2.Height;
							if (xPattern == 0)
							{
								x2 = (img2.Width - x2);
							}
							if (yPattern == 0)
							{
								y2 = (img2.Height - y2);
							}
							break;
					}
					if (x2 < 0 || x2 >= w2 || y2 < 0 || y2 >= h2)
					{
						output.SetPixel(x, y, c1);
						continue;
					}
					Color c2 = img2.GetPixel(x2, y2);
					BlendMode blendMode = mode;
					float blendAmount = amount;
					if (mode == BlendMode.Extract)
					{
						if (c1 == c2)
						{
							output.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0));
							continue;
						}
						blendMode = BlendMode.Normal;
					}
					if (mode == BlendMode.Overlay)
					{
						Color final = Combine(c1, c2, blendAmount);
						output.SetPixel(x, y, final);
						continue;
					}

					int r = Mix(c1.R, c2.R, blendAmount, blendMode);
					int g = Mix(c1.G, c2.G, blendAmount, blendMode);
					int b = Mix(c1.B, c2.B, blendAmount, blendMode);
					int a = Mix(c1.A, c2.A, blendAmount, blendMode);
					output.SetPixel(x, y, Color.FromArgb(a, r, g, b));
				}
			}
			return Task.FromResult(new PipelineResult(output));
		}

		private static DirectBitmap CombineImages(DirectBitmap img1, DirectBitmap img2, float amount, ImageWrapMode wrapMode, int xOffset, int yOffset)
		{
			DirectBitmap output = new DirectBitmap(img1.Width, img1.Height);
			using (Graphics g = Graphics.FromImage(output.Bitmap))
			{
				g.DrawImage(img1.Bitmap, 0, 0);
				float[][] matrixItems = new float[][] {
						  new float[] { 1, 0, 0, 0, 0 },
						  new float[] { 0, 1, 0, 0, 0 },
						  new float[] { 0, 0, 1, 0, 0 },
						  new float[] { 0, 0, 0, amount, 0 },
						  new float[] { 0, 0, 0, 0, 1 }
						 };
				ColorMatrix cm = new ColorMatrix(matrixItems);
				ImageAttributes ia = new ImageAttributes();
				ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

				using (TextureBrush brush = new TextureBrush(img2.Bitmap, new Rectangle(0, 0, img2.Width, img2.Height), ia))
				{
					switch (wrapMode)
					{
						case ImageWrapMode.Repeat:
							brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
							break;
						case ImageWrapMode.Mirror:
							brush.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;
							break;
					}

					brush.TranslateTransform(xOffset, yOffset);
					g.FillRectangle(brush, 0, 0, output.Width, output.Height);
				}
				return output;
			}
		}

		private int Mix(byte c1, byte c2, float m, BlendMode mode)
		{
			float src = (float)c1 / 255;
			float blend = (float)c2 / 255;

			switch (mode)
			{
				case BlendMode.Multiply:
					blend = src * blend;
					break;
				case BlendMode.Difference:
					blend = Math.Abs(src - blend);
					break;
				case BlendMode.Screen:
					blend = 1 - (1 - blend) * (1 - src);
					break;
				case BlendMode.Additive:
					blend = src + blend;
					break;
				case BlendMode.Lighten:
					blend = Math.Max(src, blend);
					break;
				case BlendMode.Darken:
					blend = Math.Min(src, blend);
					break;
			}

			//interpolation
			double a = Math.Max(0, Math.Min(1, src * (1 - m) + blend * m));
			return (int)(a * 255);
		}

		private Color Combine(Color c1, Color c2, float amount)
		{
			if (c2.A == 0)
			{
				return c1;
			}
			else if (c2.A == 1)
			{
				return c2;
			}
			float a = c2.A / 255.0f;
			byte r = (byte)(255 * (c2.R / 255.0f * a));
			byte g = (byte)(255 * (c2.G / 255.0f * a));
			byte b = (byte)(255 * (c2.B / 255.0f * a));
			int mr = Mix(c1.R, r, 1, BlendMode.Normal);
			int mg = Mix(c1.G, g, 1, BlendMode.Normal);
			int mb = Mix(c1.B, b, 1, BlendMode.Normal);
			int ma = Mix(c1.A, c2.A, 1, BlendMode.Normal);

			return Color.FromArgb(ma, mr, mg, mb);
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
		Overlay = 8
	}

	public enum ImageWrapMode
	{
		Clamp = 0,
		Repeat = 1,
		Mirror = 2,
	}
}
