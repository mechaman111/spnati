using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	/// <summary>
	/// Textbox that auto-selects when getting focus
	/// </summary>
	public class TextField : TextBox
	{
		private bool _selectAllDone;

		public TextField() : base()
		{
		}

		protected override void OnGotFocus(EventArgs e)
		{
			if (MouseButtons == MouseButtons.None)
			{
				SelectAll();
				_selectAllDone = true;
			}
			base.OnGotFocus(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			if (!string.IsNullOrEmpty(Text))
			{
				SelectionStart = 0;
				SelectionLength = Text.Length;
			}
			base.OnEnter(e);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (!_selectAllDone && SelectionLength == 0)
			{
				_selectAllDone = true;
				SelectAll();
			}
			base.OnMouseUp(mevent);
		}

		protected override void OnLeave(EventArgs e)
		{
			_selectAllDone = false;
			base.OnLeave(e);
		}
	}
}
