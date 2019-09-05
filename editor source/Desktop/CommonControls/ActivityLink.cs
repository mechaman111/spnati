using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class ActivityLink : UserControl
	{
		private Action _launchAction;
		public Action LaunchHandler
		{
			get { return _launchAction; }
			set
			{
				_launchAction = value;
				cmdGo.Enabled = lblLink.Enabled = (_launchParams != null || _launchAction != null);
			}
		}

		private LaunchParameters _launchParams;
		public LaunchParameters LaunchParameters
		{
			get { return _launchParams; }
			set
			{
				_launchParams = value;
				cmdGo.Enabled = lblLink.Enabled = (_launchParams != null || _launchAction != null);
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
			if (_launchAction != null)
			{
				_launchAction();
			}
			else
			{
				Shell.Instance.Launch(LaunchParameters);
			}
		}
	}
}
