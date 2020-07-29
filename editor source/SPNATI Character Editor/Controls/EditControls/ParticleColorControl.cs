using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.EpilogueEditing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class ParticleColorControl : PropertyEditControl
	{
		public ParticleColorControl()
		{
			InitializeComponent();

			txtValue.Tag = cmdColor;
			txtValue2.Tag = cmdColor2;
			cmdColor.Tag = txtValue;
			cmdColor2.Tag = txtValue2;
		}

		protected override void OnBoundData()
		{
			object val = GetValue();
			string value = val?.ToString();
			if (string.IsNullOrEmpty(value))
			{
				txtValue.Text = "";
				txtValue2.Text = "";
				cmdColor.BackColor = Color.Empty;
				cmdColor2.BackColor = Color.Empty;
			}
			else if (val is RandomColor)
			{
				RandomColor rc = val as RandomColor;
				Color c1 = rc.Min;
				txtValue.Text = ToHexValue(c1);
				cmdColor.BackColor = c1;

				Color c2 = rc.Max;
				txtValue2.Text = ToHexValue(c2);
				cmdColor2.BackColor = c2;
			}
			else
			{
				string[] pieces = value.Split(':');
				string color1 = pieces[0];
				try
				{
					Color color = ColorTranslator.FromHtml(color1);
					txtValue.Text = color1.Substring(1);
					cmdColor.BackColor = color;
				}
				catch
				{
					txtValue.Text = "";
					cmdColor.BackColor = Color.Empty;
				}

				if (pieces.Length > 1)
				{
					string color2 = pieces[1];
					try
					{
						Color color = ColorTranslator.FromHtml(color2);
						txtValue2.Text = color2.Substring(1);
						cmdColor2.BackColor = color;
					}
					catch
					{
						txtValue2.Text = "";
						cmdColor2.BackColor = Color.Empty;
					}
				}
			}
		}

		protected override void AddHandlers()
		{
			txtValue.TextChanged += TxtValue_TextChanged;
			txtValue2.TextChanged += TxtValue_TextChanged;
		}

		protected override void RemoveHandlers()
		{
			txtValue.TextChanged -= TxtValue_TextChanged;
			txtValue2.TextChanged -= TxtValue_TextChanged;
		}

		private void cmdColor_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			colorPicker.Color = btn.BackColor;
			if (colorPicker.ShowDialog() == DialogResult.OK)
			{
				btn.BackColor = colorPicker.Color;
				RemoveHandlers();
				(btn.Tag as TextField).Text = ToHexValue(btn.BackColor);
				AddHandlers();
				Save();
			}
		}

		private void TxtValue_TextChanged(object sender, EventArgs e)
		{
			TextField txt = sender as TextField;
			if (string.IsNullOrEmpty(txt.Text))
			{
				Save();
				return;
			}

			string hex = "#" + txt.Text;
			try
			{
				Color color = ColorTranslator.FromHtml(hex);
				Button btn = txt.Tag as Button;
				btn.BackColor = color;
				Save();
			}
			catch { }
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			cmdColor.BackColor = Color.Transparent;
			cmdColor2.BackColor = Color.Transparent;
			txtValue.Text = "";
			txtValue2.Text = "";
			AddHandlers();
			Save();
		}

		protected override void OnSave()
		{
			string color1 = txtValue.Text;
			string color2 = txtValue2.Text;
			if (string.IsNullOrEmpty(color1))
			{
				SetValue(null);
			}
			else
			{
				if (PropertyType == typeof(RandomColor))
				{
					Color c1 = cmdColor.BackColor;
					Color c2 = string.IsNullOrEmpty(color2) ? c1 : cmdColor2.BackColor;
					SetValue(new RandomColor(c1, c2));
				}
				else
				{
					string value = "#" + ToHexValue(cmdColor.BackColor);
					if (!string.IsNullOrEmpty(color2))
					{
						string value2 = "#" + ToHexValue(cmdColor2.BackColor);
						value += ":" + value2;
					}
					SetValue(value);
				}
			}
		}

		private static string ToHexValue(Color color)
		{
			return color.R.ToString("X2") +
					color.G.ToString("X2") +
					color.B.ToString("X2");
		}
	}

	public class ParticleColorAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ParticleColorControl); }
		}
	}
}
