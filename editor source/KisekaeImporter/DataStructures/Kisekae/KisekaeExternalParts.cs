using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("f", typeof(KisekaeImage))]
	public class KisekaeExternalParts : KisekaeComponent
	{
		public bool HasPart(int index)
		{
			return HasSubCode("f", index);
		}

		public KisekaeImage GetPart(int index)
		{
			return GetSubCode<KisekaeImage>("f" + index.ToString("00"));
		}
	}
}
