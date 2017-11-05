using System.Drawing;

namespace KisekaeImporter
{
	public class KisekaeColor
	{
		public string ColorIndex { get; private set; }
		public Color Color { get; private set; }

		public KisekaeColor()
		{
		}

		public KisekaeColor(Color color)
		{
			SetColor(color);
		}

		public KisekaeColor(string data)
		{
			if (data.Length == 6)
			{
				Color = ColorTranslator.FromHtml("#" + data);
			}
			else
			{
				ColorIndex = data;
			}
		}

		public void SetColorIndex(string index)
		{
			ColorIndex = index;
		}

		public void SetColor(Color color)
		{
			ColorIndex = null;
			Color = color;
		}

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(ColorIndex))
			{
				return ColorIndex;
			}
			else
			{
				return ColorTranslator.ToHtml(Color.FromArgb(Color.ToArgb())).Substring(1);
			}
		}
	}
}
