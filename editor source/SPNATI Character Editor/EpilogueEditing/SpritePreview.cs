using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SpritePreview
	{
		public string Id;
		public PosePreview Pose;
		public Image Image;
		public int AddIndex;
		public float X;
		public float Y;
		public int Width;
		public int Height;
		public int Z;
		public float ScaleX;
		public float ScaleY;
		public float Rotation;
		public float Alpha;
		public float PivotX;
		public float PivotY;

		public SpritePreview() { }

		public SpritePreview(PosePreview pose, Sprite sprite, int index)
		{
			Pose = pose;
			Id = sprite.Id;
			Z = sprite.Z;
			AddIndex = index;
			float.TryParse(sprite.X, NumberStyles.Number, CultureInfo.InvariantCulture, out X);
			float.TryParse(sprite.Y, NumberStyles.Number, CultureInfo.InvariantCulture, out Y);
			int.TryParse(sprite.Width, out Width);
			int.TryParse(sprite.Height, out Height);
			pose.Images.TryGetValue(sprite.Src, out Image);
			if (Width == 0)
			{
				Width = Image.Width;
			}
			if (Height == 0)
			{
				Height = Image.Height;
			}
			if (!float.TryParse(sprite.ScaleX, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleX))
			{
				ScaleX = 1;
			}
			if (!float.TryParse(sprite.ScaleY, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleY))
			{
				ScaleY = 1;
			}
			PivotX = SceneObject.ParsePivot(sprite.PivotX ?? "center", Width);
			PivotY = SceneObject.ParsePivot(sprite.PivotY ?? "center", Height);
			float.TryParse(sprite.Rotation, NumberStyles.Number, CultureInfo.InvariantCulture, out Rotation);
			float.TryParse(sprite.Opacity ?? "100", NumberStyles.Number, CultureInfo.InvariantCulture, out Alpha);
		}

		public override string ToString()
		{
			return Id;
		}

		public void Draw(Graphics g, int displayWidth, int displayHeight)
		{
			if (Image != null && Alpha > 0)
			{
				Rectangle bounds = ToScreenRegion(displayWidth, displayHeight);	

				float offsetX = bounds.X + PivotX / Width * bounds.Width;
				float offsetY = bounds.Y + PivotY / Height * bounds.Height;
				if (float.IsNaN(offsetX))
				{
					offsetX = 0;
				}
				if (float.IsNaN(offsetY))
				{
					offsetY = 0;
				}

				g.TranslateTransform(offsetX, offsetY);
				g.RotateTransform(Rotation);
				g.TranslateTransform(-offsetX, -offsetY);

				if (Alpha < 100)
				{
					float[][] matrixItems = new float[][] {
							new float[] { 1, 0, 0, 0, 0 },
							new float[] { 0, 1, 0, 0, 0 },
							new float[] { 0, 0, 1, 0, 0 },
							new float[] { 0, 0, 0, Alpha / 100.0f, 0 },
							new float[] { 0, 0, 0, 0, 1 }
						};
					ColorMatrix cm = new ColorMatrix(matrixItems);
					ImageAttributes ia = new ImageAttributes();
					ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(Image, bounds, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
				}
				else
				{
					g.DrawImage(Image, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height), new Rectangle(0, 0, Image.Width, Image.Height), GraphicsUnit.Pixel);
				}

				g.ResetTransform();
			}
		}

		public Rectangle ToScreenRegion(int displayWidth, int displayHeight)
		{
			//get unscaled bounds in screen space
			float x = ScaleToDisplay(X, displayHeight);
			float y = ScaleToDisplay(Y, displayHeight);
			float width = ScaleToDisplay(Width, displayHeight);
			float height = ScaleToDisplay(Height, displayHeight);
			x = (int)(x + displayWidth * 0.5f - width * 0.5f);

			//translate pivot to origin
			float pivotX = x + PivotX / Width * width;
			float pivotY = y + PivotY / Height * height;
			x -= pivotX;
			y -= pivotY;

			//apply scaling
			float right = x + width;
			x *= ScaleX;
			right *= ScaleX;
			width = right - x;

			float bottom = y + height;
			y *= ScaleY;
			bottom *= ScaleY;
			height = bottom - y;

			//translate back
			x += pivotX;
			y += pivotY;

			return new Rectangle((int)x, (int)y, (int)width, (int)height);
		}

		public int ScaleToDisplay(float value, int canvasHeight)
		{
			return (int)Math.Floor(value * canvasHeight / Pose.BaseHeight);
		}
	}
}
