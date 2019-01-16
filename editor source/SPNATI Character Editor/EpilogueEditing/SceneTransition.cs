using System;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SceneTransition
	{
		public float Elapsed;
		public float Duration;
		public Scene Transition;
		public string Ease;
		public Action<float> Effect;

		private SceneViewport View1;
		private SceneViewport View2;

		public Color Color;
		public int Alpha;

		public SceneTransition(Scene transition, int width, int height)
		{
			Transition = transition;

			View1 = new SceneViewport("Previous", Color.LightGray, width, height);
			View2 = new SceneViewport("Next", Color.DarkGray, width, height);

			try
			{
				Color = ColorTranslator.FromHtml(transition.BackgroundColor);
			}
			catch
			{
				Color = Color.Black;
			}

			if (string.IsNullOrEmpty(transition.Time) || !float.TryParse(transition.Time, out Duration))
			{
				Duration = 0;
			}
			Ease = transition.EasingMethod;
			switch (transition.Effect)
			{
				case "dissolve":
					Effect = Dissolve;
					break;
				case "fade":
					Effect = Fade;
					break;
				case "wipe-right":
					Effect = WipeRight;
					break;
				case "wipe-left":
					Effect = WipeLeft;
					break;
				case "wipe-up":
					Effect = WipeUp;
					break;
				case "wipe-down":
					Effect = WipeDown;
					break;
				case "slide-right":
					Effect = SlideRight;
					break;
				case "slide-left":
					Effect = SlideLeft;
					break;
				case "slide-up":
					Effect = SlideUp;
					break;
				case "slide-down":
					Effect = SlideDown;
					break;
				case "push-right":
					Effect = PushRight;
					break;
				case "push-left":
					Effect = PushLeft;
					break;
				case "push-up":
					Effect = PushUp;
					break;
				case "push-down":
					Effect = PushDown;
					break;
				case "uncover-right":
					Effect = UncoverRight;
					break;
				case "uncover-left":
					Effect = UncoverLeft;
					break;
				case "uncover-up":
					Effect = UncoverUp;
					break;
				case "uncover-down":
					Effect = UncoverDown;
					break;
				case "barn-open-horizontal":
					Effect = OpenBarnDoorHorizontal;
					break;
				case "barn-close-horizontal":
					Effect = CloseBarnDoorHorizontal;
					break;
				case "barn-open-vertical":
					Effect = OpenBarnDoorVertical;
					break;
				case "barn-close-vertical":
					Effect = CloseBarnDoorVertical;
					break;
				case "fly-through":
					Effect = FlyThrough;
					break;
				case "spin":
					Effect = Spin;
					break;
				default:
					Effect = Cut;
					break;
			}
		}

		public void Update(float elapsedSec)
		{
			Elapsed += elapsedSec;
			float t = 1;
			if (Duration > 0)
			{
				t = Math.Min(1, (Elapsed % Duration) / Duration);
				t = SceneAnimation.Ease(Ease, t);
			}
			Effect?.Invoke(t);
		}

		public void Draw(Graphics g, int width, int height)
		{
			using (SolidBrush fade = new SolidBrush(Color.FromArgb(Alpha, Color)))
			{
				using (Font font = new Font("Arial", 100))
				{
					using (SolidBrush brush = new SolidBrush(Color.Black))
					{
						using (Pen pen = new Pen(Color.Black, 10))
						{
							if (View1.OnTop)
							{
								DrawView(g, View2, width, height, font, brush, pen);
								DrawView(g, View1, width, height, font, brush, pen);
							}
							else
							{
								DrawView(g, View1, width, height, font, brush, pen);
								DrawView(g, View2, width, height, font, brush, pen);
							}

							g.FillRectangle(fade, 0, 0, width, height);
						}
					}
				}
			}
		}

		private void DrawView(Graphics g, SceneViewport view, int width, int height, Font font, SolidBrush brush, Pen pen)
		{
			g.ResetTransform();
			g.TranslateTransform(width / 2.0f, height / 2.0f);
			g.ScaleTransform(view.Scale, view.Scale);
			g.RotateTransform(view.Rotation);
			g.TranslateTransform(-width / 2.0f, -height / 2.0f);
			g.Clip = new Region(new RectangleF(view.CropLeft, view.CropTop, width - view.CropRight - view.CropLeft, height - view.CropBottom - view.CropTop));
			g.FillRectangle(view.Color, view.X, view.Y, width, height);
			brush.Color = pen.Color = Color.FromArgb(view.Color.Color.A, brush.Color);
			g.DrawString(view.Label, font, brush, new RectangleF(view.X, view.Y, width, height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			g.DrawRectangle(pen, view.X, view.Y, width, height);
		}

		public void Dissolve(float t)
		{
			View1.SetAlpha((int)((1 - t) * 255));
			View2.SetAlpha((int)(t * 255));
		}

		public void Cut(float t)
		{
			View1.SetAlpha(0);
			View2.SetAlpha(255);
		}

		public void Fade(float t)
		{
			float alpha = t <= 0.5f ? t * 2 : (1 - (t - 0.5f) * 2);
			Alpha = Math.Max(0, Math.Min(255, (int)(alpha * 255)));
			View1.SetAlpha(t < 0.5f ? 255 : 0);
			View2.SetAlpha(t < 0.5f ? 0 : 255);
		}

		public void SlideRight(float t)
		{
			int left = (int)Math.Ceiling(View2.Width * (1 - t));
			View2.X = -left;
		}

		public void SlideLeft(float t)
		{
			int left = (int)Math.Ceiling(View2.Width * (1 - t));
			View2.X = left;
		}

		public void SlideUp(float t)
		{
			int top = (int)Math.Ceiling(View2.Height * (1 - t));
			View2.Y = top;
		}

		public void SlideDown(float t)
		{
			int top = (int)Math.Ceiling(View2.Height * (1 - t));
			View2.Y = -top;
		}

		public void WipeRight(float t)
		{
			View2.CropRight = (int)((1 - t) * View2.Width);
		}

		public void WipeLeft(float t)
		{
			View2.CropLeft = (int)((1 - t) * View2.Width);
		}

		public void WipeUp(float t)
		{
			View2.CropTop = (int)((1 - t) * View2.Height);
		}

		public void WipeDown(float t)
		{
			View2.CropBottom = (int)((1 - t) * View2.Height);
		}

		public void PushRight(float t)
		{
			View1.X = (int)(View1.Width * t);
			View2.X = -(int)(View2.Width * (1 - t));
		}

		public void PushLeft(float t)
		{
			View1.X = -(int)(View1.Width * t);
			View2.X = (int)(View2.Width * (1 - t));
		}

		public void PushUp(float t)
		{
			View1.Y = -(int)(View1.Height * t);
			View2.Y = (int)(View2.Height * (1 - t));
		}

		public void PushDown(float t)
		{
			View1.Y = (int)(View1.Height * t);
			View2.Y = -(int)(View2.Height * (1 - t));
		}

		public void UncoverRight(float t)
		{
			View1.OnTop = true;
			View1.X = (int)(t * View1.Width);
		}

		public void UncoverLeft(float t)
		{
			View1.OnTop = true;
			View1.X = -(int)(View1.Width * t);
		}

		public void UncoverUp(float t)
		{
			View1.OnTop = true;
			View1.Y = -(int)(t * View2.Height);
		}

		public void UncoverDown(float t)
		{
			View1.OnTop = true;
			View1.Y = (int)(t * View2.Height);
		}

		public void OpenBarnDoorHorizontal(float t)
		{
			View2.CropLeft = View2.CropRight =(int)(View2.Width / 2  * (1 - t));
		}

		public void CloseBarnDoorHorizontal(float t)
		{
			View1.CropLeft = View1.CropRight = (int)(View1.Width / 2 * t);
			View1.OnTop = true;
		}

		public void OpenBarnDoorVertical(float t)
		{
			View2.CropTop = View2.CropBottom = (int)(View2.Height / 2 * (1 - t));
		}

		public void CloseBarnDoorVertical(float t)
		{
			View1.CropTop = View1.CropBottom = (int)(View1.Height / 2 * t);
			View1.OnTop = true;
		}

		public void FlyThrough(float t)
		{
			float zoom = MathUtil.Lerp(1.0f, 2.0f, t);
			View1.Scale = zoom;
			View1.SetAlpha((int)((1 - t) * 255));

			zoom = MathUtil.Lerp(0.5f, 1, t);
			View2.Scale = zoom;
			View2.SetAlpha((int)(t * 255));
		}

		public void Spin(float t)
		{
			if (t < 0.5f)
			{
				t *= 2;
				View1.SetAlpha(255);
				View2.SetAlpha(0);
				View1.Rotation = t * 1080;
				View1.Scale = 5 * t + 1;
			}
			else
			{
				t = (1 - (t - 0.5f) * 2);
				View1.SetAlpha(0);
				View2.SetAlpha(255);
				View2.Rotation = t * 1080;
				View2.Scale = 5 * t + 1;
			}
		}
	}

	public class SceneViewport
	{
		public string Label;
		public SolidBrush Color;

		public int X;
		public int Y;
		public int Width;
		public int Height;
		public int CropLeft;
		public int CropRight;
		public int CropTop;
		public int CropBottom;
		public bool OnTop;
		public float Rotation;
		public float Scale = 1;

		public SceneViewport(string label, Color color, int width, int height)
		{
			Label = label;
			Color = new SolidBrush(color);
			Width = width;
			Height = height;
		}

		public void SetAlpha(int alpha)
		{
			Color.Color = System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, alpha)), Color.Color);
		}
	}
}
