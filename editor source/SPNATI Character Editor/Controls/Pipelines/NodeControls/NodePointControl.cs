using ImagePipeline;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodePointControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodePointControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;
			Point pt = (Point)_node.GetProperty(_index);
			valX.Text = pt.X.ToString();
			valY.Text = pt.Y.ToString();

			valX.TextChanged += ValueChanged;
			valY.TextChanged += ValueChanged;
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			tmrDebounce.Stop();
			tmrDebounce.Start();
		}

		private void tmrDebounce_Tick(object sender, EventArgs e)
		{
			tmrDebounce.Stop();
			string tx = valX.Text;
			string ty = valY.Text;
			int x;
			int y;
			int.TryParse(tx, out x);
			int.TryParse(ty, out y);
			Point pt = new Point(x, y);
			_node.SetProperty(_index, pt);
		}
	}
}
