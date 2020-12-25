using Desktop.Skinning;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace SPNATI_Character_Editor.Controls
{
	public partial class TagGrid : UserControl, ISkinControl
	{
		private const int CellSize = 30;
		private const int HeaderPadding = 10;

		private Pen _border = new Pen(Color.DarkGray);

		private BindableTagList _bindings;
		private ISkin _skin;
		private Character _character;
		private int _layerCount;
		private List<Tag> _tags = new List<Tag>();
		private TagGroup _group;
		private Point _highlightedCell = new Point(-1, -1);

		private int _headerHeight = 150;
		public int ColumnHeaderHeight
		{
			get { return _headerHeight; }
			set { _headerHeight = value; ResizeGrid(); }
		}

		private int _headerWidth = 100;
		public int RowHeaderWidth
		{
			get { return _headerWidth; }
			set { _headerWidth = value; ResizeGrid(); }
		}

		public TagGrid()
		{
			InitializeComponent();
		}

		public void OnUpdateSkin(Skin skin)
		{
			_border = skin.PrimaryColor.GetBorderPen(VisualState.Normal, false, Enabled);
		}

		private void ResizeGrid()
		{
			int rows = _tags.Count;
			int cols = _layerCount + 1;
			Width = cols * CellSize + _headerWidth + _headerHeight;
			Height = rows * CellSize + _headerHeight;
			lblGroup.Size = new Size(_headerWidth, _headerHeight);
			RecreateHandle();
		}

		public void SetCharacter(ISkin skin, BindableTagList binding)
		{
			_skin = skin;
			_character = skin.Character;
			_layerCount = _character.Layers + Clothing.ExtraStages;
			if (_bindings != null)
			{
				_bindings.TagAdded -= _bindings_TagAdded;
				_bindings.TagRemoved -= _bindings_TagAdded;
				_bindings.TagModified -= _bindings_TagAdded;
			}
			_bindings = binding;
			_bindings.TagAdded += _bindings_TagAdded;
			_bindings.TagRemoved += _bindings_TagAdded;
			_bindings.TagModified += _bindings_TagAdded;
			ResizeGrid();
		}

		private void _bindings_TagAdded(object sender, BindableTag e)
		{
			panel.Invalidate();
		}

		public void SetGroup(TagGroup group)
		{
			_group = group;
			lblGroup.Text = _group.Label.Replace("/", "/\n");
			_tags.Clear();
			_tags.AddRange(group.Tags);
			ResizeGrid();
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

			Skin skin = SkinManager.Instance.CurrentSkin;

			if (_highlightedCell.X != -1)
			{
				using (SolidBrush lineBrush = new SolidBrush(Color.FromArgb(100, skin.SecondaryLightColor.Normal)))
				{
					g.FillRectangle(lineBrush, 0, _headerHeight + _highlightedCell.Y * CellSize, _headerWidth + _layerCount * CellSize + CellSize, CellSize);
					g.FillRectangle(lineBrush, _headerWidth + _highlightedCell.X * CellSize, _headerHeight, CellSize, _tags.Count * CellSize);
				}
				using (SolidBrush pointBrush = new SolidBrush(Color.FromArgb(127, skin.SecondaryColor.Normal)))
				{
					g.FillRectangle(pointBrush, _headerWidth + _highlightedCell.X * CellSize, _headerHeight + _highlightedCell.Y * CellSize, CellSize, CellSize);
				}
			}

			Image applyIcon = Properties.Resources.ApplyCheckbox;
			Image check = Properties.Resources.Checkmark;
			Image disabledCheck = Properties.Resources.CheckmarkDisabled;
			for (int layer = 0; layer < _layerCount + 1; layer++)
			{
				for (int row = 0; row < _tags.Count; row++)
				{
					BindableTag tag = _bindings.Get(_tags[row].Value);
					if (layer == 0)
					{
						g.DrawImage(applyIcon, _headerWidth + layer * CellSize + CellSize / 2 - applyIcon.Width / 2, _headerHeight + row * CellSize + CellSize / 2 - applyIcon.Height / 2, applyIcon.Width, applyIcon.Height);
					}
					else
					{
						if (tag.HasStage(layer - 1))
						{
							string error;
							g.DrawImage(CanDeselect(tag.Tag, layer - 1, out error) ? check : disabledCheck, _headerWidth + layer * CellSize + CellSize / 2 - check.Width / 2, _headerHeight + row * CellSize + CellSize / 2 - check.Height / 2, check.Width, check.Height);
						}
					}
				}
			}

			using (Brush fontBrush = new SolidBrush(skin.Background.ForeColor))
			{
				g.DrawLine(_border, 0, _headerHeight, 0, Height);
				g.DrawLine(_border, _headerWidth + _headerHeight, 0, Width, 0);
				for (int i = 0; i < _tags.Count; i++)
				{
					int y = _headerHeight + CellSize * i;

					g.DrawString(_tags[i].DisplayName, Skin.TextFont, fontBrush, new Rectangle(HeaderPadding, y + 1, _headerWidth - HeaderPadding * 2, CellSize), sf);

					g.DrawLine(_border, 0, y, Width - _headerHeight, y);
				}
				g.DrawLine(_border, 0, Height - 1, Width - _headerHeight, Height - 1);

				for (int i = 0; i < _layerCount + 1; i++)
				{
					int x = _headerWidth + CellSize * i;
					g.DrawLine(_border, x, _headerHeight, x, Height);
					g.DrawLine(_border, x, _headerHeight, x + _headerHeight, 0);
				}
				g.DrawLine(_border, Width - _headerHeight, _headerHeight, Width - _headerHeight, Height);
				g.DrawLine(_border, Width - _headerHeight, _headerHeight, Width, 0);

				//Column labels
				for (int i = 0; i < _layerCount + 1; i++)
				{
					string label = i == 0 ? "Select All" : GetLayerName(i - 1);

					int x = _headerWidth + CellSize * i;
					g.TranslateTransform(x + 10, _headerHeight - 15); //fudging some numbers empirically to make it line up nice
					g.RotateTransform(-45);

					g.DrawString(label, Font, fontBrush, new Rectangle(0, 0, _headerHeight, CellSize), sf);

					g.ResetTransform();
				}
			}
		}

		private string GetLayerName(int layer)
		{
			StageName stage = _character.LayerToStageName(layer, _skin);
			return stage.DisplayName;
		}

		private void panel_MouseMove(object sender, MouseEventArgs e)
		{
			int x = (e.X - _headerWidth) / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;
			Point oldPt = _highlightedCell;
			if (x >= 0 && x < _layerCount + 1 && y >= 0 && y < _tags.Count)
			{
				_highlightedCell.X = x;
				_highlightedCell.Y = y;

				int stage = x - 1;
				Tag tag = _tags[y];
				BindableTag cell = _bindings.Get(tag.Value);
				string error;
				if (stage >= 0 && cell.HasStage(stage) && !CanDeselect(tag.Value, stage, out error))
				{
					toolTip1.Show($"Cannot deselect due to: {error}", panel, new Point(e.X + 5, e.Y + 5));
					Cursor = Cursors.No;
				}
				else if (x == 0)
				{
					bool allowed = false;
					for (int i = 0; i < _layerCount; i++)
					{
						if (CanDeselect(tag.Value, i, out error))
						{
							allowed = true;
							Cursor = Cursors.Hand;
							break;
						}
					}
					if (!allowed)
					{
						Cursor = Cursors.No;
					}
				}
				else
				{
					toolTip1.Hide(panel);
					Cursor = Cursors.Hand;
				}
			}
			else
			{
				if (e.X < _headerWidth && y >= 0 && y < _tags.Count)
				{
					Tag tag = _tags[y];
					toolTip1.Show(tag.Description, panel, new Point(e.X + 5, e.Y + 5));
				}
				else
				{
					toolTip1.Hide(panel);
				}

				_highlightedCell.X = -1;
				_highlightedCell.Y = -1;
				Cursor = Cursors.Default;
			}
			if (_highlightedCell.X != oldPt.X || _highlightedCell.Y != oldPt.Y)
			{
				panel.Invalidate();
			}
		}

		private void panel_MouseLeave(object sender, System.EventArgs e)
		{
			toolTip1.Hide(panel);
			if (_highlightedCell.X >= 0)
			{
				_highlightedCell.X = -1;
				_highlightedCell.Y = -1;
				Cursor = Cursors.Default;
				panel.Invalidate();
			}
		}

		/// <summary>
		/// Gets whether a tag can be deselected.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		private bool CanDeselect(string tag, int stage, out string preventer)
		{
			Tag definition = TagDatabase.GetTag(tag);
			if (definition != null)
			{
				foreach (string child in definition.ChildrenTags)
				{
					BindableTag childBindable = _bindings.Get(child);
					if (childBindable != null)
					{
						if (childBindable.HasStage(stage))
						{
							Tag childTag = TagDatabase.GetTag(child);
							string group = "";
							if (childTag != null && !string.IsNullOrWhiteSpace(childTag.Group))
							{
								group = childTag.Group + " - ";
							}
							preventer = $"{group}{childTag?.DisplayName ?? child}";
							return false;
						}
					}
				}
			}
			preventer = null;
			return true;
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			int x = (e.X - _headerWidth) / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;
			if (x >= 0 && x < _layerCount + 1 && y >= 0 && y <= _tags.Count)
			{
				BindableTag tag = _bindings.Get(_tags[y].Value);
				if (x == 0)
				{
					bool allSelected = true;
					for (int i = 0; i < _layerCount; i++)
					{
						if (!tag.HasStage(i))
						{
							allSelected = false;
						}
					}
					for (int i = 0; i < _layerCount; i++)
					{
						if (allSelected)
						{
							string error;
							if (CanDeselect(tag.Tag, i, out error))
							{
								tag.RemoveStage(i);
							}
						}
						else
						{
							tag.AddStage(i);
						}
					}
				}
				else
				{
					if (tag.HasStage(x - 1))
					{
						string error;
						if (!CanDeselect(tag.Tag, x - 1, out error))
						{
							return;
						}
						tag.RemoveStage(x - 1);
					}
					else
					{
						tag.AddStage(x - 1);
					}
				}
				panel.Invalidate();
			}
		}
	}
}
