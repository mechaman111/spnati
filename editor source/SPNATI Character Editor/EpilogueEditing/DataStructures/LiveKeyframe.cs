using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// A "live" keyframe part of active animation rather than just the static definition behind an animation that comes from XML
	/// </summary>
	public class LiveKeyframe : BindableObject, ILabel
	{
		public event EventHandler LabelChanged;

		public LiveAnimatedObject Data;

		[Float(DisplayName = "Time", Key = "time", GroupOrder = 0, Increment = 0.1f)]
		public float Time
		{
			get { return Get<float>(); }
			set
			{
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public HashSet<string> TrackedProperties { get; }

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

		/// <summary>
		/// Data specific to a single property within the keyframe
		/// </summary>
		public ObservableDictionary<string, LiveKeyframeMetadata> PropertyMetadata
		{
			get { return Get<ObservableDictionary<string, LiveKeyframeMetadata>>(); }
			set { Set(value); }
		}

		public LiveKeyframe()
		{
			PropertyMetadata = new ObservableDictionary<string, LiveKeyframeMetadata>();
			TrackedProperties = new HashSet<string>();
			TrackedProperties.Add("X");
			TrackedProperties.Add("Y");
		}

		public virtual bool FilterRecord(PropertyRecord record)
		{
			if (record.Key == "time" && Time == 0)
			{
				return false;
			}
			return true;
		}

		public KeyframeType GetFrameType(string property)
		{
			if (string.IsNullOrEmpty(property)) { return KeyframeType.Normal; }
			LiveKeyframeMetadata metadata = GetMetadata(property, false);
			if (metadata == null) { return KeyframeType.Normal; }
			return metadata.FrameType;
		}

		public override string ToString()
		{
			return $"{Time}";
		}

		public string GetLabel()
		{
			if (Data == null) { return "???"; }
			return $"Keyframe: {Data.Id} ({Time}s)";
		}

		/// <summary>
		/// Gets whether this keyframe has any data set in it
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				foreach (string propName in TrackedProperties)
				{
					if (HasProperty(propName))
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
				foreach (string propName in TrackedProperties)
				{
					if (HasProperty(propName))
					{
						count++;
					}
				}
				return count;
			}
		}

		public LiveKeyframeMetadata GetMetadata(string property, bool addIfNew)
		{
			if (!PropertyMetadata.ContainsKey(property))
			{
				if (addIfNew)
				{
					LiveKeyframeMetadata metadata = new LiveKeyframeMetadata(property);
					PropertyMetadata.Add(property, metadata);
					return metadata;
				}
				else
				{
					return new LiveKeyframeMetadata(property);
				}
			}
			return PropertyMetadata[property];
		}

		public bool IsDefault(string property)
		{
			object value = Get<object>(property);
			object defaultValue = GetDefaultValue(property);

			Type propertyType = Nullable.GetUnderlyingType(PropertyTypeInfo.GetType(this.GetType(), property));
			if (propertyType == typeof(float))
			{
				return (float)value == (float)defaultValue;
			}
			else if (propertyType == typeof(int))
			{
				return (int)value == (int)defaultValue;
			}
			else
			{
				return value.Equals(defaultValue);
			}
		}

		protected virtual object GetDefaultValue(string property)
		{
			Type mainType = PropertyTypeInfo.GetType(GetType(), property);
			Type propertyType = Nullable.GetUnderlyingType(mainType) ?? mainType;
			if (propertyType == typeof(int))
			{
				return 0;
			}
			else if (propertyType == typeof(float))
			{
				return 0.0f;
			}
			else if (propertyType == typeof(string))
			{
				return null;
			}
			else if (propertyType == typeof(Color))
			{
				return Color.Black;
			}
			else
			{
				throw new NotSupportedException($"Unsupported frame property type for property {property}: {propertyType.Name}");
			}
		}
	}
}
