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

		[XmlElement("sprite")]
		public List<Sprite> Sprites = new List<Sprite>();

		[XmlElement("directive")]
		public List<PoseDirective> Directives = new List<PoseDirective>();

		public override string ToString()
		{
			if (string.IsNullOrEmpty(Id))
			{
				return "???";
			}
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

				if (item.Keyframes.Count > 0)
				{
					LiveSpriteKeyframe initialFrame = item.Keyframes[0] as LiveSpriteKeyframe;
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
						sprite.Alpha = initialFrame.Alpha.Value.ToString(CultureInfo.InvariantCulture);
					}

					//2. split remainder of keyframes into animation directives of similar settings
					DualKeyDictionary<float, string, PoseDirective> directives = new DualKeyDictionary<float, string, PoseDirective>();
					foreach (string property in initialFrame.TrackedProperties)
					{
						float delay = item.Start;
						bool foundFirst = false;
						for (int i = 0; i < item.Keyframes.Count; i++)
						{
							LiveSpriteKeyframe kf = item.Keyframes[i] as LiveSpriteKeyframe;
							if (!kf.HasProperty(property)) { continue; }
							if (!foundFirst)
							{
								foundFirst = true;
								delay += kf.Time;
							}
							LiveKeyframe blockStart = item.GetBlockKeyframe(property, kf.Time);
							LiveKeyframeMetadata metadata = blockStart.GetMetadata(property, false);
							string metaKey = metadata.ToKey();
							KeyframeType frameType = kf.GetFrameType(property);
							if (frameType != KeyframeType.Normal)
							{
								if (frameType == KeyframeType.Split)
								{
									//for a split, we need a key frame to conclude this animation and use the same thing at the start of the next animation, so create an extra
									LiveKeyframeMetadata previousMetadata = item.GetBlockMetadata(property, kf.Time - 0.001f);
									string previousMetaKey = previousMetadata.ToKey();
									CreateKeyFrame(item, directives, property, delay, kf, previousMetadata, previousMetaKey);
								}

								//force a new directive
								delay = item.Start + kf.Time;
							}
							CreateKeyFrame(item, directives, property, delay, kf, metadata, metaKey);
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
						else if (directive.Keyframes.Count > 1)
						{
							directive.Keyframes.Sort((k1, k2) =>
							{
								float t1;
								float t2;
								float.TryParse(k1.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out t1);
								float.TryParse(k2.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out t2);
								return t1.CompareTo(t2);
							});
						}
					}
				}
			}
		}

		private void CreateKeyFrame(LiveSprite item, DualKeyDictionary<float, string, PoseDirective> directives, string property, float delay, LiveSpriteKeyframe kf, LiveKeyframeMetadata metadata, string metaKey)
		{
			PoseDirective directive = directives.Get(delay, metaKey);
			if (directive == null)
			{
				directive = new PoseDirective()
				{
					Id = item.Id,
					DirectiveType = "animation",
					Looped = metadata.Looped,
					Iterations = metadata.Iterations,
					ClampingMethod = metadata.ClampMethod,
					EasingMethod = metadata.Ease,
					InterpolationMethod = metadata.Interpolation,
					Marker = item.Marker,
				};
				if (delay > 0)
				{
					directive.Delay = delay.ToString(CultureInfo.InvariantCulture);
				}
				Directives.Add(directive);
				directives.Set(delay, metaKey, directive);
			}

			string time = (kf.Time + item.Start - delay).ToString(CultureInfo.InvariantCulture);
			Keyframe frame = directive.Keyframes.Find(k => k.Time == time);
			if (frame == null)
			{
				frame = new Keyframe();
				frame.Time = time;
				bool added = false;
				for (int i = 0; i < directive.Keyframes.Count; i++)
				{
					Keyframe other = directive.Keyframes[i];
					if (other.Time.CompareTo(time) > 0)
					{
						directive.Keyframes.Insert(i, frame);
						added = true;
						break;
					}
				}
				if (!added)
				{
					directive.Keyframes.Add(frame);
				}
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
					frame.Alpha = kf.Alpha.Value.ToString(CultureInfo.InvariantCulture);
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

		public int CompareTo(Pose other)
		{
			return Id.CompareTo(other.Id);
		}
	}
}
