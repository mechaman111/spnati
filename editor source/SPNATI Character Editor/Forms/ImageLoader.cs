using Desktop.Skinning;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using System.Drawing;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ImageLoader : SkinnedForm
	{
		public ImageLoader()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Loads a code into KKL
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public async Task<bool> Import(ImageMetadata metadata, ISkin character)
		{
			KisekaeCode code = new KisekaeCode(metadata.Data, true);
			Show();
			Image image = await CharacterGenerator.GetRawImage(code, character, metadata.ExtraData, metadata.SkipPreprocessing);
			if (image == null)
			{
				FailedImport import = new FailedImport();
				import.ShowDialog();
				return false;
			}

			Close();
			return true;
		}
	}
}
