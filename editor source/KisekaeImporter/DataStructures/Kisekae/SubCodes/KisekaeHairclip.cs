namespace KisekaeImporter.SubCodes
{
	public class KisekaeHairclip : KisekaeClothes, IPoseable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeHairclip other = pose as KisekaeHairclip;
			if (other == null)
			{
				return;
			}
			Rotation = other.Rotation;
			RotationZ = other.RotationZ;
			Height = other.Height;
			ScaleX = other.ScaleX;
			ScaleY = other.ScaleY;
			Side = other.Side;
			OffsetX = other.OffsetX;	
		}

		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}
		
		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(10)); }
			set { Set(10, value.ToString()); }
		}

		public int ScaleY
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public int RotationZ
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}
	}
}
