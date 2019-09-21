using Desktop;
using Desktop.CommonControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Reference
{
	public partial class TargetReport : UserControl
	{
		public TargetReport(Character source)
		{
			_character = source;
			InitializeComponent();
			AccordionColumn col = new AccordionColumn("Name", "Name")
			{
				FillWeight = 1f
			};
			lstItems.AddColumn(col);
			col = new AccordionColumn("Line Count", "LineCount")
			{
				Width = 70
			};
			lstItems.AddColumn(col);
			lstItems.RebuildColumns();
			RunReport();
		}

		private void tsRefresh_Click(object sender, EventArgs e)
		{
			RunReport();
		}

		private void RunReport()
		{
			TargetList list = new TargetList(this._character);
			lstItems.DataSource = list;
		}

		private void lstItems_DoubleClick(object sender, EventArgs e)
		{
			ReportTarget target = lstItems.SelectedItem as ReportTarget;
			if (target != null)
			{
				Shell.Instance.LaunchWorkspace<Character>(target.Character, new object[0]);
			}
		}

		private class TargetList : GroupedList<ReportTarget>
		{
			public TargetList(Character source)
			{
				foreach (Case workingCase in source.Behavior.GetWorkingCases())
				{
					AddCharacter(workingCase.Target, workingCase);
					AddCharacter(workingCase.AlsoPlaying, workingCase);
					AddCharacterById(workingCase.Filter, workingCase);
					foreach (TargetCondition condition in workingCase.Conditions)
					{
						AddCharacterById(condition.FilterTag, workingCase);
						AddCharacter(condition.Character, workingCase);
					}
				}
				foreach (Character c in CharacterDatabase.Characters)
				{
					ReportTarget target = _targets.Get(c);
					if (target == null)
					{
						target = new ReportTarget(c);
					}
					AddItem(target);
				}
			}

			private void AddCharacter(string folderName, Case workingCase)
			{
				if (!string.IsNullOrEmpty(folderName))
				{
					Character character = CharacterDatabase.Get(folderName);
					AddCharacter(character, workingCase);
				}
			}

			private void AddCharacterById(string id, Case workingCase)
			{
				if (!string.IsNullOrEmpty(id))
				{
					Character character = CharacterDatabase.GetById(id);
					AddCharacter(character, workingCase);
				}
			}

			private void AddCharacter(Character character, Case workingCase)
			{
				if (character != null)
				{
					ReportTarget target = _targets.GetOrAddDefault(character, () => new ReportTarget(character));
					target.LineCount += workingCase.Lines.Count;
				}
			}

			private Dictionary<Character, ReportTarget> _targets = new Dictionary<Character, ReportTarget>();
		}

		private class ReportTarget : IGroupedItem, INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged
			{
				add { }
				remove { }
			}

			public Character Character { get; set; }

			public string Name
			{
				get	{ return Character.Name; }
			}

			public int LineCount { get; set; }

			public ReportTarget(Character character)
			{
				Character = character;
			}

			public override string ToString()
			{
				return Name;
			}

			public string GetGroupKey()
			{
				return (LineCount > 0) ? "Targeted" : "Untargeted";
			}
		}
	}
}
