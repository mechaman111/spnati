namespace KisekaeImporter.SubCodes
{
	public class KisekaeBitField : KisekaeSubCode 
	{
		public KisekaeBitField() : base("") { }

		public void SetBit(int position)
		{
			int value = RawData;
			Set(0, (value | (1 << position)).ToString());
		}

		public void UnsetBit(int position)
		{
			int value = RawData;
			Set(0, (value & ~(1 << position)).ToString());
		}

		public int RawData
		{
			get { return GetInt(0); }
		}
	}
}
