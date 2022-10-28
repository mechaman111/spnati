using System;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeImage : KisekaeSubCode, IPoseable
	{
		public void Pose(IPoseable pose)
		{
			KisekaeImage other = pose as KisekaeImage;
			if (other == null)
			{
				return;
			}
			AnchorPoint = other.AnchorPoint;
			ScaleX = other.ScaleX;
			ScaleY = other.ScaleY;
			Layer = other.Layer;
			Side = other.Side;
			OffsetX = other.OffsetX;
			OffsetY = other.OffsetY;
			Rotation = other.Rotation;
			RotationZ = other.RotationZ;
		}

		public int ScaleX
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int OffsetY
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int ScaleY
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		//Unknown: 6
		//Unknown: 7
		//Unknown: 8

		public int RotationZ
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int Opacity
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public int AnchorPoint
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}

		public int Side
		{
			get
			{
				return GetInt(12);
			}
			set
			{
				Set(12, value);
			}
		}
	}
}
