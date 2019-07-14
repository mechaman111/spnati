using Desktop.Skinning;
using System;
using System.Threading;

namespace SPNATI_Character_Editor
{
	public partial class ProgressForm : SkinnedForm
	{
		private CancellationTokenSource _cts;

		public ProgressForm()
		{
			InitializeComponent();
		}

		public void SetProgress(string label, int amount, int max)
		{
			lblProgress.Text = label;
			float amt = (float)Math.Min(100, Math.Max(0, amount)) / max;
			progressBar.Value = (int)(amt * 100);
		}

		public void SetCancellationSource(CancellationTokenSource cts)
		{
			_cts = cts;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			_cts.Cancel();
		}
	}
}
