using Desktop.Skinning;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ErrorReport : SkinnedForm
	{
		public ErrorReport()
		{
			InitializeComponent();
			WorkflowTracker.Instance.Paused = true;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;

			string date = DateTime.Now.ToString();
			date = date.Replace('/', '-');
			date = date.Replace('\\', '-');
			date = date.Replace(":", "");
			date = date.Replace("AM", "");
			date = date.Replace("PM", "");
			date = date.Replace(" ", "");
			string dir = Path.Combine(Config.AppDataDirectory, date);
			Directory.CreateDirectory(dir);

			string crashLog = Path.Combine(dir, "report.txt");
			File.WriteAllText(crashLog, txtDetails.Text);

			int count = 1;
			foreach (Bitmap bmp in WorkflowTracker.Instance.GetScreens())
			{
				string file = Path.Combine(dir, "capture" + count + ".png");
				bmp.Save(file);
				count++;
			}

			string zip = Path.Combine(Config.AppDataDirectory, "details.zip");
			ZipFile.CreateFromDirectory(dir, Path.Combine(dir, zip));
			File.Move(zip, Path.Combine(dir, "details.zip"));

			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					Arguments = dir,
					FileName = "explorer.exe"
				};

				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			WorkflowTracker.Instance.Paused = false;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			WorkflowTracker.Instance.Paused = false;
			Close();
		}
	}
}
