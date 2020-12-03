using ImagePipeline;
using System.Drawing;
using System.Threading.Tasks;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	public class CellNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Obtains an image from the current pose matrix cell"; }
		}

		public override string Name
		{
			get { return "Cell"; }
		}

		public override string Key { get { return "cell"; } set { } }

		public override PortDefinition[] GetInputs()
		{
			return null;
		}

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[] {
				new PortDefinition(PortType.Bitmap, "image"),
				new PortDefinition(PortType.String, "key")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return null;
		}

		public override async Task<PipelineResult> Process(PipelineArgs args)
		{
			PoseEntry cell = args.Context.Cell;
			if (cell == null)
			{
				return new PipelineResult(null, null);
			}

			PoseMatrix matrix = cell.Stage.Sheet.Matrix;
			FileStatus status = matrix.GetStatus(cell, "raw-");

			if (args.Context.Settings.CellOverride != null)
			{
				string overrideKey = args.Context.Settings.CellOverrideKey ?? cell.Key;
				return new PipelineResult(new DirectBitmap(args.Context.Settings.CellOverride.Bitmap), overrideKey);
			}

			bool cached = args.Context.Settings.Cache.ContainsKey("CellGenerated");

			if ((status != FileStatus.Missing && args.Context.Settings.PreviewMode && cached) 
				|| (status == FileStatus.Imported && !args.Context.Settings.DisallowCache))
			{
				//if it's already imported, load it from disk
				string path = matrix.GetFilePath(cell, "raw-");
				DirectBitmap bmp = new DirectBitmap(path);
				args.Context.Settings.Cache["CellGenerated"] = true;
				return new PipelineResult(bmp, cell.Key);
			}
			else
			{
				//import and crop it now
				Bitmap bmp = await PipelineImporter.ImportAndCropImage(cell, true, "raw-") as Bitmap;
				if (bmp == null)
				{
					return new PipelineResult(null, null);
				}
				args.Context.Settings.Cache["CellGenerated"] = true;
				return new PipelineResult(new DirectBitmap(bmp), cell.Key);
			}
		}
	}
}
