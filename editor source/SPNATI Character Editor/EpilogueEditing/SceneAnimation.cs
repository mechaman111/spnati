using System;
using System.Collections.Generic;
using System.Drawing;

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

		public float Elapsed;

		private SceneObject _initialState;

		public SceneAnimation(SceneObject obj, Directive directive, ScenePreview scene, bool fullPlayback)
		{
			_initialState = new SceneObject(obj); //copy off current values to use as the first keyframe
			_initialState.Index = 0;
			AssociatedObject = obj;
			Looped = directive.Looped;
			EasingMethod = directive.EasingMethod;
			TweenMethod = directive.InterpolationMethod;
			if (fullPlayback)
			{
				PreviewObject = AssociatedObject;
			}
			else
			{
				Looped = true;
				PreviewObject = new SceneObject(_initialState);
			}
			_initialState.ObjectType = SceneObjectType.Keyframe;
			Id = obj.Id;
			Directive = directive;
			Scene = scene;

			Build(true);
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
				SceneObject kf = new SceneObject(lastFrame);
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
					SceneObject kf = new SceneObject(lastFrame);
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

		public void Update(float elapsedMs)
		{
			Elapsed += elapsedMs;
			if (Looped)
			{
				Elapsed = Elapsed % Duration;
			}

			float t = Elapsed;
			if (Duration == 0)
			{
				t = 1;
			}
			else
			{
				t = Ease(t / Duration) * Duration;
			}
			for (int i = Frames.Count - 1; i >= 0; i--)
			{
				SceneObject frame = Frames[i];
				if (t >= frame.Start)
				{
					SceneObject last = (i > 0 ? Frames[i - 1] : frame);
					float time = t == 0 ? 0 : Math.Min(1, Math.Max(0, (t - frame.Start) / (frame.End - frame.Start)));
					UpdateValues(frame, last, time);

					return;
				}
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
			t = Math.Min(1, Math.Max(0, t));
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

		private float Ease(float t)
		{
			switch (EasingMethod)
			{
				case "linear":
					return t;
				case "ease-in":
					return t * t;
				case "ease-out":
					return t * (2 - t);
			}
			return 3 * t * t - 2 * t * t * t;
		}
	}
}
