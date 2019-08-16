using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class BulkReplaceTool : SkinnedForm
	{
		private string _sourceTag;
		public string SourceTag
		{
			get { return _sourceTag; }
			set
			{
				_sourceTag = value;
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(value);
				cboSource.SelectedItem = trigger;
				PrefillDestination(trigger);
			}
		}
		public HashSet<string> DestinationTags = new HashSet<string>();

		public BulkReplaceTool()
		{
			InitializeComponent();

			PopulateComboBoxes();
		}

		/// <summary>
		/// Prefills the destination box based on what it thinks would be useful for the source
		/// </summary>
		private void PrefillDestination(TriggerDefinition source)
		{
			if (source.BulkPairs != null)
			{
				foreach (string tag in source.BulkPairs)
				{
					TriggerDefinition t = TriggerDatabase.GetTrigger(tag);
					if (t != null)
					{
						DataGridViewRow row = gridDestinations.Rows[gridDestinations.Rows.Add()];
						row.Cells[0].Value = t.Label;
					}
				}
			}
		}


		private void PopulateComboBoxes()
		{
			cboSource.Items.AddRange(TriggerDatabase.Triggers);
			DataGridViewComboBoxColumn column = gridDestinations.Columns[0] as DataGridViewComboBoxColumn;
			column.Items.Add("");
			foreach (TriggerDefinition trigger in TriggerDatabase.Triggers)
			{
				column.Items.Add(trigger.Label);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to replace all non-targeted dialogue in the selected cases?", "Bulk Replace", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				DialogResult = DialogResult.OK;
				TriggerDefinition source = cboSource.SelectedItem as TriggerDefinition;
				_sourceTag = source?.Tag;
				DestinationTags.Clear();
				foreach (DataGridViewRow row in gridDestinations.Rows)
				{
					object value = row.Cells[0].Value;
					if (value == null)
						continue;
					string label = value.ToString();
					TriggerDefinition trigger = TriggerDatabase.Triggers.Find(t => t.Label == label);
					if (trigger == null)
						continue;
					if (trigger.Tag == _sourceTag)
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
