namespace KisekaeImporter.SubCodes
{
	public class KisekaeBlur : KisekaeSubCode
	{
		public KisekaeBlur() : base("bg") { }

		public int Amount
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int SpreadX
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int SpreadY
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}
	}
}
