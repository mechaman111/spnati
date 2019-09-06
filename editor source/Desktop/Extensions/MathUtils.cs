using System;
using System.Drawing;

namespace Desktop
{
	public static class MathUtils
	{
		public static float Lerp(float a, float b, float t)
		{
			if (float.IsNaN(t))
			{
				return a;
			}
			return (b - a) * t + a;
		}

		/// <summary>
		/// Gets a nice spacing between ticks for on an graph axis
		/// </summary>
		/// <param name="min">Minimum value in axis</param>
		/// <param name="max">Maximum value in axis</param>
		/// <param name="maxTicks">Maximum number of ticks to show</param>
		/// <returns></returns>
		public static int GetTickSpacing(int min, int max, int maxTicks, out int minTick, out int maxTick)
		{
			double range = GetNiceNumber(max - min, false);
			int spacing = (int)Math.Round(GetNiceNumber(range / (maxTicks - 1), true));
			minTick = Math.Max(0, (int)(Math.Floor((float)min / spacing) * spacing));
			maxTick = Math.Min(100000000, (int)(Math.Ceiling((float)max / spacing) * spacing));
			return spacing;
		}

		private static double GetNiceNumber(double range, bool round)
		{
			double exponent;
			double fraction;
			double niceFraction;

			exponent = Math.Floor(Math.Log10(range));
			fraction = range / Math.Pow(10, exponent);
			if (round)
			{
				if (fraction < 1.5)
				{
					niceFraction = 1;
				}
				else if (fraction < 3)
				{
					niceFraction = 2;
				}
				else if (fraction < 7)
				{
					niceFraction = 5;
				}
				else
				{
					niceFraction = 10;
				}
			}
			else
			{
				if (fraction <= 1)
				{
					niceFraction = 1;
				}
				else if (fraction <= 2)
				{
					niceFraction = 2;
				}
				else if (fraction <= 5)
				{
					niceFraction = 5;
				}
				else
				{
					niceFraction = 10;
				}
			}
			return niceFraction * Math.Pow(10, exponent);
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
