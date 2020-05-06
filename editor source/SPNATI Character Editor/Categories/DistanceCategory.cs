using Desktop;
using Desktop.Providers;

namespace SPNATI_Character_Editor
{
	public class DistanceCategory : Category
	{
		public DistanceCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class DistanceProvider : CategoryProvider<DistanceCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a distance";
		}

		protected override DistanceCategory[] GetCategoryValues()
		{
			return new DistanceCategory[] {
					new DistanceCategory("1", "one slot"),
					new DistanceCategory("2", "two slots"),
					new DistanceCategory("3", "three slots"),
				};
		}
	}
}