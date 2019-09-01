using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class SponsorshipWidget : UserControl, IDashboardWidget
	{
		private Character _character;

		public SponsorshipWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpRequirements.Shield();
		}

		public IEnumerator DoWork()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			TestRequirements requirements = TestRequirements.Instance;
			StringBuilder sb = new StringBuilder();
			float fileSize = history.GetTotalFileSize(true);
			LineWork current = history.Current;
			barLines.Maximum = requirements.Lines;
			barLines.Value = current.TotalLines;
			barFilters.Maximum = requirements.Filtered;
			barFilters.Value = current.FilterCount;
			barTargets.Maximum = requirements.Targeted;
			barTargets.Value = current.TargetedCount;
			barUnique.Maximum = requirements.UniqueTargets;
			barUnique.Value = current.Targets.Count;
			barSize.Maximum = requirements.SizeLimit;
			barSize.Value = (decimal)fileSize;
			grpRequirements.Unshield();
			yield break;
		}
	}
}
