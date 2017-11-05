using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// About form
	/// </summary>
	public partial class About : Form
	{
		private const string Version = "1.09";

		public About()
		{
			InitializeComponent();
			lblVersion.Text = string.Format("SPNATI Character Editor v{0}", Version);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
