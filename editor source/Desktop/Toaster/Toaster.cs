using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Desktop
{
	internal class Toaster : IDisposable
	{
		private Timer _timer;
		private List<Toast> _toasts = new List<Toast>();

		public const float MoveTime = 500;
		public const float DisplayTime = 5000;

		private const int ToastMargin = 10;

		private DateTime _lastTick;

		public Toaster()
		{
			_timer = new Timer();
			_timer.Interval = 16;
			_timer.Tick += _timer_Tick;
		}

		public void ShowToast(Toast toast)
		{
			Shell shell = Shell.Instance;
			if (shell == null) { return; }

			ToastControl ctl = new ToastControl(toast);
			toast.Control.Dismiss += Control_Dismiss;
			shell.Controls.Add(ctl);
			ctl.Location = new System.Drawing.Point(shell.Width - ToastMargin - ctl.Width, shell.Height);
			ctl.BringToFront();

			//figure out top position
			int y = shell.Height - ToastMargin - ctl.Height;
			if (_toasts.Count > 0)
			{
				int highest = _toasts.Min(t => t.TargetLocation.Y);
				y = highest - ctl.Height - ToastMargin;
			}

			_toasts.Add(toast);

			toast.TargetLocation = new System.Drawing.Point(shell.Width - ToastMargin - ctl.Width, y);

			if (_toasts.Count == 1)
			{
				_lastTick = DateTime.Now;
				_timer.Start();
			}
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			Shell shell = Shell.Instance;
			if (shell == null) { return; }

			DateTime now = DateTime.Now;
			TimeSpan elapsed = now - _lastTick;
			float elapsedMs = (float)elapsed.TotalMilliseconds;
			_lastTick = now;

			for (int i = _toasts.Count - 1; i >= 0; i--)
			{
				Toast toast = _toasts[i];
				toast.ElapsedTime += elapsedMs;
				switch (toast.State)
				{
					case ToastState.Entering:
						UpdateToastPosition(toast, toast.ElapsedTime / MoveTime);
						if (toast.ElapsedTime >= MoveTime)
						{
							UpdateToastPosition(toast, 1);
							toast.State = ToastState.Showing;
							toast.ElapsedTime -= MoveTime;
						}
						break;
					case ToastState.Showing:
						UpdateToastPosition(toast, 1);
						if (toast.ElapsedTime >= DisplayTime)
						{
							toast.State = ToastState.Hiding;
							toast.ElapsedTime -= DisplayTime;
						}
						break;
					case ToastState.Hiding:
						UpdateToastPosition(toast, 1 - toast.ElapsedTime / MoveTime);
						if (toast.ElapsedTime >= MoveTime)
						{
							DestroyToast(toast);
						}
						break;
				}
			}

			if (_toasts.Count == 0)
			{
				_timer.Stop();
			}
		}

		private void DestroyToast(Toast toast)
		{
			_toasts.Remove(toast);
			toast.Control.Dismiss -= Control_Dismiss;
			Shell.Instance.Controls.Remove(toast.Control);
			toast.Control.Toast = null;
			toast.Control.Dispose();
			toast.Control = null;
		}

		private void Control_Dismiss(object sender, EventArgs e)
		{
			ToastControl ctl = sender as ToastControl;
			if (ctl.Toast.State == ToastState.Hiding)
			{
				return;
			}
			if (ctl.Toast.State == ToastState.Entering)
			{
				ctl.Toast.ElapsedTime = MoveTime - ctl.Toast.ElapsedTime;
			}
			else
			{
				ctl.Toast.ElapsedTime = 0;
			}
			ctl.Toast.State = ToastState.Hiding;
		}

		private void UpdateToastPosition(Toast toast, float t)
		{
			Shell shell = Shell.Instance;
			if (shell == null) { return; }

			t = Math.Max(0, Math.Min(1, t));
			float lerpX = MathUtils.Lerp(shell.Width, shell.Width - toast.Control.Width - ToastMargin, t);
			toast.Control.Location = new System.Drawing.Point((int)lerpX, toast.TargetLocation.Y);
		}

		public void Dispose()
		{
			_timer.Dispose();
		}
	}
}
