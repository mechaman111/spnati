using Desktop;
using SPNATI_Character_Editor.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 1)]
	public partial class TagEditor : Activity
	{
		private Character _character;

		public TagEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Tags"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			LoadTags();
		}

		/// <summary>
		/// Populates the Tags grid with the character's tags
		/// </summary>
		private void LoadTags()
		{
			//Filter out grouper tags that aren't directly selectable
			TagDictionary dictionary = TagDatabase.Dictionary;
			List<string> tags = new List<string>();
			tags.AddRange(_character.Tags.Where((value) => {
				Tag tag = dictionary.GetTag(value);
				return !dictionary.IsPairedTag(value) || tag != null;
			}));

			//Fill the tag group
			string gender = _character.Gender;
			foreach (TagGroup group in dictionary.Groups)
			{
				if (group.Hidden)
				{
					continue;
				}
				if (string.IsNullOrEmpty(group.Gender) || group.Gender == gender)
				{
					TagControl container = new TagControl();
					container.SetGroup(group, _character);
					flowPanel.Controls.Add(container);
					container.CheckTags(tags);
				}
			}

			//Put any ungrouped tags into the misc grid
			gridTags.Rows.Clear();
			foreach (string tag in tags)
			{
				gridTags.Rows.Add(tag);
				//DataGridViewRow row = gridTags.Rows[gridTags.Rows.Add()];
				//row.Cells[0].Value = tag;
			}
		}

		public override void Save()
		{
			SaveTags();
		}

		/// <summary>
		/// Saves the Tags grid into the current character
		/// </summary>
		private void SaveTags()
		{
			_character.Tags.Clear();
			foreach (TagControl ctl in flowPanel.Controls)
			{
				_character.Tags.AddRange(ctl.GetTags());
			}

			for (int i = 0; i < gridTags.Rows.Count; i++)
			{
				DataGridViewRow row = gridTags.Rows[i];
				object value = row.Cells[0].Value;
				if (value == null)
					continue;
				string tag = value.ToString();
				_character.Tags.Add(tag);
			}
		}
	}
}
