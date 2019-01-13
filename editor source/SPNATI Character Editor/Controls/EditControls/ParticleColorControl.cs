using Desktop;
using Desktop.CommonControls;
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
			string value = GetValue()?.ToString();
			if (string.IsNullOrEmpty(value))
			{
				txtValue.Text = "";
				txtValue2.Text = "";
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
					}
				}
			}
		}

		private void AddHandlers()
		{
			txtValue.TextChanged += TxtValue_TextChanged;
			txtValue2.TextChanged += TxtValue_TextChanged;
		}

		private void RemoveHandlers()
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
				(btn.Tag as TextBox).Text = ToHexValue(btn.BackColor);
				AddHandlers();
				Save();
			}
		}

		private void TxtValue_TextChanged(object sender, EventArgs e)
		{
			TextBox txt = sender as TextBox;
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

		public override void Clear()
		{
			RemoveHandlers();
			cmdColor.BackColor = Color.Transparent;
			cmdColor2.BackColor = Color.Transparent;
			txtValue.Text = "";
			txtValue2.Text = "";
			AddHandlers();
			Save();
		}

		public override void Save()
		{
			string color1 = txtValue.Text;
			string color2 = txtValue2.Text;
			if (string.IsNullOrEmpty(color1))
			{
				SetValue(null);
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
