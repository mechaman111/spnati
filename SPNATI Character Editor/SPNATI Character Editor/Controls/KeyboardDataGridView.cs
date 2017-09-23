using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data grid view that allows handling KeyDown events
	/// </summary>
	public class KeyboardDataGridView : DataGridView
	{
		protected override bool ProcessDialogKey(Keys keyData)
		{
			var args = new KeyEventArgs(keyData);
			OnKeyDown(args);
			if (args.Handled)
				return true;
			return base.ProcessDialogKey(keyData);
		}
	}
}
