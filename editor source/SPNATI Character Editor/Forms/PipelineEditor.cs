using Desktop.Skinning;
using ImagePipeline;
using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor.Forms
{
	public partial class PipelineEditor : SkinnedForm
	{
		public ISkin Character;

		public PipelineEditor(ISkin character, PoseStage stage, PoseEntry cell, PipelineGraph graph)
		{
			InitializeComponent();
			Character = character;
			Text = $"Pipeline Editor - {graph.Name}";
			graphEditor1.SetData(character, stage, cell, graph);
		}

		private void PipelineEditor_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			graphEditor1.Destroy();
		}
	}
}
