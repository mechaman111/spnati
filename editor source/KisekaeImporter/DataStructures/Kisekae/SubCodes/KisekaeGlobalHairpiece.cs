namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlobalHairpiece : KisekaeSubCode, IPoseable, IMoveable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeGlobalHairpiece other = pose as KisekaeGlobalHairpiece;
			if (other == null)
			{
				return;
			}
			
			ScaleX = other.ScaleX;
			Rotation = other.Rotation;
			X = other.X;
			Y = other.Y;
			Depth = other.Depth;
			Skew = other.Skew;
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

		public int Unknown
		{
			get { return 9; }
			set { Set(5, value); }
		}

		public int ScaleX
		{
			get { return GetInt(6); }
			set { Set(6, value); }
		}

		public int Outline
		{
			get { return GetInt(7); }
			set { Set(7, value); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(8)); }
			set { Set(8, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(9); }
			set { Set(9, value); }
		}

		public int X
		{
			get { return GetInt(10); }
			set { Set(10, value); }
		}

		public int Y
		{
			get { return GetInt(11); }
			set { Set(11, value); }
		}

		public int Depth
		{
			get { return GetInt(12); }
			set { Set(12, value); }
		}

		public int Skew
		{
			get { return GetInt(13); }
			set { Set(13, value); }
		}

		public bool Shaded
		{
			get { return GetBool(14); }
			set { Set(14, value); }
		}

		public void ShiftX(int offset)
		{
			//for some inexplicable reason these operate on a different unit of measurement than everything else, so try to account for that empirically
			offset = (int)(offset * 0.907f);

			X += offset;
		}
	}
}
