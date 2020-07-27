using Desktop;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.DataStructures;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(CaseTemplateRecord), 0)]
	public partial class CaseTemplateEditor : Activity
	{
		private CaseDefinition _definition;
		private CaseGroup _group;

		public CaseTemplateEditor()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			splitGroup.Panel2.Enabled = false;
			splitTemplate.Panel2.Enabled = false;
			recGroup.RecordType = typeof(CaseGroup);

			foreach (TriggerDefinition trigger in TriggerDatabase.Triggers)
			{
				if (trigger.Key == "-")
				{
					continue;
				}
				cboTag.Items.Add(trigger);
			}

			foreach (CaseDefinition def in CaseDefinitionDatabase.Definitions)
			{
				if (def.IsCore) { continue; }
				lstTemplates.Items.Add(def);
			}

			foreach (CaseGroup group in CaseDefinitionDatabase.Groups)
			{
				if (group.IsCore) { continue; }
				lstGroups.Items.Add(group);
			}

			if (lstTemplates.Items.Count > 0)
			{
				lstTemplates.SelectedIndex = 0;
			}
		}

		public override string Caption
		{
			get { return "Template Editor"; }
		}

		private void cboTag_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_definition == null) { return; }
			TriggerDefinition trigger = cboTag.SelectedItem as TriggerDefinition;
			if (_definition.Case.Tag == trigger.Tag) { return; }
			tableConditions.Save();
			_definition.Case.Tag = trigger.Tag;
			ReloadTable();
		}

		private void tsAdd_Click(object sender, System.EventArgs e)
		{
			CaseDefinition definition = CaseDefinitionDatabase.AddDefinition();
			lstTemplates.Items.Add(definition);
			lstTemplates.SelectedItem = definition;
		}

		private void tsRemove_Click(object sender, System.EventArgs e)
		{
			if (_definition == null || _definition.IsCore)
			{
				return;
			}
			CaseDefinitionDatabase.RemoveDefinition(_definition);
			lstTemplates.Items.Remove(_definition);
			if (lstTemplates.Items.Count > 0)
			{
				lstTemplates.SelectedIndex = 0;
			}
		}

		private void lstTemplates_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CaseDefinition definition = lstTemplates.SelectedItem as CaseDefinition;
			if (_definition == definition)
			{
				return;
			}
			
			if (!TrySave())
			{
				return;
			}

			_definition = definition;
			if (definition == null)
			{
				splitTemplate.Panel2.Enabled = false;
			}
			else
			{
				ReloadTable();
				splitTemplate.Panel2.Enabled = true;
				TriggerDefinition trigger = definition.GetTrigger();

				cboTag.SelectedItem = trigger;
				txtLabel.Text = definition.Label;
				recGroup.RecordKey = definition.Group.ToString();

				gridLines.Rows.Clear();
				foreach (string line in _definition.Lines)
				{
					gridLines.Rows.Add(line);
				}
			}
		}

		private void ReloadTable()
		{
			tableConditions.Data = null;
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(_definition.Case.Tag);
			if (trigger == null || trigger.HasTarget)
			{
				tableConditions.RecordFilter = null;
			}
			else
			{
				tableConditions.RecordFilter = FilterTargets;
			}
			tableConditions.Data = _definition.Case;
			CaseControl.AddSpeedButtons(tableConditions, _definition.Case.Tag);
		}

		private bool FilterTargets(IRecord record)
		{
			return record.Group != "Target";
		}

		private bool TrySave()
		{
			if (tabs.SelectedTab == tabTemplates)
			{
				bool saved = SaveDefinition();
				if (!saved && _definition != null)
				{
					if (_definition.IsCore)
					{
						MessageBox.Show($"{_definition} is invalid. Conditions are not allowed for this template.");
					}
					else
					{
						MessageBox.Show($"{_definition} is invalid. You must add at least one condition.");
					}
					lstTemplates.SelectedItem = _definition;
					return false;
				}
			}
			return true;
		}

		public override bool CanDeactivate(DeactivateArgs args)
		{
			return TrySave();
		}

		public override bool CanQuit(CloseArgs args)
		{
			return TrySave();
		}

		public override void Save()
		{
			if (tabs.SelectedTab == tabGroups)
			{
				SaveGroup();
			}
		}

		public override void Quit()
		{
			CaseDefinitionDatabase.Save();
		}

		private bool SaveDefinition()
		{
			if (_definition != null)
			{
				if (!string.IsNullOrEmpty(txtLabel.Text))
				{
					_definition.Label = txtLabel.Text;
					_definition.SafeLabel = null;
				}
				string group = recGroup.RecordKey;
				int groupId;
				if (!string.IsNullOrEmpty(group) && int.TryParse(group, out groupId))
				{
					_definition.Group = groupId;
				}

				tableConditions.Save();

				bool hasConditions = _definition.Case.HasConditions;
				if (_definition.IsCore && hasConditions || !_definition.IsCore && !hasConditions)
				{
					return false;
				}

				gridLines.CommitEdit(DataGridViewDataErrorContexts.Commit);
				_definition.Lines.Clear();
				foreach (DataGridViewRow row in gridLines.Rows)
				{
					string text = row.Cells[0].Value?.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						_definition.Lines.Add(text);
					}
				}
				CaseDefinitionDatabase.UpdateDefinition(_definition);

				lstTemplates.DrawMode = DrawMode.OwnerDrawFixed;
				lstTemplates.DrawMode = DrawMode.Normal;
			}
			return true;
		}

		private void lstGroups_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SaveGroup();
			_group = lstGroups.SelectedItem as CaseGroup;
			splitGroup.Panel2.Enabled = _group != null;
			if (_group != null)
			{
				txtGroupLabel.Text = _group.Label;
			}
		}

		private void SaveGroup()
		{
			if (_group == null)
			{
				return;
			}

			if (!string.IsNullOrEmpty(txtGroupLabel.Text))
			{
				_group.Label = txtGroupLabel.Text;
			}

			lstGroups.DrawMode = DrawMode.OwnerDrawFixed;
			lstGroups.DrawMode = DrawMode.Normal;
		}

		private void tsAddGroup_Click(object sender, System.EventArgs e)
		{
			CaseGroup grp = CaseDefinitionDatabase.AddGroup("New Group");
			lstGroups.Items.Add(grp);
			lstGroups.SelectedItem = grp;
		}

		private void tsRemoveGroup_Click(object sender, System.EventArgs e)
		{
			if (_group == null || _group.IsCore)
			{
				return;
			}
			List<CaseDefinition> conflicts = new List<CaseDefinition>();
			foreach (CaseDefinition def in CaseDefinitionDatabase.Definitions)
			{
				if (def.Group == _group.Id)
				{
					conflicts.Add(def);
				}
			}

			if (conflicts.Count > 0)
			{
				MessageBox.Show("This group cannot be deleted because the following templates belong to it:\r\n\r\n" + string.Join("\r\n", conflicts), "Remove Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			CaseDefinitionDatabase.RemoveGroup(_group);
			lstGroups.Items.Remove(_group);
		}

		private void tabs_Deselecting(object sender, TabControlCancelEventArgs e)
		{
			if (tabs.SelectedTab == tabTemplates)
			{
				if (!TrySave())
				{
					e.Cancel = true;
				}
			}
			else
			{
				SaveGroup();
			}
		}
	}

	public class CaseTemplateRecord : BasicRecord
	{
		public CaseTemplateRecord()
		{
			Name = "Case Templates";
			Key = "Case Templates";
		}
	}

	public class CaseTemplateProvider : BasicProvider<CaseTemplateRecord>
	{
	}
}
