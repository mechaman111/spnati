using ImagePipeline;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeFileControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _propertyIndex;

		public NodeFileControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_propertyIndex = index;
			characterImageDialog1.UseAbsolutePaths = true;
			characterImageDialog1.IncludeOpponents = true;
			
			UpdateLabel();
		}

		private void UpdateLabel()
		{
			string path = (string)_node.GetProperty(_propertyIndex);
			lblPath.Text = path;
			toolTip1.SetToolTip(lblPath, path);
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			string filename = (string)_node.GetProperty(_propertyIndex) ?? "";
			filename = filename.Replace("/", "\\");

			if (characterImageDialog1.ShowDialog(_node.Graph.Character, filename) == DialogResult.OK)
			{
				_node.SetProperty(_propertyIndex, characterImageDialog1.FileName);
				UpdateLabel();
			}
		}
	}
}
