using SPNATI_Character_Editor.DataStructures;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CodeReplaceBar : UserControl
	{
		private List<IPoseCode> _selectedCells = new List<IPoseCode>();
		private PoseSheet _sheet;

		public event EventHandler Close;

		public CodeReplaceBar()
		{
			InitializeComponent();
			cboSearch.Items.Add("Selected Cells");
			cboSearch.Items.Add("Whole Sheet");
			cboSearch.SelectedIndex = 0;
		}

		public void SetFocus()
		{
			txtFind.Focus();
		}

		public void SetSheet(PoseSheet sheet, List<IPoseCode> cells)
		{
			_sheet = sheet;
			_selectedCells = cells;
		}

		private void cmdReplace_Click(object sender, EventArgs e)
		{
			string find = txtFind.Text;
			if (string.IsNullOrEmpty(find))
			{
				return;
			}

			List<IPoseCode> codesToModify = _selectedCells;
			if (cboSearch.SelectedIndex == 1)
			{
				codesToModify = new List<IPoseCode>();
				codesToModify.Add(_sheet);
				foreach (PoseStage stage in _sheet.Stages)
				{
					codesToModify.Add(stage);
					foreach (PoseEntry entry in stage.Poses)
					{
						codesToModify.Add(entry);
					}
				}
			}
			ReplaceText(codesToModify, find.ToLower(), (txtReplace.Text ?? "").ToLower());
		}

		private void txtFind_TextChanged(object sender, EventArgs e)
		{
			txtReplace.Enabled = !string.IsNullOrEmpty(txtFind.Text);
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			Close?.Invoke(this, EventArgs.Empty);
		}

		private void ReplaceText(List<IPoseCode> codes, string find, string replace)
		{
			int count = 0;

			Regex regex = new Regex(Regex.Escape(find), RegexOptions.IgnoreCase);
			foreach (IPoseCode item in codes)
			{
				string code = item.GetCode();
				if (!string.IsNullOrEmpty(code))
				{
					code = regex.Replace(code, (match) =>
					{
						count++;
						return replace;
					});
					item.SetCode(code);
					continue;
				}

			}
			MessageBox.Show($"Replaced {count} instances of \"{find}\" across {codes.Count} codes.");
		}
	}
}
