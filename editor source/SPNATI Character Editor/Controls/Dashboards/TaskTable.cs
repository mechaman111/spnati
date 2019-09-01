using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class TaskTable : UserControl
	{
		private Dictionary<ChecklistTask, TaskTableRow> _rows = new Dictionary<ChecklistTask, TaskTableRow>();

		public TaskTable()
		{
			InitializeComponent();
		}

		public void Clear()
		{
			pnlRecords.Controls.Clear();
			_rows.Clear();
		}

		/// <summary>
		/// Adds a task
		/// </summary>
		public void AddTask(ChecklistTask task)
		{
			TaskTableRow row = new TaskTableRow(task);
			row.Dock = DockStyle.Top;
			pnlRecords.Controls.Add(row);
			pnlRecords.Controls.SetChildIndex(row, 0);
			SkinManager.Instance.ReskinControl(row, SkinManager.Instance.CurrentSkin);
		}
	}
}
