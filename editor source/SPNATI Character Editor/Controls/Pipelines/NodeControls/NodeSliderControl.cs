using ImagePipeline;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeSliderControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _propertyIndex;

		public NodeSliderControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_propertyIndex = index;

			float value = (float)_node.GetProperty(index);
			int v = (int)(value * 100);
			slider.Value = v;

			slider.ValueChanged += Slider_ValueChanged;
		}

		private void Slider_ValueChanged(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			tmrDebounce.Start();
		}

		private void tmrDebounce_Tick(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			int value = slider.Value;
			float amount = value / 100.0f;
			_node.SetProperty(_propertyIndex, amount);
		}
	}
}
