using ImagePipeline;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeFloatControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodeFloatControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;

			float value = (float)node.GetProperty(index);
			value = Math.Max((float)valValue.Minimum, Math.Min((float)valValue.Maximum, value));
			valValue.Value = (decimal)value;

			valValue.ValueChanged += ValValue_ValueChanged;
		}

		private void ValValue_ValueChanged(object sender, EventArgs e)
		{
			float value = (float)valValue.Value;
			_node.SetProperty(_index, value);
		}
	}
}
