using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class DialogueResponder : Form
	{
		private Case _sourceCase;
		private Case _responseCase;
		private Character _source;
		private Character _responder;

		public DialogueResponder()
		{
			InitializeComponent();
		}

		public DialogueResponder(Character source, Case sourceCase, Character responder, Case responseCase) : this()
		{
			lblResponse.Text = "Response from " + responder;
			_source = source;
			_sourceCase = sourceCase;
			_responder = responder;
			_responseCase = responseCase;
			gridSource.SetData(source, sourceCase);
			responseControl.SetCharacter(responder);
			responseControl.HighlightRow += ResponseControl_HighlightRow;
			responseControl.SetCase(new Stage(responseCase.Stages[0]), responseCase);
		}

		private void cmdJumpToDialogue_Click(object sender, System.EventArgs e)
		{
			responseControl.Save();
			DialogResult = DialogResult.Retry;
			Close();
		}

		private void cmdAccept_Click(object sender, System.EventArgs e)
		{
			responseControl.Save();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
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

		private void DialogueResponder_Shown(object sender, System.EventArgs e)
		{
			responseControl.Focus();
		}
	}
}
