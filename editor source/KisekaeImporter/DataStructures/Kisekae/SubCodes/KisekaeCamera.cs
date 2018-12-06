namespace KisekaeImporter.SubCodes
{
	public class KisekaeCamera : KisekaeSubCode
	{
		public int Amount
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public int X
		{
			get { return GetInt(1); }
			set { Set(1, value); }
		}

		public int Y
		{
			get { return GetInt(2); }
			set { Set(2, value); }
		}
	}
}
