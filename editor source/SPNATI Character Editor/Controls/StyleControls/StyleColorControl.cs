using System.Drawing;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("color", "Text color")]
	[SubAttribute("background-color", "Background color")]
	public partial class StyleColorControl : SubAttributeControl
	{
		public StyleColorControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			try
			{
				fldColor.Color = ColorTranslator.FromHtml(Attribute.Value);
			}
			catch
			{
				fldColor.Color = Color.Black;
			}
		}

		protected override void AddHandlers()
		{
			fldColor.ColorChanged += FldColor_ColorChanged;
		}

		protected override void RemoveHandlers()
		{
			fldColor.ColorChanged -= FldColor_ColorChanged;
		}

		private void FldColor_ColorChanged(object sender, System.EventArgs e)
		{
			string color = ColorTranslator.ToHtml(fldColor.Color);
			Attribute.Value = color;
		}
	}
}
