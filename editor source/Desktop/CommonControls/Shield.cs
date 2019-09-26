using Desktop.Skinning;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop
{
	public partial class Shield : UserControl, ISkinControl 
	{
		private SolidBrush _background = new SolidBrush(Color.Black);
		private SolidBrush _foreground = new SolidBrush(Color.Black);

		private const int SpinnerSize = 20;
		private const int DotSize = 10;

		private Timer _timer;
		private float _time;

		public Shield()
		{
			InitializeComponent();
			DoubleBuffered = true;
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);

			_timer = new Timer();
			_timer.Interval = 16;
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			_time += _timer.Interval;
			Invalidate();
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle = cp.ExStyle | 0x20;
				return cp;
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			_background.Color = Color.FromArgb(127, SkinManager.Instance.CurrentSkin.Surface.Normal);
			_foreground.Color = SkinManager.Instance.CurrentSkin.PrimaryColor.Normal;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int boxSize = SpinnerSize * 2 + DotSize;
			g.FillRectangle(_background, Width / 2 - boxSize / 2, Height / 2 - boxSize / 2, boxSize, boxSize);

			int timeOffset = (int)(360.0f * (_time % 1000.0f) / 1000.0f);

			for (int angle = 0; angle <= 360; angle += 45)
			{
				Point pt = GetPoint(angle, SpinnerSize);
				int size = (int)((((angle + timeOffset) % 360) / 360.0f) * DotSize);
				g.FillEllipse(_foreground, pt.X - size / 2, pt.Y - size / 2, size, size);
			}
		}

		private Point GetPoint(double angle, double radius)
		{
			double angleRad = (Math.PI / 180.0) * (angle - 90);
			double x = radius * Math.Cos(angleRad);
			double y = radius * Math.Sin(angleRad);
			return new Point((int)(Width / 2 + x), (int)(Height / 2 + y));
		}
	}
}
