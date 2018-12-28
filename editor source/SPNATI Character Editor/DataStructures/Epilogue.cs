using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Epilogue
	{
		[Text(DisplayName = "Unlock Hint", Description ="Hint to the player for how to unlock the ending")]
		[XmlAttribute("hint")]
		public string Hint;

		[ComboBox(DisplayName = "Player Gender", Description = "Player's required gender to be able to view this ending", Options = new string[] { "any", "female", "male" })]
		[XmlAttribute("gender")]
		public string Gender = "any";

		[NumericRange(DisplayName = "Player Starting Layers", Description = "Number of layers the player started with in order to unlock this ending", Minimum = 0, Maximum = 8)]
		[XmlAttribute("playerStartingLayers")]
		public string PlayerStartingLayers;

		[XmlAttribute("markers")]
		public string AllMarkers;

		[XmlAttribute("not-markers")]
		public string NotMarkers;

		[XmlAttribute("any-markers")]
		public string AnyMarkers;

		[XmlAttribute("alsoplaying-markers")]
		public string AlsoPlayingAllMarkers;

		[XmlAttribute("alsoplaying-not-markers")]
		public string AlsoPlayingNotMarkers;

		[XmlAttribute("alsoplaying-any-markers")]
		public string AlsoPlayingAnyMarkers;

		[RecordSelect(DisplayName = "Also Playing", Description = "Character that must also have been playing in order to unlock this ending", RecordType = typeof(Character), RecordFilter = "FilterRecords")]
		[XmlAttribute("alsoPlaying")]
		public string AlsoPlaying;

		[FileSelect(DisplayName = "Gallery Image", Description = "Thumbnail that displays in the unlocked epilogue gallery")]
		[XmlAttribute("img")]
		public string GalleryImage;

		[Text(DisplayName = "Title", Description = "Name that displays when choosing an epilogue in game")]
		[XmlElement("title")]
		public string Title = "New Ending";

		[XmlElement("scene")]
		public List<Scene> Scenes = new List<Scene>();

		#region Legacy properties
		[XmlAttribute("ratio")]
		public string Ratio;

		[XmlNewLine(XmlNewLinePosition.Both)]
		[XmlElement("screen")]
		public List<Screen> Screens = new List<Screen>();

		[XmlElement("background")]
		public List<Background> Backgrounds = new List<Background>();
		#endregion

		public override string ToString()
		{
			string text = Title;
			if (Gender == "male")
				return text + " (m)";
			else if (Gender == "female")
				return text + " (f)";
			return text;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private bool FilterRecords(IRecord record)
		{
			Character c = record as Character;
			return c.FolderName != "human";
		}
	}
}
