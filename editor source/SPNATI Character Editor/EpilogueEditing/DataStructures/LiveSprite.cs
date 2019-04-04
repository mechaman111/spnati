using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using SPNATI_Character_Editor.EditControls;
using SPNATI_Character_Editor.Controls.EditControls;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveSprite : BindableObject, ILabel
	{
		public SpriteWidget Widget;
		public LivePose Pose;
		public event EventHandler LabelChanged;

		[Text(DisplayName = "Id", Key = "id", GroupOrder = 0)]
		public string Id
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Hide from canvas UI
		/// </summary>
		public bool Hidden
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		//Not exposing since there are still some questions about the best way to recognize this from a PoseDirective
		//[Boolean(DisplayName = "Full Length", GroupOrder = 10, Description = "If checked, the sprite has no set end time.")]
		public bool LinkedToEnd
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[DirectiveMarker(DisplayName = "Marker", GroupOrder = 13, Key = "marker", Description = "Run this directive only if the marker's condition is met", ShowPrivate = true)]
		public string Marker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Numeric(DisplayName = "Layer", Key = "z", GroupOrder = 15)]
		public int Z
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Pivot X", Key = "pivotx", GroupOrder = 20, Description = "X value of rotation/scale point of origin as a percentage of the sprite's physical size.", Minimum = -1000, Maximum = 1000, Increment = 0.1f)]
		public float PivotX
		{
			get { return Get<float>(); }
			set { Set(value); }
		}
		[Float(DisplayName = "Pivot Y", Key = "pivoty", GroupOrder = 20, Description = "Y value of Rotation/scale point of origin as a percentage of the sprite's physical size.", Minimum = -1000, Maximum = 1000, Increment = 0.1f)]
		public float PivotY
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Start", Key = "start", GroupOrder = 5, Description = "Starting time to display the sprite.", Minimum = 0, Maximum = 1000, Increment = 0.1f)]
		public float Start
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		//[AnimDuration(DisplayName = "Duration", GroupOrder = 8, Description = "Total time to display the sprite when not using Full Length.", Minimum = 0.1f, Maximum = 1000, Increment = 0.1f,
//			BoundProperties = new string[] { "LinkedToEnd", "Keyframes" })]
		public float Length
		{
			get
			{
				if (Keyframes.Count > 1)
				{
					float time = Keyframes[Keyframes.Count - 1].Time;
					return time;
				}
				return Get<float>();
			}
			set { Set(value); }
		}

		public ObservableCollection<string> Properties
		{
			get { return Get<ObservableCollection<string>>(); }
			set { Set(value); }
		}

		public ObservableDictionary<string, AnimatedProperty> AnimatedProperties
		{
			get { return Get<ObservableDictionary<string, AnimatedProperty>>(); }
			set { Set(value); }
		}

		public ObservableCollection<LiveKeyframe> Keyframes
		{
			get { return Get<ObservableCollection<LiveKeyframe>>(); }
			set { Set(value); }
		}

		public bool IsVisible
		{
			get { return Time >= Start && (LinkedToEnd || Time <= Start + Length); }
		}

		public float Time { get; private set; }

		public event EventHandler<LiveKeyframe> KeyframeChanged;

		private float GetRelativeTime()
		{
			return Time - Start;
		}

		#region Interpolated values based on the current time
		public float X;
		public float Y;
		public Bitmap Image;
		public int Width;
		public int Height;
		public float ScaleX = 1;
		public float ScaleY = 1;
		public float SkewX = 0;
		public float SkewY = 0;
		public float Rotation = 0;
		public float Alpha = 100;
		#endregion

		public LiveSprite(LivePose pose, float time) : this()
		{
			Pose = pose;
			Length = 1;
			Start = time;
			Id = "New Sprite";
			PivotX = 0.5f;
			PivotY = 0.5f;
			LinkedToEnd = true;
			LiveKeyframe startFrame = new LiveKeyframe(0);
			startFrame.X = 0;
			startFrame.Y = 0;
			AddKeyframe(startFrame);
			Update(time, false);
		}

		public LiveSprite(LivePose pose, Sprite sprite, float time) : this()
		{
			Pose = pose;
			Length = 0.5f;
			Id = sprite.Id;
			Z = sprite.Z;
			Start = time;
			LinkedToEnd = true;
			if (!string.IsNullOrEmpty(sprite.Delay))
			{
				float start;
				float.TryParse(sprite.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out start);
				Start = start;
				Length = 1;
			}
			if (!string.IsNullOrEmpty(sprite.PivotX))
			{
				float pivot;
				string pivotX = sprite.PivotX;
				if (pivotX.EndsWith("%"))
				{
					pivotX = pivotX.Substring(0, pivotX.Length - 1);
				}
				float.TryParse(pivotX, NumberStyles.Number, CultureInfo.InvariantCulture, out pivot);
				pivot /= 100.0f;
				PivotX = pivot;
			}
			else
			{
				PivotX = 0.5f;
			}
			if (!string.IsNullOrEmpty(sprite.PivotY))
			{
				float pivot;
				string pivotY = sprite.PivotY;
				if (pivotY.EndsWith("%"))
				{
					pivotY = pivotY.Substring(0, pivotY.Length - 1);
				}
				float.TryParse(pivotY, NumberStyles.Number, CultureInfo.InvariantCulture, out pivot);
				pivot /= 100.0f;
				PivotY = pivot;
			}
			else
			{
				PivotY = 0.5f;
			}
			LiveKeyframe temp;
			AddKeyframe(sprite, 0, false, out temp);
			Update(time, false);
		}

		public string GetLabel()
		{
			return $"Sprite Settings: {Id}";
		}

		private LiveSprite(LiveSprite source) : this()
		{
			source.CopyPropertiesInto(this);
			foreach (LiveKeyframe kf in this.Keyframes)
			{
				kf.Sprite = this;
				kf.PropertyChanged += Kf_PropertyChanged;
			}
		}

		private LiveSprite()
		{
			Properties = new ObservableCollection<string>();
			Keyframes = new ObservableCollection<LiveKeyframe>();
			AnimatedProperties = new ObservableDictionary<string, AnimatedProperty>();
		}

		public override string ToString()
		{
			return $"{Id} ({Start})";
		}

		public LiveSprite Copy()
		{
			return new LiveSprite(this);
		}

		public AnimatedProperty GetAnimationProperties(string propName)
		{
			AnimatedProperty props;
			AnimatedProperties.TryGetValue(propName, out props);
			return props ?? new AnimatedProperty(propName);
		}

		public void AddValue<T>(float time, string propName, string serializedValue)
		{
			AddValue<T>(time, propName, serializedValue, false);
		}

		/// <summary>
		/// Adds a property value to a keyframe at the given time
		/// </summary>
		/// <param name="time">Time in seconds from start </param>
		/// <param name="propName"></param>
		/// <param name="serializedValue"></param>
		/// <returns>Keyframe at that point</returns>
		private void AddValue<T>(float time, string propName, string serializedValue, bool addAnimBreak)
		{
			if (string.IsNullOrEmpty(serializedValue))
			{
				return;
			}
			if (!AnimatedProperties.ContainsKey(propName))
			{
				AddAnimatedProperty(propName);
			}
			LiveKeyframe keyframe = Keyframes.Find(k => k.Time == time);
			if (keyframe == null)
			{
				keyframe = AddKeyframe(time);
			}

			if (addAnimBreak)
			{
				keyframe.InterpolationBreaks[propName] = true;
			}

			object val = null;
			Type propType = typeof(T);
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
			else
			{
				throw new ArgumentException($"Type {typeof(T).Name} not supported.");
			}
			keyframe.Set(val, propName);
		}

		/// <summary>
		/// Merges a directive into this preview to have one single animation
		/// </summary>
		/// <param name="directive"></param>
		public void AddDirective(PoseDirective directive)
		{
			float delay = Start;
			if (!string.IsNullOrEmpty(directive.Delay))
			{
				float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out delay);
			}
			float startTime = delay - Start;
			if (startTime < 0)
			{
				startTime = 0; //if the delay was shorter than the sprite's delay, use no delay at all. This setup wouldn't work well anyway.
			}

			HashSet<string> affectedProperties = new HashSet<string>();
			directive.Keyframes.Sort((k1, k2) => {
				string t1 = k1.Time ?? "0";
				string t2 = k2.Time ?? "0";
				return t1.CompareTo(t2);
			});
			for (int i = 0; i < directive.Keyframes.Count; i++)
			{
				Keyframe kf = directive.Keyframes[i];
				bool addBreak = (i == 0 && startTime > 0);
				LiveKeyframe liveFrame;
				HashSet<string> properties = AddKeyframe(kf, startTime, addBreak, out liveFrame);

				foreach (string prop in properties)
				{
					affectedProperties.Add(prop);

					//if (prop == "Alpha" && i == directive.Keyframes.Count - 1 && kf.Opacity == "0")
					//{
					//	//if setting alpha to 0 on the last frame, consider this to be the sprite's duration
					//	liveFrame.Delete("Alpha");
					//	LinkedToEnd = false;
					//	Length = liveFrame.Time;
					//}
				}
			}
			foreach (string prop in affectedProperties)
			{
				AnimatedProperty animatedProperty = GetAnimationProperties(prop);
				animatedProperty.Ease = directive.EasingMethod;
				animatedProperty.Interpolation = directive.InterpolationMethod;
				animatedProperty.Looped = animatedProperty.Looped || directive.Looped;
			}
		}

		/// <summary>
		/// Adds a keyframe to the LiveSprite
		/// </summary>
		/// <param name="kf"></param>
		public HashSet<string> AddKeyframe(Keyframe kf, float timeOffset, bool addBreak, out LiveKeyframe frame)
		{
			HashSet<string> properties = new HashSet<string>();

			float time;
			float.TryParse(kf.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out time);
			time += timeOffset;

			if (!string.IsNullOrEmpty(kf.Src))
			{
				AddValue<string>(time, "Src", kf.Src, addBreak);
				properties.Add("Src");
			}
			if (!string.IsNullOrEmpty(kf.X))
			{
				AddValue<float>(time, "X", kf.X, addBreak);
				properties.Add("X");
			}
			if (!string.IsNullOrEmpty(kf.Y))
			{
				AddValue<float>(time, "Y", kf.Y, addBreak);
				properties.Add("Y");
			}
			if (!string.IsNullOrEmpty(kf.ScaleX))
			{
				AddValue<float>(time, "ScaleX", kf.ScaleX, addBreak);
				properties.Add("ScaleX");
			}
			if (!string.IsNullOrEmpty(kf.ScaleY))
			{
				AddValue<float>(time, "ScaleY", kf.ScaleY, addBreak);
				properties.Add("ScaleY");
			}
			if (!string.IsNullOrEmpty(kf.Opacity))
			{
				AddValue<float>(time, "Alpha", kf.Opacity, addBreak);
				properties.Add("Alpha");
			}
			if (!string.IsNullOrEmpty(kf.Rotation))
			{
				AddValue<float>(time, "Rotation", kf.Rotation, addBreak);
				properties.Add("Rotation");
			}
			if (!string.IsNullOrEmpty(kf.SkewX))
			{
				AddValue<float>(time, "SkewX", kf.SkewX, addBreak);
				properties.Add("SkewX");
			}
			if (!string.IsNullOrEmpty(kf.SkewY))
			{
				AddValue<float>(time, "SkewX", kf.SkewY, addBreak);
				properties.Add("SkewY");
			}

			frame = Keyframes.Find(k => k.Time == time);
			return properties;
		}

		public LiveKeyframe AddKeyframe(float time)
		{
			LiveKeyframe kf = new LiveKeyframe(time);
			AddKeyframe(kf);
			return kf;
		}

		public void AddKeyframe(LiveKeyframe kf)
		{
			kf.Sprite = this;
			kf.PropertyChanged += Kf_PropertyChanged;
			Keyframes.Add(kf);

			foreach (string prop in LiveKeyframe.TrackedProperties)
			{
				if (kf.HasProperty(prop))
				{
					UpdateProperty(prop);
				}
			}

			ResortKeyframes();
		}

		public void RemoveKeyframe(LiveKeyframe kf)
		{
			kf.PropertyChanged -= Kf_PropertyChanged;
			kf.Sprite = null;
			Keyframes.Remove(kf);

			foreach (string prop in LiveKeyframe.TrackedProperties)
			{
				if (kf.HasProperty(prop))
				{
					UpdateProperty(prop);
				}
			}
		}

		private void ResortKeyframes()
		{
			Keyframes.Sort((k1, k2) => k1.Time.CompareTo(k2.Time));
		}

		private void Kf_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			LiveKeyframe frame = sender as LiveKeyframe;
			if (e.PropertyName == "Time")
			{
				ResortKeyframes();
			}
			else if (e.PropertyName == "InterpolationBreaks")
			{
			}
			else
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
			bool hasProperty = AnimatedProperties.ContainsKey(property);
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
			for (int i = 0; i < Properties.Count; i++)
			{
				string prop = Properties[i];
				PropertyDefinition otherDef = Definitions.Instance.Get<PropertyDefinition>(prop);
				int compare = propertyDef.CompareTo(otherDef);
				if (compare < 0)
				{
					Properties.Insert(i, property);
					inserted = true;
					break;
				}
			}
			if (!inserted)
			{
				Properties.Add(property);
			}
			AnimatedProperty anim = new AnimatedProperty(property);
			anim.Interpolation = property == "Src" ? null : "linear";
			anim.Ease = null;
			AnimatedProperties[property] = anim;
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
				if (kf.HasProperty(property))
				{
					if (kf.Time <= time && kf.InterpolationBreaks.ContainsKey(property))
					{
						validFrames.Clear();
					}
					else if (kf.Time > time && kf.InterpolationBreaks.ContainsKey(property))
					{
						break;
					}

					validFrames.Add(kf);
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
		/// Gets the value of a property at the given point in time
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="property">Property name</param>
		/// <param name="time">Time in seconds from the start of the anim</param>
		/// <param name="defaultValue">Value to use if no frames define this property</param>
		/// <returns>Interpolated value at the given point in time</returns>
		public T GetPropertyValue<T>(string property, float time, T defaultValue)
		{
			return GetPropertyValue<T>(property, time, defaultValue, null, null, null);
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
		public T GetPropertyValue<T>(string property, float time, T defaultValue, string easeOverride, string interpolationOverride, bool? loopOverride)
		{
			float start;
			float end;
			float t = GetInterpolatedTime(property, time, easeOverride, interpolationOverride, loopOverride, out start, out end);
			t = start + t * (end - start);

			Type parentType = typeof(LiveKeyframe);

			AnimatedProperty propertyAnimation = GetAnimationProperties(property);
			string interpolation = interpolationOverride ?? propertyAnimation.Interpolation;
			if (string.IsNullOrEmpty(propertyAnimation.Interpolation) || propertyAnimation.Interpolation == "none")
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
				if (!kf.HasProperty(property))
				{
					continue;
				}
				if (kf.Time <= t && kf.InterpolationBreaks.ContainsKey(property))
				{
					foundNext = false;
					foundNextNext = false;
					validFrames.Clear();
				}
				if (kf.Time > t && kf.InterpolationBreaks.ContainsKey(property))
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
		public float GetInterpolatedTime(string property, float time, string easeOverride, string interpolationOverride, bool? loopOverride, out float start, out float end)
		{
			time -= Start; //use relative time
			time = Math.Max(0, time);

			//figure out this property's duration, which is from the first frame past time 0 if that frame has the same value as time 0, otherwise from time 0, to the last frame modifying this property
			start = 0;
			end = 0;
			float duration = GetPropertyDuration(property, time, out start, out end);

			Type parentType = typeof(LiveKeyframe);
			AnimatedProperty propertyAnimation = GetAnimationProperties(property);
			string ease = easeOverride ?? propertyAnimation.Ease;
			bool looped = loopOverride.HasValue ? loopOverride.Value : propertyAnimation.Looped;

			if (time < start)
			{
				return 0;
			}
			else if (time > end)
			{
				if (looped)
				{
					while (duration > 0.0001f && time > end)
					{
						time -= duration;
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
			LiveKeyframe copy = new LiveKeyframe(keyframe.Time);
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
					if (keyframe.InterpolationBreaks.ContainsKey(property))
					{
						copy.InterpolationBreaks[property] = true;
					}
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
			LiveKeyframe frame = new LiveKeyframe(time);

			foreach (string property in Properties)
			{
				frame.Set(GetPropertyValue<object>(property, time, null), property);
			}

			return frame;
		}

		#region Point-and-click editing
		/// <summary>
		/// Updates the sprite's position to a new value, updating the underlying data structures too
		/// </summary>
		/// <returns>List of objects that were modified</returns>
		public void Translate(int x, int y)
		{
			if (X == x && Y == y)
			{
				return;
			}

			float time = GetRelativeTime();
			AddValue<float>(time, "X", x.ToString());
			AddValue<float>(time, "Y", y.ToString());
		}

		public void AdjustPivot(Point screenPt, RectangleF worldBounds)
		{
			float xPct = (screenPt.X - worldBounds.X) / worldBounds.Width;
			float yPct = (screenPt.Y - worldBounds.Y) / worldBounds.Height;
			float pivotX = xPct;
			float pivotY = yPct;
			if (pivotX == PivotX && pivotY == PivotY)
			{
				return;
			}
			PivotX = xPct;
			PivotY = yPct;
		}

		public void Scale(Point screenPoint, int displayWidth, int displayHeight, Point offset, float zoom, Point startPoint, HoverContext context, bool locked)
		{
			bool horizontal = (context & HoverContext.ScaleHorizontal) != 0;
			bool vertical = (context & HoverContext.ScaleVertical) != 0;

			RectangleF bounds = ToUnscaledScreenRegion(displayWidth, displayHeight, offset, zoom);
			PointF targetPoint = new PointF(screenPoint.X, screenPoint.Y);
			PointF sourcePoint = new PointF(bounds.X, bounds.Y); //unscaled point corresponding to the point being dragged
			if (context.HasFlag(HoverContext.ScaleRight))
			{
				sourcePoint.X += bounds.Width;
			}
			if (context.HasFlag(HoverContext.ScaleBottom))
			{
				sourcePoint.Y += bounds.Height;
			}

			PointF pivot = new PointF(bounds.X + PivotX * bounds.Width, bounds.Y + PivotY * bounds.Height);
			//shift pivot to origin

			sourcePoint.X -= pivot.X;
			sourcePoint.Y -= pivot.Y;

			targetPoint.X -= pivot.X;
			targetPoint.Y -= pivot.Y;

			//determine scalar to reach given point
			float mx = targetPoint.X / sourcePoint.X;
			float my = targetPoint.Y / sourcePoint.Y;

			if (float.IsInfinity(mx))
			{
				mx = 0.001f;
			}
			if (float.IsInfinity(my))
			{
				my = 0.001f;
			}

			float time = GetRelativeTime();
			if (ScaleX != mx && horizontal)
			{
				AddValue<float>(time, "ScaleX", mx.ToString(CultureInfo.InvariantCulture));
			}
			if (ScaleY != my && vertical)
			{
				AddValue<float>(time, "ScaleY", my.ToString(CultureInfo.InvariantCulture));
			}
		}

		public void Rotate(Point screenPoint, Point screenCenter)
		{
			//quick and dirty - just use the angle to look from the point to the center

			double angle = Math.Atan2(screenCenter.Y - screenPoint.Y, screenCenter.X - screenPoint.X);
			angle = angle * (180 / Math.PI) - 90;

			if (Rotation == angle)
			{
				return;
			}

			float time = GetRelativeTime();
			Rotation = (float)angle;
			AddValue<float>(time, "Rotation", Rotation.ToString(CultureInfo.InvariantCulture));
		}

		public void Skew(Point screenPoint, Point downPoint, HoverContext context, float zoom)
		{
			float dx = (screenPoint.X - downPoint.X) / zoom;
			float dy = (screenPoint.Y - downPoint.Y) / zoom;
			switch (context)
			{
				case HoverContext.SkewLeft:
					dy = -dy;
					break;
				case HoverContext.SkewRight:
					break;
				case HoverContext.SkewTop:
					dx = -dx;
					break;
			}

			float time = GetRelativeTime();

			//skew formula: shift = size * tan(radians) / 2
			//solved for angle: angle = atan(2 * shift / size)
			if (HoverContext.SkewHorizontal.HasFlag(context))
			{
				//skewX
				float skewX = (float)(Math.Atan(2 * dx / Height) * 180 / Math.PI);
				AddValue<float>(time, "SkewX", skewX.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				//skewY
				float skewY = (float)(Math.Atan(2 * dy / Width) * 180 / Math.PI);
				AddValue<float>(time, "SkewY", skewY.ToString(CultureInfo.InvariantCulture));
			}
		}
		#endregion

		#region Drawing
		public void Update(float time, bool inPlayback)
		{
			Time = time;

			inPlayback = true;
			string easeOverride = (inPlayback ? null : "linear");
			string interpolationOverride = (inPlayback ? null : "linear");
			bool? looped = (inPlayback ? null : new bool?(false));

			string src = GetPropertyValue<string>("Src", time, null, easeOverride, interpolationOverride, looped);
			Image = LiveImageCache.Get(src);
			if (Image != null)
			{
				Width = Image.Width;
				Height = Image.Height;
			}
			else
			{
				Width = 100;
				Height = 100;
			}
			X = GetPropertyValue("X", time, 0.0f, easeOverride, interpolationOverride, looped);
			Y = GetPropertyValue("Y", time, 0.0f, easeOverride, interpolationOverride, looped);
			ScaleX = GetPropertyValue("ScaleX", time, 1.0f, easeOverride, interpolationOverride, looped);
			ScaleY = GetPropertyValue("ScaleY", time, 1.0f, easeOverride, interpolationOverride, looped);
			Alpha = GetPropertyValue("Alpha", time, 100.0f, easeOverride, interpolationOverride, looped);
			Rotation = GetPropertyValue("Rotation", time, 0.0f, easeOverride, interpolationOverride, looped);
			SkewX = GetPropertyValue("SkewX", time, 0f, easeOverride, interpolationOverride, looped);
			SkewY = GetPropertyValue("SkewY", time, 0f, easeOverride, interpolationOverride, looped);
		}

		public void Draw(Graphics g, int displayWidth, int displayHeight, Point offset, float zoom, List<string> markers)
		{
			if (!IsVisible || Hidden) { return; }
			if (!string.IsNullOrEmpty(Marker) && !markers.Contains(Marker)) { return; }

			float alpha = Alpha;
			if (Image != null && alpha > 0)
			{
				int width = Image.Width;
				int height = Image.Height;
				Rectangle bounds = ToScreenRegion(displayWidth, displayHeight, offset, zoom);

				float offsetX = bounds.X + PivotX * bounds.Width;
				float offsetY = bounds.Y + PivotY * bounds.Height;
				if (float.IsNaN(offsetX))
				{
					offsetX = 0;
				}
				if (float.IsNaN(offsetY))
				{
					offsetY = 0;
				}

				g.TranslateTransform(offsetX, offsetY);
				g.RotateTransform(Rotation);
				g.TranslateTransform(-offsetX, -offsetY);

				if ((SkewX == 0 || SkewX % 90 != 0) && (SkewY == 0 || SkewY % 90 != 0))
				{
					float skewedWidth = bounds.Height * (float)Math.Tan(Math.PI / 180.0f * SkewX);
					int skewDistanceX = (int)(skewedWidth / 2);
					float skewedHeight = bounds.Width * (float)Math.Tan(Math.PI / 180.0f * SkewY);
					int skewDistanceY = (int)(skewedHeight / 2);
					Point[] destPts = new Point[] { new Point(bounds.X - skewDistanceX, bounds.Y - skewDistanceY), new Point(bounds.Right - skewDistanceX, bounds.Y + skewDistanceY), new Point(bounds.X + skewDistanceX, bounds.Bottom - skewDistanceY) };

					if (alpha < 100)
					{
						float[][] matrixItems = new float[][] {
							new float[] { 1, 0, 0, 0, 0 },
							new float[] { 0, 1, 0, 0, 0 },
							new float[] { 0, 0, 1, 0, 0 },
							new float[] { 0, 0, 0, alpha / 100.0f, 0 },
							new float[] { 0, 0, 0, 0, 1 }
						};
						ColorMatrix cm = new ColorMatrix(matrixItems);
						ImageAttributes ia = new ImageAttributes();
						ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

						g.DrawImage(Image, destPts, new Rectangle(0, 0, Image.Width, Image.Height), GraphicsUnit.Pixel, ia);
					}
					else
					{
						g.DrawImage(Image, destPts, new Rectangle(0, 0, Image.Width, Image.Height), GraphicsUnit.Pixel);
					}
				}

				g.ResetTransform();
			}
		}

		/// <summary>
		/// Converts an object's bounds to screen space, ensuring that the width and height are positive
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public RectangleF ToAbsScreenRegion(int displayWidth, int displayHeight, Point offset, float zoom)
		{
			RectangleF region = ToScreenRegion(displayWidth, displayHeight, offset, zoom);
			if (region.Width < 0)
			{
				region.X += region.Width;
				region.Width = -region.Width;
			}
			if (region.Height < 0)
			{
				region.Y += region.Height;
				region.Height = -region.Height;
			}
			return region;
		}

		/// <summary>
		/// Converts an object's unscaled bounds to screen space
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public RectangleF ToUnscaledScreenRegion(int displayWidth, int displayHeight, Point offset, float zoom)
		{
			displayHeight = (int)(displayHeight * zoom);
			float x = ScaleToDisplay(X, displayHeight);
			float y = ScaleToDisplay(Y, displayHeight);
			float width = ScaleToDisplay(Width, displayHeight);
			float height = ScaleToDisplay(Height, displayHeight);
			x = (int)(x + displayWidth * 0.5f - width * 0.5f);
			return new RectangleF(offset.X + x, offset.Y + y, width, height);
		}

		public Rectangle ToScreenRegion(int displayWidth, int displayHeight, Point offset, float zoom)
		{
			displayHeight = (int)(displayHeight * zoom);

			//get unscaled bounds in screen space
			float x = ScaleToDisplay(X, displayHeight);
			float y = ScaleToDisplay(Y, displayHeight);
			float width = ScaleToDisplay(Width, displayHeight);
			float height = ScaleToDisplay(Height, displayHeight);
			x = (int)(x + displayWidth * 0.5f - width * 0.5f);

			//translate pivot to origin
			float pivotX = x + PivotX * width;
			float pivotY = y + PivotY * height;
			x -= pivotX;
			y -= pivotY;

			//apply scaling
			float right = x + width;
			x *= ScaleX;
			right *= ScaleX;
			width = right - x;

			float bottom = y + height;
			y *= ScaleY;
			bottom *= ScaleY;
			height = bottom - y;

			//translate back
			x += pivotX;
			y += pivotY;

			return new Rectangle(offset.X + (int)x, offset.Y + (int)y, (int)width, (int)height);
		}

		/// <summary>
		/// Gets the "center" of an object's selection
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Point ToScreenCenter(int displayWidth, int displayHeight, Point offset, float zoom)
		{
			RectangleF bounds = ToScreenRegion(displayWidth, displayHeight, offset, zoom);
			float cx = bounds.X + bounds.Width / 2;
			float cy = bounds.Y + bounds.Height / 2;
			return new Point((int)cx, (int)cy);
		}

		public int ScaleToDisplay(float value, int canvasHeight)
		{
			return (int)Math.Floor(value * canvasHeight / Pose.BaseHeight);
		}
		#endregion
	}
}
