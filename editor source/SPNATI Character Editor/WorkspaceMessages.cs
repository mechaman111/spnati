namespace SPNATI_Character_Editor
{
	public static class WorkspaceMessages
	{
		/// <summary>
		/// Sent when Ctrl+S is pressed
		/// </summary>
		public const int Save = 1;

		/// <summary>
		/// Send when Ctrl+F is pressed
		/// </summary>
		public const int Find = 2;

		/// <summary>
		/// Sent when Ctrl+H is pressed
		/// </summary>
		public const int Replace = 3;

		/// <summary>
		/// Sent when the character's wardrobe has changed
		/// </summary>
		public const int WardrobeUpdated = 4;
		
		/// <summary>
		/// Sent to inform the preview sidebar to update its image [CharacterImage: image to display]
		/// </summary>
		public const int UpdatePreviewImage = 5;

		/// <summary>
		/// Sent when the active skin has changed
		/// </summary>
		public const int SkinChanged = 6;

		/// <summary>
		/// Sent to inform the preview sidebar to update its text [DialogueLine: line information to display]
		/// </summary>
		public const int PreviewLine = 7;

		/// <summary>
		/// Sent to inform the preview sidebar to update its image [List&lt;string&gt;: markers that are set]
		/// </summary>
		public const int UpdateMarkers = 8;
	}
}
