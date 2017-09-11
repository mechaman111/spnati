using System.Drawing;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.ImageImport
{
	/// <summary>
	/// Metadata for a pose created from a kisekae code
	/// </summary>
	public class ImageMetadata
	{
		/// <summary>
		/// Stage+emotion
		/// </summary>
		public string ImageKey;

		/// <summary>
		/// KKL import string
		/// </summary>
		public string Data;

		/// <summary>
		/// Custom cropping information
		/// </summary>
		public Rect CropInfo = new Rect(0, 0, 600, 1400);

		public ImageMetadata(string imageKey, string data)
		{
			ImageKey = imageKey;
			Data = data;
		}

		public bool StartsWithVersion()
		{
			return Regex.IsMatch(Data, @"^\d*\*\*.*");
		}

		public string Serialize()
		{
			return string.Format("{0}={1}", ImageKey, Data);
		}
	}
}
