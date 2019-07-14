using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class ColorField : Button
	{
		private ColorDialog _dialog;
		public event EventHandler ColorChanged;

		public ColorField()
		{
			FlatStyle = FlatStyle.Flat;
			_dialog = new ColorDialog();
		}

		public Color Color
		{
			get { return BackColor; }
			set
			{
				BackColor = value;
				ColorChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			_dialog.Color = Color;
			if (_dialog.ShowDialog() == DialogResult.OK)
			{
				Color = _dialog.Color;
			}
		}
	}
}
