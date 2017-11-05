using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Displays information about markers that characters are using
	/// </summary>
	public partial class MarkerReport : Form
	{
		private Character _selectedCharacter;
		private Marker _selectedMarker;
		private ImageLibrary _imageLibrary;

		public MarkerReport()
		{
			InitializeComponent();

			PopulateCharacters();
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
			if (image != null)
			{
				picPortrait.Image = image.Image;
			}
		}

		private void SetCharacter(Character character)
		{
			_selectedCharacter = character;
			_imageLibrary = new ImageLibrary();
			_imageLibrary.Load(character.FolderName);
			//Make sure WorkingCases are built for easy lookup of lines
			_selectedCharacter.Behavior.BuildWorkingCases(_selectedCharacter);
			PopulateMarkers();
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
			picPortrait.Image = null;
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
}
