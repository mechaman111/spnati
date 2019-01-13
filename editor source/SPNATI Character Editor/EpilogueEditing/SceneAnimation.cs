using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SceneAnimation
	{
		public SceneObject AssociatedObject;
		public SceneObject PreviewObject;

		public string Id;
		public List<SceneObject> Frames = new List<SceneObject>();
		public float Duration;
		public Directive Directive;
		public ScenePreview Scene;
		public bool Looped;
		public string EasingMethod;
		public string TweenMethod;
		public string ClampMethod;
		public int Iterations;

		public float Elapsed;
		public float Delay;

		private SceneObject _initialState;

		public SceneAnimation(SceneObject obj, Directive directive, ScenePreview scene, bool fullPlayback)
		{
			_initialState = obj.Copy(); //copy off current values to use as the first keyframe
			_initialState.Index = 0;
			AssociatedObject = obj;
			Looped = directive.Looped;
			if (!string.IsNullOrEmpty(directive.Delay))
			{
				float.TryParse(directive.Delay, NumberStyles.Float, CultureInfo.InvariantCulture, out Delay);
			}
			EasingMethod = directive.EasingMethod;
			TweenMethod = directive.InterpolationMethod;
			ClampMethod = directive.ClampingMethod;
			Iterations = directive.Iterations;
			if (fullPlayback)
			{
				PreviewObject = AssociatedObject;
			}
			else
			{
				Looped = true;
				PreviewObject = _initialState.Copy();
			}
			_initialState.ObjectType = SceneObjectType.Keyframe;
			Id = obj.Id;
			Directive = directive;
			Scene = scene;

			Build(true);
		}

		public bool IsComplete
		{
			get
			{
				float life = Elapsed - Delay;
				if (Looped)
				{
					return Iterations > 0 ? life / Duration >= Iterations : false;
				}
				return life >= Duration;
			}
		}

		public void Rebuild()
		{
			Build(false);
		}

		private void Build(bool firstTime)
		{
			Frames.Clear();
			//current object values serve as the first keyframe
			Frames.Add(_initialState);
			_initialState.Start = 0;
			Duration = 0;

			float totalTime = 0;
			SceneObject lastFrame = _initialState;
			if (Directive.Keyframes.Count == 0)
			{
				SceneObject kf = lastFrame.Copy();
				kf.Index = 0;
				kf.LinkedFrame = Directive;
				kf.ObjectType = SceneObjectType.Keyframe;
				kf.Update(Directive, Scene);
				kf.Start = 0;
				kf.End = kf.Time;
				kf.Tween = TweenMethod;
				Duration = kf.Time;
				_initialState.End = kf.Time;
				Frames.Add(kf);
			}
			else
			{
				foreach (Keyframe frame in Directive.Keyframes)
				{
					SceneObject kf = lastFrame.Copy();
					kf.LinkedFrame = frame;
					kf.Tween = TweenMethod;
					kf.Index = Frames.Count;
					kf.ObjectType = SceneObjectType.Keyframe;
					kf.Update(frame, Scene);
					kf.Start = totalTime;
					totalTime = Math.Max(totalTime, kf.Time);
					kf.End = totalTime;
					Frames.Add(kf);
					lastFrame = kf;
				}
				Duration = totalTime;
			}
		}

		public void Update(float elapsedSec)
		{
			Elapsed += elapsedSec;

			float t = Elapsed - Delay;
			if (t < 0)
			{
				return;
			}
			if (Duration == 0)
			{
				t = 1;
			}
			else
			{
				t /= Duration;
				if (Looped)
				{
					t = Clamp(t);
					if (IsComplete)
					{
						t = 1;
					}
				}
				else
				{
					t = Math.Min(1, t);
				}
				t = Ease(EasingMethod, t);
				t *= Duration;
			}
			for (int i = Frames.Count - 1; i >= 0; i--)
			{
				SceneObject frame = Frames[i];
				if (t >= frame.Start)
				{
					SceneObject last = (i > 0 ? Frames[i - 1] : frame);
					float time = 0;
					if (frame.Start == frame.End)
					{
						time = 1;
					}
					else
					{
						time = t == 0 ? 0 : (t - frame.Start) / (frame.End - frame.Start);
					}
					
					UpdateValues(frame, last, time);
					return;
				}
			}
		}

		private float Clamp(float t)
		{
			switch (ClampMethod)
			{
				case "clamp":
					return t;
				case "mirror":
					t %= 2.0f;
					return t > 1 ? 2 - t : t;
				default:
					return t % 1.0f;
			}
		}

		/// <summary>
		/// Interpolates values between two frames
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="last"></param>
		/// <param name="time"></param>
		private void UpdateValues(SceneObject frame, SceneObject last, float time)
		{
			int index = last.Index;
			SceneObject lastLast = (index > 0 ? Frames[index - 1] : last);
			SceneObject nextNext = index < Frames.Count - 2 ? Frames[index + 1] : frame;
			PreviewObject.X = Interpolate(last.X, frame.X, frame.Tween, time, lastLast.X, nextNext.X);
			PreviewObject.Y = Interpolate(last.Y, frame.Y, frame.Tween, time, lastLast.Y, nextNext.Y);
			PreviewObject.ScaleX = Interpolate(last.ScaleX, frame.ScaleX, frame.Tween, time, lastLast.ScaleX, nextNext.ScaleX);
			PreviewObject.ScaleY = Interpolate(last.ScaleY, frame.ScaleY, frame.Tween, time, lastLast.ScaleY, nextNext.ScaleY);
			PreviewObject.Zoom = Interpolate(last.Zoom, frame.Zoom, frame.Tween, time, lastLast.Zoom, nextNext.Zoom);
			PreviewObject.Rotation = Interpolate(last.Rotation, frame.Rotation, frame.Tween, time, lastLast.Rotation, nextNext.Rotation);
			PreviewObject.Alpha = Interpolate(last.Alpha, frame.Alpha, frame.Tween, time, lastLast.Alpha, nextNext.Alpha);
			PreviewObject.Color.Color = Interpolate(last.Color, frame.Color, frame.Tween, time, lastLast.Color, nextNext.Color);
			PreviewObject.Color.Color = Color.FromArgb((int)(PreviewObject.Alpha / 100 * 255), PreviewObject.Color.Color);
		}

		public void Halt()
		{
			SceneObject frame = Frames[Frames.Count - 1];
			UpdateValues(frame, frame, 1);
		}

		private float Interpolate(float lastValue, float nextValue, string interpolationMode, float t, float lastLastValue, float nextNextValue)
		{
			switch (interpolationMode)
			{
				case "spline":
					float p0 = lastLastValue;
					float p1 = lastValue;
					float p2 = nextValue;
					float p3 = nextNextValue;
					float a = 2 * p1;
					float b = p2 - p0;
					float c = 2 * p0 - 5 * p1 + 4 * p2 - p3;
					float d = -p0 + 3 * p1 - 3 * p2 + p3;
					float p = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
					return p;
				default:
					return (nextValue - lastValue) * t + lastValue;
			}
		}

		private Color Interpolate(SolidBrush lastValue, SolidBrush nextValue, string interpolationMode, float t, SolidBrush lastLastValue, SolidBrush nextNextValue)
		{
			t = Math.Min(1, Math.Max(0, t));

			float r = Interpolate(lastValue.Color.R, nextValue.Color.R, interpolationMode, t, lastLastValue.Color.R, nextNextValue.Color.R);
			float g = Interpolate(lastValue.Color.G, nextValue.Color.G, interpolationMode, t, lastLastValue.Color.G, nextNextValue.Color.G);
			float b = Interpolate(lastValue.Color.B, nextValue.Color.B, interpolationMode, t, lastLastValue.Color.B, nextNextValue.Color.B);
			return Color.FromArgb(lastValue.Color.A, (int)r, (int)g, (int)b);
		}

		public static float Ease(string method, float t)
		{
			switch (method)
			{
				case "linear":
					return t;
				case "ease-in":
					return t * t;
				case "ease-out":
					return t * (2 - t);
				case "elastic":
					return t == 0 ? 0 : (0.04f - 0.04f / t) * (float)Math.Sin(25 * t) + 1;
				case "ease-in-cubic":
					return t * t * t;
				case "ease-out-cubic":
					t--;
					return 1 + t * t * t;
				case "ease-in-sin":
					return 1 + (float)Math.Sin(Math.PI / 2 * t - Math.PI / 2);
				case "ease-out-sin":
					return (float)Math.Sin(Math.PI / 2 * t);
				case "ease-in-out-cubic":
					return t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
				case "bounce":
					if (t < 0.3636f)
					{
						return 7.5625f * t * t;
					}
					else if (t < 0.7273f)
					{
						t -= 0.5455f;
						return 7.5625f * t * t + 0.75f;
					}
					else if (t < 0.9091f)
					{
						t -= 0.8182f;
						return 7.5625f * t * t + 0.9375f;
					}
					else
					{
						t -= 0.9545f;
						return 7.5625f * t * t + 0.984375f;
					}
			}
			return 3 * t * t - 2 * t * t * t;
		}
	}
}
