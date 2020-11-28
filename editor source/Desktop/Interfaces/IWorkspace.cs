using Desktop.Messaging;
using Desktop.Skinning;
using System;

namespace Desktop
{
	public interface IWorkspace
	{
		WorkspaceControl Control { get; set; }
		bool IsDefault { get; set; }
		IRecord Record { get; set; }
		void AddActivity(IActivity activity, ActivityMetadata metadata);
		void AddActivityPlaceholder(ActivityMetadata metadata);
		bool HasPlaceholder(ActivityMetadata metadata);
		IActivity ActiveActivity { get; set; }
		IActivity ActiveSidebarActivity { get; set; }
		IActivity GetDefaultActivity();
		IActivity GetDefaultSidebarActivity();
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
		void ShowBanner(string text, SkinnedHighlight highlight);
	}
}
