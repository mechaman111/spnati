using System.Drawing;
using System.Threading.Tasks;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	public class CellReferenceNode : NodeDefinition
	{
		public override string Name { get { return "Cell Reference"; } }

		public override string Key { get { return "ref"; } set { } }

		public override PortDefinition[] GetInputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.String, "key")
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
				new NodeProperty(NodePropertyType.CellReference, "Src")
			};
		}

		public override async Task<PipelineResult> Process(PipelineArgs args)
		{
			PoseCellReference cellRef = args.GetProperty<PoseCellReference>(0);
			if (cellRef == null)
			{
				return new PipelineResult(null);
			}

			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(args.Context.Character);
			PoseEntry cell = cellRef.GetCell(matrix);

			string key = args.GetInput<string>(0);
			if (!string.IsNullOrEmpty(key))
			{
				//match up the cell with the input key
				PoseStage stage = cell.Stage;
				PoseEntry match = stage.GetCell(key);
				if (match != null)
				{
					cell = match;
				}
			}

			FileStatus status = matrix.GetStatus(cell);
			if (status == FileStatus.Imported && !args.Context.Settings.DisallowCache)
			{
				//if it's already imported, load it from disk
				string path = matrix.GetFilePath(cell);
				DirectBitmap bmp = new DirectBitmap(path);
				return new PipelineResult(bmp);
			}
			else
			{
				//import and crop it now
				Bitmap bmp = await PipelineImporter.ImportAndCropImage(cell, true) as Bitmap;
				if (bmp == null)
				{
					return new PipelineResult(null);
				}
				return new PipelineResult(new DirectBitmap(bmp));
			}
		}
	}
}
