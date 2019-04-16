using System;
using System.Windows.Forms;

namespace Desktop.Forms
{
	public partial class MacroHelp : Form
	{
		public MacroHelp()
		{
			InitializeComponent();
		}

		public string HelpText
		{
			set
			{
				lblHelp.Text = value;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
