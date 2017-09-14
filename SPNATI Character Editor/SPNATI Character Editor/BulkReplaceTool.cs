using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class BulkReplaceTool : Form
	{
		private string _sourceTag;
		public string SourceTag
		{
			get { return _sourceTag; }
			set
			{
				_sourceTag = value;
				Trigger trigger = TriggerDatabase.GetTrigger(value);
				cboSource.SelectedItem = trigger;
			}
		}
		public HashSet<string> DestinationTags = new HashSet<string>();

		public BulkReplaceTool()
		{
			InitializeComponent();

			PopulateComboBoxes();
		}

		private void PopulateComboBoxes()
		{
			cboSource.DataSource = TriggerDatabase.Triggers;
			cboSource.BindingContext = new BindingContext();
			DataGridViewComboBoxColumn column = gridDestinations.Columns[0] as DataGridViewComboBoxColumn;
			column.Items.Add("");
			foreach (Trigger trigger in TriggerDatabase.Triggers)
			{
				column.Items.Add(trigger.Label);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to replace all non-targeted dialogue in the selected cases?", "Bulk Replace", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				DialogResult = DialogResult.OK;
				Trigger source = cboSource.SelectedItem as Trigger;
				_sourceTag = source?.Tag;
				DestinationTags.Clear();
				foreach (DataGridViewRow row in gridDestinations.Rows)
				{
					object value = row.Cells[0].Value;
					if (value == null)
						continue;
					string label = value.ToString();
					Trigger trigger = TriggerDatabase.Triggers.Find(t => t.Label == label);
					if (trigger == null)
						continue;
					DestinationTags.Add(trigger.Tag);
				}

				this.Close();
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
