using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
			Application.Run(new CharacterEditor());
		}

		static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ErrorLog.LogError((e.ExceptionObject as Exception).StackTrace);
		}
	}
}
