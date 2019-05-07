using System;
using System.Collections.Generic;
using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using System.Collections.ObjectModel;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class AnimatedProperty : BindableObject, ILabel
	{
		public event EventHandler LabelChanged;

		public string PropertyName
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		[Boolean(DisplayName = "Looping", GroupOrder = 0, Description = "When checked, this property's keyframes will loop indefinitely.")]
		public bool Looped
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public AnimatedValue<string> Ease
		{
			get { return Get<AnimatedValue<string>>(); }
			set { Set(value); }
		}

		public AnimatedValue<string> Interpolation
		{
			get { return Get<AnimatedValue<string>>(); }
			set { Set(value); }
		}

		public AnimatedProperty()
		{
		}

		public AnimatedProperty(string name)
		{
			PropertyName = name;
			Ease = new AnimatedValue<string>();
			Interpolation = new AnimatedValue<string>();
			Interpolation.SetValue(0, name == "Src" ? "none" : "linear");
		}

		public override string ToString()
		{
			return PropertyName;
		}

		public string GetLabel()
		{
			return $"Animation Settings: {PropertyName}";
		}

		public string ToKey(float time)
		{
			return $"{(Looped ? "1" : "")}|{(Ease.GetValue(time) ?? "")}|{(Interpolation.GetValue(time) ?? "")}";
		}
	}

	public class AnimatedValue<T> : BindableObject
	{
		public ObservableCollection<TimedValue<T>> Values
		{
			get { return Get<ObservableCollection<TimedValue<T>>>(); }
			set { Set(value); }
		}

		public AnimatedValue()
		{
			Values = new ObservableCollection<TimedValue<T>>();
		}

		public T GetValue(float time)
		{
			for (int i = Values.Count - 1; i >= 0; i--)
			{
				TimedValue<T> timedValue = Values[i];
				if (i == 0 || timedValue.Time <= time)
				{
					return timedValue.Value;
				}
			}
			return default(T);
		}

		public void SetValue(float time, T value)
		{
			for (int i = 0; i < Values.Count; i++)
			{
				TimedValue<T> current = Values[i];
				if (current.Time == time)
				{
					current.Value = value;
					return;
				}
				else if (current.Time > time)
				{
					Values.Insert(i, new TimedValue<T>(time, value));
					return;
				}
			}
			Values.Add(new TimedValue<T>(time, value));
		}

		public void RemoveValue(float time)
		{
			for (int i = 0; i < Values.Count; i++)
			{
				if (Values[i].Time == time)
				{
					Values.RemoveAt(i);
					break;
				}
			}
		}

		public override string ToString()
		{
			if (Values.Count == 0)
			{
				return null;
			}
			return Values[0].Value?.ToString();
		}
	}

	public class TimedValue<T> : BindableObject
	{
		public float Time
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		public T Value
		{
			get { return Get<T>(); }
			set { Set(value); }
		}

		public TimedValue()
		{
		}

		public TimedValue(float time, T value)
		{
			Time = time;
			Value = value;
		}

		public override string ToString()
		{
			return $"@{Time}s: {Value}";
		}
	}

	/// <summary>
	/// Data container for values of a property across keyframes in a sprite
	/// </summary>
	public class AnimatedPropertyClipboardData : ICommand
	{
		private List<string> _properties = new List<string>();
		private List<Tuple<float, string, object>> _data = new List<Tuple<float, string, object>>();
		private LiveSprite _fromSprite;
		private LiveSprite _sprite;

		public AnimatedPropertyClipboardData(LiveSprite sprite, List<string> properties)
		{
			_properties.AddRange(properties);
			_fromSprite = sprite;
			foreach (LiveKeyframe kf in sprite.Keyframes)
			{
				foreach (string property in properties)
				{
					if (kf.HasProperty(property))
					{
						object value = kf.Get<object>(property);
						_data.Add(new Tuple<float, string, object>(kf.Time, property, value));
					}
				}
			}
		}

		/// <summary>
		/// Applies this data to a sprite
		/// </summary>
		/// <param name="sprite"></param>
		public void Apply(LiveSprite sprite)
		{
			_sprite = sprite;
			Do();
		}

		public void Do()
		{
			foreach (Tuple<float, string, object> tuple in _data)
			{
				float time = tuple.Item1;
				string property = tuple.Item2;
				object value = tuple.Item3;
				LiveKeyframe frame = _sprite.Keyframes.Find(kf => kf.Time == time);
				if (frame == null)
				{
					frame = _sprite.AddKeyframe(time);
				}
				frame.Set(value, property);
			}

			foreach (string property in _properties)
			{
				AnimatedProperty fromProp = _fromSprite.GetAnimationProperties(property);
				AnimatedProperty toProp = _sprite.GetAnimationProperties(property);
				fromProp.CopyPropertiesInto(toProp);
			}
		}

		public void Undo()
		{
			throw new NotImplementedException();
		}
	}
}
