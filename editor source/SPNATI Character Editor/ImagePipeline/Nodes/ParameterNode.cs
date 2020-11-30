using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImagePipeline
{
	public class ParameterNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Parameter"; }
		}

		public override string Key
		{
			get { return "parameter"; }
			set { }
		}

		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Reads a parameter from the stage or cell being processed"; }
		}

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.String, "out")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.Integer, "#", 0)
			};
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			int index = args.GetProperty<int>(0);
			List<string> paramList = args.Context.Cell.PipelineParameters;
			if (paramList == null || paramList.Count == 0)
			{
				paramList = args.Context.Cell.Stage.PipelineParameters;
			}
			if (paramList == null || paramList.Count <= index)
			{
				return Task.FromResult(new PipelineResult(""));
			}
			return Task.FromResult(new PipelineResult(paramList[index]));
		}
	}
}
