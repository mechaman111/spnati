using System;
using System.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Manages writing to the error log
	/// </summary>
	public static class ErrorLog
	{
		private static string _filename = "errorlog.txt";

		public static void LogError(string error)
		{
			File.AppendAllText(Path.Combine(Config.ExecutableDirectory, _filename), string.Format("\r\n{0} - {1}", DateTime.Now, error));
		}
	}
}
