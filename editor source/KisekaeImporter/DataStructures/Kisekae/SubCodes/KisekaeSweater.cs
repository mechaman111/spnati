namespace KisekaeImporter.SubCodes
{
	public class KisekaeSweater : KisekaeClothes, IOpenable
	{
		public int Chest
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int RightSleeve
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor1
		{
			get { return new KisekaeColor(GetString(6)); }
			set { Set(6, value.ToString()); }
		}

		public KisekaeColor RightSleeveColor2
		{
			get { return new KisekaeColor(GetString(7)); }
			set { Set(7, value.ToString()); }
		}

		public int LeftSleeve
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor1
		{
			get { return new KisekaeColor(GetString(9)); }
			set { Set(9, value.ToString()); }
		}

		public KisekaeColor LeftSleeveColor2
		{
			get { return new KisekaeColor(GetString(10)); }
			set { Set(10, value.ToString()); }
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
					TopState = 0;
					BottomState = 1;
					break;
				case 2:
					TopState = 1;
					BottomState = 2;
					break;
			}
		}

		public int BottomState
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public int TopState
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}

		public int Length
		{
			get { return GetInt(13); }
			set { Set(13, value.ToString()); }
		}
	}
}
