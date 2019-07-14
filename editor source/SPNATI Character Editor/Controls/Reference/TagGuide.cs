using Desktop;
using Desktop.CommonControls;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Reference
{
	public partial class TagGuide : UserControl
	{
		public TagGuide()
		{
			InitializeComponent();

			recTag.RecordType = typeof(Tag);
			recTag.RecordFilter = FilterTags;

			lstItems.AddColumn(new AccordionColumn("Tag", "Key")
			{
				Width = 80
			});
			lstItems.AddColumn(new AccordionColumn("Description", "Description")
			{
				FillWeight = 1
			});

			lstItems.RebuildColumns();
		}

		private bool FilterTags(IRecord tag)
		{
			return !string.IsNullOrEmpty(tag.Group);
		}

		private void lstItems_Load(object sender, EventArgs e)
		{
			if (DesignMode) { return; }

			TagList list = new TagList();
			lstItems.DataSource = list;
		}

		private void recTag_RecordChanged(object sender, RecordEventArgs e)
		{
			Tag tag = recTag.Record as Tag;
			if (tag != null)
			{
				lstItems.SelectedItem = tag;
			}
		}
	}

	public class TagList : GroupedList<Tag>
	{
		public TagList()
		{
			foreach (Tag tag in TagDatabase.Dictionary.Tags)
			{
				if (!string.IsNullOrEmpty(tag.Group))
				{
					AddItem(tag);
				}
			}
		}
	}
}
