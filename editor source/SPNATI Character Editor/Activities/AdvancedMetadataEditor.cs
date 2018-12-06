using Desktop;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 60)]
	public partial class AdvancedMetadataEditor : Activity
	{
		private Character _character;

		public AdvancedMetadataEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Advanced";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			valScale.Value = Math.Max(valScale.Minimum, Math.Min((decimal)_character.Metadata.Scale, valScale.Maximum));
		}

		protected override void OnActivate()
		{
			LoadLabels();
		}

		public override void Save()
		{
			_character.Metadata.Scale = (float)valScale.Value;
			SaveLabels();
		}

		/// <summary>
		/// Populates the advanced labels grid
		/// </summary>
		private void LoadLabels()
		{
			gridLabels.Rows.Clear();
			foreach (StageSpecificValue i in _character.Labels)
			{
				DataGridViewRow row = gridLabels.Rows[gridLabels.Rows.Add()];
				row.Cells["ColLabelsStage"].Value = i.Stage;
				row.Cells["ColLabelsLabel"].Value = i.Value;
			}
		}

		/// <summary>
		/// Saves the Labels grid into the current character
		/// </summary>
		private void SaveLabels()
		{
			_character.Labels.Clear();
			for (int i = 0; i < gridLabels.Rows.Count; i++)
			{
				DataGridViewRow row = gridLabels.Rows[i];
				string label = row.Cells["ColLabelsLabel"].Value?.ToString();
				string stageString = row.Cells["ColLabelsStage"].Value?.ToString();
				if (string.IsNullOrEmpty(label))
					continue;
				stageString = stageString ?? string.Empty;
				int stage;
				if (int.TryParse(stageString, out stage))
				{
					_character.Labels.Add(new StageSpecificValue(stage, label));
				}
			}
		}
	}
}
