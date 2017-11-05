using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeNipples : KisekaeSubCode
	{
		public KisekaeNipples() : base("dh") { }

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(0)); }
			set { Set(0, value.ToString()); }
		}

		public int Size
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int XOffset
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int YOffset
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int Shape
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}
