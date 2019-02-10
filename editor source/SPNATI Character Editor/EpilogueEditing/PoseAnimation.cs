using System;
using System.Collections.Generic;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class PoseAnimation
	{
		public PosePreview Pose;
		public PoseDirective Directive;
		public SpritePreview Sprite;
		public float Duration;
		public float Delay;
		public float Elapsed;
		public bool Looped;
		public List<KeyframePreview> Frames = new List<KeyframePreview>();

		public PoseAnimation(PosePreview pose, PoseDirective directive)
		{
			Pose = pose;
			Sprite = pose.GetSprite(directive.Id);
			Directive = directive;
			foreach (Keyframe kf in directive.Keyframes)
			{
				KeyframePreview frame = new KeyframePreview(pose, kf);
				Frames.Add(frame);
			}
			Frames.Sort((f1, f2) => {
				return f1.Time.CompareTo(f2.Time);
			});

			float totalTime = 0;
			foreach (KeyframePreview kf in Frames)
			{
				kf.StartTime = totalTime;
				totalTime = kf.Time;
			}

			Duration = totalTime;
			float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out Delay);
			Delay *= 1000;
			Update(0);
		}

		public bool IsComplete
		{
			get
			{
				return Elapsed - Delay >= Duration;
			}
		}

		/// <summary>
		/// Advances the animation a certain amount
		/// </summary>
		/// <param name="elapsedMs"></param>
		public void Update(float elapsedMs)
		{
			Elapsed += elapsedMs;
			if (Directive.Looped && IsComplete)
			{
				Elapsed -= Duration;
			}
			UpdateTo(Elapsed - Delay, Sprite);
		}

		/// <summary>
		/// Updates the animation to a specific point in time
		/// </summary>
		/// <param name="time"></param>
		public void UpdateTo(float time, SpritePreview sprite)
		{
			Elapsed = time + Delay;
			while (Directive.Looped && IsComplete && Duration > 0)
			{
				Elapsed -= Duration;
			}
			float t = time;

			if (Sprite == null) { return; }
			for (int i = Frames.Count - 1; i >= 0; i--)
			{
				KeyframePreview frame = Frames[i];
				if (t <= frame.StartTime)
				{
					continue;
				}

				KeyframePreview lastFrame = (i > 0 ? Frames[i - 1] : frame);
				float progress = (t - frame.StartTime) / (frame.Time - frame.StartTime);
				progress = (t <= 0 ? 0 : Math.Min(1, Math.Max(0, progress)));

				progress = SceneAnimation.Ease(Directive.EasingMethod ?? "linear", progress);
				UpdateSprite(lastFrame, frame, progress, i, sprite);
				return;
			}
		}

		private void UpdateSprite(KeyframePreview fromFrame, KeyframePreview toFrame, float time, int index, SpritePreview sprite)
		{
			if (sprite == null) { return; }
			string interpolation = Directive.InterpolationMethod ?? "none";
			if (Directive.InterpolationMethod == "none" && !string.IsNullOrEmpty(fromFrame.Src))
			{
				sprite.Image = Pose.Images[fromFrame.Src];
			}

			KeyframePreview lastLast = (index > 0 ? Frames[index - 1] : fromFrame);
			KeyframePreview nextNext = index < Frames.Count - 2 ? Frames[index + 1] : toFrame;
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.X))
			{
				sprite.X = SceneObject.Interpolate(fromFrame.X, toFrame.X, interpolation, time, lastLast.X, nextNext.X);
			}
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.Y))
			{
				sprite.Y = SceneObject.Interpolate(fromFrame.Y, toFrame.Y, interpolation, time, lastLast.Y, nextNext.Y);
			}
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.ScaleX))
			{
				sprite.ScaleX = SceneObject.Interpolate(fromFrame.ScaleX, toFrame.ScaleX, interpolation, time, lastLast.ScaleX, nextNext.ScaleX);
			}
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.ScaleY))
			{
				sprite.ScaleY = SceneObject.Interpolate(fromFrame.ScaleY, toFrame.ScaleY, interpolation, time, lastLast.ScaleY, nextNext.ScaleY);
			}
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.Rotation))
			{
				sprite.Rotation = SceneObject.Interpolate(fromFrame.Rotation, toFrame.Rotation, interpolation, time, lastLast.Rotation, nextNext.Rotation);
			}
			if (!string.IsNullOrEmpty(fromFrame.Keyframe.Opacity))
			{
				sprite.Alpha = SceneObject.Interpolate(fromFrame.Alpha, toFrame.Alpha, interpolation, time, lastLast.Alpha, nextNext.Alpha);
			}
		}
	}

	public class KeyframePreview
	{
		public Keyframe Keyframe;
		public float Time;
		public float StartTime;
		public float X;
		public float Y;
		public float ScaleX = 1;
		public float ScaleY = 1;
		public float Alpha = 100;
		public float Rotation = 0;
		public string Src;

		public KeyframePreview(PosePreview pose, Keyframe frame)
		{
			Keyframe = frame;
			Src = frame.Src;
			float.TryParse(frame.X, NumberStyles.Number, CultureInfo.InvariantCulture, out X);
			float.TryParse(frame.Y, NumberStyles.Number, CultureInfo.InvariantCulture, out Y);
			if (!float.TryParse(frame.ScaleX, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleX))
			{
				ScaleX = 1;
			}
			if (!float.TryParse(frame.ScaleY, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleY))
			{
				ScaleY = 1;
			}
			float.TryParse(frame.Rotation, NumberStyles.Number, CultureInfo.InvariantCulture, out Rotation);
			float.TryParse(frame.Opacity, NumberStyles.Number, CultureInfo.InvariantCulture, out Alpha);
			float.TryParse(frame.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out Time);
			Time *= 1000;
		}
	}
}
