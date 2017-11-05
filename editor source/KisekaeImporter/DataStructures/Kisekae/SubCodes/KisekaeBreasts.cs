namespace KisekaeImporter.SubCodes
{
	public class KisekaeBreasts : KisekaeSubCode
	{
		public KisekaeBreasts() : base("di") { }

		public int Size
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
