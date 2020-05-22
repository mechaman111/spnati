namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlobalArm : KisekaeSubCode, IMoveable, IPoseable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeGlobalArm other = pose as KisekaeGlobalArm;
			if (other == null)
			{
				return;
			}
			Scale = other.Scale;
			Rotation = other.Rotation;
			X = other.X;
			Y = other.Y;
			Depth = other.Depth;
			Hand = other.Hand;
			HandRotation = other.HandRotation;
			MuscleSize = other.MuscleSize;
		}

		public int Type
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public bool MirrorX
		{
			get { return GetBool(1); }
			set { Set(1, value); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public int UnknownSlot
		{
			get { return 9; }
			set { Set(3, value); }
		}

		public int Opacity
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}

		public int Scale
		{
			get { return GetInt(5); }
			set { Set(5, value); }
		}

		public int Rotation
		{
			get { return GetInt(6); }
			set { Set(6, value); }
		}

		public int X
		{
			get { return GetInt(7); }
			set { Set(7, value); }
		}

		public int Y
		{
			get { return GetInt(8); }
			set { Set(8, value); }
		}

		public int Depth
		{
			get { return GetInt(9); }
			set { Set(9, value); }
		}

		public int Hand
		{
			get { return GetInt(10); }
			set { Set(10, value); }
		}

		public int HandRotation
		{
			get { return GetInt(11); }
			set { Set(11, value); }
		}

		public int MuscleSize
		{
			get { return GetInt(12); }
			set { Set(12, value); }
		}

		public void ShiftX(int offset)
		{
			X += offset;
		}
	}
}
