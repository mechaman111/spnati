using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class AddSheetForm : SkinnedForm
	{
		private bool _advanced;
		public bool ShowAdvanced
		{
			get { return _advanced; }
			set
			{
				_advanced = value;

			}
		}

		public AddSheetForm(string name)
		{
			InitializeComponent();
			txtName.Text = name;
		}

		public void SetMatrix(PoseMatrix matrix)
		{
			cboSheet.Visible = true;
			lblSheet.Visible = true;
			chkGlobal.Visible = true;
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

		public bool Global
		{
			get { return chkGlobal.Checked; }
		}

		private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
		{
			PoseSheet sheet = cboSheet.SelectedItem as PoseSheet;
			if (sheet != null && sheet.IsGlobal)
			{
				chkGlobal.Checked = sheet.IsGlobal;
			}
		}
	}
}
