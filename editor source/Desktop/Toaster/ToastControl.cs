using Desktop.Skinning;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop
{
	internal partial class ToastControl : UserControl
	{
		public event EventHandler Dismiss;

		public Toast Toast { get; set; }

		public string Caption
		{
			set { grpBubble.Text = value; }
		}

		public override string Text
		{
			get { return lblText.Text; }
			set { lblText.Text = value; }
		}

		public Image Icon
		{
			set { grpBubble.Image = value; }
		}

		public SkinnedHighlight Highlight
		{
			set { grpBubble.Highlight = value; }
		}

		public ToastControl(Toast toast)
		{
			InitializeComponent();
			SetStyle(ControlStyles.ContainerControl, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			Toast = toast;
			toast.Control = this;
			Toast = toast;
			Caption = toast.Caption;
			Text = toast.Text;
			Icon = toast.Icon;
			Highlight = toast.Highlight;
		}

		private void cmdDismiss_Click(object sender, EventArgs e)
		{
			Dismiss?.Invoke(this, e);
		}
	}
}
