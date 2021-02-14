using Desktop;
using System;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 315, DelayRun = true, Caption = "Scratch Pad")]
	[Tutorial("https://www.youtube.com/watch?v=CLRoebAM4zk")]
	public partial class LineImporter : Activity
	{
		private Character _character;

		public LineImporter()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Scratch Pad"; }
		}

		protected override void OnInitialize()
		{
			tabs.TabPages.Remove(tabGameImport);
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			lineImportControl1.SetCharacter(_character);
			scratchPadControl1.SetCharacter(_character);
			int startIndex = Config.GetInt("importtab");
			tabs.SelectedIndex = Math.Min(startIndex, tabs.TabPages.Count - 1);
		}

		protected override void OnDeactivate()
		{
			scratchPadControl1.Abort();
		}

		public override void Save()
		{
			if (tabs.SelectedTab == tabGameImport)
			{
				lineImportControl1.Save();
			}
			else if (tabs.SelectedTab == tabScratchPad)
			{
				scratchPadControl1.Save();
			}
		}

		private void tabs_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (tabs.SelectedIndex == -1)
			{
				return;
			}
			int current = Config.GetInt("importtab");
			if (current != tabs.SelectedIndex)
			{
				Config.Set("importtab", tabs.SelectedIndex);
				Config.Save();
			}
		}
	}
}
