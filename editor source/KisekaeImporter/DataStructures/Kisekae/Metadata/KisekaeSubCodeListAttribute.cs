using System;

namespace KisekaeImporter
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class KisekaeSubCodeListAttribute : Attribute
	{
		public string Prefix { get; set; }
		public Type SubCodeType { get; set; }

		public KisekaeSubCodeListAttribute(string prefix, Type subcodeType)
		{
			Prefix = prefix;
			SubCodeType = subcodeType;
		}
	}
}
