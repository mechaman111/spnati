using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("f", typeof(KisekaeImage))]
	public class KisekaeExternalParts : KisekaeComponent
	{
		public KisekaeImage GetPart(int index)
		{
			return GetSubCode<KisekaeImage>("f" + index.ToString("00"));
		}
	}
}
