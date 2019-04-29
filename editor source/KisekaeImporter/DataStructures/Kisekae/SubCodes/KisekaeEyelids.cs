namespace KisekaeImporter.SubCodes
{
	public class KisekaeEyelids : KisekaeSubCode 
	{
		public KisekaeEyelids() : base("fb") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}

	public class KisekaeEyelidBottom : KisekaeSubCode
	{
		public KisekaeEyelidBottom() : base("fh") { }

		public int Outline
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}

	public class KisekaeEyelash : KisekaeSubCode
	{
		public KisekaeEyelash() : base("fk") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}
