using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), -1, Width = 251, Pane = WorkspacePane.Sidebar)]
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
			_character.PrepareForEdit();
			SubscribeWorkspace(WorkspaceMessages.DialogueUpdated, UpdateLineCount);
			SubscribeWorkspace<CharacterImage>(WorkspaceMessages.UpdatePreviewImage, UpdatePreviewImage);
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
			lblLinesOfDialogue.Text = _character.Behavior.UniqueLines.ToString();
		}
	}
}
