using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class TargetWidget : UserControl, IDashboardWidget
	{
		private const int MaxTargets = 4;
		private const int MaxIncoming = 100;

		private Character _banterTarget;

		private Character _character;

		public TargetWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpWidget.Shield();
			linkGo.LaunchHandler = LaunchBanterWizard;
		}

		public bool IsVisible()
		{
			return true;
		}

		public IEnumerator DoWork()
		{
			graphLines.Clear();
			string character = _character.FolderName;
			if (GlobalCache.HasChanges(character))
			{
				List<Tuple<string, int>> lines = new List<Tuple<string, int>>();
				graphLines.HorizontalOrientation = true;
				DataSeries series = graphLines.AddSeries("Targets", MaxIncoming);
				grpWidget.Text = "New Lines From Opponents";
				lblName.Text = "New Lines Towards " + _character.ToString();
				foreach (string source in GlobalCache.EnumerateChangeInTargets(character))
				{
					int count = GlobalCache.GetChangeInTargets(source, character);
					lines.Add(new Tuple<string, int>(source, count));
				}
				lines.Sort((l1, l2) =>
				{
					int compare = l2.Item2.CompareTo(l1.Item2);
					if (compare == 0)
					{
						compare = l1.Item1.CompareTo(l2.Item1);
					}
					return compare;
				});
				_banterTarget = null;
				for (int i = 0; i < MaxIncoming && i < lines.Count; i++)
				{
					Character c = CharacterDatabase.Get(lines[i].Item1);
					if (_banterTarget == null)
					{
						_banterTarget = c;
					}
					series.AddPoint(i, lines[i].Item2, c?.ToString() ?? lines[i].Item1);
				}
				linkGo.Visible = true;
			}
			else
			{
				graphLines.HorizontalOrientation = false;
				DataSeries series = graphLines.AddSeries("Targets", 4);
				grpWidget.Text = "Targets";
				lblName.Text = "Most Targeted Opponents";
				CharacterHistory history = CharacterHistory.Get(_character, false);
				LineWork work = history.Current;
				for (int i = 0; i < MaxTargets && i < work.Targets.Count; i++)
				{
					TargetingInformation info = work.Targets[i];
					Character c = CharacterDatabase.Get(info.Target);
					series.AddPoint(i, info.LineCount, c?.ToString() ?? info.Target);
				}
				linkGo.Visible = false;
			}
			grpWidget.Unshield();
			yield break;
		}

		private void LaunchBanterWizard()
		{
			if (_banterTarget != null)
			{
				Shell.Instance.Launch<Character, BanterWizard>(_character, _banterTarget);
			}
		}
	}
}
