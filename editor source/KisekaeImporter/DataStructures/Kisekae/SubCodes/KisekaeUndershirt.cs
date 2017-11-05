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
	}
}
