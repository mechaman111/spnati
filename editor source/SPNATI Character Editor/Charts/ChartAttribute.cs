using System;

namespace SPNATI_Character_Editor.Charts
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ChartAttribute : Attribute
	{
		/// <summary>
		/// ChartType
		/// </summary>
		public ChartType ChartType;

		/// <summary>
		/// Sort order
		/// </summary>
		public int Order { get; set; }

		public ChartAttribute(ChartType chartType, int order)
		{
			ChartType = chartType;
			Order = order;
		}
	}

	public class ChartControlAttribute : Attribute
	{
		public ChartType ChartType;

		public ChartControlAttribute(ChartType type)
		{
			ChartType = type;
		}
	}

	public enum ChartType
	{
		Bar,
		StackedBar
	}
}
