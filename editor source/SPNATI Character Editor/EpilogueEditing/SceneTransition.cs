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
							g.FillRectangle(View1.Color, View1.X, View1.Y, width, height);
							brush.Color = pen.Color = Color.FromArgb(View1.Color.Color.A, brush.Color);
							g.DrawString(View1.Label, font, brush, new RectangleF(View1.X, View1.Y, width, height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							g.DrawRectangle(pen, View1.X, View1.Y, width, height);
							g.FillRectangle(View2.Color, View2.X, View2.Y, width, height);
							brush.Color = pen.Color = Color.FromArgb(View2.Color.Color.A, brush.Color);
							g.DrawString(View2.Label, font, brush, new RectangleF(View2.X, View2.Y, width, height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
							g.DrawRectangle(pen, View2.X, View2.Y, width, height);
							g.FillRectangle(fade, 0, 0, width, height);
						}
					}
				}
			}
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

		public void WipeRight(float t)
		{
			int left = (int)Math.Ceiling(View2.Width * (1 - t));
			View2.X = -left;
		}

		public void WipeLeft(float t)
		{
			int left = (int)Math.Ceiling(View2.Width * (1 - t));
			View2.X = left;
		}

		public void WipeUp(float t)
		{
			int top = (int)Math.Ceiling(View2.Height * (1 - t));
			View2.Y = top;
		}

		public void WipeDown(float t)
		{
			int top = (int)Math.Ceiling(View2.Height * (1 - t));
			View2.Y = -top;
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
