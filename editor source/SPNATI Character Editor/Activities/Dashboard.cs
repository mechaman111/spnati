using Desktop;
using Desktop.CommonControls;
using System;
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

		private float _fileSize;

		//pseudo background jobs since the editor data is not equipped for true multiprocessing.
		private int _currentTaskIndex = -1;
		private List<Action> _tasks = new List<Action>();
		private System.Timers.Timer _taskTimer = new System.Timers.Timer();
		private bool _canRunTasks = false;

		private PartnerGraphs _partnerGraphType = PartnerGraphs.Lines;

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
			_tasks = new List<Action>()
			{
				UpdateChecklist,
				UpdateRequirements,
				UpdateLineHistory,
				UpdatePartners,
			};
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
			_fileSize = GetTotalImageSize(_character);

			grpPartners.Shield();
			grpRequirements.Shield();
			grpHistory.Shield();
			grpChecklist.Shield();

			DoNextTask();
		}

		protected override void OnDeactivate()
		{
			_canRunTasks = false;
		}

		private void DoNextTask()
		{
			_currentTaskIndex++;
			if (_currentTaskIndex >= _tasks.Count)
			{
				return;
			}

			Action task = _tasks[_currentTaskIndex];
			task();
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

		private float GetTotalImageSize(Character character)
		{
			long size = 0;
			string dir = _character.GetDirectory();
			DirectoryInfo directory = new DirectoryInfo(dir);
			foreach (FileInfo file in directory.EnumerateFiles()
				.Where(f => f.Extension == ".png" || f.Extension == ".gif"))
			{
				if (char.IsNumber(file.Name[0])) //only include images that start with a number. Assume others are for epilogues and shouldn't count towards the requirements
				{
					size += file.Length;
				}
			}
			return (float)Math.Round(size / 1048576f, 2);
		}

		private void HideWidget(Control widget)
		{
		}

		private void UpdateRequirements()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			TestRequirements requirements = TestRequirements.Instance;
			StringBuilder sb = new StringBuilder();
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
			barSize.Value = (decimal)_fileSize;
			grpRequirements.Unshield();
		}

		private void UpdateLineHistory()
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
					work = new LineWork();
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
		}

		private void UpdateChecklist()
		{
			grpChecklist.Unshield();
		}

		private void UpdatePartners()
		{
			//Figure out which source material tag has the most characters in common
			Tuple<string, List<Character>> franchise = TagDatabase.GetSmallestGroup("Source Material", _character);

			graphPartners.Clear();
			if (franchise != null)
			{
				switch (_partnerGraphType)
				{
					case PartnerGraphs.Lines:
						lblLines.Text = "Lines";
						UpdatePartnerLines(franchise);
						break;
					case PartnerGraphs.Targets:
						lblLines.Text = $"Banter with {_character}";
						UpdatePartnerBanter(franchise);
						break;
				}
			}
			else
			{
				HideWidget(grpPartners);
			}

			grpPartners.Unshield();
		}

		private void UpdatePartnerLines(Tuple<string, List<Character>> franchise)
		{
			DataSeries lines = graphPartners.AddSeries("Lines");

			Tag tag = TagDatabase.GetTag(franchise.Item1);
			grpPartners.Text = $"{tag.DisplayName} Characters";

			int n = 0;
			foreach (Character character in franchise.Item2)
			{
				CharacterHistory characterHistory = CharacterHistory.Get(character, character != _character);
				LineWork work = characterHistory.Current;

				lines.AddPoint(n++, work.TotalLines, character.Label);
			}
		}

		private void UpdatePartnerBanter(Tuple<string, List<Character>> franchise)
		{
			DataSeries incoming = graphPartners.AddSeries($"From {_character}");
			DataSeries outgoing = graphPartners.AddSeries($"To {_character}");

			Dictionary<string, int> counts = new Dictionary<string, int>();
			Dictionary<string, HashSet<string>> incomingLines = new Dictionary<string, HashSet<string>>();
			Dictionary<string, int> indices = new Dictionary<string, int>();

			int n = 0;
			foreach (Character character in franchise.Item2)
			{
				HashSet<string> usedLines = new HashSet<string>();
				if (character == _character)
				{
					continue;
				}
				counts.Add(character.FolderName, 0);
				incomingLines.Add(character.FolderName, new HashSet<string>());
				indices[character.FolderName] = n;
				int count = 0;
				foreach (Case c in character.GetCasesTargetedAtCharacter(_character, TargetType.DirectTarget))
				{
					foreach (DialogueLine line in c.Lines)
					{
						if (!usedLines.Contains(line.Text))
						{
							usedLines.Add(line.Text);
							count++;
						}
					}
				}
				outgoing.AddPoint(n++, count, character.Label);
			}

			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (workingCase.HasTargetedConditions)
				{
					foreach (string target in workingCase.GetTargets())
					{
						if (counts.ContainsKey(target))
						{
							HashSet<string> usedLines = incomingLines[target];
							int count = 0;
							foreach (DialogueLine line in workingCase.Lines)
							{
								if (!usedLines.Contains(line.Text))
								{
									usedLines.Add(line.Text);
									count++;
								}
							}
							counts[target] += count;
						}
					}
				}
			}
			foreach (KeyValuePair<string, int> kvp in counts)
			{
				incoming.AddPoint(indices[kvp.Key], kvp.Value);
			}
		}

		private enum PartnerGraphs
		{
			Lines = 0,
			Targets = 1,
			MAX = 2,
		}

		private void cmdPreviousGraph_Click(object sender, EventArgs e)
		{
			int type = (int)_partnerGraphType;
			type--;
			if (type < 0)
			{
				type = (int)PartnerGraphs.MAX - 1;
			}
			_partnerGraphType = (PartnerGraphs)type;
			UpdatePartners();
		}

		private void cmdNextGraph_Click(object sender, EventArgs e)
		{
			int type = (int)_partnerGraphType;
			type++;
			if (type >= (int)PartnerGraphs.MAX)
			{
				type = 0;
			}
			_partnerGraphType = (PartnerGraphs)type;
			UpdatePartners();
		}
	}
}
