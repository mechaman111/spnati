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
		/// Sent when the dialogue tree has changed
		/// </summary>
		public const int DialogueUpdated = 5;

		/// <summary>
		/// Sent to inform the preview sidebar to update its image [CharacterImage: image to display]
		/// </summary>
		public const int UpdatePreviewImage = 6;

		/// <summary>
		/// Sent when a case is added to the working cases from outside the dialogue editor [Case]
		/// </summary>
		public const int CaseAdded = 7;

		/// <summary>
		/// Sent when a case is removed from the working cases from outside the dialogue editor [Case]
		/// </summary>
		public const int CaseRemoved = 7;
	}
}
