using SPNATI_Character_Editor.Charts.Builders;

namespace SPNATI_Character_Editor.Charts
{
	public interface IChartControl
	{
		void SetTitle(string title);
		void SetData(IChartDataBuilder builder, string view);
	}
}
