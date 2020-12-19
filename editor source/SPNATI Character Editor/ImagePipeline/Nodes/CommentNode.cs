using System.Threading.Tasks;

namespace ImagePipeline
{
	public class CommentNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Utility"; }
		}

		public override string Description
		{
			get { return "Allows annotations for complex pipelines"; }
		}

		public override string Name
		{
			get { return "Note"; }
		}

		public override string Key
		{
			get { return "note"; }
			set { }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return null;
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.String, "Note")
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			return Task.FromResult(new PipelineResult(null));
		}
	}
}

