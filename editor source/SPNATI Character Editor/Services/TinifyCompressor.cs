using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinifyAPI;

namespace SPNATI_Character_Editor
{
	public class TinifyCompressor : IImageCompressor
	{
		private const string DefaultAPIKey = "99wT3hQ8h4bB0jLmT0Hzl7XLrX4wtCtj";

		public bool Compress(string filepath, ISkin skin)
		{
			string key = Config.TinifyKey;
			if (string.IsNullOrEmpty(key))
			{
				key = DefaultAPIKey;
			}

			string dir = Path.Combine(skin.GetBackupDirectory(), "images");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			try
			{
				string backup = Path.Combine(dir, Path.GetFileName(filepath));
				if (File.Exists(backup))
				{
					File.Delete(backup);
				}
				File.Copy(filepath, backup);
			}
			catch (System.Exception e)
			{
				ErrorLog.LogError(e.Message);
			}

			try
			{
				Tinify.Key = key;
				Task task = Tinify.FromFile(filepath).ToFile(filepath);
				if (!task.Wait(6000))
				{
					return false;
				}
			}
			catch (System.Exception ex)
			{
				string backup = Path.Combine(dir, Path.GetFileName(filepath));
				if (File.Exists(backup))
				{
					File.Delete(backup);
				}

				string message = $"Failed to compress {Path.GetFileName(filepath)}:";
				if (Tinify.CompressionCount == 500 && key == DefaultAPIKey)
				{
					message = "Tinify only allows 500 image compressions per month per account. You are using the default shared account which has run out available compressions for this month. To fix this, you can get your own free developer API key from http://tinypng.com/developers and plug that into the editor's Image Import settings.";
				}
				else
				{
					System.Exception toDisplay = ex;
					if (ex.InnerException != null)
					{
						toDisplay = ex.InnerException;
					}
					message = $"{message}: {toDisplay.Message}";
				}
				MessageBox.Show(message, "Compress Images", MessageBoxButtons.OK);
				return false;
			}
			return true;
		}
	}
}
