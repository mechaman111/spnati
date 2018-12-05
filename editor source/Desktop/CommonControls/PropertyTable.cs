using Desktop.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
			}
		}

		/// <summary>
		/// Filter for hiding records from the record select
		/// </summary>
		public Func<PropertyRecord, bool> RecordFilter;

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

		/// <summary>
		/// Caption for remove buttons
		/// </summary>
		public string RemoveCaption { get; set; }

		public bool UseAutoComplete
		{
			get { return recAdd.UseAutoComplete; }
			set
			{
				recAdd.UseAutoComplete = value;
			}
		}

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
			PropertyEditControl ctl = EditRecord(result, -1);
			ctl.Focus(); //jump to the control for immediate editing
			recAdd.Record = null;
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
			foreach (KeyValuePair<string, Dictionary<int, PropertyTableRow>> kvp in _rows)
			{
				foreach (PropertyTableRow row in kvp.Value.Values)
				{
					DisposeRow(row);
				}
			}
			_rows.Clear();

			if (Data == null) { return; }

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

				MemberInfo member = editControl.Member;
				object value = member.GetValue(Data);
				Type memberType = member.GetDataType();
				if (memberType == typeof(int) && (int)value == 0 ||
					value == null)
				{
					continue;
				}

				if (typeof(IList).IsAssignableFrom(memberType))
				{
					IList list = value as IList;
					if (list.Count == 0)
					{
						continue;
					}
					for (int i = 0; i < list.Count; i++)
					{
						EditRecord(editControl, i);
					}
				}
				else
				{
					EditRecord(editControl, -1);
				}
			}

			BuildSpeedMenus(groups);
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

		private PropertyEditControl EditRecord(PropertyRecord result, int index)
		{
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

				ctl = Activator.CreateInstance(result.EditControlType) as PropertyEditControl;
				ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
				ctl.SetParameters(result.Attribute);

				ctl.SetData(Data, result.Property, index, Context);

				row = new PropertyTableRow();
				row.PropertyChanged += Row_PropertyChanged;
				row.RemoveRow += Row_RemoveRow;
				row.Dock = DockStyle.Top;
				row.RemoveCaption = RemoveCaption;
				row.Set(ctl, result);
				_rows.Set(result.Property, index, row);

				pnlRecords.Controls.Add(row);
				pnlRecords.Controls.SetChildIndex(row, 0);
			}

			return ctl;
		}

		private void DisposeRow(PropertyTableRow row)
		{
			pnlRecords.Controls.Remove(row);
			row.PropertyChanged -= Row_PropertyChanged;
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
		}

		private void Row_RemoveRow(object sender, EventArgs e)
		{
			PropertyTableRow row = sender as PropertyTableRow;
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

		private void SpeedButtonClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			PropertyRecord record = item.Tag as PropertyRecord;
			AddControl(record);
		}
	}
}
