using Desktop;
using System;
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
			Shell shell = new Shell("SPNATI Character Editor", Properties.Resources.editor);
			shell.Load += Shell_Load;
			shell.FormClosed += Shell_FormClosed;
			Application.Run(shell);
		}

		private static void Shell_Load(object sender, EventArgs e)
		{
			ShellLogic.Initialize();
		}

		private static void Shell_FormClosed(object sender, FormClosedEventArgs e)
		{
			ShellLogic.Teardown();
		}

		static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ErrorLog.LogError((e.ExceptionObject as Exception).StackTrace);
		}
	}
}
