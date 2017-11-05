using System;

namespace KisekaeImporter
{
	public class KisekaeSubCodeAttribute : Attribute
	{
		public string Prefix { get; set; }

		public KisekaeSubCodeAttribute(string prefix)
		{
			Prefix = prefix;
		}
	}
}
