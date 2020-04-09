namespace KisekaeImporter.SubCodes
{
	public class KisekaeAhoge : KisekaeSubCode, IColorable, IPoseable
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
			X = ahoge.X;
			Y = ahoge.Y;
			Width = ahoge.Width;
			Height = ahoge.Height;
			Gravity = ahoge.Gravity;
			RotationZ = ahoge.RotationZ;
			RotationPreScale = ahoge.RotationPreScale;
		}

		public void Pose(IPoseable pose)
		{
			KisekaeAhoge other = pose as KisekaeAhoge;
			if (other == null)
			{
				return;
			}
			CopyPositioningFrom(other);
		}

		public void SetColors(KisekaeColor color1, KisekaeColor color2, KisekaeColor color3)
		{
			Color1 = color1;
			Color2 = color2;
			Color3 = color3;
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
			get { return GetBool(11); }
			set { Set(11, value); }
		}

		//Unknown what piece 12 is

		public int RotationZ
		{
			get { return GetInt(13); }
			set { Set(13, value); }
		}

		public bool Shaded
		{
			get { return GetBool(14); }
			set { Set(14, value); }
		}

		public int RotationPreScale
		{
			get { return GetInt(15); }
			set { Set(15, value); }
		}
	}
}
