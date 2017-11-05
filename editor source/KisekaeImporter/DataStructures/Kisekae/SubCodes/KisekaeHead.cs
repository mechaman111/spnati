namespace KisekaeImporter.SubCodes
{
	public class KisekaeHead : KisekaeSubCode
	{
		public KisekaeHead() : base("dd") { }

		public int Cheeks
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int FaceShape
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int FaceWidth
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int FaceLength
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int HeadScale
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int NeckLength
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}
	}
}
