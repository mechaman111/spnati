using Desktop.Skinning;
using System;

namespace SPNATI_Character_Editor.Forms
{
	public partial class FailedImport : SkinnedForm
	{
		public FailedImport()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
