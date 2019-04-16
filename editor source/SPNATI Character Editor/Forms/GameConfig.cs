using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class GameConfig : Form
	{
		public GameConfig()
		{
			InitializeComponent();
		}

		private void GameConfig_Load(object sender, EventArgs e)
		{
			SpnatiConfig config = SpnatiConfig.Instance;
			chkDebug.Checked = config.Debug;
			chkEpilogues.Checked = config.EpiloguesUnlocked;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			SpnatiConfig config = SpnatiConfig.Instance;
			config.Debug = chkDebug.Checked;
			config.EpiloguesUnlocked = chkEpilogues.Checked;
			DialogResult = DialogResult.OK;
			if (Serialization.ExportConfig())
			{
				Desktop.Shell.Instance.SetStatus("Changes applied to game.");
			}
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
