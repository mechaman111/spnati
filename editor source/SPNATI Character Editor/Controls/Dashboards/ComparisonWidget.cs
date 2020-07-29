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
					int targetIndex = -1;
					string targetFranchise = Config.LastFranchise;
					foreach (Tuple<string, List<Character>> group in franchiseGroups)
					{
						_allGroups.Add(group);
						if (!string.IsNullOrEmpty(targetFranchise) && targetFranchise == group.Item1)
						{
							targetIndex = _allGroups.Count - 1;
						}
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
					if (targetIndex >= 0)
					{
						cboGroups.SelectedIndex = targetIndex;
					}
					else
					{
						cboGroups.SelectedIndex = _allGroups.IndexOf(min);
					}
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
				case PartnerGraphs.CrossFranchise:
					UpdateCrossFranchiseBanter(_franchises);
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

		private int GetTargetedLineCount(Character from, Character to)
		{
			int count = 0;
			if (from is CachedCharacter)
			{
				CachedCharacter cache = from as CachedCharacter;
				count = cache.GetTargetedCountTowards(to.FolderName);
			}
			else
			{
				HashSet<string> usedLines = new HashSet<string>();
				foreach (Case c in from.GetCasesTargetedAtCharacter(to, TargetType.DirectTarget))
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
			return count;
		}

		private void UpdateCrossFranchiseBanter(Tuple<string, List<Character>> franchise)
		{
			lblLines.Text = $"Banter between all {grpPartners.Text}";

			DataSeries outgoing = graphPartners.AddSeries($"Spoken");
			DataSeries incoming = graphPartners.AddSeries($"Received");

			Dictionary<Character, int> incomingCounts = new Dictionary<Character, int>();
			Dictionary<Character, int> outgoingCounts = new Dictionary<Character, int>();

			for (int i = 0; i < franchise.Item2.Count; i++)
			{
				Character c1 = franchise.Item2[i];
				for (int j = i + 1; j < franchise.Item2.Count; j++)
				{
					Character c2 = franchise.Item2[j];
					int c1ToC2 = GetTargetedLineCount(c1, c2);
					int c2ToC1 = GetTargetedLineCount(c2, c1);

					int c1Incoming, c1Outgoing, c2Incoming, c2Outgoing;
					incomingCounts.TryGetValue(c1, out c1Incoming);
					incomingCounts.TryGetValue(c2, out c2Incoming);
					outgoingCounts.TryGetValue(c1, out c1Outgoing);
					outgoingCounts.TryGetValue(c2, out c2Outgoing);
					incomingCounts[c1] = c1Incoming + c1ToC2;
					incomingCounts[c2] = c2Incoming + c2ToC1;
					outgoingCounts[c1] = c1Outgoing + c2ToC1;
					outgoingCounts[c2] = c2Outgoing + c1ToC2;
				}
			}
			foreach (Character character in franchise.Item2)
			{
				if (character == _character)
				{
					continue;
				}
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
			}

			int n = 0;
			foreach (Character character in franchise.Item2)
			{
				int incomingCount;
				int outgoingCount;
				incomingCounts.TryGetValue(character, out incomingCount);
				outgoingCounts.TryGetValue(character, out outgoingCount);
				incoming.AddPoint(n, incomingCount, character.Label);
				outgoing.AddPoint(n, outgoingCount, character.Label);
				n++;
			}
		}

		private enum PartnerGraphs
		{
			Lines = 0,
			Targets = 1,
			CrossFranchise = 2,
			MAX = 3,
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
				Config.LastFranchise = _franchises.Item1;
				DoWork().MoveNext();
			}
			else
			{
				_franchises = null;
			}
		}
	}
}
