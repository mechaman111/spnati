using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	static class Program
	{
		private static WorkflowTracker _workflowFilter;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (!Debugger.IsAttached)
			{
				_workflowFilter = new WorkflowTracker();
				Application.AddMessageFilter(_workflowFilter);
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
			}

			Shell shell = new Shell("SPNATI Character Editor", Properties.Resources.editor);
			SkinManager.Instance.LoadSkins(Path.Combine(Config.ExecutableDirectory, "Resources", "Skins"));
			string skinName = Config.Skin;
			Skin skin = SkinManager.Instance.AvailableSkins.Find(s => s.Name == skinName);
			if (skin == null)
			{
				skin = SkinManager.Instance.AvailableSkins.Find(s => s.Name == "Blue");
			}
			if (skin != null)
			{
				SkinManager.Instance.SetSkin(skin);
			}

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
			try
			{
				Exception exception = e.ExceptionObject as Exception;
				string stack = exception.StackTrace;
				List<string> errorData = new List<string>();
				errorData.Add(exception.Message);
				errorData.Add(stack);

				string date = DateTime.Now.ToString();
				date = date.Replace('/', '-');
				date = date.Replace('\\', '-');
				date = date.Replace(":", "");
				date = date.Replace("AM", "");
				date = date.Replace("PM", "");
				date = date.Replace(" ", "");
				string dir = Path.Combine(Config.AppDataDirectory, date);
				Directory.CreateDirectory(dir);

				string crashLog = Path.Combine(dir, "crash.txt");
				File.WriteAllLines(crashLog, errorData);

				int count = 1;
				foreach (Bitmap bmp in _workflowFilter.GetScreens())
				{
					string file = Path.Combine(dir, "capture" + count + ".png");
					bmp.Save(file);
					count++;
				}

				ErrorLog.LogError(stack);

				string zip = Path.Combine(Config.AppDataDirectory, "crashdetails.zip");
				ZipFile.CreateFromDirectory(dir, Path.Combine(dir, zip));
				File.Move(zip, Path.Combine(dir, "crashdetails.zip"));

				ErrorTrace trace = new ErrorTrace();
				trace.SetPath(dir);
				trace.ShowDialog();
			}
			catch {	}
		}
	}
}
