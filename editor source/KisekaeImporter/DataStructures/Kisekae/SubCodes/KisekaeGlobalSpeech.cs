namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlobalSpeech : KisekaeSubCode, IPoseable, IMoveable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeGlobalSpeech other = pose as KisekaeGlobalSpeech;
			if (other == null)
			{
				return;
			}
			ScaleX = other.ScaleX;
			ScaleY = other.ScaleY;
			ArrowScaleX = other.ArrowScaleX;
			ArrowScaleY = other.ArrowScaleY;
			Rotation = other.Rotation;
			X = other.X;
			Y = other.Y;
			Depth = other.Depth;
			Skew = other.Skew;
			ArrowX = other.ArrowX;
			ArrowY = other.ArrowY;
			ArrowRotation = other.ArrowRotation;
		}

		public int Type
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public int MirrorX
		{
			get { return GetInt(1); }
			set { Set(1, value); }
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(4)); }
			set { Set(4, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(6); }
			set { Set(6, value); }
		}

		public int ScaleY
		{
			get { return GetInt(7); }
			set { Set(7, value); }
		}

		public int Outline
		{
			get { return GetInt(8); }
			set { Set(8, value); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(9)); }
			set { Set(9, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(10); }
			set { Set(10, value); }
		}

		public int X
		{
			get { return GetInt(11); }
			set { Set(11, value); }
		}

		public int Y
		{
			get { return GetInt(12); }
			set { Set(12, value); }
		}

		public int Depth
		{
			get { return GetInt(13); }
			set { Set(13, value); }
		}

		public int Opacity
		{
			get { return GetInt(14); }
			set { Set(14, value); }
		}

		public int BlendMode
		{
			get { return GetInt(15); }
			set { Set(15, value); }
		}

		public int Skew
		{
			get { return GetInt(16); }
			set { Set(16, value); }
		}

		public bool ArrowVisible
		{
			get { return GetBool(17); }
			set { Set(17, value); }
		}

		public int ArrowType
		{
			get { return GetInt(18); }
			set { Set(18, value); }
		}

		public int ArrowSide
		{
			get { return GetInt(19); }
			set { Set(19, value); }
		}

		public int ArrowOutline
		{
			get { return GetInt(20); }
			set { Set(20, value); }
		}

		public int ArrowScaleX
		{
			get { return GetInt(21); }
			set { Set(21, value); }
		}

		public int ArrowScaleY
		{
			get { return GetInt(22); }
			set { Set(22, value); }
		}

		public int ArrowRotation
		{
			get { return GetInt(23); }
			set { Set(23, value); }
		}

		public int ArrowX
		{
			get { return GetInt(24); }
			set { Set(24, value); }
		}

		public int ArrowY
		{
			get { return GetInt(25); }
			set { Set(25, value); }
		}

		public void ShiftX(int offset)
		{
			X += offset;
		}
	}
}
