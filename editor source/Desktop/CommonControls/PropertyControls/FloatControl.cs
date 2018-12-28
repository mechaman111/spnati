using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class FloatControl : PropertyEditControl
	{
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

		private void RemoveHandlers()
		{
			valValue.ValueChanged -= valValue_ValueChanged;
		}

		private void AddHandlers()
		{
			valValue.ValueChanged += valValue_ValueChanged;
		}

		protected override void OnBoundData()
		{
			float value = 0;
			if (DataType == typeof(string))
			{
				string valueStr = GetValue()?.ToString();
				if (string.IsNullOrEmpty(valueStr))
				{
					valueStr = _defaultValue;
				}
				if (!float.TryParse(valueStr, out value))
				{
					valValue.Text = "";
				}
				else
				{
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)value));
				}
			}
			else
			{
				value = (float)GetValue();
				valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, (decimal)value));
			}

			AddHandlers();
		}

		protected override void OnRebindData()
		{
			RemoveHandlers();
			OnBoundData();
		}

		public override void Clear()
		{
			valValue.Text = "";
			Save();
		}

		public override void Save()
		{
			float value = (float)valValue.Value;
			if (DataType == typeof(string))
			{
				if (valValue.Text == "")
				{
					SetValue(null);
				}
				else
				{
					SetValue(value.ToString());
				}
			}
			else
			{
				SetValue((float)valValue.Value);
			}
		}

		private void valValue_ValueChanged(object sender, EventArgs e)
		{
			Save();
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
