using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Xml.Serialization;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Controls.EditControls;
using SPNATI_Character_Editor.EditControls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Editable speech bubble
	/// </summary>
	public class LiveBubble : LiveAnimatedObject
	{
		private const int Padding = 5;

		private static Font _font;
		private static StringFormat _stringFormat;
		private static Graphics _graphics;
		private static Pen _alignmentPen;

		static LiveBubble()
		{
			_font = new Font("Trebuchet MS", 14);
			_stringFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
			_graphics = Graphics.FromImage(new Bitmap(1, 1));
			_alignmentPen = new Pen(Color.FromArgb(127, 255, 255, 255), 2);
		}

		public LiveBubble()
		{
			DisplayPastEnd = false;
			CenterX = false;
		}
		public LiveBubble(LiveData data, float time) : this()
		{
			Data = data;
			Length = 1;
			Start = time;
			LiveBubbleKeyframe startFrame = CreateKeyframe(0) as LiveBubbleKeyframe;
			startFrame.Text = "New text";
			TextWidth = "20%";
			AddKeyframe(startFrame);
			Update(time, 0, false);
			UpdateLocalTransform();
		}

		public LiveBubble(LiveData data, Directive directive, float time) : this()
		{
			DisplayPastEnd = false;
			Data = data;
			Length = 1;

			TextX = directive.X;
			TextY = directive.Y;
			TextWidth = directive.Width;
			Id = directive.Id;

			LiveKeyframe temp;
			AddKeyframe(directive, 0, false, out temp);
			Update(time, 0, false);
		}

		public string Text
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Measurement(DisplayName = "X", Key = "x", GroupOrder = 5, Description = "Speech bubble left position to the screen width")]
		public string TextX
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Measurement(DisplayName = "Y", Key = "y", GroupOrder = 10, Description = "Speech bubble top position to the screen height")]
		public string TextY
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Measurement(DisplayName = "Width", Key = "width", GroupOrder = 20, Description = "Speech bubble width relative to the screen width")]
		public string TextWidth
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[ComboBox(DisplayName = "Arrow", Key = "arrow", GroupOrder = 30, Description = "Speech bubble arrow direction", Options = new string[] { "down", "up", "left", "right" })]
		public string Arrow
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[HorizontalAlignment(DisplayName = "X Alignment", Key = "alignmentx", GroupOrder = 31, Description = "Horizontal alignment")]
		public string AlignmentX
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[VerticalAlignment(DisplayName = "Y Alignment", Key = "alignmenty", GroupOrder = 32, Description = "Vertical alignment")]
		[XmlAttribute("alignmenty")]
		public string AlignmentY
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		protected override void OnUpdateDimensions() { }

		public override Type GetKeyframeType()
		{
			return typeof(LiveBubbleKeyframe);
		}

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			return new TextWidget(this, timeline);
		}

		protected override void ParseKeyframe(Keyframe kf, bool addBreak, HashSet<string> properties, float time)
		{
			Directive dir = kf as Directive;
			if (dir != null)
			{
				if (!string.IsNullOrEmpty(dir.Text))
				{
					AddValue<string>(time, "Text", dir.Text);
					properties.Add("Text");
				}
			}
		}

		protected override void OnUpdate(float time, float offset, string ease, string interpolation, bool? looped, bool inPlayback)
		{
			Text = GetPropertyValue<string>("Text", time, offset, null, "linear", "none", false);
		}

		public bool Contains(Point pt, Matrix sceneTransform)
		{
			RectangleF rect = GetRectangle();
			return rect.Contains(pt);
		}

		private RectangleF GetRectangle()
		{
			LiveScene scene = Data as LiveScene;
			float width = EpilogueEditing.SceneObject.Parse(TextWidth, scene.Width);
			float x = EpilogueEditing.SceneObject.Parse(TextX, scene.Width);
			if (TextX == "centered")
			{
				x = scene.Width / 2 - width / 2;
			}
			float y = EpilogueEditing.SceneObject.Parse(TextY, scene.Height);

			SizeF size = _graphics.MeasureString(Text, _font, (int)(width - Padding * 2), _stringFormat);
			float height = size.Height + Padding * 2;

			X = x;
			Y = y;

			if (!string.IsNullOrEmpty(AlignmentX) && AlignmentX != "left" && TextX != "centered")
			{
				if (AlignmentX == "center")
				{
					x -= width / 2;
				}
				else if (AlignmentX == "right")
				{
					x -= width;
				}
			}
			if (!string.IsNullOrEmpty(AlignmentY) && AlignmentY != "top")
			{
				if (AlignmentY == "center")
				{
					y -= height / 2;
				}
				else if (AlignmentY == "bottom")
				{
					y -= height;
				}
			}

			Width = (int)width;
			Height = (int)height;

			return new RectangleF(x, y, width, height);
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{
			if (!IsVisible || Hidden) { return; }
			if (HiddenByMarker(markers))
			{
				return;
			}

			RectangleF bounds = GetRectangle();

			//alignment grid
			if (!inPlayback)
			{
				float gridX = bounds.X;
				if (AlignmentX == "center")
				{
					gridX = bounds.X + bounds.Width / 2;
				}
				else if (AlignmentX == "right")
				{
					gridX = bounds.Right;
				}
				float gridY = bounds.Y;
				if (AlignmentY == "center")
				{
					gridY = bounds.Y + bounds.Height / 2;
				}
				else if (AlignmentY == "bottom")
				{
					gridY = bounds.Bottom;
				}
				g.DrawLine(_alignmentPen, bounds.X - 20, gridY, bounds.Right + 20, gridY);
				g.DrawLine(_alignmentPen, gridX, bounds.Y - 20, gridX, bounds.Bottom + 20);
			}

			g.FillRectangle(Brushes.White, bounds);
			g.DrawString(Text, _font, Brushes.Black, new RectangleF(bounds.X, bounds.Y + Padding, bounds.Width, bounds.Height - Padding * 2), _stringFormat);
			g.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height);

			if (!string.IsNullOrEmpty(Arrow))
			{
				DrawArrow(g, bounds, Arrow, true);
			}
		}

		private void DrawArrow(Graphics g, RectangleF bounds, string side, bool opaque)
		{
			using (SolidBrush fillBrush = new SolidBrush(opaque ? Color.White : Color.FromArgb(127, Color.White)))
			{
				const int ArrowSize = 16;
				if (side == "down")
				{
					float center = bounds.X + bounds.Width / 2;
					float y = bounds.Y + bounds.Height - 1;
					PointF p1 = new PointF(center - ArrowSize, y);
					PointF p2 = new PointF(center + ArrowSize, y);
					PointF p3 = new PointF(center, y + ArrowSize);
					PointF[] triangle = new PointF[] { p1, p2, p3 };
					g.FillPolygon(fillBrush, triangle);
					g.DrawLine(Pens.Black, p1, p3);
					g.DrawLine(Pens.Black, p2, p3);
				}
				if (side == "up")
				{
					float center = bounds.X + bounds.Width / 2;
					float y = bounds.Y + 1;
					PointF p1 = new PointF(center + ArrowSize, y);
					PointF p2 = new PointF(center - ArrowSize, y);
					PointF p3 = new PointF(center, y - ArrowSize);
					PointF[] triangle = new PointF[] { p1, p2, p3 };
					g.FillPolygon(fillBrush, triangle);
					g.DrawLine(Pens.Black, p1, p3);
					g.DrawLine(Pens.Black, p2, p3);
				}
				if (side == "left")
				{
					float center = bounds.Y + bounds.Height / 2;
					float x = bounds.X + 1;
					PointF p1 = new PointF(x, center - ArrowSize);
					PointF p2 = new PointF(x, center + ArrowSize);
					PointF p3 = new PointF(x - ArrowSize, center);
					PointF[] triangle = new PointF[] { p1, p2, p3 };
					g.FillPolygon(fillBrush, triangle);
					g.DrawLine(Pens.Black, p1, p3);
					g.DrawLine(Pens.Black, p2, p3);
				}
				if (side == "right")
				{
					float center = bounds.Y + bounds.Height / 2;
					float x = bounds.X + bounds.Width - 1;
					PointF p1 = new PointF(x, center + ArrowSize);
					PointF p2 = new PointF(x, center - ArrowSize);
					PointF p3 = new PointF(x + ArrowSize, center);
					PointF[] triangle = new PointF[] { p1, p2, p3 };
					g.FillPolygon(fillBrush, triangle);
					g.DrawLine(Pens.Black, p1, p3);
					g.DrawLine(Pens.Black, p2, p3);
				}
			}
		}

		public override void DrawSelection(Graphics g, Matrix sceneTransform, CanvasState editState, HoverContext hoverContext)
		{
			LiveScene scene = Data as LiveScene;
			if (scene != null)
			{
				g.MultiplyTransform(scene.Camera.WorldTransform);
				g.MultiplyTransform(sceneTransform, MatrixOrder.Append);
				RectangleF rect = GetRectangle();
				DrawSelectionBox(g,
					new PointF(rect.Left, rect.Top),
					new PointF(rect.Right, rect.Top),
					new PointF(rect.Right, rect.Bottom),
					new PointF(rect.Left, rect.Bottom)
				);
				DrawHandles(g,
					new PointF(rect.Left, rect.Top),
					new PointF(rect.Left, rect.Bottom));
				DrawHandles(g,
					new PointF(rect.Right, rect.Top),
					new PointF(rect.Right, rect.Bottom));

				string arrowPreview = null;
				switch (hoverContext)
				{
					case HoverContext.ArrowDown:
						arrowPreview = "down";
						break;
					case HoverContext.ArrowLeft:
						arrowPreview = "left";
						break;
					case HoverContext.ArrowRight:
						arrowPreview = "right";
						break;
					case HoverContext.ArrowUp:
						arrowPreview = "up";
						break;
				}
				if (!string.IsNullOrEmpty(arrowPreview))
				{
					DrawArrow(g, GetRectangle(), arrowPreview, false);
				}
				g.ResetTransform();
			}
		}

		public override PointF[] ToLocalPt(Matrix sceneTransform, params PointF[] pts)
		{
			LiveScene scene = Data as LiveScene;
			Matrix transform = new Matrix();
			transform.Multiply(scene.Camera.WorldTransform);
			transform.Multiply(sceneTransform, MatrixOrder.Append);
			transform.Invert();
			RectangleF rect = GetRectangle(); //bubble in camera space

			//convert points to camera space
			transform.TransformPoints(pts);

			//difference is local space
			for (int i = 0; i < pts.Length; i++)
			{
				pts[i] = new PointF(pts[i].X - rect.X, pts[i].Y - rect.Y);
			}
			return pts;
		}

		public override PointF[] ToScreenPt(Matrix sceneTransform, params PointF[] pts)
		{
			LiveScene scene = Data as LiveScene;
			Matrix transform = new Matrix();
			transform.Multiply(scene.Camera.WorldTransform);
			transform.Multiply(sceneTransform, MatrixOrder.Append);
			RectangleF rect = GetRectangle();

			for (int i = 0; i < pts.Length; i++)
			{
				pts[i] = new PointF(pts[i].X + rect.X, pts[i].Y + rect.Y);
			}

			transform.TransformPoints(pts);

			return pts;
		}

		public override PointF[] ToWorldPt(params PointF[] pts)
		{
			LiveScene scene = Data as LiveScene;
			for (int i = 0; i < pts.Length; i++)
			{
				pts[i] = new PointF(pts[i].X / scene.Width * 100, pts[i].Y / scene.Height * 100);
			}
			return pts;
		}

		public override PointF[] ScreenToWorldPt(Matrix sceneTransform, params PointF[] pts)
		{
			LiveScene scene = Data as LiveScene;
			Matrix transform = new Matrix();
			transform.Multiply(scene.Camera.WorldTransform);
			transform.Multiply(sceneTransform, MatrixOrder.Append);
			transform.Invert();

			//convert points to camera space, which is world space for text boxes
			transform.TransformPoints(pts);

			//but as a percentage of the scene
			for (int i = 0; i < pts.Length; i++)
			{
				pts[i] = new PointF(pts[i].X / scene.Width * 100, pts[i].Y / scene.Height * 100);
			}

			return pts;
		}

		protected override void OnPreviewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			LiveBubble preview = LinkedPreview as LiveBubble;
			if (e.PropertyName == "TextX")
			{
				TextX = preview.TextX;
			}
			else if (e.PropertyName == "TextY")
			{
				TextY = preview.TextY;
			}
			else if (e.PropertyName == "Arrow")
			{
				Arrow = preview.Arrow;
			}
			else if (e.PropertyName == "TextWidth")
			{
				TextWidth = preview.TextWidth;
			}
		}

		protected override void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			LiveBubble preview = LinkedPreview as LiveBubble;
			if (e.PropertyName == "TextX")
			{
				preview.TextX = TextX;
			}
			else if (e.PropertyName == "TextY")
			{
				preview.TextY = TextY;
			}
			else if (e.PropertyName == "Arrow")
			{
				preview.Arrow = Arrow;
			}
			else if (e.PropertyName == "TextWidth")
			{
				preview.TextWidth = TextWidth;
			}
			else if (e.PropertyName == "AlignmentX")
			{
				preview.AlignmentX = AlignmentX;
			}
			else if (e.PropertyName == "AlignmentY")
			{
				preview.AlignmentY = AlignmentY;
			}
		}

		public override bool CanPivot { get { return false; } }
		public override bool CanRotate { get { return false; } }
		public override bool CanSkew { get { return false; } }
		public override bool CanArrow { get { return true; } }

		public override void Scale(Point screenPoint, Matrix sceneTransform, HoverContext context)
		{
			LiveScene scene = Data as LiveScene;
			RectangleF rect = GetRectangle();
			switch (context)
			{
				case HoverContext.ScaleRight:
					PointF localPt = ToLocalPt(screenPoint.X, screenPoint.Y, sceneTransform);
					int width = (int)Math.Round(localPt.X / scene.Width * 100);
					string newWidth = Math.Max(0, width) + "%";
					TextWidth = newWidth;
					break;
				case HoverContext.ScaleLeft:
					localPt = ToLocalPt(screenPoint.X, screenPoint.Y, sceneTransform);
					width = (int)Math.Round((rect.Width - localPt.X) / scene.Width * 100);
					int oldW = (int)Math.Max(0, rect.Width / scene.Width * 100);
					//update left position as well so the right doesn't move
					int left = (int)(Math.Round(localPt.X + rect.X) / scene.Width * 100);
					int oldLeft = (int)(Math.Round(rect.X / scene.Width * 100));
					int diff = oldLeft - left;
					newWidth = (oldW + diff) + "%";
					TextX = left + "%";
					TextWidth = newWidth;
					break;
			}
		}

		public override void Translate(float x, float y, Matrix sceneTransform)
		{
			TextX = Math.Round(x).ToString(CultureInfo.InvariantCulture) + "%";
			TextY = Math.Round(y).ToString(CultureInfo.InvariantCulture) + "%";
		}

		public override void UpdateArrowPosition(HoverContext arrowContext)
		{
			string position = "";
			switch (arrowContext)
			{
				case HoverContext.ArrowDown:
					position = "down";
					break;
				case HoverContext.ArrowLeft:
					position = "left";
					break;
				case HoverContext.ArrowRight:
					position = "right";
					break;
				case HoverContext.ArrowUp:
					position = "up";
					break;
			}
			Arrow = position;
		}

		public override bool FilterRecord(string key)
		{
			switch (key)
			{
				case "pivotx":
				case "pivoty":
				case "z":
					return false;
				default:
					return true;
			}
		}

		public override void GetBlock(string property, float time, out LiveKeyframe start, out LiveKeyframe end)
		{
			start = null;
			end = null;
			if (Keyframes.Count == 0)
			{
				return;
			}
			for (int i = 0; i < Keyframes.Count; i++)
			{
				LiveKeyframe kf = Keyframes[i];
				if (kf.Time <= time)
				{
					start = kf;
				}
				else if (kf.Time > time)
				{
					if (start == null)
					{
						start = kf;
					}
					end = kf;
					return;
				}
			}
			end = start = Keyframes[0];
		}
	}
}
