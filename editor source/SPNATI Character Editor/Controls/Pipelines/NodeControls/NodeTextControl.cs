using ImagePipeline;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeTextControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;

		public NodeTextControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;

			string value = (string)_node.GetProperty(index) ?? "";
			txtValue.Text = value;

			txtValue.TextChanged += TxtValue_TextChanged;
		}

		private void TxtValue_TextChanged(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			tmrDebounce.Start();
		}

		private void tmrDebounce_Tick(object sender, System.EventArgs e)
		{
			tmrDebounce.Stop();
			_node.SetProperty(_index, txtValue.Text);
		}
	}
}
