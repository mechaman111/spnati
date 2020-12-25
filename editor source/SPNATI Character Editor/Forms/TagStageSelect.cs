using Desktop.Skinning;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class TagStageSelect : SkinnedForm
	{
		private BindableTag _bindable;

		public TagStageSelect()
		{
			InitializeComponent();
		}

		public void SetData(BindableTag tag, ISkin skin)
		{
			_bindable = tag;
			Character character = skin.Character;

			Tag definition = TagDatabase.GetTag(tag.Tag);
			if (definition != null)
			{
				lblTag.Text = definition.ToLookupString();
			}
			else
			{
				lblTag.Text = tag.Tag;
			}

			for (int stage = 0; stage < character.Layers + Clothing.ExtraStages; stage++)
			{
				ListViewItem item = lstItems.Items.Add(character.LayerToStageName(stage).DisplayName);
				item.Tag = stage;
				if (tag.HasStage(stage))
				{
					item.Checked = true;
				}
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			foreach (ListViewItem item in lstItems.Items)
			{
				int stage = (int)item.Tag;
				if (item.Checked && !_bindable.HasStage(stage))
				{
					_bindable.AddStage(stage);
				}
				else if (!item.Checked && _bindable.HasStage(stage))
				{
					_bindable.RemoveStage(stage);
				}
			}
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void cmdSelectAll_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem item in lstItems.Items)
			{
				item.Checked = true;
			}
		}
	}
}
