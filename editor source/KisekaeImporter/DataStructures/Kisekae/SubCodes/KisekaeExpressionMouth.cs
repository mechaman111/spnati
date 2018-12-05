namespace KisekaeImporter.SubCodes
{
	public class KisekaeExpressionMouth : KisekaeSubCode
	{
		public KisekaeExpressionMouth() : base("hd") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public bool Flipped
		{
			get { return GetInt(1) == 1; }
			set { Set(1, value ? "1" : "0"); }
		}

		public int Width
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int OffsetY
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}
	}
}
