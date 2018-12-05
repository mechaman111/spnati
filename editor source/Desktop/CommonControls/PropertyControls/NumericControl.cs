using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class NumericControl : PropertyEditControl
	{
		public NumericControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			NumericAttribute p = parameters as NumericAttribute;
			valValue.Minimum = p.Minimum;
			valValue.Maximum = p.Maximum;
		}

		protected override void OnBoundData()
		{
			valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (int)GetValue()));
		}

		public override void Clear()
		{
			valValue.Value = 0;
			Save();
		}

		public override void Save()
		{
			SetValue((int)valValue.Value);
		}

		private void valValue_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class NumericAttribute : EditControlAttribute
	{
		/// <summary>
		/// Minimum allowed value
		/// </summary>
		public int Minimum = 0;
		/// <summary>
		/// Maximum allowed value
		/// </summary>
		public int Maximum = 100;

		public override Type EditControlType
		{
			get { return typeof(NumericControl); }
		}
	}
}
