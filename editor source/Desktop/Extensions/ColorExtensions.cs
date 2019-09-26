using System.Drawing;

namespace Desktop
{
	public static class ColorExtensions
	{
		public static Color Invert(this Color color)
		{
			return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
		}

		public static double GetLuminance(this Color color)
		{
			return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
		}
	}
}
