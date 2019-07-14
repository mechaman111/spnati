using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class CanvasHelp : SkinnedForm
	{
		public CanvasHelp()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
