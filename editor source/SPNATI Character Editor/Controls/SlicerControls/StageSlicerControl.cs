using Desktop.Reporting;
using Desktop.Reporting.Controls;
using SPNATI_Character_Editor.DataSlicers;

namespace SPNATI_Character_Editor.Controls.SlicerControls
{
	[DataSlicerControl(typeof(StageSlicer))]
	public partial class StageSlicerControl : ComboSlicerControl
	{
		public StageSlicerControl()
		{
			InitializeComponent();
		}
	}
}
