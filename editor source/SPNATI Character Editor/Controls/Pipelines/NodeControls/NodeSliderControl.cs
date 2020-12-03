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

			NodeProperty propertyDef = node.Definition.Properties[index];
			float minimum = propertyDef.MinValue;
			if (minimum != 0)
			{
				slider.Minimum = (int)(minimum * 100);
				valValue.Minimum = (decimal)minimum;
			}

			float maximum = node.Definition.Properties[index].MaxValue;
			if (maximum > 0)
			{
				slider.Maximum = (int)(maximum * 100);
				valValue.Maximum = (decimal)maximum;
			}

			float value = (float)_node.GetProperty(index);
			int v = (int)(value * 100);
			slider.Value = v;

			valValue.Value = (decimal)value;

			slider.ValueChanged += Slider_ValueChanged;
			valValue.ValueChanged += ValValue_ValueChanged;
		}

		private void ValValue_ValueChanged(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			tmrDebounce.Start();
			UpdateSlider();
		}

		private void Slider_ValueChanged(object sender, System.EventArgs e)
		{
			int value = slider.Value;
			float amount = value / 100.0f;
			valValue.Value = (decimal)amount;
		}

		private void tmrDebounce_Tick(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			float amount = (float)valValue.Value;
			_node.SetProperty(_propertyIndex, amount);
		}

		private void UpdateSlider()
		{
			slider.ValueChanged -= Slider_ValueChanged;
			float value = (float)valValue.Value;
			int v = (int)(value * 100);
			slider.Value = v;
			slider.ValueChanged += Slider_ValueChanged;
		}
	}
}
