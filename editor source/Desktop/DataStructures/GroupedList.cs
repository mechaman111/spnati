using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Desktop.CommonControls
{
	/// <summary>
	/// List of bindable lists that have group headers. Assumes that an item can only appear in the list once
	/// </summary>
	public class GroupedList<T> : IGroupedList where T : class, IGroupedItem, INotifyPropertyChanged
	{
		/// <summary>
		/// List of all top-level groups
		/// </summary>
		private List<Group> _groups { get; set; } = new List<Group>();
		/// <summary>
		/// Map between group keys and group objects
		/// </summary>
		private Dictionary<string, Group> _groupMap = new Dictionary<string, Group>();
		/// <summary>
		/// Mapping between items and what group they belong to
		/// </summary>
		private Dictionary<T, Group> _groupIndex = new Dictionary<T, Group>();
		/// <summary>
		/// Mapping between items and their index within a group
		/// </summary>
		private Dictionary<T, int> _index = new Dictionary<T, int>();
		/// <summary>
		/// Mapping between items and their PropertyChanged handlers
		/// </summary>
		private Dictionary<T, PropertyChangedEventHandler> _propertyHandlers = new Dictionary<T, PropertyChangedEventHandler>();

		public event EventHandler<GroupedListMovingEventArgs> BeforeMovingItem;
		public event EventHandler<GroupedListMovingEventArgs> AfterMovingItem;
		public event EventHandler<GroupedListChangedEventArgs> ListChanged;

		public Func<T, T, int> ItemComparer;
		public Func<string, string, int> GroupComparer;

		private bool _sorted;
		public bool Sorted
		{
			get { return _sorted; }
			set
			{
				if (_sorted != value)
				{
					_sorted = value;
					if (_sorted)
					{
						SortItems();
					}
				}
			}
		}

		public void AddGroup(string key)
		{
			AddGroupPrivate(key);
		}
		private Group AddGroupPrivate(string key)
		{
			string[] chain = key.Split(new string[] { ">" }, StringSplitOptions.RemoveEmptyEntries);
			Group parentGroup = null;
			string fullPath = null;
			for (int i = 0; i < chain.Length; i++)
			{
				string groupKey = chain[i];
				if (fullPath == null)
				{
					fullPath = groupKey;
				}
				else
				{
					fullPath += ">" + groupKey;
				}
				Group group;
				if (!_groupMap.TryGetValue(fullPath, out group))
				{
					GroupedListGrouper grouper = new GroupedListGrouper(fullPath, groupKey);
					group = new Group(grouper, parentGroup);
					_groupMap[fullPath] = group;
					if (parentGroup == null)
					{
						_groups.Add(group);
						_groups.Sort(SortGroups);
						for (int j = 0; j < _groups.Count; j++)
						{
							_groups[j].Index = j;
						}
					}
					else
					{
						parentGroup.SubGroups.Add(group);
						parentGroup.SubGroups.Sort(SortGroups);
						for(int j = 0; j < parentGroup.SubGroups.Count; j++)
						{
							parentGroup.SubGroups[j].Index = j;
						}
					}
				}
				parentGroup = group;
			}
			return parentGroup;
		}

		private int SortGroups(Group g1, Group g2)
		{
			if (GroupComparer != null)
			{
				return GroupComparer(g1.Key, g2.Key);
			}
			return g1.Key.CompareTo(g2.Key);
		}

		/// <summary>
		/// Current count of expanded groups and items. If an item is behind a collapsed group, it is not counted here
		/// </summary>
		public int Count
		{
			get
			{
				int count = 0;
				for (int i = 0; i < _groups.Count; i++)
				{
					count++;
					Group g = _groups[i];
					count += g.GetExpandedCount();
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the number of first-level items contained under a group path, regardless of collapsed state
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public int GetGroupCount(string path)
		{
			Group group;
			if (_groupMap.TryGetValue(path, out group))
			{
				return group.Items.Count + group.SubGroups.Count;
			}
			return -1;
		}

		/// <summary>
		/// Gets the item or group occupying a virtual index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public GroupedListItem GetItem(int index)
		{
			object item;
			for (int i = 0; i < _groups.Count; i++)
			{
				Group g = _groups[i];
				Group parent = null;
				item = g.GetItem(ref index, ref parent);
				if (item != null)
				{
					bool lastInGroup = false;
					if (item is T)
					{
						lastInGroup = _index[(T)item] == parent.Items.Count - 1;
					}
					return new GroupedListItem(item, parent?.Grouper, lastInGroup);
				}
			}
			return null;
		}

		public void SortItems()
		{
			if (ItemComparer != null)
			{
				foreach (Group group in _groups)
				{
					SortGroup(group);
				}
			}
		}

		private void SortGroup(Group group)
		{
			foreach (Group subgroup in group.SubGroups)
			{
				SortGroup(subgroup);
			}
			group.Items.Sort(SortItemsMethod);
			IndexGroup(group);
		}

		private int SortItemsMethod(T item1, T item2)
		{
			return ItemComparer(item1, item2);
		}

		public void AddItem(T item)
		{
			string groupKey = item.GetGroupKey();
			Group group;
			if (!_groupMap.TryGetValue(groupKey, out group))
			{
				group = AddGroupPrivate(groupKey);
			}
			bool added = false;
			if (_sorted && ItemComparer != null)
			{
				for (int i = 0; i < group.Items.Count; i++)
				{
					int compare = ItemComparer(group.Items[i], item);
					if (compare > 0)
					{
						group.Items.Insert(i, item);
						added = true;
						break;
					}
				}
			}
			if (!added)
			{
				group.Items.Add(item);
			}

			IndexGroup(group);
			_groupIndex[item] = group;
			int index = GetIndex(item);
			PropertyChangedEventHandler handler = new PropertyChangedEventHandler((sender, e) =>
			{
				OnItemModified(item);
			});
			item.PropertyChanged += handler;
			_propertyHandlers[item] = handler;
			ListChanged?.Invoke(this, new GroupedListChangedEventArgs(GroupedListChangedAction.Add, item, index));
		}

		private void OnItemModified(T item)
		{
			int index = GetIndex(item);
			string key = item.GetGroupKey();
			Group indexedGroup = _groupIndex[item];
			if (indexedGroup.Path != key)
			{
				//group changed; need to remove and add back, which will also do a re-sort
				GroupedListMovingEventArgs modifyArgs = new GroupedListMovingEventArgs(item, index);
				BeforeMovingItem?.Invoke(this, modifyArgs);
				RemoveItem(item);
				AddItem(item);
				AfterMovingItem?.Invoke(this, modifyArgs);
			}
			else
			{
				//auto-resort disabled for now since it makes selecting a node that causes a resort that moves that node to behave funky

				////see if we need to re-sort this
				//if (_sorted && ItemComparer != null)
				//{
				//	bool sorted = false;
				//	int indexedPos = _index[item];
				//	int newIndex = indexedPos;
				//	for (int i = 0; i < indexedGroup.Items.Count; i++)
				//	{
				//		if (i == indexedPos)
				//		{
				//			continue;
				//		}
				//		int compare = ItemComparer(indexedGroup.Items[i], item);
				//		if (compare > 0)
				//		{
				//			sorted = true;
				//			if (indexedPos != i - 1)
				//			{
				//				newIndex = i;
				//			}
				//			break;
				//		}
				//	}
				//	if (!sorted && indexedPos < indexedGroup.Items.Count - 1)
				//	{
				//		//move to the end
				//		newIndex = indexedGroup.Items.Count;
				//	}
				//	if (newIndex != indexedPos)
				//	{
				//		GroupedListMovingEventArgs modifyArgs = new GroupedListMovingEventArgs(item, index);
				//		BeforeMovingItem?.Invoke(this, modifyArgs);
				//		if (newIndex > indexedPos)
				//		{
				//			newIndex--;
				//		}
				//		indexedGroup.Items.RemoveAt(indexedPos);
				//		indexedGroup.Items.Insert(newIndex, item);
				//		IndexGroup(indexedGroup);
				//		AfterMovingItem?.Invoke(this, modifyArgs);
				//	}
				//}

				ListChanged?.Invoke(this, new GroupedListChangedEventArgs(GroupedListChangedAction.Modify, item, index));
			}
		}

		public void RemoveItem(T item)
		{
			string groupKey = item.GetGroupKey();
			Group group;
			if (_groupIndex.TryGetValue(item, out group))
			{
				int index = _index[item];
				int absIndex = GetIndex(item);
				group.Items.RemoveAt(index);
				IndexGroup(group);
				ListChanged?.Invoke(this, new GroupedListChangedEventArgs(GroupedListChangedAction.Remove, item, absIndex));
				PropertyChangedEventHandler handler = _propertyHandlers[item];
				item.PropertyChanged -= handler;
				_propertyHandlers.Remove(item);
				_groupIndex.Remove(item);
			}
		}

		/// <summary>
		/// Gets the virtual index of an item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetIndex(object item)
		{
			if (item == null) { return -1; }
			int index = 0;
			for (int i = 0; i < _groups.Count; i++)
			{
				if (GetIndex(_groups[i], item, ref index))
				{
					return index;
				}
			}
			return -1;
		}

		private bool GetIndex(Group group, object item, ref int index)
		{
			if (item is GroupedListGrouper)
			{
				if (group.Grouper.Equals(item))
				{
					return true;
				}
				index++; //count the group
				if (group.Expanded)
				{
					foreach (Group subgroup in group.SubGroups)
					{
						if (GetIndex(subgroup, item, ref index))
						{
							return true;
						}
					}
				}
			}
			else
			{
				Group indexedGroup;
				if (!_groupIndex.TryGetValue((T)item, out indexedGroup))
				{
					return false;
				}
				index++; //count the group
				if (group.Expanded)
				{
					foreach (Group subgroup in group.SubGroups)
					{
						if (GetIndex(subgroup, item, ref index))
						{
							return true;
						}
					}
					if (indexedGroup == group)
					{
						//item is in this group
						int groupIndex = _index[(T)item];
						index += groupIndex;
						return true;
					}
					else
					{
						//item is not in this group, so skip iterating it
						index += group.Items.Count;
					}
				}
				else if (indexedGroup == group)
				{
					//item is in this group, but it's collapsed
					index = -1;
					return true;
				}
			}
			return false;
		}

		private void IndexGroup(Group group)
		{
			for (int i = 0; i < group.SubGroups.Count; i++)
			{
				Group subgroup = group.SubGroups[i];
				subgroup.Index = i;
				IndexGroup(subgroup);
			}
			for (int i = 0; i < group.Items.Count; i++)
			{
				_index[group.Items[i]] = i;
			}
			Group parent = group.Parent;
			Group root = group;
			int depth = 0;
			while (parent != null)
			{
				depth++;
				root = parent;
				parent = parent.Parent;
			}
			group.Depth = depth;
			parent = group.Parent;
			while (parent != null && depth > 1)
			{
				parent.Depth = --depth;
				parent = parent.Parent;
			}
			group.RootKey = root.Key;
		}

		public int GetDepth(object item)
		{
			Group group;
			if (_groupIndex.TryGetValue((T)item, out group))
			{
				return group.Depth;
			}
			return 0;
		}

		public void ToggleGroup(string key, bool expanded)
		{
			Group group;
			if (_groupMap.TryGetValue(key, out group))
			{
				if (group.Expanded != expanded)
				{
					group.Expanded = expanded;
					ListChanged?.Invoke(this, new GroupedListChangedEventArgs(GroupedListChangedAction.GroupToggled, group.Grouper, -1));
				}
			}
		}

		public void ExpandAll()
		{
			foreach (Group group in _groupMap.Values)
			{
				ToggleGroup(group.Path, true);
			}
		}

		public void CollapseAll()
		{
			foreach (Group group in _groupMap.Values)
			{
				ToggleGroup(group.Path, false);
			}
		}

		/// <summary>
		/// Expands all the groups leading up to an item
		/// </summary>
		/// <param name="item"></param>
		public void ExpandTo(object item)
		{
			T model = item as T;
			if (model == null)
			{
				return;
			}

			Group group;
			if (_groupIndex.TryGetValue(model, out group))
			{
				while (group != null)
				{
					if (!group.Expanded)
					{
						ToggleGroup(group.Path, true);
					}
					group = group.Parent;
				}
			}
		}

		private class Group
		{
			public int Depth { get { return Grouper.Depth; } set { Grouper.Depth = value; } }
			public int Index { get { return Grouper.Index; } set { Grouper.Index = value; } }
			public string RootKey { get { return Grouper.RootKey; } set { Grouper.RootKey = value; } }
			public Group Parent;
			public GroupedListGrouper Grouper;
			public string Path { get { return Grouper.Path; } }
			public string Key { get { return Grouper.Key; } }
			public List<Group> SubGroups = new List<Group>();
			public List<T> Items = new List<T>();
			public bool Expanded
			{
				get
				{
					return Grouper.Expanded;
				}
				set
				{
					Grouper.Expanded = value;
				}
			}

			public Group(GroupedListGrouper grouper, Group parent)
			{
				Grouper = grouper;
				Parent = parent;
			}

			/// <summary>
			/// Gets the number of items expanded under this group (not including this group itself)
			/// </summary>
			/// <returns></returns>
			public int GetExpandedCount()
			{
				int count = 0;
				if (Expanded)
				{
					count += Items.Count;
					for (int i = 0; i < SubGroups.Count; i++)
					{
						count++;
						count += SubGroups[i].GetExpandedCount();
					}
				}
				return count;
			}

			/// <summary>
			/// Gets the item at a virtual index, which can change depending on the collapsed states
			/// </summary>
			/// <param name="index">Index to lookup. Output is input - number of virtual items were examined</param>
			/// <returns>Item at the index if found</returns>
			public object GetItem(ref int index, ref Group parentGroup)
			{
				if (index == 0)
				{
					return Grouper;
				}
				index--;
				if (Expanded)
				{
					foreach (Group subgroup in SubGroups)
					{
						object item = subgroup.GetItem(ref index, ref parentGroup);
						if (item != null)
						{
							parentGroup = subgroup;
							return item;
						}
					}
					if (index < Items.Count)
					{
						parentGroup = this;
						return Items[index];
					}
					index -= Items.Count;
				}
				parentGroup = null;
				return null;
			}
		}
	}

	/// <summary>
	/// Public accessor to a group
	/// </summary>
	public class GroupedListGrouper
	{
		public int Depth { get; internal set; }

		/// <summary>
		/// Full group chain to this one
		/// </summary>
		public string Path { get; private set; }
		/// <summary>
		/// Group's unique key
		/// </summary>
		public string Key { get; private set; }
		/// <summary>
		/// Whether this group is expanded
		/// </summary>
		public bool Expanded { get; internal set; }

		/// <summary>
		/// Groups index within its parent
		/// </summary>
		public int Index;

		/// <summary>
		/// Key for the root node
		/// </summary>
		public string RootKey;

		public GroupedListGrouper(string path, string key)
		{
			Path = path;
			Key = key;
			Expanded = false;
		}
	}

	public interface IGroupedList
	{
		int Count { get; }
		GroupedListItem GetItem(int index);
		/// <summary>
		/// Gets the virtual index of item based on the groups' collapsed states
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		int GetIndex(object item);
		/// <summary>
		/// Gets an item's depth
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		int GetDepth(object item);
		/// <summary>
		/// Gets the number of items under a group
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		int GetGroupCount(string key);
		void ToggleGroup(string key, bool expanded);
		/// <summary>
		/// Forces a item's group to expand if it isn't already
		/// </summary>
		/// <param name="item">Item to expand to</param>
		void ExpandTo(object item);
		void ExpandAll();
		void CollapseAll();
		event EventHandler<GroupedListChangedEventArgs> ListChanged;
		event EventHandler<GroupedListMovingEventArgs> BeforeMovingItem;
		event EventHandler<GroupedListMovingEventArgs> AfterMovingItem;
	}

	public interface IGroupedItem
	{
		string GetGroupKey();
	}

	public class GroupedListChangedEventArgs : EventArgs
	{
		public GroupedListChangedAction Action { get; private set; }
		public int Index { get; private set; }
		public object Item { get; private set; }


		public GroupedListChangedEventArgs(GroupedListChangedAction action) : this(action, null, -1) { }
		public GroupedListChangedEventArgs(GroupedListChangedAction action, object item, int index)
		{
			Action = action;
			Item = item;
			Index = index;
		}
	}

	public class GroupedListMovingEventArgs : EventArgs
	{
		public object Item;
		public int OriginalIndex;

		public GroupedListMovingEventArgs(object item, int index)
		{
			Item = item;
			OriginalIndex = index;
		}
	}

	public enum GroupedListChangedAction
	{
		/// <summary>
		/// One item was added
		/// </summary>
		Add,
		/// <summary>
		/// One item was removed
		/// </summary>
		Remove,
		/// <summary>
		/// All items were removed
		/// </summary>
		Clear,
		/// <summary>
		/// One item was modified
		/// </summary>
		Modify,
		/// <summary>
		/// A group was expanded or collapsed
		/// </summary>
		GroupToggled,
	}

	public class GroupedListItem
	{
		public object Data;
		public GroupedListGrouper Group;
		public bool LastInGroup;

		public GroupedListItem(object data, GroupedListGrouper group, bool lastInGroup)
		{
			Data = data;
			Group = group;
			LastInGroup = lastInGroup;
		}
	}
}
