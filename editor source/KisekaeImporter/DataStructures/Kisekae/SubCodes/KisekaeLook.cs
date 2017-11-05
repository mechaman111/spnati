using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeLook : KisekaeSubCode
	{
		public KisekaeLook() : base("hb") { }

		public int Horizontal
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public bool CrossEyed
		{
			get { return GetInt(1) == 1; }
			set { Set(1, value ? "1" : "0"); }
		}

		public int Vertical
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int PupilSize
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}
	}
}
