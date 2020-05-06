namespace KisekaeImporter.SubCodes
{
	public class KisekaeFacePaint : KisekaeSubCode, IPoseable
	{
		public KisekaeFacePaint() : base("t") { }

		public void CopyPositionFrom(KisekaeFacePaint paint)
		{
			Side = paint.Side;
			ScaleX = paint.ScaleX;
			ScaleY = paint.ScaleY;
			Rotation = paint.Rotation;
			OffsetX = paint.OffsetX;
			OffsetY = paint.OffsetY;
		}

		public void Pose(IPoseable pose)
		{
			KisekaeFacePaint other = pose as KisekaeFacePaint;
			if (other == null)
			{
				return;
			}
			CopyPositionFrom(other);
		}

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int ScaleY
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

		public int Side
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int RotationZ
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public int Alpha
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}
	}
}
