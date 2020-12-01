using System.Drawing;
using System.Windows.Forms;
using ImagePipeline;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeColorControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodeColorControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;

			Color color = (Color)node.GetProperty(index);
			colorField1.Color = color;

			colorField1.ColorChanged += ColorField1_ColorChanged;
		}

		private void ColorField1_ColorChanged(object sender, System.EventArgs e)
		{
			Color color = colorField1.Color;
			_node.SetProperty(_index, color);
		}
	}
}
