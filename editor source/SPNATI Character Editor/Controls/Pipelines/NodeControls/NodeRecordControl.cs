using Desktop;
using ImagePipeline;
using System.Windows.Forms;
using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeRecordControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;
		private PoseMatrix _matrix;

		public NodeRecordControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_index = index;
			_matrix = CharacterDatabase.GetPoseMatrix(node.Graph.Character);

			NodeProperty nodeDefinition = node.Definition.Properties[index];
			recField.RecordContext = _matrix;
			recField.RecordType = nodeDefinition.DataType;
			recField.RecordKey = (string)node.GetProperty(index);
			if (nodeDefinition.DataType == typeof(PipelineGraph))
			{
				recField.RecordFilter = FilterPipelines;
			}

			recField.RecordChanged += RecField_RecordChanged;
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			string key = recField.RecordKey;
			_node.SetProperty(_index, key);
		}

		private bool FilterPipelines(IRecord record)
		{
			//prevent selecting the node or anything that uses this one
			PipelineGraph other = record as PipelineGraph;
			if (other == _node.Graph)
			{
				return false;
			}
			if (ContainsPipeline(other))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Checks if one of this graph's sub-pipelines is this node's graph
		/// </summary>
		/// <param name="graph"></param>
		/// <returns></returns>
		private bool ContainsPipeline(PipelineGraph graph)
		{
			foreach (PipelineNode node in graph.Nodes)
			{
				if (node.Definition.Key == "sub" && node.Properties.Count >= 0)
				{
					PipelineGraph sub = _matrix.GetPipeline((string)node.GetProperty(0));
					if (sub != null)
					{
						if (sub.Key == _node.Graph.Key || ContainsPipeline(sub))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
