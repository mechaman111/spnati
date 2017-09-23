using System;

namespace SPNATI_Character_Editor.Charts
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ChartAttribute : Attribute
	{
		/// <summary>
		/// Sort order
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// Label for the Graphs list
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Function in the class used to generate the chart data
		/// </summary>
		public string GenerationFunction { get; set; }

		public ChartAttribute(string label, int order, string generationFunction)
		{
			Label = label;
			Order = order;
			GenerationFunction = generationFunction;
		}
	}
}
