using Desktop;
using SPNATI_Character_Editor.Controls;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 210)]
	[Activity(typeof(Costume), 210)]
	public partial class PoseCreator : Activity
	{
		private CharacterEditorData _editorData;
		private ISkin _character;

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

			_currentPose = lstPoses.SelectedItem as Pose;
			table.Data = _currentPose;
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
