using SPNATI_Character_Editor.Controls;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SceneObject : IDisposable
	{
		public ScenePreview PreviewScene;
		public static int NextLayer = 0;
		public Character Character;
		public SceneObject SourceObject;
		public int SortLayer;
		public int Layer;
		public float X;
		public float Y;
		public float WidthPct;
		public float HeightPct;
		public float Width;
		public float Height;
		public float ScaleX = 1;
		public float ScaleY = 1;
		public float SkewX = 0;
		public float SkewY = 0;
		public float Zoom = 1;
		public float Rotation = 0;
		private float _alpha = 100;
		public float Alpha
		{
			get { return _alpha; }
			set
			{
				_alpha = Math.Max(0, Math.Min(100, value));
				Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100.0f * 255), Color.Color);
			}
		}
		public SolidBrush Color = new SolidBrush(System.Drawing.Color.Black);
		public string Id;
		public string Text;
		public string Arrow;
		public float Time;
		public string AlignmentX;
		public string AlignmentY;
		public string Tween;
		public string Ease;
		public float Start;
		public float End;
		public float PivotX;
		public float PivotY;
		public int Index;
		public float Rate = 0;

		/// <summary>
		/// Previous directive or keyframe in the scene that affects this object
		/// </summary>
		public Keyframe LastFrame;
		/// <summary>
		/// Directive or keyframe that this is linked to (i.e. updates to one should reflect on the other)
		/// </summary>
		public Keyframe LinkedFrame;
		/// <summary>
		/// Animation that this belongs to
		/// </summary>
		public SceneAnimation LinkedAnimation;

		public SceneObjectType ObjectType = SceneObjectType.Other;

		public Image Image;
		public string Src;

		public SceneObject() { }

		public virtual SceneObject Copy()
		{
			SceneObject copy = new SceneObject();
			copy.CopyValuesFrom(this);
			return copy;
		}

		public SceneObject(ScenePreview scene, Character character, Directive directive) : this(scene, character, directive.Id, directive.Src, directive.Color)
		{
			PreviewScene = scene;
			LinkedFrame = directive;
			if (directive.DirectiveType == "sprite")
			{
				ObjectType = SceneObjectType.Sprite;
			}
			else if (directive.DirectiveType == "text")
			{
				ObjectType = SceneObjectType.Text;
			}
			else if (directive.DirectiveType == "emitter")
			{
				ObjectType = SceneObjectType.Emitter;
			}

			string width = directive.Width;
			int widthValue = 0;
			var regex = new Regex(@"^(-?\d+)(px|%)?$");

			string height = directive.Height;
			int heightValue = 0;

			int imgWidth = (Image == null ? 1 : Image.Width);
			int imgHeight = (Image == null ? 1 : Image.Height);

			if (!string.IsNullOrEmpty(width))
			{
				Match match = regex.Match(width);
				if (match.Success)
				{
					int.TryParse(match.Groups[1].Value, out widthValue);
				}
				if (width.EndsWith("%"))
				{
					WidthPct = widthValue / 100.0f;
				}
				else
				{
					WidthPct = widthValue / scene.Width;
				}
				if (string.IsNullOrEmpty(height))
				{
					HeightPct = imgHeight / imgWidth * WidthPct * scene.AspectRatio;
				}
			}
			else
			{
				WidthPct = imgWidth / scene.Width;
				if (ObjectType == SceneObjectType.Text)
				{
					WidthPct = 0.2f;
				}
			}
			if (!string.IsNullOrEmpty(height))
			{
				Match match = regex.Match(height);
				if (match.Success)
				{
					int.TryParse(match.Groups[1].Value, out heightValue);
				}

				if (height.EndsWith("%"))
				{
					HeightPct = heightValue / 100.0f;
				}
				else
				{
					HeightPct = heightValue / scene.Height;
				}
				if (string.IsNullOrEmpty(width))
				{
					WidthPct = imgWidth / imgHeight * HeightPct / scene.AspectRatio;
				}
			}
			else
			{
				HeightPct = imgHeight / scene.Height;
			}

			if (ObjectType == SceneObjectType.Text)
			{
				Width = WidthPct * 100;
				Height = HeightPct * 100;
			}
			else
			{
				Width = WidthPct * scene.Width;
				Height = HeightPct * scene.Height;
			}

			Update(directive, scene);

			Arrow = directive.Arrow;
			Text = directive.Text;
			AlignmentX = directive.AlignmentX;
			AlignmentY = directive.AlignmentY;
			PivotX = ParsePivot(directive.PivotX, Width);
			PivotY = ParsePivot(directive.PivotY, Height);
			if (!string.IsNullOrEmpty(directive.Rate))
			{
				float.TryParse(directive.Rate, NumberStyles.Number, CultureInfo.InvariantCulture, out Rate);
			}

			SortLayer = ++NextLayer;
			if (directive.Layer != 0)
			{
				Layer = directive.Layer;
			}
		}

		public static int ParsePivot(string pivot, float size)
		{
			switch (pivot)
			{
				case "left":
				case "top":
					return 0;
				case "right":
				case "bottom":
					return (int)size;
				case "center":
					return (int)(size * 0.5f);
				default:
					if (string.IsNullOrEmpty(pivot))
					{
						return (int)(size * 0.5f);
					}
					return (int)Parse(pivot, size);
			}
		}

		public void ResyncAnimation()
		{
			LinkedAnimation?.Rebuild();
		}

		protected void CopyValuesFrom(SceneObject source)
		{
			PreviewScene = source.PreviewScene;
			Character = source.Character;
			ObjectType = source.ObjectType;
			X = source.X;
			Y = source.Y;
			Width = source.Width;
			Height = source.Height;
			WidthPct = source.WidthPct;
			HeightPct = source.HeightPct;
			ScaleX = source.ScaleX;
			ScaleY = source.ScaleY;
			SkewX = source.SkewX;
			SkewY = source.SkewY;
			PivotX = source.PivotX;
			PivotY = source.PivotY;
			Rotation = source.Rotation;
			Alpha = source.Alpha;
			Color.Color = source.Color.Color;
			Id = source.Id;
			Text = source.Text;
			Arrow = source.Arrow;
			AlignmentX = source.AlignmentX;
			AlignmentY = source.AlignmentY;
			Tween = source.Tween;
			Ease = source.Ease;
			Image = source.Image;
			Zoom = source.Zoom;
			SortLayer = source.SortLayer;
			Layer = source.Layer;
			Rate = source.Rate;
			Src = source.Src;
		}

		public SceneObject(ScenePreview scene, Character character, string id, string imagePath, string color)
		{
			PreviewScene = scene;
			Character = character;
			Id = id;
			if (!string.IsNullOrEmpty(color))
			{
				try
				{
					Color.Color = ColorTranslator.FromHtml(color);
				}
				catch { }
			}
			if (!string.IsNullOrEmpty(imagePath))
			{
				SetImage(imagePath);
			}

			if (Image == null)
			{
				WidthPct = 1;
				HeightPct = 1;
			}
			else
			{
				WidthPct = Image.Width / scene.Width;
				HeightPct = Image.Height / scene.Height;
			}

			Layer = -100;
			Width = WidthPct * scene.Width;
			Height = HeightPct * scene.Height;
		}

		private void SetImage(string src)
		{
			Src = src;
			Image = PreviewScene.Images.Get(src);
		}

		public static float Parse(string str, float sceneSize)
		{
			if (string.IsNullOrEmpty(str))
			{
				return 0;
			}
			var regex = new Regex(@"^(-?\d+)(px|%)?$");
			float value = 0;
			Match match = regex.Match(str);
			if (match.Success)
			{
				float.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
			}
			if (str.EndsWith("%"))
			{
				value = value / 100.0f * sceneSize;
			}
			return value;
		}

		/// <summary>
		/// Runs every frame when previewing
		/// </summary>
		/// <param name="elapsedSec"></param>
		/// <param name="scene"></param>
		public virtual void UpdateTick(float elapsedSec, ScenePreview scene)
		{
		}

		/// <summary>
		/// Sets current values to those of a keyframe
		/// </summary>
		/// <param name="frame"></param>
		public virtual void Update(Keyframe frame, ScenePreview scene)
		{
			if (!string.IsNullOrEmpty(frame.Time))
			{
				float.TryParse(frame.Time, NumberStyles.Float, CultureInfo.InvariantCulture, out Time);
			}
			if (!string.IsNullOrEmpty(frame.ScaleX))
			{
				float.TryParse(frame.ScaleX, NumberStyles.Float, CultureInfo.InvariantCulture, out ScaleX);
			}
			if (!string.IsNullOrEmpty(frame.ScaleY))
			{
				float.TryParse(frame.ScaleY, NumberStyles.Float, CultureInfo.InvariantCulture, out ScaleY);
			}
			if (!string.IsNullOrEmpty(frame.SkewX))
			{
				float.TryParse(frame.SkewX, NumberStyles.Float, CultureInfo.InvariantCulture, out SkewX);
			}
			if (!string.IsNullOrEmpty(frame.SkewY))
			{
				float.TryParse(frame.SkewY, NumberStyles.Float, CultureInfo.InvariantCulture, out SkewY);
			}
			if (!string.IsNullOrEmpty(frame.Rotation))
			{
				float.TryParse(frame.Rotation, NumberStyles.Float, CultureInfo.InvariantCulture, out Rotation);
			}
			if (!string.IsNullOrEmpty(frame.Alpha))
			{
				float a;
				if (float.TryParse(frame.Alpha, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
				{
					Alpha = a;
				}
			}
			if (!string.IsNullOrEmpty(frame.Zoom))
			{
				float.TryParse(frame.Zoom, NumberStyles.Float, CultureInfo.InvariantCulture, out Zoom);
				if (Zoom == 0)
				{
					Zoom = 0.01f;
				}
			}
			if (!string.IsNullOrEmpty(frame.Src) && frame.Src != Src)
			{
				SetImage(frame.Src);
			}
			SetColor(frame);

			Directive directive = frame as Directive;
			if (directive == null || directive.Keyframes.Count == 0)
			{
				if (ObjectType != SceneObjectType.Text)
				{
					//only update X and Y is this is either a keyframe or the directive has no keyframes
					if (!string.IsNullOrEmpty(frame.X) || X == 0)
					{
						X = Parse(frame.X, scene.Width);
					}
					if (!string.IsNullOrEmpty(frame.Y) || Y == 0)
					{
						Y = Parse(frame.Y, scene.Height);
					}
				}
				else
				{
					//text stores the percentage in its values rather than a world-space coordinate
					if (!string.IsNullOrEmpty(frame.X) || X == 0)
					{
						X = Parse(frame.X, 100);
					}
					if (!string.IsNullOrEmpty(frame.Y) || Y == 0)
					{
						Y = Parse(frame.Y, 100);
					}
					if (frame.X == "centered")
					{
						X = 50 - Width / 2;
					}
				}
			}
			if (directive != null)
			{
				Tween = directive.InterpolationMethod;
				Ease = directive.EasingMethod;
			}

			SetColor(frame);
		}

		public virtual void Interpolate(SceneObject last, SceneObject frame, float time, SceneObject lastLast, SceneObject nextNext)
		{
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.X)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.X)))
			{
				X = Interpolate(last.X, frame.X, frame.Tween, time, lastLast.X, nextNext.X);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Y)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Y)))
			{
				Y = Interpolate(last.Y, frame.Y, frame.Tween, time, lastLast.Y, nextNext.Y);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.ScaleX)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.ScaleX)))
			{
				ScaleX = Interpolate(last.ScaleX, frame.ScaleX, frame.Tween, time, lastLast.ScaleX, nextNext.ScaleX);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.ScaleY)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.ScaleY)))
			{
				ScaleY = Interpolate(last.ScaleY, frame.ScaleY, frame.Tween, time, lastLast.ScaleY, nextNext.ScaleY);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.SkewX)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.SkewX)))
			{
				SkewX = Interpolate(last.SkewX, frame.SkewX, frame.Tween, time, lastLast.SkewX, nextNext.SkewX);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.SkewY)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.SkewY)))
			{
				SkewY = Interpolate(last.SkewY, frame.SkewY, frame.Tween, time, lastLast.SkewY, nextNext.SkewY);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Zoom)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Zoom)))
			{
				Zoom = Interpolate(last.Zoom, frame.Zoom, frame.Tween, time, lastLast.Zoom, nextNext.Zoom);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Rotation)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Rotation)))
			{
				Rotation = Interpolate(last.Rotation, frame.Rotation, frame.Tween, time, lastLast.Rotation, nextNext.Rotation);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Alpha)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Alpha)))
			{
				Alpha = Interpolate(last.Alpha, frame.Alpha, frame.Tween, time, lastLast.Alpha, nextNext.Alpha);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Color)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Color)))
			{
				Color.Color = Interpolate(last.Color, frame.Color, frame.Tween, time, lastLast.Color, nextNext.Color);
				Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100 * 255), Color.Color);
			}
			if ((last.LinkedFrame == null || !string.IsNullOrEmpty(last.LinkedFrame.Rate)) && (frame.LinkedFrame == null || !string.IsNullOrEmpty(frame.LinkedFrame.Rate)))
			{
				Rate = Interpolate(last.Rate, frame.Rate, frame.Tween, time, lastLast.Rate, nextNext.Rate);
			}
			if (!string.IsNullOrEmpty(frame.Src))
			{
				string src = "";
				if (time >= 1)
				{
					src = frame.Src;
				}
				else
				{
					src = last.Src;
				}
				if (src != Src)
				{
					SetImage(src);
				}
			}
		}

		public static float Interpolate(float lastValue, float nextValue, string interpolationMode, float t, float lastLastValue, float nextNextValue)
		{
			switch (interpolationMode)
			{
				case "spline":
					float p0 = lastLastValue;
					float p1 = lastValue;
					float p2 = nextValue;
					float p3 = nextNextValue;
					float a = 2 * p1;
					float b = p2 - p0;
					float c = 2 * p0 - 5 * p1 + 4 * p2 - p3;
					float d = -p0 + 3 * p1 - 3 * p2 + p3;
					float p = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
					return p;
				case "none":
					return t >= 1 ? nextValue : lastValue;
				default:
					return (nextValue - lastValue) * t + lastValue;
			}
		}

		protected Color Interpolate(SolidBrush lastValue, SolidBrush nextValue, string interpolationMode, float t, SolidBrush lastLastValue, SolidBrush nextNextValue)
		{
			t = Math.Min(1, Math.Max(0, t));

			float r = Interpolate(lastValue.Color.R, nextValue.Color.R, interpolationMode, t, lastLastValue.Color.R, nextNextValue.Color.R);
			float g = Interpolate(lastValue.Color.G, nextValue.Color.G, interpolationMode, t, lastLastValue.Color.G, nextNextValue.Color.G);
			float b = Interpolate(lastValue.Color.B, nextValue.Color.B, interpolationMode, t, lastLastValue.Color.B, nextNextValue.Color.B);
			return System.Drawing.Color.FromArgb(lastValue.Color.A, (int)r, (int)g, (int)b);
		}

		public override string ToString()
		{
			return Id;
		}

		protected string GetImagePath(string path)
		{
			if (!path.StartsWith("/"))
			{
				return Path.Combine(Config.GetRootDirectory(Character), path);
			}
			return Path.Combine(Config.SpnatiDirectory, path.Substring(1));
		}

		public void SetColor(Keyframe frame)
		{
			if (!string.IsNullOrEmpty(frame.Alpha))
			{
				float a;
				if (float.TryParse(frame.Alpha, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
				{
					Alpha = a;
				}
			}
			if (!string.IsNullOrEmpty(frame.Color))
			{
				try
				{
					Color.Color = ColorTranslator.FromHtml(frame.Color);
				}
				catch { }
			}

			Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100 * 255), Color.Color);
		}

		/// <summary>
		/// Sets the color and alpha values based on either settings in the provided scene, or whatever was most recently set in an earlier scene
		/// </summary>
		/// <param name="epilogue"></param>
		/// <param name="currentScene"></param>
		public void SetColor(Epilogue epilogue, Scene currentScene)
		{
			Alpha = 0;

			bool foundAlpha = false;
			bool foundColor = false;
			if (!string.IsNullOrEmpty(currentScene.FadeOpacity))
			{
				foundAlpha = true;
				float a;
				if (float.TryParse(currentScene.FadeOpacity, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
				{
					Alpha = a;
				}
			}
			if (!string.IsNullOrEmpty(currentScene.FadeColor))
			{
				foundColor = true;
				try
				{
					Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100.0f * 255), ColorTranslator.FromHtml(currentScene.FadeColor));
				}
				catch { }
			}
			if (!foundColor || !foundAlpha)
			{
				int index = epilogue.Scenes.IndexOf(currentScene);
				//work backwards through scenes until both settings are found
				for (int i = index - 1; i >= 0; i--)
				{
					Scene scene = epilogue.Scenes[i];
					for (int j = scene.Directives.Count - 1; j >= 0; j--)
					{
						Directive directive = scene.Directives[j];
						if (directive.DirectiveType == "fade")
						{
							if (directive.Keyframes.Count > 0)
							{
								for (int f = directive.Keyframes.Count - 1; f >= 0; f--)
								{
									Keyframe frame = directive.Keyframes[f];
									if (!foundAlpha && !string.IsNullOrEmpty(frame.Alpha))
									{
										foundAlpha = true;
										float a;
										if (float.TryParse(frame.Alpha, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
										{
											Alpha = a;
										}
									}
									if (!foundColor && !string.IsNullOrEmpty(frame.Color))
									{
										foundColor = true;
										try
										{
											Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100.0f * 255), ColorTranslator.FromHtml(frame.Color));
										}
										catch { }
									}
									if (foundAlpha && foundColor)
									{
										return;
									}
								}
							}
							else
							{
								if (!foundAlpha && !string.IsNullOrEmpty(directive.Alpha))
								{
									foundAlpha = true;
									float a;
									if (float.TryParse(directive.Alpha, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
									{
										Alpha = a;
									}
								}
								if (!foundColor && !string.IsNullOrEmpty(directive.Color))
								{
									foundColor = true;
									try
									{
										Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100.0f * 255), ColorTranslator.FromHtml(directive.Color));
									}
									catch { }
								}
								if (foundAlpha && foundColor)
								{
									return;
								}
							}
						}
					}

					//use the scene settings if a directive didn't set it
					if (!string.IsNullOrEmpty(scene.FadeOpacity))
					{
						foundAlpha = true;
						float a;
						if (float.TryParse(scene.FadeOpacity, NumberStyles.Float, CultureInfo.InvariantCulture, out a))
						{
							Alpha = a;
						}
					}
					if (!string.IsNullOrEmpty(scene.FadeColor))
					{
						foundColor = true;
						try
						{
							Color.Color = System.Drawing.Color.FromArgb((int)(Alpha / 100.0f * 255), ColorTranslator.FromHtml(scene.FadeColor));
						}
						catch { }
					}
					if (foundAlpha && foundColor)
					{
						return;
					}
				}
			}
		}

		public virtual void Dispose()
		{
			Color?.Dispose();
		}

		/// <summary>
		/// Updates the X and Y values of the object and applies those to the provided directive
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="directive"></param>
		/// <returns>Whether the values were actually changed</returns>
		public virtual bool AdjustPosition(int x, int y, ScenePreview scene)
		{
			if (X == x && Y == y)
			{
				return false;
			}
			X = x;
			Y = y;

			LinkedFrame.X = ApplyPosition(x, LinkedFrame.X, (int)scene.Width);
			LinkedFrame.Y = ApplyPosition(y, LinkedFrame.Y, (int)scene.Height);
			ResyncAnimation();
			return true;
		}

		/// <summary>
		/// Gets an updated measurement, preserving whether it was in px or %
		/// </summary>
		/// <param name="value"></param>
		/// <param name="sourceValue"></param>
		/// <param name="sceneValue"></param>
		protected string ApplyPosition(int value, string sourceValue, int sceneValue)
		{
			if (ObjectType == SceneObjectType.Text)
			{
				return value + "%";
			}
			if ((sourceValue != null && sourceValue.EndsWith("%")) || sourceValue == "centered" || ObjectType == SceneObjectType.Text)
			{
				float pct = value / (float)sceneValue;
				int percent = (int)(pct * 100);
				return percent + "%";
			}
			else
			{
				//pixels can just go straight in
				return value.ToString();
			}
		}

		public virtual bool AdjustPivot(Point screenPt, RectangleF worldBounds)
		{
			float xPct = (screenPt.X - worldBounds.X) / worldBounds.Width;
			float yPct = (screenPt.Y - worldBounds.Y) / worldBounds.Height;
			float pivotX = xPct * Width;
			float pivotY = yPct * Height;
			if (pivotX == PivotX && pivotY == PivotY)
			{
				return false;
			}
			PivotX = pivotX;
			PivotY = pivotY;
			LinkedFrame.PivotX = ((int)(xPct * 100)).ToString() + "%";
			LinkedFrame.PivotY = ((int)(yPct * 100)).ToString() + "%";
			return true;
		}

		public virtual bool AdjustSize(Point pt, HoverContext context, ScenePreview scene)
		{
			Directive directive = LinkedFrame as Directive;
			if (directive == null) { return false; }

			switch (context)
			{
				case HoverContext.SizeRight:
					int rt = Math.Max((int)X + 10, pt.X);

					int width = (int)((-2 * rt + 2 * X) / (-ScaleX - 1));
					if (Width == width)
					{
						return false;
					}
					Width = width;
					directive.Width = ApplyPosition((int)Width, directive.Width, (int)scene.Width);
					ResyncAnimation();
					return true;
				case HoverContext.SizeLeft:
					//tricker than adjusting the right side since it involves translating X too

					//first get the target width, which is the same as moving the right side by the amount the left side moved
					int lt = Math.Min((int)(X + Width - 10), pt.X); //new left position
					int dx = (int)(X - lt);
					rt = (int)(X + Width + dx);

					//use formula for SizeRight to get the width now
					width = (int)((-2 * rt + 2 * X) / (-ScaleX - 1));
					if (Width == width)
					{
						return false;
					}

					//now solve for X given the width, scale, and new left where l = X - (w * s) / 2
					int x = (int)((width * ScaleX - width) / 2 + lt);
					Width = width;
					X = x;
					directive.X = ApplyPosition((int)X, directive.X, (int)scene.Width);
					directive.Width = ApplyPosition((int)Width, directive.Width, (int)scene.Width);
					ResyncAnimation();
					return true;
				case HoverContext.SizeBottom:
					int b = Math.Max((int)Y + 10, pt.Y);

					int height = (int)((-2 * b + 2 * Y) / (-ScaleY - 1));
					if (Height == height)
					{
						return false;
					}
					Height = height;
					directive.Height = ApplyPosition((int)Height, directive.Height, (int)scene.Height);
					ResyncAnimation();
					return true;
				case HoverContext.SizeTop:
					//works the same as left

					int t = Math.Min((int)(Y + Height - 10), pt.Y); //new left position
					int dy = (int)(Y - t);
					b = (int)(Y + Height + dy);

					height = (int)((-2 * b + 2 * Y) / (-ScaleY - 1));
					if (Height == height)
					{
						return false;
					}

					int y = (int)((height * ScaleY - height) / 2 + t);
					Height = height;
					Y = y;
					directive.Y = ApplyPosition((int)Y, directive.Y, (int)scene.Height);
					directive.Height = ApplyPosition((int)Height, directive.Height, (int)scene.Height);
					ResyncAnimation();
					return true;
			}
			return false;
		}

		public virtual bool AdjustScale(Point point, Point startPoint, HoverContext context, bool locked)
		{
			bool horizontal = (context & HoverContext.ScaleHorizontal) != 0;
			bool vertical = (context & HoverContext.ScaleVertical) != 0;
			if (locked && horizontal && vertical)
			{
				//if locking X-Y, adjust the target point to use the same distance for both X and Y
				int dx = point.X - startPoint.X;
				int dy = point.Y - startPoint.Y;
				int dist = Math.Min(Math.Abs(dx), Math.Abs(dy));
				point.X = startPoint.X + Math.Sign(dx) * dist;
				point.Y = startPoint.Y + Math.Sign(dy) * dist;
			}

			PointF targetPoint = new PointF(point.X, point.Y);
			PointF sourcePoint = new PointF(X, Y); //unscaled point corresponding to the point being dragged
			if (context.HasFlag(HoverContext.ScaleRight))
			{
				sourcePoint.X = X + Width;
			}
			if (context.HasFlag(HoverContext.ScaleBottom))
			{
				sourcePoint.Y = Y + Height;
			}

			PointF pivot = new PointF(X + PivotX, Y + PivotY);
			//shift pivot to origin

			sourcePoint.X -= pivot.X;
			sourcePoint.Y -= pivot.Y;

			targetPoint.X -= pivot.X;
			targetPoint.Y -= pivot.Y;

			//determine scalar to get reach given point
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

			bool changed = false;
			if (ScaleX != mx && horizontal)
			{
				changed = true;
				ScaleX = mx;
				LinkedFrame.ScaleX = mx.ToString(CultureInfo.InvariantCulture);
			}
			if (ScaleY != my && vertical)
			{
				changed = true;
				ScaleY = my;
				LinkedFrame.ScaleY = my.ToString(CultureInfo.InvariantCulture);
			}
			if (changed)
			{
				ResyncAnimation();
			}

			return changed;
		}

		public bool AdjustRotation(Point point)
		{
			//quick and dirty - just use the angle to look from the point to the center

			float cx = X + Width / 2;
			float cy = Y + Height / 2;

			double angle = Math.Atan2(cy - point.Y, cx - point.X);
			angle = angle * (180 / Math.PI);

			if (Rotation == angle)
			{
				return false;
			}

			Rotation = (float)angle;
			LinkedFrame.Rotation = angle.ToString(CultureInfo.InvariantCulture);
			ResyncAnimation();

			return true;
		}

		public bool AdjustSkew(Point worldPoint, Point downPoint, HoverContext context)
		{
			float dx = (worldPoint.X - downPoint.X);
			float dy = (worldPoint.Y - downPoint.Y);
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

			bool changed = false;

			//skew formula: shift = size * tan(radians) / 2
			//solved for angle: angle = atan(2 * shift / size)
			if (HoverContext.SkewHorizontal.HasFlag(context))
			{
				//skewX
				float skewX = (float)(Math.Atan(2 * dx / Height) * 180 / Math.PI);
				if (SkewX != skewX)
				{
					changed = true;
					SkewX = skewX;
					LinkedFrame.SkewX = skewX.ToString(CultureInfo.InvariantCulture);
				}
			}
			else
			{
				//skewY
				float skewY = (float)(Math.Atan(2 * dy / Width) * 180 / Math.PI);
				if (SkewY != skewY)
				{
					changed = true;
					SkewY = skewY;
					LinkedFrame.SkewY = skewY.ToString(CultureInfo.InvariantCulture);
				}
			}
			return changed;
		}
	}

	public enum SceneObjectType
	{
		Other,
		Sprite,
		Text,
		Keyframe,
		Camera,
		Emitter,
		Particle,
	}
}
