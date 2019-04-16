using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop
{
	public partial class WorkspaceControl : UserControl
	{
		public IWorkspace Workspace { get; set; }

		private Dictionary<IActivity, TabPage> _tabs = new Dictionary<IActivity, TabPage>();
		private Dictionary<IActivity, TabPage> _sideTabs = new Dictionary<IActivity, TabPage>();
		private HashSet<TabPage> _spacers = new HashSet<TabPage>();
		private bool _forceSwitch = false;
		private bool _activated;

		public WorkspaceControl()
		{
			InitializeComponent();
		}

		#region Tab drawing
		private void tabActivities_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			Brush textBrush;
			Brush backBrush;

			Rectangle rect = tabActivities.ClientRectangle;
			//e.Graphics.FillRectangle(Brushes.LightSlateGray, rect);

			// Get the item from the collection.
			TabPage tabPage = tabActivities.TabPages[e.Index];

			if (_spacers.Contains(tabPage))
			{
				return;
			}

			// Get the real bounds for the tab rectangle.
			Rectangle tabBounds = tabActivities.GetTabRect(e.Index);

			if (e.State == DrawItemState.Selected)
			{
				// Draw a different background color, and don't paint a focus rectangle.
				textBrush = new SolidBrush(Color.White);
				backBrush = new SolidBrush(Color.SlateBlue);
				g.FillRectangle(backBrush, e.Bounds);
			}
			else
			{
				textBrush = new SolidBrush(e.ForeColor);
				//e.DrawBackground();
				g.FillRectangle(Brushes.White, e.Bounds);
			}

			// Use our own font.
			Font tabFont = new Font("Arial", (float)11.0, FontStyle.Bold, GraphicsUnit.Pixel);

			// Draw string. Center the text.
			StringFormat stringFlags = new StringFormat();
			stringFlags.Alignment = StringAlignment.Center;
			stringFlags.LineAlignment = StringAlignment.Center;
			g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
		}
		#endregion

		internal void AddActivity(IActivity activity)
		{
			activity.Activated += Activity_Activated;
			Control ctl = activity as Control;
			if (ctl == null)
				throw new ArgumentException($"Expected activity {activity.GetType().Name} to be an instance of Control");

			if (activity.GetType().GetCustomAttribute<SpacerAttribute>() != null)
			{
				AddSpacer(activity.Pane);
			}

			//Create the tab page
			TabPage page = new TabPage();
			page.Text = activity.Caption;
			page.Tag = activity;
			ctl.Dock = DockStyle.Fill;
			page.Controls.Add(ctl);
			switch (activity.Pane)
			{
				case WorkspacePane.Main:
					tabActivities.TabPages.Add(page);
					_tabs[activity] = page;
					break;
				case WorkspacePane.Sidebar:
					tabSidebarActivities.TabPages.Add(page);
					_sideTabs[activity] = page;
					break;
			}

			UpdateTabVisibility();
		}

		private void AddSpacer(WorkspacePane pane)
		{
			TabPage page = new TabPage();
			switch (pane)
			{
				case WorkspacePane.Main:
					tabActivities.TabPages.Add(page);
					break;
				case WorkspacePane.Sidebar:
					tabSidebarActivities.TabPages.Add(page);
					break;
			}
			_spacers.Add(page);
		}

		internal void RemoveActivity(IActivity activity)
		{
			activity.Activated -= Activity_Activated;
			switch (activity.Pane)
			{
				case WorkspacePane.Main:
					TabPage tab = _tabs[activity];
					tabActivities.TabPages.Remove(tab);
					_tabs.Remove(activity);
					break;
				case WorkspacePane.Sidebar:
					tab = _sideTabs[activity];
					tabSidebarActivities.TabPages.Remove(tab);
					_sideTabs.Remove(activity);
					break;
			}

			UpdateTabVisibility();
		}

		private void Activity_Activated(object sender, EventArgs e)
		{
			_forceSwitch = true;
			TabPage tab;
			IActivity activity = sender as IActivity;
			if (_tabs.TryGetValue(activity, out tab))
			{
				tabActivities.SelectedTab = tab;
			}
			else if (_sideTabs.TryGetValue(activity, out tab))
			{
				if (!_activated)
				{
					tabSidebarActivities.SelectedTab = tab;
					splitContainer1.SplitterDistance = splitContainer1.Width - Math.Max(100, activity.SidebarWidth);
					_activated = true;
				}
			}
			_forceSwitch = false;
		}

		private void UpdateTabVisibility()
		{
			if (_tabs.Count == 0)
				return;
			TabPage page = tabActivities.TabPages[0];
			Control ctl = page.Tag as Control;

			DockTabPage(_tabs, tabActivities, splitContainer1.Panel1);

			//Update sidebar visibility
			if (tabSidebarActivities.TabPages.Count == 0)
			{
				splitContainer1.Panel2Collapsed = true;
			}
			else
			{
				splitContainer1.Panel2Collapsed = false;
				DockTabPage(_sideTabs, tabSidebarActivities, sidebar);
			}
		}

		private void DockTabPage(Dictionary<IActivity, TabPage> tabs, TabControl tabControl, Control parentControl)
		{
			TabPage page = tabControl.TabPages[0];
			Control ctl = page.Tag as Control;

			if (tabs.Count == 1)
			{
				//Move the control out of the tabstrip
				parentControl.Controls.Add(ctl);
				tabControl.Visible = false;
			}
			else
			{
				//put the control back into the tabstrip
				page.Controls.Add(ctl);
				tabControl.Visible = true;
			}
		}

		private void OnSelectingTab(object sender, TabControlCancelEventArgs e)
		{
			if (_spacers.Contains(e.TabPage))
			{
				e.Cancel = true;
				return;
			}

			if (_forceSwitch)
				return;
			IActivity activity = e.TabPage.Tag as IActivity;
			if (!Shell.Instance.Activate(activity))
				e.Cancel = true;
		}

		public bool IsSidebarExpanded
		{
			get { return Workspace.ActiveSidebarActivity != null && !splitContainer1.Panel2Collapsed; }
		}

		public void ToggleSidebar(bool expanded)
		{
			if (Workspace.ActiveSidebarActivity == null) { return; }

			splitContainer1.Panel2Collapsed = !expanded;
		}
	}
}
