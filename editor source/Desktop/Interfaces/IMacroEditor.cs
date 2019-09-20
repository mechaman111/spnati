using Desktop.CommonControls;
using System;

namespace Desktop
{
	/// <summary>
	/// Interface for providing necessary setup data to the macro editor
	/// </summary>
	public interface IMacroEditor
	{
		bool ShowHelp { get; }
		string GetHelpText();
		object CreateData();
		object GetRecordContext();
		object GetSecondaryRecordContext();
		Func<PropertyRecord, bool> GetRecordFilter(object data);
		void AddSpeedButtons(PropertyTable table);
	}
}
