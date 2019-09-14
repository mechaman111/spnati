using Desktop.Forms;
using Desktop.Providers;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public partial class MacroEditor : SkinnedForm
	{
		private Macro _macro;
		private IMacroEditor _editor;

		public MacroEditor()
		{
			InitializeComponent();

			cboMenu.Items.AddRange(new string[] { "", "Filter", "Game", "Player", "Table" });
		}

		public void SetMacro(Macro macro, IMacroEditor editor)
		{
			_macro = macro;
			_editor = editor;
			txtName.Text = macro.Name;
			cboMenu.SelectedItem = macro.Group;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!SaveMacro())
			{
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private bool SaveMacro()
		{
			string name = txtName.Text;
			if (string.IsNullOrEmpty(name))
			{
				MessageBox.Show("Macro name cannot be blank.", "Save Macro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			MacroProvider provider = new MacroProvider();
			Type macroType = tableConditions.Data.GetType();
			provider.SetContext(macroType);
			Macro existing = provider.Get(macroType, name);
			if (existing != null && existing != _macro)
			{
				MessageBox.Show($"\"{name}\" is already in use as another macro.", "Save Macro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			Macro macro = tableConditions.CreateMacro();
			_macro.CopyFrom(macro);
			if (name != _macro.Name)
			{
				provider.Remove(macroType, _macro);
				_macro.Name = name;
				provider.Add(macroType, _macro);
			}
			_macro.Group = cboMenu.SelectedItem?.ToString() ?? "";
			return true;
		}

		private void lblHelp_Click(object sender, EventArgs e)
		{
			ShowHelp();
		}

		private void ShowHelp()
		{
			MacroHelp form = new MacroHelp();
			form.HelpText = _editor.GetHelpText();
			form.ShowDialog();
		}

		private void MacroEditor_Shown(object sender, EventArgs e)
		{
			if (_editor.ShowHelp)
			{
				ShowHelp();
			}
		}

		private void MacroEditor_Load(object sender, EventArgs e)
		{
			tableConditions.Data = _editor.CreateData();
			tableConditions.Context = _editor.GetRecordContext();
			tableConditions.RecordFilter = _editor.GetRecordFilter(tableConditions.Data);
			_editor.AddSpeedButtons(tableConditions);
			tableConditions.ApplyMacro(_macro, new Dictionary<string, string>());
		}
	}
}
