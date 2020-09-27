using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Desktop.DataStructures;
using Desktop.Skinning;

namespace Desktop.CommonControls
{
	public partial class LineGraph : UserControl, ISkinControl
	{
		private const int AxesPadding = 3;
		private const int LegendWidth = 10;
		private const int PointSize = 5;

		private Font _font = new Font("Arial", 8);
		private Pen _axesPen = new Pen(Color.Black);
		private Pen _linePen = new Pen(Color.Black, 1);
		private Pen _thresholdPen = new Pen(Color.Black);
		private SolidBrush _pointBrush = new SolidBrush(Color.Black);
		private SolidBrush _textBrush = new SolidBrush(Color.Black);
		private StringFormat _rightAlign = new StringFormat() { Alignment = StringAlignment.Far };

		private bool _invalidated = true;
		private List<DataSeries> _series = new List<DataSeries>();
		private Rectangle _yAxisRect;
		private Rectangle _graphRect;
		private Rectangle _legendRect;
		private int _tickMin;
		private int _tickMax;
		private int _tickSpacing;

		private Dictionary<int, LineTick> _tickMap = new Dictionary<int, LineTick>();
		private List<LineTick> _ticks = new List<LineTick>();
		private Animator _animator = new Animator(0.10f);

		private int _lineHeight;

		private int _maxTicks = 5;
		public int MaxTicks
		{
			get { return _maxTicks; }
			set { _maxTicks = value; InvalidateGraph(); }
		}

		public LineGraph()
		{
			InitializeComponent();
			DoubleBuffered = true;
			Paint += LineGraph_Paint;
			_thresholdPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			_animator.OnUpdate += _animator_OnUpdate;
		}

		private void _animator_OnUpdate(object sender, float e)
		{
			Invalidate();
		}

		public void OnUpdateSkin(Skin skin)
		{
			_axesPen.Color = skin.GetForeColor(SkinnedBackgroundType.Background);
			_textBrush.Color = skin.GetForeColor(SkinnedBackgroundType.Background);

			for (int i = 0; i < _series.Count; i++)
			{
				_series[i].Color = GetAxesColor(i);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			InvalidateGraph();
		}

		private void LineGraph_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			if (_invalidated)
			{
				RecalculateBounds(g);
			}

			//g.DrawRectangle(Pens.White, 0, 0, Width, Height);
			//g.DrawRectangle(_axesPen, _yAxisRect);
			//g.DrawRectangle(_axesPen, _graphRect);
			//g.DrawRectangle(_axesPen, _legendRect);

			//axes
			g.DrawLine(_axesPen, _graphRect.Left, _graphRect.Top, _graphRect.Left, _graphRect.Bottom);
			g.DrawLine(_axesPen, _graphRect.Left, _graphRect.Bottom, _graphRect.Right, _graphRect.Bottom);

			//tick marks
			foreach (LineTick tick in _ticks)
			{
				g.DrawLine(_axesPen, tick.Position.X, tick.Position.Y, tick.Position.X, tick.Position.Y - 3);
				g.DrawString(tick.Label, _font, _textBrush, tick.LabelBounds.Left, tick.LabelBounds.Top);
			}
			int ty = 0;
			if (_tickSpacing > 0)
			{
				for (int t = _tickMin; t <= _tickMax; t += _tickSpacing)
				{
					float m = ((float)t - _tickMin) / (_tickMax - _tickMin);
					int y = (int)MathUtils.Lerp(_graphRect.Bottom, _graphRect.Top, m);
					g.DrawLine(_axesPen, _graphRect.Left - 2, y, _graphRect.Left + 2, y);
					g.DrawString(t.ToString(), _font, _textBrush, _yAxisRect.Right, y - _lineHeight + 3, _rightAlign);
					ty++;
				}
			}

			int legendLeft = _legendRect.Left;

			//data points
			int py = PointSize / 2;
			foreach (DataSeries series in _series)
			{
				_linePen.Color = series.Color;
				_pointBrush.Color = series.Color;
				int lastX = -1;
				int lastY = -1;

				//legend
				int width = LegendWidth + (int)g.MeasureString(series.Label, _font).Width;
				int midY = _legendRect.Top + _legendRect.Height / 2;
				g.DrawLine(_linePen, legendLeft, midY, legendLeft + LegendWidth, midY);
				g.DrawString(series.Label, _font, _textBrush, legendLeft + LegendWidth, _legendRect.Top);
				legendLeft += width + LegendWidth;

				if (series.Threshold > 0)
				{
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
					_thresholdPen.Color = Color.FromArgb(80, series.Color);
					float t = (float)(series.Threshold - _tickMin) / (_tickMax - _tickMin);
					int y = (int)MathUtils.Lerp(_graphRect.Bottom, _graphRect.Top, t);
					g.DrawLine(_thresholdPen, _graphRect.Left + 1, y, _graphRect.Right - 1, y);
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				}

				for (int i = 0; i < series.Points.Count; i++)
				{
					DataPoint pt = series.Points[i];
					LineTick tick;
					if (_tickMap.TryGetValue(pt.X, out tick))
					{
						int x = tick.Position.X;
						int realY = pt.Y;
						int animY = (int)MathUtils.Lerp(_tickMin, realY, _animator.Value);
						float t = (float)(animY - _tickMin) / (_tickMax - _tickMin);
						int y = (int)MathUtils.Lerp(_graphRect.Bottom, _graphRect.Top, t);

						if (i == 0)
						{
							lastX = x;
							lastY = y;
						}
						g.DrawLine(_linePen, lastX, lastY, x, y);
						g.FillEllipse(_pointBrush, x - py, y - py, PointSize, PointSize);

						lastX = x;
						lastY = y;
					}
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
		}

		public void Clear()
		{
			_series.Clear();
			_animator.Reset();
			InvalidateGraph();
		}

		public DataSeries AddSeries(string label)
		{
			DataSeries series = new DataSeries()
			{
				Label = label,
				Color = GetAxesColor(_series.Count)
			};
			_series.Add(series);
			series.PropertyChanged += Series_PropertyChanged;
			_animator.Reset();
			InvalidateGraph();
			return series;
		}

		private void Series_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_animator.Reset();
			InvalidateGraph();
		}

		private void InvalidateGraph()
		{
			_invalidated = true;
			Invalidate();
		}

		public static Color GetAxesColor(int index)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			switch (index % 8)
			{
				case 1:
					return skin.Red;
				case 2:
					return skin.Green;
				case 3:
					return skin.Orange;
				case 4:
					return skin.Purple;
				case 5:
					return skin.Gray;
				case 6:
					return skin.Pink;
				case 7:
					return skin.LightGray;
				default:
					return skin.Blue;
			}
		}

		private void RecalculateBounds(Graphics g)
		{
			_lineHeight = (int)g.MeasureString("X", _font).Height;

			int minY = int.MaxValue;
			int maxY = int.MinValue;
			_tickMap.Clear();
			_ticks.Clear();
			SortedDictionary<int, LineTick> ticks = new SortedDictionary<int, LineTick>();
			for (int i = 0; i < _series.Count; i++)
			{
				DataSeries series = _series[i];

				for (int j = 0; j < series.Points.Count; j++)
				{
					DataPoint pt = series.Points[j];

					minY = Math.Min(minY, pt.Y);
					maxY = Math.Max(maxY, pt.Y);

					LineTick tick;
					if (!ticks.TryGetValue(pt.X, out tick))
					{
						tick = new LineTick(pt.X);
						ticks[pt.X] = tick;
						_tickMap[pt.X] = tick;
					}
					if (string.IsNullOrEmpty(tick.Label))
					{
						tick.Label = pt.Label;
					}
				}

				maxY = Math.Max(maxY, series.Threshold);
			}

			//Y ticks
			minY = Math.Min(minY, maxY);
			minY--; //make sure the bottom value never appears on the axis
			if (minY == maxY || minY < 0)
			{
				minY = 0;
			}
			if (maxY < 5)
			{
				maxY = 5;
			}
			_tickSpacing = MathUtils.GetTickSpacing(minY, maxY, MaxTicks, out _tickMin, out _tickMax);
			int widest = 0;
			if (_tickSpacing > 0)
			{
				for (int t = _tickMin; t <= _tickMax; t += _tickSpacing)
				{
					widest = Math.Max(widest, (int)g.MeasureString(t.ToString(), _font).Width);
				}
			}

			//Graph regions
			_graphRect = new Rectangle(AxesPadding * 4 + widest, AxesPadding + _lineHeight * 2, Width - AxesPadding * 6 - widest, Height - AxesPadding * 2 - _lineHeight * 3);
			_yAxisRect = new Rectangle(AxesPadding, _graphRect.Top, _graphRect.Left - AxesPadding * 2, _graphRect.Height);
			_legendRect = new Rectangle(_graphRect.Left, AxesPadding, _graphRect.Width, _lineHeight);

			//X ticks
			int tickCount = ticks.Count;
			int spacing = tickCount <= 1 ? _graphRect.Width : _graphRect.Width / (tickCount - 1);
			int n = 0;
			foreach (LineTick tick in ticks.Values)
			{
				_ticks.Add(tick);
				tick.Position = new Point(_graphRect.Left + n * spacing, _graphRect.Bottom);
				int width = (int)g.MeasureString(tick.Label, _font).Width;
				tick.LabelBounds = new Rectangle(tick.Position.X - width / 2, tick.Position.Y, width, _lineHeight);
				n++;
			}

			for (int i = 0; i < _series.Count; i++)
			{
				DataSeries series = _series[i];
				series.LegendBounds = new Rectangle(_legendRect.Left + AxesPadding + LegendWidth, _legendRect.Bottom - (_series.Count - i) * _lineHeight - _lineHeight, _legendRect.Width - AxesPadding - LegendWidth, _lineHeight);
			}

			_animator.StartAnimation(AnimationDirection.In);
			_invalidated = false;
		}
	}

	public class DataSeries : BindableObject
	{
		public string Label
		{
			get { return Get<string>(); }
			set { Set(value); }
		}
		public Color Color { get; set; }

		public ObservableCollection<DataPoint> Points
		{
			get { return Get<ObservableCollection<DataPoint>>(); }
			set { Set(value); }
		}

		public Rectangle LegendBounds;
		public Point TickBounds;

		public int Threshold
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public int Index { get; set; }

		public DataSeries()
		{
			Points = new ObservableCollection<DataPoint>();
		}

		public override string ToString()
		{
			return Label;
		}

		public void AddPoint(int x, int y)
		{
			AddPoint(x, y, null);
		}
		public void AddPoint(int x, int y, string label)
		{
			Points.Add(new DataPoint()
			{
				X = x,
				Y = y,
				Label = label
			});
		}
	}

	public class DataPoint
	{
		public int X;
		public int Y;
		public string Label;
	}

	public class LineTick
	{
		public string Label;
		public int XValue;
		public Rectangle LabelBounds;
		public Point Position;

		public LineTick(int value)
		{
			XValue = value;
		}

		public override string ToString()
		{
			return Label;
		}
	}
}
