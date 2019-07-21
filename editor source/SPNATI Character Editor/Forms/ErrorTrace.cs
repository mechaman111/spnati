using Desktop.Skinning;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ErrorTrace : SkinnedForm
	{
		private string _directory;

		public ErrorTrace()
		{
			InitializeComponent();
		}

		public void SetPath(string directory)
		{
			_directory = directory;
		}

		private void cmdOpen_Click(object sender, EventArgs e)
		{
			OpenDirectory();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void skinnedLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenDirectory();
		}

		private void OpenDirectory()
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					Arguments = _directory,
					FileName = "explorer.exe"
				};

				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
