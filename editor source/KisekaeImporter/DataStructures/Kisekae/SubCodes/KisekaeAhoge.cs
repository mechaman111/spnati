namespace KisekaeImporter.SubCodes
{
	public class KisekaeAhoge : KisekaeSubCode, IHair
	{
		public KisekaeAhoge() : base("r") { }

		/// <summary>
		/// Copies position and rotation pieces from one to another
		/// </summary>
		/// <param name="ahoge"></param>
		public void CopyPositioningFrom(KisekaeAhoge ahoge)
		{
			Rotation = ahoge.Rotation;
			Layer = ahoge.Layer;
			Side = ahoge.Side;
			X = ahoge.X;
			Y = ahoge.Y;
			Width = ahoge.Width;
			Height = ahoge.Height;
			Gravity = ahoge.Gravity;
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

		public int Width
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int X
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int Y
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public bool Gravity
		{
			get { return GetInt(11) == 1; }
			set { Set(11, value ? "1" : "0"); }
		}
	}
}
