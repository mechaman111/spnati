using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveSprite : LiveAnimatedObject
	{
		public SpriteWidget Widget;

		#region Pose
		public LiveSprite(LiveData data, float time) : this()
		{
			Data = data;
			Length = 1;
			Start = time;
			Id = "New Sprite";
			PivotX = 0.5f;
			PivotY = 0.5f;
			LiveKeyframe startFrame = CreateKeyframe(0);
			startFrame.X = 0;
			startFrame.Y = 0;
			AddKeyframe(startFrame);
			Update(time, 0, false);
		}

		public LiveSprite(LivePose pose, Sprite sprite, float time) : this()
		{
			Data = pose;
			ParentId = sprite.ParentId;
			Marker = sprite.Marker;
			Length = 1;
			Id = sprite.Id;
			Z = sprite.Z;
			Start = time;
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
			AddKeyframe(sprite, 0, false, 0);
			Update(time, 0, false);
		}
		#endregion

		#region Epilogue
		public LiveSprite(LiveSceneSegment scene, Directive directive, Character character, float time) : this()
		{
			CenterX = false;
			DisplayPastEnd = false;
			Data = scene;
			ParentId = directive.ParentId;
			LinkedToEnd = true;
			Length = 1;
			Character = character;
			Id = directive.Id;
			Z = directive.Layer;
			Marker = directive.Marker;
			Start = time;
			if (!string.IsNullOrEmpty(directive.Delay))
			{
				float start;
				float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out start);
				Start += start;
				Length = 1;
			}
			if (!string.IsNullOrEmpty(directive.PivotX))
			{
				float pivot;
				string pivotX = directive.PivotX;
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
			if (!string.IsNullOrEmpty(directive.PivotY))
			{
				float pivot;
				string pivotY = directive.PivotY;
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
			
			AddKeyframe(directive, 0, false, 0);

			if (!string.IsNullOrEmpty(directive.Width))
			{
				int width;
				int.TryParse(directive.Width, NumberStyles.Number, CultureInfo.InvariantCulture, out width);
				WidthOverride = width;
			}
			if (!string.IsNullOrEmpty(directive.Height))
			{
				int height;
				int.TryParse(directive.Height, NumberStyles.Number, CultureInfo.InvariantCulture, out height);
				HeightOverride = height;
			}

			Update(time, 0, false);
		}
		#endregion

		public LiveSprite() : base()
		{

		}

		public override string GetLabel()
		{
			return $"Sprite Settings: {Id}";
		}

		public override Type GetKeyframeType()
		{
			return typeof(LiveSpriteKeyframe);
		}

		protected override void ParseKeyframe(Keyframe kf, bool addBreak, HashSet<string> properties, float time, float origin)
		{
			if (!string.IsNullOrEmpty(kf.X))
			{
				if (time > origin && !AnimatedProperties.Contains("X"))
				{
					AddValue<float>(origin, "X", "0", true);
				}
				AddValue<float>(time, "X", kf.X, addBreak);
				properties.Add("X");
			}
			if (!string.IsNullOrEmpty(kf.Y))
			{
				if (time > origin && !AnimatedProperties.Contains("Y"))
				{
					AddValue<float>(origin, "Y", "0", true);
				}
				AddValue<float>(time, "Y", kf.Y, addBreak);
				properties.Add("Y");
			}
			if (!string.IsNullOrEmpty(kf.Src))
			{
				if (time > origin && !AnimatedProperties.Contains("Src"))
				{
					AddValue<string>(origin, "Src", "", true);
				}
				string src = LiveSceneSegment.FixPath(kf.Src, Character);
				AddValue<string>(time, "Src", src, addBreak);
				properties.Add("Src");
			}
			if (!string.IsNullOrEmpty(kf.Scale))
			{
				kf.ScaleX = kf.Scale;
				kf.ScaleY = kf.Scale;
			}
			if (!string.IsNullOrEmpty(kf.ScaleX))
			{
				if (time > origin && !AnimatedProperties.Contains("ScaleX"))
				{
					AddValue<float>(origin, "ScaleX", "1", true);
				}
				AddValue<float>(time, "ScaleX", kf.ScaleX, addBreak);
				properties.Add("ScaleX");
			}
			if (!string.IsNullOrEmpty(kf.ScaleY))
			{
				if (time > origin && !AnimatedProperties.Contains("ScaleY"))
				{
					AddValue<float>(origin, "ScaleY", "1", true);
				}
				AddValue<float>(time, "ScaleY", kf.ScaleY, addBreak);
				properties.Add("ScaleY");
			}
			if (!string.IsNullOrEmpty(kf.Alpha))
			{
				if (time > origin && !AnimatedProperties.Contains("Alpha"))
				{
					AddValue<float>(origin, "Alpha", "100", true);
				}
				AddValue<float>(time, "Alpha", kf.Alpha, addBreak);
				properties.Add("Alpha");
			}
			if (!string.IsNullOrEmpty(kf.Rotation))
			{
				if (time > origin && !AnimatedProperties.Contains("Rotation"))
				{
					AddValue<float>(origin, "Rotation", "0", true);
				}
				AddValue<float>(time, "Rotation", kf.Rotation, addBreak);
				properties.Add("Rotation");
			}
			if (!string.IsNullOrEmpty(kf.SkewX))
			{
				if (time > origin && !AnimatedProperties.Contains("SkewX"))
				{
					AddValue<float>(origin, "SkewX", "0", true);
				}
				AddValue<float>(time, "SkewX", kf.SkewX, addBreak);
				properties.Add("SkewX");
			}
			if (!string.IsNullOrEmpty(kf.SkewY))
			{
				if (time > origin && !AnimatedProperties.Contains("SkewY"))
				{
					AddValue<float>(origin, "SkewY", "0", true);
				}
				AddValue<float>(time, "SkewY", kf.SkewY, addBreak);
				properties.Add("SkewY");
			}
		}

		protected override void OnUpdate(float time, float offset, string easeOverride, string interpolationOverride, bool? looped, bool inPlayback)
		{
			X = GetPropertyValue("X", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
			Y = GetPropertyValue("Y", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
			string src = GetPropertyValue<string>("Src", time, 0, null, easeOverride, interpolationOverride, looped);
			Src = src;
			Image = LiveImageCache.Get(src);
			ScaleX = GetPropertyValue("ScaleX", time, offset, 1.0f, easeOverride, interpolationOverride, looped);
			ScaleY = GetPropertyValue("ScaleY", time, offset, 1.0f, easeOverride, interpolationOverride, looped);
			Alpha = GetPropertyValue("Alpha", time, offset, 100.0f, easeOverride, interpolationOverride, looped);
			Rotation = GetPropertyValue("Rotation", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
			SkewX = GetPropertyValue("SkewX", time, offset, 0f, easeOverride, interpolationOverride, looped);
			SkewY = GetPropertyValue("SkewY", time, offset, 0f, easeOverride, interpolationOverride, looped);
		}

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			return new SpriteWidget(this, timeline);
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{
			if (!IsVisible || Hidden) { return; }
			if (HiddenByMarker(markers))
			{
				return;
			}

			float alpha = WorldAlpha;
			if (Image != null && alpha > 0)
			{
				g.MultiplyTransform(WorldTransform);

				g.MultiplyTransform(sceneTransform, MatrixOrder.Append);

				//draw
				if ((SkewX == 0 || SkewX % 90 != 0) && (SkewY == 0 || SkewY % 90 != 0))
				{
					float skewedWidth = Height * (float)Math.Tan(Math.PI / 180.0f * SkewX);
					float skewDistanceX = skewedWidth / 2;
					float skewedHeight = Width * (float)Math.Tan(Math.PI / 180.0f * SkewY);
					float skewDistanceY = skewedHeight / 2;
					PointF[] destPts = new PointF[] { new PointF(-skewDistanceX, -skewDistanceY), new PointF(Width - skewDistanceX, skewDistanceY), new PointF(skewDistanceX, Height - skewDistanceY) };

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

				//restore
				g.ResetTransform();
			}
		}

		public override Directive CreateCreationDirective(Scene scene)
		{
			Directive sprite = new Directive()
			{
				Id = Id,
				DirectiveType = "sprite",
				Delay = Start.ToString(CultureInfo.InvariantCulture),
			};
			if (WidthOverride.HasValue)
			{
				sprite.Width = WidthOverride.Value.ToString(CultureInfo.InvariantCulture);
			}
			if (HeightOverride.HasValue)
			{
				sprite.Height = HeightOverride.Value.ToString(CultureInfo.InvariantCulture);
			}

			sprite.PivotX = Math.Round(PivotX * 100, 0).ToString(CultureInfo.InvariantCulture) + "%";
			sprite.PivotY = Math.Round(PivotY * 100, 0).ToString(CultureInfo.InvariantCulture) + "%";

			sprite.Layer = Z;
			sprite.ParentId = ParentId;
			sprite.Marker = Marker;

			if (Keyframes.Count > 0)
			{
				LiveSpriteKeyframe initialFrame = Keyframes[0] as LiveSpriteKeyframe;
				if (!string.IsNullOrEmpty(initialFrame.Src))
				{
					sprite.Src = Scene.FixPath(initialFrame.Src, (Data as LiveSceneSegment).Character);
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
			}

			return sprite;
		}
	}
}
