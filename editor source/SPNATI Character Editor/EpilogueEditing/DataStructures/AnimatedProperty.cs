using System;
using System.Collections.Generic;
using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;

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

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupOrder = 20, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-in-sin", "ease-in-cubic", "ease-out", "ease-out-sin", "ease-out-cubic", "ease-in-out-cubic", "bounce", "elastic" })]
		public string Ease
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[ComboBox(DisplayName = "Tweening Function", Key = "tween", GroupOrder = 25, Description = "Tweening function for how positions between keyframes are computed", Options = new string[] { "linear", "spline", "none" })]
		public string Interpolation
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public AnimatedProperty()
		{
		}

		public AnimatedProperty(string name)
		{
			PropertyName = name;
			Interpolation = name == "Src" ? "none" : "linear";
		}

		public override string ToString()
		{
			return PropertyName;
		}

		public string GetLabel()
		{
			return $"Animation Settings: {PropertyName}";
		}

		public string ToKey()
		{
			return $"{(Looped ? "1" : "")}|{(Ease ?? "")}|{(Interpolation ?? "")}";
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
