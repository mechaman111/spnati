using Desktop.CommonControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class ComparisonWidget : UserControl, IDashboardWidget
	{
		private Character _character;
		private PartnerGraphs _partnerGraphType = PartnerGraphs.Lines;
		private Tuple<string, List<Character>> _franchises;
		private List<Tuple<string, List<Character>>> _allGroups = new List<Tuple<string, List<Character>>>();

		public ComparisonWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpPartners.Shield();
		}

		public bool IsVisible()
		{
			foreach (CharacterTag tag in _character.Tags)
			{
				Tag def = TagDatabase.GetTag(tag.Tag);
				if (def != null && def.Group == "Source Material")
				{
					int max = Config.MaxFranchisePartners;
					List<Tuple<string, List<Character>>> franchiseGroups = TagDatabase.GetGroups("Source Material", _character, max);
					franchiseGroups.Sort((c1, c2) => c1.Item1.CompareTo(c2.Item1));
					Tuple<string, List<Character>> min = null;
					cboGroups.Items.Clear();
					_allGroups.Clear();
					foreach (Tuple<string, List<Character>> group in franchiseGroups)
					{
						_allGroups.Add(group);
						if (min == null || min.Item2.Count > group.Item2.Count)
						{
							min = group;
						}
						Tag t = TagDatabase.GetTag(group.Item1);
						if (t != null)
						{
							cboGroups.Items.Add(t);
						}
					}
					cboGroups.SelectedIndex = _allGroups.IndexOf(min);
					return min != null;
				}
			}
			return false;
		}

		public IEnumerator DoWork()
		{
			//Figure out which source material tag has the most characters in common
			graphPartners.Clear();
			switch (_partnerGraphType)
			{
				case PartnerGraphs.Lines:
					lblLines.Text = "Lines";
					UpdatePartnerLines(_franchises);
					break;
				case PartnerGraphs.Targets:
					lblLines.Text = $"Banter with {_character}";
					UpdatePartnerBanter(_franchises);
					break;
			}
		
			grpPartners.Unshield();
			yield break;
		}

		private void UpdatePartnerLines(Tuple<string, List<Character>> franchise)
		{
			DataSeries lines = graphPartners.AddSeries("Lines");

			Tag tag = TagDatabase.GetTag(franchise.Item1);
			grpPartners.Text = $"{tag.DisplayName} Characters";

			int n = 0;
			foreach (Character character in franchise.Item2)
			{
				if (character is CachedCharacter)
				{
					lines.AddPoint(n++, ((CachedCharacter)character).TotalLines, character.Label);
				}
				else
				{
					CharacterHistory characterHistory = CharacterHistory.Get(character, character != _character);
					LineWork work = characterHistory.Current;

					lines.AddPoint(n++, work.TotalLines, character.Label);
				}
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
				if (character == _character)
				{
					continue;
				}
				counts.Add(character.FolderName, 0);
				incomingLines.Add(character.FolderName, new HashSet<string>());
				indices[character.FolderName] = n;
				int count = 0;

				if (character is CachedCharacter)
				{
					CachedCharacter cache = character as CachedCharacter;
					count = cache.GetTargetedCountTowards(_character.FolderName);
				}
				else
				{
					HashSet<string> usedLines = new HashSet<string>();
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
			DoWork().MoveNext();
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
			DoWork().MoveNext();
		}

		private void cboGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboGroups.SelectedIndex >= 0)
			{
				_franchises = _allGroups[cboGroups.SelectedIndex];
				DoWork().MoveNext();
			}
			else
			{
				_franchises = null;
			}
		}
	}
}
