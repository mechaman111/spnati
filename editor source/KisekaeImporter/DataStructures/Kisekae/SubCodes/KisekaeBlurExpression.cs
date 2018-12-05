namespace KisekaeImporter.SubCodes
{
	public class KisekaeBlurExpression : KisekaeSubCode
	{
		public KisekaeBlurExpression() : base("bh") { }

		public bool Blur
		{
			get { return GetBool(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
