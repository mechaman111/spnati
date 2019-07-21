using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// About form
	/// </summary>
	public partial class About : SkinnedForm
	{
		public About()
		{
			InitializeComponent();
			lblVersion.Text = string.Format("SPNATI Character Editor {0}", Config.Version);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
