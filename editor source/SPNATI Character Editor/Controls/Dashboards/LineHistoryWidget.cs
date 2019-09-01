using Desktop.CommonControls;
using System;
using System.Collections;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class LineHistoryWidget : UserControl, IDashboardWidget
	{
		private Character _character;

		public LineHistoryWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpHistory.Shield();
		}

		public IEnumerator DoWork()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			graphLines.Clear();

			DataSeries lines = graphLines.AddSeries("Total");
			DataSeries generic = graphLines.AddSeries("Generic");
			DataSeries targeted = graphLines.AddSeries("Targeted");

			DateTime today = DateTime.UtcNow;
			//last 7 days
			for (int i = 6; i >= 0; i--)
			{
				TimeSpan time = new TimeSpan(i, 0, 0, 0);
				DateTime date = today - time;

				LineWork previous = history.GetMostRecentWorkBefore(date);
				LineWork work = history.GetWork(date, false);
				if (previous == null)
				{
					previous = work;
				}
				DateTime localDate = date.ToLocalTime();
				string label = localDate.ToString("ddd");
				if (work == null)
				{
					if (previous != null)
					{
						work = previous.Clone() as LineWork;
					}
					else
					{
						work = new LineWork();
					}
				}
				if (previous == null)
				{
					previous = work;
				}
				int diffLines = work.TotalLines - previous.TotalLines;
				lines.AddPoint(6 - i, diffLines, label);
				int diffGeneric = (work.GenericCount + work.ConditionCount) - (previous.GenericCount + previous.ConditionCount);
				generic.AddPoint(6 - i, diffGeneric, label);
				int diffTarget = work.TargetedCount - previous.TargetedCount;
				targeted.AddPoint(6 - i, diffTarget, label);
			}
			grpHistory.Unshield();
			yield break;
		}
	}
}
