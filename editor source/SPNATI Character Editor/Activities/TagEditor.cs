using Desktop;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 2)]
	public partial class TagEditor : Activity
	{
		private Character _character;
		private BindableTagList _bindings;

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
			foreach (CharacterTag tag in _character.Tags.Where((value) =>
			{
				Tag t = dictionary.GetTag(value.Tag);
				return !dictionary.IsPairedTag(value.Tag) || t != null;
			}))
			{
				tags.Add(tag.Tag);
			}

			_bindings = new BindableTagList(_character);

			foreach (Tag tag in dictionary.Tags)
			{
				_bindings.Add(tag.Value);
			}

			tagList.SetData(_bindings, _character);

			tagGrid.SetCharacter(_character, _bindings);
			tagGrid.Visible = false;

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
					TreeNode node = toc.Nodes.Add(group.Label);
					node.Tag = group;
				}
			}

			if (toc.Nodes.Count > 0)
			{
				toc.SelectedNode = toc.Nodes[0];
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
			_bindings.SaveIntoCharacter();
		}

		private void toc_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = toc.SelectedNode;
			TagGroup group = node.Tag as TagGroup;
			tagGrid.SetGroup(group);
			tagGrid.Visible = true;
		}
	}
}
