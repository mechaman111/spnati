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
	}
}
