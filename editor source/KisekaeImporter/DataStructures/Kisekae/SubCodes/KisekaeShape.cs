namespace KisekaeImporter.SubCodes
{
	/// <summary>
	/// Sub-code that only has a shape
	/// </summary>
	public class KisekaeShape : KisekaeSubCode
	{
		public KisekaeShape() : base("") { }
		
		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
