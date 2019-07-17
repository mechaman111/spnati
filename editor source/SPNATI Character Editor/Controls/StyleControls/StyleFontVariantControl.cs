namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("font-variant", "Font variant like small caps")]
	public partial class StyleFontVariantControl : SubAttributeControl
	{
		public StyleFontVariantControl()
		{
			InitializeComponent();
			cboVariant.Items.AddRange(new string[] {
				"normal",
				"small-caps",
				});
		}

		protected override void OnBoundData()
		{
			string value = Attribute.Value?.ToLower();
			if (value == "small-caps")
			{
				cboVariant.SelectedItem = "small-caps";
			}
			else
			{
				cboVariant.SelectedItem = "normal";
			}
		}

		protected override void AddHandlers()
		{
			cboVariant.SelectedIndexChanged += CboVariant_SelectedIndexChanged;
		}
		protected override void RemoveHandlers()
		{
			cboVariant.SelectedIndexChanged -= CboVariant_SelectedIndexChanged;
		}

		private void CboVariant_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			string item = cboVariant.SelectedItem?.ToString() ?? "normal";
			Attribute.Value = item;
		}
	}
}
