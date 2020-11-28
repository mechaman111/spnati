using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 50, DelayRun = true, Caption = "Markers")]
	public partial class MarkerEditor : Activity
	{
		public MarkerEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Markers";
			}
		}

		protected override void OnFirstActivate()
		{
			grid.SetCharacter(Record as Character);
		}

		protected override void OnActivate()
		{
			(Record as Character).IsDirty = true;
		}

		public override void Save()
		{
			grid.Save();
		}
	}
}
