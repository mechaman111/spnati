using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class AddSheetForm : SkinnedForm
	{
		public AddSheetForm(string name)
		{
			InitializeComponent();
			txtName.Text = name;
		}

		public void SetMatrix(PoseMatrix matrix)
		{
			cboSheet.Visible = true;
			lblSheet.Visible = true;
			cboSheet.Items.Add("");
			foreach (PoseSheet sheet in matrix.Sheets)
			{
				cboSheet.Items.Add(sheet);
			}
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

		public string SheetName
		{
			get { return txtName.Text; }
		}

		public PoseSheet SelectedSheet
		{
			get { return cboSheet.SelectedItem as PoseSheet; }
		}
	}
}
