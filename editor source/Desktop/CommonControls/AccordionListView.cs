using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class AccordionListView : UserControl, ISkinControl
	{
		private StringFormat _sf = new StringFormat() { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
		private bool _resizing;
		private bool _suspendFormatting;

		private List<AccordionColumn> _columns = new List<AccordionColumn>();
		private HashSet<ListViewItem> _refreshedItems = new HashSet<ListViewItem>();
		private IGroupedList _source;

		public event EventHandler<EventArgs> SelectedIndexChanged;
		public event EventHandler<FormatRowEventArgs> FormatRow;
		public event EventHandler<FormatGroupEventArgs> FormatGroup;
		public event EventHandler<AccordionListViewEventArgs> RightClick;

		public bool ShowIndicators { get; set; }

		private object _movingObject;
		private bool _directSelection;

		public AccordionListView()
		{
			InitializeComponent();

			Type lv = view.GetType();
			PropertyInfo pi = lv.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
			pi.SetValue(view, true);
			view.ColumnWidthChanged += View_ColumnWidthChanged;
			view.KeyDown += View_KeyDown;
		}

		private void View_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		public void OnUpdateSkin(Skin skin)
		{
			view.BackColor = skin.FieldBackColor;
		}

		public void ClearColumns()
		{
			_columns.Clear();
		}
		public void AddColumn(AccordionColumn column)
		{
			_columns.Add(column);
		}

		public void RebuildColumns()
		{
			view.Columns.Clear();
			foreach (AccordionColumn c in _columns)
			{
				ColumnHeader col = new ColumnHeader();
				col.Text = c.Text;
				col.Tag = c;
				col.Width = c.Width;
				col.TextAlign = c.TextAlign;
				view.Columns.Add(col);
			}
			ResizeColumns();
		}

		public IGroupedList DataSource
		{
			get { return _source; }
			set
			{
				if (_source != value)
				{
					//scroll back to the top or visual oddities happen with the virtual list
					if (view.Items.Count > 0)
					{
						_suspendFormatting = true;
						view.TopItem = view.Items[0];
						_suspendFormatting = false;
					}

					if (_source != null)
					{
						_source.BeforeMovingItem -= _source_BeforeMovingItem;
						_source.AfterMovingItem -= _source_AfterMovingItem;
						_source.ListChanged -= _source_ListChanged;
					}
					_source = value;
					if (_source == null)
					{
						view.VirtualListSize = 0;
					}
					else
					{
						view.VirtualListSize = _source.Count;
						_source.BeforeMovingItem += _source_BeforeMovingItem;
						_source.AfterMovingItem += _source_AfterMovingItem;
						_source.ListChanged += _source_ListChanged;
					}
				}
			}
		}

		private void _source_BeforeMovingItem(object sender, GroupedListMovingEventArgs e)
		{
			object selected = SelectedItem;
			if (selected == e.Item)
			{
				_movingObject = selected;
			}
		}

		private void _source_AfterMovingItem(object sender, GroupedListMovingEventArgs e)
		{
			if (_movingObject != null)
			{
				SelectedItem = _movingObject;
				_movingObject = null;
			}
		}

		private void _source_ListChanged(object sender, GroupedListChangedEventArgs e)
		{
			switch (e.Action)
			{
				case GroupedListChangedAction.Add:
					if (e.Index == -1) { return; }
					int currentItemIndex = _source.GetIndex(SelectedItem);
					view.VirtualListSize++;
					if (currentItemIndex == e.Index)
					{
						//if adding into the same index as the selected item, quietly reselect the current item
						view.SelectedIndices.Clear();
						view.SelectedIndices.Add(e.Index + 1);
					}
					break;
				case GroupedListChangedAction.Remove:
					if (e.Index == -1) { return; }
					view.VirtualListSize--;
					View_SelectedIndexChanged(this, EventArgs.Empty);
					break;
				case GroupedListChangedAction.Modify:
					if (e.Index == -1) { return; }
					AccordionListViewItem listItem = view.Items[e.Index] as AccordionListViewItem;
					view.BeginUpdate();
					UpdateRow(listItem, e.Item);
					view.EndUpdate();
					break;
				case GroupedListChangedAction.Clear:
					view.VirtualListSize = 0;
					break;
				case GroupedListChangedAction.GroupToggled:
					view.VirtualListSize = _source.Count;
					break;
			}
		}

		private void View_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			GroupedListItem groupItem = _source.GetItem(e.ItemIndex);
			AccordionListViewItem item = new AccordionListViewItem(groupItem);
			item.Tag = groupItem.Data;
			UpdateRow(item, groupItem.Data);
			e.Item = item;
		} 

		private void UpdateRow(AccordionListViewItem item, object model)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Type type = model.GetType();

			GroupedListGrouper grouper = model as GroupedListGrouper;
			if (grouper != null)
			{
				//Grouper
				FormatGroupEventArgs args = new FormatGroupEventArgs(grouper, new Font(view.Font, FontStyle.Bold));
				args.ForeColor = skin.PrimaryForeColor;
				if (!_suspendFormatting)
				{
					FormatGroup?.Invoke(this, args);
				}
				item.Text = args.Label;
				while (item.SubItems.Count < _columns.Count)
				{
					item.SubItems.Add("");
				}
				item.ForeColor = Enabled ? args.ForeColor : skin.PrimaryColor.DisabledForeColor;
				item.Font = args.Font;
			}
			else
			{
				//Item
				FormatRowEventArgs args = new FormatRowEventArgs(model, item.Item.Group);
				args.GrouperColor = item.GrouperColor;
				args.ForeColor = skin.Surface.ForeColor;
				if (!_suspendFormatting)
				{
					FormatRow?.Invoke(this, args);
				}

				item.ForeColor = Enabled ? args.ForeColor : skin.Surface.DisabledForeColor;
				item.GrouperColor = args.GrouperColor;
				item.Font = view.Font;
				item.ToolTipText = args.Tooltip;

				for (int i = 0; i < _columns.Count; i++)
				{
					string propName = _columns[i].PropertyName;
					string value = "";
					if (string.IsNullOrEmpty(propName))
					{
						value = "Unbound";
					}
					else
					{
						MemberInfo mi = PropertyTypeInfo.GetMemberInfo(type, propName);
						if (mi != null)
						{
							value = mi.GetValue(model)?.ToString() ?? "";
						}
					}
					if (i == 0)
					{
						item.Text = value;
					}
					else
					{
						if (item.SubItems.Count > i)
						{
							item.SubItems[i].Text = value;
						}
						else
						{
							item.SubItems.Add(value);
						}
					}
				}
			}
		}

		private void View_SizeChanged(object sender, EventArgs e)
		{
			ResizeColumns();
		}

		private void ResizeColumns()
		{
			if (_resizing) { return; }
			_resizing = true;

			float totalWeight = 0;
			float usedWidth = 0;
			foreach (ColumnHeader col in view.Columns)
			{
				AccordionColumn c = col.Tag as AccordionColumn;
				totalWeight += c.FillWeight;
				if (c.FillWeight == 0)
				{
					usedWidth += c.Width;
				}
			}

			for (int i = 0; i < view.Columns.Count; i++)
			{
				AccordionColumn c = view.Columns[i].Tag as AccordionColumn;
				if (c.FillWeight > 0)
				{
					float percent = c.FillWeight / totalWeight;
					view.Columns[i].Width = Math.Max(20, (int)(percent * (view.ClientRectangle.Width - usedWidth)));
				}
			}

			_resizing = false;
		}

		private void View_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_movingObject == null)
			{
				if (_directSelection)
				{
					tmrTick.Enabled = false;
					SelectedIndexChanged?.Invoke(this, e);
				}
				else
				{
					tmrTick.Enabled = true;
				}
			}
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			tmrTick.Enabled = false;
			SelectedIndexChanged?.Invoke(this, e);
		}

		public object SelectedItem
		{
			get
			{
				return view.SelectedIndices.Count > 0 ? view.Items[view.SelectedIndices[0]].Tag : null;
			}
			set
			{
				if (_source == null || value == SelectedItem) { return; }
				_directSelection = true;
				if (value != null)
				{
					_source.ExpandTo(value);
					int index = _source.GetIndex(value);
					if (index >= 0)
					{
						view.Items[index].Selected = true;
						view.EnsureVisible(index);
					}
				}
				else
				{
					if (view.SelectedIndices.Count > 0)
					{
						view.Items[view.SelectedIndices[0]].Selected = false;
					}
				}
				_directSelection = false;
			}
		}

		public void ExpandAll()
		{
			_source?.ExpandAll();
		}

		public void CollapseAll()
		{
			GroupedListItem item = null;
			if (view.SelectedIndices.Count > 0)
			{
				item = _source.GetItem(this.view.SelectedIndices[0]);
			}
			_source?.CollapseAll();
			if (item != null)
			{
				SelectedItem = item.Group;
			}
		}

		private void View_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			SkinManager manager = SkinManager.Instance;
			Skin skin = manager.CurrentSkin;
			Graphics g = e.Graphics;
			SolidBrush back = skin.Surface.GetBrush(VisualState.Normal, false, true);
			g.FillRectangle(back, e.Bounds);
			SolidBrush fore = manager.GetBrush(skin.Surface.ForeColor);
			Rectangle textRect = new Rectangle(e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height);
			g.DrawString(e.Header.Text, Font, fore, textRect, _sf);
			Pen border = skin.Surface.GetBorderPen(VisualState.Normal, false, true);
			g.DrawLine(border, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
			if (e.ColumnIndex > 0)
			{
				g.DrawLine(border, e.Bounds.X, e.Bounds.Y, e.Bounds.X, e.Bounds.Bottom - 2);
			}
		}

		private void View_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			return;
		}

		private void View_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			Graphics g = e.Graphics;
			SkinManager manager = SkinManager.Instance;
			Skin skin = manager.CurrentSkin;
			if (e.Item.Selected && e.ColumnIndex == 0)
			{
				g.FillRectangle(skin.PrimaryColor.GetBrush(VisualState.Focused, false, Enabled), e.Item.Bounds);
				e.DrawFocusRectangle(e.Item.Bounds);
			}

			using (StringFormat sf = new StringFormat())
			{
				// Store the column text alignment, letting it default
				// to Left if it has not been set to Center or Right.
				switch (e.Header.TextAlign)
				{
					case HorizontalAlignment.Center:
						sf.Alignment = StringAlignment.Center;
						break;
					case HorizontalAlignment.Right:
						sf.Alignment = StringAlignment.Far;
						break;
				}
				sf.FormatFlags = StringFormatFlags.NoWrap;
				sf.Trimming = StringTrimming.EllipsisCharacter;

				AccordionListViewItem item = e.Item as AccordionListViewItem;
				int depthShift = 0;
				if (item.Tag is GroupedListGrouper)
				{
					int depth = ((GroupedListGrouper)item.Tag).Depth;
					depthShift += Properties.Resources.Collapse.Width * depth;
				}
				Rectangle itemBounds = new Rectangle(e.Bounds.X + 4 + depthShift, e.Bounds.Y + 1, e.Bounds.Width - 4 - depthShift, e.Bounds.Height);

				Rectangle bounds = e.Bounds;
				bounds = itemBounds;
				if (e.ColumnIndex == 0)
				{
					if (item.Tag is GroupedListGrouper)
					{
						if (!item.Selected)
						{
							SolidBrush back = manager.GetBrush(skin.FieldBackColor);
							g.FillRectangle(back, new Rectangle(0, itemBounds.Y, view.Width, itemBounds.Height));
						}
						Pen divider = skin.PrimaryColor.GetBorderPen(VisualState.Normal, false, Enabled);
						GroupedListGrouper group = item.Tag as GroupedListGrouper;
						Image img = group.Expanded ? Properties.Resources.Collapse : Properties.Resources.Expand;
						bounds = new Rectangle(itemBounds.X + img.Width, itemBounds.Y - 1, view.ClientRectangle.Width - (itemBounds.X + img.Width), itemBounds.Height);
						if (_source.GetGroupCount(group.Path) > 0)
						{
							g.DrawImage(img, itemBounds.X, itemBounds.Y - 2);
						}
						g.DrawLine(divider, 5, e.Bounds.Bottom - 2, view.ClientRectangle.Width - 5, e.Bounds.Bottom - 2);

						if (ShowIndicators)
						{
							SolidBrush br = manager.GetBrush(item.ForeColor);
							g.FillRectangle(br, e.Bounds.X, e.Bounds.Y, 3, e.Bounds.Height);
						}
					}
				}
				else if (item.Tag is GroupedListGrouper)
				{
					return;
				}

				if (item.Selected)
				{
					SolidBrush textBrush = manager.GetBrush(Enabled ? skin.PrimaryColor.ForeColor : skin.PrimaryColor.DisabledForeColor);
					g.DrawString(e.SubItem.Text, item.Font, textBrush, bounds, sf);
				}
				else
				{
					if (!(item.Tag is GroupedListGrouper))
					{
						int depth = _source.GetDepth(item.Tag);
						SolidBrush back = manager.GetBrush(depth % 2 == 0 ? skin.FieldBackColor : skin.FieldAltBackColor);
						g.FillRectangle(back, e.Bounds);
						if (e.ColumnIndex == 0 && ShowIndicators)
						{
							SolidBrush br2 = manager.GetBrush(item.GrouperColor);
							g.FillRectangle(br2, e.Bounds.X, e.Bounds.Y, 3, e.Bounds.Height);
						}
					}
					SolidBrush br = manager.GetBrush(item.ForeColor);
					g.DrawString(e.SubItem.Text, item.Font, br, bounds, sf);
				}

				if (item.Item.LastInGroup)
				{
					Pen divider = skin.PrimaryColor.GetBorderPen(VisualState.Normal, false, Enabled);
					g.DrawLine(divider, 5, e.Bounds.Bottom - 2, view.ClientRectangle.Width - 5, e.Bounds.Bottom - 2);
				}
			}
		}

		private void View_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			view.Invalidate();
		}

		private void View_MouseClick(object sender, MouseEventArgs e)
		{
			if (_source == null) { return; }
			if (e.Button == MouseButtons.Right)
			{
				if (view.FocusedItem.Bounds.Contains(e.Location))
				{
					RightClick?.Invoke(this, new AccordionListViewEventArgs(view.FocusedItem.Tag));
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				ListViewItem item = view.GetItemAt(e.X, e.Y);
				if (item != null && item.Tag is GroupedListGrouper)
				{
					GroupedListGrouper grouper = item.Tag as GroupedListGrouper;
					int depth = grouper.Depth;
					int targetX = 4 + depth * Properties.Resources.Collapse.Width;
					if (e.X < targetX || e.X > targetX + Properties.Resources.Collapse.Width)
					{
						return;
					}
					if (_source.GetGroupCount(grouper.Path) > 0)
					{
						_source.ToggleGroup(grouper.Path, !grouper.Expanded);
					}
				}
			}
		}	
	}

	[Serializable]
	public class AccordionListViewItem : ListViewItem
	{
		public GroupedListItem Item { get; private set; }

		public Color GrouperColor { get; set; } = Color.Black;

		public AccordionListViewItem(GroupedListItem item)
		{
			Item = item;
		}

		protected AccordionListViewItem(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
