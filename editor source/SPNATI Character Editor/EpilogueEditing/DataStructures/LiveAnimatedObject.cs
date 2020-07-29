using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Desktop;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Root class for LiveObjects that can be animated with key frames
	/// </summary>
	public abstract class LiveAnimatedObject : LiveObject, IFixedLength
	{
		public Character Character;
		public bool DisplayPastEnd = true;

		private float _lastPlaybackTime;
		private float _lastElapsedTime;
		protected Dictionary<string, string> _animationIds = new Dictionary<string, string>();

		public float Length
		{
			get
			{
				float max = Get<float>();
				if (Keyframes.Count > 1)
				{
					max = Keyframes[Keyframes.Count - 1].Time;
				}
				if (Events.Count > 0)
				{
					max = Math.Max(max, Events[Events.Count - 1].Time);
				}
				return max;
			}
			set { Set(value); }
		}

		public bool IsPreview;

		public ObservableCollection<string> Properties
		{
			get { return Get<ObservableCollection<string>>(); }
			set { Set(value); }
		}

		public ObservableSet<string> AnimatedProperties
		{
			get { return Get<ObservableSet<string>>(); }
			set { Set(value); }
		}

		public ObservableCollection<LiveKeyframe> Keyframes
		{
			get { return Get<ObservableCollection<LiveKeyframe>>(); }
			set { Set(value); }
		}

		public ObservableCollection<LiveEvent> Events
		{
			get { return Get<ObservableCollection<LiveEvent>>(); }
			set { Set(value); }
		}

		/// <summary>
		/// The last frame containing a property, which might be in another object. Only used during scene creation
		/// </summary>
		public Dictionary<string, KeyframeHistory> PropertyHistory = new Dictionary<string, KeyframeHistory>();

		public override bool IsVisible
		{
			get { return Time >= Start && (IsPreview || DisplayPastEnd || LinkedToEnd || Time <= Start + Length); }
		}

		public event EventHandler<LiveKeyframe> KeyframeChanged;

		public LiveAnimatedObject()
		{
			Properties = new ObservableCollection<string>();
			Keyframes = new ObservableCollection<LiveKeyframe>();
			Events = new ObservableCollection<LiveEvent>();
			AnimatedProperties = new ObservableSet<string>();
		}

		public abstract Directive CreateCreationDirective(Scene scene);

		protected override void OnCopyTo(LiveObject copy)
		{
			LiveAnimatedObject animatedCopy = copy as LiveAnimatedObject;
			if (animatedCopy != null)
			{
				foreach (LiveKeyframe kf in animatedCopy.Keyframes)
				{
					kf.Data = animatedCopy;
					kf.PropertyChanged += Kf_PropertyChanged;
				}
				foreach (LiveEvent evt in animatedCopy.Events)
				{
					evt.Data = animatedCopy;
				}
			}
		}

		public abstract Type GetKeyframeType();
		public virtual Type GetEventType()
		{
			return null;
		}

		public bool AllowsCrossStageImages
		{
			get
			{
				return Data.AllowsCrossStageImages;
			}
		}

		public LiveKeyframe CreateKeyframe(float time)
		{
			LiveKeyframe kf = Activator.CreateInstance(GetKeyframeType()) as LiveKeyframe;
			kf.Time = time;
			return kf;
		}

		/// <summary>
		/// Converts a supported value from a string to its type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializedValue"></param>
		/// <returns></returns>
		private object GetValue<T>(string propName, string serializedValue)
		{
			object val = null;
			Type propType = typeof(T);
			if (propType == typeof(Object))
			{
				propType = PropertyTypeInfo.GetType(this.GetType(), propName);
			}
			if (propType == typeof(string))
			{
				val = serializedValue;
			}
			else if (propType == typeof(float))
			{
				float valFloat;
				float.TryParse(serializedValue, NumberStyles.Number, CultureInfo.InvariantCulture, out valFloat);
				val = valFloat;
			}
			else if (propType == typeof(int))
			{
				int valInt;
				int.TryParse(serializedValue, out valInt);
				val = valInt;
			}
			else if (propType == typeof(Color))
			{
				try
				{
					val = ColorTranslator.FromHtml(serializedValue);
				}
				catch { }
			}
			else
			{
				throw new ArgumentException($"Type {typeof(T).Name} not supported.");
			}
			return val;
		}

		/// <summary>
		/// Adds a property value to a keyframe at the given time
		/// </summary>
		/// <param name="time">Time in seconds from start </param>
		/// <param name="propName"></param>
		/// <param name="serializedValue"></param>
		/// <returns>Keyframe at that point</returns>
		public override void AddValue<T>(float time, string propName, string serializedValue, bool addAnimBreak)
		{
			if (string.IsNullOrEmpty(serializedValue))
			{
				return;
			}

			object value = GetValue<T>(propName, serializedValue);

			AddValue(time, propName, value, addAnimBreak);
		}

		/// <summary>
		/// Adds/replaces a property value to a keyframe at the given time
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="time"></param>
		/// <param name="propName"></param>
		/// <param name="value"></param>
		/// <param name="addAnimBreak"></param>
		/// <returns>The keyframe the value was added to</returns>
		public LiveKeyframe AddValue(float time, string propName, object value, bool addAnimBreak)
		{
			if (!AnimatedProperties.Contains(propName))
			{
				AddAnimatedProperty(propName);
			}
			LiveKeyframe keyframe = Keyframes.Find(k => k.Time == time);
			if (keyframe == null)
			{
				keyframe = AddKeyframe(time);
			}
			else if (keyframe.Time == 0)
			{
				KeyframeType type = keyframe.GetMetadata(propName, false).FrameType;
				if (LinkedFromPrevious && type == KeyframeType.Normal)
				{
					//when changing the first frame, it it's breaking a link from a previous object, then make it a break
					string prevValue = GetPreviousValue(propName, time, true)?.ToString() ?? "0";
					string curValue = keyframe.Get<object>(propName)?.ToString() ?? "0";
					string newValue = value?.ToString() ?? "0";
					if (prevValue.Equals(curValue) && !prevValue.Equals(newValue))
					{
						addAnimBreak = true;
					}					
				}
			}

			if (addAnimBreak)
			{
				bool isSplit = keyframe.HasProperty(propName) && time > 0;
				keyframe.GetMetadata(propName, true).FrameType = isSplit ? KeyframeType.Split : KeyframeType.Begin;
			}

			keyframe.Set(value, propName);
			return keyframe;
		}

		public LiveKeyframe AddKeyframe(float time)
		{
			LiveKeyframe kf = CreateKeyframe(time);
			AddKeyframe(kf);
			return kf;
		}

		public void AddKeyframe(LiveKeyframe kf)
		{
			kf.Data = this;
			kf.PropertyChanged += Kf_PropertyChanged;
			float time = kf.Time;
			bool added = false;
			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe other = Keyframes[i];
				if (other.Time > time)
				{
					Keyframes.Insert(i, kf);
					added = true;
					break;
				}
			}
			if (!added)
			{
				Keyframes.Add(kf);
			}

			foreach (string prop in kf.TrackedProperties)
			{
				if (kf.HasProperty(prop))
				{
					UpdateProperty(prop);
				}
			}
		}

		public void RemoveKeyframe(LiveKeyframe kf)
		{
			kf.PropertyChanged -= Kf_PropertyChanged;
			kf.Data = null;
			Keyframes.Remove(kf);

			foreach (string prop in kf.TrackedProperties)
			{
				if (kf.HasProperty(prop))
				{
					UpdateProperty(prop);
				}
			}
		}

		public void RemoveEvent(LiveEvent evt)
		{
			evt.Data = null;
			Events.Remove(evt);
		}

		public void AddEvent(LiveEvent evt)
		{
			if (Events.Find(e => e.Time == evt.Time) != null)
			{
				return;
			}
			evt.Data = this;

			for (int i = 0; i < Events.Count; i++)
			{
				if (evt.Time < Events[i].Time)
				{
					Events.Insert(i, evt);
					return;
				}
			}
			Events.Add(evt);
		}

		private void ResortKeyframe(LiveKeyframe kf)
		{
			int index = Keyframes.IndexOf(kf);
			if (index == -1) { return; }
			float time = kf.Time;
			bool added = false;
			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe other = Keyframes[i];
				if (i == index)
				{
					continue;
				}
				if (other.Time > time)
				{
					added = true;
					int newIndex = i > index ? i - 1 : i;
					if (newIndex != index)
					{
						Keyframes.RemoveAt(index);
						Keyframes.Insert(newIndex, kf);
					}
					break;
				}
			}
			if (!added && index != Keyframes.Count - 1)
			{
				Keyframes.Add(kf);
			}
		}

		private void Kf_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			LiveKeyframe frame = sender as LiveKeyframe;
			if (e.PropertyName == "Time")
			{
				//resort
				ResortKeyframe(sender as LiveKeyframe);
			}
			else if (e.PropertyName == "PropertyMetadata")
			{
				//just don't do the else block
			}
			else if (frame.TrackedProperties.Contains(e.PropertyName))
			{
				UpdateProperty(e.PropertyName);

				//wipe out the frame if it has no properties remaining
				if (frame.IsEmpty && frame.Time > 0)
				{
					RemoveKeyframe(frame);
				}

				KeyframeChanged?.Invoke(this, frame);
			}
		}

		/// <summary>
		/// Updates the Properties array when a property changes
		/// </summary>
		/// <param name="property"></param>
		private void UpdateProperty(string property)
		{
			bool hasProperty = AnimatedProperties.Contains(property);
			int count = Keyframes.Count(kf => kf.HasProperty(property));
			if (count == 0 && hasProperty)
			{
				//need to remove the property
				RemoveAnimatedProperty(property);
			}
			else if (count > 0 && !hasProperty)
			{
				//need to add the property
				AddAnimatedProperty(property);
			}
		}

		private void AddAnimatedProperty(string property)
		{
			PropertyDefinition propertyDef = Definitions.Instance.Get<PropertyDefinition>(property);
			bool inserted = false;
			if (propertyDef != null)
			{
				for (int i = 0; i < Properties.Count; i++)
				{
					string prop = Properties[i];
					PropertyDefinition otherDef = Definitions.Instance.Get<PropertyDefinition>(prop);
					if (otherDef == null) { continue; }
					int compare = propertyDef.CompareTo(otherDef);
					if (compare < 0)
					{
						Properties.Insert(i, property);
						inserted = true;
						break;
					}
				}
			}
			if (!inserted)
			{
				Properties.Add(property);
			}
			AnimatedProperties.Add(property);
		}

		private void RemoveAnimatedProperty(string property)
		{
			Properties.Remove(property);
			AnimatedProperties.Remove(property);
		}

		/// <summary>
		/// Gets the time a particular property is animating.
		/// </summary>
		/// <remarks>
		/// If the property's 1st non-0 keyframe has the same value as time 0, then that frame will be treated as the starting frame
		/// </remarks>
		/// <param name="property"></param>
		/// <returns></returns>
		private float GetPropertyDuration(string property, float time, out float start, out float end)
		{
			start = 0;
			end = 0;
			List<LiveKeyframe> validFrames = new List<LiveKeyframe>();
			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe kf = Keyframes[i];
				if (!kf.HasProperty(property)) { continue; }

				KeyframeType type = kf.GetFrameType(property);
				if (kf.Time <= time && type != KeyframeType.Normal)
				{
					validFrames.Clear();
				}
				else if (kf.Time > time && type == KeyframeType.Begin)
				{
					break;
				}

				validFrames.Add(kf);

				if (kf.Time > time && type == KeyframeType.Split)
				{
					break;
				}
			}

			if (validFrames.Count == 0)
			{
				return 0;
			}

			start = validFrames[0].Time;
			end = validFrames[validFrames.Count - 1].Time;
			return end - start;
		}

		/// <summary>
		/// Gets the keyframe that starts a block
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public LiveKeyframe GetBlockKeyframe(string property, float time)
		{
			LiveKeyframe firstFrame = null;
			for (int i = Keyframes.Count - 1; i >= 0; i--)
			{
				LiveKeyframe kf = Keyframes[i];
				if (!kf.HasProperty(property)) { continue; }

				firstFrame = kf;
				if (i == 0 || kf.Time <= time)
				{
					LiveKeyframeMetadata metadata = kf.GetMetadata(property, false);
					if (metadata != null && (metadata.FrameType == KeyframeType.Begin || metadata.FrameType == KeyframeType.Split))
					{
						return kf;
					}
				}
			}

			if (Keyframes.Count == 0)
			{
				return null;
			}
			return firstFrame ?? Keyframes[0];
		}

		/// <summary>
		/// Gets the metadata for a keyframe block that encompasses the given time
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public LiveKeyframeMetadata GetBlockMetadata(string property, float time)
		{
			LiveKeyframe kf = GetBlockKeyframe(property, time);
			if (kf == null)
			{
				return new LiveKeyframeMetadata(property);
			}
			return kf.GetMetadata(property, false);
		}

		/// <summary>
		/// Gets the metadata of the last block for a property
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public LiveKeyframeMetadata GetLastBlockMetadata(string property)
		{
			for (int i = Keyframes.Count - 1; i >= 0; i--)
			{
				LiveKeyframe prevFrame = Keyframes[i];
				if (prevFrame.HasProperty(property))
				{
					LiveKeyframe prevStart = GetBlockKeyframe(property, prevFrame.Time);
					return prevStart?.GetMetadata(property, false);
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the value of a property at the given point in time
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="property">Property name</param>
		/// <param name="time">Time in seconds from the start of the anim</param>
		/// <param name="defaultValue">Value to use if no frames define this property</param>
		/// <returns>Interpolated value at the given point in time</returns>
		public T GetPropertyValue<T>(string property, float time, float offset, T defaultValue)
		{
			return GetPropertyValue<T>(property, time, offset, defaultValue, null, null, null);
		}
		/// <summary>
		/// Gets the value of a property at the given point in time
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="property">Property name</param>
		/// <param name="time">Time in seconds from the start of the anim</param>
		/// <param name="defaultValue">Value to use if no frames define this property</param>
		/// <param name="easeOverride">Ease to use instead of the property's defined ease</param>
		/// <param name="interpolationOverride">Interpolation to use instead of the property's defined interpolation</param>
		/// <returns>Interpolated value at the given point in time</returns>
		public T GetPropertyValue<T>(string property, float time, float offset, T defaultValue, string easeOverride, string interpolationOverride, bool? loopOverride)
		{
			float start;
			float end;

			if (Previous is LiveAnimatedObject)
			{
				//if before the first keyframe, and linked to a loop, treat time as the time since the start of the loop
				LiveKeyframe firstFrame = Keyframes.Find(kf => kf.HasProperty(property));
				if (firstFrame == null || firstFrame.Time > time)
				{
					LiveAnimatedObject prev = Previous as LiveAnimatedObject;
					bool foundPreviousProp = false;
					while (prev != null && !foundPreviousProp)
					{
						for (int i = prev.Keyframes.Count - 1; i >= 0; i--)
						{
							LiveKeyframe prevFrame = prev.Keyframes[i];
							if (prevFrame.HasProperty(property))
							{
								LiveKeyframe prevStart = prev.GetBlockKeyframe(property, prevFrame.Time);
								if (prevStart != null)
								{
									foundPreviousProp = true;
									if (prevStart.GetMetadata(property, false).Looped)
									{
										return prev.GetPropertyValue<T>(property, prev.Start + prevStart.Time + time, offset, defaultValue, null, null, null);
									}
									break;
								}
							}
						}
						prev = prev.Previous as LiveAnimatedObject;
					}
				}
			}

			float t = GetInterpolatedTime(property, time, offset, easeOverride, loopOverride, out start, out end);
			t = start + t * (end - start);

			Type parentType = GetKeyframeType();

			LiveKeyframeMetadata metadata = GetBlockMetadata(property, t);
			string frameInterp = metadata.Interpolation;
			string interpolation = interpolationOverride ?? frameInterp;
			if (string.IsNullOrEmpty(frameInterp) || frameInterp == "none")
			{
				interpolation = "none";
			}

			LiveKeyframe previousFrame = null;
			LiveKeyframe previousPreviousFrame = null;
			LiveKeyframe nextFrame = null;
			LiveKeyframe nextNextFrame = null;
			bool foundNext = false;
			bool foundNextNext = false;
			Stack<LiveKeyframe> validFrames = new Stack<LiveKeyframe>();

			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe kf = Keyframes[i];
				if (!kf.HasProperty(property)) { continue; }

				KeyframeType type = kf.GetFrameType(property);
				if (kf.Time <= t && type != KeyframeType.Normal)
				{
					foundNext = false;
					foundNextNext = false;
					validFrames.Clear();
				}
				else if (kf.Time > t && type == KeyframeType.Begin)
				{
					break;
				}
				validFrames.Push(kf);
				if (kf.Time > t)
				{
					if (foundNext)
					{
						foundNextNext = true;
						break;
					}
					foundNext = true;
				}
			}

			if (foundNextNext && validFrames.Count > 0)
			{
				nextNextFrame = validFrames.Pop();
			}
			if (validFrames.Count > 0)
			{
				nextFrame = validFrames.Pop();
			}
			if (validFrames.Count > 0)
			{
				previousFrame = validFrames.Pop();
			}
			if (validFrames.Count > 0)
			{
				previousPreviousFrame = validFrames.Pop();
			}

			if (nextFrame != null)
			{
				previousFrame = previousFrame ?? nextFrame;
				nextNextFrame = nextNextFrame ?? nextFrame;
				previousPreviousFrame = previousPreviousFrame ?? previousFrame;
				object previous = previousFrame.Get<object>(property);
				object next = nextFrame.Get<object>(property);
				object previousPrevious = previousPreviousFrame.Get<object>(property);
				object nextNext = nextNextFrame.Get<object>(property);
				float prevTime = previousFrame.Time;
				float nextTime = nextFrame.Time;
				float frameT = nextTime == prevTime ? 0 : (t - prevTime) / (nextTime - prevTime);
				Type propertyType = PropertyTypeInfo.GetType(parentType, property);
				return (T)AnimationHelpers.Interpolate(propertyType, previous, next, interpolation, frameT, previousPrevious, nextNext);
			}
			return defaultValue;
		}

		/// <summary>
		/// Gets a time from 0-1 where 0=first frame and 1=last frame based on a property's keyframes and animation settings
		/// </summary>
		/// <param name="property"></param>
		/// <param name="time"></param>
		/// <param name="easeOverride"></param>
		/// <param name="interpolationOverride"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		public float GetInterpolatedTime(string property, float time, float offset, string easeOverride, bool? loopOverride, out float start, out float end)
		{
			time -= Start; //use relative time
			time = Math.Max(0, time);

			//figure out this property's duration, which is from the first frame past time 0 if that frame has the same value as time 0, otherwise from time 0, to the last frame modifying this property
			start = 0;
			end = 0;
			float duration = GetPropertyDuration(property, time, out start, out end);

			LiveKeyframeMetadata metadata = GetBlockMetadata(property, time);
			string ease = easeOverride ?? metadata.Ease;
			bool looped = loopOverride ?? metadata.Looped;

			if (looped)
			{
				time += offset;
			}

			if (time < start)
			{
				return 0;
			}
			else if (time > end)
			{
				if (looped)
				{
					if (duration > 0.0001f && time > end)
					{
						time = Clamp(time, start, duration, metadata.Iterations, metadata.ClampMethod);
					}
				}
				else
				{
					return 1;
				}
			}

			float relativeTime = 0;
			if (duration > 0)
			{
				relativeTime = (time - start) / duration;
			}

			float t = AnimationHelpers.Ease(ease, relativeTime);
			return t;
		}

		private float Clamp(float t, float start, float duration, int iterations, string clampMethod)
		{
			if (iterations > 0)
			{
				if (t >= iterations * duration)
				{
					return start + duration;
				}
			}

			switch (clampMethod)
			{
				case "clamp":
					return t > start + duration ? start + duration : t;
				case "mirror":
					t -= start;
					float d2 = duration * 2;
					t %= d2;
					t = t > duration ? d2 - t : t;
					t += start;
					return t;
				default:
					while (t > start + duration)
					{
						t -= duration;
					}
					return t;
			}
		}

		/// <summary>
		/// Moves one or more properties from one keyframe to another (generating a new frame if it needs to)
		/// </summary>
		/// <param name="sourceFrame">Keyframe that the property originated on</param>
		/// <param name="time">Relative time to move the property to</param>
		/// <param name="targetFrame">Frame to move to. If not provided, a new frame at time will be generated</param>
		/// <returns>Keyframe containing the property after moving it</returns>
		public LiveKeyframe MoveProperty(LiveKeyframe sourceFrame, List<string> properties, float time, LiveKeyframe targetFrame)
		{
			if (targetFrame != null && !Keyframes.Contains(targetFrame))
			{
				AddKeyframe(targetFrame);
			}
			targetFrame = targetFrame ?? Keyframes.Find(k => k.Time == time);
			foreach (string property in properties)
			{
				if (!sourceFrame.HasProperty(property))
				{
					throw new ArgumentException($"Cannot move a property that doesn't exist: {property}.", nameof(properties));
				}
			}
			if (targetFrame == sourceFrame)
			{
				//if moving onto the same keyframe, just update the time
				targetFrame.Time = time;
			}
			else
			{
				//if the affected properties are the only properties on the sourceFrame, and there is no targetFrame, then just move the whole frame
				if (targetFrame == null && sourceFrame.PropertyCount == properties.Count)
				{
					sourceFrame.Time = time;
					targetFrame = sourceFrame;
				}
				else
				{
					foreach (string property in properties)
					{
						object val = sourceFrame.Get<object>(property);

						//1. Remove it from the previous keyframe, which might delete the sourceKeyframe too
						sourceFrame.Delete(property);

						//2. Create a new keyframe if needed
						if (targetFrame == null)
						{
							targetFrame = AddKeyframe(time);
						}

						//3. Put the property into the target frame
						targetFrame.Set(val, property);
					}
				}
			}

			return targetFrame;
		}

		/// <summary>
		/// Copies one or more properties from a keyframe into a new, loose keyframe
		/// </summary>
		/// <param name="keyframe">Keyframe to copy</param>
		/// <param name="properties">Properties to copy</param>
		/// <returns></returns>
		public LiveKeyframe CopyKeyframe(LiveKeyframe keyframe, HashSet<string> properties)
		{
			LiveKeyframe copy = CreateKeyframe(keyframe.Time);
			if (properties.Count == 0)
			{
				keyframe.CopyPropertiesInto(copy);
				return copy;
			}
			else
			{
				foreach (string property in properties)
				{
					copy.Set(keyframe.Get<object>(property), property);
				}
				return copy;
			}
		}

		/// <summary>
		/// Copies the properties from a keyframe into this sprite, replacing any previous properties at that time
		/// </summary>
		/// <param name="source">Keyframe to copy from</param>
		/// <param name="time">Time to paste properties at</param>
		/// <param name="target">Target frame to paste to. If not provided, a new frame will be created.</param>
		/// <returns></returns>
		public LiveKeyframe PasteKeyframe(LiveKeyframe source, float time, LiveKeyframe target)
		{
			target = target ?? Keyframes.Find(kf => kf.Time == time);
			if (target == null)
			{
				target = AddKeyframe(time);
			}
			else if (!Keyframes.Contains(target))
			{
				AddKeyframe(target);
			}
			foreach (string property in source.TrackedProperties)
			{
				if (source.HasProperty(property))
				{
					target.Set(source.Get<object>(property), property);
					if (source.PropertyMetadata.ContainsKey(property))
					{
						LiveKeyframeMetadata metadata = source.GetMetadata(property, false);
						target.PropertyMetadata[property] = metadata;
					}
				}
			}
			target.Time = time;
			return target;
		}

		/// <summary>
		/// Copies an event
		/// </summary>
		/// <param name="evt"></param>
		/// <returns></returns>
		public LiveEvent CopyEvent(LiveEvent evt)
		{
			LiveEvent copy = CreateEvent(evt.Time);
			evt.CopyPropertiesInto(copy);
			return copy;
		}

		public LiveEvent PasteEvent(LiveEvent source, float time)
		{
			LiveEvent target = Events.Find(kf => kf.Time == time);
			if (target != null)
			{
				return target;
			}
			target = AddEvent(time);
			source.CopyPropertiesInto(target);
			target.Time = time;
			return target;
		}

		/// <summary>
		/// Creates a keyframe representing the interpolated values at a particular time, without adding the frame to the sprite
		/// </summary>
		/// <param name="time">Relative time</param>
		/// <returns></returns>
		public LiveKeyframe GetInterpolatedFrame(float time)
		{
			LiveKeyframe frame = CreateKeyframe(time);

			foreach (string property in Properties)
			{
				frame.Set(GetPropertyValue<object>(property, time, 0, null), property);
			}

			return frame;
		}

		public override LiveObject CreateLivePreview(float time)
		{
			LiveAnimatedObject copy = Copy() as LiveAnimatedObject;
			copy.IsPreview = true;
			copy.Previous = Previous;
			copy.CenterX = CenterX;
			copy.CenterY = CenterY;
			copy.DisplayPastEnd = DisplayPastEnd;
			LinkedPreview = copy;
			copy.Data = Data;
			copy.Hidden = false;
			if (Keyframes.Count > 0)
			{
				copy.Keyframes.Clear();
				foreach (LiveKeyframe kf in Keyframes) //use the same keyframe references so we can modify them from either the preview or the source
				{
					copy.Keyframes.Add(kf);
				}
			}
			copy.PropertyChanged += Preview_PropertyChanged;
			copy.Keyframes.CollectionChanged += Preview_KeyframesChanged;
			copy.Update(time, 0, false);
			AttachSourceListener();
			return copy;
		}

		public override void DestroyLivePreview()
		{
			if (LinkedPreview == null) { return; }
			(LinkedPreview as LiveAnimatedObject).Keyframes.CollectionChanged -= Preview_KeyframesChanged;
			LinkedPreview.PropertyChanged -= Preview_PropertyChanged;
			DetachSourceListener();
			LinkedPreview = null;
		}

		/// <summary>
		/// Raised on a preview when a property on the preview object has changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Preview_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			DetachSourceListener();
			OnPreviewPropertyChanged(sender, e);
			if (e.PropertyName == "PivotX")
			{
				PivotX = LinkedPreview.PivotX;
			}
			else if (e.PropertyName == "PivotY")
			{
				PivotY = LinkedPreview.PivotY;
			}
			AttachSourceListener();
		}
		protected virtual void OnPreviewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{

		}

		/// <summary>
		/// Raised on a preview when the keyframes of the preview have changed to handle auto-keyframe insertion
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Preview_KeyframesChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				DetachSourceListener();
				foreach (LiveKeyframe kf in e.NewItems)
				{
					AddKeyframe(kf);
				}
				AttachSourceListener();
			}
		}

		/// <summary>
		/// Attaches property changed listeners to a source so it can keep its preview current
		/// </summary>
		private void AttachSourceListener()
		{
			Keyframes.CollectionChanged += Source_KeyframesChanged;
			PropertyChanged += Source_PropertyChanged;
		}

		private void DetachSourceListener()
		{
			Keyframes.CollectionChanged -= Source_KeyframesChanged;
			PropertyChanged -= Source_PropertyChanged;
		}

		private void Source_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (LinkedPreview == null) { return; }
			if (e.PropertyName == "Start")
			{
				LinkedPreview.Start = Start;
				LinkedPreview?.Update(Time, 0, false);
			}
			if (e.PropertyName == "PivotX")
			{
				LinkedPreview.PivotX = PivotX;
			}
			else if (e.PropertyName == "PivotY")
			{
				LinkedPreview.PivotY = PivotY;
			}
			else if (e.PropertyName == "ParentId")
			{
				LinkedPreview.ParentId = ParentId;
			}
			else if (e.PropertyName == "Length")
			{
				((LiveAnimatedObject)LinkedPreview).Length = Length;
			}
			OnSourcePropertyChanged(sender, e);
		}
		protected virtual void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		private void Source_KeyframesChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			InvalidatePreview();
		}

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			return false;
		}

		public sealed override void Update(float time, float elapsedTime, bool inPlayback)
		{
			Time = time;
			if (inPlayback)
			{
				if (_lastPlaybackTime != time)
				{
					TimeOffset = 0;
				}
				else
				{
					TimeOffset += (elapsedTime - _lastElapsedTime);
				}
				_lastElapsedTime = elapsedTime;
				_lastPlaybackTime = time;
			}

			string easeOverride = (inPlayback ? null : "linear");
			string interpolationOverride = (inPlayback ? null : "linear");
			bool? loopOverride = (inPlayback ? null : new bool?(false));

			OnUpdate(time, TimeOffset, easeOverride, interpolationOverride, loopOverride, inPlayback);
			OnUpdateDimensions();
		}
		protected virtual void OnUpdate(float time, float offset, string ease, string interpolation, bool? looped, bool inPlayback)
		{

		}
		protected virtual void OnUpdateDimensions()
		{
			int newWidth = WidthOverride ?? Width;
			int newHeight = HeightOverride ?? Height;
			if (!WidthOverride.HasValue && !HeightOverride.HasValue)
			{
				if (Image != null)
				{
					newWidth = Image.Width;
					newHeight = Image.Height;
				}
				else
				{
					newWidth = 100;
					newHeight = 100;
				}
			}
			if (newWidth != Width || newHeight != Height)
			{
				Width = newWidth;
				Height = newHeight;
				InvalidateTransform();
			}
		}

		/// <summary>
		/// Adds a keyframe from a definition
		/// </summary>
		/// <param name="kf"></param>
		/// <param name="timeOffset">Relative time from start of animation</param>
		public HashSet<string> AddKeyframe(Keyframe kf, float timeOffset, bool addBreak, float origin)
		{
			HashSet<string> properties = new HashSet<string>();

			float time;
			float.TryParse(kf.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out time);
			time += timeOffset;

			ParseKeyframe(kf, addBreak, properties, time, origin);
			return properties;
		}

		protected virtual void ParseKeyframe(Keyframe kf, bool addBreak, HashSet<string> properties, float time, float origin)
		{
		}

		/// <summary>
		/// Merges a directive into this preview to have one single animation
		/// </summary>
		/// <param name="directive"></param>
		/// <param name="offset">Absolute time that this animation block starts</param>
		/// <param name="linkToPrevious">IF true, previous keyframes will be copied/updated to match the split</param>
		public void AddKeyframeDirective(Directive directive, float offset, string defaultEase, string defaultInterpolation, bool linkToPrevious)
		{
			float startTime = offset - Start;
			if (!string.IsNullOrEmpty(directive.Delay))
			{
				float delay;
				if (float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out delay))
				{
					startTime += delay;
				}
			}
			if (startTime < 0)
			{
				startTime = 0; //if the delay was shorter than the sprite's delay, use no delay at all. This setup wouldn't work well anyway.
			}

			HashSet<string> affectedProperties = new HashSet<string>();
			if (directive.Keyframes.Count == 0)
			{
				bool addBreak = !linkToPrevious && startTime > 0;
				HashSet<string> properties = AddKeyframe(directive, startTime, addBreak, offset);
				affectedProperties.AddRange(properties);
			}
			else
			{
				directive.Keyframes.Sort((k1, k2) =>
				{
					string t1 = k1.Time ?? "0";
					string t2 = k2.Time ?? "0";
					float f1, f2;
					float.TryParse(t1, NumberStyles.Number, CultureInfo.InvariantCulture, out f1);
					float.TryParse(t2, NumberStyles.Number, CultureInfo.InvariantCulture, out f2);
					return f1.CompareTo(f2);
				});
				for (int i = 0; i < directive.Keyframes.Count; i++)
				{
					Keyframe kf = directive.Keyframes[i];
					bool addBreak = !linkToPrevious && (i == 0 && startTime > 0 && (string.IsNullOrEmpty(kf.Time) || kf.Time == "0"));
					HashSet<string> properties = AddKeyframe(kf, startTime, addBreak, offset);
					affectedProperties.AddRange(properties);
				}
			}
			LiveKeyframe startFrame = Keyframes.Find(kf => kf.Time == startTime);
			Dictionary<string, KeyframeType> frameTypes = new Dictionary<string, KeyframeType>();

			if (linkToPrevious)
			{
				Keyframe firstDirFrame = directive.Keyframes.Count > 0 ? directive.Keyframes[0] : directive;

				//may need to update the previous keyframe or copy it
				foreach (string prop in affectedProperties)
				{
					bool instantChange = (firstDirFrame.Time == "0" || string.IsNullOrEmpty(firstDirFrame.Time)) && firstDirFrame.Properties.ContainsKey(prop);

					LiveKeyframe mostRecent = null;
					LiveKeyframe first = null;
					LiveKeyframe last = null;
					int count = 0;
					for (int i = 0; i < Keyframes.Count; i++)
					{
						LiveKeyframe k = Keyframes[i];
						if (k.HasProperty(prop))
						{
							if (first == null)
							{
								first = k;
							}
							if (k.Time > startTime)
							{
								break;
							}
							else
							{
								count++;
								last = mostRecent;
								mostRecent = k;
							}
						}
					}
					if (mostRecent != null)
					{
						if (mostRecent == startFrame && startFrame.HasProperty(prop))
						{
							if (startFrame.Time == 0)
							{
								//any directive that changes a property at time 0 should be a begin
								if (instantChange)
								{
									frameTypes[prop] = KeyframeType.Begin;
								}
							}
							else
							{
								//directive whose first frame is after time 0

								if (instantChange)
								{
									//if this was changing immediately, always use begin
									frameTypes[prop] = KeyframeType.Begin;
								}
								else if (mostRecent == first)
								{
									//this is the first frame touching this property, so use a begin
									frameTypes[prop] = KeyframeType.Begin;
								}
								else
								{
									//if the value changed from the previous, this is a split. Otherwise it's a new begin
									object value = mostRecent.Get<object>(prop);
									object lastValue = last.Get<object>(prop);
									frameTypes[prop] = value.Equals(lastValue) ? KeyframeType.Begin : KeyframeType.Split;
								}
								////there was already a frame at this time, and there's a frame before it, so this has to be a split

								//frameTypes[prop] = count > 1 && first.GetMetadata(prop, false).FrameType != KeyframeType.Normal &&
								//	(!instantChange || !firstDirFrame.Properties.ContainsKey(prop)) ? KeyframeType.Split : KeyframeType.Begin;
							}
						}
						else
						{
							//no frame at the directive's delay, so this must be a Begin type
							//copy the most recent frame to the delay time
							if (startFrame == null)
							{
								startFrame = AddKeyframe(startTime);
							}
							startFrame.Set(mostRecent.Get<object>(prop), prop);
							frameTypes[prop] = KeyframeType.Begin;
						}
					}
					else if (first != null)
					{
						//there were no frames modifying this property prior to this frame's time, so add a default at time 0
						object defaultVal = GetDefaultValue(prop);
						startFrame = AddValue(startTime, prop, defaultVal, true);
					}
				}
			}
			else if (startFrame == null)
			{
				startFrame = AddKeyframe(startTime);
			}

			if (startFrame != null)
			{
				foreach (string prop in affectedProperties)
				{
					if (!startFrame.HasProperty(prop))
					{
						continue;
					}
					LiveKeyframeMetadata metadata = startFrame.GetMetadata(prop, true);
					if (linkToPrevious)
					{
						KeyframeType frameType;
						if (frameTypes.TryGetValue(prop, out frameType))
						{
							metadata.FrameType = frameType;
						}
					}
					string ease = directive.EasingMethod ?? defaultEase;
					string interpolation = directive.InterpolationMethod ?? defaultInterpolation;
					bool looped = directive.Looped;
					string clamp = directive.ClampingMethod;
					int iterations = directive.Iterations;

					metadata.Ease = ease;
					metadata.Interpolation = interpolation;
					metadata.Looped = looped;
					metadata.ClampMethod = clamp;
					metadata.Iterations = iterations;
				}
			}
		}

		public void AddStopDirective(Directive directive, float offset)
		{
			offset -= Start;

			LiveKeyframe kfRef = CreateKeyframe(0);
			HashSet<string> trackedProperties = kfRef.TrackedProperties;

			//special handling for the camera being split apart
			if (directive.Id == "fade")
			{
				trackedProperties.Remove("X");
				trackedProperties.Remove("Y");
				trackedProperties.Remove("Zoom");
			}
			else if (directive.Id == "camera")
			{
				trackedProperties.Remove("Color");
				trackedProperties.Remove("Alpha");
			}

			//Add a new begin frame with the same values as the last keyframe of every looping property
			foreach (string property in trackedProperties)
			{
				string propName = property.ToLowerInvariant();
				if (directive.AffectedProperties.Count > 0 && !directive.AffectedProperties.Contains(propName))
				{
					continue;
				}

				bool found = false;
				for (int i = Keyframes.Count - 1; i >= 0; i--)
				{
					LiveKeyframe kf = Keyframes[i];
					if (kf.HasProperty(property))
					{
						LiveKeyframeMetadata metadata = GetBlockMetadata(property, kf.Time);
						if (metadata.Indefinite)
						{
							HashSet<string> properties = new HashSet<string>();
							properties.Add(property);
							LiveKeyframe copy = CopyKeyframe(kf, properties);
							LiveKeyframe target = PasteKeyframe(copy, offset, null);
							LiveKeyframeMetadata md = target.GetMetadata(property, true);
							md.FrameType = KeyframeType.Begin;
							found = true;
							break;
						}
					}
				}
				if (!found && Previous != null)
				{
					//go back to the last animation if this one doesn't contain the property
					LiveAnimatedObject prev = Previous as LiveAnimatedObject;
					while (!found && prev != null)
					{
						for (int i = prev.Keyframes.Count - 1; i >= 0; i--)
						{
							LiveKeyframe kf = prev.Keyframes[i];
							if (kf.HasProperty(property))
							{
								LiveKeyframeMetadata metadata = prev.GetBlockMetadata(property, kf.Time);
								if (metadata.Indefinite)
								{
									HashSet<string> properties = new HashSet<string>();
									properties.Add(property);
									LiveKeyframe copy = prev.CopyKeyframe(kf, properties);
									LiveKeyframe target = PasteKeyframe(copy, offset, null);
									LiveKeyframeMetadata md = target.GetMetadata(property, true);
									md.FrameType = KeyframeType.Begin;
									found = true;
									break;
								}
							}
						}
						if (found)
						{
							break;
						}
						prev = prev.Previous as LiveAnimatedObject;
					}
				}
			}
		}

		/// <summary>
		/// Gets the first frame containing a property
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public LiveKeyframe GetFirstFrame(string property)
		{
			return GetNextFrame(-1, property);
		}

		/// <summary>
		/// Gets the last frame containing a property
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public LiveKeyframe GetLastFrame(string property)
		{
			for (int i = Keyframes.Count - 1; i >= 0; i--)
			{
				LiveKeyframe kf = Keyframes[i];
				if (kf.HasProperty(property))
				{
					return kf;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the next frame containing a property after the frame at index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		private LiveKeyframe GetNextFrame(int index, string property)
		{
			for (int j = index + 1; j < Keyframes.Count; j++)
			{
				LiveKeyframe kf = Keyframes[j];
				if (kf.HasProperty(property))
				{
					return kf;
				}
			}
			return null;
		}

		/// <summary>
		/// Retrieves the beginning and ending frames of animation block
		/// </summary>
		/// <param name="property">Property being animated</param>
		/// <param name="time">Time to find which block it belongs to</param>
		/// <param name="useNextBlock">If true, gets the block following this time if it's not part of a block, otherwise the previous block</param>
		/// <param name="start">Outputs start frame</param>
		/// <param name="end">Outputs end frame</param>
		public virtual void GetBlock(string property, float time, bool useNextBlock, out LiveKeyframe start, out LiveKeyframe end)
		{
			start = null;
			end = null;
			LiveKeyframe firstStart = null;
			LiveKeyframe first = null;
			LiveKeyframe last = null;
			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe kf = Keyframes[i];
				if (!kf.HasProperty(property)) { continue; }

				if (first == null)
				{
					first = kf;
				}
				last = kf;

				KeyframeType type = i == 0 ? KeyframeType.Begin : kf.GetFrameType(property);
				if (start == null)
				{
					if (kf.Time <= time && (i == 0 || type == KeyframeType.Begin || type == KeyframeType.Split))
					{
						//found a possible starting point
						start = kf;
					}
					else if (first == kf && kf.Time > time)
					{
						//first frame for a particular property is considered start point if time is before that frame
						start = kf;
					}
					else if (useNextBlock && firstStart == null && kf.Time > time)
					{
						//if this starts past the given time but we're using the next block, count it
						start = kf;
					}

					if (start != null && firstStart != null)
					{
						firstStart = start;
					}
				}
				else
				{
					LiveKeyframe next = GetNextFrame(i, property);
					KeyframeType nextType = next == null ? KeyframeType.Begin : next.GetFrameType(property);

					if (type == KeyframeType.Begin)
					{
						//this frame is a begin, so use the last frame as the end if time is in range
						//this should only be entered if the previous frame was the start. Otherwise the nextType == Begin below would cover this case a step earlier
						if (time < kf.Time)
						{
							end = start;
							return;
						}
						else
						{
							//otherwise use this as the new start
							last = null;
							start = kf;
						}
					}
					else if (type == KeyframeType.Split)
					{
						//this frame is a split, so if time is in range, use this frame as the end
						if (time < kf.Time)
						{
							end = kf;
							return;
						}
						else
						{
							//otherwise use this as the new start
							last = null;
							start = kf;
						}
					}
					else if (nextType == KeyframeType.Begin)
					{
						//next frame begins a new set, so if time is before that, use this frame as the end
						if (time < kf.Time)
						{
							end = last;
							return;
						}
						else if (next == null)
						{
							//no more keyframes, so this must be the end
							end = kf;
							return;
						}
						else
						{
							//otherwise this range doesn't contain the frame
							last = null;
							start = null;
						}
					}
				}
			}
			if (start == null)
			{
				start = last;
			}
			end = (last ?? start);
		}

		/// <summary>
		/// Adds an event at the given point
		/// </summary>
		/// <param name="time"></param>
		public LiveEvent AddEvent(float time)
		{
			LiveEvent evt = CreateEvent(time);
			if (evt == null) { return null; }

			AddEvent(evt);
			return evt;
		}

		public LiveEvent CreateEvent(float time)
		{
			Type type = GetEventType();
			if (type == null) { return null; }
			LiveEvent kf = Activator.CreateInstance(type) as LiveEvent;
			kf.Time = time - Start;
			return kf;
		}

		protected override void OnSetPrevious()
		{
			List<string> remainingProps = null;
			LiveAnimatedObject anim = Previous as LiveAnimatedObject;

			PropertyHistory = anim.PropertyHistory; //transfer the history over

			//copy last frame of previous into time 0
			if (anim.Keyframes.Count > 0)
			{
				for (int i = anim.Keyframes.Count - 1; i >= 0; i--)
				{
					LiveKeyframe kf = anim.Keyframes[i];
					if (remainingProps == null)
					{
						remainingProps = new List<string>();
						remainingProps.AddRange(kf.TrackedProperties);
					}
					for (int p = 0; p < remainingProps.Count; p++)
					{
						string property = remainingProps[p];
						if (kf.HasProperty(property))
						{
							remainingProps.RemoveAt(p--);

							//Only add if this property isn't looping
							LiveKeyframeMetadata blockData = anim.GetBlockMetadata(property, kf.Time);
							if (!blockData.Indefinite)
							{
								AddValue(0, property, kf.Get<object>(property), false);
							}
						}
					}
					if (remainingProps.Count == 0)
					{
						return;
					}
				}
			}
		}

		/// <summary>
		/// Gets the last value set for a property prior to this time
		/// </summary>
		/// <param name="property"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public object GetPreviousValue(string property, float time, bool disallowLoops)
		{
			for (int i = Keyframes.Count - 1; i >= 0; i--)
			{
				LiveKeyframe kf = Keyframes[i];
				if (kf.Time >= time)
				{
					continue;
				}
				if (kf.HasProperty(property))
				{
					LiveKeyframeMetadata blockData = GetBlockMetadata(property, kf.Time);
					if (disallowLoops && blockData.Indefinite || !disallowLoops && !blockData.Indefinite)
					{
						return null;
					}
					return kf.Get<object>(property);
				}
			}
			if (Previous is LiveAnimatedObject)
			{
				LiveAnimatedObject prev = Previous as LiveAnimatedObject;
				if (prev.Keyframes.Count > 0)
				{
					time = prev.Keyframes[prev.Keyframes.Count - 1].Time + 0.001f;
					return prev.GetPreviousValue(property, time, disallowLoops);
				}
			}
			return null;
		}

		protected virtual HashSet<string> GetLoopableProperties(string sourceProperty)
		{
			return CreateKeyframe(0).TrackedProperties;
		}

		/// <summary>
		/// Gets all properties on this object that are looping prior to a keyframe
		/// </summary>
		/// <returns></returns>
		public HashSet<string> GetLoopedProperties(LiveKeyframe kf, string sourceProperty)
		{
			HashSet<string> trackedProperties = GetLoopableProperties(sourceProperty);

			HashSet<string> props = new HashSet<string>();
			int index = Keyframes.IndexOf(kf);
			if (index < 0)
			{
				return props;
			}
			foreach (string property in trackedProperties)
			{
				bool found = false;
				for (int i = index - 1; i >= 0; i--)
				{
					LiveKeyframe frame = Keyframes[i];
					if (frame.HasProperty(property))
					{
						LiveKeyframeMetadata md = GetBlockMetadata(property, frame.Time);
						if (md.Indefinite)
						{
							props.Add(property.ToLowerInvariant());
							found = true;
							break;
						}
					}
				}
				if (!found)
				{
					LiveAnimatedObject prev = Previous as LiveAnimatedObject;
					LiveKeyframeMetadata metadata = null;
					while (prev != null && metadata == null)
					{
						metadata = prev.GetLastBlockMetadata(property);
						prev = prev.Previous as LiveAnimatedObject;
					}
					if (metadata != null && metadata.Indefinite)
					{
						props.Add(property.ToLowerInvariant());
					}
				}
			}
			return props;
		}

		/// <summary>
		/// Updates the previous values to use those from the given frame
		/// </summary>
		/// <param name="sourceObject">The object the frame belongs to</param>
		/// <param name="frame">The frame to add</param>
		public void UpdateHistory(LiveAnimatedObject sourceObject, LiveKeyframe frame)
		{
			foreach (string property in frame.TrackedProperties)
			{
				if (frame.HasProperty(property))
				{
					UpdateHistory(sourceObject, frame, property);
				}
			}
		}
		/// <summary>
		/// Updates the previous values to use those from the given frame
		/// </summary>
		/// <param name="sourceObject">The object the frame belongs to</param>
		/// <param name="frame">The frame to add</param>
		/// <param name="property">Target property to update</param>
		public void UpdateHistory(LiveAnimatedObject sourceObject, LiveKeyframe frame, string property)
		{
			if (frame.HasProperty(property))
			{
				LiveKeyframeMetadata block = GetBlockMetadata(property, frame.Time - 0.001f);
				LiveKeyframeMetadata frameData = frame.GetMetadata(property, false);
				object value = frame.Get<object>(property);

				KeyframeHistory history;
				if (!PropertyHistory.TryGetValue(property, out history))
				{
					history = new KeyframeHistory(PropertyTypeInfo.GetType(frame.GetType(), property));
					PropertyHistory[property] = history;
				}

				history.BlockMetadata = block;
				history.FrameMetadata = frameData;
				history.Value = value;
			}
		}
	}

	public class KeyframeHistory
	{
		/// <summary>
		/// The metadata for the block the keyframe is part of
		/// </summary>
		public LiveKeyframeMetadata BlockMetadata = null;

		/// <summary>
		/// The metadata for the frame itself
		/// </summary>
		public LiveKeyframeMetadata FrameMetadata = null;

		/// <summary>
		/// The value in for the property
		/// </summary>
		public object Value;

		/// <summary>
		/// Property type
		/// </summary>
		public Type Type;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type">Value type of the property</param>
		public KeyframeHistory(Type type)
		{
			Type = type;
		}

		/// <summary>
		/// Gets whether a value matches the one in this history
		/// </summary>
		/// <param name="value">Value to compare. Assumed to not be null.</param>
		/// <returns></returns>
		public bool MatchesValue(object value)
		{
			if (Value == null)
			{
				return false;
			}
			if (Type == typeof(string) || Type == typeof(Color))
			{
				return Value.Equals(value);
			}
			else
			{
				//assumed to be a number
				return (float)Value == (float)value;
			}
		}

		public override string ToString()
		{
			return Value.ToString() + (BlockMetadata.Indefinite ? " (Looping)" : "");
		}
	}
}
