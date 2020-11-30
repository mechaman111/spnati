using Desktop.Skinning;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueAdvancedControl : UserControl, IDialogueDropDownControl
	{
		public int RowIndex { get; private set; }

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		private Character _character;
		private DialogueLine _line;

		public event EventHandler DataUpdated;

		public DialogueAdvancedControl()
		{
			InitializeComponent();
			cboGender.Items.AddRange(new string[] { "", "female", "male" });
			cboSize.Items.AddRange(new string[] { "", "small", "medium", "large" });
			cboDirection.DataSource = DialogueLine.ArrowDirections;
			cboAI.DataSource = DialogueLine.AILevels;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.Background.Normal;
			foreach (Control child in Controls)
			{
				SkinManager.Instance.ReskinControl(child, skin);
			}
			Invalidate(true);
		}

		public void SetData(int row, DialogueLine line, Character character)
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			RowIndex = row;
			_line = line;
			_character = character;
			cboDirection.Text = line.Direction ?? "";
			
			cboSize.Text = line.Size ?? "";
			cboAI.Text = line.Intelligence ?? "";
			cboGender.Text = line.Gender ?? "";
			txtLabel.Text = line.Label;
			chkResetAI.Checked = line.Intelligence == "";
			chkResetLabel.Checked = line.Label == "";
			chkLayer.Checked = line.Layer == "over";

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

			if (chkResetAI.Checked)
			{
				_line.Intelligence = "";
			}
			else
			{
				string ai = cboAI.Text;
				if (string.IsNullOrEmpty(ai))
				{
					ai = null;
				}
				_line.Intelligence = ai;
			}

			string gender = cboGender.Text;
			if (string.IsNullOrEmpty(gender))
			{
				gender = null;
			}
			_line.Gender = gender;

			if (chkResetLabel.Checked)
			{
				_line.Label = "";
			}
			else
			{
				string label = txtLabel.Text;
				if (label == "")
				{
					label = null;
				}
				_line.Label = label;
			}

			_line.Layer = chkLayer.Checked ? "over" : "";

			return _line;
		}

		private void valLocation_ValueChanged(object sender, EventArgs e)
		{
			DataUpdated?.Invoke(this, e);
		}

		private void chkResetAI_CheckedChanged(object sender, EventArgs e)
		{
			cboAI.Enabled = !chkResetAI.Checked;
		}

		private void chkResetLabel_CheckedChanged(object sender, EventArgs e)
		{
			txtLabel.Enabled = !chkResetLabel.Checked;
		}
	}

	public interface IDialogueDropDownControl : ISkinnedPanel, ISkinControl
	{
		event EventHandler DataUpdated;
		int RowIndex { get; }
		void SetData(int rowIndex, DialogueLine line, Character character);
		DialogueLine GetLine();
	}
}
