namespace KisekaeImporter.SubCodes
{
	public class KisekaeSkirt : KisekaeSubCode
	{
		public KisekaeSkirt() : base("ic") { }

		public int Layer
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}
