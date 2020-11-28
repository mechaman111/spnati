using Desktop;
using Desktop.Reporting;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 750, DelayRun = true, Caption = "Line Slicer")]
	public partial class CaseDataSlicer : Activity
	{
		private Character _character;

		public CaseDataSlicer()
		{
			InitializeComponent();
		}

		public override bool CanRun()
		{
			return !Config.SafeMode;
		}

		public override string Caption
		{
			get { return "Line Slicer"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			DataSlicer slicer = new DataSlicer(_character);
			ctlSlicer.SetSlicer(slicer, _character.Behavior.GetWorkingCases());
		}

		protected override void OnActivate()
		{
			Workspace.ToggleSidebar(false);
		}

		protected override void OnDeactivate()
		{
			Workspace.ToggleSidebar(true);
		}
	}
}
