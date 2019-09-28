using Desktop.CommonControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	[DefaultEvent("SelectedIndexChanged")]
	public class SkinnedComboBox : ListControl, ISkinControl, ISkinnedComboBox
	{
		private const int ButtonWidth = 18;
		private const int ArrowRadius = 4;

		private bool _selectAllDone;

		public VisualState MouseState { get; private set; }
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version = 2.0.0.0, Culture = neutral, PublicKeyToken = b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ObjectCollection Items { get; }
		public string KeyMember { get; set; }

		private ToolStripControlHost _controlHost;
		private SkinnedListBox _listBox;
		private ToolStripDropDown _popupControl;
		private SkinnedTextBox _textBox;
		private bool _changingDataSource;

		private Timer _typingTimer;
		private string _typingString = "";
		
		private bool _isDroppedDown = false;
		public bool DroppedDown { get { return _isDroppedDown; } }

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; OnUpdateSkin(SkinManager.Instance.CurrentSkin); Invalidate(); }
		}

		private AutoCompleteSource _autoCompleteSource;
		public AutoCompleteSource AutoCompleteSource
		{
			get { return _textBox.AutoCompleteSource; }
			set
			{
				if (value == AutoCompleteSource.ListItems)
				{
					_autoCompleteSource = value;
					value = AutoCompleteSource.CustomSource;
					PopulateAutoCompleteFromList();
				}
				else
				{
					_autoCompleteSource = value;
				}
				_textBox.AutoCompleteSource = value;
			}
		}

		public new object DataSource
		{
			get { return base.DataSource; }
			set
			{
				_changingDataSource = true;
				int index = _selectedIndex;
				Items.Clear();
				IEnumerable list = value as IEnumerable;
				Items.AddRange(list);
				_changingDataSource = false;
				SelectedIndex = index;
				UpdateIndex(index);
			}
		}

		public AutoCompleteMode AutoCompleteMode
		{
			get { return _textBox.AutoCompleteMode; }
			set { _textBox.AutoCompleteMode = value; }
		}

		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get { return _textBox.AutoCompleteCustomSource; }
			set { _textBox.AutoCompleteCustomSource = value; }
		}

		private ComboBoxStyle _dropDownStyle;
		public ComboBoxStyle DropDownStyle
		{
			get { return _dropDownStyle; }
			set
			{
				_dropDownStyle = value;
				if (_dropDownStyle == ComboBoxStyle.DropDownList)
				{
					_textBox.Visible = false;
				}
				else
				{
					_textBox.Visible = true;
				}
			}
		}

		[Category("Behavior"), Description("Occurs when the SelectedIndex property changes.")]
		public event EventHandler SelectedIndexChanged;

		public SkinnedComboBox()
		{
			Items = new ObjectCollection(this);
			Items.Sorted = Sorted;

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserMouse, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ContainerControl, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			this.SuspendLayout();
			_typingTimer = new Timer();
			_typingTimer.Interval = 1000;
			_typingTimer.Tick += _typingTimer_Tick;
			_textBox = new SkinnedTextBox();
			_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			_textBox.Location = new System.Drawing.Point(3, 4);
			_textBox.Size = new System.Drawing.Size(60, 13);
			_textBox.TabIndex = 0;
			_textBox.WordWrap = false;
			_textBox.Margin = new Padding(0);
			_textBox.Padding = new Padding(0);
			_textBox.TextAlign = HorizontalAlignment.Left;
			_textBox.Resize += new EventHandler(_textBox_Resize);
			_textBox.TextChanged += new EventHandler(_textBox_TextChanged);
			_textBox.Validated += _textBox_Validated;
			_textBox.GotFocus += TxtField_GotFocus;
			_textBox.Enter += TxtField_Enter;
			_textBox.MouseUp += TxtField_MouseUp;
			_textBox.Leave += TxtField_Leave;
			Controls.Add(_textBox);
			ResumeLayout(false);

			PositionControls();

			CreateDropdown();
		}

		private void TxtField_GotFocus(object sender, EventArgs e)
		{
			if (MouseButtons == MouseButtons.None)
			{
				_textBox.SelectAll();
				_selectAllDone = true;
			}
		}

		private void TxtField_Enter(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(_textBox.Text))
			{
				_textBox.SelectionStart = 0;
				_textBox.SelectionLength = _textBox.Text.Length;
			}
		}

		private void TxtField_MouseUp(object sender, MouseEventArgs e)
		{
			if (!_selectAllDone && _textBox.SelectionLength == 0)
			{
				_selectAllDone = true;
				_textBox.SelectAll();
			}
		}

		private void TxtField_Leave(object sender, EventArgs e)
		{
			_selectAllDone = false;
		}

		public static string GetFormattedValue(object item, string displayMember)
		{
			string value = "";
			if (item == null || string.IsNullOrEmpty(displayMember))
			{
				value = item?.ToString() ?? "";
			}
			else
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(item.GetType(), displayMember);
				if (mi != null)
				{
					value = mi.GetValue(item)?.ToString();
				}
				else
				{
					value = item.ToString();
				}
			}
			return value;
		}

		private void _textBox_Validated(object sender, EventArgs e)
		{
			string text = _textBox.Text.ToLower();
			for (int i = 0; i < Items.Count; i++)
			{
				object item = Items[i];
				string value = GetFormattedValue(item, DisplayMember);
				if (value.ToLower() == text)
				{
					SelectedIndex = i;
					break;
				}
				value = GetFormattedValue(item, KeyMember);
				if (value.ToLower() == text)
				{
					SelectedIndex = i;
					break;
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			PositionControls();
			base.OnResize(e);
		}

		private void PositionControls()
		{
			SuspendLayout();
			_textBox.Top = 4;
			_textBox.Left = 5;
			Rectangle buttonRect = GetButtonRect(ClientRectangle);
			_textBox.Width = buttonRect.Left - 2 - _textBox.Left;
			ResumeLayout();
		}

		private static Rectangle GetButtonRect(Rectangle clientBounds)
		{
			return new Rectangle(clientBounds.Left + clientBounds.Width - ButtonWidth, clientBounds.Top, ButtonWidth, clientBounds.Height);
		}

		private static Rectangle GetTextRect(Rectangle clientBounds)
		{
			return new Rectangle(clientBounds.Left + 2, clientBounds.Top, clientBounds.Width - ButtonWidth - 3, clientBounds.Height);
		}

		public override Font Font
		{
			get
			{
				return base.Font;
			}

			set
			{
				base.Font = value;
				_textBox.Font = value;
				Invalidate(true);
			}
		}

		public override string Text
		{
			get
			{
				return _textBox.Text;
			}
			set
			{
				if (_textBox.Text != value)
				{
					for (int i = 0; i < Items.Count; i++)
					{
						if (GetFormattedValue(Items[i], DisplayMember) == value)
						{
							SelectedIndex = i;
							break;
						}
					}
					_textBox.Text = value;
					base.Text = value;
					OnTextChanged(EventArgs.Empty);
				}
			}
		}

		private void _typingTimer_Tick(object sender, EventArgs e)
		{
			_typingString = "";
			_typingTimer.Stop();
		}

		public object SelectedItem
		{
			get { return _selectedIndex >= 0 ? Items[_selectedIndex] : null; }
			set
			{
				int index = Items.IndexOf(value);
				SelectedIndex = index;
			}
		}

		public new object SelectedValue
		{
			get
			{
				object item = SelectedItem;
				if (item != null)
				{
					string value = GetFormattedValue(item, ValueMember);
					return value;
				}
				return null;
			}
			set
			{
				string valueMember = GetFormattedValue(value, ValueMember);
				for (int i = 0; i < Items.Count; i++)
				{
					string v = GetFormattedValue(Items[i], ValueMember);
					if (v == valueMember)
					{
						SelectedIndex = i;
						return;
					}
				}
				Text = valueMember;
			}
		}

		private int _selectedIndex = -1;
		public override int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				if (!_changingDataSource && _selectedIndex != value && value >= 0 && value < Items.Count)
				{
					UpdateIndex(value);
					OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		private void UpdateIndex(int value)
		{
			_selectedIndex = value;
			Text = GetFormattedValue(SelectedItem, DisplayMember);
			Invalidate(true);
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			SelectedIndexChanged?.Invoke(this, e);
		}

		private bool _sorted;
		public bool Sorted
		{
			get { return _sorted; }
			set
			{
				_sorted = value;
				if (Items != null)
				{
					Items.Sorted = value;
				}
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta < 0 && SelectedIndex < Items.Count - 1)
			{
				SelectedIndex++;
			}
			else if (e.Delta > 0 && SelectedIndex > 0)
			{
				SelectedIndex--;
			}
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			e.Control.MouseDown += new MouseEventHandler(Control_MouseDown);
			e.Control.MouseEnter += new EventHandler(Control_MouseEnter);
			e.Control.MouseLeave += new EventHandler(Control_MouseLeave);
			e.Control.GotFocus += new EventHandler(Control_GotFocus);
			e.Control.LostFocus += new EventHandler(Control_LostFocus);
			base.OnControlAdded(e);
		}
		#region NestedControlsEvents
		void Control_LostFocus(object sender, EventArgs e)
		{
			OnLostFocus(e);
		}

		void Control_GotFocus(object sender, EventArgs e)
		{
			OnGotFocus(e);
		}

		void Control_MouseLeave(object sender, EventArgs e)
		{
			OnMouseLeave(e);
		}

		void Control_MouseEnter(object sender, EventArgs e)
		{
			OnMouseEnter(e);
		}

		void Control_MouseDown(object sender, MouseEventArgs e)
		{
			OnMouseDown(e);
		}
		#endregion

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (DropDownStyle == ComboBoxStyle.DropDownList || GetButtonRect(ClientRectangle).Contains(new Point(e.X, e.Y)))
			{
				MouseState = VisualState.Hover;
				Cursor = Cursors.Default;
			}
			else if (GetTextRect(ClientRectangle).Contains(new Point(e.X, e.Y)))
			{
				Cursor = Cursors.IBeam;
			}
			else
			{
				MouseState = VisualState.Normal;
				Cursor = Cursors.Default;
			}
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			MouseState = VisualState.Normal;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && (DropDownStyle == ComboBoxStyle.DropDownList || GetButtonRect(ClientRectangle).Contains(new Point(e.X, e.Y))))
			{
				Focus();
				MouseState = VisualState.Pressed;
				Invalidate();

				if (_isDroppedDown)
				{
					HideDropDown();
				}
				else if (DropDownStyle == ComboBoxStyle.DropDownList || RectangleToScreen(GetButtonRect(ClientRectangle)).Contains(MousePosition))
				{
					ShowDropDown();
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			MouseState = VisualState.Hover;
			Invalidate();
		}

		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Right:
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
					return true;
				case Keys.Escape:
					return DroppedDown;
				default:
					return base.IsInputKey(keyData);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Space)
			{
				ShowDropDown();
			}
			else if (e.KeyCode == Keys.Down && SelectedIndex < Items.Count - 1)
			{
				SelectedIndex++;
			}
			else if (e.KeyCode == Keys.Up && SelectedIndex > 0)
			{
				SelectedIndex--;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				HideDropDown();
			}
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (_textBox.Visible)
			{
				_textBox.Focus();
			}
			Invalidate(true);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			MouseState = VisualState.Normal;
			Invalidate(true);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (DropDownStyle == ComboBoxStyle.DropDownList)
			{
				_typingTimer.Stop();
				_typingTimer.Start();

				char character = e.KeyChar;
				_typingString += character;
				_typingString = _typingString.ToLower();
				bool found = false;
				int start = SelectedIndex;
				if (start < 0)
				{
					start = 0;
				}
				for (int i = start + 1; i < Items.Count; i++)
				{
					string value = GetFormattedValue(Items[i], DisplayMember);
					if (value == null)
					{
						continue;
					}
					if (value.ToLower().StartsWith(_typingString))
					{
						found = true;
						SelectedIndex = i;
						break;
					}
				}
				if (!found)
				{
					for (int i = 0; i < start; i++)
					{
						string value = GetFormattedValue(Items[i], DisplayMember);
						if (value == null)
						{
							continue;
						}
						if (value.StartsWith(_typingString))
						{
							found = true;
							SelectedIndex = i;
							break;
						}
					}
				}
				if (found)
				{
					e.Handled = true;
				}
			}
			base.OnKeyPress(e);
		}

		private void CreateDropdown()
		{
			_listBox = new SkinnedListBox();
			_listBox.IntegralHeight = false;
			_listBox.BorderStyle = BorderStyle.FixedSingle;
			_listBox.SelectionMode = SelectionMode.One;
			_listBox.BindingContext = new BindingContext();
			_listBox.FormattingEnabled = true;

			_controlHost = new ToolStripControlHost(_listBox);
			_controlHost.Padding = new Padding(0);
			_controlHost.Margin = new Padding(0);
			_controlHost.AutoSize = false;

			_popupControl = new ToolStripDropDown();
			_popupControl.Padding = new Padding(0);
			_popupControl.Margin = new Padding(0);
			_popupControl.AutoSize = true;
			_popupControl.DropShadowEnabled = false;
			_popupControl.Items.Add(_controlHost);
			_popupControl.Closed += new ToolStripDropDownClosedEventHandler(_popupControl_Closed);

			_listBox.MouseClick += new MouseEventHandler(_listBox_MouseClick);
			_listBox.MouseMove += new MouseEventHandler(_listBox_MouseMove);
			_listBox.KeyDown += _listBox_KeyDown;
		}

		#region ListBox events
		private void _listBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
			{
				SelectedIndex = _listBox.SelectedIndex;

				if (DropDownStyle == ComboBoxStyle.DropDownList)
				{
					Invalidate(true);
				}

				HideDropDown();
			}
		}

		private void _listBox_MouseMove(object sender, MouseEventArgs e)
		{
			int i;
			for (i = 0; i < (_listBox.Items.Count); i++)
			{
				if (_listBox.GetItemRectangle(i).Contains(_listBox.PointToClient(MousePosition)))
				{
					_listBox.SelectedIndex = i;
					return;
				}
			}
		}

		private void _listBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (_listBox.Items.Count == 0)
			{
				return;
			}

			if (_listBox.SelectedItems.Count != 1)
			{
				return;
			}

			this.SelectedIndex = _listBox.SelectedIndex;

			if (DropDownStyle == ComboBoxStyle.DropDownList)
			{
				this.Invalidate(true);
			}

			HideDropDown();
		}

		void _popupControl_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			_isDroppedDown = false;
			if (!this.RectangleToScreen(this.ClientRectangle).Contains(MousePosition))
			{
				MouseState = VisualState.Normal;
			}
			Invalidate(true);
		}

		private void HideDropDown()
		{
			if (_isDroppedDown && _popupControl.IsDropDown)
			{
				_popupControl.Close();
			}
			_isDroppedDown = false;
		}

		public void ShowDropDown()
		{
			HideDropDown();

			_isDroppedDown = true;

			_controlHost.Control.Width = Width;

			_listBox.Items.Clear();
			foreach (object item in Items)
			{
				_listBox.Items.Add(item);
			}
			_listBox.SelectedIndex = SelectedIndex;
			_listBox.DisplayMember = DisplayMember;
			_listBox.Refresh();

			const int DropDownItemHeight = 13;

			if (Items.Count > 0)
			{
				_listBox.Height = Math.Min(500, DropDownItemHeight * Items.Count + 2);
			}
			else
			{
				_listBox.Height = 26;
			}

			_popupControl.Show(this, CalculateDropPosition(), ToolStripDropDownDirection.BelowRight);
			_listBox.Focus();

			Invalidate(true);
		}

		private Point CalculateDropPosition()
		{
			Point point = new Point(0, this.Height);
			if ((this.PointToScreen(new Point(0, 0)).Y + this.Height + _controlHost.Height) > Screen.PrimaryScreen.WorkingArea.Height)
			{
				point.Y = -this._controlHost.Height - 7;
			}
			return point;
		}

		void _textBox_Resize(object sender, EventArgs e)
		{
			PositionControls();
		}

		void _textBox_TextChanged(object sender, EventArgs e)
		{
			OnTextChanged(e);
		}
		#endregion

		public void OnUpdateSkin(Skin skin)
		{
			Font = Skin.TextFont;
			BackColor = skin.FieldBackColor;
			_listBox.OnUpdateSkin(skin);
		}

		private void PopulateAutoCompleteFromList()
		{
			AutoCompleteStringCollection col = new AutoCompleteStringCollection();
			foreach (object item in Items)
			{
				string value = GetFormattedValue(item, DisplayMember);
				col.Add(value);
			}
			_textBox.AutoCompleteCustomSource = col;
		}

		private object _itemDuringChange;
		public void PreItemChange()
		{
			_itemDuringChange = SelectedItem;
		}

		public void PostItemChange()
		{
			int index = Items.IndexOf(_itemDuringChange);
			if (string.IsNullOrEmpty(Text) || index >= 0 || DropDownStyle == ComboBoxStyle.DropDownList)
			{
				//only change if the text was something that isn't part of the list
				UpdateIndex(index);
			}
			_itemDuringChange = null;

			if (AutoCompleteSource == AutoCompleteSource.ListItems)
			{
				PopulateAutoCompleteFromList();
			}
		}

		public void SortItems()
		{
			object item = SelectedItem;
			Items.SortItems();
			if (item != null)
			{
				UpdateIndex(Items.IndexOf(item));
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle bounds = ClientRectangle;

			bool textBoxFocused = _textBox.Focused;
			bool focused = Focused;
			bool enabled = Enabled;
			ComboBoxStyle style = DropDownStyle;
			Graphics g = e.Graphics;
			string value = GetFormattedValue(SelectedItem, DisplayMember);
			RenderComboBox(g, bounds, value, textBoxFocused, focused, enabled, style, FieldType, MouseState);
		}

		public static void RenderComboBox(Graphics g, Rectangle bounds, object value, bool textBoxFocused, bool focused, bool enabled, ComboBoxStyle style, SkinnedFieldType fieldType, VisualState state)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Rectangle buttonRect = GetButtonRect(bounds);
			Rectangle textRect = GetTextRect(bounds);

			ColorSet colorSet;
			if (style == ComboBoxStyle.DropDownList)
			{
				colorSet = skin.GetFieldColorSet(fieldType, SkinnedLightLevel.Light);
			}
			else
			{
				colorSet = skin.GetFieldColorSet(fieldType, SkinnedLightLevel.Normal);
			}

			// field
			Color backColor = enabled ? skin.FieldBackColor : skin.FieldDisabledBackColor;
			if (style == ComboBoxStyle.DropDownList)
			{
				backColor = colorSet.GetColor(state, focused, enabled);
			}
			using (SolidBrush back = new SolidBrush(backColor))
			{
				g.FillRectangle(back, bounds);
			}

			// button background
			if (style != ComboBoxStyle.DropDownList)
			{
				Brush buttonBrush = colorSet.GetBrush(state, focused, enabled);
				g.FillRectangle(buttonBrush, buttonRect);
			}

			Pen pen = colorSet.GetBorderPen(state, focused, enabled);

			// button foreground
			Color foreColor = enabled ? colorSet.ForeColor : colorSet.DisabledForeColor;
			if (style == ComboBoxStyle.DropDownList)
			{
				g.DrawLine(pen, bounds.Right - ButtonWidth, bounds.Y + 1, bounds.Right - ButtonWidth, bounds.Bottom - 2);
			}
			else
			{
				g.DrawLine(pen, bounds.Right - ButtonWidth, bounds.Y, bounds.Right - ButtonWidth, bounds.Bottom);
			}
			using (Pen arrowPen = new Pen(foreColor, 2))
			{
				g.DrawLine(arrowPen, bounds.Right - ButtonWidth / 2 - ArrowRadius, bounds.Y + bounds.Height / 2 - ArrowRadius / 2, bounds.Right - ButtonWidth / 2, bounds.Y + bounds.Height / 2 + ArrowRadius / 2);
				g.DrawLine(arrowPen, bounds.Right - ButtonWidth / 2, bounds.Y + bounds.Height / 2 + ArrowRadius / 2, bounds.Right - ButtonWidth / 2 + ArrowRadius, bounds.Y + bounds.Height / 2 - ArrowRadius / 2);
			}

			// text
			if (style == ComboBoxStyle.DropDownList)
			{
				using (Brush br = new SolidBrush(foreColor))
				{
					if (value != null)
					{
						using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
						{
							g.DrawString(value.ToString(), Skin.TextFont, br, textRect, sf);
						}
					}
				}

			}
			// border
			if (focused || textBoxFocused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, new Rectangle(bounds.X, bounds.Y, bounds.Width - ButtonWidth + 1, bounds.Height));
			}
			g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
		}

		public void SelectAll()
		{
			_textBox.SelectAll();
		}

		protected override void RefreshItem(int index)
		{
			//throw new NotImplementedException();
		}

		protected override void SetItemsCore(IList items)
		{
			//throw new NotImplementedException();
		}

		public class ObjectCollection : IList
		{
			private List<object> _objects = new List<object>();

			private ISkinnedComboBox _owner;

			private bool _bulkAdd;

			private bool _sorted;
			public bool Sorted
			{
				get { return _sorted; }
				set
				{
					if (_sorted != value)
					{
						_sorted = value;
						if (_sorted)
						{
							_owner.PreItemChange();
							_owner.SortItems();
							_owner.PostItemChange();
						}
					}
				}
			}

			public string DisplayMember { get; set; }

			public ObjectCollection(ISkinnedComboBox owner)
			{
				_owner = owner;
			}

			public int Count
			{
				get { return _objects.Count; }
			}

			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			public bool IsFixedSize
			{
				get
				{
					return false;
				}
			}

			public object SyncRoot
			{
				get
				{
					return false;
				}
			}

			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			internal void SortItems()
			{
				_objects.Sort(Sort);
			}

			private int Sort(object o1, object o2)
			{
				string v1 = GetFormattedValue(o1, _owner.DisplayMember);
				string v2 = GetFormattedValue(o2, _owner.DisplayMember);
				return v1.CompareTo(v2);
			}

			public object this[int index]
			{
				get
				{
					if (index < 0 || index >= _objects.Count)
					{
						return null;
					}
					return _objects[index];
				}

				set
				{
					_objects[index] = value;
				}
			}

			public int Add(object value)
			{
				if (!_bulkAdd)
				{
					_owner.PreItemChange();
				}
				_objects.Add(value);
				if (!_bulkAdd)
				{
					if (Sorted)
					{
						_owner.SortItems();
					}
					_owner.PostItemChange();
				}
				return _objects.IndexOf(value);
			}

			public void AddRange(IEnumerable objects)
			{
				_bulkAdd = true;
				_owner.PreItemChange();
				foreach (object o in objects)
				{
					Add(o);
				}
				if (_sorted)
				{
					_owner.SortItems();
				}
				_bulkAdd = false;
				_owner.PostItemChange();
			}

			public bool Contains(object value)
			{
				return _objects.Contains(value);
			}

			public void Clear()
			{
				_owner.PreItemChange();
				_objects.Clear();
				_owner.PostItemChange();
			}

			public int IndexOf(object value)
			{
				return _objects.IndexOf(value);
			}

			public void Insert(int index, object value)
			{
				_owner.PreItemChange();
				_objects.Insert(index, value);
				_owner.PostItemChange();
			}

			public void Remove(object value)
			{
				_owner.PreItemChange();
				_objects.Remove(value);
				_owner.PostItemChange();
			}

			public void RemoveAt(int index)
			{
				_owner.PreItemChange();
				_objects.RemoveAt(index);
				_owner.PostItemChange();
			}

			public void CopyTo(Array array, int index)
			{
				for (int i = 0; i < _objects.Count; i++)
				{
					array.SetValue(_objects[i], index + i);
				}
			}

			public IEnumerator GetEnumerator()
			{
				return _objects.GetEnumerator();
			}
		}
	}

	public interface ISkinnedComboBox
	{
		void PreItemChange();
		void SortItems();
		void PostItemChange();
		string DisplayMember { get; }
	}
}
