using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class ColorControl : PropertyEditControl
	{
		private bool _cleared;

		public ColorControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string value = GetValue()?.ToString();
			if (string.IsNullOrEmpty(value))
			{
				_cleared = true;
			}
			else
			{
				try
				{
					Color color = ColorTranslator.FromHtml(value);
					txtValue.Text = value.Substring(1);
					cmdColor.BackColor = color;
				}
				catch
				{
					_cleared = true;
				}
			}

			AddHandlers();
		}

		private void AddHandlers()
		{
			txtValue.TextChanged += TxtValue_TextChanged;
		}

		private void RemoveHandlers()
		{
			txtValue.TextChanged -= TxtValue_TextChanged;
		}

		private void CmdColor_Click(object sender, EventArgs e)
		{
			colorPicker.Color = cmdColor.BackColor;
			if (colorPicker.ShowDialog() == DialogResult.OK)
			{
				_cleared = false;
				cmdColor.BackColor = colorPicker.Color;
				RemoveHandlers();
				txtValue.Text = ToHexValue(cmdColor.BackColor);
				AddHandlers();
				Save();
			}
		}

		private void TxtValue_TextChanged(object sender, EventArgs e)
		{
			string hex = "#" + txtValue.Text;
			try
			{
				Color color = ColorTranslator.FromHtml(hex);
				cmdColor.BackColor = color;
				Save();
			}
			catch
			{
				txtValue.Text = "";
			}
		}


		public override void Clear()
		{
			RemoveHandlers();
			cmdColor.BackColor = Color.Transparent;
			txtValue.Text = "";
			_cleared = true;
			AddHandlers();
			Save();
		}

		public override void Save()
		{
			if (_cleared)
			{
				SetValue(null);
			}
			else
			{
				string value = "#" + ToHexValue(cmdColor.BackColor);
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

	public class ColorAttribute : EditControlAttribute
	{
		public override Type EditControlType { get { return typeof(ColorControl); } }
	}
}
