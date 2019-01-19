using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Activities;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	/// <summary>
	/// IMPORTANT - This is NOT a generic control and will only work with a CrossStagePose
	/// </summary>
	public partial class PoseStageSelect : PropertyEditControl
	{
		private IWardrobe _wardrobe;
		private CrossStagePose _pose;
		private Character _character;

		public PoseStageSelect()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			_pose = Data as CrossStagePose;

			PoseContext context = Context as PoseContext;
			if (context != null)
			{
				_wardrobe = context.Character as IWardrobe;
				_character = context.Character.Character;
				CreateStageCheckboxes();
			}
		}

		private void CreateStageCheckboxes()
		{
			//Stage checkmarks
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box != null)
				{
					box.CheckedChanged -= Check_CheckedChanged;
				}
			}
			flowStageChecks.Controls.Clear();

			string file = _pose.FileName ?? "";

			int layers = _character.Layers + Clothing.ExtraStages;
			for (int i = 0; i < layers; i++)
			{
				StageName stage = _character.LayerToStageName(i, false, _wardrobe);
				CheckBox check = new CheckBox();
				check.Tag = i;
				check.Text = string.Format("{0} ({1})", stage.DisplayName, stage.Id);
				check.Width = 180;
				check.Margin = new Padding(0);
				check.Checked = _pose.Stages.Contains(i);
				check.CheckedChanged += Check_CheckedChanged;
				check.Enabled = (!file.StartsWith(i.ToString()));
				flowStageChecks.Controls.Add(check);
			}
		}

		private void Check_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = sender as CheckBox;
			int stage = (int)box.Tag;
			if (box.Checked)
			{
				_pose.Stages.Add(stage);
			}
			else
			{
				_pose.Stages.Remove(stage);
			}
		}
	}

	public class PoseStageAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(PoseStageSelect); }
		}
	}
}
