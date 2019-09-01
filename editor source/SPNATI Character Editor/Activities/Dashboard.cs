using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Controls.Dashboards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	/// <remarks>
	/// TODO: Split this into real, reusable widgets
	/// </remarks>
	[Activity(typeof(Character), -5)]
	public partial class Dashboard : Activity
	{
		private const int TaskInterval = 100;

		private Character _character;

		//pseudo background jobs since the editor data is not equipped for true multiprocessing.
		private int _currentTaskIndex = -1;
		private IEnumerator _currentTask;
		private System.Timers.Timer _taskTimer = new System.Timers.Timer();
		private bool _canRunTasks = false;

		private List<IDashboardWidget> _widgets = new List<IDashboardWidget>();

		public Dashboard()
		{
			InitializeComponent();
		}

		public override string Caption { get { return "Dashboard"; } }

		protected override void OnInitialize()
		{
			_character = Record as Character;

			_taskTimer.Interval = TaskInterval;
			_taskTimer.Elapsed += _taskTimer_Elapsed;

			AddWidget(colRight.Panel1, new LineHistoryWidget());
			AddWidget(colLeft.Panel2, new SponsorshipWidget());
			AddWidget(colRight.Panel2, new ComparisonWidget());
			AddWidget(colLeft.Panel1, new ChecklistWidget());
		}

		private void AddWidget(Control parent, IDashboardWidget widget)
		{
			Control ctl = (Control)widget;
			ctl.Dock = DockStyle.Fill;
			parent.Controls.Add(ctl);
			widget.Initialize(_character);
			_widgets.Add(widget);
		}

		protected override void OnActivate()
		{
			_canRunTasks = true;
			_currentTaskIndex = -1;
			string portrait = _character.Metadata.Portrait;
			if (!string.IsNullOrEmpty(portrait))
			{
				PoseMapping pose = _character.PoseLibrary.GetPose(portrait);
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, pose, 0));
			}

			CharacterHistory history = CharacterHistory.Get(_character, true);

			DoNextTask();
		}

		protected override void OnDeactivate()
		{
			_canRunTasks = false;
		}

		private void DoNextTask()
		{
			if (_currentTask != null)
			{
				if (!_currentTask.MoveNext())
				{
					_currentTask = null;
				}
			}
			else
			{
				_currentTaskIndex++;

				if (_currentTaskIndex >= _widgets.Count)
				{
					return;
				}

				IDashboardWidget widget = _widgets[_currentTaskIndex];
				_currentTask = widget.DoWork();
				if (!_currentTask.MoveNext())
				{
					_currentTask = null;
				}
			}

			_taskTimer.Start();
		}

		private void _taskTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_taskTimer.Stop();
			if (_canRunTasks)
			{
				MethodInvoker invoker = delegate ()
				{
					DoNextTask();
				};
				Invoke(invoker);
			}
		}

		private void HideWidget(Control widget)
		{
		}
	}
}
