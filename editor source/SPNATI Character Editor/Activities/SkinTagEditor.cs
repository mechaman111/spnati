using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Costume), 5)]
	public partial class SkinTagEditor : Activity
	{
		private Costume _skin;
		private Character _character;

		private AutoCompleteStringCollection _availableTags;

		public SkinTagEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get	{ return "Tags"; }
		}

		protected override void OnInitialize()
		{
			_skin = Record as Costume;
			_character = _skin?.Character;
			if (_character == null)
			{
				Enabled = false;
			}

			ColRemove.TrueValue = true;
		}

		protected override void OnFirstActivate()
		{
			if (_character == null) { return; }

			HashSet<string> tags = new HashSet<string>();
			foreach (Tag tag in TagDatabase.Dictionary.Tags)
			{
				tags.Add(tag.Key);
			}
			foreach (string tag in _character.Tags)
			{
				tags.Remove(tag);
			}
			_availableTags = new AutoCompleteStringCollection();
			foreach (string tag in tags)
			{
				_availableTags.Add(tag);
			}

			foreach (SkinTag tag in _skin.Tags)
			{
				if (!tag.Remove)
				{
					gridAdd.Rows.Add(new string[] { tag.Name });
				}
			}
		}

		protected override void OnActivate()
		{
			if (_character == null) { return; }
			gridRemove.Rows.Clear(); //rebuild to account for any changes to the main character
			foreach (string tag in _character.Tags)
			{
				DataGridViewRow row = gridRemove.Rows[gridRemove.Rows.Add()];
				row.Cells[0].Value = tag;
				row.Cells[1].Value = _skin.Tags.Find(t => t.Name == tag && t.Remove) != null;
			}
		}

		private void gridAdd_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			TextBox box = e.Control as TextBox;
			if (box != null)
			{
				box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				box.AutoCompleteSource = AutoCompleteSource.CustomSource;
				box.AutoCompleteCustomSource = _availableTags;
			}
		}

		public override void Save()
		{
			_skin.Tags.Clear();
			gridRemove.EndEdit();

			foreach (DataGridViewRow row in gridRemove.Rows)
			{
				string name = row.Cells[0].Value?.ToString();
				if (!string.IsNullOrEmpty(name))
				{
					SkinTag tag = new SkinTag(name);
					if ((bool)row.Cells[1].Value)
					{
						tag.Remove = true;
						_skin.Tags.Add(tag);
					}
				}
			}
			foreach (DataGridViewRow row in gridAdd.Rows)
			{
				string name = row.Cells[0].Value?.ToString();
				if (!string.IsNullOrEmpty(name))
				{
					_skin.Tags.Add(new SkinTag(name));
				}
			}

			_skin.Tags.Sort();
		}
	}
}
