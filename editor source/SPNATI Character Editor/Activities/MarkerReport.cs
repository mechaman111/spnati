using Desktop;
using System;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(MarkerReportRecord), 0)]
	public partial class MarkerReport : Activity
	{
		private Character _selectedCharacter;
		private Marker _selectedMarker;
		private ImageLibrary _imageLibrary;

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
				if (c.Markers.Count > 0)
				{
					lstCharacters.Items.Add(c);
				}
			}
		}

		private void lstCharacters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetCharacter(lstCharacters.SelectedItem as Character);
		}

		private void lstLines_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			MarkerItem marker = lstLines.SelectedItem as MarkerItem;
			DialogueLine line = marker.Case.Lines[0];
			int stage = marker.Case.Stages[0];
			CharacterImage image = _imageLibrary.Find(string.Format("{0}-{1}", stage, line.Image));
			picPortrait.SetImage(image);
		}

		private void SetCharacter(Character character)
		{
			if (character == null)
				return;
			_selectedCharacter = character;
			_imageLibrary = ImageLibrary.Get(character);
			//Make sure WorkingCases are built for easy lookup of lines
			_selectedCharacter.Behavior.BuildWorkingCases(_selectedCharacter);
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
			picPortrait.SetImage(null);
			PopulateLines();
		}

		private void PopulateLines()
		{
			lstLines.Items.Clear();
			if (_selectedMarker == null)
				return;

			//Find all the lines that set the marker
			foreach (Case workingCase in _selectedCharacter.Behavior.WorkingCases)
			{
				foreach (var line in workingCase.Lines)
				{
					if (line.Marker == _selectedMarker.Name)
					{
						Case container = workingCase.CopyConditions();
						container.Lines.Add(line);
						container.Stages.AddRange(workingCase.Stages);
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
