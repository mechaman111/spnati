using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class ActivityLink : UserControl
	{
		private LaunchParameters _launchParams;
		public LaunchParameters LaunchParameters
		{
			get { return _launchParams; }
			set
			{
				_launchParams = value;
				cmdGo.Enabled = lblLink.Enabled = (_launchParams != null);
			}
		}

		public ActivityLink()
		{
			InitializeComponent();
		}

		private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Launch();
		}

		private void cmdGo_Click(object sender, EventArgs e)
		{
			Launch();
		}

		private void Launch()
		{
			Shell.Instance.Launch(LaunchParameters);
		}
	}
}
