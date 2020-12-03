using ImagePipeline;
using System;
using System.Windows.Forms;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public partial class NodeCellControl : UserControl, INodeControl
	{
		private PipelineNode _node;
		private int _index;
		private PoseCellReference _cellReference;
		private PoseMatrix _matrix;

		public NodeCellControl()
		{
			InitializeComponent();
		}

		public void SetData(PipelineNode node, int index)
		{
			_node = node;
			_matrix = CharacterDatabase.GetPoseMatrix(node.Graph.Character);
			_index = index;
			_cellReference = _node.GetProperty(_index) as PoseCellReference;

			UpdateLabel();
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			PoseSelectionForm form = new PoseSelectionForm();
			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(_node.Graph.Character);
			PoseSheet sheet = matrix.Sheets[0];
			PoseEntry currentCell = null;
			if (_cellReference != null)
			{
				currentCell = _cellReference.GetCell(matrix);
				if (currentCell != null)
				{
					sheet = currentCell.Stage.Sheet;
					matrix = sheet.Matrix;
				}
			}
			form.SetData(matrix, sheet, currentCell);
			if (form.ShowDialog() == DialogResult.OK)
			{
				PoseEntry cell = form.Cell;
				if (_cellReference == null || _cellReference.GetCell(_matrix) != cell)
				{
					_cellReference = new PoseCellReference(cell);
					if (_cellReference.CharacterFolder == _node.Graph.Character.FolderName)
					{
						_cellReference.CharacterFolder = null;
					}
					_node.SetProperty(_index, _cellReference);
					UpdateLabel();
				}
			}
		}

		private void UpdateLabel()
		{
			PoseCellReference cellRef = _node.GetProperty(_index) as PoseCellReference;
			if (cellRef == null)
			{
				lblPath.Text = "Unknown";
				return;
			}

			bool hasKey = _node.Graph.GetInput(_node, 0) != null;
			if (hasKey)
			{
				lblPath.Text = cellRef.SheetName;
			}
			else
			{
				string stage = cellRef.Stage.ToString();
				if (!string.IsNullOrEmpty(cellRef.StageName))
				{
					stage = cellRef.StageName;
				}

				string key = hasKey ? "key" : cellRef.Key;
				lblPath.Text = $"{stage} > {key}";
			}
		}
	}
}
