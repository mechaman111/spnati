using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(ValidationRecord), 0)]
	public partial class ValidateAllActivity : Activity
	{
		private bool _loaded;

		public ValidateAllActivity()
		{
			InitializeComponent();
		}

		protected override void OnFirstActivate()
		{
			if (!CharacterDatabase.AllLoaded)
			{
				Shell.Instance.LaunchWorkspace(new CharacterLoaderRecord());
			}
		}
		protected override void OnActivate()
		{
			if (!CharacterDatabase.AllLoaded || _loaded)
			{
				return;
			}
			_loaded = true;
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
