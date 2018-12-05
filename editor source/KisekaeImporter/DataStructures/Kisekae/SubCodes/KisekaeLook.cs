namespace KisekaeImporter.SubCodes
{
	public class KisekaeLook : KisekaeSubCode
	{
		public KisekaeLook() : base("hb") { }

		public int LeftHorizontal
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public bool CrossEyed
		{
			get { return GetInt(1) == 1; }
			set { Set(1, value ? "1" : "0"); }
		}

		public int LeftVertical
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int LeftPupilSize
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int RightPupilSize
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int RightHorizontal
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int RightVertical
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}
	}
}
