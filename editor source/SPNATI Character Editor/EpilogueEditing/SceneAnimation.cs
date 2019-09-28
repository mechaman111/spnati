using System;
using System.Collections.Generic;

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

		private SceneObject _initialState;

		public SceneAnimation(SceneObject obj, Directive directive, ScenePreview scene, bool fullPlayback)
		{
			_initialState = obj.Copy(); //copy off current values to use as the first keyframe
			_initialState.Index = 0;
			AssociatedObject = obj;
			Looped = directive.Looped;
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
			_initialState.SourceObject = obj;
			Id = obj.Id ?? directive.DirectiveType;
			Directive = directive;
			Scene = scene;

			Build();
		}

		public bool IsComplete
		{
			get
			{
				float life = Elapsed;
				if (Looped)
				{
					return Iterations > 0 ? life / Duration >= Iterations : false;
				}
				return life >= Duration;
			}
		}

		public void Rebuild()
		{
			Build();
		}

		private void Build()
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
				kf.SourceObject = _initialState.SourceObject;
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
					kf.SourceObject = _initialState.SourceObject;
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

			float t = Elapsed;
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
				t = AnimationHelpers.Ease(EasingMethod, t);
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
			PreviewObject.Interpolate(last, frame, time, lastLast, nextNext);
		}

		public void Halt()
		{
			SceneObject frame = Frames[Frames.Count - 1];
			UpdateValues(frame, frame, 1);
		}
	}
}
