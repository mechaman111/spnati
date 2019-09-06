using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class Animator
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public event EventHandler<float> OnUpdate;

		private readonly Timer _timer = new Timer { Interval = 5, Enabled = false };

		private float _progress;
		public float Value;
		public float Increment = 0.05f;

		private AnimationDirection _direction;

		public Animator(float increment)
		{
			Increment = increment;
			_timer.Tick += _timer_Tick;
		}

		public void Reset()
		{
			_timer.Stop();
			_progress = 0;
			Value = 0;
		}

		public void StartAnimation(AnimationDirection direction)
		{
			_direction = direction;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			_progress += (_direction == AnimationDirection.In ? 1 : -1) * Increment;
			_progress = Math.Min(1, Math.Max(0, _progress));
			Value = _progress;

			OnUpdate?.Invoke(this, Value);
			if (_progress == 0 && _direction == AnimationDirection.Out || _progress == 1 && _direction == AnimationDirection.In)
			{
				_timer.Stop();
			}
		}
	}

	public enum AnimationDirection
	{
		In,
		Out
	}
}
