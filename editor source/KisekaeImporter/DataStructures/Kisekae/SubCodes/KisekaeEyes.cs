using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeEyes : KisekaeSubCode
	{
		public KisekaeEyes() : base("fa") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int XOffset
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int YOffset
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int Width
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(6)); }
			set { Set(6, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

	}
}
