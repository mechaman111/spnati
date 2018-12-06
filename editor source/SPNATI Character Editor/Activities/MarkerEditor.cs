using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 50)]
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

		public override void Save()
		{
			grid.Save();
		}
	}
}
