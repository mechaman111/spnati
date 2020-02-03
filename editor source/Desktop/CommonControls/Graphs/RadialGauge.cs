using System;
using System.Drawing;
using System.Windows.Forms;
using Desktop.Skinning;

namespace Desktop.CommonControls
{
	public partial class RadialGauge : UserControl, ISkinControl
	{
		private const int PenWidth = 10;
		private Pen _pen = new Pen(Brushes.Green, PenWidth);
		private Pen _backPen = new Pen(Brushes.Black, PenWidth);
		private Font _font = new Font("Segoe UI", 30);
		private Font _headerFont = new Font("Segoe UI", 10);
		private Font _tickFont = new Font("Arial", 8);
		private SolidBrush _headerBrush = new SolidBrush(Color.Black);
		private SolidBrush _fontBrush = new SolidBrush(Color.Black);
		private SolidBrush _tickBrush = new SolidBrush(Color.Black);

		private StringFormat _center = new StringFormat() { Alignment = StringAlignment.Center };
		private StringFormat _leftAlign = new StringFormat() { Alignment = StringAlignment.Near };
		private StringFormat _rightAlign = new StringFormat() { Alignment = StringAlignment.Far };

		private Animator _animator = new Animator(0.15f);

		private string _caption;
		public string Caption
		{
			get { return _caption; }
			set { _caption = value; Invalidate(true); }
		}

		private string _unit = "";
		public string Unit
		{
			get { return _unit; }
			set { _unit = value; Invalidate(true); }
		}

		private bool _capacityMode;
		public bool CapacityMode
		{
			get { return _capacityMode; }
			set
			{
				_capacityMode = value;
				if (!value)
				{
					_pen.Color = SkinManager.Instance.CurrentSkin.SecondaryColor.GetColor(VisualState.Normal, false, true);
				}
				Invalidate(true);
			}
		}
		private bool _flippedCapacity;
		public bool InvertCapacityColors
		{
			get { return _flippedCapacity; }
			set
			{
				_flippedCapacity = value;
				Invalidate(true);
			}
		}

		public bool ShowPercentage { get; set; }

		private decimal _min = 0;
		public decimal Minimum
		{
			get { return _min; }
			set { _min = Math.Min(value, _max - 1); Invalidate(true); }
		}
		private decimal _max = 100;
		public decimal Maximum
		{
			get { return _max; }
			set { _max = Math.Max(value, _min + 1); Invalidate(true); }
		}
		private decimal _previousValue = 0;
		private decimal _value = 0;
		public decimal Value
		{
			get { return _value; }
			set
			{
				if (_value != value)
				{
					_previousValue = _value;
					_value = value;
					_animator.Reset();
					_animator.StartAnimation(AnimationDirection.In);
					Invalidate(true);
				}
			}
		}

		private bool _overcapacity;
		public bool HighlightOverCapacity
		{
			get { return _overcapacity; }
			set
			{
				if (_overcapacity != value)
				{
					_overcapacity = value;
					Invalidate();
				}
			}
		}

		public RadialGauge()
		{
			InitializeComponent();
			DoubleBuffered = true;
			_previousValue = _value;
			_animator.OnUpdate += _animator_OnUpdate;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate(true);
		}

		private void _animator_OnUpdate(object sender, float e)
		{
			Invalidate(true);
		}

		private void RadialGauge_Load(object sender, EventArgs e)
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			_pen.Color = skin.SecondaryColor.GetColor(VisualState.Normal, false, true);
			_backPen.Color = skin.EmptyColor;
			_fontBrush.Color = skin.PrimaryForeColor;
			_tickBrush.Color = skin.LabelForeColor;
			_headerBrush.Color = skin.PrimaryForeColor;
		}

		private void RadialGauge_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(SkinManager.Instance.CurrentSkin.Surface.Normal);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			int radius = PenWidth / 2;

			float current = MathUtils.Lerp((float)_previousValue, (float)Math.Min(_max, Math.Max(_min, _value)), _animator.Value);
			float amount = (current - (float)Minimum) / (float)(Maximum - Minimum);

			int captionHeight = 0;
			if (!string.IsNullOrEmpty(Caption))
			{
				captionHeight = (int)g.MeasureString(Caption, _headerFont).Height;
				g.DrawString(Caption, _headerFont, _headerBrush, new Rectangle(0, 0, panel.Width, captionHeight), _center);
			}

			string label = null;
			int unitHeight = 0;
			if (ShowPercentage)
			{
				label = Math.Round(100 * amount) + "%";
			}
			else
			{
				label = Value.ToString();
				if (!string.IsNullOrEmpty(Unit))
				{
					unitHeight = (int)g.MeasureString(Unit, _tickFont).Height;
				}
			}
			int labelHeight = (int)g.MeasureString(label, _tickFont).Height + unitHeight;

			int arcTop = radius + captionHeight;
			int arcSize = Math.Max(1, panel.Width - PenWidth - 1);
			int arcHeight = Math.Max(1, (panel.Height - arcTop - labelHeight) * 2);
			int arcBottom = captionHeight + arcHeight / 2;
			g.DrawArc(_backPen, radius, arcTop, arcSize, arcHeight, 180, 180);

			Skin skin = SkinManager.Instance.CurrentSkin;

			float height = g.MeasureString(Maximum.ToString(), _tickFont).Height;
			Rectangle textRect = new Rectangle(0, arcBottom + radius, panel.Width, (int)height);
			g.DrawString(Minimum.ToString(), _tickFont, _tickBrush, textRect, _leftAlign);
			g.DrawString(Maximum.ToString(), _tickFont, _tickBrush, textRect, _rightAlign);

			if (_capacityMode)
			{
				Color good = _flippedCapacity ? skin.BadForeColor : skin.GoodForeColor;
				Color bad = _flippedCapacity ? skin.GoodForeColor : skin.BadForeColor;
				if (_overcapacity)
				{
					if (_value > Maximum)
					{
						_pen.Color = bad;
					}
					else
					{
						_pen.Color = good;
					}
				}
				else
				{
					if (amount < 0.5f)
					{
						_pen.Color = MathUtils.Lerp(good, skin.CautionForeColor, amount / 0.5f);
					}
					else
					{
						_pen.Color = MathUtils.Lerp(skin.CautionForeColor, bad, (amount - 0.5f) / 0.5f);
					}
				}
			}

			float angle = amount * 180;
			g.DrawArc(_pen, radius, arcTop, arcSize, arcHeight, 180, angle);
			int labelTop = (int)(arcTop + (arcBottom - arcTop) * 0.33f);
			g.DrawString(label, _font, _fontBrush, new Rectangle(0, labelTop, panel.Width, arcBottom - arcTop), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			if (!string.IsNullOrEmpty(Unit) && !ShowPercentage)
			{
				g.DrawString(Unit.ToString(), _tickFont, _fontBrush, new Rectangle(0, labelTop + (arcBottom - arcTop) / 2 + labelHeight - unitHeight, panel.Width, unitHeight), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
		}
	}
}
