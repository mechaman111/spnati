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
				chkText.Checked = Config.GetBoolean("PreviewText");
			}
			else
			{
				lblSkin.Visible = false;
				cboSkin.Visible = false;
				chkText.Visible = false;
			}
			SubscribeWorkspace<DialogueLine>(WorkspaceMessages.PreviewLine, UpdatePreview);
			SubscribeWorkspace<CharacterImage>(WorkspaceMessages.UpdatePreviewImage, UpdatePreviewImage);
			UpdateLineCount();
		}

		protected override void OnActivate()
		{
			PopulateSkinCombo();
		}

		private void PopulateSkinCombo()
		{
			if (_character == null) { return; }

			SkinLink previous = cboSkin.SelectedItem as SkinLink;

			cboSkin.Items.Clear();
			cboSkin.Items.Add("- Default - ");
			foreach (AlternateSkin alt in _character.Metadata.AlternateSkins)
			{
				foreach (SkinLink link in alt.Skins)
				{
					cboSkin.Items.Add(link);
				}
			}
			cboSkin.Sorted = true;
			cboSkin.Visible = cboSkin.Items.Count > 1;
			lblSkin.Visible = cboSkin.Visible;

			if (previous == null)
			{
				cboSkin.SelectedIndex = 0;
			}
			else
			{
				cboSkin.SelectedItem = previous;
			}
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

		private void UpdatePreview(DialogueLine line)
		{
			if (Config.GetBoolean(Settings.HideImages))
				return;
			picPortrait.SetText(line);
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

		private void cboSkin_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_character == null) { return; } 
			SkinLink current = cboSkin.SelectedItem as SkinLink;
			_character.CurrentSkin = current?.Costume;

			//update images in use to use new skin
			ImageLibrary library = ImageLibrary.Get(_character);
			library.UpdateSkin(_character.CurrentSkin);
			Workspace.SendMessage(WorkspaceMessages.SkinChanged);
		}

		private void chkText_CheckedChanged(object sender, System.EventArgs e)
		{
			bool preview = chkText.Checked;
			Config.Set("PreviewText", preview);
			picPortrait.ShowTextBox = preview;
		}
	}
}
