using Desktop.Reporting;
using SPNATI_Character_Editor.DataSlicers;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.SlicerControls
{
	[DataSlicerControl(typeof(OneShotSlicer))]
	public partial class OneShotSlicerControl : UserControl, ISlicerControl
	{
		private OneShotSlicer _slicer;

		public OneShotSlicerControl()
		{
			InitializeComponent();
		}

		public void SetSlicer(IDataSlicer slicer)
		{
			_slicer = slicer as OneShotSlicer;
			chkYes.Checked = _slicer.YesGroup.Active;
			chkNo.Checked = _slicer.NoGroup.Active;
			
		}

		private void chkYes_CheckedChanged(object sender, EventArgs e)
		{
			ISlicerGroup group = _slicer.YesGroup;
			group.Active = chkYes.Checked;
		}

		private void chkNo_CheckedChanged(object sender, EventArgs e)
		{
			ISlicerGroup group = _slicer.NoGroup;
			group.Active = chkNo.Checked;
		}
	}
}
