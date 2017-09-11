namespace SPNATI_Character_Editor
{
	public class StageName
	{
		public string Id;

		public string DisplayName;

		public StageName(string stage, string displayName)
		{
			Id = stage;
			DisplayName = displayName;
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
