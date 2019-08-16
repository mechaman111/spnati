using Desktop;
using SPNATI_Character_Editor.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Recipe), 0)]
	public partial class RecipeEditor : Activity
	{
		private Recipe _recipe;

		public RecipeEditor()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_recipe = Record as Recipe;
			txtName.Text = _recipe.Name;
			txtDescription.Text = _recipe.Description;
			txtFile.Text = _recipe.FileName;
			txtGroup.Text = _recipe.Group;
			txtLabel.Text = _recipe.Label;
			cboTag.Items.AddRange(TriggerDatabase.Triggers);

			for (int i = 0; i < TriggerDatabase.Triggers.Count; i++)
			{
				if (_recipe.Case.Tag == TriggerDatabase.Triggers[i].Tag)
				{
					cboTag.SelectedIndex = i;
					break;
				}
			}

			ReloadTable();
			cboTag.SelectedIndexChanged += cboTag_SelectedIndexChanged;
			gridLines.SetData(null, _recipe.Case);
		}

		private void ReloadTable()
		{
			tableConditions.Data = null;
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(_recipe.Case.Tag);
			if (trigger == null || trigger.HasTarget)
			{
				tableConditions.RecordFilter = null;
			}
			else
			{
				tableConditions.RecordFilter = FilterTargets;
			}
			tableConditions.Data = _recipe.Case;
			CaseControl.AddSpeedButtons(tableConditions, _recipe.Case.Tag);
		}

		private bool FilterTargets(IRecord record)
		{
			return record.Group != "Target";
		}

		public override void Save()
		{
			_recipe.Name = txtName.Text;
			_recipe.Description = txtDescription.Text;
			_recipe.FileName = txtFile.Text;
			_recipe.Group = txtGroup.Text;
			_recipe.Label = txtLabel.Text;
			tableConditions.Save();
			gridLines.Save();

			string file = _recipe.GetFilePath();
			string json = Json.Serialize(_recipe);
			try
			{
				File.WriteAllText(file, json);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to save recipe: " + ex.Message);
			}
		}

		private void cboTag_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TriggerDefinition trigger = cboTag.SelectedItem as TriggerDefinition;
			if (_recipe.Case.Tag == trigger.Tag) { return; }
			tableConditions.Save();
			_recipe.Case.Tag = trigger.Tag;
			ReloadTable();
		}

		private void cmdOpen_Click(object sender, System.EventArgs e)
		{
			Save();
			string directory = Path.GetDirectoryName(_recipe.GetFilePath());
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					Arguments = directory,
					FileName = "explorer.exe"
				};

				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
