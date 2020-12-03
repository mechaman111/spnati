using System.Drawing;
using System.Threading.Tasks;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	/// <summary>
	/// Process a PipelineGraph within a PipelineGraph. Does not check for recursion.
	/// </summary>
	public class SubNode : NodeDefinition
	{
		public override string Name
		{
			get { return "Pipeline"; }
		}

		public override string Key
		{
			get { return "sub"; }
			set { }
		}

		public override string Group { get { return "Utility"; } }

		public override string Description { get { return "Runs another pipeline using the input as its Cell node"; } }

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "in"),
				new PortDefinition(PortType.String, "key"),
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
			return new NodeProperty[] {
				new NodeProperty(NodePropertyType.String, "Pipeline", null, typeof(PipelineGraph))
			};
		}

		public override async Task<PipelineResult> Process(PipelineArgs args)
		{
			DirectBitmap bmp = args.GetInput<DirectBitmap>(0);
			if (bmp == null)
			{
				return new PipelineResult(null);
			}
			string key = args.GetProperty<string>(0);
			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(args.Context.Character);
			PipelineGraph graph = matrix.GetPipeline(key);
			if (graph == null)
			{
				return new PipelineResult(null);
			}
			PipelineSettings settings = new PipelineSettings(args.Context.Settings);
			settings.CellOverride = bmp;
			settings.CellOverrideKey = args.GetInput<string>(1);
			using (Bitmap output = await graph.Process(args.Context.Cell, settings))
			{
				if (output == null)
				{
					return new PipelineResult(null);
				}
				return new PipelineResult(new DirectBitmap(output));
			}

		}
	}
}
