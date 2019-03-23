using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.CommonControls;
using Desktop;
using System.Globalization;

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
			string value = GetValue()?.ToString();
			if (string.IsNullOrEmpty(value))
			{
				valFrom.Text = "";
				valTo.Text = "";
			}
			else
			{
				string[] pieces = value.Split(':');
				float min;
				if (float.TryParse(pieces[0], NumberStyles.Float, CultureInfo.InvariantCulture, out min))
				{
					min = (float)Math.Round(min, valFrom.DecimalPlaces);
					valFrom.Value = (decimal)min;
				}
				else
				{
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
						valTo.Text = "";
					}
				}
				else
				{
					valTo.Text = "";
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

		public override void Clear()
		{
			RemoveHandlers();
			valFrom.Value = 0;
			valTo.Value = 0;
			valFrom.Text = "";
			valTo.Text = "";
			Save();
			AddHandlers();
		}

		public override void Save()
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
