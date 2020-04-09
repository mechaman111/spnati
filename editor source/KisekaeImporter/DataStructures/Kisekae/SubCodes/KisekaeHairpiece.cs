namespace KisekaeImporter.SubCodes
{
	public class KisekaeHairpiece : KisekaeClothes, IPoseable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeHairpiece other = pose as KisekaeHairpiece;
			if (other == null)
			{
				return;
			}
			ScaleX = other.ScaleX;
			Rotation = other.Rotation;
			Layer = other.Layer;
			Side = other.Side;
			Height = other.Height;
			ScaleY = other.ScaleY;
			Skew = other.Skew;
		}

		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(11)); }
			set { Set(11, value.ToString()); }
		}

		public int ScaleY
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}

		public int Skew
		{
			get { return GetInt(13); }
			set { Set(13, value.ToString()); }
		}

		public bool Flipped
		{
			get { return GetBool(14); }
			set { Set(14, value); }
		}

		public int DockPoint
		{
			get { return GetInt(15); }
			set { Set(15, value); }
		}

		public bool Shaded
		{
			get { return GetBool(16); }
			set { Set(16, value); }
		}
	}
}
