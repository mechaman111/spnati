using System;
using System.Drawing;

namespace SPNATI_Character_Editor
{
	public static class MathUtil
	{
		private static Random _random = new Random();

		public static float GetRandom()
		{
			return _random.Next(0, 100) / 100.0f;
		}

		public static float Lerp(float a, float b, float t)
		{
			return (b - a) * t + a;
		}

		public static Color Lerp(Color a, Color b, float t)
		{
			float r1 = a.R / 255.0f;
			float g1 = a.G / 255.0f;
			float b1 = a.B / 255.0f;

			float r2 = b.R / 255.0f;
			float g2 = b.G / 255.0f;
			float b2 = b.B / 255.0f;

			float rf = Lerp(r1, r2, t);
			float gf = Lerp(g1, g2, t);
			float bf = Lerp(b1, b2, t);

			byte rb = (byte)(rf * 255.0f);
			byte gb = (byte)(gf * 255.0f);
			byte bb = (byte)(bf * 255.0f);
			return Color.FromArgb(rb, gb, bb);
		}
	}
}
