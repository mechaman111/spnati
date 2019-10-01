using Desktop.Forms;
using Desktop.Providers;
using Desktop.Skinning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class PropertyTable : UserControl, ISkinControl, ISkinnedPanel
	{
		/// <summary>
		/// Contextual object that will be passed to edit controls
		/// </summary>
		public object Context;
		/// <summary>
		/// Contextual object that will be passed to edit controls
		/// </summary>
		public object SecondaryContext;

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<MacroArgs> EditingMacro;
		public event EventHandler<MacroArgs> MacroChanged;
		public event EventHandler RowAdded;
		public event EventHandler RowRemoved;

		private SkinnedBackgroundType _background;
		public SkinnedBackgroundType PanelType
		{
			get { return _background; }
			set { _background = value; OnUpdateSkin(SkinManager.Instance.CurrentSkin); Invalidate(true); }
		}

		public SkinnedBackgroundType HeaderType
		{
			get { return menuSpeedButtons.Background; }
			set { menuSpeedButtons.Background = value; }
		}

		private int _pendingDataChanges = 0;

		private object _data;
		/// <summary>
		/// Data to provide to the edit controls
		/// </summary>
		public object Data
		{
			get { return _data; }
			set
			{
				if (_data == value) { return; }
				_data = value;
				if (_data != null)
				{
					PropertyProvider.BuildControlMap(_data.GetType());
				}
				recAdd.Text = "";
				BuildEditControls();

				if (_data != null && !HideSpeedButtons)
				{
					AddMacros();
				}
			}
		}

		private object _pendingData;
		/// <summary>
		/// Sets the Data object after a brief delay, so that only the last request in that amount of time is honored
		/// </summary>
		/// <param name="data"></param>
		public async void SetDataAsync(object data, object previewData)
		{
			_pendingData = data;
			_pendingDataChanges++;
			await Task.Delay(1);
			_pendingDataChanges--;
			if (_pendingDataChanges == 0)
			{
				if (Data != _pendingData)
				{
					PreviewData = previewData;
					Data = _pendingData;
				}
			}
		}

		private object _previewData;
		/// <summary>
		/// Secondary data that displays on edit controls that don't have the primary data defined.
		/// This does not come into effect automatically; Data must be set after setting this.
		/// </summary>
		public object PreviewData
		{
			get { return _previewData; }
			set { _previewData = value; }
		}

		public UndoManager UndoManager { get; set; }

		/// <summary>
		/// Filter for hiding records from the record select
		/// </summary>
		public Func<PropertyRecord, bool> RecordFilter;

		/// <summary>
		/// Filter for making records show up even without a value
		/// </summary>
		public Func<PropertyRecord, bool> RequiredFilter;

		private string _placeholder;
		/// <summary>
		/// What to show in the add field
		/// </summary>
		public string PlaceholderText
		{
			get { return _placeholder; }
			set
			{
				_placeholder = value;
				recAdd.PlaceholderText = _placeholder + " (Alt+A)";
			}
		}

		private bool _allowFavorites;
		public bool AllowFavorites
		{
			get { return _allowFavorites; }
			set
			{
				if (_allowFavorites != value)
				{
					_allowFavorites = value;
					_favoriteRecords.Clear();
				}
			}
		}

		public bool RunInitialAddEvents { get; set; }

		private bool _allowHelp = false;
		public bool AllowHelp { get { return _allowHelp; } set { _allowHelp = value; } }
		private bool _allowDelete = true;
		public bool AllowDelete { get { return _allowDelete; } set { _allowDelete = value; } }

		private HashSet<PropertyRecord> _favoriteRecords = new HashSet<PropertyRecord>();

		/// <summary>
		/// Caption for remove buttons
		/// </summary>
		public string RemoveCaption { get; set; }

		/// <summary>
		/// Whether property rows are sorted in the table, or added first-come-first-serve
		/// </summary>
		public bool Sorted { get; set; }

		public bool UseAutoComplete
		{
			get { return recAdd.UseAutoComplete; }
			set
			{
				recAdd.UseAutoComplete = value;
			}
		}

		private bool _hideAdd = false;
		public bool HideAddField
		{
			get { return _hideAdd; }
			set
			{
				_hideAdd = value;
				recAdd.Visible = !value;
				PositionControls();
			}
		}

		private bool _hideMenu = false;
		public bool HideSpeedButtons
		{
			get { return _hideMenu; }
			set
			{
				_hideMenu = value;
				menuSpeedButtons.Visible = !value;
				PositionControls();
			}
		}

		private ToolStripMenuItem _macroMenu;
		private bool _allowMacros = false;
		public bool AllowMacros
		{
			get { return _allowMacros; }
			set
			{
				_allowMacros = value;
			}
		}

		/// <summary>
		/// When set, switching Data will keep controls around and simply rebind them instead of building the list from scratch. Useful for contexts where the same controls get used for each Data object
		/// </summary>
		public bool PreserveControls { get; set; }

		private void PositionControls()
		{
			recAdd.Visible = !_hideAdd;
			menuSpeedButtons.Visible = !_hideMenu;
			menuSpeedButtons.Left = _hideAdd ? 0 : recAdd.Right;
			menuSpeedButtons.Width = _hideAdd ? Width : Width - recAdd.Right - 50;
			int bottom = pnlRecords.Bottom;
			pnlRecords.Top = (_hideAdd && _hideMenu ? 0 : menuSpeedButtons.Bottom + 3);
			pnlRecords.Height = bottom - pnlRecords.Top;
		}

		public float RowHeaderWidth { get; set; }

		private DualKeyDictionary<string, int, PropertyTableRow> _rows = new DualKeyDictionary<string, int, PropertyTableRow>();

		public string ModifyingProperty { get; set; }

		public PropertyTable()
		{
			RemoveCaption = "Remove";
			InitializeComponent();

			recAdd.RecordFilter = FilterControlsToData;
			recAdd.RecordType = typeof(PropertyRecord);

			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		/// <summary>
		/// Forces a save of all edit controls. Useful for keyboard shortcuts which could trigger before a field wtih focus validates
		/// </summary>
		public void Save()
		{
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					row.EditControl.Save();
				}
			}
		}

		/// <summary>
		/// Gets a list of keys of favorited records
		/// </summary>
		/// <returns></returns>
		public List<string> GetFavorites()
		{
			List<string> list = new List<string>();
			foreach (PropertyRecord rec in _favoriteRecords)
			{
				list.Add(rec.Key);
			}
			return list;
		}

		/// <summary>
		/// Sets the list of favorited records
		/// </summary>
		/// <param name="favorites"></param>
		public void SetFavorites(List<string> favorites)
		{
			if (Data == null) { return; }
			_favoriteRecords.Clear();

			Dictionary<string, PropertyRecord> recs = new Dictionary<string, PropertyRecord>();
			foreach (PropertyRecord rec in PropertyProvider.GetEditControls(Data.GetType()))
			{
				recs[rec.Key] = rec;
			}

			foreach (string key in favorites)
			{
				PropertyRecord record;
				if (recs.TryGetValue(key, out record))
				{
					_favoriteRecords.Add(record);
				}
			}
		}

		private void recAdd_RecordChanged(object sender, RecordEventArgs e)
		{
			if (Data == null)
			{
				return;
			}
			PropertyRecord result = e.Record as PropertyRecord;
			if (result != null)
			{
				AddControl(result);
			}
		}

		private PropertyEditControl FindControl(string key, object data)
		{
			Dictionary<int, PropertyTableRow> rows = _rows[key];
			if (rows != null)
			{
				foreach (PropertyTableRow row in rows.Values)
				{
					if (row.EditControl.GetValue() == data)
					{
						return row.EditControl;
					}
				}
			}
			return null;
		}

		public void AddProperty(string property)
		{
			PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == property);
			if (record != null)
			{
				AddControl(record);
			}
		}

		private PropertyEditControl AddControl(PropertyRecord result)
		{
			return AddControl(result, null);
		}
		private PropertyEditControl AddControl(PropertyRecord result, PropertyMacro macro)
		{
			bool newlyAdded;
			PropertyEditControl ctl = EditRecord(result, -1, out newlyAdded);
			ctl.SetInitialFocus(); //jump to the control for immediate editing
			recAdd.Record = null;
			if (macro != null)
			{
				List<string> values = new List<string>();
				foreach (string v in macro.Values)
				{
					string replacedValue = v;
					if (macro.VariableMap != null)
					{
						foreach (KeyValuePair<string, string> kvp in macro.VariableMap)
						{
							replacedValue = replacedValue.Replace(kvp.Key, kvp.Value);
						}
					}
					values.Add(replacedValue);
				}
				ctl.ApplyMacro(values);
				newlyAdded = false;
			}
			if (newlyAdded && RunInitialAddEvents)
			{
				ctl.OnInitialAdd();
			}
			return ctl;
		}

		private void focusOnAdd_Click(object sender, EventArgs e)
		{
			recAdd.Focus();
		}

		private bool FilterControlsToData(IRecord record)
		{
			PropertyRecord rec = record as PropertyRecord;
			if (RecordFilter != null && !RecordFilter(rec))
			{
				return false;
			}
			return rec.DataType == Data.GetType();
		}

		/// <summary>
		/// Creates records for all populated data members
		/// </summary>
		private void BuildEditControls()
		{
			this.SuspendDrawing();
			try
			{
				if (!PreserveControls || Data == null)
				{
					foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
					{
						foreach (PropertyTableRow row in kvp.Value.Values)
						{
							DisposeRow(row);
						}
					}
					_rows.Clear();
				}

				if (Data == null) { return; }

				HashSet<PropertyRecord> controlsToAdd = new HashSet<PropertyRecord>();
				HashSet<PropertyRecord> controlsToRemove = new HashSet<PropertyRecord>();
				HashSet<PropertyRecord> controlsToRebind = new HashSet<PropertyRecord>();
				foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
				{
					foreach (PropertyTableRow row in kvp.Value.Values)
					{
						controlsToRemove.Add(row.Record);
					}
				}

				SortedDictionary<string, List<PropertyRecord>> groups = new SortedDictionary<string, List<PropertyRecord>>();
				foreach (PropertyRecord editControl in PropertyProvider.GetEditControls(Data.GetType()))
				{
					if (RecordFilter != null && !RecordFilter(editControl)) { continue; }

					string group = editControl.Group;
					if (!string.IsNullOrEmpty(group))
					{
						List<PropertyRecord> groupList;
						if (!groups.TryGetValue(group, out groupList))
						{
							groupList = new List<PropertyRecord>();
							groups[group] = groupList;
						}
						groupList.Add(editControl);
					}

					bool required = editControl.Required || (_hideMenu && _hideAdd);
					if (!required && RequiredFilter != null)
					{
						required = RequiredFilter(editControl);
					}

					MemberInfo member = editControl.Member;
					object value = member.GetValue(Data);
					Type memberType = member.GetDataType();
					if (memberType == typeof(int) && (int)value == 0 ||
						memberType == typeof(bool) && (bool)value == false ||
						value == null)
					{
						bool favorited = _favoriteRecords.Contains(editControl);
						if (!favorited && !required)
						{
							//skip the field if it has no value and is not favorited or required
							continue;
						}
					}

					if (typeof(IList).IsAssignableFrom(memberType))
					{
						//always remove and readd List type controls rather than going down into individual indices
						controlsToAdd.Add(editControl);
					}
					else
					{
						if (!controlsToRemove.Contains(editControl))
						{
							controlsToAdd.Add(editControl);
						}
						else
						{
							controlsToRebind.Add(editControl);
							controlsToRemove.Remove(editControl);
						}
					}
				}

				BuildSpeedMenus(groups);

				//remove any old controls
				foreach (PropertyRecord record in controlsToRemove)
				{
					Dictionary<int, PropertyTableRow> rows = _rows[record.Property];
					foreach (KeyValuePair<int, PropertyTableRow> kvp in rows)
					{
						DisposeRow(kvp.Value);
					}
					_rows.Remove(record.Property);
				}

				bool newlyAdded;
				//add or rebind the controls
				foreach (PropertyRecord editControl in controlsToAdd)
				{
					MemberInfo member = editControl.Member;
					object value = member.GetValue(Data);
					Type memberType = member.GetDataType();
					if (value != null && typeof(IList).IsAssignableFrom(memberType))
					{
						IList list = value as IList;
						if (list.Count == 0)
						{
							continue;
						}
						for (int i = 0; i < list.Count; i++)
						{
							EditRecord(editControl, i, out newlyAdded);
						}
					}
					else
					{
						EditRecord(editControl, -1, out newlyAdded);
					}
				}
				foreach (PropertyRecord editControl in controlsToRebind)
				{
					PropertyEditControl ctl = EditRecord(editControl, -1, out newlyAdded);
					ctl.Rebind(Data, PreviewData, Context, SecondaryContext);
				}
			}
			finally
			{
				this.ResumeDrawing();
			}
		}

		private void BuildSpeedMenus(SortedDictionary<string, List<PropertyRecord>> groups)
		{
			menuSpeedButtons.Items.Clear();
			foreach (KeyValuePair<string, List<PropertyRecord>> kvp in groups)
			{
				ToolStripMenuItem groupMenu = new ToolStripMenuItem(kvp.Key);
				menuSpeedButtons.Items.Add(groupMenu);

				List<PropertyRecord> list = kvp.Value;
				list.Sort();

				foreach (PropertyRecord record in list)
				{
					ToolStripDropDownItem item = new ToolStripMenuItem(record.Name);
					item.Tag = record;
					groupMenu.DropDownItems.Add(item);
					item.Click += SpeedButtonClick;
				}
			}
		}

		private PropertyEditControl EditRecord(PropertyRecord result, int index, out bool newControl)
		{
			newControl = false;
			PropertyEditControl ctl = null;
			PropertyTableRow row = _rows.Get(result.Property, index);
			if (row != null)
			{
				ctl = row.EditControl;
			}

			//if the record doesn't exist, create and add it
			if (ctl == null)
			{
				MemberInfo mi = Data.GetType().GetMember(result.Property)[0];
				Type memberType = mi.GetDataType();
				if (typeof(IList).IsAssignableFrom(memberType))
				{
					//need to create a control for a new entry in the list
					IList list = mi.GetValue(Data) as IList;
					if (list == null)
					{
						//list is currently null, so create it
						list = Activator.CreateInstance(memberType) as IList;
						mi.SetValue(Data, list);
					}

					//create an item
					if (index == -1)
					{
						//see if any item doesn't have a control yet and use that. Otherwise create a new item
						for (int i = 0; i < list.Count; i++)
						{
							PropertyTableRow indexRow = _rows.Get(result.Property, i);
							if (indexRow == null)
							{
								index = i;
								break;
							}
						}
						if (index == -1)
						{
							Type itemType = memberType.GenericTypeArguments[0];
							object item = null;
							if (itemType == typeof(string))
							{
								item = "";
							}
							else
							{
								item = Activator.CreateInstance(itemType);
							}
							list.Add(item);
							if (Data is IPropertyChangedNotifier)
							{
								((IPropertyChangedNotifier)Data).NotifyPropertyChanged(result.Property);
							}
							index = list.Count - 1;
						}
					}
				}

				newControl = true;
				ctl = Activator.CreateInstance(result.EditControlType) as PropertyEditControl;
				ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				ctl.SetParameters(result.Attribute);
				ctl.RequireHeight += PropertyEditControl_RequireHeight;

				ctl.SetData(Data, result.Property, index, Context, SecondaryContext, UndoManager, _previewData, this);

				row = new PropertyTableRow();
				if (result.RowHeight > 0)
				{
					row.Height = result.RowHeight;
				}
				row.AllowFavorites = AllowFavorites;
				row.AllowDelete = AllowDelete;
				row.AllowHelp = AllowHelp;
				row.EditingMacro += Row_EditingMacro;
				if (RowHeaderWidth > 0)
				{
					row.HeaderWidth = RowHeaderWidth;
				}
				row.Required = result.Required || (_hideMenu && _hideAdd) || (RequiredFilter != null && RequiredFilter(result));
				row.Favorited = _favoriteRecords.Contains(result);
				row.PropertyChanged += Row_PropertyChanged;
				row.RemoveRow += Row_RemoveRow;
				row.ToggleFavorite += Row_ToggleFavorite;
				row.Dock = DockStyle.Top;
				row.RemoveCaption = RemoveCaption;
				row.Set(ctl, result);
				_rows.Set(result.Property, index, row);

				ctl.OnAddedToRow();

				pnlRecords.Controls.Add(row);
				pnlRecords.Controls.SetChildIndex(row, 0);
				if (Sorted)
				{
					for (int i = pnlRecords.Controls.Count - 1; i >= 1; i--)
					{
						PropertyTableRow otherRow = pnlRecords.Controls[i] as PropertyTableRow;
						if (row.CompareTo(otherRow) < 0)
						{
							pnlRecords.Controls.SetChildIndex(row, i);
							break;
						}
					}
					for (int i = 0; i < pnlRecords.Controls.Count; i++)
					{
						Control rowCtl = pnlRecords.Controls[i];
						rowCtl.TabIndex = pnlRecords.Controls.Count - i - 1;
					}
				}
				RowAdded?.Invoke(this, EventArgs.Empty);
			}

			return ctl;
		}

		/// <summary>
		/// Raised when a control needs a specific height
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="height"></param>
		private void PropertyEditControl_RequireHeight(object sender, int height)
		{
			PropertyEditControl ctl = sender as PropertyEditControl;
			PropertyTableRow row = _rows.Get(ctl.Property, ctl.Index);
			if (row != null)
			{
				row.Height = height + row.Padding.Top + row.Padding.Bottom + ctl.Margin.Top + ctl.Margin.Bottom;
			}
		}

		private void Row_EditingMacro(object sender, MacroArgs args)
		{
			EditingMacro?.Invoke(this, args);
			if (args.Editor != null)
			{
				MacroEditor form = new MacroEditor();
				form.SetMacro(args.Macro, args.Editor);
				if (form.ShowDialog() == DialogResult.OK)
				{
					AddMacros();
					MacroChanged?.Invoke(this, new MacroArgs(args.Macro, false));
				}
				else if (args.IsNew)
				{
					MacroProvider provider = new MacroProvider();
					provider.Remove(Data.GetType(), args.Macro);
				}
			}
		}

		private void DisposeRow(PropertyTableRow row)
		{
			pnlRecords.Controls.Remove(row);
			row.PropertyChanged -= Row_PropertyChanged;
			row.RemoveRow -= Row_RemoveRow;
			row.ToggleFavorite -= Row_ToggleFavorite;
			row.EditingMacro -= Row_EditingMacro;
			row.EditControl.RequireHeight -= PropertyEditControl_RequireHeight;
			row.Destroy();
			row.Dispose();
		}

		private void Row_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					row.OnOtherPropertyChanged(sender, e);
				}
			}

			PropertyTableRow propRow = sender as PropertyTableRow;
			PropertyChanged?.Invoke(propRow.EditControl.Property, e);
		}

		/// <summary>
		/// Manual notification that a property has changed and any rows listening to that (or bound to it) should be updated.
		/// </summary>
		/// <remarks>
		/// Hacky workaround to a lack of databinding.
		/// </remarks>
		/// <param name="propertyName"></param>
		public void UpdateProperty(string propertyName)
		{
			if (Data == null) { return; }
			PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					if (row.EditControl.Property == propertyName)
					{
						row.EditControl.Rebind();
					}
					else
					{
						row.OnOtherPropertyChanged(this, e);
					}
				}
			}

			//if no record exists for this property, add it
			if (!_rows.ContainsPrimaryKey(propertyName))
			{
				PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == propertyName);
				if (record != null && (RecordFilter == null || RecordFilter(record)))
				{
					AddControl(record);
				}
			}
		}

		private void Row_RemoveRow(object sender, EventArgs e)
		{
			PropertyTableRow row = sender as PropertyTableRow;
			RemoveRow(row);
		}

		private void RemoveRow(PropertyTableRow row)
		{
			PropertyEditControl ctl = row.EditControl;
			ctl.Clear();

			DisposeRow(row);
			_rows.Remove(ctl.Property, ctl.Index);

			int index = ctl.Index;
			if (index >= 0)
			{
				//this is a list, so we need to remove the item from the list and shift the indices of all the other edit controls
				ctl.RemoveFromList();
				Dictionary<int, PropertyTableRow> siblings = _rows[ctl.Property];
				if (siblings != null)
				{
					List<PropertyTableRow> affectedRows = new List<PropertyTableRow>();
					foreach (PropertyTableRow otherRow in siblings.Values)
					{
						if (otherRow.EditControl.Index > index)
						{
							affectedRows.Add(otherRow);
						}
					}

					//Update the keys for the affected rows
					foreach (PropertyTableRow otherRow in affectedRows)
					{
						PropertyEditControl otherCtl = otherRow.EditControl;
						_rows.Remove(otherCtl.Property, otherCtl.Index);
						otherCtl.Index--;
						_rows.Set(otherCtl.Property, otherCtl.Index, otherRow);
					}
				}
			}

			RowRemoved?.Invoke(this, EventArgs.Empty);
		}

		private void Row_ToggleFavorite(object sender, EventArgs e)
		{
			PropertyTableRow row = sender as PropertyTableRow;
			PropertyRecord record = row.Record;
			if (row.Favorited)
			{
				_favoriteRecords.Add(record);
			}
			else
			{
				_favoriteRecords.Remove(record);
			}
		}

		private void SpeedButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			PropertyRecord record = item.Tag as PropertyRecord;
			AddControl(record);
		}

		/// <summary>
		/// Runs a filter over added rows to hide and show them
		/// </summary>
		/// <param name="filter"></param>
		public void RunFilter(Func<PropertyRecord, object, object, object, bool> filter)
		{
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					row.Visible = filter(row.Record, Data, Context, SecondaryContext);
				}
			}
		}

		private ToolStripMenuItem GetOrAddGroupMenu(string group)
		{
			int macroSeparator = -1;
			ToolStripMenuItem groupMenu = null;
			for (int i = 0; i < menuSpeedButtons.Items.Count; i++)
			{
				ToolStripItem mnuItem = menuSpeedButtons.Items[i];
				if (mnuItem is ToolStripSeparator)
				{
					macroSeparator = i;
					continue;
				}
				if (mnuItem.Text == group)
				{
					groupMenu = mnuItem as ToolStripMenuItem;
					break;
				}
			}
			if (groupMenu == null)
			{
				groupMenu = new ToolStripMenuItem(group);
				if (macroSeparator >= 0)
				{
					menuSpeedButtons.Items.Insert(macroSeparator, groupMenu);
				}
				else
				{
					if (group == "Macros")
					{
						menuSpeedButtons.Items.Add(new ToolStripSeparator());
					}
					menuSpeedButtons.Items.Add(groupMenu);
				}
			}
			return groupMenu;
		}

		public void AddSpeedButton(string group, string caption, Func<object, SpeedButtonData> propertyCreator)
		{
			ToolStripMenuItem groupMenu = GetOrAddGroupMenu(group);

			ToolStripMenuItem item = new ToolStripMenuItem(caption);
			item.Tag = propertyCreator;
			item.Click += CustomSpeedButtonClick;

			//insert before the macro separator if there is one
			for (int i = 0; i < groupMenu.DropDownItems.Count; i++)
			{
				if (groupMenu.DropDownItems[i] is ToolStripSeparator)
				{
					groupMenu.DropDownItems.Insert(i, item);
					return;
				}
			}

			groupMenu.DropDownItems.Add(item);
		}

		private void CustomSpeedButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Func<object, SpeedButtonData> func = item.Tag as Func<object, SpeedButtonData>;
			SpeedButtonData data = func(Data);
			string property = data.Property;
			string subproperty = data.Subproperty;
			string[] pieces = property.Split('>');
			if (pieces.Length > 1)
			{
				property = pieces[0];
				subproperty = pieces[1];
			}
			PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == property);
			if (record != null)
			{
				PropertyEditControl ctl = null;
				if (data.ListItem != null)
				{
					ctl = FindControl(property, data.ListItem);
				}
				if (ctl == null)
				{
					ctl = AddControl(record);
				}
				if (ctl != null && !string.IsNullOrEmpty(subproperty))
				{
					ctl.EditSubProperty(subproperty);
				}
			}
		}

		public void AddMacros()
		{
			if (Data == null) { return; }
			if (HideSpeedButtons || !AllowMacros)
			{
				return;
			}
			if (_macroMenu != null)
			{
				_macroMenu.DropDownItems.Clear();
			}

			MacroProvider provider = new MacroProvider();
			provider.SetContext(Data.GetType());
			foreach (Macro macro in provider.GetRecords(""))
			{
				AddMacro(macro);
			}
		}

		private void AddMacro(Macro macro)
		{
			//if any properties are filtered out, filter out the whole macro
			if (RecordFilter != null)
			{
				foreach (PropertyMacro property in macro.Properties)
				{
					PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == property.Property);
					if (!RecordFilter(record))
					{
						return;
					}
				}
			}

			string group = macro.Group;
			if (string.IsNullOrEmpty(group))
			{
				group = "Macros";
			}
			ToolStripMenuItem groupMenu = GetOrAddGroupMenu(group);
			_macroMenu = groupMenu;
			ToolStripMenuItem item = new ToolStripMenuItem(macro.Name);
			item.Tag = macro;
			item.Click += MacroButtonClick;

			bool added = false;
			int macroSeparator = -1;
			for (int i = 0; i < groupMenu.DropDownItems.Count; i++)
			{
				ToolStripSeparator separator = groupMenu.DropDownItems[i] as ToolStripSeparator;
				if (separator != null)
				{
					macroSeparator = i;
					break;
				}
				if (macroSeparator >= 0 || group == "Macros")
				{
					ToolStripItem existing = groupMenu.DropDownItems[i];
					if (existing.Name.CompareTo(macro.Name) > 0)
					{
						added = true;
						groupMenu.DropDownItems.Insert(i, item);
						break;
					}
				}
			}

			if (!added)
			{
				if (macroSeparator == -1 && group != "Macros")
				{
					groupMenu.DropDownItems.Add(new ToolStripSeparator());
				}
				groupMenu.DropDownItems.Add(item);
			}
		}

		private void MacroButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Macro macro = item.Tag as Macro;
			HashSet<string> vars = macro.GetVariables();
			Dictionary<string, string> varMap = new Dictionary<string, string>();
			if (vars.Count > 0)
			{
				VariableMapper mapper = new VariableMapper(vars);
				if (mapper.ShowDialog() == DialogResult.Cancel)
				{
					return;
				}
				varMap = mapper.Map;
			}
			ApplyMacro(macro, varMap);
		}

		/// <summary>
		/// Applies a macro to the data
		/// </summary>
		/// <param name="macro"></param>
		internal void ApplyMacro(Macro macro, Dictionary<string, string> varMap)
		{
			foreach (PropertyMacro application in macro.Properties)
			{
				application.VariableMap = varMap;
				string property = application.Property;
				PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == property);
				if (record != null)
				{
					AddControl(record, application);
				}
			}
		}

		/// <summary>
		/// Converts the current properties into a macro
		/// </summary>
		/// <returns></returns>
		public Macro CreateMacro()
		{
			Macro macro = new Macro();
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> row in _rows)
			{
				foreach (KeyValuePair<int, PropertyTableRow> kvp in row.Value)
				{
					List<string> values = new List<string>();
					kvp.Value.EditControl.BuildMacro(values);
					macro.AddProperty(row.Key, kvp.Key, values);
				}
			}
			return macro;
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.GetBackColor(PanelType);
		}

		/// <summary>
		/// Gets the minimum height of everything in the property to avoid scrolling
		/// </summary>
		/// <returns></returns>
		public int GetTotalHeight()
		{
			int height = pnlRecords.Top;
			int max = 0;
			foreach (Control ctl in pnlRecords.Controls)
			{
				max = Math.Max(max, ctl.Bottom);
			}
			height += max;
			return height + pnlRecords.Margin.Bottom + pnlRecords.Margin.Top;
		}
	}

	public class SpeedButtonData
	{
		public string Property;
		public string Subproperty;
		public object ListItem;

		public SpeedButtonData(string property)
		{
			Property = property;
		}

		public SpeedButtonData(string property, string subproperty)
		{
			Property = property;
			Subproperty = subproperty;
		}
	}
}
