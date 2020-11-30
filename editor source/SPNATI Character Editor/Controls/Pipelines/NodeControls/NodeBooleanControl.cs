using ImagePipeline;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeBooleanControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodeBooleanControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;

			bool value = (bool)node.GetProperty(index);
			chkValue.Checked = value;

			chkValue.CheckedChanged += ChkValue_CheckedChanged;
		}

		private void ChkValue_CheckedChanged(object sender, EventArgs e)
		{
			_node.SetProperty(_index, chkValue.Checked);
		}
	}
}
