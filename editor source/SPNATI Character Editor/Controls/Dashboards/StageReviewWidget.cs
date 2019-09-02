using Desktop.CommonControls;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class StageReviewWidget : UserControl, IDashboardWidget
	{
		private Character _character;

		public StageReviewWidget()
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
			DataSeries series = graphLines.AddSeries("Lines", 3);
			foreach (KeyValuePair<int, int> kvp in work.LinesPerStage)
			{
				series.AddPoint(kvp.Key, kvp.Value, kvp.Key.ToString());
			}
			grpWidget.Unshield();
			yield break;
		}
	}
}
