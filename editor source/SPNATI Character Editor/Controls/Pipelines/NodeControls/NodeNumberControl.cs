using ImagePipeline;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeNumberControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodeNumberControl()
		{
			InitializeComponent();
		}

		void INodeControl.SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;

			int value = (int)node.GetProperty(index);
			valValue.Value = value;

			valValue.ValueChanged += ValValue_ValueChanged;
		}

		private void ValValue_ValueChanged(object sender, EventArgs e)
		{
			int value = (int)valValue.Value;
			_node.SetProperty(_index, value);
		}
	}
}
