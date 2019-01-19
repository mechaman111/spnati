using Desktop;
using SPNATI_Character_Editor.Controls;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	// >>>> NOT READY FOR 3.4
	//[Activity(typeof(Character), 210)]
	//[Activity(typeof(Costume), 210)]
	public partial class PoseCreator : Activity
	{
		private CharacterEditorData _editorData;
		private ISkin _character;

		private CrossStagePose _currentLink;
		private Pose _currentPose;

		private bool _populatingPoses;

		public PoseCreator()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Pose Maker"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
			_editorData = CharacterDatabase.GetEditorData(_character.Character);
			table.Context = new PoseContext(_character);
		}

		protected override void OnActivate()
		{
			Workspace.ToggleSidebar(false);
			RebuildPoseList();
		}

		private void RebuildPoseList()
		{
			_populatingPoses = true;
			object selectedItem = lstPoses.SelectedItem;

			//Rebuild the pose list because things could've been added from another activity
			lstPoses.Items.Clear();
			foreach (CrossStagePose pose in _editorData.Poses)
			{
				lstPoses.Items.Add(pose);
			}

			lstPoses.SelectedItem = selectedItem;
			_populatingPoses = false;
		}

		protected override void OnDeactivate()
		{
			Workspace.ToggleSidebar(true);
		}

		private void lstPoses_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingPoses)
			{
				return;
			}

			table.Save();

			object selected = lstPoses.SelectedItem;
			if (selected is CrossStagePose)
			{
				_currentLink = selected as CrossStagePose;
			}
			else
			{
				_currentPose = selected as Pose;
			}
			table.Data = selected;
		}

		private void tsAddLink_Click(object sender, System.EventArgs e)
		{
			CrossStagePose pose = new CrossStagePose();
			if (openFileDialog1.ShowDialog(_character, "") == System.Windows.Forms.DialogResult.OK)
			{
				pose.FileName = openFileDialog1.FileName;
				if (!string.IsNullOrEmpty(pose.FileName))
				{
					char stageChar = pose.FileName[0];
					if (char.IsDigit(stageChar))
					{
						int stage = int.Parse(stageChar.ToString());
						pose.Stages.Add(stage);
					}
					_editorData.Poses.Add(pose);
					lstPoses.Items.Add(pose);
					lstPoses.SelectedItem = pose;
				}
			}
		}

		private void table_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "FileName" || e.PropertyName == "Id")
			{
				RebuildPoseList();
			}
		}

		private void tsRemove_Click(object sender, System.EventArgs e)
		{
			CrossStagePose link = lstPoses.SelectedItem as CrossStagePose;
			if (link != null)
			{
				if (MessageBox.Show($"Are you sure you want to unlink {link} from use in other stages?", "Unlink Pose", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					_editorData.Poses.Remove(link);
					lstPoses.Items.Remove(link);
				}
			}
			Pose pose = lstPoses.SelectedItem as Pose;
			if (pose != null)
			{
				if (MessageBox.Show($"Are you sure you want to remove {pose}? This cannot be undone.", "Remove Pose", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					
				}
			}
		}
	}

	public class PoseContext : ICharacterContext
	{
		public ISkin Character { get; }

		public PoseContext(ISkin character)
		{
			Character = character;
		}
	}
}
