namespace KisekaeImporter.SubCodes
{
	public class KisekaeBelt : KisekaeClothes, IPoseable
	{
		public void CopyPositionFrom(KisekaeBelt belt)
		{
			ScaleX = belt.ScaleX;
			ScaleY = belt.ScaleY;
			Rotation = belt.Rotation;
			OffsetX = belt.OffsetX;
			OffsetY = belt.OffsetY;
			Length = belt.Length;
			Crop = belt.Crop;
			RotationZ = belt.RotationZ;
		}

		public void Pose(IPoseable pose)
		{
			KisekaeBelt other = pose as KisekaeBelt;
			if (other == null)
			{
				return;
			}
			CopyPositionFrom(other);
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

		public int OffsetY
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Length
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public int Crop
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public int ScaleY
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(13); }
			set { Set(13, value.ToString()); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(14)); }
			set { Set(14, value.ToString()); }
		}

		public int RotationZ
		{
			get { return GetInt(15); }
			set { Set(15, value.ToString()); }
		}
	}
}
