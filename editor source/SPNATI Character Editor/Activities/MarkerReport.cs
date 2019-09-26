using Desktop;
using System;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(MarkerReportRecord), 0)]
	public partial class MarkerReport : Activity
	{
		private Character _selectedCharacter;
		private Marker _selectedMarker;

		public MarkerReport()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			PopulateCharacters();
		}

		protected override void OnActivate()
		{
			SetCharacter(_selectedCharacter);
		}

		private void PopulateCharacters()
		{
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c.Markers.Value.Count > 0)
				{
					lstCharacters.Items.Add(c);
				}
			}
		}

		private void lstCharacters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Character c = lstCharacters.SelectedItem as Character;
			if (c != null)
			{
				if (!c.IsFullyLoaded)
				{
					c = CharacterDatabase.Load(c.FolderName);
					lstCharacters.Items[lstCharacters.SelectedIndex] = c;
				}
			}
			SetCharacter(c);
		}

		private void lstLines_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			MarkerItem marker = lstLines.SelectedItem as MarkerItem;
			if (marker == null) { return; }
			DialogueLine line = marker.Case.Lines[0];
			int stage = marker.Case.Stages[0];
			picPortrait.SetImage(_selectedCharacter.PoseLibrary.GetPose(line.Image), stage);
		}

		private void SetCharacter(Character character)
		{
			if (character == null)
				return;
			picPortrait.SetCharacter(character);
			_selectedCharacter = character;
			PopulateMarkers();
		}

		public override void Quit()
		{
			picPortrait.Destroy();
		}

		private void PopulateMarkers()
		{
			if (_selectedCharacter == null)
				return;
			gridMarker.SetCharacter(_selectedCharacter);
		}

		private void gridMarker_SelectionChanged(object sender, Marker marker)
		{
			SetMarker(marker);
		}

		private void SetMarker(Marker marker)
		{
			_selectedMarker = marker;
			picPortrait.SetImage(null, -1);
			PopulateLines();
		}

		private void PopulateLines()
		{
			lstLines.Items.Clear();
			if (_selectedMarker == null)
				return;

			//Find all the lines that set the marker
			foreach (Case workingCase in _selectedCharacter.Behavior.GetWorkingCases())
			{
				foreach (var line in workingCase.Lines)
				{
					if (line.Marker == _selectedMarker.Name)
					{
						Case container = workingCase.CopyConditions();
						container.Lines.Add(line);
						container.AddStages(workingCase.Stages);
						lstLines.Items.Add(new MarkerItem(container));
					}
				}
			}
			lstLines.Sorted = true;
		}

		private class MarkerItem : IComparable<MarkerItem>
		{
			public Case Case { get; set; }
			public MarkerItem(Case c)
			{
				Case = c;
			}

			public override string ToString()
			{
				string c = Case.ToString();
				string line = Case.Lines[0].Text.ToString();
				return string.Format("{0} - ({1})", line, c);
			}

			public int CompareTo(MarkerItem other)
			{
				return Case.CompareTo(other.Case);
			}
		}
	}

	public class MarkerReportRecord : BasicRecord
	{
		public MarkerReportRecord()
		{
			Name = Key = "Marker Report";
		}
	}

	public class MarkerReportProvider : BasicProvider<MarkerReportRecord>
	{
	}
}
