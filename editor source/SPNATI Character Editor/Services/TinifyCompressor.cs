using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinifyAPI;

namespace SPNATI_Character_Editor
{
	public class TinifyCompressor : IImageCompressor
	{
		private const string DefaultAPIKey = "99wT3hQ8h4bB0jLmT0Hzl7XLrX4wtCtj";

		public bool Compress(string filepath)
		{
			string key = Config.TinifyKey;
			if (string.IsNullOrEmpty(key))
			{
				key = DefaultAPIKey;
			}
			
			string dir = Path.GetFileName(Path.GetDirectoryName(filepath));
			dir = Path.Combine(Config.AppDataDirectory, dir, "images");
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
				string message = $"Failed to compress {Path.GetFileName(filepath)}: {ex.Message}";
				MessageBox.Show(message, "Compress Images", MessageBoxButtons.OK);
			}
			return true;
		}
	}
}
