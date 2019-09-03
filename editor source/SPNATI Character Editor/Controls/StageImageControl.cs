using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class StageImageControl : UserControl
	{
		private Case _workingCase;
		private Character _character;
		private StageImage _image;

		public event EventHandler Delete;
		public event EventHandler<UpdateImageArgs> Preview;

		public StageImageControl()
		{
			InitializeComponent();
		}

		public StageImage StageImage
		{
			get
			{
				_image.Pose = cboImage.SelectedItem as PoseMapping;
				_image.Stages.Clear();
				foreach (int stage in GetSelectedStages())
				{
					_image.Stages.Add(stage);
				}
				return _image;
			}
		}

		public void SetData(Character character, Case workingCase, StageImage image)
		{
			_character = character;
			_workingCase = workingCase.Copy();
			_image = image;
			HashSet<PoseMapping> poses = new HashSet<PoseMapping>();
			//limit poses to those available in at least one selected stage
			foreach (int stage in workingCase.Stages)
			{
				gridStages.AllowedStages.Add(stage);
				foreach (PoseMapping pose in character.PoseLibrary.GetPoses(stage))
				{
					poses.Add(pose);
				}
			}
			foreach (int stage in image.Stages)
			{
				_workingCase.Stages.Add(stage);
			}
			List<PoseMapping> list = poses.ToList();
			list.Sort();
			cboImage.DataSource = list;
			gridStages.SetData(_character, _workingCase, _workingCase.Stages.Count > 0 ? _workingCase.Stages[0] : -1);
			gridStages.LayerSelected += GridStages_LayerSelected;
			cboImage.SelectedItem = image.Pose;
		}

		private void GridStages_LayerSelected(object sender, int e)
		{
			gridStages.SetPreviewStage(e);
			PoseMapping pose = cboImage.SelectedItem as PoseMapping;
			Preview?.Invoke(this, new UpdateImageArgs(_character, pose, e));
		}

		private HashSet<int> GetSelectedStages()
		{
			HashSet<int> selectedStages = new HashSet<int>();
			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				if (gridStages.GetChecked(i))
				{
					selectedStages.Add(i);
				}
			}
			return selectedStages;
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			Delete?.Invoke(this, EventArgs.Empty);
		}

		private void cboImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			PoseMapping pose = cboImage.SelectedItem as PoseMapping;
			if (pose != null)
			{
				foreach (int stage in _workingCase.Stages)
				{
					PoseReference poseRef = pose.GetPose(stage);
					if (poseRef != null)
					{
						gridStages.AllowedStages.Add(stage);
					}
					else
					{
						gridStages.AllowedStages.Remove(stage);
						if (gridStages.GetChecked(stage))
						{
							gridStages.ToggleStage(stage);
						}
					}
				}
			}
			Preview?.Invoke(this, new UpdateImageArgs(_character, pose, gridStages.GetPreviewStage()));
			gridStages.Invalidate(true);
		}
	}
}
