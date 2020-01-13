using Desktop;
using Desktop.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.Categories
{
	public class MonthCategory : Category
	{
		public MonthCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class MonthProvider : CategoryProvider<MonthCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a month";
		}

		protected override MonthCategory[] GetCategoryValues()
		{
			return new MonthCategory[] {
					new MonthCategory("January", "January"),
					new MonthCategory("February", "February"),
					new MonthCategory("March", "March"),
					new MonthCategory("April", "April"),
					new MonthCategory("May", "May"),
					new MonthCategory("June", "June"),
					new MonthCategory("July", "July"),
					new MonthCategory("August", "August"),
					new MonthCategory("September", "September"),
					new MonthCategory("October", "October"),
					new MonthCategory("November", "November"),
					new MonthCategory("December", "December"),
				};
		}
	}

	public class MonthNumericCategory : Category
	{
		public MonthNumericCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class MonthNumericProvider : CategoryProvider<MonthNumericCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a month";
		}

		protected override MonthNumericCategory[] GetCategoryValues()
		{
			return new MonthNumericCategory[] {
					new MonthNumericCategory("1", "January"),
					new MonthNumericCategory("2", "February"),
					new MonthNumericCategory("3", "March"),
					new MonthNumericCategory("4", "April"),
					new MonthNumericCategory("5", "May"),
					new MonthNumericCategory("6", "June"),
					new MonthNumericCategory("7", "July"),
					new MonthNumericCategory("8", "August"),
					new MonthNumericCategory("9", "September"),
					new MonthNumericCategory("10", "October"),
					new MonthNumericCategory("11", "November"),
					new MonthNumericCategory("12", "December"),
				};
		}
	}

	public class WeekdayCategory : Category
	{
		public WeekdayCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class WeekdayProvider : CategoryProvider<WeekdayCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a Weekday";
		}

		protected override WeekdayCategory[] GetCategoryValues()
		{
			return new WeekdayCategory[] {
					new WeekdayCategory("Monday", "Monday"),
					new WeekdayCategory("Tuesday", "Tuesday"),
					new WeekdayCategory("Wednesday", "Wednesday"),
					new WeekdayCategory("Thursday", "Thursday"),
					new WeekdayCategory("Friday", "Friday"),
					new WeekdayCategory("Saturday", "Saturday"),
					new WeekdayCategory("Sunday", "Sunday"),
				};
		}
	}

	public class WeekdayNumericCategory : Category
	{
		public WeekdayNumericCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class WeekdayNumericProvider : CategoryProvider<WeekdayNumericCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a Weekday";
		}

		protected override WeekdayNumericCategory[] GetCategoryValues()
		{
			return new WeekdayNumericCategory[] {
					new WeekdayNumericCategory("1", "Monday"),
					new WeekdayNumericCategory("2", "Tuesday"),
					new WeekdayNumericCategory("3", "Wednesday"),
					new WeekdayNumericCategory("4", "Thursday"),
					new WeekdayNumericCategory("5", "Friday"),
					new WeekdayNumericCategory("6", "Saturday"),
					new WeekdayNumericCategory("7", "Sunday"),
				};
		}
	}

	public class DayCategory : Category
	{
		public DayCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class DayProvider : CategoryProvider<DayCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a Day";
		}

		protected override DayCategory[] GetCategoryValues()
		{
			List<DayCategory> list = new List<DayCategory>();
			for (int i = 1; i <= 31; i++)
			{
				string suffix = "th";
				int mod = i % 10;
				if (mod == 1) {
					suffix = "st";
				}
				else if (mod == 2) {
					suffix = "nd";
				}
				else if (mod == 3) {
					suffix = "th";
				}
				string key = $"{i}{suffix}";
				list.Add(new DayCategory(key, key));
			}

			return list.ToArray();
		}
	}

	public class DayNumericCategory : Category
	{
		public DayNumericCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class DayNumericProvider : CategoryProvider<DayNumericCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a Day";
		}

		protected override DayNumericCategory[] GetCategoryValues()
		{
			List<DayNumericCategory> list = new List<DayNumericCategory>();
			for (int i = 1; i <= 31; i++)
			{
				string key = i.ToString();
				list.Add(new DayNumericCategory(key, key));
			}

			return list.ToArray();
		}
	}
}
