namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlobalFlag : KisekaeSubCode, IPoseable, IMoveable
	{
		public int Type
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public void Pose(IPoseable pose)
		{
			KisekaeGlobalFlag other = pose as KisekaeGlobalFlag;
			if (other == null)
			{
				return;
			}
			ScaleX = other.ScaleX;
			ScaleY = other.ScaleY;
			Rotation = other.Rotation;
			X = other.X;
			Y = other.Y;
			Depth = other.Depth;
			Skew = other.Skew;
		}

		public int ScaleX
		{
			get { return GetInt(2); }
			set { Set(2, value); }
		}

		public int ScaleY
		{
			get { return GetInt(3); }
			set { Set(3, value); }
		}

		public int Rotation
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}

		public int X
		{
			get { return GetInt(5); }
			set { Set(5, value); }
		}

		public int Y
		{
			get { return GetInt(6); }
			set { Set(6, value); }
		}

		public int Depth
		{
			get { return GetInt(7); }
			set { Set(7, value); }
		}

		public int Shape
		{
			get { return GetInt(8); }
			set { Set(8, value); }
		}

		public int Skew
		{
			get { return GetInt(9); }
			set { Set(9, value); }
		}

		public void ShiftX(int offset)
		{
			X += offset;
		}
	}
}
