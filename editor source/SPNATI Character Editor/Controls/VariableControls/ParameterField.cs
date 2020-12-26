using System.ComponentModel;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.VariableControls
{
	public partial class ParameterField : UserControl
	{
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Bindable(true)]
		public string Label
		{
			get { return lblLabel.Text; }
			set { lblLabel.Text = value; }
		}

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Bindable(true)]
		public override string Text
		{
			get { return txtValue.Text; }
			set { txtValue.Text = value; }
		}

		public ParameterField()
		{
			InitializeComponent();
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			OnTextChanged(e);
		}
	}
}
