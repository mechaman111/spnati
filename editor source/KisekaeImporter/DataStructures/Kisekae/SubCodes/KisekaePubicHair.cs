using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaePubicHair : KisekaeSubCode 
	{
		public KisekaePubicHair() : base("eh") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public int Opacity
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}
	}
}
