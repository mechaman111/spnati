using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	/// <summary>
	/// Interface for generating chart data
	/// </summary>
	public interface IChartDataBuilder
	{
		/// <summary>
		/// Label for the list of charts
		/// </summary>
		/// <returns></returns>
		string GetLabel();

		/// <summary>
		/// Gets the chart title
		/// </summary>
		/// <returns></returns>
		string GetTitle();

		/// <summary>
		/// Gets a list of available views
		/// </summary>
		/// <returns></returns>
		string[] GetViews();

		/// <summary>
		/// Generates data
		/// </summary>
		/// <returns></returns>
		void GenerateData();

		/// <summary>
		/// Gets the data series for a view
		/// </summary>
		/// <param name="view"></param>
		/// <returns></returns>
		List<List<ChartData>> GetSeries(string view);
	}

	public class ChartData
	{
		public string X;
		public int Y;

		public ChartData(string x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
