using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.EpilogueEditing;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class ParticleFloatControl : PropertyEditControl
	{
		public ParticleFloatControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			ParticleFloatAttribute p = parameters as ParticleFloatAttribute;
			valTo.Minimum = valFrom.Minimum = (decimal)p.Minimum;
			valTo.Maximum = valFrom.Maximum = (decimal)p.Maximum;
			valTo.Increment = valFrom.Increment = (decimal)p.Increment;
			valTo.DecimalPlaces = valFrom.DecimalPlaces = p.DecimalPlaces;
		}

		protected override void RemoveHandlers()
		{
			valFrom.ValueChanged -= val_ValueChanged;
			valFrom.TextChanged -= val_TextChanged;
			valTo.ValueChanged -= val_ValueChanged;
			valTo.TextChanged -= val_TextChanged;
		}

		protected override void AddHandlers()
		{
			valFrom.ValueChanged += val_ValueChanged;
			valFrom.TextChanged += val_TextChanged;
			valTo.ValueChanged += val_ValueChanged;
			valTo.TextChanged += val_TextChanged;
		}

		protected override void OnBoundData()
		{
			object val = GetValue();
			if (val == null)
			{
				valFrom.Value = Math.Max(valFrom.Minimum, Math.Min(valFrom.Maximum, 0));
				valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, 0));
				valFrom.Text = "";
				valTo.Text = "";
			}
			else
			{
				if (val is RandomParameter)
				{
					RandomParameter rp = val as RandomParameter;
					valFrom.Value = (decimal)Math.Max((float)valFrom.Minimum, Math.Min((float)valFrom.Maximum, rp.Min));
					valTo.Value = (decimal)Math.Max((float)valTo.Minimum, Math.Min((float)valTo.Maximum, rp.Max));
				}
				else
				{
					string value = GetValue()?.ToString();
					string[] pieces = value.Split(':');
					float min;
					if (float.TryParse(pieces[0], NumberStyles.Float, CultureInfo.InvariantCulture, out min))
					{
						min = (float)Math.Round(min, valFrom.DecimalPlaces);
						valFrom.Value = (decimal)min;
					}
					else
					{
						valFrom.Value = Math.Max(valFrom.Minimum, Math.Min(valFrom.Maximum, 0));
						valFrom.Text = "";
					}
					if (pieces.Length > 1)
					{
						float max;
						if (float.TryParse(pieces[1], NumberStyles.Float, CultureInfo.InvariantCulture, out max))
						{
							max = (float)Math.Round(max, valFrom.DecimalPlaces);
							valTo.Value = (decimal)max;
						}
						else
						{
							valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, 0));
							valTo.Text = "";
						}
					}
					else
					{
						valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, 0));
						valTo.Text = "";
					}
				}
			}
		}

		private void val_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void val_TextChanged(object sender, EventArgs e)
		{
			NumericUpDown ctl = sender as NumericUpDown;
			if (ctl.Text == "" || GetValue() == null)
			{
				Save();
			}
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			valFrom.Value = Math.Max(0, valFrom.Minimum);
			valTo.Value = Math.Max(0, valFrom.Minimum);
			valFrom.Text = "";
			valTo.Text = "";
			Save();
			AddHandlers();
		}

		protected override void OnSave()
		{
			if (valFrom.Text == "")
			{
				SetValue(null);
			}
			else
			{
				float min = (float)valFrom.Value;
				float max;
				if (valTo.Text == "")
				{
					max = min;
				}
				else
				{
					max = (float)valTo.Value;
				}
				if (PropertyType == typeof(RandomParameter))
				{
					if (min >= max)
					{
						min = max;
					}
					SetValue(new RandomParameter(min, max));
				}
				else
				{
					if (min >= max)
					{
						SetValue(min.ToString(CultureInfo.InvariantCulture));
					}
					else
					{
						SetValue($"{min.ToString(CultureInfo.InvariantCulture)}:{max.ToString(CultureInfo.InvariantCulture)}");
					}
				}
			}
		}
	}

	public class ParticleFloatAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ParticleFloatControl); }
		}

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
	}
}
