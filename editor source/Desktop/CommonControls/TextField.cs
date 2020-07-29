using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class TextField : UserControl, ISkinControl
	{
		private bool _selectAllDone;

		public TextField()
		{
			InitializeComponent();

			txtField.GotFocus += TxtField_GotFocus;
			txtField.Enter += TxtField_Enter;
			txtField.MouseUp += TxtField_MouseUp;
			txtField.Leave += TxtField_Leave;
			txtField.TextChanged += TxtField_TextChanged;
		}

		public new event EventHandler TextChanged;

		public bool Multiline
		{
			get { return txtField.Multiline; }
			set { txtField.Multiline = value; }
		}

		public bool ReadOnly
		{
			get { return txtField.ReadOnly; }
			set { txtField.ReadOnly = value; }
		}

		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get { return txtField.AutoCompleteCustomSource; }
			set { txtField.AutoCompleteCustomSource = value; }
		}

		public AutoCompleteSource AutoCompleteSource
		{
			get { return txtField.AutoCompleteSource; }
			set { txtField.AutoCompleteSource = value; }
		}

		public AutoCompleteMode AutoCompleteMode
		{
			get { return txtField.AutoCompleteMode; }
			set { txtField.AutoCompleteMode = value; }
		}

		public string PlaceholderText
		{
			get { return lblPlaceholder.Text; }
			set
			{
				lblPlaceholder.Text = value;
				lblPlaceholder.Visible = IsPlaceholderVisible;
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			}
		}

		public override string Text
		{
			get { return txtField.Text; }
			set
			{
				txtField.Text = value;
				lblPlaceholder.Visible = IsPlaceholderVisible;
			}
		}

		private bool IsPlaceholderVisible
		{
			get { return string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(lblPlaceholder.Text); }
		}

		private void TxtField_TextChanged(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = IsPlaceholderVisible;
			TextChanged?.Invoke(this, e);
		}
		
		private void lblPlaceholder_Click(object sender, EventArgs e)
		{
			txtField.Focus();
		}

		private void valField_Enter(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = false;
		}

		private void valField_Leave(object sender, EventArgs e)
		{
			lblPlaceholder.Visible = IsPlaceholderVisible;
		}

		private void TxtField_GotFocus(object sender, EventArgs e)
		{
			if (MouseButtons == MouseButtons.None && !ReadOnly)
			{
				txtField.SelectAll();
				_selectAllDone = true;
			}
		}

		private void TxtField_Enter(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Text) && !ReadOnly)
			{
				txtField.SelectionStart = 0;
				txtField.SelectionLength = Text.Length;
			}
		}

		private void TxtField_MouseUp(object sender, MouseEventArgs e)
		{
			if (!_selectAllDone && txtField.SelectionLength == 0 && !ReadOnly)
			{
				_selectAllDone = true;
				txtField.SelectAll();
			}
		}

		private void TxtField_Leave(object sender, EventArgs e)
		{
			if (!ReadOnly)
			{
				_selectAllDone = false;
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			lblPlaceholder.BackColor = skin.FieldBackColor;
			lblPlaceholder.ForeColor = skin.Surface.DisabledForeColor;
		}
	}
}
