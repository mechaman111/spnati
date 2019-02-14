using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KisekaeImporter.ImageImport
{
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
		/// Key-value pairs of other data to include in the import (ex. transparencies)
		/// </summary>
		public Dictionary<string, string> ExtraData = new Dictionary<string, string>();

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
			List<string> attributes = new List<string>();
			foreach (KeyValuePair<string, string> kvp in ExtraData)
			{
				KisekaePart key = kvp.Key.ToKisekaePart();
				int keyIndex = (int)key;
				attributes.Add($"{keyIndex}={kvp.Value}");
			}
			return string.Format("{0}={1}|{2}", ImageKey, Data, string.Join(",", attributes));
		}
	}
}
