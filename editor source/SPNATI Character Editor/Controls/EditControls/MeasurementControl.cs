using System;
using System.Text.RegularExpressions;
using Desktop;
using Desktop.CommonControls;
using System.Drawing;
using System.IO;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EditControls
{
	public partial class MeasurementControl : PropertyEditControl
	{
		private Directive _directive;

		public MeasurementControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			MeasurementAttribute attrib = parameters as MeasurementAttribute;
			valValue.Minimum = attrib.Minimum;
			valValue.Maximum = attrib.Maximum;
		}

		private void RemoveHandlers()
		{
			radPct.CheckedChanged -= ValueChanged;
			radPx.CheckedChanged -= ValueChanged;
			valValue.TextChanged -= ValueChanged;
		}

		private void AddHandlers()
		{
			radPct.CheckedChanged += ValueChanged;
			radPx.CheckedChanged += ValueChanged;
			valValue.TextChanged += ValueChanged;
			chkCentered.CheckedChanged += ChkCentered_CheckedChanged;
		}

		private void ChkCentered_CheckedChanged(object sender, EventArgs e)
		{
			valValue.Enabled = !chkCentered.Checked;
			Save();
		}

		protected override void OnBoundData()
		{
			_directive = Data as Directive;

			bool isText = (_directive != null && _directive.DirectiveType == "text");
			int value;
			string text = GetValue()?.ToString();
			Regex regex = new Regex(@"^(-?\d+)(px|%)?$");
			if (!string.IsNullOrEmpty(text))
			{
				Match match = regex.Match(text);
				if (match.Success)
				{
					int.TryParse(match.Groups[1].Value, out value);
					valValue.Value = Math.Max(valValue.Minimum, Math.Min(valValue.Maximum, value));
				}
			}
			else
			{
				valValue.Text = "";
			}

			if (isText)
			{
				radPct.Checked = true;
				radPct.Visible = false;
				radPx.Visible = false;
				lblPct.Left = valValue.Left + valValue.Width;
				lblPct.Visible = true;

				if (Property == "X")
				{
					chkCentered.Left = lblPct.Left + lblPct.Width + 5;
					chkCentered.Visible = true;

					if (text == "centered")
					{
						chkCentered.Checked = true;
						valValue.Enabled = false;
					}
					else
					{
						chkCentered.Checked = false;
					}
				}
			}
			else
			{
				if (text != null && text.EndsWith("%"))
				{
					radPct.Checked = true;
				}
				else
				{
					radPx.Checked = true;
				}
			}

			AddHandlers();
		}

		protected override void OnBindingUpdated(string property)
		{
			if (property == "Background")
			{
				EpilogueContext context = Context as EpilogueContext;
				ISkin character = context?.Character;
				string file = GetBindingValue(property)?.ToString();
				if (!string.IsNullOrEmpty(file) && character != null)
				{
					file = Path.Combine(character.GetDirectory(), file);
					if (File.Exists(file))
					{
						using (Bitmap bmp = new Bitmap(file))
						{
							if (Property == "Width")
							{
								RemoveHandlers();
								valValue.Value = bmp.Width;
								radPx.Checked = true;
								chkCentered.Checked = false;
								AddHandlers();
								Save();
							}
							else if (Property == "Height")
							{
								RemoveHandlers();
								valValue.Value = bmp.Height;
								radPx.Checked = true;
								chkCentered.Checked = false;
								AddHandlers();
								Save();
							}
						}
					}
				}
			}
		}

		protected override void OnRebindData()
		{
			RemoveHandlers();
			OnBoundData();
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		public override void Clear()
		{
			RemoveHandlers();
			radPx.Checked = true;
			valValue.Text = "";
			Save();
			AddHandlers();
		}

		public override void Save()
		{
			if (chkCentered.Checked)
			{
				SetValue("centered");
				return;
			}

			if (string.IsNullOrEmpty(valValue.Text))
			{
				SetValue(null);
				return;
			}
			bool pctUnits = radPct.Checked;
			int value = (int)valValue.Value;
			string v = value.ToString();
			if (pctUnits)
			{
				v += "%";
			}
			SetValue(v);
		}
	}

	public class MeasurementAttribute : EditControlAttribute
	{
		public override Type EditControlType { get { return typeof(MeasurementControl); } }

		public int Minimum = -100000;
		public int Maximum = 100000;
	}
}
