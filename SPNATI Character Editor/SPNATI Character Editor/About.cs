using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// About form
	/// </summary>
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
