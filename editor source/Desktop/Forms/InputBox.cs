using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace Desktop
{
	public partial class InputBox : SkinnedForm
	{
		public string LabelText
		{
			get
			{
				return lblText.Text;
			}
			set
			{
				lblText.Text = value;
			}
		}

		public string Value
		{
			get
			{
				return txtValue.Text;
			}
		}

		public InputBox()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public static string Show(string label, string caption)
		{
			InputBox box = new InputBox();
			box.Text = caption;
			box.LabelText = label;
			DialogResult result = box.ShowDialog();
			if (result == DialogResult.OK)
			{
				return box.Value;
			}
			return null;
		}
	}
}
