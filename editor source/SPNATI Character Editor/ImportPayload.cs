using KisekaeImporter.ImageImport;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public class ImportPayload
	{
		[JsonProperty("code")]
		public string Code;

		[JsonProperty("blush")]
		public int Blush = -1;

		[JsonProperty("anger")]
		public int Anger = -1;

		[JsonProperty("juice")]
		public int Juice = -1;

		[JsonProperty("hide_show")]
		public Dictionary<string, float> Transparencies = new Dictionary<string, float>();

		[JsonProperty("remove_motion")]
		public bool RemoveMotion = false;

		public ImportPayload(ImageMetadata metadata)
		{
			Code = metadata.Data;
			if (metadata.ExtraData != null)
			{
				foreach (KeyValuePair<string, string> kvp in metadata.ExtraData)
				{
					int v;
					if (int.TryParse(kvp.Value, out v))
					{
						string[] keys = kvp.Key.Split('/');
						foreach (string key in keys)
						{
							Transparencies.Add(key, v / 100f);
						}
					}
				}
			}
		}
	}
}