using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ChangeLogReview : Form
	{
		public ChangeLogReview()
		{
			InitializeComponent();
		}

		private void ChangeLogReview_Load(object sender, System.EventArgs e)
		{
			wb.Navigate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VersionHistory", "whatsnew.html"));
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			foreach (string version in Config.VersionHistory)
			{
				if (!version.EndsWith("a") && !version.EndsWith("b"))
				{
					lstVersions.Items.Add(version);
				}
			}
		}

		private void lstVersions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowVersion(lstVersions.SelectedItem?.ToString());
		}

		private void ShowVersion(string version)
		{
			string file = $"VersionHistory/{version}.html";
			StringBuilder sb = new StringBuilder();
			sb.Append("<section class='card'>");
			if (File.Exists(file))
			{
				sb.Append(File.ReadAllText(file));
			}
			else
			{
				sb.Append("No change log for this version.");
			}
			sb.Append("</section>");
			wb.Document.Body.InnerHtml = sb.ToString();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
