using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data grid view that allows handling KeyDown events
	/// </summary>
	public class KeyboardDataGridView : DataGridView
	{
		[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int GetKeyboardState(byte[] keystate);

		[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int SetKeyboardState(byte[] keystate);

		protected override bool ProcessDialogKey(Keys keyData)
		{
			var args = new KeyEventArgs(keyData);
			OnKeyDown(args);
			if (args.Handled)
				return true;
			return base.ProcessDialogKey(keyData);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Space | Keys.Shift))
			{
				//DataGridView will "helpfully" treat Shift+Space as a row selection shortcut for RowHeaderSelect selection mode.
				//This swallows the space keystroke and is horrible in general, so here's a hacky way found online to work around it.

				byte[] keyStates = new byte[255];
				GetKeyboardState(keyStates);
				byte shiftKeyState = keyStates[16];
				keyStates[16] = 0; // turn off the shift key
				SetKeyboardState(keyStates);

				SendKeys.SendWait(" ");

				keyStates[16] = shiftKeyState; // turn the shift key back on
				SetKeyboardState(keyStates);
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
