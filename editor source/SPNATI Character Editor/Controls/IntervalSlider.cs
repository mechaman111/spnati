using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class IntervalSlider : UserControl, ISkinControl, ISupportInitialize
	{
		public ObservableCollection<int> Splits = new ObservableCollection<int>();
		private Dictionary<int, Rectangle> _splitRects = new Dictionary<int, Rectangle>();

		private Font _font;
		private bool _dragging;
		private int _selectedSplit = -1;
		private int _hoverSplit = -1;

		public IntervalSlider()
		{
			InitializeComponent();
			DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserMouse, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ContainerControl, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			Splits.CollectionChanged += Splits_CollectionChanged;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);

			tsAdd.Click += TsAdd_Click;
			tsRemove.Click += TsRemove_Click;
		}

		private void Splits_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			UpdateRects();
			Invalidate();
		}

		private void UpdateRects()
		{
			_splitRects.Clear();
			Rectangle trackRect = GetTrackRectangle();
			foreach (int split in Splits)
			{
				float pct = (split - 0.5f - Minimum) / (Maximum - Minimum);
				int left = (int)(trackRect.Left + pct * trackRect.Width);
				Rectangle thumbRect = new Rectangle(left - MarkerSize / 2, trackRect.Y + trackRect.Height / 2 - MarkerSize / 2, MarkerSize, MarkerSize);
				_splitRects[split] = thumbRect;
			}
		}

		private int _minimum = 0;
		public int Minimum
		{
			get { return _minimum; }
			set { _minimum = value; }
		}

		private int _maximum = 10;
		public int Maximum
		{
			get { return _maximum; }
			set { _maximum = value; }
		}

		private SkinnedFieldType _fieldType = SkinnedFieldType.Primary;
		public SkinnedFieldType FieldType
		{
			get { return _fieldType; }
			set
			{
				_fieldType = value;
				_colorSet = null;
				Invalidate(true);
			}
		}

		private ColorSet _colorSet;
		private ColorSet _lightColorSet;

		public VisualState MouseState { get; private set; }

		private const int MarkerSize = 10;

		public void OnUpdateSkin(Skin skin)
		{
			_colorSet = null;
			_font = new Font(Skin.TextFont.FontFamily, 6, FontStyle.Regular);
		}

		public void BeginInit()
		{
		}

		public void EndInit()
		{
		}

		/// <summary>
		/// Moves the slider thumb to the given point
		/// </summary>
		/// <param name="x"></param>
		private void MoveSlider(int x)
		{
			if (_selectedSplit == -1) { return; }
			Rectangle track = GetTrackRectangle();
			float amount = (x - track.X) / (float)track.Width;
			int target = (int)(Minimum + amount * (Maximum - Minimum));
			target += 1;
			if (target <= Minimum || target > Maximum) { return; }
			int current = Splits[_selectedSplit];
			for (int i = 0; i < Splits.Count; i++)
			{
				if (i == _selectedSplit) { continue; }
				if (current <= Splits[i] && Splits[i] <= target ||
					target <= Splits[i] && Splits[i] <= current)
				{
					return;
				}
			}
			if (target != current)
			{
				Splits[_selectedSplit] = target;
				UpdateRects();
				Invalidate();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_dragging)
			{
				MoveSlider(e.X);
			}
			int oldSplit = _hoverSplit;
			_hoverSplit = -1;
			foreach (KeyValuePair<int, Rectangle> kvp in _splitRects)
			{
				Rectangle thumbRect = kvp.Value;
				if (thumbRect.Contains(new Point(e.X, e.Y)))
				{
					_hoverSplit = Splits.IndexOf(kvp.Key);
					break;
				}
			}
			if (oldSplit != _hoverSplit)
			{
				Invalidate();
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			MouseState = VisualState.Normal;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				MouseState = VisualState.Pressed;

				Rectangle trackRect = GetTrackRectangle();
				Point mouse = new Point(e.X, e.Y);
				for (int i = 0; i < Splits.Count; i++)
				{
					Rectangle thumbRect = _splitRects[Splits[i]];
					if (thumbRect.Contains(mouse))
					{
						_selectedSplit = i;
						_dragging = true;
						Invalidate();
						return;
					}
				}

				if (_selectedSplit != -1 && trackRect.Contains(mouse))
				{
					int value = Splits[_selectedSplit];
					Rectangle thumbRect = _splitRects[Splits[_selectedSplit]];
					if (e.X < thumbRect.X && !Splits.Contains(value - 1))
					{
						Splits[_selectedSplit]--;
						UpdateRects();
					}
					else if (e.X > thumbRect.Width && !Splits.Contains(value + 1))
					{
						Splits[_selectedSplit]++;
						UpdateRects();
					}
				}
			}
		}
		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			MouseState = VisualState.Hover;
			if (_dragging)
			{
				_dragging = false;
			}
			Invalidate();
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Invalidate();
		}

		private Rectangle GetTrackRectangle()
		{
			return new Rectangle(ClientRectangle.X + MarkerSize, ClientRectangle.Y, ClientRectangle.Width - MarkerSize * 2, ClientRectangle.Height);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			if (_colorSet == null)
			{
				_colorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Normal);
				_lightColorSet = skin.GetFieldColorSet(FieldType, SkinnedLightLevel.Light);
			}

			Graphics g = e.Graphics;

			Color backColor = DesignMode ? SystemColors.Control : this.GetSkinnedPanelBackColor();
			g.Clear(backColor);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			//trackbar
			Rectangle trackRect = new Rectangle(ClientRectangle.X + MarkerSize, ClientRectangle.Y, ClientRectangle.Width - MarkerSize * 2, ClientRectangle.Height);

			using (Pen pen = new Pen(Enabled ? _lightColorSet.Normal : _lightColorSet.Disabled, 2))
			{
				using (Pen forePen = new Pen(DesignMode ? ForeColor : this.GetSkinnedPanelForeColor()))
				{
					using (Brush foreBrush = new SolidBrush(forePen.Color))
					{
						g.DrawLine(pen, trackRect.X, trackRect.Y + trackRect.Height / 2, trackRect.Right, trackRect.Y + trackRect.Height / 2);

						//tick marks
						for (int i = Minimum; i <= Maximum; i++)
						{
							float p = (i - Minimum) / (float)(Maximum - Minimum);
							int tickLeft = (int)(trackRect.Left + p * trackRect.Width);
							g.DrawLine(forePen, tickLeft, trackRect.Bottom - 5, tickLeft, trackRect.Bottom - 1);
							if (i == Minimum || i == Maximum || Splits.Contains(i))
							{
								g.DrawString(i.ToString(), _font, foreBrush, tickLeft - 3, trackRect.Top + 1);
							}
						}
					}

					ColorSet thumbSet = skin.GetWidgetColorSet(FieldType);

					for (int i = 0; i < Splits.Count; i++)
					{
						Rectangle thumbRect;
						if (_splitRects.TryGetValue(Splits[i], out thumbRect))
						{
							SolidBrush filledBrush = _lightColorSet.GetBrush(VisualState.Normal, false, Enabled);
							if (_hoverSplit == i)
							{
								filledBrush = thumbSet.GetBrush(VisualState.Hover, Focused, Enabled);
							}
							else if (_selectedSplit == i)
							{
								filledBrush = thumbSet.GetBrush(VisualState.Focused, true, Enabled);
							}
							//thumb
							g.FillEllipse(filledBrush, thumbRect);
						}
					}
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

			if (Focused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, ClientRectangle);
			}
		}

		private void mnuOptions_Opening(object sender, CancelEventArgs e)
		{
			int splitPt = GetValueUnderCursor();
			if (splitPt == -1)
			{
				e.Cancel = true;
				return;
			}

			if (_splitRects.ContainsKey(splitPt))
			{
				tsRemove.Tag = splitPt;
				tsAdd.Visible = false;
				tsRemove.Visible = true;
				return;
			}
			tsAdd.Tag = splitPt;
			tsAdd.Visible = true;
			tsRemove.Visible = false;
		}

		private int GetValueUnderCursor()
		{
			Point pt = PointToClient(MousePosition);
			Rectangle trackRect = new Rectangle(ClientRectangle.X + MarkerSize, ClientRectangle.Y, ClientRectangle.Width - MarkerSize * 2, ClientRectangle.Height);

			if (!trackRect.Contains(pt))
			{
				return -1;
			}

			float pct = (pt.X - trackRect.Left) / (float)trackRect.Width;
			return (int)(pct * (Maximum - Minimum)) + Minimum + 1;
		}

		private void TsRemove_Click(object sender, EventArgs e)
		{
			int split = (int)(sender as ToolStripMenuItem).Tag;
			if (_splitRects.ContainsKey(split))
			{
				Splits.Remove(split);
				_selectedSplit = -1;
			}
		}

		private void TsAdd_Click(object sender, EventArgs e)
		{
			int split = (int)(sender as ToolStripMenuItem).Tag;
			for (int i = 0; i < Splits.Count; i++)
			{
				if(split < Splits[i])
				{
					Splits.Insert(i, split);
					_selectedSplit = i;
					return;
				}
			}
			Splits.Add(split);
			_selectedSplit = Splits.Count - 1;
		}
	}
}
