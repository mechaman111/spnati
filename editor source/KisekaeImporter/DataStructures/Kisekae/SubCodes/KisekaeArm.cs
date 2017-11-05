namespace KisekaeImporter.SubCodes
{
	public class KisekaeArm : KisekaeSubCode
	{
		public KisekaeArm() : base("aa") { }

		public int RightShoulder
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int RightElbow
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int RightArmLayer
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int RightHandShape
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int RightHandRotation
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int LeftShoulder
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int LeftElbow
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int LeftArmLayer
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int LeftHandShape
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int LeftHandRotation
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}
	}
}
