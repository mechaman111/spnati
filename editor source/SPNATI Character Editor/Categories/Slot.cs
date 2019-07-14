using Desktop;
using Desktop.Providers;

namespace SPNATI_Character_Editor
{
	public class Slot : Category
	{
		public Slot(string key, string value) : base(key, value)
		{
		}
	}

	public class SlotProvider : CategoryProvider<Slot>
	{
		public override string GetLookupCaption()
		{
			return "Choose a slot";
		}

		protected override Slot[] GetCategoryValues()
		{
			return new Slot[] {
					new Slot("1", "Outer Left"),
					new Slot("2", "Inner Left"),
					new Slot("3", "Inner Right"),
					new Slot("4", "Outer Right")
				};
		}
	}
}
