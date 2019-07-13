namespace KisekaeImporter.SubCodes
{
	public class KisekaeShirt : KisekaeClothes, IOpenable
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

		public void SetOpenState(int state)
		{
			switch (state)
			{
				case 0:
					TopState = 0;
					BottomState = 0;
					break;
				case 1:
					TopState = 1;
					BottomState = 0;
					break;
				case 2:
					TopState = 2;
					BottomState = 0;
					break;
				case 3:
					TopState = 2;
					BottomState = 1;
					break;
				case 4:
					TopState = 2;
					BottomState = 2;
					break;
				case 5:
					TopState = 3;
					BottomState = 3;
					break;
			}
		}

		public int Chest
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int RightSleeve
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor1
		{
			get { return new KisekaeColor(GetString(9)); }
			set { Set(9, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor2
		{
			get { return new KisekaeColor(GetString(10)); }
			set { Set(10, value.ToString()); }
		}

		public int LeftSleeve
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor1
		{
			get { return new KisekaeColor(GetString(12)); }
			set { Set(12, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor2
		{
			get { return new KisekaeColor(GetString(13)); }
			set { Set(13, value.ToString()); }
		}

		public int Length
		{
			get { return GetInt(14); }
			set { Set(14, value.ToString()); }
		}

		public int Pocket
		{
			get { return GetInt(15); }
			set { Set(15, value.ToString()); }
		}

		public KisekaeColor PocketColor1
		{
			get { return new KisekaeColor(GetString(16)); }
			set { Set(16, value.ToString()); }
		}

		public KisekaeColor PocketColor2
		{
			get { return new KisekaeColor(GetString(17)); }
			set { Set(17, value.ToString()); }
		}

		public KisekaeColor PocketColor3
		{
			get { return new KisekaeColor(GetString(18)); }
			set { Set(18, value.ToString()); }
		}

		public int PocketSide
		{
			get { return GetInt(19); }
			set { Set(19, value.ToString()); }
		}
		
		public int CollarStyle
		{
			get { return GetInt(20); }
			set { Set(20, value.ToString()); }
		}

		public int CollarPattern
		{
			get { return GetInt(21); }
			set { Set(21, value.ToString()); }
		}

		public int LowerPocket
		{
			get { return GetInt(22); }
			set { Set(22, value.ToString()); }
		}

		public KisekaeColor LowerPocketColor1
		{
			get { return new KisekaeColor(GetString(23)); }
			set { Set(23, value.ToString()); }
		}

		public KisekaeColor LowerPocketColor2
		{
			get { return new KisekaeColor(GetString(24)); }
			set { Set(24, value.ToString()); }
		}

		public KisekaeColor LowerPocketColor3
		{
			get { return new KisekaeColor(GetString(25)); }
			set { Set(25, value.ToString()); }
		}

		public int LowerPocketRotation
		{
			get { return GetInt(26); }
			set { Set(26, value.ToString()); }
		}
	}
}
