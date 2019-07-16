using System;
using System.Drawing;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("text-shadow", "Text shadow effect")]
	public partial class StyleTextShadowControl : SubAttributeControl
	{
		public StyleTextShadowControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string[] pieces = GetPieces();
			if (pieces.Length > 0)
			{
				valHoriz.Value = Math.Min(valHoriz.Maximum, Math.Max(valHoriz.Minimum, (int)PxToInt(pieces[0])));
			}
			if (pieces.Length > 1)
			{
				valVert.Value = Math.Min(valVert.Maximum, Math.Max(valVert.Minimum, (int)PxToInt(pieces[1])));
			}
			if (pieces.Length > 2)
			{
				valBlur.Value = Math.Min(valVert.Maximum, Math.Max(valBlur.Minimum, (int)PxToInt(pieces[2])));
			}
			if (pieces.Length > 3)
			{
				try
				{
					Color color = ColorTranslator.FromHtml(pieces[3]);
					fldColor.Color = color;
				}
				catch
				{
					fldColor.Color = Color.Black;
				}
			}
		}

		protected override void AddHandlers()
		{
			valHoriz.ValueChanged += FieldChanged;
			valVert.ValueChanged += FieldChanged;
			valBlur.ValueChanged += FieldChanged;
			fldColor.ColorChanged += FieldChanged;
		}

		protected override void RemoveHandlers()
		{
			valHoriz.ValueChanged -= FieldChanged;
			valVert.ValueChanged -= FieldChanged;
			valBlur.ValueChanged -= FieldChanged;
			fldColor.ColorChanged -= FieldChanged;
		}

		private void FieldChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			string horiz = IntToPx((int)valHoriz.Value);
			string vert = IntToPx((int)valVert.Value);
			string blur = IntToPx((int)valBlur.Value);
			string color = ColorTranslator.ToHtml(fldColor.Color);
			Attribute.Value = $"{horiz} {vert} {blur} {color}";
		}
	}
}
