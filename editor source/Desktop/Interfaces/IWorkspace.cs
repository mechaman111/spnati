using Desktop.Messaging;
using System;

namespace Desktop
{
	public interface IWorkspace
	{
		WorkspaceControl Control { get; set; }
		bool IsDefault { get; set; }
		IRecord Record { get; set; }
		void AddActivity(IActivity activity);
		IActivity ActiveActivity { get; set; }
		IActivity ActiveSidebarActivity { get; set; }
		IActivity GetFirstActivity();
		IActivity GetFirstSidebarActivity();
		IActivity Find<T>() where T : IActivity;
		IActivity Find(Type type);
		void RemoveActivity(IActivity activity);
		void Initialize();
		string Caption { get; }
		int Id { get; set; }
		bool CanDeactivate(DeactivateReason reason);
		bool CanQuit(CloseReason reason);
		void Activate();
		void Deactivate();
		void Quit();
		void Destroy();
		bool ActivateActivity(IActivity activity);
		event EventHandler Activated;
		Mailbox GetMailbox();
		void SendMessage(int message);
		void SendMessage<T>(int message, T args);
		bool IsSidebarExpanded { get; }
		void ToggleSidebar(bool expanded);
		void SetData(string key, object value);
		T GetData<T>(string key);
		bool AllowAutoStart(Type activityType);
	}
}
