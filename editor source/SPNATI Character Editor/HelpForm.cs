using Desktop.Skinning;
using System;
using System.IO;

namespace SPNATI_Character_Editor
{
	public partial class HelpForm : SkinnedForm
	{
		public HelpForm()
		{
			InitializeComponent();
		}

		private void HelpForm_Load(object sender, EventArgs e)
		{
			wb.Navigate(Path.Combine(Config.ExecutableDirectory, "Help", "help.html"));
		}
	}
}
