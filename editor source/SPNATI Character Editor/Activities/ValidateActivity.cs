using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Spacer]
	[Activity(typeof(Character), 500, DelayRun = true, Caption = "Validate")]
	public partial class ValidateActivity : Activity
	{
		private Character _character;

		public ValidateActivity()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Validate";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnActivate()
		{
			ctlValidation.DoValidation(_character);
		}
	}
}
