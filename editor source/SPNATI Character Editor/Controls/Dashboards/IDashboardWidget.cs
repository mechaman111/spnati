using System.Collections;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	interface IDashboardWidget
	{
		void Initialize(Character character);
		bool IsVisible();
		IEnumerator DoWork();
	}
}
