using Desktop.CommonControls;
using System.Collections;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class TargetWidget : UserControl, IDashboardWidget
	{
		private const int MaxTargets = 3;

		private Character _character;

		public TargetWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpWidget.Shield();
		}

		public bool IsVisible()
		{
			return true;
		}

		public IEnumerator DoWork()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			LineWork work = history.Current;
			graphLines.Clear();
			DataSeries series = graphLines.AddSeries("Targets", 4);
			for (int i = 0; i < MaxTargets && i < work.Targets.Count; i++)
			{
				TargetingInformation info = work.Targets[i];
				Character c = CharacterDatabase.Get(info.Target);
				series.AddPoint(i, info.LineCount, c?.ToString() ?? info.Target);
			}
			grpWidget.Unshield();
			yield break;
		}
	}
}
