using Desktop;
using Desktop.Providers;
using Desktop.Skinning;
using System;
using System.Windows.Forms;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.Forms
{
	public partial class MacroManager : SkinnedForm, IMacroEditor
	{
		private Type _macroType;
		private string _key;
		private MacroProvider _provider;

		public MacroManager()
		{
			InitializeComponent();
		}

		public void SetType(Type type, string key)
		{
			_key = key;
			_macroType = type;

			_provider = new MacroProvider();
			_provider.SetContext(_macroType);
			BuildMacroList();
		}

		private void BuildMacroList()
		{
			Macro selected = lstMacros.SelectedItem as Macro;
			lstMacros.Items.Clear();
			foreach (IRecord record in _provider.GetRecords("", new LookupArgs()))
			{
				Macro macro = record as Macro;
				lstMacros.Items.Add(macro);
			}
			lstMacros.Sorted = true;
			if (selected != null)
			{
				lstMacros.SelectedItem = selected;
			}
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			Macro macro = lstMacros.SelectedItem as Macro;
			if (macro == null) { return; }
			if (MessageBox.Show("Are you sure you want to delete this macro? This operation cannot be undone.", "Delete Macro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				lstMacros.Items.Remove(macro);
				_provider.Remove(_macroType, macro);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Config.SaveMacros(_key);
			DialogResult = DialogResult.OK;
			Close();
		}

		public const string HelpText = "Macros are shortcuts for quickly pulling in one or more conditions. A \"Macros\" button will appear in the Case Editor with an option using the provided macro name. When selected, it will pull in all the conditions provided in this form, exactly as they are filled out.";

		public static bool ShowMacroHelp
		{
			get
			{
				if (!Config.SeenMacroHelp)
				{
					Config.SeenMacroHelp = true;
					Config.Save();
					return true;
				}
				return false;
			}
		}

		public bool ShowHelp
		{
			get
			{
				return ShowMacroHelp;
			}
		}

		public string GetHelpText()
		{
			return HelpText;
		}

		public object CreateData()
		{
			Case data = new Case("opponent_lost");
			return data;
		}

		public object GetRecordContext()
		{
			Character character = Shell.Instance.ActiveWorkspace?.Record as Character;
			if (character == null)
			{
				character = RecordLookup.DoLookup(typeof(Character), "") as Character;
			}
			return character;
		}

		public object GetSecondaryRecordContext()
		{
			return GetRecordContext();
		}

		public Func<PropertyRecord, bool> GetRecordFilter(object data)
		{
			return null;
		}

		private void cmdEdit_Click(object sender, EventArgs e)
		{
			Macro macro = lstMacros.SelectedItem as Macro;
			if (macro == null) { return; }
			MacroEditor editor = new MacroEditor();
			editor.SetMacro(macro, this);
			if (editor.ShowDialog() == DialogResult.OK)
			{
				BuildMacroList();
			}
		}

		private void cmdNew_Click(object sender, EventArgs e)
		{
			Macro macro = _provider.Create("New Macro") as Macro;
			lstMacros.Items.Add(macro);
			lstMacros.SelectedItem = macro;
			cmdEdit_Click(sender, e);
		}

		private void lstMacros_DoubleClick(object sender, EventArgs e)
		{
			cmdEdit_Click(sender, e);
		}

		public void AddSpeedButtons(PropertyTable table)
		{
			CaseControl.AddSpeedButtons(table, "opponent_lost");
		}
	}
}
