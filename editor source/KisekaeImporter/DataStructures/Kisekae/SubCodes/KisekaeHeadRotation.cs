namespace KisekaeImporter.SubCodes
{
	public class KisekaeHeadRotation : KisekaeSubCode
	{
		public KisekaeHeadRotation() : base("ba") { }

		public int Rotation
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
