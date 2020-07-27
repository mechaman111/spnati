using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ScratchPadControl : UserControl
	{
		private Character _character;
		private CharacterEditorData _editorData;

		public ScratchPadControl()
		{
			InitializeComponent();
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(_character);

			importCtl.SetCharacter(character);
			importCtl.ImportedLines += ImportCtl_ImportedLines;

			List<string> lines = _editorData.FreeLines;
			PopulateLines(lines);
		}

		private void PopulateLines(List<string> lines)
		{
			if (lines != null)
			{
				string text = string.Join("\r\n", lines);
				txtLines.Text = text;
			}
			else
			{
				txtLines.Text = "";
			}
		}

		public void Save()
		{
			List<string> lines = GetLines();
			_editorData.FreeLines = lines;
		}

		/// <summary>
		/// Converts the text field into lines
		/// </summary>
		/// <returns></returns>
		private List<string> GetLines()
		{
			string text = txtLines.Text;
			string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			return new List<string>(lines);
		}

		private void ImportCtl_ImportedLines(object sender, List<string> e)
		{
			PopulateLines(e);
		}

		private void cmdImport_Click(object sender, EventArgs e)
		{
			List<string> lines = GetLines();
			importCtl.SetLines(lines);
			importCtl.Visible = true;
		}

		private void importCtl_Cancel(object sender, EventArgs e)
		{
			importCtl.Visible = false;
		}
	}
}
