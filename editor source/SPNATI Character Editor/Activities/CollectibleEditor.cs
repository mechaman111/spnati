using Desktop;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Desktop.Skinning;
using System.Collections.ObjectModel;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 70, DelayRun = true, Caption = "Collectibles")]
	public partial class CollectibleEditor : Activity
	{
		private Character _character;
		private ListViewItem _selectedItem;

		public CollectibleEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Collectibles";
			}
		}

		public override bool CanRun()
		{
			return !Config.SafeMode;
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			lstCollectibles.LargeImageList = new ImageList();
			lstCollectibles.LargeImageList.ImageSize = new Size(128, 128);
			lstCollectibles.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			lstCollectibles.LargeImageList.Images.Add("???", Properties.Resources.Achievement);
			table.Context = new CollectibleContext(_character, CharacterContext.Collectible);
		}

		protected override void OnActivate()
		{
			PopulateCollectibles();
			UpdateAddButton();
		}

		private void UpdateAddButton()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			int limit = TestRequirements.Instance.GetAllowedCollectibles(history.Current.TotalLines);
			tsAdd.Enabled = _character.Collectibles.Count < limit;
			if (!tsAdd.Enabled)
			{
				int thresholds = _character.Collectibles.Count - 2 + 1;
				int requirement = thresholds * 600 + 1;
				tsAdd.ToolTipText = $"You need {requirement} lines to be able to add another collectible.";
			}
			else
			{
				tsAdd.ToolTipText = "Add Collectible";
			}
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length > 0)
			{
				ValidationContext context = parameters[0] as ValidationContext;
				if (context != null)
				{
					for (int i = 0; i < lstCollectibles.Items.Count; i++)
					{
						Collectible c = lstCollectibles.Items[i].Tag as Collectible;
						if (c == context.Collectible)
						{
							lstCollectibles.Items[i].Selected = true;
							break;
						}
					}
				}
			}
		}

		private void PopulateCollectibles()
		{
			lstCollectibles.Items.Clear();
			foreach (Collectible c in _character.Collectibles.Collectibles)
			{
				AddCollectible(c, false);
			}
		}

		private void AddCollectible(Collectible c, bool select)
		{
			ListViewItem item = new ListViewItem(c.Title);
			item.Tag = c;
			Bitmap thumbnail = GetImage(c.Thumbnail);
			if (thumbnail != null)
			{
				if (!lstCollectibles.LargeImageList.Images.ContainsKey(c.Thumbnail))
				{
					lstCollectibles.LargeImageList.Images.Add(c.Thumbnail, thumbnail);
				}
				item.ImageKey = c.Thumbnail;
			}
			else
			{
				item.ImageKey = "???";
			}
			lstCollectibles.Items.Add(item);
			if (select)
			{
				item.Selected = true;
			}
			UpdateAddButton();
		}

		public static Bitmap GetImage(string src)
		{
			if (string.IsNullOrEmpty(src)) { return null; }
			Bitmap img = null;
			string path = Path.Combine(Config.SpnatiDirectory, src);
			if (!File.Exists(path))
			{
				return null;
			}
			try
			{
				using (Bitmap temp = new Bitmap(path))
				{
					img = new Bitmap(temp);
				}
			}
			catch { }
			return img;
		}

		private void tsAdd_Click(object sender, EventArgs e)
		{
			Collectible c = new Collectible()
			{
				Id = "new_collectible",
				Title = "New Collectible"
			};
			_character.Collectibles.Add(c);
			AddCollectible(c, true);
		}

		private void tsRemove_Click(object sender, EventArgs e)
		{
			if (lstCollectibles.SelectedItems.Count == 0) { return; }
			ListViewItem item = lstCollectibles.SelectedItems[0];
			Collectible collectible = item.Tag as Collectible;
			if (collectible != null && MessageBox.Show($"Are you sure you want to remove {collectible}? This cannot be undone.", "Remove Collectible", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				lstCollectibles.Items.Remove(item);
				_character.Collectibles.Remove(collectible);
				UpdateAddButton();
			}
		}

		private void lstCollectibles_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedItem != null)
			{
				table.Save();
			}

			if (lstCollectibles.SelectedItems.Count == 0)
			{
				_selectedItem = null;
				table.Data = null;
				return;
			}
			ListViewItem item = lstCollectibles.SelectedItems[0];
			_selectedItem = item;
			Collectible collectible = item.Tag as Collectible;
			table.Data = collectible;
			UpdatePreview();
		}

		public override void Save()
		{
			table.Save();
		}

		private void table_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (_selectedItem == null)
			{
				return;
			}
			if (e.PropertyName == "Title")
			{
				Collectible collectible = _selectedItem.Tag as Collectible;
				_selectedItem.Text = collectible.Title;
			}
			else if (e.PropertyName == "Thumbnail")
			{
				Collectible collectible = _selectedItem.Tag as Collectible;
				Bitmap thumbnail = GetImage(collectible.Thumbnail);
				if (thumbnail != null)
				{
					if (!lstCollectibles.LargeImageList.Images.ContainsKey(collectible.Thumbnail))
					{
						lstCollectibles.LargeImageList.Images.Add(collectible.Thumbnail, thumbnail);
					}
					_selectedItem.ImageKey = collectible.Thumbnail;
				}
				else
				{
					_selectedItem.ImageKey = "???";
				}
			}
			else if (e.PropertyName == "Image")
			{
				UpdatePreview();
			}
		}

		private void UpdatePreview()
		{
			Collectible collectible = _selectedItem.Tag as Collectible;
			if (collectible != null)
			{
				Bitmap bmp = GetImage(collectible.Image);
				picPreview.Image = bmp;
			}
			else
			{
				picPreview.Image = null;
			}
		}

		private void tsUp_Click(object sender, EventArgs e)
		{
			Collectible collectible = _selectedItem?.Tag as Collectible;
			if (collectible == null)
			{
				return;
			}
			ObservableCollection<Collectible> collectibles = _character.Collectibles.Collectibles;

			int index = collectibles.IndexOf(collectible);
			if (index == 0)
			{
				return;
			}
			collectibles.Remove(collectible);
			collectibles.Insert(index - 1, collectible);

			lstCollectibles.ListViewItemSorter = new CollectibleSorter(collectibles);
			lstCollectibles.Sort();
		}

		private void tsDown_Click(object sender, EventArgs e)
		{
			Collectible collectible = _selectedItem?.Tag as Collectible;
			if (collectible == null)
			{
				return;
			}
			ObservableCollection<Collectible> collectibles = _character.Collectibles.Collectibles;

			int index = collectibles.IndexOf(collectible);
			if (index == collectibles.Count - 1)
			{
				return;
			}
			collectibles.Remove(collectible);
			collectibles.Insert(index + 1, collectible);

			lstCollectibles.ListViewItemSorter = new CollectibleSorter(collectibles);
			lstCollectibles.Sort();
		}

		private class CollectibleSorter : IComparer
		{
			private ObservableCollection<Collectible> _list;
			public CollectibleSorter(ObservableCollection<Collectible> list)
			{
				_list = list;
			}

			public int Compare(object x, object y)
			{
				ListViewItem i1 = x as ListViewItem;
				ListViewItem i2 = y as ListViewItem;
				Collectible c1 = i1.Tag as Collectible;
				Collectible c2 = i2.Tag as Collectible;
				return _list.IndexOf(c1).CompareTo(_list.IndexOf(c2));
			}
		}

		protected override void OnSkinChanged(Skin skin)
		{
			base.OnSkinChanged(skin);
			lstCollectibles.BackColor = skin.FieldBackColor;
			lstCollectibles.ForeColor = skin.Surface.ForeColor;
		}
	}

	public class CollectibleContext : ICharacterContext
	{
		public ISkin Character { get; }
		public CharacterContext Context { get; }

		public CollectibleContext(ISkin character, CharacterContext context)
		{
			Character = character;
			Context = context;
		}
	}
}
