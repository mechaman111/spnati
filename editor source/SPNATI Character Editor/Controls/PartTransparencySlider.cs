using KisekaeImporter;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class PartTransparencySlider : UserControl
	{
		public PartTransparencySlider()
		{
			InitializeComponent();

			AddHandlers();
		}

		private void AddHandlers()
		{
			track.ValueChanged += TrackValue_ValueChanged;
			valValue.ValueChanged += ValValue_ValueChanged;
		}

		private void RemoveHandlers()
		{
			track.ValueChanged -= TrackValue_ValueChanged;
			valValue.ValueChanged -= ValValue_ValueChanged;
		}

		private void TrackValue_ValueChanged(object sender, EventArgs e)
		{
			RemoveHandlers();
			valValue.Value = track.Value;
			AddHandlers();
		}

		private void ValValue_ValueChanged(object sender, EventArgs e)
		{
			RemoveHandlers();
			track.Value = (int)valValue.Value;
			AddHandlers();
		}

		public void SetLabel(KisekaePart part)
		{
			lblName.Text = part.ToDisplayName();
		}

		public int Value
		{
			get { return (int)valValue.Value; }
			set
			{
				valValue.Value = value;
			}
		}
	}
}
