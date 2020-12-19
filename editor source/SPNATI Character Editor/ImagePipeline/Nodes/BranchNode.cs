using System.Threading.Tasks;

namespace ImagePipeline
{
	public class BranchNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Branch"; }
		}

		public override string Key { get { return "branch"; } set { } }

		public override string Group
		{
			get { return "Utility"; }
		}

		public override string Description
		{
			get { return "Outputs one of the inputs based upon a boolean Predicate"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.String, "predicate"),
				new PortDefinition(PortType.Bitmap, "true"),
				new PortDefinition(PortType.Bitmap, "false"),
			};
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return null;
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			if (!args.HasInput())
			{
				return Task.FromResult(new PipelineResult(null));
			}
			string input = (args.GetInput<string>(0) ?? "").ToLower();
			bool isTrue = input == "true" || input == "t" || input == "1";
			DirectBitmap img1 = args.GetInput<DirectBitmap>(1);
			DirectBitmap img2 = args.GetInput<DirectBitmap>(2);
			DirectBitmap selected = isTrue ? img1 : img2;
			if (selected == null)
			{
				return Task.FromResult(new PipelineResult(null));
			}
			return Task.FromResult(new PipelineResult(new DirectBitmap(selected)));
		}
	}
}
