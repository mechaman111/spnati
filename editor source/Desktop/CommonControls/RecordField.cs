using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class RecordField : UserControl, ISkinnedPanel
	{
		public event EventHandler<RecordEventArgs> RecordChanged;

		private Type _recordType;
		public Type RecordType
		{
			get { return _recordType; }
			set
			{
				_recordType = value;
				PopulateAutoComplete();
			}
		}
		public bool AllowCreate { get; set; }

		private object _context;
		/// <summary>
		/// Contextual object to pass to record provider
		/// </summary>
		public object RecordContext
		{
			get { return _context; }
			set
			{
				_context = value;
				PopulateAutoComplete();
			}
		}

		private bool _needValidation = false;
		private bool _selectAllDone = false;
		private bool _populatingRecord = false;

		private Func<IRecord, bool> _filter;
		/// <summary>
		/// Filter for hiding records from the record select
		/// </summary>
		public Func<IRecord, bool> RecordFilter
		{
			get { return _filter; }
			set
			{
				_filter = value;
				PopulateAutoComplete();
			}
		}

		private IRecord _record;
		public IRecord Record
		{
			get { return _record; }
			set
			{
				if (_record != value)
				{
					_populatingRecord = true;
					_needValidation = false;
					_record = value;
					if (_record != null)
					{
						txtInput.Text = _record.ToLookupString();
					}
					else
					{
						txtInput.Text = "";
					}
					txtInput.ForeColor = SkinManager.Instance.CurrentSkin.Surface.ForeColor;
					RecordChanged?.Invoke(this, new RecordEventArgs(_record));
					OnRecordChanged();
					_populatingRecord = false;
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			Skin skin = SkinManager.Instance.CurrentSkin;
			e.Graphics.Clear(Enabled ? skin.FieldBackColor : SystemColors.Control);
			Pen pen = skin.PrimaryColor.GetBorderPen(VisualState.Normal, Focused, Enabled);
			e.Graphics.DrawRectangle(pen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}

		protected virtual void OnRecordChanged()
		{
		}

		public string RecordKey
		{
			get { return _record?.Key; }
			set
			{
				if (value != null)
				{
					Record = RecordLookup.Get(RecordType, value, AllowCreate, RecordContext);
				}
				else
				{
					Record = null;
				}
			}
		}

		private bool _useAutoComplete;
		public bool UseAutoComplete
		{
			get { return _useAutoComplete; }
			set
			{
				_useAutoComplete = value;
			}
		}

		public void PopulateAutoComplete()
		{
			if (UseAutoComplete)
			{
				txtInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				txtInput.AutoCompleteCustomSource = new AutoCompleteStringCollection();
				txtInput.AutoCompleteCustomSource.AddRange(RecordLookup.GetAllRecords(RecordType, RecordFilter, RecordContext).Select(r => r.Name).ToArray());
			}
			else
			{
				txtInput.AutoCompleteMode = AutoCompleteMode.None;
			}
		}

		public List<string> AutoCompleteItems
		{
			set
			{
				txtInput.AutoCompleteCustomSource = new AutoCompleteStringCollection();
				txtInput.AutoCompleteCustomSource.AddRange(value.ToArray());
			}
		}

		private string _placeholderText;
		/// <summary>
		/// Text to display when the field is empty
		/// </summary>
		public string PlaceholderText
		{
			get { return _placeholderText; }
			set
			{
				_placeholderText = value;
				SetPlaceholder();
			}
		}

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Field; }
		}

		public RecordField()
		{
			InitializeComponent();
			txtInput.GotFocus += txtInput_GotFocus;
			cmdSearch.TabStop = false;
		}

		private void RecordField_Load(object sender, EventArgs e)
		{
			SetPlaceholder();
		}

		private string GetText()
		{
			string text = txtInput.Text;
			int bracketStart = text.IndexOf('[');
			int bracketEnd = text.IndexOf(']');
			if (bracketStart >= 0 && bracketEnd > bracketStart)
			{
				text = text.Substring(bracketStart + 1, bracketEnd - bracketStart - 1);
			}
			return text;
		}

		private void RecordField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ValidateField();
		}

		public void SelectAll()
		{
			txtInput.SelectAll();
		}

		private void txtInput_Enter(object sender, EventArgs e)
		{
			ClearPlaceholder();
			if (!string.IsNullOrEmpty(txtInput.Text))
			{
				txtInput.SelectionStart = 0;
				txtInput.SelectionLength = txtInput.Text.Length;
			}
		}

		private void txtInput_MouseUp(object sender, MouseEventArgs e)
		{
			if (!_selectAllDone && txtInput.SelectionLength == 0)
			{
				_selectAllDone = true;
				txtInput.SelectAll();
			}
		}

		private void txtInput_GotFocus(object sender, EventArgs e)
		{
			if (MouseButtons == MouseButtons.None)
			{
				txtInput.SelectAll();
				_selectAllDone = true;
			}
		}

		private void txtInput_Leave(object sender, EventArgs e)
		{
			SetPlaceholder();
			_selectAllDone = false;
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				DoSearch(false);
			}
		}

		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			if (_populatingRecord) { return; }
			_needValidation = !string.IsNullOrEmpty(txtInput.Text);
			string text = txtInput.Text;
			int start = txtInput.SelectionStart;
			int length = txtInput.SelectionLength;
			Record = null;
			txtInput.Text = text;
			txtInput.SelectionStart = start;
			txtInput.SelectionLength = length;
		}

		private void cmdSearch_Click(object sender, EventArgs e)
		{
			DoSearch(true, "");
		}

		private void SetPlaceholder()
		{
			if (string.IsNullOrEmpty(PlaceholderText)) { return; }
			if (txtInput.Text == "")
			{
				txtInput.Text = PlaceholderText;
				txtInput.ForeColor = SkinManager.Instance.CurrentSkin.Gray;
			}
		}

		private void ClearPlaceholder()
		{
			if (string.IsNullOrEmpty(PlaceholderText)) { return; }
			if (txtInput.Text == PlaceholderText && !string.IsNullOrEmpty(PlaceholderText))
			{
				txtInput.Text = "";
				txtInput.ForeColor = SkinManager.Instance.CurrentSkin.Surface.ForeColor;
			}
		}

		public void ValidateField()
		{
			string text = GetText();
			if (_needValidation && text != "" && text != PlaceholderText)
			{
				DoSearch(false);				
			}
		}

		public void DoSearch()
		{
			DoSearch(true);
		}

		protected void DoSearch(bool forceLookup, string text = null)
		{
			if (RecordLookup.IsOpen) { return; }
			if (text == null)
			{
				text = GetText();
				if (text == PlaceholderText)
				{
					text = "";
				}
			}

			IRecord record = RecordLookup.DoLookup(RecordType, text, AllowCreate, RecordFilter, forceLookup, RecordContext);
			if (record != null)
			{
				Record = record;
			}
			else
			{
				ActiveControl = txtInput;
			}
		}
	}

	public class RecordEventArgs : EventArgs
	{
		public IRecord Record { get; private set; }

		public RecordEventArgs(IRecord record)
		{
			Record = record;
		}
	}
}
