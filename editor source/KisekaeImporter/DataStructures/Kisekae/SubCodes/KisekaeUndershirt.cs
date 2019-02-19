namespace KisekaeImporter.SubCodes
{
	public class KisekaeUndershirt : KisekaeClothes
	{
		public int BottomState
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int TopState
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int Chest
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int RightSleeve
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor1
		{
			get { return new KisekaeColor(GetString(8)); }
			set { Set(8, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor2
		{
			get { return new KisekaeColor(GetString(9)); }
			set { Set(9, value.ToString()); }
		}

		public int LeftSleeve
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor1
		{
			get { return new KisekaeColor(GetString(11)); }
			set { Set(11, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor2
		{
			get { return new KisekaeColor(GetString(12)); }
			set { Set(12, value.ToString()); }
		}

		public int Length
		{
			get { return GetInt(13); }
			set { Set(13, value.ToString()); }
		}

		public int Pocket
		{
			get { return GetInt(14); }
			set { Set(14, value.ToString()); }
		}

		public KisekaeColor PocketColor1
		{
			get { return new KisekaeColor(GetString(15)); }
			set { Set(15, value.ToString()); }
		}

		public KisekaeColor PocketColor2
		{
			get { return new KisekaeColor(GetString(16)); }
			set { Set(16, value.ToString()); }
		}

		public KisekaeColor PocketColor3
		{
			get { return new KisekaeColor(GetString(17)); }
			set { Set(17, value.ToString()); }
		}

		public int PocketSide
		{
			get { return GetInt(18); }
			set { Set(18, value.ToString()); }
		}
	}
}
