using System;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class HelpForm : Form
	{
		public HelpForm()
		{
			InitializeComponent();
		}

		private void HelpForm_Load(object sender, EventArgs e)
		{
			wb.Navigate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help", "help.html"));
		}
	}
}
