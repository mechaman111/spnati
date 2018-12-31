using Desktop.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop
{
	public partial class Shell : Form
	{
		private static bool _busy = false;

		const int LEADING_SPACE = 12;
		const int CLOSE_SPACE = 15;
		const int CLOSE_AREA = 15;

		private int _nextId;
		private bool _forceSwitch = false;

		private int _workspaceTop;

		public static Shell Instance { get; private set; }

		public PostOffice PostOffice = new PostOffice();

		private Dictionary<Type, SortedList<int, Type>> _recordToActivityMap;
		private Dictionary<Type, Type> _recordToWorkspaceMap;
		private Dictionary<Type, WorkspacePane> _activityToPaneMap;
		private Dictionary<Type, int> _activityWidthMap;
		private Dictionary<IWorkspace, WorkspaceControl> _workspaces = new Dictionary<IWorkspace, WorkspaceControl>();
		private Dictionary<IWorkspace, TabPage> _tabs = new Dictionary<IWorkspace, TabPage>();
		public IWorkspace ActiveWorkspace;
		public IActivity ActiveActivity;
		public IActivity ActiveSidebarActivity;

		/// <summary>
		/// Iterates through all open workspaces. Do not try launching or closing workspaces while iterating over this
		/// </summary>
		public IEnumerable<IWorkspace> Workspaces
		{
			get { return _workspaces.Keys; }
		}

		/// <summary>
		/// Frequency to raise the AutoTick event in milliseconds
		/// </summary>
		public int AutoTickFrequency
		{
			get { return tmrAutoTick.Interval; }
			set
			{
				tmrAutoTick.Enabled = (value > 0);
				value = Math.Max(1, value);
				tmrAutoTick.Interval = value;
			}
		}
		public event EventHandler AutoTick;

		private List<IActivity> _activationOrder = new List<IActivity>();

		public Shell(string caption, Icon icon)
		{
			Instance = this;
			InitializeComponent();

			Text = caption;
			Icon = icon;

			_workspaceTop = tabWorkspaces.Top;
			SetStatus("");
		}

		private void Shell_Load(object sender, EventArgs e)
		{
			BuildWorkspaceMap();

			////Load data
			//Settings.Load();
			//DataLoader.Load(Settings.DataSet);
		}

		public static void ShowMessage(string message)
		{
			_busy = true;
			MessageBox.Show(message);
			_busy = false;
		}

		public static DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons)
		{
			_busy = true;
			DialogResult result = MessageBox.Show(message, title, buttons);
			_busy = false;
			return result;
		}

		public static void ShowMessageWithCallback(int number, string message, Action callback)
		{
			ShowMessage(message);
			callback();
		}

		public static void ShowForm(Form form)
		{
			_busy = true;
			form.ShowDialog();
			_busy = false;
		}

		private void BuildWorkspaceMap()
		{
			_recordToWorkspaceMap = new Dictionary<Type, Type>();
			_recordToActivityMap = new Dictionary<Type, SortedList<int, Type>>();
			_activityToPaneMap = new Dictionary<Type, WorkspacePane>();
			_activityWidthMap = new Dictionary<Type, int>();
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				string name = assembly.FullName;
				if (name.StartsWith("Microsoft") || name.StartsWith("Newtonsoft") || name.StartsWith("mscor") || name.StartsWith("System"))
					continue;
				foreach (var type in assembly.GetTypes())
				{
					WorkspaceAttribute attr = type.GetCustomAttribute<WorkspaceAttribute>();
					if (attr != null)
					{
						if (_recordToWorkspaceMap.ContainsKey(attr.RecordType))
						{
							throw new CustomAttributeFormatException($"Record type {attr.RecordType.Name} already has multiple Workspaces defined.");
						}
						_recordToWorkspaceMap[attr.RecordType] = type;
					}
					foreach (ActivityAttribute attr2 in type.GetCustomAttributes<ActivityAttribute>())
					{
						if (attr2 != null)
						{
							SortedList<int, Type> activities;
							if (!_recordToActivityMap.TryGetValue(attr2.RecordType, out activities))
							{
								activities = new SortedList<int, Type>(new DuplicateKeyComparer<int>());
								_recordToActivityMap[attr2.RecordType] = activities;
							}
							activities.Add(attr2.Order, type);

							_activityToPaneMap[type] = attr2.Pane;
							_activityWidthMap[type] = attr2.Width;
						}
					}
				}
			}
		}

		private void CreateToolbar()
		{
			AddToolbarSeparator();
		}

		#region Toolbar
		public ToolStripMenuItem AddToolbarSubmenu(string caption)
		{
			return AddToolbarSubmenu(caption, null);
		}

		public ToolStripMenuItem AddToolbarSubmenu(string caption, ToolStripMenuItem submenu)
		{
			ToolStripMenuItem item = new ToolStripMenuItem(caption);
			if (submenu == null)
			{
				toolbar.Items.Add(item);
			}
			else
			{
				submenu.DropDownItems.Add(submenu);
			}
			return item;
		}

		public ToolStripMenuItem AddToolbarItem(string caption, Type recordType, ToolStripMenuItem submenu)
		{
			return AddToolbarItem(caption, recordType, null, submenu);
		}
		public ToolStripMenuItem AddToolbarItem(string caption, Type recordType)
		{
			return AddToolbarItem(caption, recordType, null, null);
		}
		public ToolStripMenuItem AddToolbarItem(string caption, Type recordType, Func<IRecord> recordProvider)
		{
			return AddToolbarItem(caption, recordType, recordProvider, null);
		}
		public ToolStripMenuItem AddToolbarItem(string caption, Action clickHandler, Keys shortcut)
		{
			return AddToolbarItem(caption, clickHandler, null, shortcut);
		}
		public ToolStripMenuItem AddToolbarItem(string caption, Action clickHandler, ToolStripMenuItem submenu, Keys shortcut)
		{
			ToolStripMenuItem item = new ToolStripMenuItem(caption);
			item.Tag = clickHandler;
			item.Click += OnClickAction;
			if (submenu == null)
			{
				toolbar.Items.Add(item);
			}
			else
			{
				submenu.DropDownItems.Add(item);
			}
			item.ShortcutKeys = shortcut;
			return item;
		}

		public ToolStripMenuItem AddToolbarItem(string caption, Type recordType, Func<IRecord> recordProvider, ToolStripMenuItem submenu)
		{
			ToolStripMenuItem item = new ToolStripMenuItem(caption);
			if (recordProvider != null)
			{
				item.Tag = new Tuple<Type, Func<IRecord>>(recordType, recordProvider);
				item.Click += OnClickProvidedToolbarItem;
			}
			else
			{
				item.Tag = new Tuple<Type, string>(recordType, "");
				item.Click += OnClickToolbarItem;
			}

			if (submenu == null)
				toolbar.Items.Add(item);
			else submenu.DropDownItems.Add(item);
			return item;
		}

		public void AddToolbarSeparator(ToolStripMenuItem submenu = null)
		{
			ToolStripSeparator sep = new ToolStripSeparator();
			if (submenu == null)
				toolbar.Items.Add(sep);
			else submenu.DropDownItems.Add(sep);
		}

		private void OnClickAction(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			Action action = item.Tag as Action;
			action?.Invoke();
		}

		private void OnClickToolbarItem(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			Tuple<Type, string> tuple = item.Tag as Tuple<Type, string>;
			Type recordType = tuple.Item1;
			if (recordType != null)
			{
				LaunchWorkspace(recordType, tuple.Item2);
			}
		}

		private void OnClickProvidedToolbarItem(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			Tuple<Type, Func<IRecord>> tuple = item.Tag as Tuple<Type, Func<IRecord>>;
			Type recordType = tuple.Item1;
			if (recordType != null)
			{
				IRecord record = tuple.Item2();
				LaunchWorkspace(recordType, record);
			}
		}
		#endregion

		private void tabWorkspaces_DrawItem(object sender, DrawItemEventArgs e)
		{
			IWorkspace ws = tabWorkspaces.TabPages[e.Index].Tag as IWorkspace;
			if (!ws.IsDefault)
			{
				e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
			}
			e.Graphics.DrawString(tabWorkspaces.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4);
			e.DrawFocusRectangle();
		}

		private void tabWorkspaces_MouseClick(object sender, MouseEventArgs e)
		{
			//See if an X on a tab was clicked
			for (int i = 0; i < tabWorkspaces.TabPages.Count; i++)
			{
				Rectangle r = tabWorkspaces.GetTabRect(i);
				Rectangle closeButton = new Rectangle(r.Right - CLOSE_AREA, r.Top, CLOSE_AREA, 20);
				if (closeButton.Contains(e.Location))
				{
					IWorkspace ws = tabWorkspaces.TabPages[i].Tag as IWorkspace;
					if (!ws.IsDefault)
					{
						CloseWorkspace(ws);
					}
				}
			}
		}

		/// <summary>
		/// Launches to a specific activity, opening the workspace if necessary
		/// </summary>
		/// <typeparam name="TRecord"></typeparam>
		/// <typeparam name="TActivity"></typeparam>
		/// <param name="record"></param>
		/// <param name="parameters"></param>
		public void Launch<TRecord, TActivity>(TRecord record, params object[] parameters) where TRecord : IRecord where TActivity : IActivity
		{
			bool changingWorkspace = ActiveWorkspace?.Record != (IRecord)record;

			if (!changingWorkspace && ActiveActivity.GetType() == typeof(TActivity))
			{
				//already active, so just pass new run parameters
				ActiveActivity.UpdateParameters(parameters);
				return;
			}

			DeactivateArgs args = new DeactivateArgs(DeactivateReason.SwitchingActivities);
			if (ActiveActivity != null && !ActiveActivity.CanDeactivate(args))
				return;

			IWorkspace workspace = null;
			if (changingWorkspace)
			{
				//only need to validate sidebar if switching workspaces
				if (ActiveSidebarActivity != null && !ActiveSidebarActivity.CanDeactivate(args))
					return;

				workspace = FindWorkspace(typeof(TRecord), record.Key);
				if (workspace == null)
				{
					//need to launch the workspace
					workspace = CreateWorkspace(record);
				}
			}
			else
			{
				workspace = ActiveWorkspace;
			}

			//Activate the workspace and its first activity
			IActivity activity = workspace.Find<TActivity>();
			if (activity == null)
			{
				throw new NotImplementedException("No support for launching activities into an open workspace yet.");
			}
			Activate(activity);
			activity.UpdateParameters(parameters);

			if (changingWorkspace && activity.Pane == WorkspacePane.Main)
			{
				//Also need to mark the sidebar as active too
				activity = workspace.GetFirstSidebarActivity();
				if (activity != null)
				{
					Activate(activity);
				}
			}
		}

		public void LaunchWorkspace<T>(T record, params object[] parameters) where T : IRecord
		{
			LaunchWorkspace(typeof(T), record, false, parameters);
		}

		public void LaunchWorkspace(Type type, params object[] parameters)
		{
			LaunchWorkspace(type, "", parameters);
		}

		/// <summary>
		/// Launches a workspace, or activates it if already open
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="recordKey"></param>
		public void LaunchWorkspace(Type type, string recordKey, params object[] parameters)
		{
			DeactivateArgs args = new DeactivateArgs(DeactivateReason.SwitchingWorkspaces);
			if (ActiveActivity != null && !ActiveActivity.CanDeactivate(args))
				return;
			if (ActiveSidebarActivity != null && !ActiveSidebarActivity.CanDeactivate(args))
				return;
			IWorkspace workspace = FindWorkspace(type, recordKey);
			if (workspace == null)
			{
				//open record lookup
				IRecord record = RecordLookup.DoLookup(type, recordKey);
				if (record == null)
					return; //no record to launch, so stop

				//one more chance to find the workspace
				workspace = FindWorkspace(type, record.Key);
				if (workspace == null)
				{
					//create the workspace
					workspace = CreateWorkspace(record);
				}
			}

			//Activate the workspace and its first activity
			IActivity activity = workspace.GetFirstActivity();
			Activate(activity);
			activity.UpdateParameters(parameters);

			//Also the sidebar
			activity = workspace.GetFirstSidebarActivity();
			if (activity != null)
			{
				Activate(activity);
			}
		}

		public void LaunchWorkspace(Type type, IRecord record, bool defaultWorkspace = false, params object[] parameters)
		{
			DeactivateArgs args = new DeactivateArgs(DeactivateReason.SwitchingWorkspaces);
			if (ActiveActivity != null && !ActiveActivity.CanDeactivate(args)
				|| ActiveSidebarActivity != null && !ActiveSidebarActivity.CanDeactivate(args)
				|| record == null)
				return;
			IWorkspace workspace = FindWorkspace(type, record.Key);
			if (workspace == null)
			{
				workspace = CreateWorkspace(record, defaultWorkspace);
			}

			//Activate the workspace and its first activity
			IActivity activity = workspace.GetFirstActivity();
			Activate(activity);
			activity.UpdateParameters(parameters);

			//Also the sidebar
			activity = workspace.GetFirstSidebarActivity();
			if (activity != null)
			{
				Activate(activity);
			}
		}

		private IWorkspace CreateWorkspace(IRecord record, bool defaultWorkspace = false)
		{
			IWorkspace workspace;
			Type workspaceType;
			if (!_recordToWorkspaceMap.TryGetValue(record.GetType(), out workspaceType))
			{
				workspaceType = typeof(Workspace);
			}
			workspace = Activator.CreateInstance(workspaceType) as IWorkspace;

			//initialize the workspace
			workspace.IsDefault = defaultWorkspace;
			workspace.Record = record;
			workspace.Id = ++_nextId;
			workspace.Initialize();

			CreateWorkspaceControl(workspace);
			return workspace;
		}

		/// <summary>
		/// Gets an opened workspace based on its record
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="record"></param>
		/// <returns></returns>
		public IWorkspace GetWorkspace<T>(T record) where T : IRecord
		{
			return FindWorkspace(typeof(T), record.Key);
		}

		/// <summary>
		/// Finds an opened workspace for a particular record
		/// </summary>
		/// <typeparam name="T">Record type</typeparam>
		/// <param name="key">Record key</param>
		/// <returns>The workspace, or null if not found</returns>
		private IWorkspace FindWorkspace(Type type, string key)
		{
			foreach (IWorkspace ws in _workspaces.Keys)
			{
				if (ws.Record.GetType() == type && ws.Record.Key == key)
					return ws;
			}
			return null;
		}

		/// <summary>
		/// Creates a workspace control for a workspace
		/// </summary>
		/// <param name="ws"></param>
		private void CreateWorkspaceControl(IWorkspace ws)
		{
			WorkspaceControl ctl = new WorkspaceControl();
			ctl.Workspace = ws;
			ctl.Dock = DockStyle.Fill;
			ws.Control = ctl;
			TabPage page = new TabPage();
			page.Tag = ws;
			page.Text = ws.Caption;
			page.Controls.Add(ctl);
			tabWorkspaces.TabPages.Add(page);
			_workspaces[ws] = ctl;
			_tabs[ws] = page;

			//Create the workspace's activities
			SortedList<int, Type> activities;
			if (_recordToActivityMap.TryGetValue(ws.Record.GetType(), out activities))
			{
				foreach (var type in activities.Values)
				{
					CreateActivityInWorkspace(ws, type);
				}
			}
		}

		private IActivity CreateActivityInWorkspace(IWorkspace workspace, Type activityType)
		{
			if (!typeof(IActivity).IsAssignableFrom(activityType))
				throw new ArgumentException($"Activity type {activityType.Name} does not derive from Activity.");

			IActivity a = Activator.CreateInstance(activityType) as IActivity;
			int width = _activityWidthMap[activityType];
			WorkspacePane pane = _activityToPaneMap[activityType];
			a.SidebarWidth = width;
			a.Pane = pane;
			a.Initialize(workspace, workspace.Record);

			WorkspaceControl ctl = _workspaces[workspace];
			ctl.AddActivity(a);
			workspace.AddActivity(a);
			return a;
		}

		/// <summary>
		/// Attempts to close a workspace
		/// </summary>
		/// <param name="ws">Workspace to close</param>
		/// <param name="silent">IF true, the workspace is closed without validating or saving anything in it</param>
		/// <returns>True if the worksapce was closed</returns>
		public bool CloseWorkspace(IWorkspace ws, bool silent = false)
		{
			if(!silent && !ws.CanQuit(CloseReason.ClosingWorkspace))
				return false;
			QuitWorkspace(ws, false);
			return true;
		}

		/// <summary>
		/// Quits a workspace
		/// </summary>
		/// <param name="ws"></param>
		private void QuitWorkspace(IWorkspace ws, bool shuttingDown)
		{
			//Remove this workspace's activities from the activation order
			for (int i = _activationOrder.Count - 1; i >= 0; i--)
			{
				if (_activationOrder[i].Workspace == ws)
					_activationOrder.RemoveAt(i);
			}

			ws.Quit();
			ws.Destroy();

			if (shuttingDown)
				return;

			ActiveWorkspace = null;
			ActiveActivity = null;
			ActiveSidebarActivity = null;
			if (_activationOrder.Count > 0)
				Activate(_activationOrder[_activationOrder.Count - 1]);

			//Remove the control
			TabPage tab = _tabs[ws];
			_tabs.Remove(ws);
			_workspaces.Remove(ws);
			tabWorkspaces.Controls.Remove(tab);
			tab.Dispose();
		}

		/// <summary>
		/// Activates an activity
		/// </summary>
		/// <param name="activity"></param>
		/// <returns></returns>
		public bool Activate(IActivity activity)
		{
			if (activity == null)
			{
				return true;
			}
			IActivity activeActivity = (activity.Pane == WorkspacePane.Main ? ActiveActivity : ActiveSidebarActivity);
			bool changingWorkspace = false;
			DeactivateReason reason = DeactivateReason.SwitchingActivities;
			if (activeActivity?.Workspace != activity.Workspace)
			{
				changingWorkspace = true;
				reason |= DeactivateReason.SwitchingWorkspaces;
			}
			DeactivateArgs activitySwitchArgs = new DeactivateArgs(reason);

			//Ensure the previous activity can switch
			if (activeActivity != null && !activeActivity.CanDeactivate(activitySwitchArgs))
			{
				return false;
			}

			//make sure the previous workspace is good
			if (changingWorkspace)
			{
				if (ActiveWorkspace != null)
				{
					if (!ActiveWorkspace.CanDeactivate(reason))
					{
						return false;
					}
				}
			}

			Cursor.Current = Cursors.WaitCursor;
			//save the previous activity if it wanted to, and deactivate
			if (activeActivity != null)
			{
				activeActivity.Save();
				if (activitySwitchArgs.SaveData)
				{
					activeActivity.Deactivate();
				}
			}
			if (ActiveWorkspace != null)
			{
				ActiveWorkspace.Deactivate();
			}

			//good to go; activate the new activity
			ActiveWorkspace = activity.Workspace;
			activeActivity = activity;
			switch (activity.Pane)
			{
				case WorkspacePane.Main:
					ActiveActivity = activity;
					if (ActiveWorkspace != null)
						ActiveWorkspace.ActiveActivity = activity;
					break;
				case WorkspacePane.Sidebar:
					ActiveSidebarActivity = activity;
					if (ActiveWorkspace != null)
						ActiveWorkspace.ActiveSidebarActivity = activity;
					break;
			}

			SwitchToTab(ActiveWorkspace);
			if (activity != null && activity.Workspace != null)
			{
				activity.Workspace.Activate();
				activity.Activate();
				_activationOrder.Add(activity);
			}

			Cursor.Current = Cursors.Default;
			return true;
		}

		private void SwitchToTab(IWorkspace ws)
		{
			if (ws == null)
				return;
			TabPage tab = _tabs[ws];
			_forceSwitch = true;
			tabWorkspaces.SelectedTab = tab;
			_forceSwitch = false;
		}

		public void FocusWorkspace()
		{
			if (ActiveWorkspace == null)
				return;
			TabPage page = _tabs[ActiveWorkspace];
			page.Focus();
		}

		private void tabWorkspaces_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPage == null || _forceSwitch)
				return;
			IWorkspace ws = e.TabPage.Tag as IWorkspace;
			if (!Activate(ws.ActiveActivity) || !Activate(ws.ActiveSidebarActivity))
				e.Cancel = true;
		}

		private void Shell_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Make sure each workspace can close
			CloseReason reason = CloseReason.ClosingDesktop;
			foreach (IWorkspace ws in _workspaces.Keys)
			{
				if (!ws.CanQuit(reason))
				{
					e.Cancel = true;
					return;
				}
			}

			foreach (IWorkspace ws in _workspaces.Keys)
			{
				QuitWorkspace(ws, true);
			}
		}

		private void Shell_KeyDown(object sender, KeyEventArgs e)
		{
			ActiveActivity?.OnKeyPressed(e);
			ActiveSidebarActivity?.OnKeyPressed(e);
		}

		/// <summary>
		/// Maximizes the current workspace, hiding the toolbar and workspace tabs
		/// </summary>
		/// <param name="maximize"></param>
		public void Maximize(bool maximize)
		{
			const int TabHeight = 24;
			if (maximize)
			{
				if (toolbar.Visible)
				{
					toolbar.Visible = false;
					int height = tabWorkspaces.Height;
					tabWorkspaces.Top = -TabHeight;
					tabWorkspaces.Height = height + _workspaceTop + TabHeight;
				}
			}
			else
			{
				if (!toolbar.Visible)
				{
					toolbar.Visible = true;
					int height = tabWorkspaces.Height;
					tabWorkspaces.Top = _workspaceTop;
					tabWorkspaces.Height = height - _workspaceTop - TabHeight;
				}
			}
		}

		public event EventHandler FrameUpdate;
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (_busy)
				return;
			if (FrameUpdate != null)
				FrameUpdate.Invoke(this, EventArgs.Empty);
		}

		public string ShowOpenFileDialog(string initialDirectory, string filename)
		{
			openFileDialog1.InitialDirectory = initialDirectory;
			openFileDialog1.FileName = filename;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog1.FileName;
			}
			return null;
		}

		public void SetStatus(string message)
		{
			lblStatus.Text = message;
		}

		private void tmrAutoTick_Tick(object sender, EventArgs e)
		{
			AutoTick?.Invoke(this, EventArgs.Empty);
		}
	}

	public enum WorkspacePane
	{
		Main,
		Sidebar
	}

	[Flags]
	public enum DeactivateReason
	{
		None = 0,
		SwitchingWorkspaces = 1,
		SwitchingActivities = 2
	}

	public class DeactivateArgs
	{
		public DeactivateReason Reason { get; set; }
		public bool SaveData { get; set; }

		public DeactivateArgs(DeactivateReason reason)
		{
			Reason = reason;
			SaveData = true;
		}
	}

	[Flags]
	public enum CloseReason
	{
		None = 0,
		ClosingWorkspace = 1,
		ClosingDesktop = 2
	}

	public class CloseArgs
	{
		public CloseReason Reason { get; set; }
		public bool SaveData { get; set; }

		public CloseArgs(CloseReason reason)
		{
			Reason = reason;
			SaveData = true;
		}
	}
}
