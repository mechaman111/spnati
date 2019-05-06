using System;
using System.Globalization;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueAdvancedControl : UserControl, IDialogueDropDownControl
	{
		public int RowIndex { get; private set; }
		private DialogueLine _line;

		public event EventHandler DataUpdated;

		public DialogueAdvancedControl()
		{
			InitializeComponent();
			cboDirection.DataSource = DialogueLine.ArrowDirections;
		}

		public void SetData(int row, DialogueLine line)
		{
			RowIndex = row;
			_line = line;
			cboDirection.Text = line.Direction ?? "";
			
			cboSize.Text = line.Size ?? "";
			cboAI.Text = line.Intelligence ?? "";
			cboGender.Text = line.Gender ?? "";
			txtLabel.Text = line.Label;

			valWeight.Value = Math.Max(valWeight.Minimum, Math.Min(valWeight.Maximum, (decimal)line.Weight));

			float location = 0;
			string loc = line.Location;
			if (loc != null && loc.EndsWith("%"))
			{
				loc = loc.Substring(0, loc.Length - 1);
			}
			if (!float.TryParse(loc, NumberStyles.Number, CultureInfo.InvariantCulture, out location))
			{
				location = 50;
			}
			valLocation.Value = Math.Max(valLocation.Minimum, Math.Min(valLocation.Maximum, (decimal)location));

		}

		public DialogueLine GetLine()
		{
			float location = (float)valLocation.Value;
			if (location == 50)
			{
				_line.Location = null;
			}
			else
			{
				_line.Location = location.ToString(CultureInfo.InvariantCulture) + "%";
			}
			string direction = cboDirection.Text;
			if (string.IsNullOrEmpty(direction))
			{
				direction = null;
			}
			_line.Direction = direction;
			_line.Weight = (float)valWeight.Value;

			string size = cboSize.Text;
			if (string.IsNullOrEmpty(size))
			{
				size = null;
			}
			_line.Size = size;

			string ai = cboAI.Text;
			if (string.IsNullOrEmpty(ai))
			{
				ai = null;
			}
			_line.Intelligence = ai;

			string gender = cboGender.Text;
			if (string.IsNullOrEmpty(gender))
			{
				gender = null;
			}
			_line.Gender = gender;

			string label = txtLabel.Text;
			if (label == "")
			{
				label = null;
			}
			_line.Label = label;

			return _line;
		}

		private void valLocation_ValueChanged(object sender, EventArgs e)
		{
			DataUpdated?.Invoke(this, e);
		}
	}

	public interface IDialogueDropDownControl
	{
		event EventHandler DataUpdated;
		int RowIndex { get; }
		void SetData(int rowIndex, DialogueLine line);
		DialogueLine GetLine();
	}
}
