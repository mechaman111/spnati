using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeExpressionBrow : KisekaeSubCode
	{
		public KisekaeExpressionBrow() : base("hc") { }

		public int RightShape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int RightAngle
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int RightHeight
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int LeftShape
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int LeftAngle
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int LeftHeight
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}
	}
	}
