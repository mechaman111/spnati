using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	[DefaultEvent("ValueChanged")]
	public partial class SkinnedSlider : UserControl, ISkinControl, ISupportInitialize
	{
		public event EventHandler ValueChanged;

		private bool _dragging;

		public SkinnedSlider()
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
		}

		private int _minimum = 0;
		public int Minimum
		{
			get { return _minimum; }
			set { _minimum = value; Value = _value; }
		}

		private int _maximum = 100;
		public int Maximum
		{
			get { return _maximum; }
			set { _maximum = value; Value = _value; }
		}

		private int _value = 0;
		public int Value
		{
			get { return _value; }
			set
			{
				value = Math.Max(_minimum, Math.Min(_maximum, value));
				if (_value != value)
				{
					_value = value;
					ValueChanged?.Invoke(this, EventArgs.Empty);
					Invalidate(true);
				}
			}
		}

		public int Increment { get; set; } = 10;

		private int _tickFrequency = 10;
		public int TickFrequency
		{
			get { return _tickFrequency; }
			set
			{
				if (_tickFrequency != value)
				{
					_tickFrequency = value;
					Invalidate(true);
				}
			}
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

		public void OnUpdateSkin(Skin skin)
		{
			_colorSet = null;
		}

		public VisualState MouseState { get; private set; }

		private const int MarkerSize = 10;

		private Rectangle GetTrackRectangle()
		{
			return new Rectangle(ClientRectangle.X + MarkerSize, ClientRectangle.Y, ClientRectangle.Width - MarkerSize * 2, ClientRectangle.Height);
		}

		private Rectangle GetThumbRectangle(int padding)
		{
			Rectangle trackRect = GetTrackRectangle();
			float pct = (Value - Minimum) / (float)(Maximum - Minimum);
			int left = (int)(trackRect.Left + pct * trackRect.Width);
			Rectangle thumbRect = new Rectangle(left - MarkerSize / 2 - padding, trackRect.Y + trackRect.Height / 2 - MarkerSize / 2 - padding, MarkerSize + padding * 2, MarkerSize + padding * 2);
			return thumbRect;
		}

		/// <summary>
		/// Moves the slider thumb to the given point
		/// </summary>
		/// <param name="x"></param>
		private void MoveSlider(int x)
		{
			Rectangle track = GetTrackRectangle();
			float amount = (x - track.X) / (float)track.Width;
			Value = (int)(Minimum + amount * (Maximum - Minimum));
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_dragging)
			{
				MoveSlider(e.X);
			}
			if (GetThumbRectangle(3).Contains(new Point(e.X, e.Y)))
			{
				MouseState = VisualState.Hover;
				Invalidate();
			}
			else if (MouseState == VisualState.Hover)
			{
				MouseState = VisualState.Normal;
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
				Rectangle thumbRect = GetThumbRectangle(3);
				Point mouse = new Point(e.X, e.Y);
				if (thumbRect.Contains(mouse))
				{
					_dragging = true;
				}
				else if (trackRect.Contains(mouse))
				{
					if (e.X < thumbRect.X)
					{
						Value -= Increment;
					}
					else if (e.X > thumbRect.Width)
					{
						Value += Increment;
					}
				}

				Invalidate();
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (e.Delta < 0)
			{
				Value -= Increment;
			}
			else if (e.Delta > 0)
			{
				Value += Increment;
			}
		}

		protected override bool IsInputKey(Keys keyData)
		{
			bool isInput = base.IsInputKey(keyData);
			if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
			{
				return true;
			}
			return isInput;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Down)
			{
				Value -= Increment;
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
			{
				Value += Increment;
				e.Handled = true;
			}
			base.OnKeyDown(e);
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
					g.DrawLine(pen, trackRect.X, trackRect.Y + trackRect.Height / 2, trackRect.Right, trackRect.Y + trackRect.Height / 2);

					//tick marks
					if (_tickFrequency > 0)
					{
						for (int i = Minimum; i <= Maximum; i += _tickFrequency)
						{
							float p = (i - Minimum) / (float)(Maximum - Minimum);
							int tickLeft = (int)(trackRect.Left + p * trackRect.Width);
							g.DrawLine(forePen, tickLeft, trackRect.Bottom - 5, tickLeft, trackRect.Bottom - 1);
						}
					}

					float pct = (Value - Minimum) / (float)(Maximum - Minimum);
					int left = (int)(trackRect.Left + pct * trackRect.Width);

					//filled track
					ColorSet thumbSet = skin.GetWidgetColorSet(FieldType);
					pen.Color = thumbSet.GetColor(VisualState.Normal, Focused, Enabled);
					g.DrawLine(pen, trackRect.X, trackRect.Y + trackRect.Height / 2, left, trackRect.Y + trackRect.Height / 2);

					VisualState state = MouseState;
					Rectangle thumbRect = new Rectangle(left - MarkerSize / 2, trackRect.Y + trackRect.Height / 2 - MarkerSize / 2, MarkerSize, MarkerSize);

					//thumb
					SolidBrush filledBrush = thumbSet.GetBrush(state, Focused, Enabled);
					g.FillEllipse(filledBrush, thumbRect);
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

			if (Focused)
			{
				SkinManager.Instance.DrawFocusRectangle(g, ClientRectangle);
			}
		}

		public void BeginInit()
		{
			
		}

		public void EndInit()
		{
			
		}
	}
}
