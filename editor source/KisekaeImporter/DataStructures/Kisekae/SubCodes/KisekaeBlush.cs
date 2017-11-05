namespace KisekaeImporter.SubCodes
{
	public class KisekaeBlush : KisekaeSubCode 
	{
		public KisekaeBlush() : base("gc") { }

		public int Blush
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Anger
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}
	}
}
