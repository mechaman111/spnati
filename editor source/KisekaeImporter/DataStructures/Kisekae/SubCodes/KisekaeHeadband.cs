namespace KisekaeImporter.SubCodes
{
	public class KisekaeHeadband : KisekaeSubCode
	{
		public KisekaeHeadband() : base("lc") { }

		public int Layer
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}
