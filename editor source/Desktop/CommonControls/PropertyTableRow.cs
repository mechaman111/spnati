using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	[ToolboxItem(false)]
	public partial class PropertyTableRow : UserControl, INotifyPropertyChanged, IComparable<PropertyTableRow>
	{
		public event EventHandler RemoveRow;
		public event EventHandler ToggleFavorite;
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<MacroArgs> EditingMacro;

		public PropertyRecord Record { get; private set; }

		private float _favoriteWidth;
		private bool _allowFavorites;
		public bool AllowFavorites
		{
			get { return _allowFavorites; }
			set
			{
				_favoriteWidth = table.ColumnStyles[3].Width;
				_allowFavorites = value;
				cmdPin.Visible = value;
				if (!_allowFavorites)
				{
					table.ColumnStyles[3].Width = 0;
				}
				else
				{
					table.ColumnStyles[3].Width = _favoriteWidth;
				}
			}
		}

		private float _helpWidth;
		private bool _allowHelp;
		public bool AllowHelp
		{
			get { return _allowHelp; }
			set
			{
				_helpWidth = table.ColumnStyles[0].Width;
				_allowHelp = value;
				lblHelp.Visible = value;
				if (!_allowHelp)
				{
					table.ColumnStyles[0].Width = 0;
				}
				else
				{
					table.ColumnStyles[0].Width = _helpWidth;
				}
			}
		}

		public bool AllowDelete { get; set; }
		
		private bool _favorited;
		public bool Favorited
		{
			get { return _favorited; }
			set
			{
				_favorited = value;
				cmdPin.Image = value ? Properties.Resources.StarFilled : Properties.Resources.StarOutline;
			}
		}

		public string RemoveCaption { set { if (!_required) { toolTip1.SetToolTip(cmdRemove, value); } } }

		public float HeaderWidth
		{
			get { return table.ColumnStyles[1].Width; }
			set { table.ColumnStyles[1].Width = value; }
		}

		private bool _required;
		public bool Required
		{
			get { return _required; }
			set
			{
				_required = value;
				if (_required)
				{
					cmdRemove.Image = Properties.Resources.Eraser;
					toolTip1.SetToolTip(cmdRemove, "Clear");
				}
				else
				{
					cmdRemove.Image = Properties.Resources.Delete;
					toolTip1.SetToolTip(cmdRemove, "Remove");
				}
			}
		}

		public PropertyEditControl EditControl { get; private set; }

		public PropertyTableRow()
		{
			InitializeComponent();
		}

		public void Set(PropertyEditControl ctl, PropertyRecord record)
		{
			Record = record;
			lblName.Text = record.Name;
			toolTip1.SetToolTip(lblHelp.Visible ? lblHelp : lblName, record.Description);
			EditControl = ctl;
			ctl.ChangeLabel += EditControl_ChangeLabel;
			EditControl.PropertyChanged += EditControl_PropertyChanged;
			ctl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			ctl.Dock = DockStyle.Fill;
			table.Controls.Add(ctl);
			ctl.TabIndex = 0;
			table.SetCellPosition(ctl, new TableLayoutPanelCellPosition(2, 0));
			if (record.HideLabel)
			{
				table.ColumnStyles[1].Width = 0;
			}
		}

		public void Destroy()
		{
			EditControl.ChangeLabel -= EditControl_ChangeLabel;
			EditControl.PropertyChanged -= EditControl_PropertyChanged;
			EditControl.Destroy();
			EditControl = null;
		}

		private void EditControl_ChangeLabel(object sender, string label)
		{
			lblName.Text = label;	
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
			if (AllowDelete && !Required)
			{
				RemoveRow?.Invoke(this, e);
			}
			else
			{
				EditControl.Clear();
			}
		}

		private void cmdPin_Click(object sender, EventArgs e)
		{
			itemFavorite.Checked = Favorited;
			menuFavorite.Show(cmdPin, cmdPin.Width, 0);
		}

		public int CompareTo(PropertyTableRow other)
		{
			return Record.CompareTo(other.Record);
		}

		public override string ToString()
		{
			return Record.ToString();
		}

		private void itemFavorite_Click(object sender, EventArgs e)
		{
			Favorited = !Favorited;
			ToggleFavorite?.Invoke(this, e);
		}

		private void itemMacro_Click(object sender, EventArgs e)
		{
			Macro macro = RecordLookup.DoLookup(typeof(Macro), "", true, null, true, Record.DataType) as Macro;
			if (macro != null)
			{
				List<string> values = new List<string>();
				EditControl.BuildMacro(values);
				macro.AddProperty(Record.Property, EditControl.Index, values);
				MacroArgs args = new MacroArgs(macro, RecordLookup.IsNewRecord);
				EditingMacro?.Invoke(this, args);
			}
		}
	}

	public class MacroArgs : EventArgs
	{
		public IMacroEditor Editor { get; private set; }
		public Macro Macro { get; private set; }
		public bool IsNew { get; private set; }

		public MacroArgs(Macro macro, bool isNew)
		{
			Macro = macro;
			IsNew = isNew;
		}

		public void SetEditor(IMacroEditor editor)
		{
			Editor = editor;
		}
	}
}
