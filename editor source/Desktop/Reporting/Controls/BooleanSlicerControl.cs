using System;
using System.Windows.Forms;

namespace Desktop.Reporting.Controls
{
	[DataSlicerControl(typeof(BooleanSlicer))]
	public partial class BooleanSlicerControl : UserControl, ISlicerControl
	{
		private BooleanSlicer _slicer;

		public BooleanSlicerControl()
		{
			InitializeComponent();
		}

		public void SetSlicer(IDataSlicer slicer)
		{
			_slicer = slicer as BooleanSlicer;
			foreach (ISlicerGroup group in _slicer.Groups)
			{
				if (group.Label == "Yes")
				{
					chkYes.Checked = group.Active;
				}
				else if (group.Label == "No")
				{
					chkNo.Checked = group.Active;
				}
			}
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
