using Desktop;
using Desktop.Providers;

namespace SPNATI_Character_Editor
{
	public class RelativePosition : Category
	{
		public RelativePosition(string key, string value) : base(key, value)
		{
		}
	}

	public class PositionProvider : CategoryProvider<RelativePosition>
	{
		public override string GetLookupCaption()
		{
			return "Choose a position";
		}

		protected override RelativePosition[] GetCategoryValues()
		{
			return new RelativePosition[] {
					new RelativePosition("left", "Left of me"),
					new RelativePosition("right", "Right of me"),
					new RelativePosition("self", "Me"),
				};
		}
	}
}