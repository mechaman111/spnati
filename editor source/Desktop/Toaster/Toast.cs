using Desktop.Skinning;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop
{
	internal class Toast
	{
		public string Caption;
		public string Text;
		public SkinnedHighlight Highlight = SkinnedHighlight.Heading;
		public Image Icon;

		public ToastControl Control;

		public float ElapsedTime;
		public ToastState State;

		public Point TargetLocation;

		internal Toast(string caption, string text)
		{
			Caption = caption;
			Text = text;
			State = ToastState.Entering;
		}

		public override string ToString()
		{
			return Caption;
		}
	}

	public enum ToastState
	{
		Entering,
		Showing,
		Hiding
	}
}
