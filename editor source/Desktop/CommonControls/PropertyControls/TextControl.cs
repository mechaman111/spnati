using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class TextControl : PropertyEditControl
	{
		public TextControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			txtValue.Text = GetValue()?.ToString();
		}

		public override void Clear()
		{
			txtValue.Text = "";
			Save();
		}

		public override void Save()
		{
			SetValue(txtValue.Text);
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			Save();
		}
	}

	public class TextAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(TextControl); }
		}
	}
}
