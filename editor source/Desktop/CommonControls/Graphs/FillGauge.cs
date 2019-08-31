using System;
using System.Drawing;
using System.Windows.Forms;
using Desktop.Skinning;

namespace Desktop.CommonControls.Graphs
{
	public partial class FillGauge : UserControl, ISkinControl
	{
		private const int AreaPadding = 3;

		public bool ShowPercentage { get; set; }
		public bool ShowLimits { get; set; }

		private Animator _animator = new Animator(0.15f);
		private StringFormat _centered = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		private StringFormat _right = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

		private decimal _min = 0;
		public decimal Minimum
		{
			get { return _min; }
			set { _min = Math.Min(value, _max - 1); _invalidated = true; Invalidate(); }
		}
		private decimal _max = 100;
		public decimal Maximum
		{
			get { return _max; }
			set { _max = Math.Max(value, _min + 1); _invalidated = true; Invalidate(); }
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
					_value = Math.Max(Math.Min(_max, value), _min);
					_animator.Reset();
					_invalidated = true;
					_animator.StartAnimation(AnimationDirection.In);
					Invalidate();
				}
			}
		}

		private bool _invalidated = true;
		private Rectangle _graphBounds;
		private Rectangle _limitsBounds;

		private Font _font = new Font("Arial", 8);
		private SolidBrush _textBrush = new SolidBrush(Color.Black);
		private SolidBrush _invertedTextBrush = new SolidBrush(Color.Black);
		private Pen _outline = new Pen(Color.Black);
		private SolidBrush _fillBrush = new SolidBrush(Color.Green);

		public FillGauge()
		{
			InitializeComponent();
			DoubleBuffered = true;
			Paint += FillGauge_Paint;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			_animator.OnUpdate += _animator_OnUpdate;
		}

		private void _animator_OnUpdate(object sender, float e)
		{
			Invalidate();
		}

		public void OnUpdateSkin(Skin skin)
		{
			_outline.Color = skin.Surface.GetBorderPen(VisualState.Normal, false, true).Color;
			_textBrush.Color = skin.GetForeColor(SkinnedBackgroundType.Background);
			_invertedTextBrush.Color = _textBrush.Color.Invert();
			Invalidate();
		}

		private void RecalculateBounds(Graphics g)
		{
			_invalidated = false;
			if (ShowLimits)
			{
				string label = $"{Maximum}/{Maximum}";
				int width = (int)g.MeasureString(label, _font).Width;
				_graphBounds = new Rectangle(0, 0, Width - width - AreaPadding, Height - 1);
			}
			else
			{
				_graphBounds = new Rectangle(0, 0, Width - 1, Height - 1);
			}
			_limitsBounds = new Rectangle(_graphBounds.Right + AreaPadding, 0, Width - _graphBounds.Right, Height);
		}

		private void FillGauge_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (_invalidated)
			{
				RecalculateBounds(g);
			}
			g.DrawRectangle(_outline, _graphBounds);
			RectangleF clip = g.ClipBounds;

			float current = MathUtils.Lerp((float)_previousValue, (float)_value, _animator.Value);
			float amount = (current - (float)Minimum) / (float)(Maximum - Minimum);

			if (ShowPercentage)
			{
				g.DrawString(Math.Round(amount * 100, 0) + "%", _font, Brushes.Black, _graphBounds, _centered);
			}

			int width = (int)MathUtils.Lerp(0, _graphBounds.Width, amount);
			Rectangle fill = new Rectangle(_graphBounds.Left + 1, _graphBounds.Top + 1, width, _graphBounds.Height - 1);
			g.FillRectangle(_fillBrush, fill);

			if (ShowLimits)
			{
				g.DrawString($"{Value}/{Maximum}", _font, _textBrush, _limitsBounds, _right);
			}
			if (ShowPercentage)
			{
				g.SetClip(fill);
				Brush brush = Brushes.Black;
				if (_fillBrush.Color.GetLuminance() < 0.5f)
				{
					brush = Brushes.White;
				}
				g.DrawString(Math.Round(amount * 100, 0) + "%", _font, brush, _graphBounds, _centered);
				g.SetClip(clip);
			}
		}
	}
}
