using Desktop.Skinning;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ResponseSetupForm : SkinnedForm
	{
		private Character _character;
		private Case _original;
		private Case _response;

		private HashSet<ListViewItem> _disabledStages = new HashSet<ListViewItem>();

		public ResponseSetupForm()
		{
			InitializeComponent();
		}

		public void SetData(Character character, Case speakerCase, Case response)
		{
			_character = character;
			_original = speakerCase;
			_response = response;

			PopulateStages();
			PopulateMarker();
		}

		private void PopulateStages()
		{
			bool limitedStages = _response.Stages.Count < _character.Layers + Clothing.ExtraStages;

			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				StageName stage = _character.LayerToStageName(i);
				ListViewItem item = new ListViewItem(string.Format("{0} ({1})", stage.DisplayName, stage.Id));
				item.Tag = i;

				if (_response.Stages.Contains(i))
				{
					item.Checked = true;
				}
				else if (limitedStages)
				{
					_disabledStages.Add(item);
					item.ForeColor = SystemColors.GrayText;
				}

				lstStages.Items.Add(item);
			}
		}

		private void PopulateMarker()
		{
			if (TriggerDatabase.GetTrigger(_original.Tag).OncePerStage)
			{
				chkOneTime.Visible = false; //if the line can only happen once, there's no point in marking the response as one time
			}

			if (!string.IsNullOrEmpty(_response.NotSaidMarker))
			{
				//if we're already looking at a marker not being said, we can't make this one time
				chkOneTime.Visible = false;
			}
		}

		private void chkOneTime_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkOneTime.Checked)
			{
				txtMarker.Enabled = true;
				txtMarker.Focus();
			}
			else
			{
				txtMarker.Enabled = false;
				txtMarker.Text = "";
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;

			_response.Stages.Clear();
			foreach (ListViewItem item in lstStages.Items)
			{
				if (item.Checked)
				{
					int stage = (int)item.Tag;
					_response.Stages.Add(stage);
				}
			}

			if (!string.IsNullOrEmpty(txtMarker.Text))
			{
				_response.NotSaidMarker = txtMarker.Text;
				//make a dialogue line that sets the marker
				DialogueLine line = new DialogueLine("", "Wow, that's pretty nifty.");
				line.Marker = txtMarker.Text;
				_response.Lines.Add(line);
			}

			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void lstStages_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			ListViewItem item = lstStages.Items[e.Index];
			if (_disabledStages.Contains(item))
			{
				e.NewValue = e.CurrentValue;
			}
		}
	}
}
