using Desktop;
using Desktop.Providers;

namespace SPNATI_Character_Editor
{
	public class SizeCategory : Category
	{
		public SizeCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class SizeProvider : CategoryProvider<SizeCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a size";
		}

		protected override SizeCategory[] GetCategoryValues()
		{
			return new SizeCategory[] {
					new SizeCategory("small", "small"),
					new SizeCategory("medium", "medium"),
					new SizeCategory("large", "large"),
				};
		}
	}
}
