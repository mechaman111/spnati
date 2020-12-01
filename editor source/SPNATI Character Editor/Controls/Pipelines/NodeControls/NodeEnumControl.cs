using ImagePipeline;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeEnumControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _propertyIndex;

		public NodeEnumControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_propertyIndex = index;

			NodeProperty propData = node.Definition.Properties[_propertyIndex];
			cboData.Items.AddRange(Enum.GetValues(propData.DataType));
			cboData.SelectedIndex = (int)node.GetProperty(_propertyIndex);
			cboData.SelectedIndexChanged += CboData_SelectedIndexChanged;
		}

		private void CboData_SelectedIndexChanged(object sender, EventArgs e)
		{
			_node.SetProperty(_propertyIndex, cboData.SelectedItem);
		}
	}
}
