using Desktop.Skinning;
using ImagePipeline;
using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor.Forms
{
	public partial class PipelineEditor : SkinnedForm
	{
		public ISkin Character;
		private PipelineGraph _graph;

		public PipelineEditor(ISkin character, PoseStage stage, PoseEntry cell, PipelineGraph graph)
		{
			InitializeComponent();
			Character = character;
			SetName(graph);
			graphEditor1.SetData(character, stage, cell, graph);
			graphEditor1.NameChanged += GraphEditor1_NameChanged;
			_graph = graph;
		}

		private void GraphEditor1_NameChanged(object sender, System.EventArgs e)
		{
			PipelineGraph graph = graphEditor1.Graph;
			SetName(graph);
		}

		private void SetName(PipelineGraph graph)
		{
			Text = $"Pipeline Editor - {graph.Name}";
		}

		private void PipelineEditor_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			graphEditor1.Destroy();
			_graph.DisposeResults();
		}
	}
}
