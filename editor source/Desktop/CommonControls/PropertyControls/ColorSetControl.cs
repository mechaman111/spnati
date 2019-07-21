using Desktop.Skinning;
using System;
using System.Drawing;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class ColorSetControl : PropertyEditControl
	{
		public ColorSetControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			ColorSet set = GetValue() as ColorSet;
			clrNormal.Color = set.Normal;
			clrHover.Color = set.Hover;
			clrPressed.Color = set.Pressed;
			clrSelected.Color = set.Selected;
			clrDisabled.Color = set.Disabled;
			clrDisSelected.Color = set.DisabledSelected;
			clrText.Color = set.ForeColor;
			clrDisabledText.Color = set.DisabledForeColor;
			clrBorder.Color = set.Border;
			clrBorderHover.Color = set.BorderHover;
			clrBorderDisabled.Color = set.BorderDisabled;
			clrBorderSelected.Color = set.BorderSelected;
		}

		protected override void AddHandlers()
		{
			clrNormal.ColorChanged += Normal_ColorChanged;
			clrHover.ColorChanged += ColorChanged;
			clrPressed.ColorChanged += ColorChanged;
			clrSelected.ColorChanged += ColorChanged;
			clrDisabled.ColorChanged += ColorChanged;
			clrDisSelected.ColorChanged += ColorChanged;
			clrText.ColorChanged += ColorChanged;
			clrDisabledText.ColorChanged += ColorChanged;
			clrBorder.ColorChanged += ColorChanged;
			clrBorderHover.ColorChanged += ColorChanged;
			clrBorderDisabled.ColorChanged += ColorChanged;
			clrBorderSelected.ColorChanged += ColorChanged;
		}

		protected override void RemoveHandlers()
		{
			clrNormal.ColorChanged -= Normal_ColorChanged;
			clrHover.ColorChanged -= ColorChanged;
			clrPressed.ColorChanged -= ColorChanged;
			clrSelected.ColorChanged -= ColorChanged;
			clrDisabled.ColorChanged -= ColorChanged;
			clrDisSelected.ColorChanged -= ColorChanged;
			clrText.ColorChanged -= ColorChanged;
			clrDisabledText.ColorChanged -= ColorChanged;
			clrBorder.ColorChanged -= ColorChanged;
			clrBorderHover.ColorChanged -= ColorChanged;
			clrBorderDisabled.ColorChanged -= ColorChanged;
			clrBorderSelected.ColorChanged -= ColorChanged;
		}

		private Color Darken(Color inColor, float factor)
		{
			float r = inColor.R / 255.0f;
			float g = inColor.G / 255.0f;
			float b = inColor.B / 255.0f;

			r *= 1 - factor;
			g *= 1 - factor;
			b *= 1 - factor;

			Color outColor = Color.FromArgb(inColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			return outColor;
		}

		private Color Lighten(Color inColor, float factor)
		{
			float r = inColor.R / 255.0f;
			float g = inColor.G / 255.0f;
			float b = inColor.B / 255.0f;

			r = r + (1 - r) * factor;
			g = g + (1 - g) * factor;
			b = b + (1 - b) * factor;

			Color outColor = Color.FromArgb(inColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			return outColor;
		}

		private Color Grayscale(Color inColor)
		{
			float r = inColor.R / 255.0f;
			float g = inColor.G / 255.0f;
			float b = inColor.B / 255.0f;

			r = g = b = 0.2126f * r + 0.7152f * g + 0.0722f * b;

			Color outColor = Color.FromArgb(inColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			return outColor;
		}

		private Color GetForeColor(Color backColor)
		{
			float r = backColor.R / 255.0f;
			float g = backColor.G / 255.0f;
			float b = backColor.B / 255.0f;

			float intensity = 0.299f * r + 0.587f * g + 0.114f * b;
			if (intensity * 255f > 186)
			{
				return Color.Black;
			}
			return Color.White;
		}

		private void Normal_ColorChanged(object sender, EventArgs e)
		{
			RemoveHandlers();

			//auto-update all the other colors based on this one
			Color normal = clrNormal.Color;

			clrHover.Color = Lighten(normal, 0.25f);
			clrPressed.Color = Darken(normal, 0.25f);
			clrSelected.Color = Darken(normal, 0.05f);
			clrDisabled.Color = Grayscale(normal);
			clrDisSelected.Color = Grayscale(clrSelected.Color);
			clrText.Color = GetForeColor(normal);
			clrDisabledText.Color = clrText.Color == Color.White ? Color.FromArgb(180, 180, 180) : Color.FromArgb(70, 70, 70);

			clrBorder.Color = clrPressed.Color;
			clrBorderHover.Color = normal;
			clrBorderSelected.Color = Darken(clrBorder.Color, 0.05f);
			clrBorderDisabled.Color = Grayscale(clrBorder.Color);

			Save();
			AddHandlers();
		}

		private void ColorChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			ColorSet set = GetValue() as ColorSet;
			set.Normal = clrNormal.Color;
			set.Hover = clrHover.Color;
			set.Pressed = clrPressed.Color;
			set.Selected = clrSelected.Color;
			set.Disabled = clrDisabled.Color;
			set.DisabledSelected = clrDisSelected.Color;
			set.ForeColor = clrText.Color;
			set.DisabledForeColor = clrDisabledText.Color;
			set.Border = clrBorder.Color;
			set.BorderDisabled = clrBorderDisabled.Color;
			set.BorderHover = clrBorderHover.Color;
			set.BorderSelected = clrBorderSelected.Color;
			NotifyPropertyChanged();
			set.ClearCache();
		}
	}

	public class ColorSetAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ColorSetControl); }
		}
	}
}
