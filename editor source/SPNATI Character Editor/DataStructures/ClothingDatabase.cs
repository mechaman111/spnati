namespace SPNATI_Character_Editor
{
	public static class ClothingDatabase
	{
		public static CountedSet<string> Items { get; set; } = new CountedSet<string>();

		public static void AddClothing(Clothing item)
		{
			Items.Add(item.GenericName);
		}
	}
}
