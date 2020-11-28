using Desktop;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 60, DelayRun = true, Caption = "Advanced")]
	public partial class AdvancedMetadataEditor : Activity
	{
		private Character _character;
		private bool _dirty;

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
			gridNicknames.Data = _character.Nicknames;
			LoadNicknames();
			styleControl.SetCharacter(_character);
			_character.PropertyChanged += _character_PropertyChanged;
		}

		private void _character_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Nicknames")
			{
				_dirty = true;
			}
		}

		protected override void OnActivate()
		{
			gridLabels.Data = _character.Labels;
		}

		public override void Save()
		{
			gridOtherNicknames.EndEdit();
			gridNicknames.EndEdit();
			_character.Metadata.Scale = (float)valScale.Value;
			_character.Metadata.BubblePosition = (DialogueLayer)cboDialogueLayer.SelectedItem;
			_character.Metadata.Z = (int)valLayer.Value;
			gridLabels.Save(colLabelsStage);
			SaveNicknames();
			styleControl.Save();
		}

		private void LoadNicknames()
		{
			gridNicknames.Rows.Clear();
			gridOtherNicknames.Rows.Clear();
			foreach (Nickname nickname in _character.Nicknames)
			{
				if (nickname.Character == "*")
				{
					DataGridViewRow row = gridOtherNicknames.Rows[gridOtherNicknames.Rows.Add(new object[] { nickname.Label })];
					row.Tag = nickname;
				}
				else
				{
					DataGridViewRow row = gridNicknames.Rows[gridNicknames.Rows.Add(new object[] { nickname.Character, nickname.Label })];
					row.Tag = nickname;
				}
			}
		}

		private void SaveNicknames()
		{
			for (int i = 0; i < gridNicknames.Rows.Count; i++)
			{
				DataGridViewRow row = gridNicknames.Rows[i];
				if (row.IsNewRow) { continue; }
				Nickname nickname = row.Tag as Nickname;
				string key = row.Cells[0].Value?.ToString();
				string value = row.Cells[1].Value?.ToString();
				if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
				{
					gridNicknames.Rows.RemoveAt(i);
					if (nickname != null)
					{
						_character.Nicknames.Remove(nickname);
					}
					i--;
					continue;
				}
				else
				{
					if (nickname == null)
					{
						nickname = new Nickname(key, value);
						_character.Nicknames.Add(nickname);
						row.Tag = nickname;
					}
					nickname.Character = key;
					nickname.Label = value;
				}
			}
			for (int i = 0; i < gridOtherNicknames.Rows.Count; i++)
			{
				DataGridViewRow row = gridOtherNicknames.Rows[i];
				if (row.IsNewRow) { continue; }
				string value = row.Cells[0].Value?.ToString();
				Nickname nickname = row.Tag as Nickname;
				if (string.IsNullOrEmpty(value))
				{
					gridOtherNicknames.Rows.RemoveAt(i);
					if (nickname != null)
					{
						_character.Nicknames.Remove(nickname);
					}
					i--;
				}
				else
				{
					if (nickname == null)
					{
						nickname = new Nickname("*", value);
						_character.Nicknames.Add(nickname);
						row.Tag = nickname;
					}
					nickname.Label = value;
				}
			}

			if (_dirty)
			{
				_character.Nicknames.Sort();
				_dirty = false;
			}
		}
	}
}
