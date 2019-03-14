using Desktop;
using Desktop.Providers;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class MacroManager : Form
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
			foreach (IRecord record in _provider.GetRecords(""))
			{
				Macro macro = record as Macro;
				lstMacros.Items.Add(macro);
			}
			lstMacros.Sorted = true;
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			Macro macro = lstMacros.SelectedItem as Macro;
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
	}
}
