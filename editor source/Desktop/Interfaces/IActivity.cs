using System;
using System.Windows.Forms;

namespace Desktop
{
	public interface IActivity
	{
		IWorkspace Workspace { get; set; }
		WorkspacePane Pane { get; set; }
		int SidebarWidth { get; set; }
		void Initialize(IWorkspace ws, IRecord record);
		string Caption { get; }
		bool CanRun();
		bool CanDeactivate(DeactivateArgs args);
		bool CanQuit(CloseArgs args);
		void Activate();
		void UpdateParameters(params object[] parameters);
		void Deactivate();
		void Save();
		void Quit();
		void Destroy();
		void OnKeyPressed(KeyEventArgs e);
		event EventHandler Activated;
	}
}
