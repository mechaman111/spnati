using Desktop;
using SPNATI_Character_Editor.DataStructures;
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
			
			cboDialogueLayer.Items.AddRange(Enum.GetValues(typeof(DialogueLayer)));
			ColCharacter.RecordType = typeof(Character);
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
			valLayer.Value = _character.Metadata.Z;
			cboDialogueLayer.SelectedItem = _character.Metadata.BubblePosition;
			LoadNicknames();
			styleControl.SetCharacter(_character);
		}

		protected override void OnActivate()
		{
			LoadLabels();
		}

		public override void Save()
		{
			_character.Metadata.Scale = (float)valScale.Value;
			_character.Metadata.BubblePosition = (DialogueLayer)cboDialogueLayer.SelectedItem;
			_character.Metadata.Z = (int)valLayer.Value;
			SaveLabels();
			SaveNicknames();
			styleControl.Save();
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

		private void LoadNicknames()
		{
			gridNicknames.Rows.Clear();
			gridOtherNicknames.Rows.Clear();
			foreach (Nickname nickname in _character.Nicknames)
			{
				if (nickname.Character == "*")
				{
					gridOtherNicknames.Rows.Add(new object[] { nickname.Label });
				}
				else
				{
					gridNicknames.Rows.Add(new object[] { nickname.Character, nickname.Label });
				}
			}
		}

		private void SaveNicknames()
		{
			_character.Nicknames.Clear();
			foreach (DataGridViewRow row in gridNicknames.Rows)
			{
				string key = row.Cells[0].Value?.ToString();
				string value = row.Cells[1].Value?.ToString();
				if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) { continue; }

				Nickname name = new Nickname(key, value);
				_character.Nicknames.Add(name);
			}
			foreach (DataGridViewRow row in gridOtherNicknames.Rows)
			{
				string value = row.Cells[0].Value?.ToString();
				if (string.IsNullOrEmpty(value)) { continue; }

				Nickname name = new Nickname("*", value);
				_character.Nicknames.Add(name);
			}

			_character.Nicknames.Sort();
		}
	}
}
