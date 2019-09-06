using Desktop.Skinning;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
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

			foreach (StageImage stageImage in line.Images)
			{
				Add(stageImage);
			}
		}

		private void Add(StageImage stageImage)
		{
			StageImageControl control = new StageImageControl();
			control.Dock = DockStyle.Top;
			control.SetData(_character, _workingCase, stageImage);
			pnlImages.Controls.Add(control);
			pnlImages.Controls.SetChildIndex(control, 0);
			control.Delete += Control_Delete;
			control.Preview += Control_Preview;
		}

		private void Control_Delete(object sender, EventArgs e)
		{
			Control ctl = sender as Control;
			pnlImages.Controls.Remove(ctl);
		}

		private void Control_Preview(object sender, UpdateImageArgs e)
		{
			ShowImage(e.Pose, e.Stage);
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
			_line.Images.Clear();
			for(int i = pnlImages.Controls.Count - 1; i >= 0; i--)
			{
				StageImageControl stageControl = pnlImages.Controls[i] as StageImageControl;
				if (stageControl != null)
				{
					_line.Images.Add(stageControl.StageImage);
				}
			}
			_line.NotifyPropertyChanged(nameof(_line.Images));

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}
		
		private void tsAdd_Click(object sender, EventArgs e)
		{
			Add(new StageImage());
		}

		private void pnlImages_ControlAdded(object sender, ControlEventArgs e)
		{
			if (pnlImages.Controls.Count > 1)
			{
				pnlZeroState.Visible = false;
			}
		}

		private void pnlImages_ControlRemoved(object sender, ControlEventArgs e)
		{
			if (pnlImages.Controls.Count == 1)
			{
				pnlZeroState.Visible = true;
			}
		}
	}
}
