namespace SPNATI_Character_Editor
{
	public static class DataConversions
	{
		public static void ConvertVersion(Character character)
		{
			string version = character.Version;
			if (Config.VersionPredates(version, "v3.2"))
			{
				Convert3_2(character);
			}
			character.Version = Config.Version;
		}

		
		private static void Convert3_2(Character character)
		{
		}
	}
}
