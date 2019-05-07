using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Pose composed of sprites and animations
	/// </summary>
	public class Pose : ICloneable, IComparable<Pose>
	{
		[Text(DisplayName = "ID", GroupOrder = 0)]
		[XmlAttribute("id")]
		public string Id;

		[Float(DisplayName = "Base Height", Key = "baseHeight", GroupOrder = 10, Minimum = 0, Maximum = 50000, DecimalPlaces = 0)]
		[DefaultValue("1400")]
		[XmlAttribute("baseHeight")]
		public string BaseHeight = "1400";

		[XmlIgnore]
		public CharacterImage ImageLink;

		[XmlElement("sprite")]
		public List<Sprite> Sprites = new List<Sprite>();

		[XmlElement("directive")]
		public List<PoseDirective> Directives = new List<PoseDirective>();

		public override string ToString()
		{
			return Id;
		}

		public object Clone()
		{
			Pose pose = MemberwiseClone() as Pose;
			pose.Sprites = new List<Sprite>();
			foreach (Sprite sprite in Sprites)
			{
				Sprite clonedSprite = sprite.Clone() as Sprite;
				pose.Sprites.Add(clonedSprite);
			}

			pose.Directives = new List<PoseDirective>();
			foreach (PoseDirective directive in Directives)
			{
				PoseDirective clonedDirective = directive.Clone() as PoseDirective;
				pose.Directives.Add(clonedDirective);
			}
			return pose;
		}

		public void OnAfterDeserialize()
		{
			foreach (Sprite sprite in Sprites)
			{
				sprite.ScaleX = sprite.ScaleX ?? sprite.Scale;
				sprite.ScaleY = sprite.ScaleY ?? sprite.Scale;
			}
		}

		/// <summary>
		/// Converts a LivePose into a Pose definition
		/// </summary>
		/// <param name="pose"></param>
		public void CreateFrom(LivePose pose)
		{
			Directives.Clear();
			Sprites.Clear();

			Id = pose.Id;
			BaseHeight = pose.BaseHeight.ToString();

			//1. Create Sprites for each LiveSprite's first frame
			foreach (LiveSprite item in pose.Sprites)
			{
				Sprite sprite = new Sprite();
				sprite.Id = item.Id;
				sprite.PivotX = Math.Round(item.PivotX * 100, 0).ToString(CultureInfo.InvariantCulture) + "%";
				sprite.PivotY = Math.Round(item.PivotY * 100, 0).ToString(CultureInfo.InvariantCulture) + "%";
				if (sprite.PivotX == "50%")
				{
					sprite.PivotX = null;
				}
				if (sprite.PivotY == "50%")
				{
					sprite.PivotY = null;
				}
				sprite.Z = item.Z;
				sprite.ParentId = item.ParentId;
				sprite.Marker = item.Marker;
				Sprites.Add(sprite);

				if (item.Start > 0)
				{
					sprite.Delay = item.Start.ToString(CultureInfo.InvariantCulture);
				}

				if (item.Keyframes.Count >= 0)
				{
					LiveKeyframe initialFrame = item.Keyframes[0];
					if (!string.IsNullOrEmpty(initialFrame.Src))
					{
						sprite.Src = initialFrame.Src;
					}
					if (initialFrame.X.HasValue)
					{
						sprite.X = initialFrame.X.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.Y.HasValue)
					{
						sprite.Y = initialFrame.Y.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.ScaleX.HasValue)
					{
						sprite.ScaleX = initialFrame.ScaleX.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.ScaleY.HasValue)
					{
						sprite.ScaleY = initialFrame.ScaleY.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.SkewX.HasValue)
					{
						sprite.SkewX = initialFrame.SkewX.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.SkewY.HasValue)
					{
						sprite.SkewY = initialFrame.SkewY.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.Rotation.HasValue)
					{
						sprite.Rotation = initialFrame.Rotation.Value.ToString(CultureInfo.InvariantCulture);
					}
					if (initialFrame.Alpha.HasValue)
					{
						sprite.Opacity = initialFrame.Alpha.Value.ToString(CultureInfo.InvariantCulture);
					}

					//2. split remainder of keyframes into animation directives of similar settings
					DualKeyDictionary<float, string, PoseDirective> directives = new DualKeyDictionary<float, string, PoseDirective>();
					foreach (string property in LiveKeyframe.TrackedProperties)
					{
						float delay = item.Start;
						if (item.Keyframes.Count > 0)
						{
							for (int i = 0; i < item.Keyframes.Count; i++)
							{
								LiveKeyframe kf = item.Keyframes[i];
								if (kf.HasProperty(property))
								{
									PoseDirective directive;

									AnimatedProperty settings = item.GetAnimationProperties(property);
									string settingsKey = settings.ToKey(kf.Time);
									if (kf.InterpolationBreaks.ContainsKey(property))
									{
										//force a new directive
										delay = item.Start + kf.Time;
									}
									directive = directives.Get(delay, settingsKey);
									if (directive == null)
									{
										directive = new PoseDirective()
										{
											Id = item.Id,
											DirectiveType = "animation",
											Looped = settings.Looped,
											EasingMethod = settings.Ease.GetValue(kf.Time),
											InterpolationMethod = settings.Interpolation.GetValue(kf.Time),
											Marker = item.Marker,
										};
										if (delay > 0)
										{
											directive.Delay = delay.ToString(CultureInfo.InvariantCulture);
										}
										Directives.Add(directive);
										directives.Set(delay, settingsKey, directive);
									}

									string time = (kf.Time + item.Start - delay).ToString(CultureInfo.InvariantCulture);
									Keyframe frame = directive.Keyframes.Find(k => k.Time == time);
									if (frame == null)
									{
										frame = new Keyframe();
										frame.Time = time;
										directive.Keyframes.Add(frame);
										directive.Keyframes.Sort((k1, k2) =>
										{
											return k1.Time.CompareTo(k2.Time);
										});
									}
									switch (property)
									{
										case "Src":
											frame.Src = kf.Src;
											break;
										case "X":
											frame.X = kf.X.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "Y":
											frame.Y = kf.Y.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "Alpha":
											frame.Opacity = kf.Alpha.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "Rotation":
											frame.Rotation = kf.Rotation.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "ScaleX":
											frame.ScaleX = kf.ScaleX.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "ScaleY":
											frame.ScaleY = kf.ScaleY.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "SkewX":
											frame.SkewX = kf.SkewX.Value.ToString(CultureInfo.InvariantCulture);
											break;
										case "SkewY":
											frame.SkewY = kf.SkewY.Value.ToString(CultureInfo.InvariantCulture);
											break;
									}
								}
							}
						}
					}

					//3. cull any directives that only apply things to Time 0
					for (int i = Directives.Count - 1; i >= 0; i--)
					{
						PoseDirective directive = Directives[i];
						if (directive.Keyframes.Count == 1 && directive.Keyframes[0].Time == "0" && (directive.Delay ?? "0") == (sprite.Delay ?? "0"))
						{
							Directives.RemoveAt(i);
						}
					}
				}
				//if (!item.LinkedToEnd)
				//{
				//	//for finite-length sprites, add an anim-break opacity change at the very end
				//	string delay = (item.Start + item.Length).ToString(CultureInfo.InvariantCulture);

				//	//but first get rid of any directives that change opacity starting at that point
				//	for (int i = 0; i < Directives.Count; i++)
				//	{
				//		Directive dir = Directives[i];
				//		if (dir.Id == item.Id && dir.Delay == delay && dir.Opacity == "0")
				//		{
				//			Directives.RemoveAt(i);
				//			break;
				//		}
				//	}

				//	//now add the opacity change
				//	PoseDirective directive = new PoseDirective()
				//	{
				//		Id = item.Id,
				//		DirectiveType = "animation",
				//		Delay = delay
				//	};
				//	directive.Keyframes.Add(new Keyframe() { Time = "0", Opacity = "0" });
				//	Directives.Add(directive);
				//}
			}
		}

		public int CompareTo(Pose other)
		{
			return Id.CompareTo(other.Id);
		}
	}
}
