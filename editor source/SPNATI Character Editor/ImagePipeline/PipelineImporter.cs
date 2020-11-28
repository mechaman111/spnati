using KisekaeImporter;
using KisekaeImporter.ImageImport;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace ImagePipeline
{
	/// <summary>
	/// Utility class for creating images from a pipeline
	/// </summary>
	public static class PipelineImporter
	{
		/// <summary>
		/// Imports an image from a pipeline
		/// </summary>
		/// <param name="cell">The cell to import</param>
		/// <param name="save">True to save</param>
		/// <returns>The imported image</returns>
		public static async Task<Image> Import(PoseEntry cell, bool save, bool rebuildAssets)
		{
			PoseMatrix matrix = cell.Stage.Sheet.Matrix;

			Image img;

			PipelineGraph pipeline = matrix.GetPipeline(cell.Pipeline) ?? matrix.GetPipeline(cell.Stage.Pipeline);
			if (pipeline != null)
			{
				//run in through the pipeline
				img = await pipeline.Process(cell, new PipelineSettings() { DisallowCache = rebuildAssets });
			}
			else
			{
				//just import from KKL directly
				img = await ImportAndCropImage(cell, false);
			}

			if (img != null && save)
			{
				SaveImage(matrix, cell, img);
			}
			return img;
		}

		/// <summary>
		/// Imports a pose cell without running it through its pipeline
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		public static async Task<Image> ImportAndCropImage(PoseEntry cell, bool save, string prefix = "")
		{
			ImageMetadata metadata = cell.CreateMetadata();
			PoseMatrix matrix = cell.Stage.Sheet.Matrix;
			ISkin character = matrix.Character;
			Image img = await CharacterGenerator.GetCroppedImage(new KisekaeCode(metadata.Data), metadata.CropInfo, character, metadata.ExtraData, metadata.SkipPreprocessing);
			if (img != null && save)
			{
				SaveImage(matrix, cell, img, prefix);
			}
			return img;
		}

		/// <summary>
		/// Saves an image to disk
		/// </summary>
		/// <param name="imageKey">Name of image (stage+pose)</param>
		/// <param name="image">Image to save</param>
		public static string SaveImage(PoseMatrix matrix, PoseEntry cell, Image image, string prefix = "")
		{
			string fullPath = matrix.GetFilePath(cell, prefix);
			string baseDir = Path.GetDirectoryName(fullPath);

			try
			{
				if (!Directory.Exists(baseDir))
				{
					Directory.CreateDirectory(baseDir);
				}
				image.Save(fullPath);
			}
			catch { }

			return fullPath;
		}
	}
}
