namespace KisekaeImporter.SubCodes
{
	public class KisekaeBodyShape : KisekaeSubCode
	{
		public KisekaeBodyShape() : base("ca") { }

		public int Height
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Stomach
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int TorsoHeight
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int LegLength
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int TorsoWidth
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int HipWidth
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int ArmWidth
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int LegWidth
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int ShoulderWidth
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int BellyButton
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int MuscleDefinition
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}
	}
}
