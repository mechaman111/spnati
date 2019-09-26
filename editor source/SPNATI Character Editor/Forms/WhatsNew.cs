using Desktop.Skinning;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class WhatsNew : SkinnedForm
	{
		public WhatsNew()
		{
			InitializeComponent();

			lblVersion.Text = Config.Version;
		}

		private void WhatsNew_Load(object sender, EventArgs e)
		{
			wb.Navigate(Path.Combine(Config.ExecutableDirectory, "VersionHistory", "whatsnew.html"));
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			DisplayVersionHistory();
		}

		private void DisplayVersionHistory()
		{
			//Acquire updates from all versions skipped
			string lastVersion = Config.GetString(Settings.LastVersionRun);
			StringBuilder updates = new StringBuilder();
			bool isBeta = Config.Version.EndsWith("a") || Config.Version.EndsWith("b");
			for (int i = Config.VersionHistory.Length - 1; i >= 0; i--)
			{
				string version = Config.VersionHistory[i];
				if (version == lastVersion) { break; }
				bool beta = version.EndsWith("a") || version.EndsWith("b");
				if (!isBeta && beta)
				{
					continue; //skip beta versions when not in a beta
				}

				string file = Path.Combine(Config.ExecutableDirectory, $"VersionHistory/{version}.html");
				if (File.Exists(file))
				{
					updates.Append("<section class='card'>");
					updates.Append("<h1>");
					updates.Append(version);
					updates.Append("</h1>");
					updates.Append(File.ReadAllText(file));
					updates.Append("</section>");
				}
			}
			wb.Document.Body.InnerHtml = updates.ToString();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void WhatsNew_FormClosing(object sender, FormClosingEventArgs e)
		{
			Config.Set(Settings.LastVersionRun, Config.Version);
			Config.Save();
		}
	}
}
