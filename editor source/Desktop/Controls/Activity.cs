using System;
using System.Windows.Forms;
using System.ComponentModel;
using Desktop.Messaging;
using Desktop.Skinning;

namespace Desktop
{
	[ToolboxItem(false)]
	public partial class Activity : UserControl, IActivity, ISkinControl, ISkinnedPanel
	{
		public IWorkspace Workspace { get; set; }
		public WorkspacePane Pane { get; set; }
		public int SidebarWidth { get; set; }
		public IRecord Record;

		private Mailbox _desktopMailbox;
		private Mailbox _workspaceMailbox;

		private bool _previouslyActivated;

		public virtual string Caption { get; }

		public void Initialize(IWorkspace ws, IRecord record)
		{
			Workspace = ws;
			Record = record;

			_workspaceMailbox = ws.GetMailbox();
			_desktopMailbox = Shell.Instance.PostOffice.GetMailbox();

			SkinManager.UpdateSkin(this, SkinManager.Instance.CurrentSkin);
			OnInitialize();
		}
		protected virtual void OnInitialize()
		{

		}

		public virtual bool CanDeactivate(DeactivateArgs args)
		{
			return true;
		}

		public virtual bool CanQuit(CloseArgs args)
		{
			return true;
		}

		public void Activate()
		{
			if (!_previouslyActivated)
			{
				OnFirstActivate();
				_previouslyActivated = true;
			}
			OnActivate();
			Activated?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnActivate()
		{

		}
		public event EventHandler Activated;

		public bool IsActive
		{
			get { return Workspace != null && Workspace.ActiveActivity == this; }
		}

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		protected virtual void OnFirstActivate()
		{
		}

		public void UpdateParameters(params object[] parameters)
		{
			OnParametersUpdated(parameters);
		}
		protected virtual void OnParametersUpdated(params object[] parameters)
		{
		}

		public void Deactivate()
		{
			OnDeactivate();
		}
		protected virtual void OnDeactivate()
		{
		}

		public virtual void Quit()
		{
		}

		public virtual void Save()
		{

		}

		public void Destroy()
		{
			OnDestroy();
			Record = null;
			Workspace = null;
			_desktopMailbox.Destroy();
			_workspaceMailbox.Destroy();
		}
		protected virtual void OnDestroy()
		{
		}

		public virtual void OnKeyPressed(KeyEventArgs e)
		{

		}

		protected void SubscribeDesktop(int message, Action handler)
		{
			_desktopMailbox.Subscribe(message, handler);
		}

		protected void SubscribeDesktop<T>(int message, Action<T> handler)
		{
			_desktopMailbox.Subscribe(message, handler);
		}

		protected void SubscribeWorkspace(int message, Action handler)
		{
			_workspaceMailbox.Subscribe(message, handler);
		}

		protected void SubscribeWorkspace<T>(int message, Action<T> handler)
		{
			_workspaceMailbox.Subscribe(message, handler);
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.Background.Normal;
			OnSkinChanged(skin);
		}

		protected virtual void OnSkinChanged(Skin skin)
		{
		}
	}
}
