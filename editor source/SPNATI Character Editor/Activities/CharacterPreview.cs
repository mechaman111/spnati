using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), -1, Width = 251, Pane = WorkspacePane.Sidebar)]
	[Activity(typeof(Costume), -1, Width = 251, Pane = WorkspacePane.Sidebar)]
	public partial class CharacterPreview : Activity
	{
		private Character _character;

		public CharacterPreview()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Overview"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			if (_character != null)
			{
				_character.PrepareForEdit();
				_character.Behavior.CaseAdded += WorkingCasesChanged;
				_character.Behavior.CaseRemoved += WorkingCasesChanged;
				_character.Behavior.CaseModified += WorkingCasesChanged;
			}
			SubscribeWorkspace<CharacterImage>(WorkspaceMessages.UpdatePreviewImage, UpdatePreviewImage);
			UpdateLineCount();
		}

		private void WorkingCasesChanged(object sender, Case e)
		{
			UpdateLineCount();
		}

		public override void Quit()
		{
			picPortrait.Destroy();
		}

		private void UpdatePreviewImage(CharacterImage image)
		{
			if (Config.GetBoolean(Settings.HideImages))
				return;
			picPortrait.SetImage(image);
		}

		private void UpdateLineCount()
		{
			if (_character != null)
			{
				lblLinesOfDialogue.Text = _character.Behavior.UniqueLines.ToString();
			}
			else
			{
				label4.Visible = false;
				lblLinesOfDialogue.Visible = false;
			}
		}
	}
}
