using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desktop.Skinning;

namespace Desktop.CommonControls.Graphs
{
	public partial class StackedBarGraph : UserControl, ISkinControl
	{
		private const int AxesPadding = 3;
		private const int LegendWidth = 10;

		public bool HorizontalOrientation { get; set; }

		private Animator _animator = new Animator(0.10f);

		private StringFormat _centered = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		private StringFormat _leftAlign = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

		private Pen _axesPen = new Pen(Color.Black);
		private SolidBrush _textBrush = new SolidBrush(Color.Black);
		private SolidBrush _barTextBrush = new SolidBrush(Color.Black);
		private SolidBrush _barBrush = new SolidBrush(Color.Blue);
		private Font _font = new Font("Arial", 8);

		private bool _invalidated = true;
		private int _lineHeight;
		private List<DataSeries> _series = new List<DataSeries>();
		private int _maxValueY;
		private int _maxValueX;

		private Rectangle _graphRect;
		private Rectangle _legendRect;
		private int _labelWidth;

		public bool ShowLegend { get; set; }
		public bool ShowTotals { get; set; }

		public StackedBarGraph()
		{
			InitializeComponent();
			DoubleBuffered = true;
			Paint += BarGraph_Paint;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			_animator.OnUpdate += _animator_OnUpdate;
		}

		public void OnUpdateSkin(Skin skin)
		{
			_axesPen.Color = skin.GetForeColor(SkinnedBackgroundType.Background);
			_textBrush.Color = skin.GetForeColor(SkinnedBackgroundType.Background);

			for (int i = 0; i < _series.Count; i++)
			{
				int index = _series[i].Index;
				_series[i].Color = LineGraph.GetAxesColor(index > 0 ? index : i);
			}
		}

		private void _animator_OnUpdate(object sender, float e)
		{
			Invalidate();
		}

		public void Clear()
		{
			_series.Clear();
			InvalidateGraph();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			InvalidateGraph();
		}

		private void InvalidateGraph()
		{
			_invalidated = true;
			Invalidate();
		}

		public DataSeries AddSeries(string label, int index = 0)
		{
			DataSeries series = new DataSeries()
			{
				Label = label,
				Index = index,
				Color = LineGraph.GetAxesColor(index > 0 ? index : _series.Count),
			};
			_series.Add(series);
			series.PropertyChanged += Series_PropertyChanged;
			InvalidateGraph();
			_animator.Reset();
			return series;
		}

		private void Series_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			InvalidateGraph();
			_animator.Reset();
		}

		private void BarGraph_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (_invalidated)
			{
				RecalculateBounds(g);
			}

			if (HorizontalOrientation)
			{
				PaintHorizontal(g);
			}
			else
			{
				PaintVertical(g);
			}
		}

		private void RecalculateBounds(Graphics g)
		{
			_lineHeight = (int)g.MeasureString("X", _font).Height;

			Dictionary<int, int> bars = new Dictionary<int, int>();
			int maxY = 0;
			int maxX = 0;
			int labelWidth = 0;
			int valueWidth = 0;
			foreach (DataSeries series in _series)
			{
				foreach (DataPoint pt in series.Points)
				{
					int current = 0;
					bars.TryGetValue(pt.X, out current);

					maxX = Math.Max(maxX, pt.X);
					bars[pt.X] = current + pt.Y;
					maxY = Math.Max(maxY, bars[pt.X]);

					if (!string.IsNullOrEmpty(pt.Label) && HorizontalOrientation)
					{
						labelWidth = Math.Max(labelWidth, (int)g.MeasureString(pt.Label, _font).Width);
						valueWidth = Math.Max(valueWidth, (int)g.MeasureString(pt.Y.ToString(), _font).Width);
					}
				}
			}
			_labelWidth = labelWidth + AxesPadding;
			_maxValueX = maxX;
			_maxValueY = maxY;

			int legendHeight = 0;
			if (ShowLegend && _series.Count > 1)
			{
				legendHeight = _lineHeight;
			}
			_legendRect = new Rectangle(AxesPadding, 0, Width - AxesPadding * 2, legendHeight);
			if (HorizontalOrientation)
			{
				_graphRect = new Rectangle(AxesPadding * 2 + _labelWidth, legendHeight, Width - AxesPadding * 3 - _labelWidth - valueWidth, Height - _lineHeight - legendHeight);
			}
			else
			{
				_graphRect = new Rectangle(AxesPadding, _lineHeight + legendHeight, Width - AxesPadding * 2, Height - _lineHeight * 2 - legendHeight);
			}

			_animator.StartAnimation(AnimationDirection.In);
			_invalidated = false;
		}

		private void PaintHorizontal(Graphics g)
		{
			int lineLeft = AxesPadding * 2 + _labelWidth - 1;
			g.DrawLine(_axesPen, lineLeft, _graphRect.Top, lineLeft, _graphRect.Bottom);

			int legendLeft = _legendRect.Left;

			Dictionary<int, int> bars = new Dictionary<int, int>();
			Dictionary<int, string> labels = new Dictionary<int, string>();
			int ySpacing = _graphRect.Height / (_maxValueX + 2);
			int barWidth = 2 * ySpacing / 3;

			int yStart = _graphRect.Top;
			for (int n = 0; n < _series.Count; n++)
			{
				DataSeries series = _series[n];

				_barBrush.Color = series.Color;

				//legend
				if (ShowLegend && _series.Count > 1)
				{
					int width = LegendWidth + (int)g.MeasureString(series.Label, _font).Width;
					g.FillRectangle(_barBrush, legendLeft, _legendRect.Top + 1, LegendWidth, _lineHeight - 2);
					g.DrawString(series.Label, _font, _textBrush, legendLeft + LegendWidth, _legendRect.Top);
					legendLeft += width + LegendWidth;
				}

				//bars
				foreach (DataPoint pt in series.Points)
				{
					int currentY = 0;
					bars.TryGetValue(pt.X, out currentY);

					int x = pt.Y;
					int y = yStart + (pt.X + 1) * ySpacing;

					int current = (int)MathUtils.Lerp(0, x, _animator.Value);
					bars[pt.X] = current + currentY;

					float amount = current / (float)_maxValueY;
					int width = (int)(_graphRect.Width * amount);

					g.FillRectangle(_barBrush, _graphRect.Left, y - barWidth / 2, width, barWidth);

					if (!labels.ContainsKey(pt.X) && !string.IsNullOrEmpty(pt.Label))
					{
						labels[pt.X] = pt.Label;
						g.DrawString(pt.Label, _font, _textBrush, new Rectangle(AxesPadding, y - barWidth / 2, _labelWidth, barWidth), _leftAlign);
					}

					if (ShowTotals)
					{
						g.DrawString(pt.Y.ToString(), _font, _textBrush, _graphRect.Left + width, y - _lineHeight / 2);
					}
				}
			}
		}

		private void PaintVertical(Graphics g)
		{
			//axes
			int axesY = Height - _lineHeight;
			g.DrawLine(_axesPen, AxesPadding, axesY, Width - AxesPadding, axesY);

			if (_series.Count == 0) { return; }

			int legendLeft = _legendRect.Left;

			Dictionary<int, int> bars = new Dictionary<int, int>();
			Dictionary<int, string> labels = new Dictionary<int, string>();
			int xSpacing = _graphRect.Width / (_maxValueX + 2);
			int barWidth = 2 * xSpacing / 3;
			int xStart = _graphRect.Left;
			for (int n = 0; n < _series.Count; n++)
			{
				DataSeries series = _series[n];

				_barBrush.Color = series.Color;

				//legend
				if (ShowLegend && _series.Count > 1)
				{
					int width = LegendWidth + (int)g.MeasureString(series.Label, _font).Width;
					g.FillRectangle(_barBrush, legendLeft, _legendRect.Top + 1, LegendWidth, _lineHeight - 2);
					g.DrawString(series.Label, _font, _textBrush, legendLeft + LegendWidth, _legendRect.Top);
					legendLeft += width + LegendWidth;
				}

				//bars
				foreach (DataPoint pt in series.Points)
				{
					int currentY = 0;
					bars.TryGetValue(pt.X, out currentY);

					int x = xStart + (pt.X + 1) * xSpacing;
					int y = pt.Y;

					int current = (int)MathUtils.Lerp(0, y, _animator.Value);
					int previous = currentY;
					bars[pt.X] = current + currentY;

					float previousAmount = previous / (float)_maxValueY;
					int previousHeight = (int)(_graphRect.Height * previousAmount);
					int previousTop = _graphRect.Bottom - previousHeight;

					float amount = current / (float)_maxValueY;
					int height = (int)(_graphRect.Height * amount);
					int top = previousTop - height;

					g.FillRectangle(_barBrush, x - barWidth / 2, top, barWidth, height);

					//Bar label
					if (height >= _lineHeight && (!ShowTotals || _series.Count > 1))
					{
						Color textColor = _barBrush.Color.GetLuminance() > 0.5f ? Color.Black : Color.White;
						_barTextBrush.Color = textColor;
						g.DrawString(current.ToString(), _font, _barTextBrush, new Rectangle(x - barWidth / 2, top, barWidth, height), _centered);
					}

					if (!labels.ContainsKey(pt.X) && !string.IsNullOrEmpty(pt.Label))
					{
						labels[pt.X] = pt.Label;
						int width = (int)g.MeasureString(pt.Label, _font).Width;
						g.DrawString(pt.Label, _font, _textBrush, x - width / 2, _graphRect.Bottom);
					}
				}
			}

			if (ShowTotals)
			{
				foreach (KeyValuePair<int, int> bar in bars)
				{
					int x = xStart + (bar.Key + 1) * xSpacing;
					int current = bar.Value;
					float amount = current / (float)_maxValueY;
					int height = (int)(_graphRect.Height * amount);
					int top = _graphRect.Bottom - height - _lineHeight;

					string text = current.ToString();
					int width = (int)g.MeasureString(text, _font).Width;
					g.DrawString(text, _font, _textBrush, x - width / 2, top);
				}
			}
		}
	}
}