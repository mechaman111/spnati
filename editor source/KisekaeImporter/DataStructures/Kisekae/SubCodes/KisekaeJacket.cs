namespace KisekaeImporter.SubCodes
{
	public class KisekaeJacket : KisekaeClothes, IOpenable
	{
		public int OpenState
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
			OpenState = state;
		}
	}
}
