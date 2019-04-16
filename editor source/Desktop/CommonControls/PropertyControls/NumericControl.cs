using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class NumericControl : PropertyEditControl
	{
		private bool _cleared;

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
			if (PropertyType == typeof(int?))
			{
				int? value = (int?)GetValue();
				if (value.HasValue)
				{
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, value.Value));
				}
				else
				{
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, 0));
					valValue.Text = "";
					_cleared = true;
				}
			}
			else
			{
				int value = (int)GetValue();
				valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, value));
			}
		}

		protected override void AddHandlers()
		{
			valValue.ValueChanged += valValue_ValueChanged;
			valValue.TextChanged += valValue_TextChanged;
		}

		protected override void RemoveHandlers()
		{
			valValue.ValueChanged -= valValue_ValueChanged;
			valValue.TextChanged -= valValue_TextChanged;
		}

		public override void Clear()
		{
			valValue.Value = 0;
			valValue.Text = "";
			_cleared = true;
			Save();
		}

		public override void Save()
		{
			if (PropertyType == typeof(int?))
			{
				if (valValue.Text == "")
				{
					SetValue(null);
				}
				else
				{
					int? value = (int)valValue.Value;
					SetValue(value);
				}
			}
			else
			{
				SetValue((int)valValue.Value);
			}
		}

		private void valValue_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void valValue_TextChanged(object sender, EventArgs e)
		{
			string text = valValue.Text;
			if (!string.IsNullOrEmpty(text) && _cleared)
			{
				_cleared = false;
				RemoveHandlers();
				Save();
				AddHandlers();
			}
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
