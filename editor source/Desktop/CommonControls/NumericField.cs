using Desktop.Skinning;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class NumericField : UserControl, ISupportInitialize, ISkinControl
	{
		public decimal Minimum
		{
			get { return valField.Minimum; }
			set { valField.Minimum = value; }
		}

		public decimal Maximum
		{
			get { return valField.Maximum; }
			set { valField.Maximum = value; }
		}

		public decimal Increment
		{
			get { return valField.Increment; }
			set { valField.Increment = value; }
		}

		public decimal Value
		{
			get { return valField.Value; }
			set
			{
				valField.Value = value;
				lblPlaceholder.Visible = false;
				valField.Text = value.ToString(CultureInfo.CurrentCulture);
			}
		}

		public int DecimalPlaces
		{
			get { return valField.DecimalPlaces; }
			set { valField.DecimalPlaces = value; }
		}

		public string PlaceholderText
		{
			get { return lblPlaceholder.Text; }
			set
			{
				lblPlaceholder.Text = value;
				lblPlaceholder.Visible = IsPlaceholderVisible;
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			}
		}

		public new event EventHandler TextChanged;
		public event EventHandler ValueChanged;

		public NumericField()
		{
			InitializeComponent();
			valField.TextChanged += ValField_TextChanged;
			valField.ValueChanged += ValField_ValueChanged;
		}

		private void ValField_ValueChanged(object sender, EventArgs e)
		{
			ValueChanged?.Invoke(this, e);
		}

		public override string Text
		{
			get { return valField.Text; }
			set
			{
				valField.Text = value;
				lblPlaceholder.Visible = IsPlaceholderVisible;
			}
		}

		private bool IsPlaceholderVisible
		{
			get { return string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(lblPlaceholder.Text); }
		}

		private void ValField_TextChanged(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = IsPlaceholderVisible;
			TextChanged?.Invoke(this, e);
		}

		public void BeginInit()
		{
			valField.BeginInit();
		}

		public void EndInit()
		{
			valField.EndInit();
		}

		private void lblPlaceholder_Click(object sender, EventArgs e)
		{
			valField.Focus();
		}

		private void valField_Enter(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = false;
		}

		private void valField_Leave(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = IsPlaceholderVisible;
		}

		public void OnUpdateSkin(Skin skin)
		{
			lblPlaceholder.BackColor = skin.FieldBackColor;
			lblPlaceholder.ForeColor = skin.Surface.DisabledForeColor;
		}
	}
}
