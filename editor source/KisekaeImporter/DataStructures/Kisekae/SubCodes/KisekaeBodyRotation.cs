namespace KisekaeImporter.SubCodes
{
	public class KisekaeBodyRotation : KisekaeSubCode
	{
		public KisekaeBodyRotation() : base("be") { }

		public int Rotation
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
