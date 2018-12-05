namespace KisekaeImporter.SubCodes
{
	public class KisekaeBreastPosition : KisekaeSubCode
	{
		public KisekaeBreastPosition() : base("ae") { }

		public bool Auto
		{
			get { return GetBool(0); }
			set { Set(0, value); }
		}

		public int RightBreast
		{
			get { return GetInt(1); }
			set { Set(1, value); }
		}

		public int LeftBreast
		{
			get { return GetInt(2); }
			set { Set(2, value); }
		}

		public int RightNipple
		{
			get { return GetInt(3); }
			set { Set(3, value); }
		}

		public int LeftNipple
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}
	}
}
