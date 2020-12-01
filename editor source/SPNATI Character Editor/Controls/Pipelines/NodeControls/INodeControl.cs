using ImagePipeline;

namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	public interface INodeControl
	{
		void SetData(PipelineNode node, int index);
	}
}
