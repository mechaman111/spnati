namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("font-style", "Font style (italics)")]
	public partial class StyleFontStyleControl : SubAttributeControl
	{
		public StyleFontStyleControl()
		{
			InitializeComponent();
		}

		private bool _bound;

		protected override void OnBoundData()
		{
			if (!_bound)
			{
				_bound = true;
				Attribute.PropertyChanged += Attribute_PropertyChanged;
			}

			if (Attribute.Value == "italic" || Attribute.Value == "oblique")
			{
				//yeah, these aren't actually the same, but who cares
				radItalic.Checked = true;
			}
			else
			{
				radNormal.Checked = true;
			}
		}

		protected override void RemoveHandlers()
		{
			radNormal.CheckedChanged -= RadNormal_CheckedChanged;
			radItalic.CheckedChanged -= RadNormal_CheckedChanged;
		}
		protected override void AddHandlers()
		{
			radNormal.CheckedChanged += RadNormal_CheckedChanged;
			radItalic.CheckedChanged += RadNormal_CheckedChanged;
		}

		private void RadNormal_CheckedChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		private void Attribute_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (IsUpdating) { return; }
			RemoveHandlers();
			OnBoundData();
			AddHandlers();
		}

		protected override void OnSave()
		{
			if (radItalic.Checked)
			{
				Attribute.Value = "italic";
			}
			else
			{
				Attribute.Value = "normal";
			}
		}
	}
}
