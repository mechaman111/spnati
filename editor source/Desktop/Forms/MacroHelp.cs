using Desktop.Skinning;
using System;

namespace Desktop.Forms
{
	public partial class MacroHelp : SkinnedForm
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
