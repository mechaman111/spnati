using System.Drawing;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	//[SubAttribute("font-family", "Text font")]
	public partial class StyleFontFamilyControl : SubAttributeControl
	{
		public StyleFontFamilyControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string font = Attribute.Value;
			if (string.IsNullOrEmpty(font))
			{
				font = "Choose a font";
			}
			lblFont.Text = font;
		}

		private void lblFont_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			float size = 16;
			FontStyle style = FontStyle.Regular;
			StyleAttribute sizeAttrib = GetAttribute("font-size");
			if (sizeAttrib != null)
			{
				size = ToPts(sizeAttrib.Value);
			}
			StyleAttribute weightAttrib = GetAttribute("font-weight");
			if (weightAttrib != null)
			{
				if (weightAttrib.Value == "700")
				{
					style |= FontStyle.Bold;
				}
			}
			StyleAttribute styleAttrib = GetAttribute("font-style");
			if (styleAttrib != null)
			{
				if (styleAttrib.Value == "italic" || styleAttrib.Value == "oblique")
				{
					style |= FontStyle.Italic;
				}
			}

			fontDialog1.Font = new Font(lblFont.Text, size, style);

			if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				lblFont.Text = fontDialog1.Font.Name;
				Attribute.Value = lblFont.Text;

				if (sizeAttrib != null)
				{
					sizeAttrib.Value = fontDialog1.Font.SizeInPoints + "pt";
				}
				if (weightAttrib != null)
				{
					if (fontDialog1.Font.Style.HasFlag(FontStyle.Bold))
					{
						weightAttrib.Value = "700";
					}
					else
					{
						weightAttrib.Value = "400";
					}
				}
				if (styleAttrib != null)
				{
					if (fontDialog1.Font.Style.HasFlag(FontStyle.Italic))
					{
						styleAttrib.Value = "italic";
					}
					else
					{
						styleAttrib.Value = "normal";
					}
				}
			}
		}
	}
}
