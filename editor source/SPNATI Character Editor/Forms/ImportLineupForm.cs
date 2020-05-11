using Desktop.Skinning;
using KisekaeImporter;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ImportLineupForm : SkinnedForm
	{
		public KisekaeCode Code { get; private set; }

		public ImportLineupForm(ImportLineupMode mode)
		{
			InitializeComponent();

			switch (mode)
			{
				case ImportLineupMode.Pose:
					picHelp.Image = new Bitmap("Resources/Images/LineupPose.png");
					break;
				case ImportLineupMode.Wardrobe:
					picHelp.Image = new Bitmap("Resources/Images/LineupClothing.png");
					break;
				default:
					picHelp.Image = new Bitmap("Resources/Images/LineupAll.png");
					break;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			string text = txtCode.Text;
			if (!text.Contains("***"))
			{
				MessageBox.Show("This does not appear to be a character lineup. Do you have ALL selected in Kisekae's Export window?");
				return;
			}

			Code = new KisekaeCode(txtCode.Text, false);

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	public enum ImportLineupMode
	{
		All,
		Wardrobe,
		Pose
	}
}
