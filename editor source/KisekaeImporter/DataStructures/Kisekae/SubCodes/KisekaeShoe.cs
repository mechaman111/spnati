namespace KisekaeImporter.SubCodes
{

	public class KisekaeShoe : KisekaeClothes
	{
		public int Top
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}

		public KisekaeColor TopColor1
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor TopColor2
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor TopColor3
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}
	}
}
