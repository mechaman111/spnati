using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Desktop
{
	public partial class RecordLookup : SkinnedForm
	{
		private static Dictionary<Type, IRecordProvider> _recordProviders;
		private static Dictionary<Type, List<IRecord>> _recentRecords = new Dictionary<Type, List<IRecord>>();

		public static bool IsOpen { get; set; }

		public static IRecord Get(Type type, string key, bool allowCreate, object recordContext)
		{
			if (key == null) { return null; }
			if (_recordProviders == null)
			{
				PrepRecordProviders();
			}

			IRecordProvider provider;
			if (_recordProviders.TryGetValue(type, out provider))
			{
				provider.SetContext(recordContext);
				List<IRecord> records = provider.GetRecords(key, new LookupArgs());
				foreach (IRecord record in records)
				{
					if (record.Key == key)
					{
						return record;
					}
				}
				if (allowCreate)
				{
					return provider.Create(key);
				}
			}
			return null;
		}

		public static IRecord DoLookup(Type type, string text)
		{
			return DoLookup(type, text, true, null);
		}

		public static IRecord DoLookup(Type type, string text, bool allowCreate, object recordContext)
		{
			return DoLookup(type, text, allowCreate, null, false, recordContext);
		}

		public static IRecord DoLookup(Type type, string text, bool allowCreate, Func<IRecord, bool> filter, object recordContext)
		{
			return DoLookup(type, text, allowCreate, filter, false, recordContext);
		}

		public static IRecord DoLookup(Type type, string text, bool allowCreate, Func<IRecord, bool> filter, bool forceOpen, object recordContext)
		{
			IsNewRecord = false;
			if (_recordProviders == null)
			{
				PrepRecordProviders();
			}

			IRecordProvider provider;
			if (_recordProviders.TryGetValue(type, out provider))
			{
				bool showRecent = provider.TrackRecent;
				provider.SetContext(recordContext);

				if (!forceOpen/* && (!allowCreate || !provider.AllowsNew)*/)
				{
					//No point in bringing up the form if there's only one record
					List<IRecord> records = provider.GetRecords(text, new LookupArgs());
					IRecord exactMatch = records.Find(r => r.Key == text);
					if (exactMatch != null && (filter == null || filter(exactMatch)))
					{
						if (showRecent)
						{
							AddToRecent(type, exactMatch);
						}
						return exactMatch;
					}
					if (records.Count == 1 && (filter == null || filter(records[0])))
					{
						if (showRecent)
						{
							AddToRecent(type, records[0]);
						}
						return records[0];
					}
				}
			}

			LookupFormat formatData = new LookupFormat();
			provider.SetFormatInfo(formatData);
			RecordLookup form = new RecordLookup();
			form.AllowCreate = allowCreate;
			form.AllowDelete = allowCreate && provider.AllowsDelete;
			form.Text = formatData.Caption;
			form.SetFields(formatData.ExtraFields);
			form.SetContext(type, text, filter, formatData);
			form.Filter = filter;
			IsOpen = true;
			if (form.ShowDialog() == DialogResult.OK)
			{
				IsOpen = false;
				return form.Record;
			}
			else
			{
				IsOpen = false;
				return null;
			}
		}

		/// <summary>
		/// Replaces a recent record with another, if it exists
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="record"></param>
		public static void ReplaceRecent<T>(T record) where T : IRecord
		{
			List<IRecord> records;
			if (_recentRecords.TryGetValue(typeof(T), out records))
			{
				for (int i = 0; i < records.Count; i++)
				{
					if (records[i].Key == record.Key)
					{
						records[i] = record;
						break;
					}
				}
			}
		}

		public static void AddToRecent(Type type, IRecord record)
		{
			List<IRecord> records;
			if (!_recentRecords.TryGetValue(type, out records))
			{
				records = new List<IRecord>();
				_recentRecords[type] = records;
			}
			for (int i = records.Count - 1; i >= 0; i--)
			{
				if (records[i].Key == record.Key)
				{
					records.RemoveAt(i);
				}
			}
			records.Add(record);
			const int MaxRecent = 3;
			while (records.Count > MaxRecent)
			{
				records.RemoveAt(0);
			}
		}

		public static IEnumerable<IRecord> GetAllRecords(Type type, Func<IRecord, bool> filter, object recordContext)
		{
			if (_recordProviders == null)
			{
				PrepRecordProviders();
			}

			List<IRecord> records = new List<IRecord>();
			IRecordProvider provider;
			if (_recordProviders.TryGetValue(type, out provider))
			{
				provider.SetContext(recordContext);
				foreach (IRecord record in provider.GetRecords("", new LookupArgs()))
				{
					if (filter == null || filter(record))
					{
						yield return record;
					}
				}
			}
		}

		private static void PrepRecordProviders()
		{
			_recordProviders = new Dictionary<Type, IRecordProvider>();
			foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				string name = assembly.FullName;
				if (name.StartsWith("Microsoft") || name.StartsWith("Newtonsoft") || name.StartsWith("ms") || name.StartsWith("System"))
				{
					continue;
				}

				foreach (Type type in assembly.GetTypes())
				{
					if (typeof(IRecordProvider).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType)
					{
						IRecordProvider provider = Activator.CreateInstance(type) as IRecordProvider;
						Type providerInterface = type.GetInterface(typeof(IRecordProvider<IRecord>).Name);
						Type recordType = providerInterface.GenericTypeArguments.GetValue(0) as Type;
						_recordProviders[recordType] = provider;
					}
				}
			}
		}

		public RecordLookup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets if the last record looked up was newly created
		/// </summary>
		public static bool IsNewRecord { get; private set; }

		private bool _loading = false;
		private bool _usingRecent = false;
		private Type _recordType;
		private IRecordProvider _provider;
		private IRecord Record { get; set; }
		private List<SkinnedTextBox> _extraFields = new List<SkinnedTextBox>();

		private static Dictionary<Type, string> _lastLookups = new Dictionary<Type, string>();

		private bool _allowCreate = true;
		public bool AllowCreate
		{
			get { return _allowCreate; }
			set
			{
				_allowCreate = value;
				cmdNew.Visible = _allowCreate;
			}
		}

		private bool _allowDelete = true;
		public bool AllowDelete
		{
			get { return _allowDelete; }
			set
			{
				_allowDelete = value;
				cmdDelete.Visible = _allowDelete;
			}
		}

		public Func<IRecord, bool> Filter;

		/// <summary>
		/// Gets the key of whatever record was lasted looked up
		/// </summary>
		/// <typeparam name="T">Type of record to lookup</typeparam>
		/// <returns></returns>
		public static string GetLastLookup<T>() where T : IRecord
		{
			string key;
			_lastLookups.TryGetValue(typeof(T), out key);
			return key;
		}

		private void SetContext(Type recordType, string startText, Func<IRecord, bool> filter, LookupFormat formatData)
		{
			_loading = true;
			_recordType = recordType;
			if (!_recordProviders.TryGetValue(recordType, out _provider))
			{
				throw new ArgumentException($"Record type {recordType.Name} has no record provider, so record lookup cannot be used.");
			}

			lstRecent.HeaderStyle = ColumnHeaderStyle.None;
			lstRecent.Columns.Clear();
			lstItems.Columns.Clear();
			string[] cols = formatData.Columns;
			int[] widths = formatData.ColumnWidths;
			for (int i = 0; i < cols.Length; i++)
			{
				string col = cols[i];
				int width = (i == 0 ? 150 : i < cols.Length - 1 ? 100 : -2);
				if (widths != null)
				{
					width = widths[i];
				}
				lstItems.Columns.Add(col, width);
				lstRecent.Columns.Add(col, width);
			}
			if (lstItems.Columns.Count == 0)
			{
				lstItems.View = View.Tile;
				lstRecent.View = View.Tile;
			}
			else
			{
				lstItems.View = View.Details;
				lstRecent.View = View.Details;
			}

			if (_provider.TrackRecent)
			{
				List<IRecord> records;
				if (!_recentRecords.TryGetValue(recordType, out records))
				{
					records = new List<IRecord>();
					_recentRecords[recordType] = records;
				}
				foreach (IRecord record in records)
				{
					if (filter == null || filter(record))
					{
						ListViewItem item = _provider.FormatItem(record);
						item.Tag = record;
						lstRecent.Items.Insert(0, item);
					}
				}
			}
			else
			{
				lblRecent.Visible = false;
				lstRecent.Visible = false;
				lstItems.Height = lstRecent.Bottom - lstItems.Top;
			}

			txtName.Text = startText;
			_loading = false;
		}

		private void RecordLookup_Shown(object sender, EventArgs e)
		{
			UpdateSearch();
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			if (_loading)
			{
				return;
			}

			UpdateSearch();
		}

		private LookupArgs GetLookupArgs()
		{
			LookupArgs args = new LookupArgs();
			args.ExtraText = _extraFields.Select(box => box.Text).ToArray();
			return args;
		}

		private void UpdateSearch()
		{
			_usingRecent = false;
			if (txtName.Text == "=")
			{
				string lastEntry;
				if (_lastLookups.TryGetValue(_recordType, out lastEntry))
				{
					txtName.Text = lastEntry;
				}
				else
				{
					txtName.Text = "";
				}
				return;
			}
			cmdNew.Enabled = (txtName.Text.Length > 0);
			AcceptButton = cmdAccept;
			List<IRecord> records = _provider.GetRecords(txtName.Text, GetLookupArgs());
			_provider.Sort(records);
			lstItems.Items.Clear();
			lstItems.Groups.Clear();
			Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();
			bool foundMatch = false;
			foreach (IRecord record in records)
			{
				if (Filter == null || Filter(record))
				{
					if (_provider.FilterFromUI(record))
					{
						continue;
					}
					ListViewItem item = _provider.FormatItem(record);
					item.Tag = record;

					if (record.Name?.ToLower() == txtName.Text.ToLower() || record.Key.ToLower() == txtName.Text.ToLower())
					{
						//if an exact match, insert at the start
						lstItems.Items.Insert(0, item);
						foundMatch = true;
						lstItems.SelectedIndices.Add(0);
					}
					else
					{
						lstItems.Items.Add(item);
					}

					ListViewGroup group = null;
					string groupName = record.Group;
					if (!string.IsNullOrEmpty(groupName))
					{
						if (!groups.ContainsKey(groupName))
						{
							group = new ListViewGroup(groupName);
							lstItems.Groups.Add(group);
							groups[groupName] = group;
						}
						else
						{
							group = groups[groupName];
						}
						item.Group = group;
					}
				}
			}

			//if (lstItems.Groups.Count == 0 && lstItems.Columns.Count == 1)
			//{
			//	lstItems.View = View.List;
			//}

			if (!foundMatch)
			{
				if (lstItems.Items.Count > 0)
				{
					lstItems.SelectedIndices.Add(0);
				}
				else
				{
					lstItems.SelectedIndices.Clear();
					if (!string.IsNullOrEmpty(txtName.Text))
					{
						cmdNew.Enabled = true;
						AcceptButton = cmdNew;
					}
				}
			}
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			if (txtName.Text.Length == 0 && lstItems.Items.Count == 0 && !_usingRecent)
			{
				return;
			}

			IRecord selectedRecord = null;
			if (lstItems.Items.Count == 0 && !_usingRecent && _allowCreate)
			{
				//If there are no items, enable Create New
				cmdNew.Enabled = true;
				AcceptButton = cmdNew;
				return;
			}
			else if (_usingRecent && lstRecent.SelectedIndices.Count > 0)
			{
				selectedRecord = lstRecent.SelectedItems[0].Tag as IRecord;
			}
			else if (lstItems.SelectedIndices.Count == 0 && lstItems.Items.Count > 0)
			{
				//If there are items, but none are selected, select the first one
				lstItems.SelectedIndices.Add(0);
				return;
			}
			else if (lstItems.Items.Count > 0)
			{
				//If an item is selected, use it
				selectedRecord = lstItems.SelectedItems[0].Tag as IRecord;
			}

			if (selectedRecord != null)
			{
				Record = selectedRecord;
				_lastLookups[_recordType] = Record.Key;
				if (_provider.TrackRecent)
				{
					AddToRecent(_recordType, Record);
				}
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void cmdNew_Click(object sender, EventArgs e)
		{
			if (!_allowCreate)
				return;
			if (txtName.Text.Length == 0)
				return;
			Record = _provider.Create(txtName.Text);
			IsNewRecord = true;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void lstItems_DoubleClick(object sender, EventArgs e)
		{
			_usingRecent = false;
			cmdAccept_Click(this, new EventArgs());
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			if (!_allowCreate)
				return;
			if (lstItems.SelectedIndices.Count == 0)
				return;
			IRecord record = lstItems.SelectedItems[0].Tag as IRecord;
			if (record != null)
			{
				_provider.Delete(record);
				UpdateSearch();
			}
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (lstItems.Items.Count > 0)
			{
				if (e.KeyCode == Keys.Down)
				{
					if (lstItems.SelectedIndices.Count == 0)
					{
						lstItems.SelectedIndices.Add(0);
					}
					else
					{
						int index = lstItems.SelectedIndices[0];
						index++;
						if (index < lstItems.Items.Count)
						{
							lstItems.SelectedIndices.Clear();
							lstItems.SelectedIndices.Add(index);
						}
					}
				}
				else if (e.KeyCode == Keys.Up)
				{
					if (lstItems.SelectedIndices.Count == 0)
					{
						lstItems.SelectedIndices.Add(0);
					}
					else
					{
						int index = lstItems.SelectedIndices[0];
						index--;
						if (index >= 0)
						{
							lstItems.SelectedIndices.Clear();
							lstItems.SelectedIndices.Add(index);
						}
					}
				}
			}
		}

		private void lstRecent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstRecent.SelectedIndices.Count > 0)
			{
				_usingRecent = true;
			}
		}

		private void lstRecent_DoubleClick(object sender, EventArgs e)
		{
			_usingRecent = true;
			cmdAccept_Click(this, new EventArgs());
		}

		public static List<IRecord> GetRecentRecords<T>()
		{
			List<IRecord> records;
			if (!_recentRecords.TryGetValue(typeof(T), out records))
			{
				records = new List<IRecord>();
			}
			return records;
		}

		public void SetFields(List<string> fields)
		{
			int tabIndex = txtName.TabIndex + 1;
			foreach (string name in fields)
			{
				SkinnedLabel label = new SkinnedLabel()
				{
					Text = name
				};
				SkinnedTextBox box = new SkinnedTextBox();
				Controls.Add(label);
				Controls.Add(box);
				label.Top = lstItems.Top + 3;
				label.Left = label1.Left;
				label.Width = label1.Width;
				label.TabIndex = tabIndex++;
				box.TabIndex = tabIndex++;
				box.Top = lstItems.Top;
				box.Left = txtName.Left;
				box.Width = txtName.Width;
				box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				box.TextChanged += txtName_TextChanged;

				const int Margin = 0;
				lstItems.Top = label.Bottom + Margin;
				lstItems.Height -= label.Height + Margin;

				_extraFields.Add(box);
			}
		}
	}
}
