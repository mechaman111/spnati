using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeMassage : KisekaeSubCode
	{
		public KisekaeMassage() : base("ad") { }

		public bool RightBreastVertical
		{
			get { return GetBool(0); }
			set { Set(0, value); }
		}

		public bool LeftBreastVertical
		{
			get { return GetBool(1); }
			set { Set(1, value); }
		}

		public bool RightBreastHorizontal
		{
			get { return GetBool(2); }
			set { Set(2, value); }
		}

		public bool LeftBreastHorizontal
		{
			get { return GetBool(3); }
			set { Set(3, value); }
		}

		public bool RightNippleVertical
		{
			get { return GetBool(4); }
			set { Set(4, value); }
		}

		public bool LeftNippleVertical
		{
			get { return GetBool(5); }
			set { Set(5, value); }
		}

		public bool RightNippleHorizontal
		{
			get { return GetBool(6); }
			set { Set(6, value); }
		}

		public bool LeftNippleHorizontal
		{
			get { return GetBool(7); }
			set { Set(7, value); }
		}

		public bool Crotch
		{
			get { return GetBool(8); }
			set { Set(8, value); }
		}

		public bool Kissing
		{
			get { return GetBool(9); }
			set { Set(9, value); }
		}
	}
}
