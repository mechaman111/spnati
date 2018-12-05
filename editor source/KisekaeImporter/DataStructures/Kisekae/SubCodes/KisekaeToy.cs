namespace KisekaeImporter.SubCodes
{
	public class KisekaeToy : KisekaeClothes
	{
		public KisekaeToy() : base() { }

		public int Mode
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}

		public int Speed
		{
			get { return GetInt(5); }
			set { Set(5, value); }
		}

		public int Frame
		{
			get { return GetInt(6); }
			set { Set(6, value); }
		}

		public bool RandomMode
		{
			get { return GetBool(7); }
			set { Set(7, value); }
		}

		public bool RandomSpeed
		{
			get { return GetBool(8); }
			set { Set(8, value); }
		}

		public bool MoveBody
		{
			get { return GetBool(9); }
			set { Set(9, value); }
		}

		public int Scale
		{
			get { return GetInt(10); }
			set { Set(10, value); }
		}

		public bool Penetrate
		{
			get { return GetBool(11); }
			set { Set(11, value); }
		}
	}
}
