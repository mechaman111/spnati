using System;
using System.Globalization;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueAdvancedControl : UserControl
	{
		public int RowIndex { get; private set; }
		private DialogueLine _line;

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
			float location = 0;
			if (!string.IsNullOrEmpty(line.Location))
			{
				float.TryParse(line.Location, NumberStyles.Number, CultureInfo.InvariantCulture, out location);
			}
			else
			{
				location = 50;
			}
			valLocation.Value = Math.Max(valLocation.Minimum, Math.Min(valLocation.Maximum, (decimal)location));

			cboSize.Text = line.Size ?? "";
			cboAI.Text = line.Intelligence ?? "";
			cboGender.Text = line.Gender ?? "";
			txtLabel.Text = line.Label;

			valWeight.Value = Math.Max(valWeight.Minimum, Math.Min(valWeight.Maximum, (decimal)line.Weight));
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
				_line.Location = location.ToString(CultureInfo.InvariantCulture);
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
	}
}
