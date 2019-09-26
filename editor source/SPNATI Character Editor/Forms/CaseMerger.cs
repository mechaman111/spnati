using System;
using System.Collections.Generic;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.Forms
{
	public partial class CaseMerger : SkinnedForm
	{
		private Character _character;
		private Queue<DuplicateCase> _cases = new Queue<DuplicateCase>();
		private DuplicateCase _case;
		private Case _replacement;
		private int _count;
		private int _current;

		public CaseMerger()
		{
			InitializeComponent();
		}

		public void SetData(Character character)
		{
			_character = character;
			caseControl1.SetCharacter(_character);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (_character != null)
			{
				foreach (DuplicateCase dupe in _character.EnumerateDuplicates())
				{
					_cases.Enqueue(dupe);
				}
				_current = 0;
				_count = _cases.Count;
				Advance();
			}
		}

		private void Advance()
		{
			if (_cases.Count == 0)
			{
				Close();
				return;
			}
			_current++;
			grpCases.Text = $"Case {_current} of {_count}";
			_case = _cases.Dequeue();
			Case src = _case.Duplicates[0];
			Case replacement = src.Copy();
			replacement.AddStages(src.Stages);
			replacement.Tag = _case.ResolutionTag;
			caseControl1.SetCase(new Stage(replacement.Stages[0]), replacement);
			_replacement = replacement;
			lstDupes.Items.Clear();
			foreach (Case copy in _case.Duplicates)
			{
				TriggerDefinition t = TriggerDatabase.GetTrigger(copy.Tag);
				lstDupes.Items.Add(t?.Name ?? copy.Tag);
			}
		}

		private void cmdSkip_Click(object sender, EventArgs e)
		{
			Advance();
		}

		private void cmdMerge_Click(object sender, EventArgs e)
		{
			if (_case.Replacement == null)
			{
				_character.Behavior.AddWorkingCase(_replacement);
			}
			foreach (Case removal in _case.Duplicates)
			{
				_character.Behavior.RemoveWorkingCase(removal);
			}
			Advance();
		}
	}
}
