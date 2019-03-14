using Desktop.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class PropertyTable : UserControl
	{
		/// <summary>
		/// Contextual object that will be passed to edit controls
		/// </summary>
		public object Context;

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<MacroArgs> EditingMacro;
		public event EventHandler<Macro> MacroChanged;

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

		private void PositionControls()
		{
			recAdd.Visible = !_hideAdd;
			menuSpeedButtons.Visible = !_hideMenu;
			menuSpeedButtons.Left = _hideAdd ? 0 : recAdd.Right;
			int bottom = pnlRecords.Bottom;
			pnlRecords.Top = (_hideAdd && _hideMenu ? 0 : menuSpeedButtons.Bottom + 3);
			pnlRecords.Height = bottom - pnlRecords.Top;
		}

		public float RowHeaderWidth { get; set; }

		private DualKeyDictionary<string, int, PropertyTableRow> _rows = new DualKeyDictionary<string, int, PropertyTableRow>();

		public PropertyTable()
		{
			RemoveCaption = "Remove";
			InitializeComponent();

			recAdd.RecordFilter = FilterControlsToData;
			recAdd.RecordType = typeof(PropertyRecord);
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

		private void recAdd_RecordChanged(object sender, IRecord record)
		{
			if (Data == null)
			{
				return;
			}
			PropertyRecord result = record as PropertyRecord;
			if (result != null)
			{
				AddControl(result);
			}
		}

		private void AddControl(PropertyRecord result)
		{
			AddControl(result, null);
		}
		private void AddControl(PropertyRecord result, PropertyMacro macro)
		{
			bool newlyAdded;
			PropertyEditControl ctl = EditRecord(result, -1, out newlyAdded);
			ctl.Focus(); //jump to the control for immediate editing
			recAdd.Record = null;
			if (macro != null)
			{
				ctl.ApplyMacro(macro.Values);
				newlyAdded = false;
			}
			if (newlyAdded && RunInitialAddEvents)
			{
				ctl.OnInitialAdd();
			}
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

				foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
				{
					foreach (PropertyTableRow row in kvp.Value.Values)
					{
						DisposeRow(row);
					}
				}
				_rows.Clear();

				if (Data == null) { return; }

				bool addDefaults = true;
				List<PropertyEditControl> defaultRows = new List<PropertyEditControl>();

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
					bool isDefault = editControl.Default;
					bool hasData = true;

					MemberInfo member = editControl.Member;
					object value = member.GetValue(Data);
					Type memberType = member.GetDataType();
					if (memberType == typeof(int) && (int)value == 0 ||
						memberType == typeof(bool) && (bool)value == false ||
						value == null)
					{
						hasData = false;
						bool favorited = _favoriteRecords.Contains(editControl);
						if (!favorited && !required && (!addDefaults || !isDefault))
						{
							//skip the field if it has no value and is not favorited, required, or default
							continue;
						}
					}

					bool newlyAdded;
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
						if (!required && hasData)
						{
							addDefaults = false;

							//found something not required, so get rid of any default fields added
							foreach (PropertyEditControl defaultCtl in defaultRows)
							{
								PropertyTableRow row = _rows.Get(defaultCtl.Property, defaultCtl.Index);
								RemoveRow(row);
							}

							if (isDefault && !hasData)
							{
								continue;
							}
						}

						PropertyEditControl ctl = EditRecord(editControl, -1, out newlyAdded);
						if (isDefault && !hasData)
						{
							defaultRows.Add(ctl);
						}
					}
				}

				BuildSpeedMenus(groups);
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
							index = list.Count - 1;
						}
					}
				}

				newControl = true;
				ctl = Activator.CreateInstance(result.EditControlType) as PropertyEditControl;
				ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				ctl.SetParameters(result.Attribute);

				ctl.SetData(Data, result.Property, index, Context);

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
			}

			return ctl;
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
					MacroChanged?.Invoke(this, args.Macro);
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
				if (RecordFilter == null || RecordFilter(record))
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
		public void RunFilter(Func<PropertyRecord, object, object, bool> filter)
		{
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					row.Visible = filter(row.Record, Data, Context);
				}
			}
		}

		private ToolStripMenuItem GetOrAddGroupMenu(string group)
		{
			ToolStripMenuItem groupMenu = null;
			for (int i = 0; i < menuSpeedButtons.Items.Count; i++)
			{
				ToolStripItem mnuItem = menuSpeedButtons.Items[i];
				if (mnuItem.Text == group)
				{
					groupMenu = mnuItem as ToolStripMenuItem;
					break;
				}
			}
			if (groupMenu == null)
			{
				groupMenu = new ToolStripMenuItem(group);
				menuSpeedButtons.Items.Add(groupMenu);
			}
			return groupMenu;
		}

		public void AddSpeedButton(string group, string caption, Func<object, string> propertyCreator)
		{
			ToolStripMenuItem groupMenu = GetOrAddGroupMenu(group);

			ToolStripMenuItem item = new ToolStripMenuItem(caption);
			item.Tag = propertyCreator;
			item.Click += CustomSpeedButtonClick;
			groupMenu.DropDownItems.Add(item);
		}

		private void CustomSpeedButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Func<object, string> func = item.Tag as Func<object, string>;
			string property = func(Data);
			PropertyRecord record = PropertyProvider.GetEditControls(Data.GetType()).FirstOrDefault(r => r.Property == property);
			if (record != null)
			{
				AddControl(record);
			}
		}

		public void AddMacros()
		{
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

			ToolStripMenuItem groupMenu = GetOrAddGroupMenu("Macros");
			_macroMenu = groupMenu;
			ToolStripMenuItem item = new ToolStripMenuItem(macro.Name);
			item.Tag = macro;
			item.Click += MacroButtonClick;
			groupMenu.DropDownItems.Add(item);
		}

		private void MacroButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Macro macro = item.Tag as Macro;
			ApplyMacro(macro);
		}

		/// <summary>
		/// Applies a macro to the data
		/// </summary>
		/// <param name="macro"></param>
		internal void ApplyMacro(Macro macro)
		{
			foreach (PropertyMacro application in macro.Properties)
			{
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
	}
}
