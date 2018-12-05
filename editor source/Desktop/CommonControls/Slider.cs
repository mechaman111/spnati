using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class Slider : UserControl
	{
		public Slider()
		{
			InitializeComponent();
		}

		public string Label
		{
			get { return lblLabel.Text; }
			set { lblLabel.Text = value; }
		}

		public int Value
		{
			get { return trackbar.Value; }
			set { trackbar.Value = value; }
		}

		public int TickFrequency
		{
			get { return trackbar.TickFrequency; }
			set { trackbar.TickFrequency = value; }
		}

		public int Maximum
		{
			get { return trackbar.Maximum; }
			set { trackbar.Maximum = value; }
		}

		public int Minimum
		{
			get { return trackbar.Minimum; }
			set { trackbar.Minimum = value; }
		}

		public int SmallChange
		{
			get { return trackbar.SmallChange; }
			set { trackbar.SmallChange = value; }
		}

		public int LargeChange
		{
			get { return trackbar.LargeChange; }
			set { trackbar.LargeChange = value; }
		}

		public event EventHandler ValueChanged;
		private void trackbar_ValueChanged(object sender, EventArgs e)
		{
			lblValue.Text = trackbar.Value.ToString();
			ValueChanged?.Invoke(this, e);
		}
	}
}
