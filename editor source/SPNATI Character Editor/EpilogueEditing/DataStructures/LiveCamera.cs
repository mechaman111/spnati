using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveCamera : LiveAnimatedObject
	{
		public float Zoom
		{
			get { return Get<float>(); }
			set
			{
				value = (float)Math.Round(value, 2);
				if (value <= 0)
				{
					value = 0.01f;
				}
				Set(value);
				InvalidateTransform();
			}
		}

		public Color Color
		{
			get { return Get<Color>(); }
			set { Set(value); }
		}

		public bool BlockOutsideView
		{
			get; set;
		}

		private Pen _penLens;
		private Brush _outsideBrush;

		public LiveCamera()
		{
			Id = "Camera";
			Zoom = 1;
			Color = Color.Black;
			Alpha = 0;
			Length = 1;
			LinkedToEnd = true;

			_penLens = new Pen(Brushes.LightGray, 5);
			_penLens.Color = Color.FromArgb(127, _penLens.Color);

			_outsideBrush = new SolidBrush(Color.FromArgb(80, 0, 10, 30));

			AddValue<float>(0, "X", "0");
			AddValue<float>(0, "Y", "0");
			AddValue<float>(0, "Zoom", "1");
		}

		public LiveCamera(Scene scene) : this()
		{
			if (!string.IsNullOrEmpty(scene.Width))
			{
				int width;
				if (int.TryParse(scene.Width, NumberStyles.Number, CultureInfo.InvariantCulture, out width))
				{
					Width = width;
				}
			}
			if (!string.IsNullOrEmpty(scene.Height))
			{
				int height;
				if (int.TryParse(scene.Height, NumberStyles.Number, CultureInfo.InvariantCulture, out height))
				{
					Height = height;
				}
			}
			Id = "Camera";
			PopulateSceneAttributes(scene);
			Update(0, 0, false);
		}

		protected override HashSet<string> GetLoopableProperties(string sourceProperty)
		{
			HashSet<string> props = new HashSet<string>();
			if (sourceProperty == "Alpha" || sourceProperty == "Color")
			{
				props.Add("Color");
				props.Add("Alpha");
			}
			else
			{
				props.Add("X");
				props.Add("Y");
				props.Add("Zoom");
			}
			return props;
		}

		public void PopulateSceneAttributes(Scene scene)
		{
			AddValue<float>(0, "X", scene.X);
			AddValue<float>(0, "Y", scene.Y);
			AddValue<float>(0, "Zoom", scene.Zoom);
			AddValue<float>(0, "Alpha", scene.FadeOpacity);
			AddValue<Color>(0, "Color", scene.FadeColor);
		}

		protected override void OnCopyTo(LiveObject copy)
		{
			copy.Width = Width;
			copy.Height = Height;
			LiveCamera cameraCopy = copy as LiveCamera;
			cameraCopy.BlockOutsideView = BlockOutsideView;
		}

		public override Type GetKeyframeType()
		{
			return typeof(LiveCameraKeyframe);
		}

		protected override void ParseKeyframe(Keyframe kf, bool addBreak, HashSet<string> properties, float time, float origin)
		{
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
			if (!string.IsNullOrEmpty(kf.Zoom))
			{
				AddValue<float>(time, "Zoom", kf.Zoom, addBreak);
				properties.Add("Zoom");
			}
			if (!string.IsNullOrEmpty(kf.Alpha))
			{
				AddValue<float>(time, "Alpha", kf.Alpha, addBreak);
				properties.Add("Alpha");
			}
			if (!string.IsNullOrEmpty(kf.Color))
			{
				AddValue<Color>(time, "Color", kf.Color, addBreak);
				properties.Add("Color");
			}
		}

		public override Directive AddToScene(Scene scene)
		{
			if (Keyframes.Count == 0)
			{
				return null;
			}
			//the camera has no creation directive - they go directly on the scene
			LiveCameraKeyframe firstFrame = Keyframes[0] as LiveCameraKeyframe;
			scene.X = firstFrame.X?.ToString(CultureInfo.InvariantCulture);
			scene.Y = firstFrame.Y?.ToString(CultureInfo.InvariantCulture);
			scene.Zoom = firstFrame.Zoom?.ToString(CultureInfo.InvariantCulture);
			scene.FadeOpacity = firstFrame.Alpha?.ToString(CultureInfo.InvariantCulture);
			scene.FadeColor = firstFrame.Color.A > 0 ? firstFrame.Color.ToHexValue() : null;
			return null;
		}

		protected override void OnUpdateDimensions() { }

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			return new CameraWidget(this, timeline);
		}

		protected override void OnUpdate(float time, float offset, string ease, string interpolation, bool? looped, bool inPlayback)
		{
			X = GetPropertyValue("X", time, offset, 0.0f, ease, interpolation, looped);
			Y = GetPropertyValue("Y", time, offset, 0.0f, ease, interpolation, looped);
			Zoom = GetPropertyValue("Zoom", time, offset, 1.0f, ease, interpolation, looped);
			Alpha = GetPropertyValue("Alpha", time, offset, 0.0f, ease, interpolation, looped);
			Color = GetPropertyValue("Color", time, offset, Color.Black, ease, interpolation, looped);
		}

		protected override Matrix UpdateLocalTransform()
		{
			Matrix transform = new Matrix();
			float pivotX = 0.5f * Width;
			float pivotY = 0.5f * Height;
			transform.Translate(-pivotX, -pivotY, MatrixOrder.Append);
			transform.Scale(1 / Zoom, 1 / Zoom, MatrixOrder.Append);
			transform.Translate(pivotX, pivotY, MatrixOrder.Append);

			transform.Translate(X, Y, MatrixOrder.Append); //local position
			return transform;
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{
			PointF[] bounds = ToScreenPt(sceneTransform, new PointF(0, 0), new PointF(Width, Height));

			PointF tl = bounds[0];
			PointF br = new PointF(bounds[1].X, bounds[1].Y);
			PointF tr = new PointF(bounds[1].X, bounds[0].Y);
			PointF bl = new PointF(bounds[0].X, bounds[1].Y);

			int width = (int)g.ClipBounds.Width;
			int height = (int)g.ClipBounds.Height;

			Brush outerBrush = BlockOutsideView ? Brushes.Black : _outsideBrush;
			g.FillRectangle(outerBrush, 0, 0, bounds[0].X, height);
			g.FillRectangle(outerBrush, bounds[1].X, 0, width - bounds[1].X, height);
			g.FillRectangle(outerBrush, bounds[0].X, 0, bounds[1].X - bounds[0].X, bounds[0].Y);
			g.FillRectangle(outerBrush, bounds[0].X, bounds[1].Y, bounds[1].X - bounds[0].X, height - bounds[1].Y);

			//Fade overlay
			if (Alpha > 0)
			{
				using (SolidBrush overlay = new SolidBrush(Color.FromArgb((int)(Alpha / 100.0 * 255), Color)))
				{
					g.FillRectangle(overlay, 0, 0, width, height);
				}
			}

			if (!BlockOutsideView)
			{
				g.DrawLine(Pens.Gray, tl, bl);
				g.DrawLine(Pens.Gray, tl, tr);
				g.DrawLine(Pens.Gray, tr, br);
				g.DrawLine(Pens.Gray, bl, br);

				//corners
				const int CornerSize = 50;
				float halfWidth = _penLens.Width / 2;
				g.DrawLine(_penLens, new PointF(tl.X, tl.Y - halfWidth), new PointF(tl.X - CornerSize, tl.Y - halfWidth));
				g.DrawLine(_penLens, new PointF(tl.X - halfWidth, tl.Y), new PointF(tl.X - halfWidth, tl.Y - CornerSize));

				g.DrawLine(_penLens, new PointF(tr.X, tl.Y - halfWidth), new PointF(tr.X + CornerSize, tl.Y - halfWidth));
				g.DrawLine(_penLens, new PointF(tr.X + halfWidth, tl.Y), new PointF(tr.X + halfWidth, tl.Y - CornerSize));

				g.DrawLine(_penLens, new PointF(bl.X, bl.Y + halfWidth), new PointF(bl.X - CornerSize, bl.Y + halfWidth));
				g.DrawLine(_penLens, new PointF(bl.X - halfWidth, bl.Y), new PointF(bl.X - halfWidth, bl.Y + CornerSize));

				g.DrawLine(_penLens, new PointF(br.X, br.Y + halfWidth), new PointF(br.X + CornerSize, br.Y + halfWidth));
				g.DrawLine(_penLens, new PointF(br.X + halfWidth, br.Y), new PointF(br.X + halfWidth, br.Y + CornerSize));
			}
		}

		public override bool CanRotate { get { return false; } }
		public override bool CanSkew { get { return false; } }
		public override bool CanPivot { get { return false; } }
		public override bool CanTranslate { get { return !BlockOutsideView; } }
		public override bool CanScale { get { return !BlockOutsideView; } }

		public override void Scale(Point screenPoint, Matrix sceneTransform, HoverContext context)
		{
			float time = GetRelativeTime();
			bool horizontal = (context & HoverContext.ScaleHorizontal) != 0;
			bool vertical = (context & HoverContext.ScaleVertical) != 0;

			//scale is determined by first converting point to local space
			PointF localPt = ToLocalPt(sceneTransform, screenPoint)[0];
			PointF pivotPt = new PointF(0.5f * Width, 0.5f * Height);

			float scaleX = Zoom;
			float scaleY = Zoom;

			if (context.HasFlag(HoverContext.ScaleRight))
			{
				float scaledDist = Width - pivotPt.X;
				float distFromPivot = localPt.X - pivotPt.X;
				float unscaledDist = scaledDist * Zoom;
				scaleX = unscaledDist / distFromPivot;
			}
			else if (context.HasFlag(HoverContext.ScaleLeft))
			{
				float scaledDist = pivotPt.X;
				float distFromPivot = pivotPt.X - localPt.X;
				float unscaledDist = scaledDist * Zoom;
				scaleX = unscaledDist / distFromPivot;
			}
			if (context.HasFlag(HoverContext.ScaleBottom))
			{
				float scaledDist = Height - pivotPt.Y;
				float distFromPivot = localPt.Y - pivotPt.Y;
				float unscaledDist = scaledDist * Zoom;
				scaleY = unscaledDist / distFromPivot;
			}
			else if (context.HasFlag(HoverContext.ScaleTop))
			{
				float scaledDist = pivotPt.Y;
				float distFromPivot = pivotPt.Y - localPt.Y;
				float unscaledDist = scaledDist * Zoom;
				scaleY = unscaledDist / distFromPivot;
			}
			if (horizontal && !float.IsInfinity(scaleX))
			{
				scaleX = (float)Math.Round(scaleX, 2);
				if (scaleX == 0)
				{
					scaleX = 0.01f;
				}
				if (scaleX != Zoom)
				{
					AddValue<float>(time, "Zoom", scaleX.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (vertical && !float.IsInfinity(scaleY))
			{
				scaleY = (float)Math.Round(scaleY, 2);
				if (scaleY == 0)
				{
					scaleY = 0.01f;
				}
				if (scaleY != Zoom)
				{
					AddValue<float>(time, "Zoom", scaleY.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		public override bool FilterRecord(string key)
		{
			switch (key)
			{
				case "pivotx":
				case "pivoty":
				case "z":
				case "id":
				case "marker":
					return false;
				default:
					return true;
			}
		}

		public override Directive CreateCreationDirective(Scene scene)
		{
			if (Keyframes.Count > 0)
			{
				UpdateHistory(this, Keyframes[0]);
			}
			return null;
		}
	}
}
