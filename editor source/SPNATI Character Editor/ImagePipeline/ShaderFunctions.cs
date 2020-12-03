using System;
using System.Drawing;

namespace ImagePipeline
{
	/// <summary>
	/// Common "shader" functions
	/// </summary>
	public static class ShaderFunctions
	{
		public static Color Saturate(Color color)
		{
			return Color.FromArgb(
				Saturate(color.A),
				Saturate(color.R),
				Saturate(color.G),
				Saturate(color.B));
		}

		public static float Saturate(float value)
		{
			return Math.Max(0, Math.Min(1.0f, value));
		}

		public static int Saturate(int value)
		{
			return Math.Max(0, Math.Min(255, value));
		}

		public static float Lerp(float a, float b, float t)
		{
			t = Math.Max(0, Math.Min(1, t));
			return (1 - t) * a + t * b;
		}

		public static float[] Lerp(float[] a, float[] b, float t)
		{
			float[] result = new float[Math.Min(a.Length, b.Length)];
			for (int i = 0; i < a.Length && i < b.Length; i++)
			{
				result[i] = Lerp(a[i], b[i], t);
			}
			return result;
		}

		public static float Distance(Color c1, Color c2, bool includeAlpha)
		{
			float[] a1 = c1.ToFloatArray();
			float[] a2 = c2.ToFloatArray();
			float r = a2[0] - a1[0];
			float g = a2[1] - a1[1];
			float b = a2[2] - a1[2];
			float a = includeAlpha ? a2[3] - a1[3] : 0;
			return (float)Math.Sqrt(r * r + g * g + b * b + a * a);
		}

		/// <summary>
		/// Converts a color into an [R,G,B,A] array where each channel goes from [0-1]
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static float[] ToFloatArray(this Color c)
		{
			float r = c.R / 255.0f;
			float g = c.G / 255.0f;
			float b = c.B / 255.0f;
			float a = c.A / 255.0f;
			return new float[] { r, g, b, a };
		}

		/// <summary>
		/// Converts an [R,G,B,A] array into a Color
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static Color ToColor(float[] array)
		{
			int r = Saturate((int)(array[0] * 255));
			int g = Saturate((int)(array[1] * 255));
			int b = Saturate((int)(array[2] * 255));
			int a = Saturate((int)(array[3] * 255));
			return Color.FromArgb(a, r, g, b);
		}

		/// <summary>
		/// Clamps a value using a certain mode
		/// </summary>
		/// <param name="value"></param>
		/// <param name="mode"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int OffsetAndWrap(int value, int max, ImageWrapMode mode, int offset)
		{
			int v = value - offset;
			switch (mode)
			{
				case ImageWrapMode.Repeat:
					v = (v + max) % max;
					break;
				case ImageWrapMode.Mirror:
					int pattern = ((v + max) / max) % 2;
					v = (v + max) % max;
					if (pattern == 0)
					{
						v = (max - 1 - v);
					}
					break;
			}
			return v;
		}

		/// <summary>
		/// Desaturates a color
		/// </summary>
		/// <param name="color"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public static Color Desaturate(Color color, float amount)
		{
			amount = Saturate(amount);
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
			return Color.FromArgb(color.A, vr, vg, vb);
		}
	}
}
