using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class MarkerOptions : UserControl, IDialogueDropDownControl
	{
		public int RowIndex { get; private set; }

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		private DialogueLine _line;

		public event EventHandler DataUpdated;

		public MarkerOptions()
		{
			InitializeComponent();
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

		public void SetData(int rowIndex, DialogueLine line, Character character)
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			RowIndex = rowIndex;
			_line = line;
			string markerValue;
			bool perTarget;
			string marker = Marker.ExtractPieces(line.Marker, out markerValue, out perTarget);
			txtMarker.Text = marker;
			txtValue.Text = markerValue;
			if (markerValue == "+")
			{
				radIncrement.Checked = true;
			}
			else if (markerValue == "-")
			{
				radDecrement.Checked = true;
			}
			else if (markerValue == "0")
			{
				radClear.Checked = true;
			}
			else
			{
				radSet.Checked = true;
			}
			chkPerTarget.Checked = perTarget;
			chkPersistent.Checked = line.IsMarkerPersistent;
		}

		private void radSet_CheckedChanged(object sender, EventArgs e)
		{
			if (radIncrement.Checked)
			{
				txtValue.Text = "+";
			}
			else if (radDecrement.Checked)
			{
				txtValue.Text = "-";
			}
			else if (radClear.Checked)
			{
				txtValue.Text = "0";
			}
			txtValue.Enabled = radSet.Checked;
			DataUpdated?.Invoke(this, e);
		}

		public DialogueLine GetLine()
		{
			string marker = txtMarker.Text;
			if (string.IsNullOrEmpty(marker))
			{
				marker = null;
			}
			string markerValue = txtValue.Text;
			bool perTarget = chkPerTarget.Checked;
			if (perTarget)
			{
				marker += "*";
			}

			_line.IsMarkerPersistent = chkPersistent.Checked;

			if (string.IsNullOrEmpty(markerValue))
			{
				_line.Marker = marker;
			}
			else if (markerValue == "+" || markerValue == "-")
			{
				_line.Marker = $"{markerValue}{marker}";
			}
			else if (markerValue == "+1")
			{
				_line.Marker = $"+{marker}";
			}
			else if (markerValue == "-1")
			{
				_line.Marker = $"-{marker}";
			}
			else
			{
				_line.Marker = $"{marker}={markerValue}";
			}
			return _line;
		}
	}
}
