using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(ValidationRecord), 0)]
	public partial class ValidateAllActivity : Activity
	{
		public ValidateAllActivity()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			ctlValidation.DoValidation(null);
		}

		public override bool CanDeactivate(DeactivateArgs args)
		{
			return !ctlValidation.IsBusy;
		}

		public override void Quit()
		{
			ctlValidation.Cancel();
		}
	}

	public class ValidationRecord : BasicRecord
	{
		public ValidationRecord()
		{
			Name = "Validation";
		}
	}

	public class ValidationProvider : BasicProvider<ValidationRecord>
	{
	}
}
