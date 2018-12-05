using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public partial class RecordLookup : Form
	{
		private static Dictionary<Type, IRecordProvider> _recordProviders;

		public static IRecord Get(Type type, string key, bool allowCreate, object recordContext)
		{
			if (_recordProviders == null)
			{
				PrepRecordProviders();
			}

			IRecordProvider provider;
			if (_recordProviders.TryGetValue(type, out provider))
			{
				provider.SetContext(recordContext);
				List<IRecord> records = provider.GetRecords(key);
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
			return DoLookup(type, text, allowCreate, null, false, null);
		}

		public static IRecord DoLookup(Type type, string text, bool allowCreate, Func<IRecord, bool> filter, object recordContext)
		{
			return DoLookup(type, text, allowCreate, filter, false, recordContext);
		}

		public static IRecord DoLookup(Type type, string text, bool allowCreate, Func<IRecord, bool> filter, bool forceOpen, object recordContext)
		{
			if (_recordProviders == null)
			{
				PrepRecordProviders();
			}

			IRecordProvider provider;
			if (_recordProviders.TryGetValue(type, out provider))
			{
				provider.SetContext(recordContext);

				if (!forceOpen && (!allowCreate || !provider.AllowsNew))
				{
					//No point in bringing up the form if there's only one record
					List<IRecord> records = provider.GetRecords(text);
					if (records.Count == 1 && (filter == null || filter(records[0])))
					{
						return records[0];
					}
				}
			}

			RecordLookup form = new RecordLookup();
			form.AllowCreate = allowCreate;
			form.Text = provider.GetLookupCaption();
			form.SetContext(type, text);
			form.Filter = filter;
			if (form.ShowDialog() == DialogResult.OK)
			{
				return form.Record;
			}
			else
			{
				return null;
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
				foreach (IRecord record in provider.GetRecords(""))
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

		private bool _loading = false;
		private Type _recordType;
		private IRecordProvider _provider;
		private IRecord Record { get; set; }

		private static Dictionary<Type, string> _lastLookups = new Dictionary<Type, string>();

		private bool _allowCreate = true;
		public bool AllowCreate
		{
			get { return _allowCreate; }
			set
			{
				_allowCreate = value;
				//cmdDelete.Visible = _allowCreate;
				cmdNew.Visible = _allowCreate;
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

		public void SetContext(Type recordType, string startText)
		{
			_loading = true;
			_recordType = recordType;
			if (!_recordProviders.TryGetValue(recordType, out _provider))
			{
				throw new ArgumentException($"Record type {recordType.Name} has no record provider, so record lookup cannot be used.");
			}

			lstItems.Columns.Clear();
			foreach (string col in _provider.GetColumns())
			{
				int width = (lstItems.Columns.Count == 0 ? 150 : -2);
				lstItems.Columns.Add(col, width);
			}
			if (lstItems.Columns.Count == 0)
			{
				lstItems.View = View.Tile;
			}
			else
			{
				lstItems.View = View.Details;
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

		private void UpdateSearch()
		{
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
			cmdNew.Enabled = false;
			AcceptButton = cmdAccept;
			List<IRecord> records = _provider.GetRecords(txtName.Text);
			_provider.Sort(records);
			lstItems.Items.Clear();
			lstItems.Groups.Clear();
			Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();
			bool foundMatch = false;
			foreach (IRecord record in records)
			{
				if (Filter == null || Filter(record))
				{
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

			if (lstItems.Groups.Count == 0 && lstItems.Columns.Count == 1)
			{
				lstItems.View = View.List;
			}

			if (!foundMatch)
			{
				if (lstItems.Items.Count > 0)
				{
					lstItems.SelectedIndices.Add(0);
				}
				else
				{
					lstItems.SelectedIndices.Clear();
				}
			}
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			if (txtName.Text.Length == 0 && lstItems.Items.Count == 0)
			{
				return;
			}

			if (lstItems.Items.Count == 0 && _allowCreate)
			{
				//If there are no items, enable Create New
				cmdNew.Enabled = true;
				AcceptButton = cmdNew;
				return;
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
				Record = lstItems.SelectedItems[0].Tag as IRecord;
				_lastLookups[_recordType] = Record.Key;
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
	}
}
