using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	[ToolboxItem(false)]
	public partial class PropertyTableRow : UserControl, INotifyPropertyChanged
	{
		public event EventHandler RemoveRow;
		public event PropertyChangedEventHandler PropertyChanged;

		public string RemoveCaption { set { toolTip1.SetToolTip(cmdRemove, value); } }

		public PropertyEditControl EditControl { get; private set; }

		public PropertyTableRow()
		{
			InitializeComponent();
		}

		public void Set(PropertyEditControl ctl, PropertyRecord record)
		{
			lblName.Text = record.Name;
			toolTip1.SetToolTip(lblName, record.Description);
			EditControl = ctl;
			EditControl.PropertyChanged += EditControl_PropertyChanged;
			ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			table.Controls.Add(ctl);
			ctl.TabIndex = 0;
			table.SetCellPosition(ctl, new TableLayoutPanelCellPosition(1, 0));
		}

		public void Destroy()
		{
			EditControl.PropertyChanged -= EditControl_PropertyChanged;
		}

		private void EditControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}

		public void OnOtherPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			EditControl.OnOtherPropertyChanged(sender, e);
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			RemoveRow?.Invoke(this, e);
		}
	}
}
