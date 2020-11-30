using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public interface IRecordProvider
	{
		void SetContext(object context);

		List<IRecord> GetRecords(string text, LookupArgs args);
		ListViewItem FormatItem(IRecord record);
		void SetFormatInfo(LookupFormat info);

		IRecord Create(string key);
		void Delete(IRecord record);
		void Sort(List<IRecord> list);
		bool AllowsNew { get; }
		bool AllowsDelete { get; }
		bool TrackRecent { get; }
		bool FilterFromUI(IRecord record);
	}

	public interface IRecordProvider<T> : IRecordProvider where T : IRecord
	{

	}

	public class LookupArgs
	{
		public string[] ExtraText;
	}

	public class LookupFormat
	{
		public string Caption = "Look Up Record";
		public string[] Columns = new string[1];
		public int[] ColumnWidths = null;
		public List<string> ExtraFields = new List<string>();
	}
}
