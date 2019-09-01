using System.Collections;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	interface IDashboardWidget
	{
		void Initialize(Character character);
		IEnumerator DoWork();
	}
}
