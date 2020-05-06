using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class CallOutForm : SkinnedForm
	{
		public CallOutForm()
		{
			InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void cmdCreate_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		public SituationPriority Priority
		{
			get
			{
				return radPri1.Checked ? SituationPriority.MustTarget :
					radPri2.Checked ? SituationPriority.Noteworthy :
					radPri3.Checked ? SituationPriority.FYI :
					SituationPriority.Prompt;
			}
		}
	}
}
