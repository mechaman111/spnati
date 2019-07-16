using System;
using System.Globalization;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class FloatControl : PropertyEditControl
	{
		private bool _cleared;
		private string _defaultValue;

		public FloatControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			FloatAttribute p = parameters as FloatAttribute;
			valValue.Minimum = (decimal)p.Minimum;
			valValue.Maximum = (decimal)p.Maximum;
			valValue.Increment = (decimal)p.Increment;
			valValue.DecimalPlaces = p.DecimalPlaces;
			_defaultValue = p.DefaultValue;
		}

		protected override void RemoveHandlers()
		{
			valValue.ValueChanged -= valValue_ValueChanged;
			valValue.TextChanged -= valValue_TextChanged;
		}

		protected override void AddHandlers()
		{
			valValue.ValueChanged += valValue_ValueChanged;
			valValue.TextChanged += valValue_TextChanged;
		}

		protected override void OnBoundData()
		{
			SetPreview();
			SetField();
		}

		private void SetPreview()
		{
			object data = GetPreviewValue();
			if (data != null)
			{
				if (DataType == typeof(string))
				{
					valValue.PlaceholderText = data?.ToString();
				}
				if (DataType == typeof(float?))
				{
					float? value = (float?)data;
					if (value.HasValue)
					{
						valValue.PlaceholderText = GetPreviewString(value.Value);
					}
					else
					{
						valValue.PlaceholderText = "";
					}
				}
				else
				{
					float value = (float)data;
					valValue.PlaceholderText = GetPreviewString(value);
				}
			}
			else
			{
				valValue.PlaceholderText = "";
			}
		}

		private string GetPreviewString(float value)
		{
			string format = "0";
			if (valValue.DecimalPlaces > 0)
			{
				format = $"0.{"".PadLeft(valValue.DecimalPlaces, '0')}";
			}
			return value.ToString(format, CultureInfo.InvariantCulture);
		}

		private bool SetField()
		{
			_cleared = false;
			object data = GetValue();
			if (DataType == typeof(string))
			{
				string valueStr = data?.ToString();
				if (string.IsNullOrEmpty(valueStr))
				{
					valueStr = _defaultValue;
				}
				float value;
				if (!float.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
				{
					_cleared = true;
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, 0));
					valValue.Text = "";
					return false;
				}
				else
				{
					if (double.IsInfinity(value))
					{
						value = 0;
					}
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)value));
					return true;
				}
			}
			else if (DataType == typeof(float?))
			{
				float? value = (float?)data;
				if (value.HasValue)
				{
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)value));
					return true;
				}
				else
				{
					float floatValue;
					if (!string.IsNullOrEmpty(_defaultValue) && float.TryParse(_defaultValue, NumberStyles.Float, CultureInfo.InvariantCulture, out floatValue))
					{
						valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)floatValue));
						return false;
					}
					else
					{
						_cleared = true;
						valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, 0));
						valValue.Text = "";
						return false;
					}
				}
			}
			else
			{
				float value = (float)data;
				valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)value));
				return true;
			}
		}

		protected override void OnClear()
		{
			_cleared = true;
			valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, 0));
			valValue.Text = "";
			Save();
		}

		protected override void OnSave()
		{
			float value = (float)valValue.Value;
			SaveValue(value, valValue.Text);
		}

		private void SaveValue(float value, string text)
		{
			if (DataType == typeof(string))
			{
				if (text == "")
				{
					SetValue(null);
				}
				else
				{
					SetValue(value.ToString(CultureInfo.InvariantCulture));
				}
			}
			else if (DataType == typeof(float?))
			{
				if (text == "")
				{
					SetValue(null);
				}
				else
				{
					SetValue((float?)value);
				}
			}
			else
			{
				SetValue(value);
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

				//Working around Save() because accessing valValue.Value will cause it to format the decimal places, screwing up the cursor position
				//since we just barely started typing a number in
				float value;
				float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
				SaveValue(value, text);
			}
		}
	}

	public class FloatAttribute : EditControlAttribute
	{
		/// <summary>
		/// Default value if value was null
		/// </summary>
		public string DefaultValue = "";
		/// <summary>
		/// Minimum allowed value
		/// </summary>
		public float Minimum = 0;
		/// <summary>
		/// Maximum allowed value
		/// </summary>
		public float Maximum = 100;
		public float Increment = 1;
		/// <summary>
		/// Number of decimal places allowed
		/// </summary>
		public int DecimalPlaces = 2;

		public override Type EditControlType
		{
			get { return typeof(FloatControl); }
		}
	}
}
