using Desktop;
using SPNATI_Character_Editor.Forms;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(ResponseRecord), 0)]
	public partial class RespondToThis : Activity
	{
		public RespondToThis()
		{
			InitializeComponent();
		}

		private bool _isNew;
		private Case _sourceCase;
		private Case _responseCase;
		private Character _source;
		private Character _responder;

		private bool _closing;
		private bool _jumpingToWorkspace;
		private bool _cancelling;

		protected override void OnInitialize()
		{
			ResponseRecord rec = Record as ResponseRecord;
			_isNew = rec.IsNew;
			lblFrom.Text = $"Responding to {rec.SourceCharacter}:";
			lblResponse.Text = "Response from " + rec.ResponseCharacter;
			_source = rec.SourceCharacter;
			_sourceCase = rec.SourceCase;
			_responder = rec.ResponseCharacter;
			_responseCase = rec.ResponseCase;
			gridSource.SetData(_source, _sourceCase);
			responseControl.SetCharacter(_responder);
			responseControl.SetCase(new Stage(_responseCase.Stages[0]), _responseCase);
		}

		protected override void OnFirstActivate()
		{
			responseControl.Focus();
		}

		private void cmdJumpToDialogue_Click(object sender, System.EventArgs e)
		{
			_jumpingToWorkspace = true;
			_closing = true;
			Shell.Instance.CloseWorkspace(Workspace);
		}

		private void cmdAccept_Click(object sender, System.EventArgs e)
		{
			_closing = true;
			Shell.Instance.CloseWorkspace(Workspace);
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			_cancelling = true;
			_closing = true;
			Shell.Instance.CloseWorkspace(Workspace);
		}

		private void gridSource_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			string image = gridSource.GetImage(index);
			ImageLibrary lib = ImageLibrary.Get(_source);
			CharacterImage img = null;
			img = lib.Find(image);
			if (img == null)
			{
				int stage = _sourceCase.Stages[0];
				image = DialogueLine.GetStageImage(stage, DialogueLine.GetDefaultImage(image));
				img = lib.Find(image);
			}
			imgSource.SetImage(img);
		}


		private void ResponseControl_HighlightRow(object sender, int line)
		{
			if (line == -1)
				return;
			string image = responseControl.GetImage(line);
			ImageLibrary lib = ImageLibrary.Get(_responder);
			CharacterImage img = null;
			img = lib.Find(image);
			if (img == null)
			{
				int stage = _responseCase.Stages[0];
				image = DialogueLine.GetStageImage(stage, DialogueLine.GetDefaultImage(image));
				img = lib.Find(image);
			}
			imgResponse.SetImage(img);
		}

		public override bool CanDeactivate(DeactivateArgs args)
		{
			if (_closing) { return true; }
			DiscardResponsePrompt prompt = new DiscardResponsePrompt();
			DialogResult result = prompt.ShowDialog();
			if (result == DialogResult.Cancel)
			{
				return false;
			}
			if (result == DialogResult.No)
			{
				_cancelling = true;
			}
			_closing = true;
			Shell.Instance.CloseWorkspace(Workspace);
			return true;
		}


		public override void Quit()
		{
			if (_cancelling) { return; }
			responseControl.Save();
			if (_isNew)
			{
				CharacterEditorData responderData = CharacterDatabase.GetEditorData(_responder);
				responderData?.MarkResponse(_source, _sourceCase, _responseCase);
				_responder.Behavior.AddWorkingCase(_responseCase);
			}

			if (_jumpingToWorkspace)
			{
				Shell.Instance.Launch<Character, DialogueEditor>(_responder, _responseCase);
			}
			else
			{
				//if their workspace isn't open, save them now
				IWorkspace ws = Shell.Instance.GetWorkspace(_responder);
				if (ws == null)
				{
					Serialization.ExportCharacter(_responder);
				}
			}
		}
	}

	public class ResponseRecord : BasicRecord
	{
		public Character SourceCharacter;
		public Case SourceCase;
		public Character ResponseCharacter;
		public Case ResponseCase;
		public bool IsNew;

		public ResponseRecord(Character source, Case sourceCase, Character responder, Case response, bool isNew)
		{
			Key = "Response";
			SourceCharacter = source;
			SourceCase = sourceCase;
			ResponseCharacter = responder;
			ResponseCase = response;
			IsNew = isNew;
			Name = $"{ResponseCharacter.FirstName}->{SourceCharacter.FirstName}";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
