using System;
using System.Collections.Generic;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using SPNATI_Character_Editor.Controls;
using Desktop;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// A "live" keyframe part of active animation rather than just the static definition behind an animation that comes from XML
	/// </summary>
	public class LiveKeyframe : BindableObject, ILabel
	{
		public static List<string> TrackedProperties;

		public event EventHandler LabelChanged;

		public LiveSprite Sprite;

		[Float(DisplayName = "Time", Key = "time", GroupOrder = 0)]
		public float Time
		{
			get { return Get<float>(); }
			set
			{
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		[FileSelect(DisplayName = "Source", GroupOrder = 10, Key = "src", Description = "Sprite source image")]
		public string Src
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "X", Key = "x", GroupOrder = 15, Minimum = -100000, Maximum = 100000, DecimalPlaces = 0)]
		public float? X
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Y", Key = "y", GroupOrder = 20, Minimum = -100000, Maximum = 100000, DecimalPlaces = 0)]
		public float? Y
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Scale X", GroupOrder = 40, Key = "scalex", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Scale Y", GroupOrder = 45, Key = "scaley", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Slider(DisplayName = "Opacity (0-100)", GroupOrder = 30, Key = "alpha", Description = "Opacity/transparency level")]
		public float? Alpha
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Rotation (deg)", GroupOrder = 50, Key = "rotation", Description = "Sprite rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		public float? Rotation
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew X", GroupOrder = 60, Key = "skewx", Description = "Sprite shearing factor horizontally", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew Y", GroupOrder = 65, Key = "skewx", Description = "Sprite shearing factor vertically", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Properties that should not be interpolated from the previous frame, like being the start of a completely separate animation
		/// </summary>
		public ObservableDictionary<string, bool> InterpolationBreaks
		{
			get { return Get<ObservableDictionary<string, bool>>(); }
			set { Set(value); }
		}

		public LiveKeyframe()
		{
			InterpolationBreaks = new ObservableDictionary<string, bool>();
		}

		public LiveKeyframe(float time) : this()
		{
			Time = time;
		}

		static LiveKeyframe()
		{
			TrackedProperties = new List<string>();
			TrackedProperties.AddRange(new string[] { "Src", "X", "Y", "ScaleX", "ScaleY", "Alpha", "Rotation", "SkewX", "SkewY" });
		}

		public override string ToString()
		{
			return $"{Time}";
		}

		public string GetLabel()
		{
			return $"Keyframe: {Sprite.Id} ({Time}s)";
		}

		/// <summary>
		/// Gets whether this keyframe has any data set in it
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				for (int i = 0; i < TrackedProperties.Count; i++)
				{
					if (HasProperty(TrackedProperties[i]))
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>
		/// Gets how many animated properties are set on this frame
		/// </summary>
		public int PropertyCount
		{
			get
			{
				int count = 0;
				for (int i = 0; i < TrackedProperties.Count; i++)
				{
					if (HasProperty(TrackedProperties[i]))
					{
						count++;
					}
				}
				return count;
			}
		}
	}
}
