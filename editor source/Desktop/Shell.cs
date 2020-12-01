using Desktop.Messaging;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop
{
	public partial class Shell : SkinnedForm
	{
		private static bool _busy = false;

		const int LEADING_SPACE = 12;
		const int CLOSE_SPACE = 15;
		const int CLOSE_AREA = 15;

		private int _nextId;
		private bool _forceSwitch = false;

		public static Shell Instance { get; private set; }

		public PostOffice PostOffice = new PostOffice();

		public string Description
		{
			get { return stripActivities.DecorationText; }
			set { stripActivities.DecorationText = value; }
		}

		private Dictionary<Type, SortedList<int, Type>> _recordToActivityMap;
		private Dictionary<Type, Type> _recordToWorkspaceMap;
		private Dictionary<Type, ActivityMetadata> _activityMetadataMap;
		private Dictionary<IWorkspace, WorkspaceControl> _workspaces = new Dictionary<IWorkspace, WorkspaceControl>();
		private Dictionary<IWorkspace, TabPage> _tabs = new Dictionary<IWorkspace, TabPage>();
		private Toaster _toaster = new Toaster();
		private Messenger _messenger = new Messenger();
		private HashSet<Action> _batchedActions = new HashSet<Action>();

		public IWorkspace ActiveWorkspace;
		public IActivity ActiveActivity;
		public IActivity ActiveSidebarActivity;

		public event EventHandler VersionClick;
		public event EventHandler SubActionClick;

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

		public string Version
		{
			get { return tsVersion.Text; }
			set { tsVersion.Text = value; }
		}

		public string SubActionLabel
		{
			get { return tsSubAction.Text; }
			set { tsSubAction.Text = value; }
		}

		private List<IActivity> _activationOrder = new List<IActivity>();

		public Shell(string caption, Icon icon)
		{
			Instance = this;
			InitializeComponent();

			Text = caption;
			Icon = icon;

			SetStatus("");
		}

		private void Shell_Load(object sender, EventArgs e)
		{
			statusStrip1.Padding = new Padding(statusStrip1.Padding.Left, statusStrip1.Padding.Top, statusStrip1.Padding.Left, statusStrip1.Padding.Bottom);

			BuildWorkspaceMap();
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
			_activityMetadataMap = new Dictionary<Type, ActivityMetadata>();
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

							SpacerAttribute attr3 = type.GetCustomAttribute<SpacerAttribute>();

							ActivityMetadata md = new ActivityMetadata()
							{
								Width = attr2.Width,
								Caption = attr2.Caption,
								DelayRun = attr2.DelayRun,
								Pane = attr2.Pane,
								ActivityType = type,
								HasSpacer = attr3 != null,
							};
							_activityMetadataMap[type] = md;
						}
					}
				}
			}
		}

		#region Action bar
		public ToolStripMenuItem AddActionMenu(Image icon, string tooltip)
		{
			ToolStripMenuItem menu = new ToolStripMenuItem();
			menu.Image = icon;
			menu.ToolTipText = tooltip;
			actionStrip.Items.Add(menu);
			return menu;
		}

		public ToolStripMenuItem AddActionItem(Image icon, string text, string tooltip, Action action, ToolStripMenuItem submenu)
		{
			ToolStripMenuItem menu = new ToolStripMenuItem();
			menu.Image = icon;
			if (submenu == null)
			{
				menu.ToolTipText = tooltip ?? text;
				actionStrip.Items.Add(menu);
			}
			else
			{
				menu.Text = text;
				menu.ToolTipText = tooltip;
				submenu.DropDownItems.Add(menu);
			}
			menu.Tag = action;
			menu.Click += Action_Click;
			return menu;
		}

		public void AddActionSeparator() { AddActionSeparator(null); }
		public void AddActionSeparator(ToolStripMenuItem submenu)
		{
			ToolStripSeparator separator = new ToolStripSeparator();
			if (submenu == null)
			{
				actionStrip.Items.Add(separator);
			}
			else
			{
				submenu.DropDownItems.Add(separator);
			}
		}

		private void Action_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem ctl = sender as ToolStripMenuItem;
			Action action = ctl.Tag as Action;
			action?.Invoke();
		}
		#endregion

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

		private void stripActivities_CloseButtonClicked(object sender, EventArgs e)
		{
			IWorkspace ws = tabWorkspaces.TabPages[tabWorkspaces.SelectedIndex].Tag as IWorkspace;
			if (!ws.IsDefault)
			{
				CloseWorkspace(ws);
			}
		}

		public void Launch(LaunchParameters launchParameters)
		{
			Launch(launchParameters.Record, launchParameters.Activity, launchParameters.Parameters);
		}

		public void Launch(IRecord record, Type activityType, params object[] parameters)
		{
			Type recordType = record.GetType();
			bool changingWorkspace = ActiveWorkspace?.Record != record;

			if (!changingWorkspace && ActiveActivity?.GetType() == activityType)
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

				workspace = FindWorkspace(recordType, record.Key);
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
			IActivity activity = workspace.Find(activityType);
			if (activity == null)
			{
				//see if there's a placeholder
				ActivityMetadata md = _activityMetadataMap[activityType];
				if (workspace.HasPlaceholder(md))
				{
					//need to initialize the activity
					activity = CreateActivityInWorkspace(workspace, activityType, false);
				}
				else
				{
					//activity isn't in the workspace
					throw new NotImplementedException("No support for launching activities into an open workspace yet.");
				}
			}
			Activate(activity);
			activity.UpdateParameters(parameters);

			if (changingWorkspace && activity.Pane == WorkspacePane.Main)
			{
				//Also need to mark the sidebar as active too
				activity = workspace.GetDefaultSidebarActivity();
				if (activity != null)
				{
					Activate(activity);
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
			Launch(record, typeof(TActivity), parameters);
		}

		/// <summary>
		/// Executs the CanRun method for every open activity and closes any that fail
		/// </summary>
		public void RunChecks()
		{
			foreach (WorkspaceControl ws in _workspaces.Values)
			{
				HashSet<IActivity> toClose = new HashSet<IActivity>();
				foreach (IActivity activity in ws.Activities)
				{
					if (!activity.CanRun())
					{
						toClose.Add(activity);
					}
				}
				foreach (IActivity activity in toClose)
				{
					CloseActivity(activity);
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
			tabWorkspaces.SuspendDrawing();
			if (workspace == null)
			{
				//open record lookup
				IRecord record = RecordLookup.DoLookup(type, recordKey);
				if (record == null)
				{
					tabWorkspaces.ResumeDrawing();
					return; //no record to launch, so stop
				}

				//one more chance to find the workspace
				workspace = FindWorkspace(type, record.Key);
				if (workspace == null)
				{
					//create the workspace
					workspace = CreateWorkspace(record);
				}
			}

			//Activate the workspace and its first activity
			IActivity activity = workspace.GetDefaultActivity();
			Activate(activity);
			activity.UpdateParameters(parameters);

			//Also the sidebar
			activity = workspace.GetDefaultSidebarActivity();
			if (activity != null)
			{
				Activate(activity);
			}
			tabWorkspaces.ResumeDrawing();
		}

		public void LaunchWorkspace(Type type, IRecord record, bool defaultWorkspace = false, params object[] parameters)
		{
			DeactivateArgs args = new DeactivateArgs(DeactivateReason.SwitchingWorkspaces);
			if (ActiveActivity != null && !ActiveActivity.CanDeactivate(args)
				|| ActiveSidebarActivity != null && !ActiveSidebarActivity.CanDeactivate(args)
				|| record == null)
				return;
			tabWorkspaces.SuspendDrawing();
			IWorkspace workspace = FindWorkspace(type, record.Key);
			if (workspace == null)
			{
				workspace = CreateWorkspace(record, defaultWorkspace);
			}

			//Activate the workspace and its first activity
			IActivity activity = workspace.GetDefaultActivity();
			Activate(activity);
			activity.UpdateParameters(parameters);

			//Also the sidebar
			activity = workspace.GetDefaultSidebarActivity();
			if (activity != null)
			{
				Activate(activity);
			}
			tabWorkspaces.ResumeDrawing();
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
			tabWorkspaces.Visible = true;
			_workspaces[ws] = ctl;
			_tabs[ws] = page;

			//Create the workspace's activities
			SortedList<int, Type> activities;
			if (_recordToActivityMap.TryGetValue(ws.Record.GetType(), out activities))
			{
				foreach (var type in activities.Values)
				{
					if (ws.AllowAutoStart(type))
					{
						CreateActivityInWorkspace(ws, type, true);
					}
				}
			}
		}

		private IActivity CreateActivityInWorkspace(IWorkspace workspace, Type activityType, bool allowDelay)
		{
			if (!typeof(IActivity).IsAssignableFrom(activityType))
				throw new ArgumentException($"Activity type {activityType.Name} does not derive from Activity.");

			ActivityMetadata md = _activityMetadataMap[activityType];
			WorkspaceControl ctl = _workspaces[workspace];
			if (allowDelay && md.DelayRun)
			{
				ctl.AddActivityTab(md);
				workspace.AddActivityPlaceholder(md);
				return null;
			}

			IActivity a = Activator.CreateInstance(activityType) as IActivity;
			if (!a.CanRun())
			{
				return null;
			}
			
			a.SidebarWidth = md.Width;
			a.Pane = md.Pane;
			a.Initialize(workspace, workspace.Record);
			
			ctl.AddActivity(a, md);
			workspace.AddActivity(a, md);
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
			if (!silent && !ws.CanQuit(CloseReason.ClosingWorkspace))
				return false;
			QuitWorkspace(ws, false);
			return true;
		}

		/// <summary>
		/// Attempts to close an activity
		/// </summary>
		/// <param name="activity"></param>
		/// <returns></returns>
		public bool CloseActivity(IActivity activity)
		{
			CloseArgs args = new CloseArgs(CloseReason.None);
			if (activity.CanQuit(args))
			{
				IWorkspace ws = activity.Workspace;
				activity.Quit();
				WorkspaceControl ctl = _workspaces[ws];
				ctl.RemoveActivity(activity);
				ws.RemoveActivity(activity);
				return true;
			}
			return false;
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
			if (tabWorkspaces.TabPages.Count == 0)
			{
				tabWorkspaces.Visible = false;
			}
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
			{
				stripActivities.Invalidate(true);
				e.Cancel = true;
			}
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
			if (maximize)
			{
				if (toolbar.Visible)
				{
					toolbar.Visible = false;
					actionStrip.Visible = false;
					stripActivities.Visible = false;
				}
			}
			else
			{
				if (!toolbar.Visible)
				{
					toolbar.Visible = true;
					actionStrip.Visible = true;
					stripActivities.Visible = true;
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

		public string ShowOpenFileDialog(string initialDirectory, string filename, string filter)
		{
			openFileDialog1.InitialDirectory = initialDirectory;
			openFileDialog1.FileName = filename;
			openFileDialog1.Filter = filter;
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

		public void SetDirty(IWorkspace ws, bool dirty)
		{
			TabPage page;
			if (_tabs.TryGetValue(ws, out page))
			{
				if (dirty && !page.Text.EndsWith("*"))
				{
					page.Text += "*";
				}
				else if (!dirty && page.Text.EndsWith("*"))
				{
					page.Text = page.Text.Substring(0, page.Text.Length - 1);
				}
			}
		}

		private void tsVersion_Click(object sender, EventArgs e)
		{
			VersionClick?.Invoke(this, e);
		}

		private void tsSubAction_Click(object sender, EventArgs e)
		{
			SubActionClick?.Invoke(this, e);
		}

		public int DelayAction(Action action, int delayMs)
		{
			return _messenger.Send(action, delayMs);
		}

		/// <summary>
		/// Delays an action and groups all calls within that amount of time so that only one fires
		/// </summary>
		/// <param name="action"></param>
		public void BatchAction(Action action, int delayMs)
		{
			if (_batchedActions.Contains(action))
			{
				return;
			}
			_batchedActions.Add(action);
			DelayAction(() =>
			{
				_batchedActions.Remove(action);
				action();
			}, delayMs);
		}

		public void CancelAction(int id)
		{
			_messenger.Cancel(id);
		}

		#region Toaster
		public void ShowToast(string caption, string text, Image icon = null, SkinnedHighlight highlight = SkinnedHighlight.Heading)
		{
			Toast toast = new Toast(caption, text)
			{
				Highlight = highlight,
				Icon = icon,
			};
			_toaster.ShowToast(toast);
		}
		#endregion
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

	public class LaunchParameters
	{
		public IRecord Record { get; set; }
		public Type Activity { get; set; }
		public object[] Parameters { get; set; }

		public LaunchParameters(IRecord record, Type activityType, params object[] parameters)
		{
			Record = record;
			Activity = activityType;
			Parameters = parameters;
		}
	}

	public class ActivityMetadata
	{
		public bool HasSpacer;
		public WorkspacePane Pane;
		public int Width;
		public bool DelayRun;
		public string Caption;
		public Type ActivityType;
	}
}
