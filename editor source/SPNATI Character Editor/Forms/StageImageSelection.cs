using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class StageImageSelection : SkinnedForm
	{
		private DialogueLine _line;
		private Character _character;
		private Case _workingCase;

		public StageImageSelection()
		{
			InitializeComponent();
		}
		public StageImageSelection(Character character, DialogueLine line, Case workingCase) : this()
		{
			_line = line;
			_character = character;
			_workingCase = workingCase;

			gridImages.Preview += GridImages_Preview;
			gridImages.SetData(_character, _workingCase, line);
		}

		private void GridImages_Preview(object sender, Tuple<PoseMapping, int> e)
		{
			ShowImage(e.Item1, e.Item2);
		}

		private void ShowImage(PoseMapping image, int stage)
		{
			if (image != null)
			{
				Desktop.IWorkspace ws = Desktop.Shell.Instance.GetWorkspace(_character);
				if (ws != null)
				{
					ws.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, stage));
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			_line.Images = gridImages.GetStages();
			_line.NotifyPropertyChanged(nameof(_line.Images));

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
