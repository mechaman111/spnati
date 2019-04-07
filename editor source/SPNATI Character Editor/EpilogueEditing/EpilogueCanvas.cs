using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.EpilogueEditing;
using SPNATI_Character_Editor.Properties;
using System.Globalization;
using SPNATI_Character_Editor.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class EpilogueCanvas : UserControl
	{
		/// <summary>
		/// How many pixels the user has to click within to select a handle
		/// </summary>
		public const int SelectionLeeway = 5;
		public const int RotationLeeway = 30;

		public const float MinZoom = 0.25f;
		public const float MaxZoom = 3;
		private const int ArrowSize = 16;

		private Epilogue _epilogue;
		private Character _character;
		private Scene _selectedScene;
		private Directive _selectedDirective;
		private Keyframe _selectedKeyframe;
		private SceneAnimation _selectedAnimation;
		private List<PendedDirective> _pendingDirectives = new List<PendedDirective>();

		private bool _viewportLocked;
		private Point _prelockOffset;
		private float _prelockZoom;

		private Point _canvasOffset = new Point(0, 0);
		private Point _dragOffset = new Point(0, 0);
		private Point _downPoint = new Point(0, 0);
		private HoverContext _dragContext;
		private HoverContext _moveContext;

		private bool _lockedBeforePlayback;
		private int _directiveIndex = -1;
		private bool _waitingForAnims = false;
		private bool _showOverlay = true;
		private List<string> _markers = new List<string>();

		private float _zoom = 1;
		public float ZoomLevel
		{
			get { return _zoom; }
			set
			{
				_zoom = Math.Max(MinZoom, Math.Min(MaxZoom, value));
				UpdateZoomLevel();
			}
		}

		private DateTime _lastTick;
		private SceneObject _selectedObject = null;
		private ScenePreview _scenePreview = null;
		private SceneTransition _sceneTransition;
		private SceneObject _overlay = null;
		private List<SceneAnimation> _animations = new List<SceneAnimation>();

		private Font _font = new Font("Trebuchet MS", 14);
		private StringFormat _textFormat = new StringFormat() { Alignment = StringAlignment.Center };
		private Pen _borderPen;
		private Pen _penOuterSelection;
		private Pen _penInnerSelection;
		private Pen _penLens;
		private Pen _penKeyframe;
		private Pen _penKeyframeSegment;
		private Pen _penKeyframeConnection;
		private Brush _brushKeyframe;
		private Point _lastMouse;
		private Brush _outsideBrush;
		private Brush _brushPreviewArrow;


		private EditMode _mode = EditMode.Edit;
		private CanvasState _state = CanvasState.Normal;

		public EpilogueCanvas()
		{
			InitializeComponent();

			_outsideBrush = new SolidBrush(Color.FromArgb(80, 0, 10, 30));

			_borderPen = new Pen(Brushes.DarkGray, 1);
			_borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			_penLens = new Pen(Brushes.LightGray, 5);
			_penLens.Color = Color.FromArgb(127, _penLens.Color);

			_penOuterSelection = new Pen(Brushes.White, 1);
			_penOuterSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			_penInnerSelection = new Pen(Brushes.Black, 1);
			_penInnerSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

			_penKeyframe = new Pen(Color.FromArgb(127, 255, 255, 255));
			_penKeyframe.Width = 2;
			_penKeyframeConnection = new Pen(Color.FromArgb(127, 255, 255, 255), 8);
			_penKeyframeConnection.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
			_penKeyframeSegment = new Pen(Color.FromArgb(127, 255, 255, 255), 8);

			_brushPreviewArrow = new SolidBrush(Color.FromArgb(127, 255, 255, 255));

			_brushKeyframe = new SolidBrush(Color.FromArgb(127, 127, 127, 127));

			canvas.MouseWheel += Canvas_MouseWheel;
			canvas.KeyDown += Canvas_KeyDown;
		}

		public void SetEpilogue(Epilogue epilogue, Character character)
		{
			_selectedScene = null;
			_epilogue = epilogue;
			_character = character;
			Enabled = (_epilogue != null);
			propertyTable.Data = null;
			treeScenes.SetData(epilogue);
			BuildScene(false);
			FitToScreen();
		}

		public void JumpToNode(Scene scene, Directive directive, Keyframe keyframe)
		{
			treeScenes.SelectNode(scene, directive, keyframe);
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			if (_sceneTransition != null)
			{
				_sceneTransition.Draw(e.Graphics, canvas.Width, canvas.Height);
				return;
			}

			if (_scenePreview == null) { return; }
			Graphics g = e.Graphics;

			//Point viewportTopLeft = ToScreenPoint(new Point(0, 0));
			//Point viewportBottomRight = ToScreenPoint(new PointF(_scenePreview.Width, _scenePreview.Height));

			//_font = new Font("Trebuchet MS", 14 * ZoomLevel / _scenePreview.Scale); //use this to scale font with viewport
			//_font = new Font("Trebuchet MS", 14 * ZoomLevel); //use this to scale with zoom but constant relative to viewport
			//both commented out keeps size 14 regardless of window size, which matches current game behavior

			////Scene edges
			//g.DrawLine(_borderPen, viewportTopLeft.X, 0, viewportTopLeft.X, canvas.Height);
			//g.DrawLine(_borderPen, viewportBottomRight.X, 0, viewportBottomRight.X, canvas.Height);
			//g.DrawLine(_borderPen, 0, viewportTopLeft.Y, canvas.Width, viewportTopLeft.Y);
			//g.DrawLine(_borderPen, 0, viewportBottomRight.Y, canvas.Width, viewportBottomRight.Y);

			//images
			foreach (SceneObject obj in _scenePreview.Objects)
			{
				//keyframes
				if (_selectedAnimation?.AssociatedObject == obj && _mode == EditMode.Edit)
				{
					//draw keyframes
					DrawAnimation(g, obj, true);
				}
				DrawSprite(g, obj);
				if (_selectedAnimation?.AssociatedObject == obj && tmrPlay.Enabled && _mode == EditMode.Edit)
				{
					DrawSprite(g, _selectedAnimation.PreviewObject);
				}
			}

			//overlay
			if (_showOverlay || _mode == EditMode.Playback)
			{
				if (tmrPlay.Enabled && _mode == EditMode.Edit && _selectedAnimation?.AssociatedObject == _overlay)
				{
					g.FillRectangle(_selectedAnimation.PreviewObject.Color, 0, 0, canvas.Width, canvas.Height);
				}
				else
				{
					g.FillRectangle(_overlay.Color, 0, 0, canvas.Width, canvas.Height);
				}
			}

			//textboxes
			foreach (SceneObject obj in _scenePreview.TextBoxes)
			{
				DrawTextBox(g, obj);
			}

			//darken the area outside the scene
			//if (viewportTopLeft.X > 0)
			//{
			//	g.FillRectangle(_outsideBrush, 0, 0, viewportTopLeft.X, canvas.Height);
			//}
			//if (viewportTopLeft.Y > 0)
			//{
			//	g.FillRectangle(_outsideBrush, Math.Max(viewportTopLeft.X, 0), 0, canvas.Width, viewportTopLeft.Y);
			//}
			//if (viewportBottomRight.Y < canvas.Height)
			//{
			//	g.FillRectangle(_outsideBrush, Math.Max(viewportTopLeft.X, 0), viewportBottomRight.Y, canvas.Width, canvas.Height - viewportBottomRight.Y);
			//}
			//g.FillRectangle(_outsideBrush, Math.Max(viewportBottomRight.X, 0), viewportTopLeft.Y, canvas.Width, viewportBottomRight.Y - viewportTopLeft.Y);

			//camera viewport
			if (_selectedAnimation?.AssociatedObject == _scenePreview)
			{
				DrawAnimation(g, _scenePreview, false);

				if (tmrPlay.Enabled)
				{
					DrawCamera(g, _selectedAnimation.PreviewObject);
				}
			}
			DrawCamera(g, _scenePreview);

			//selection and gizmos
			if (_selectedObject != null)
			{
				DrawSelection(g, _selectedObject);

				//rotation arrow
				if (_moveContext == HoverContext.Rotate)
				{
					Image arrow = Resources.rotate_arrow;
					Point pt = new Point(_lastMouse.X - arrow.Width / 2, _lastMouse.Y - arrow.Height / 2);

					//rotate to face the object's center
					PointF center = ToScreenCenter(_selectedObject); ;

					double angle = Math.Atan2(center.Y - pt.Y, center.X - pt.X);
					angle = angle * (180 / Math.PI) - 90;

					g.TranslateTransform(_lastMouse.X, _lastMouse.Y);
					g.RotateTransform((float)angle);
					g.TranslateTransform(-_lastMouse.X, -_lastMouse.Y);
					g.DrawImage(arrow, pt);
					g.ResetTransform();
				}
			}
		}

		private void DrawAnimation(Graphics g, SceneObject obj, bool filled)
		{
			if (_mode == EditMode.Playback) { return; }
			SceneObject lastFrame = null;
			for (int i = 0; i < _selectedAnimation.Frames.Count; i++)
			{
				SceneObject frame = _selectedAnimation.Frames[i];
				DrawKeyframe(g, frame, filled);
				if (lastFrame != null)
				{
					//draw connection

					if (obj.Tween == "spline")
					{
						//spline
						DrawSpline(g, _selectedAnimation.Frames, i);
					}
					else
					{
						//linear
						RectangleF b1 = ToScreenRegion(lastFrame);
						RectangleF b2 = ToScreenRegion(frame);
						g.DrawLine(_penKeyframeConnection, b1.X + b1.Width / 2, b1.Y + b1.Height / 2, b2.X + b2.Width / 2, b2.Y + b2.Height / 2);
					}
				}
				lastFrame = frame;
			}
		}

		private void DrawSprite(Graphics g, SceneObject obj)
		{
			RectangleF bounds = ToScreenRegion(obj);

			float offsetX = bounds.X + obj.PivotX / obj.Width * bounds.Width;
			float offsetY = bounds.Y + obj.PivotY / obj.Height * bounds.Height;
			if (float.IsNaN(offsetX))
			{
				offsetX = 0;
			}
			if (float.IsNaN(offsetY))
			{
				offsetY = 0;
			}
			g.TranslateTransform(offsetX, offsetY);
			g.RotateTransform(obj.Rotation);
			g.TranslateTransform(-offsetX, -offsetY);

			if (obj.ObjectType == SceneObjectType.Emitter)
			{
				if (_mode != EditMode.Playback)
				{
					Point center = ToScreenPoint(new PointF(obj.X, obj.Y));
					g.DrawImage(Resources.emitter, center.X - Resources.emitter.Width / 2, center.Y - Resources.emitter.Height / 2);
				}
			}
			else
			{
				if (obj.Image == null)
				{
					if (obj.ObjectType == SceneObjectType.Other)
					{
						g.FillRectangle(obj.Color, 0, 0, canvas.Width, canvas.Height);
					}
					else
					{
						g.FillEllipse(obj.Color, bounds.X, bounds.Y, bounds.Width, bounds.Height);
					}
				}
				else
				{
					if ((obj.SkewX == 0 || obj.SkewX % 90 != 0) && (obj.SkewY == 0 || obj.SkewY % 90 != 0))
					{
						float skewedWidth = bounds.Height * (float)Math.Tan(Math.PI / 180.0f * obj.SkewX);
						int skewDistanceX = (int)(skewedWidth / 2);
						float skewedHeight = bounds.Width * (float)Math.Tan(Math.PI / 180.0f * obj.SkewY);
						int skewDistanceY = (int)(skewedHeight / 2);
						//TODO: This doesn't properly account for the pivot
						Point[] destPts = new Point[] { new Point((int)bounds.X - skewDistanceX, (int)bounds.Y - skewDistanceY), new Point((int)bounds.Right - skewDistanceX, (int)bounds.Y + skewDistanceY), new Point((int)bounds.X + skewDistanceX, (int)bounds.Bottom - skewDistanceY) };

						if (obj.Alpha > 0)
						{
							if (obj.Alpha < 100)
							{
								float[][] matrixItems = new float[][] {
							new float[] { 1, 0, 0, 0, 0 },
							new float[] { 0, 1, 0, 0, 0 },
							new float[] { 0, 0, 1, 0, 0 },
							new float[] { 0, 0, 0, obj.Alpha / 100.0f, 0 },
							new float[] { 0, 0, 0, 0, 1 }
						};
								ColorMatrix cm = new ColorMatrix(matrixItems);
								ImageAttributes ia = new ImageAttributes();
								ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

								g.DrawImage(obj.Image, destPts, new Rectangle(0, 0, obj.Image.Width, obj.Image.Height), GraphicsUnit.Pixel, ia);
							}
							else
							{
								g.DrawImage(obj.Image, destPts, new Rectangle(0, 0, obj.Image.Width, obj.Image.Height), GraphicsUnit.Pixel);
							}
						}
					}
				}
			}

			g.ResetTransform();
		}

		private void DrawKeyframe(Graphics g, SceneObject obj, bool filled)
		{
			RectangleF bounds = ToAbsScreenRegion(obj);
			if (_selectedAnimation.AssociatedObject == _scenePreview)
			{
				bounds = GetViewportBounds(obj);
			}
			float offsetX = bounds.X + bounds.Width / 2;
			float offsetY = bounds.Y + bounds.Height / 2;
			g.TranslateTransform(offsetX, offsetY);
			g.RotateTransform(obj.Rotation);
			g.TranslateTransform(-offsetX, -offsetY);

			if (filled)
			{
				g.FillRectangle(_brushKeyframe, bounds.X, bounds.Y, bounds.Width, bounds.Height);
			}
			g.DrawRectangle(_penKeyframe, bounds.X, bounds.Y, bounds.Width, bounds.Height);

			g.ResetTransform();
		}

		private void DrawTextBox(Graphics g, SceneObject textbox)
		{
			const int Padding = 5;

			RectangleF bounds = ToScreenRegion(textbox);
			SizeF size = g.MeasureString(textbox.Text, _font, (int)(bounds.Width - Padding * 2), _textFormat);
			bounds.Height = size.Height + Padding * 2;
			textbox.Height = bounds.Height;
			g.FillRectangle(Brushes.White, bounds);
			g.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height);
			g.DrawString(textbox.Text, _font, Brushes.Black, new RectangleF(bounds.X + Padding, bounds.Y + Padding, bounds.Width - Padding * 2, bounds.Height - Padding * 2), _textFormat);

			if (textbox.Arrow == "down" || (_selectedObject == textbox && _moveContext == HoverContext.ArrowDown))
			{
				float center = bounds.X + bounds.Width / 2;
				float y = bounds.Y + bounds.Height - 1;
				PointF p1 = new PointF(center - ArrowSize, y);
				PointF p2 = new PointF(center + ArrowSize, y);
				PointF p3 = new PointF(center, y + ArrowSize);
				PointF[] triangle = new PointF[] { p1, p2, p3 };
				g.FillPolygon(textbox.Arrow == "down" ? Brushes.White : _brushPreviewArrow, triangle);
				g.DrawLine(Pens.Black, p1, p3);
				g.DrawLine(Pens.Black, p2, p3);
			}
			if (textbox.Arrow == "up" || (_selectedObject == textbox && _moveContext == HoverContext.ArrowUp))
			{
				float center = bounds.X + bounds.Width / 2;
				float y = bounds.Y + 1;
				PointF p1 = new PointF(center + ArrowSize, y);
				PointF p2 = new PointF(center - ArrowSize, y);
				PointF p3 = new PointF(center, y - ArrowSize);
				PointF[] triangle = new PointF[] { p1, p2, p3 };
				g.FillPolygon(textbox.Arrow == "up" ? Brushes.White : _brushPreviewArrow, triangle);
				g.DrawLine(Pens.Black, p1, p3);
				g.DrawLine(Pens.Black, p2, p3);
			}
			if (textbox.Arrow == "left" || (_selectedObject == textbox && _moveContext == HoverContext.ArrowLeft))
			{
				float center = bounds.Y + bounds.Height / 2;
				float x = bounds.X + 1;
				PointF p1 = new PointF(x, center - ArrowSize);
				PointF p2 = new PointF(x, center + ArrowSize);
				PointF p3 = new PointF(x - ArrowSize, center);
				PointF[] triangle = new PointF[] { p1, p2, p3 };
				g.FillPolygon(textbox.Arrow == "left" ? Brushes.White : _brushPreviewArrow, triangle);
				g.DrawLine(Pens.Black, p1, p3);
				g.DrawLine(Pens.Black, p2, p3);
			}
			if (textbox.Arrow == "right" || (_selectedObject == textbox && _moveContext == HoverContext.ArrowRight))
			{
				float center = bounds.Y + bounds.Height / 2;
				float x = bounds.X + bounds.Width - 1;
				PointF p1 = new PointF(x, center + ArrowSize);
				PointF p2 = new PointF(x, center - ArrowSize);
				PointF p3 = new PointF(x + ArrowSize, center);
				PointF[] triangle = new PointF[] { p1, p2, p3 };
				g.FillPolygon(textbox.Arrow == "right" ? Brushes.White : _brushPreviewArrow, triangle);
				g.DrawLine(Pens.Black, p1, p3);
				g.DrawLine(Pens.Black, p2, p3);
			}
		}

		private void DrawSelection(Graphics g, SceneObject obj)
		{
			if (_mode == EditMode.Playback) { return; }
			RectangleF bounds = ToAbsScreenRegion(obj);
			const int SelectionPadding = 0;
			g.DrawRectangle(_penOuterSelection, bounds.X - 2 - SelectionPadding, bounds.Y - 2 - SelectionPadding, bounds.Width + 4 + SelectionPadding * 2, bounds.Height + 4 + SelectionPadding * 2);
			g.DrawRectangle(_penInnerSelection, bounds.X - 1 - SelectionPadding, bounds.Y - 1 - SelectionPadding, bounds.Width + 2 + SelectionPadding * 2, bounds.Height + 2 + SelectionPadding * 2);

			//pivot point
			if (obj.ObjectType == SceneObjectType.Sprite)
			{
				bounds = ToUnscaledScreenRegion(obj);

				if (_state == CanvasState.MovingPivot || _moveContext == HoverContext.Pivot)
				{
					g.DrawRectangle(_penKeyframe, bounds.X, bounds.Y, bounds.Width, bounds.Height);
				}

				PointF pt = new PointF(bounds.X + obj.PivotX / obj.Width * bounds.Width, bounds.Y + obj.PivotY / obj.Height * bounds.Height);
				g.FillEllipse(Brushes.White, pt.X - 3, pt.Y - 3, 6, 6);
				g.FillEllipse(Brushes.Black, pt.X - 2, pt.Y - 2, 4, 4);
			}
		}

		private void DrawCamera(Graphics g, SceneObject camera)
		{
			if (camera != null)
			{
				//get unscaled center
				Rectangle rect = GetViewportBounds(camera);
				Point tl = new Point(rect.X, rect.Y);
				Point br = new Point(rect.X + rect.Width, rect.Y + rect.Height);
				Point tr = new Point(rect.X + rect.Width, rect.Y);
				Point bl = new Point(rect.X, rect.Y + rect.Height);

				Brush outerBrush = (_viewportLocked ? Brushes.Black : _outsideBrush);
				//darken the area outside the viewport. when locked to the scene, turn it completely black
				g.FillRectangle(outerBrush, 0, 0, tl.X, canvas.Height);
				g.FillRectangle(outerBrush, tr.X, 0, canvas.Width - tr.X, canvas.Height);
				g.FillRectangle(outerBrush, tl.X, 0, tr.X - tl.X, tl.Y);
				g.FillRectangle(outerBrush, tl.X, bl.Y, tr.X - tl.X, canvas.Height - bl.Y);

				//viewport
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

				return;
			}
		}

		/// <summary>
		/// Draws a spline from the current index in a list of points to the next one.
		/// The first and last points are used as their own control points in the Catmull-Rom implementation
		/// </summary>
		/// <param name="g"></param>
		/// <param name="frames"></param>
		/// <param name="pos"></param>
		private void DrawSpline(Graphics g, List<SceneObject> frames, int pos)
		{
			SceneObject obj0 = frames[pos > 1 ? pos - 2 : pos - 1];
			SceneObject obj1 = frames[pos - 1];
			SceneObject obj2 = frames[pos];
			SceneObject obj3 = frames[pos + 1 < frames.Count ? pos + 1 : pos];

			Point p0 = ToScreenPoint(new PointF(obj0.X + obj0.Width / 2, obj0.Y + obj0.Height / 2));
			Point p1 = ToScreenPoint(new PointF(obj1.X + obj1.Width / 2, obj1.Y + obj1.Height / 2));
			Point p2 = ToScreenPoint(new PointF(obj2.X + obj2.Width / 2, obj2.Y + obj2.Height / 2));
			Point p3 = ToScreenPoint(new PointF(obj3.X + obj3.Width / 2, obj3.Y + obj3.Height / 2));

			Point lastPos = p1;
			int steps = 10;
			for (int i = 0; i <= steps; i++)
			{
				float t = i / (float)steps;
				PointF newPosF = EvaluateSubspline(t, p0, p1, p2, p3);
				Point newPos = new Point((int)newPosF.X, (int)newPosF.Y);
				g.DrawLine(i < steps ? _penKeyframeSegment : _penKeyframeConnection, lastPos, newPos);
				lastPos = newPos;
			}
		}

		private static PointF EvaluateSubspline(float t, Point p0, Point p1, Point p2, Point p3)
		{
			float ax = 2.0f * p1.X;
			float ay = 2.0f * p1.Y;
			float bx = p2.X - p0.X;
			float by = p2.Y - p0.Y;
			float cx = 2.0f * p0.X - 5.0f * p1.X + 4.0f * p2.X - p3.X;
			float cy = 2.0f * p0.Y - 5.0f * p1.Y + 4.0f * p2.Y - p3.Y;
			float dx = -p0.X + 3.0f * p1.X - 3.0f * p2.X + p3.X;
			float dy = -p0.Y + 3.0f * p1.Y - 3.0f * p2.Y + p3.Y;

			float px = 0.5f * (ax + (bx * t) + (cx * t * t) + (dx * t * t * t));
			float py = 0.5f * (ay + (by * t) + (cy * t * t) + (dy * t * t * t));
			return new PointF(px, py);
		}

		private Point ToScreenPoint(PointF pt)
		{
			int x = (int)((pt.X + _canvasOffset.X) * ZoomLevel);
			int y = (int)((pt.Y + _canvasOffset.Y) * ZoomLevel);
			return new Point(x, y);
		}

		/// <summary>
		/// Converts a scene object's scaled but unrotated bounds to a screen-space rectangle
		/// </summary>
		/// <param name=""></param>
		/// <param name=""></param>
		/// <returns></returns>
		private RectangleF ToScreenRegion(SceneObject obj)
		{
			if (obj.ObjectType == SceneObjectType.Text)
			{
				//textboxes are scaled and positioned relative to the viewport size and not the scene
				Rectangle viewport = GetViewportBounds();
				RectangleF position = new RectangleF(viewport.X + obj.X / 100.0f * viewport.Width, viewport.Y + obj.Y / 100.0f * viewport.Height, obj.Width / 100.0f * viewport.Width, obj.Height);
				int arrowWidth = (obj.Arrow == "left" || obj.Arrow == "right") ? ArrowSize : 0;
				int arrowHeight = (obj.Arrow == "up" || obj.Arrow == "down") ? ArrowSize : 0;
				if (obj.AlignmentX == "right")
				{
					int width = (int)(position.Width + arrowWidth);
					position.X = (position.X - width);
				}
				else if (obj.AlignmentX == "center")
				{
					int width = (int)(position.Width + arrowWidth);
					position.X = (position.X - width * 0.5f);
				}
				if (obj.AlignmentY == "bottom")
				{
					int height = (int)(position.Height + arrowHeight);
					position.Y = (position.Y - height);
				}
				else if (obj.AlignmentY == "center")
				{
					int height = (int)(position.Height + arrowHeight);
					position.Y = (position.Y - height * 0.5f);
				}
				return position;
			}
			else if (obj.ObjectType == SceneObjectType.Emitter || obj?.SourceObject?.ObjectType == SceneObjectType.Emitter)
			{
				//emitters don't actually have a size, so just use a constant sized box centered around the upper left to represent where things get emitted from
				Point center = ToScreenPoint(new PointF(obj.X, obj.Y));
				return new RectangleF(center.X - SceneEmitter.EmitterRadius, center.Y - SceneEmitter.EmitterRadius, SceneEmitter.EmitterRadius * 2, SceneEmitter.EmitterRadius * 2);
			}
			else
			{
				//rotations are not computed currently when considering the bounding box


				//get unscaled bounds in screen space
				PointF pt = new PointF(obj.X, obj.Y);
				RectangleF rect = ToUnscaledScreenRegion(obj);
				Point pivot = ToScreenPoint(new PointF(pt.X + obj.PivotX, pt.Y + obj.PivotY));

				//translate pivot to origin
				rect.X -= pivot.X;
				rect.Y -= pivot.Y;

				//apply scaling
				float right = rect.X + rect.Width;
				rect.X *= obj.ScaleX;
				right *= obj.ScaleX;
				rect.Width = right - rect.X;

				float bottom = rect.Y + rect.Height;
				rect.Y *= obj.ScaleY;
				bottom *= obj.ScaleY;
				rect.Height = bottom - rect.Y;

				//translate back
				rect.X += pivot.X;
				rect.Y += pivot.Y;

				return rect;
			}
		}

		/// <summary>
		/// Converts an object's bounds to screen space, ensuring that the width and height are positive
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private RectangleF ToAbsScreenRegion(SceneObject obj)
		{
			RectangleF region = ToScreenRegion(obj);
			if (obj.ObjectType == SceneObjectType.Text)
			{
				return region;
			}
			if (region.Width < 0)
			{
				region.X += region.Width;
				region.Width = -region.Width;
			}
			if (region.Height < 0)
			{
				region.Y += region.Height;
				region.Height = -region.Height;
			}
			return region;
		}

		/// <summary>
		/// Converts an object's unscaled bounds to screen space
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private RectangleF ToUnscaledScreenRegion(SceneObject obj)
		{
			PointF pt = new PointF(obj.X, obj.Y);
			Point tl = ToScreenPoint(pt);
			Point br = ToScreenPoint(new PointF(pt.X + obj.Width, pt.Y + obj.Height));
			RectangleF rect = new RectangleF(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y);
			return rect;
		}

		/// <summary>
		/// Gets the "center" of an object's selection
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private PointF ToScreenCenter(SceneObject obj)
		{
			RectangleF bounds = ToScreenRegion(obj);
			if (obj.ObjectType == SceneObjectType.Emitter)
			{
				return new PointF(bounds.X, bounds.Y);
			}
			else
			{
				float cx = bounds.X + bounds.Width / 2;
				float cy = bounds.Y + bounds.Height / 2;
				return new PointF(cx, cy);
			}
		}

		/// <summary>
		/// Converts a point from screen space to world space
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		private Point ToWorldPoint(Point pt)
		{
			int wx = (int)((pt.X - _canvasOffset.X * ZoomLevel) / ZoomLevel);
			int wy = (int)((pt.Y - _canvasOffset.Y * ZoomLevel) / ZoomLevel);
			return new Point(wx, wy);
		}

		/// <summary>
		/// Converts a point from screen space to viewport space
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		private Point ToViewportPoint(Point pt)
		{
			Rectangle viewport = GetViewportBounds(); //viewport in screen space
			float x = pt.X - viewport.X;
			float y = pt.Y - viewport.Y;

			return new Point((int)(x / viewport.Width * 100), (int)(y / viewport.Height * 100));
		}

		private Rectangle GetViewportBounds()
		{
			return GetViewportBounds(_scenePreview);
		}
		private Rectangle GetViewportBounds(SceneObject scene)
		{
			int cx = (int)(scene.X + scene.Width / 2);
			int cy = (int)(scene.Y + scene.Height / 2);

			float width = scene.Width / scene.Zoom / 2;
			float height = scene.Height / scene.Zoom / 2;

			float l = cx - width;
			float t = cy - height;
			float r = cx + width;
			float b = cy + height;
			Point tl = ToScreenPoint(new PointF(l, t));
			Point br = ToScreenPoint(new PointF(r, b));
			return new Rectangle(tl, new Size(br.X - tl.X, br.Y - tl.Y));
		}

		private void Canvas_MouseWheel(object sender, MouseEventArgs e)
		{
			if (_viewportLocked) { return; }
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				((HandledMouseEventArgs)e).Handled = true;
				if (e.Delta > 0 && sliderZoom.Value < sliderZoom.Maximum)
				{
					sliderZoom.Value += sliderZoom.SmallChange;
				}
				else if (e.Delta < 0 && sliderZoom.Value > sliderZoom.Minimum)
				{
					sliderZoom.Value -= sliderZoom.SmallChange;
				}
			}
		}

		/// <summary>
		/// Gets the highest object at the given screen point
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="objects">List of objects to select from</param>
		/// <returns></returns>
		private SceneObject GetObjectAtPoint(int x, int y, List<SceneObject> objects)
		{
			//NOTE: rotations are ignored for now. They definitely can't be if parenting ever gets added

			//search in reverse order because objects are sorted by depth
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				RectangleF bounds = ToScreenRegion(objects[i]);
				if (bounds.X <= x && x <= bounds.X + bounds.Width &&
					bounds.Y <= y && y <= bounds.Y + bounds.Height)
				{
					return objects[i];
				}
			}
			return null;
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (_scenePreview == null) { return; }
			switch (_mode)
			{
				case EditMode.Edit:
					_downPoint = ToWorldPoint(new Point(e.X, e.Y));
					if (e.Button == MouseButtons.Left)
					{
						if (_selectedScene == null) { return; }

						switch (_moveContext)
						{
							case HoverContext.ArrowLeft:
							case HoverContext.ArrowDown:
							case HoverContext.ArrowRight:
							case HoverContext.ArrowUp:
								UpdateArrowPosition();
								break;
							default:
								//object selection
								SceneObject obj = null;
								if (_moveContext == HoverContext.None || _moveContext == HoverContext.Select)
								{
									//1 - Keyframe
									if (_selectedAnimation != null)
									{
										obj = GetObjectAtPoint(e.X, e.Y, _selectedAnimation.Frames);
										if (obj != null)
										{
											if (obj.LinkedFrame != null)
											{
												treeScenes.SelectNode(_selectedScene, _selectedDirective, obj.LinkedFrame as Keyframe);
												break;
											}
											else
											{
												treeScenes.SelectNode(_selectedScene, _selectedAnimation.AssociatedObject.LastFrame?.Directive, _selectedAnimation.AssociatedObject.LastFrame);
											}
										}
									}

									//2 - textbox
									if (obj == null)
									{
										obj = GetObjectAtPoint(e.X, e.Y, _scenePreview.TextBoxes);
									}

									//3 - scene object
									if (obj == null)
									{
										obj = GetObjectAtPoint(e.X, e.Y, _scenePreview.Objects);
									}

									if (obj != null && obj != _selectedObject && (string.IsNullOrEmpty(obj.Id) || obj.Id != _selectedObject?.Id))
									{
										Directive dir = string.IsNullOrEmpty(obj.Id) ? (obj.LinkedFrame as Directive) : _selectedScene.GetDirective(obj.Id);
										if (dir != null)
										{
											treeScenes.SelectNode(_selectedScene, dir, null);
										}
									}
								}
								break;
						}
						break;
					}
					else if (e.Button == MouseButtons.Right)
					{
						_lastMouse = new Point(e.X, e.Y);
						_state = CanvasState.Panning;
						canvas.Cursor = _viewportLocked ? Cursors.No : Cursors.NoMove2D;
					}
					break;
				case EditMode.Playback:
					if (e.Button == MouseButtons.Left)
					{
						AdvanceDirective();
					}
					break;
			}
		}

		/// <summary>
		/// Gets a contextual action based on where the mouse is relative to objects on screen
		/// </summary>
		/// <param name="worldPt"></param>
		private HoverContext GetContext(Point screenPt)
		{
			if (_selectedObject != null)
			{
				bool locked = false;
				Keyframe frame = _selectedObject.LinkedFrame;
				if (frame != null)
				{
					locked = frame.Locked;
				}

				RectangleF bounds = ToAbsScreenRegion(_selectedObject);
				if (_selectedObject.ObjectType == SceneObjectType.Sprite || _selectedObject.ObjectType == SceneObjectType.Emitter)
				{
					bool allowPivot = (_selectedObject.ObjectType == SceneObjectType.Sprite);
					bool allowRotate = true;
					bool allowScale = (_selectedObject.ObjectType == SceneObjectType.Sprite);
					bool allowSkew = (_selectedObject.ObjectType == SceneObjectType.Sprite);

					float dl = Math.Abs(screenPt.X - bounds.X);
					float dr = Math.Abs(screenPt.X - (bounds.X + bounds.Width));
					float dt = Math.Abs(screenPt.Y - bounds.Y);
					float db = Math.Abs(screenPt.Y - (bounds.Y + bounds.Height));

					//pivot position
					if (allowPivot)
					{
						RectangleF pivotBounds = ToUnscaledScreenRegion(_selectedObject);
						PointF pivot = new PointF(pivotBounds.X + _selectedObject.PivotX / _selectedObject.Width * pivotBounds.Width, pivotBounds.Y + _selectedObject.PivotY / _selectedObject.Height * pivotBounds.Height);

						//pivoting - hovering over the pivot circle
						if (_selectedDirective?.DirectiveType == "sprite")
						{
							float px = Math.Abs(screenPt.X - pivot.X);
							float py = Math.Abs(screenPt.Y - pivot.Y);
							if (px <= SelectionLeeway && py <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.Pivot;
							}
						}
					}

					if (allowRotate)
					{
						//rotating - hovering outside a corner
						if (screenPt.X < bounds.X - SelectionLeeway && screenPt.X >= bounds.X - RotationLeeway && dt <= RotationLeeway ||
							screenPt.Y < bounds.Y - SelectionLeeway && screenPt.Y >= bounds.Y - RotationLeeway && dl <= RotationLeeway ||
							screenPt.X > bounds.X + bounds.Width + SelectionLeeway && screenPt.X <= bounds.X + bounds.Width + RotationLeeway && dt <= RotationLeeway ||
							screenPt.Y < bounds.Y - SelectionLeeway && screenPt.Y >= bounds.Y - RotationLeeway && dr <= RotationLeeway ||
							screenPt.X < bounds.X - SelectionLeeway && screenPt.X >= bounds.X - RotationLeeway && db <= RotationLeeway ||
							screenPt.Y > bounds.Y + bounds.Height + SelectionLeeway && screenPt.Y <= bounds.Y + bounds.Height + RotationLeeway && dl <= RotationLeeway ||
							screenPt.X > bounds.X + bounds.Width + SelectionLeeway && screenPt.X <= bounds.X + bounds.Width + RotationLeeway && db <= RotationLeeway ||
							screenPt.Y > bounds.Y + bounds.Height + SelectionLeeway && screenPt.Y <= bounds.Y + bounds.Height + RotationLeeway && dr <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.Rotate;
						}
					}

					if (allowSkew && ModifierKeys.HasFlag(Keys.Shift))
					{
						//skewing - grabbing an edge while Shift is held down
						if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
						{
							if (dl <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.SkewLeft;
							}
							else if (dr <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.SkewRight;
							}
						}
						if (bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width)
						{
							if (dt <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.SkewTop;
							}
							else if (db <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.SkewBottom;
							}
						}
					}

					if (allowScale)
					{
						//scaling/stretching - grabbing an edge
						if (dl <= SelectionLeeway)
						{
							if (dt <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleTop | HoverContext.ScaleLeft;
							}
							else if (db <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleBottom | HoverContext.ScaleLeft;
							}
							else if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleLeft;
							}
						}

						if (dr <= SelectionLeeway)
						{
							if (dt <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleTop | HoverContext.ScaleRight;
							}
							else if (db <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleBottom | HoverContext.ScaleRight;
							}
							else if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleRight;
							}
						}

						if (dt <= SelectionLeeway && bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleTop;
						}

						if (db <= SelectionLeeway && bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleBottom;
						}
					}
				}
				else if (_selectedObject.ObjectType == SceneObjectType.Text)
				{
					if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
					{
						float dl = Math.Abs(screenPt.X - bounds.X);
						float dr = Math.Abs(screenPt.X - (bounds.X + bounds.Width));
						if (dl > SelectionLeeway && screenPt.X < bounds.X && dl <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowLeft;
						}
						else if (dl <= SelectionLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.SizeLeft;
						}
						if (dr <= RotationLeeway && dr > SelectionLeeway && screenPt.X > bounds.X + bounds.Width)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowRight;
						}
						else if (dr <= SelectionLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.SizeRight;
						}
					}
					if (bounds.X + bounds.Width / 4 <= screenPt.X && screenPt.X <= bounds.X + bounds.Width - bounds.Width / 4)
					{
						float dt = Math.Abs(screenPt.Y - bounds.Y);
						float db = Math.Abs(screenPt.Y - (bounds.Y + bounds.Height));
						if (dt > SelectionLeeway && (screenPt.Y < bounds.Y && dt <= RotationLeeway))
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowUp;
						}
						else if (db > SelectionLeeway && screenPt.Y >= bounds.Y + bounds.Height && db <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowDown;
						}
					}
				}

				if (bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width &&
					bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
				{
					return locked ? HoverContext.Locked : HoverContext.Drag;
				}
			}

			//If nothing else was selected, see if we should do anything with the camera
			Rectangle viewport = GetViewportBounds();
			if ((_selectedDirective == null || _selectedDirective.DirectiveType == "camera") && !_viewportLocked)
			{
				if (_selectedScene != null && !_selectedScene.Locked)
				{
					bool canStretch = (_selectedDirective == null);
					float dcl = Math.Abs(screenPt.X - viewport.X);
					float dcr = Math.Abs(screenPt.X - (viewport.X + viewport.Width));
					float dct = Math.Abs(screenPt.Y - viewport.Y);
					float dcb = Math.Abs(screenPt.Y - (viewport.Y + viewport.Height));
					if (dcl <= SelectionLeeway)
					{
						if (dct <= SelectionLeeway)
						{
							return HoverContext.CameraZoomTopLeft;
						}
						else if (dcb <= SelectionLeeway)
						{
							return HoverContext.CameraZoomBottomLeft;
						}
						else if (canStretch && viewport.Y <= screenPt.Y && screenPt.Y <= viewport.Y + viewport.Height)
						{
							return HoverContext.CameraSizeLeft;
						}
					}

					if (dcr <= SelectionLeeway)
					{
						if (dct <= SelectionLeeway)
						{
							return HoverContext.CameraZoomTopRight;
						}
						else if (dcb <= SelectionLeeway)
						{
							return HoverContext.CameraZoomBottomRight;
						}
						else if (canStretch && viewport.Y <= screenPt.Y && screenPt.Y <= viewport.Y + viewport.Height)
						{
							return HoverContext.CameraSizeRight;
						}
					}

					if (canStretch)
					{
						if (dct <= SelectionLeeway && viewport.X <= screenPt.X && screenPt.X <= viewport.X + viewport.Width)
						{
							return HoverContext.CameraSizeTop;
						}

						if (dcb <= SelectionLeeway && viewport.X <= screenPt.X && screenPt.X <= viewport.X + viewport.Width)
						{
							return HoverContext.CameraSizeBottom;
						}
					}
				}
			}

			//see if we're on top of an object
			SceneObject obj = GetObjectAtPoint(screenPt.X, screenPt.Y, _scenePreview.TextBoxes) ?? GetObjectAtPoint(screenPt.X, screenPt.Y, _scenePreview.Objects);
			if (obj != null && obj.ObjectType != SceneObjectType.Other)
			{
				if (obj.LinkedFrame != null && obj.LinkedFrame.Locked)
				{
					return HoverContext.None;
				}
				return HoverContext.Select;
			}

			if ((_selectedDirective == null || _selectedDirective.DirectiveType == "camera") && !_viewportLocked && !_selectedScene.Locked)
			{
				//finally see if we're within the camera viewport
				if (viewport.X <= screenPt.X && screenPt.X <= viewport.X + viewport.Width &&
					viewport.Y <= screenPt.Y && screenPt.Y <= viewport.Y + viewport.Height)
				{
					return HoverContext.CameraPan;
				}
			}

			return HoverContext.None;
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (_scenePreview == null) { return; }
			Point screenPt = new Point(e.X, e.Y);
			Point worldPt = ToWorldPoint(screenPt);
			if (_selectedObject?.ObjectType == SceneObjectType.Text)
			{
				//for text, use viewport coordinates instead of world
				worldPt = ToViewportPoint(screenPt);
			}

			lblCoord.Text = $"{worldPt}";

			switch (_mode)
			{
				case EditMode.Edit:
					switch (_state)
					{
						case CanvasState.Normal:
							HoverContext context = GetContext(screenPt);
							if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
								_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot)
							{
								canvas.Invalidate();
							}
							_moveContext = context;
							switch (context)
							{
								case HoverContext.ScaleTopLeft:
								case HoverContext.ScaleBottomRight:
								case HoverContext.CameraZoomTopLeft:
								case HoverContext.CameraZoomBottomRight:
									canvas.Cursor = Cursors.SizeNWSE;
									break;
								case HoverContext.ScaleTopRight:
								case HoverContext.ScaleBottomLeft:
								case HoverContext.CameraZoomTopRight:
								case HoverContext.CameraZoomBottomLeft:
									canvas.Cursor = Cursors.SizeNESW;
									break;
								case HoverContext.Drag:
								case HoverContext.CameraPan:
									canvas.Cursor = Cursors.SizeAll;
									break;
								case HoverContext.SizeLeft:
								case HoverContext.SizeRight:
								case HoverContext.CameraSizeLeft:
								case HoverContext.CameraSizeRight:
								case HoverContext.ScaleLeft:
								case HoverContext.ScaleRight:
									canvas.Cursor = Cursors.SizeWE;
									break;
								case HoverContext.SizeTop:
								case HoverContext.SizeBottom:
								case HoverContext.CameraSizeTop:
								case HoverContext.CameraSizeBottom:
								case HoverContext.ScaleTop:
								case HoverContext.ScaleBottom:
									canvas.Cursor = Cursors.SizeNS;
									break;
								case HoverContext.Select:
									canvas.Cursor = Cursors.Hand;
									break;
								case HoverContext.Pivot:
									canvas.Cursor = Cursors.Cross;
									break;
								case HoverContext.SkewTop:
								case HoverContext.SkewBottom:
									canvas.Cursor = Cursors.VSplit;
									break;
								case HoverContext.SkewLeft:
								case HoverContext.SkewRight:
									canvas.Cursor = Cursors.HSplit;
									break;
								case HoverContext.Locked:
									canvas.Cursor = Cursors.No;
									break;
								default:
									canvas.Cursor = Cursors.Default;
									break;
							}

							if (e.Button == MouseButtons.Left && context != HoverContext.None && context != HoverContext.Locked)
							{
								//start dragging
								if (HoverContext.Object.HasFlag(context))
								{
									_dragOffset = worldPt;
									_dragOffset.X -= (int)_selectedObject.X;
									_dragOffset.Y -= (int)_selectedObject.Y;

									switch (context)
									{
										case HoverContext.Drag:
											_state = CanvasState.MovingObject;
											break;
										case HoverContext.ScaleTopLeft:
										case HoverContext.ScaleTopRight:
										case HoverContext.ScaleBottomLeft:
										case HoverContext.ScaleBottomRight:
										case HoverContext.ScaleLeft:
										case HoverContext.ScaleTop:
										case HoverContext.ScaleRight:
										case HoverContext.ScaleBottom:
											//flip context according to the current scale
											if (_selectedObject.ScaleX < 0)
											{
												if (context.HasFlag(HoverContext.ScaleLeft))
												{
													context &= ~HoverContext.ScaleLeft;
													context |= HoverContext.ScaleRight;
												}
												else if (context.HasFlag(HoverContext.ScaleRight))
												{
													context &= ~HoverContext.ScaleRight;
													context |= HoverContext.ScaleLeft;
												}
											}
											if (_selectedObject.ScaleY < 0)
											{
												if (context.HasFlag(HoverContext.ScaleTop))
												{
													context &= ~HoverContext.ScaleTop;
													context |= HoverContext.ScaleBottom;
												}
												else if (context.HasFlag(HoverContext.ScaleBottom))
												{
													context &= ~HoverContext.ScaleBottom;
													context |= HoverContext.ScaleTop;
												}
											}
											_dragContext = context;
											_moveContext = context;
											_state = CanvasState.Scaling;
											break;
										case HoverContext.SizeLeft:
										case HoverContext.SizeTop:
										case HoverContext.SizeRight:
										case HoverContext.SizeBottom:
											_dragContext = context;
											_state = CanvasState.Resizing;
											break;
										case HoverContext.Rotate:
											_state = CanvasState.Rotating;
											break;
										case HoverContext.Pivot:
											_state = CanvasState.MovingPivot;
											break;
										case HoverContext.SkewLeft:
										case HoverContext.SkewRight:
										case HoverContext.SkewTop:
										case HoverContext.SkewBottom:
											_state = CanvasState.Skewing;
											break;
									}
								}
								else if (HoverContext.Camera.HasFlag(context))
								{
									_dragOffset = worldPt;
									_dragOffset.X -= (int)_scenePreview.X;
									_dragOffset.Y -= (int)_scenePreview.Y;

									switch (context)
									{
										case HoverContext.CameraPan:
											_state = CanvasState.MovingCamera;
											break;
										case HoverContext.CameraSizeLeft:
										case HoverContext.CameraSizeRight:
										case HoverContext.CameraSizeTop:
										case HoverContext.CameraSizeBottom:
											_dragContext = context;
											_state = CanvasState.ResizingCamera;
											break;
										case HoverContext.CameraZoomTopLeft:
										case HoverContext.CameraZoomTopRight:
										case HoverContext.CameraZoomBottomLeft:
										case HoverContext.CameraZoomBottomRight:
											_dragContext = context;
											_state = CanvasState.ZoomingCamera;
											break;
									}
								}
							}
							break;
						case CanvasState.MovingObject:
							Point newPt = new Point(worldPt.X - _dragOffset.X, worldPt.Y - _dragOffset.Y);
							if (_selectedObject.AdjustPosition(newPt.X, newPt.Y, _scenePreview))
							{
								//This is really ugly and a good sign that we're way overdue for proper data binding.
								//But anyway, this is to force the properties in the table to reflect the new values to keep them in sync and not fight
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("X");
									propertyTable.UpdateProperty("Y");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.MovingCamera:
							newPt = new Point(worldPt.X - _dragOffset.X, worldPt.Y - _dragOffset.Y);
							if (_scenePreview.AdjustPosition(newPt.X, newPt.Y, _scenePreview))
							{
								treeScenes.UpdateNode(_scenePreview.LinkedFrame != null ? (object)_scenePreview.LinkedFrame : _selectedScene);
								if (_selectedScene == propertyTable.Data || _scenePreview?.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("X");
									propertyTable.UpdateProperty("Y");
								}
								canvas.Invalidate();
								if (_viewportLocked)
								{
									FitToCamera();
								}
							}
							break;
						case CanvasState.MovingPivot:
							//figure out where new pivot position is in relation to object bounds
							if (_selectedObject.AdjustPivot(screenPt, ToUnscaledScreenRegion(_selectedObject)))
							{
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("PivotX");
									propertyTable.UpdateProperty("PivotY");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Resizing:
							if (_selectedObject.AdjustSize(worldPt, _dragContext, _scenePreview))
							{
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									switch (_dragContext)
									{
										case HoverContext.SizeRight:
											propertyTable.UpdateProperty("Width");
											break;
										case HoverContext.SizeLeft:
											propertyTable.UpdateProperty("X");
											propertyTable.UpdateProperty("Width");
											break;
										case HoverContext.SizeBottom:
											propertyTable.UpdateProperty("Height");
											break;
										case HoverContext.SizeTop:
											propertyTable.UpdateProperty("Y");
											propertyTable.UpdateProperty("Height");
											break;
									}
								}

								canvas.Invalidate();
							}
							break;
						case CanvasState.ResizingCamera:
							if (_scenePreview.AdjustSize(worldPt, _dragContext, _scenePreview))
							{
								treeScenes.UpdateNode(_selectedScene);
								if (_selectedScene == propertyTable.Data || _scenePreview?.LinkedFrame == propertyTable.Data)
								{
									switch (_dragContext)
									{
										case HoverContext.CameraSizeRight:
											propertyTable.UpdateProperty("Width");
											break;
										case HoverContext.CameraSizeLeft:
											propertyTable.UpdateProperty("X");
											propertyTable.UpdateProperty("Width");
											break;
										case HoverContext.CameraSizeBottom:
											propertyTable.UpdateProperty("Height");
											break;
										case HoverContext.CameraSizeTop:
											propertyTable.UpdateProperty("Y");
											propertyTable.UpdateProperty("Height");
											break;
									}
								}
								RebuildTextBoxes();

								canvas.Invalidate();
							}
							break;
						case CanvasState.Scaling:
							if (_selectedObject.AdjustScale(worldPt, _downPoint, _moveContext, ModifierKeys.HasFlag(Keys.Shift)))
							{
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("ScaleX");
									propertyTable.UpdateProperty("ScaleY");
								}
								RebuildTextBoxes();
								canvas.Invalidate();
							}
							break;
						case CanvasState.ZoomingCamera:
							if (_scenePreview.AdjustScale(worldPt, _downPoint, _moveContext, true))
							{
								treeScenes.UpdateNode(_selectedScene);
								if (_selectedScene == propertyTable.Data || _scenePreview?.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("Zoom");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Rotating:
							if (_selectedObject.AdjustRotation(worldPt))
							{
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("Rotation");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Skewing:
							if (_selectedObject.AdjustSkew(worldPt, _downPoint, _moveContext))
							{
								treeScenes.UpdateNode(_selectedObject.LinkedFrame);
								if (_selectedObject.LinkedFrame == propertyTable.Data)
								{
									propertyTable.UpdateProperty("SkewX");
									propertyTable.UpdateProperty("SkewY");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Panning:
							if (_viewportLocked)
							{
								break;
							}
							int dx = screenPt.X - _lastMouse.X;
							int dy = screenPt.Y - _lastMouse.Y;
							if (dx != 0 || dy != 0)
							{
								if (dx != 0)
								{
									int moveX = (int)(dx / ZoomLevel);
									if (moveX == 0)
									{
										moveX = (dx < 0 ? -1 : 1);
									}
									_canvasOffset.X += moveX;
								}
								if (dy != 0)
								{
									int moveY = (int)(dy / ZoomLevel);
									if (moveY == 0)
									{
										moveY = (dy < 0 ? -1 : 1);
									}
									_canvasOffset.Y += moveY;
								}
								canvas.Invalidate();
							}
							break;
					}
					_lastMouse = screenPt;

					if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
						_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot)
					{
						canvas.Invalidate();
					}
					break;
				case EditMode.Playback:
					canvas.Cursor = Cursors.Hand;
					break;
			}
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (_scenePreview == null) { return; }
			_moveContext = HoverContext.None;
			canvas.Cursor = Cursors.Default;
			switch (_state)
			{
				case CanvasState.MovingObject:
				case CanvasState.Resizing:
				case CanvasState.Rotating:
				case CanvasState.Scaling:
				case CanvasState.Panning:
				case CanvasState.MovingCamera:
				case CanvasState.ResizingCamera:
				case CanvasState.ZoomingCamera:
				case CanvasState.MovingPivot:
				case CanvasState.Skewing:
					_state = CanvasState.Normal;
					canvas.Invalidate();
					break;
			}
		}

		private void SliderZoom_ValueChanged(object sender, EventArgs e)
		{
			float zoom = sliderZoom.Value;
			int max = sliderZoom.Maximum;

			float amount = zoom / max;

			ZoomLevel = amount * (MaxZoom - MinZoom) + MinZoom;
		}

		/// <summary>
		/// Updates the screen to reflect a new zoom level
		/// </summary>
		private void UpdateZoomLevel()
		{
			lblZoom.Text = ZoomLevel.ToString("0.00") + "x";
			float t = (ZoomLevel - MinZoom) / (MaxZoom - MinZoom);
			int tick = (int)(t * sliderZoom.Maximum);
			sliderZoom.ValueChanged -= SliderZoom_ValueChanged;
			sliderZoom.Value = tick;
			sliderZoom.ValueChanged += SliderZoom_ValueChanged;
			canvas.Invalidate();
		}

		private void Canvas_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				treeScenes.DeleteSelectedNode();
			}
			else if (e.KeyCode == Keys.Down)
			{
				MoveSelectedObject(0, 1);
			}
			else if (e.KeyCode == Keys.Up)
			{
				MoveSelectedObject(0, -1);
			}
			else if (e.KeyCode == Keys.Left)
			{
				MoveSelectedObject(-1, 0);
			}
			else if (e.KeyCode == Keys.Right)
			{
				MoveSelectedObject(1, 0);
			}
		}

		private void TreeScenes_AfterSelect(object sender, SceneTreeEventArgs e)
		{
			EnableDirectivePlayback(false);
			propertyTable.Save();

			Scene oldScene = _selectedScene;
			_selectedScene = e.Scene;
			_selectedDirective = e.Directive;
			_selectedKeyframe = e.Keyframe;

			propertyTable.Context = new EpilogueContext(_character, _epilogue, _selectedScene);

			if (e.Keyframe != null)
			{
				propertyTable.RecordFilter = DirectiveFilter;
				propertyTable.Data = e.Keyframe;
			}
			else if (e.Directive != null)
			{
				propertyTable.RecordFilter = DirectiveFilter;
				propertyTable.Data = e.Directive;
			}
			else if (e.Scene != null)
			{
				propertyTable.RecordFilter = SceneFilter;
				propertyTable.Data = e.Scene;
			}
			else
			{
				propertyTable.RecordFilter = null;
				propertyTable.Data = null;
			}
			propertyTable.RunFilter(HideRow);

			BuildScene(false);
			if (_selectedScene != oldScene)
			{
				FitToScreen();
			}
		}

		private bool DirectiveFilter(PropertyRecord record)
		{
			if (_selectedDirective == null)
			{
				return true;
			}

			DirectiveDefinition def = Definitions.Instance.Get<DirectiveDefinition>(_selectedDirective.DirectiveType);
			if (def == null)
			{
				return false;
			}

			bool allowed = def.AllowsProperty(record.Key);
			if (!allowed)
			{
				return false;
			}
			if (_selectedKeyframe == null && _selectedDirective.Keyframes.Count > 0)
			{
				if (def.RequiresAnimatedProperty(record.Key))
				{
					return false;
				}
			}
			return true;
		}

		private static readonly string[] TransitionProperties = new string[] { "effect", "ease", "time" }; //hardcoding allowed properties for now since I don't expect this to change
		private bool SceneFilter(PropertyRecord record)
		{
			if (_selectedScene == null)
			{
				return true;
			}

			string key = record.Key;

			if (key == "color")
			{
				return true;
			}

			for (int i = 0; i < TransitionProperties.Length; i++)
			{
				if (TransitionProperties[i] == key)
				{
					if (_selectedScene.Transition)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return !_selectedScene.Transition;
		}

		private void propertyTable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			treeScenes.UpdateNode(propertyTable.Data);
			BuildScene(false);
			if (e.PropertyName == "Id")
			{
				propertyTable.RunFilter(HideRow);
			}
		}

		private bool HideRow(PropertyRecord record, object data, object context)
		{
			if (_selectedDirective == null || record.Key == "id")
			{
				return true;
			}
			DirectiveDefinition def = Definitions.Instance.Get<DirectiveDefinition>(_selectedDirective.DirectiveType);
			if (def == null || !def.FilterPropertiesById)
			{
				return true;
			}

			string id = _selectedDirective.Id;
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}

			string closestMatch = "";
			string matchType = "";

			for (int i = 0; i < _selectedScene.Directives.Count; i++)
			{
				Directive directive = _selectedScene.Directives[i];
				if (directive == _selectedDirective)
				{
					if (matchType.Length == 0)
					{
						return false; //nothing previously defined the ID, so we have no idea what this type is supposed to be
					}
					else
					{
						//hardcoded list for now. may want to move this to the DirectiveDefinition
						string key = record.Key;
						if (key == "rate" || key == "counter")
						{
							return matchType == "emitter";
						}
						else if (key == "scalex" || key == "scaley" || key == "alpha")
						{
							return matchType == "sprite";
						}
						else
						{
							return true; //other properties are assumed to be globally available
						}
					}
				}

				if (!string.IsNullOrEmpty(directive.Id) && directive.Id.StartsWith(id) && directive.Id.Length > closestMatch.Length)
				{
					closestMatch = directive.Id;
					matchType = directive.DirectiveType;
				}
			}

			return false; //if no ID is available, don't make anything available yet
		}

		/// <summary>
		/// Populates the canvas with the current state of the scene
		/// </summary>
		private void BuildScene(bool previewMode)
		{
			_selectedObject = null;
			_selectedAnimation = null;
			_animations.Clear();
			_scenePreview?.Dispose();
			_sceneTransition = null;
			_pendingDirectives.Clear();

			canvas.Invalidate();

			if (_selectedScene == null)
			{
				_scenePreview = null;
				return;
			}

			if (_selectedScene.Transition)
			{
				_scenePreview = null;
				_sceneTransition = new SceneTransition(_selectedScene, canvas.Width, canvas.Height);
				return;
			}

			_scenePreview = new ScenePreview(_selectedScene, _character, _markers);
			_overlay = new SceneObject(_scenePreview, _character, null, null, null);
			_overlay.Id = "fade";
			_overlay.SetColor(_epilogue, _selectedScene);
			_scenePreview.AddObject(new SceneObject(_scenePreview, _character, "background", _selectedScene.Background, _selectedScene.BackgroundColor));

			if (!previewMode)
			{
				//iterate up to the selected point in the timeline
				bool readyToStop = _selectedDirective == null;
				foreach (Directive d in _selectedScene.Directives)
				{
					if (readyToStop && d.DirectiveType != "sprite" && d.DirectiveType != "text" && d.DirectiveType != "emitter")
					{
						break;
					}
					if (readyToStop && d.DirectiveType == "text" && d.Id == _selectedDirective?.Id)
					{
						break;
					}
					ApplyDirective(d);
					if (d == _selectedDirective)
					{
						//found the directive; keep going until the next non-sprite or text directive
						readyToStop = true;
					}
				}

				if (_selectedDirective != null)
				{
					string id = _selectedDirective.Id;
					//select the object corresponding to the selected directive
					_selectedObject = _scenePreview.Objects.Find(obj => (!string.IsNullOrEmpty(id) && obj.Id == id) || obj.LinkedFrame == _selectedDirective);
					if (_selectedObject == null)
					{
						_selectedObject = _scenePreview.TextBoxes.Find(obj => (!string.IsNullOrEmpty(id) && obj.Id == id) || obj.LinkedFrame == _selectedDirective);
					}
				}
			}

			if (_viewportLocked)
			{
				FitToCamera();
			}
		}

		private void PerformDirective()
		{
			_directiveIndex++;
			if (_directiveIndex < _selectedScene.Directives.Count)
			{
				Directive directive = _selectedScene.Directives[_directiveIndex];
				if (!string.IsNullOrEmpty(directive.Delay) && directive.Delay != "0")
				{
					float delay;
					float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out delay);
					delay *= 1000;
					PendDirective(directive, delay);
				}
				else if (!ApplyDirective(directive))
				{
					return;
				}
				PerformDirective();
			}
		}

		/// <summary>
		/// Applies a directive to the scene
		/// </summary>
		/// <param name="directive"></param>
		private bool ApplyDirective(Directive directive)
		{
			SceneObject obj = null;
			if (directive.Id != null)
			{
				obj = _scenePreview.Objects.Find(o => o.Id == directive.Id);
			}
			switch (directive.DirectiveType)
			{
				case "sprite":
					_scenePreview.AddObject(new SceneObject(_scenePreview, _character, directive));
					canvas.Invalidate();
					break;
				case "emitter":
					_scenePreview.AddObject(new SceneEmitter(_scenePreview, _character, directive));
					canvas.Invalidate();
					break;
				case "remove":
					if (obj != null)
					{
						_scenePreview.Objects.Remove(obj);
						obj.Dispose();
					}
					canvas.Invalidate();
					break;
				case "fade":
					obj = _overlay;
					if (_selectedKeyframe == null)
					{
						Keyframe link = _selectedScene.GetLastFrame("fade", directive);
						if (link == directive)
						{
							link = null;
						}
						obj.LastFrame = link;
						if (directive.Keyframes.Count > 0)
						{
							obj.LinkedFrame = directive.Keyframes[0];
						}
						else
						{
							obj.LinkedFrame = directive;
						}
					}
					else if (directive == _selectedDirective)
					{
						int index = directive.Keyframes.IndexOf(_selectedKeyframe);
						if (index > 0)
						{
							obj.LastFrame = directive.Keyframes[index - 1];
						}
						else
						{
							obj.LastFrame = _selectedScene.GetLastFrame("fade", directive);
						}
						obj.LinkedFrame = _selectedKeyframe;
					}
					AddAnimation(obj, directive);
					obj.Update(directive, _scenePreview);
					canvas.Invalidate();
					break;
				case "text":
					//kill any old textbox with the ID
					SceneObject old = _scenePreview.TextBoxes.Find(b => !string.IsNullOrEmpty(b.Id) && b.Id == directive.Id);
					if (old != null)
					{
						_scenePreview.TextBoxes.Remove(old);
					}
					_scenePreview.TextBoxes.Add(new SceneObject(_scenePreview, _character, directive));
					canvas.Invalidate();
					break;
				case "move":
					if (obj != null)
					{
						if (_selectedKeyframe == null)
						{
							Keyframe link = _selectedScene.GetLastFrame(obj.Id, directive);
							obj.LastFrame = link;
							if (directive.Keyframes.Count > 0)
							{
								obj.LinkedFrame = directive.Keyframes[0];
							}
							else
							{
								obj.LinkedFrame = directive;
							}
						}
						else if (directive == _selectedDirective)
						{
							int index = directive.Keyframes.IndexOf(_selectedKeyframe);
							if (index > 0)
							{
								obj.LastFrame = directive.Keyframes[index - 1];
							}
							else
							{
								obj.LastFrame = _selectedScene.GetLastFrame(obj.Id, directive);
							}
							obj.LinkedFrame = _selectedKeyframe;
						}
						AddAnimation(obj, directive);
						obj.Update(directive, _scenePreview);
					}
					break;
				case "camera":
					obj = _scenePreview;
					if (_selectedKeyframe == null)
					{
						Keyframe link = _selectedScene.GetLastFrame("camera", directive);
						if (link == directive)
						{
							link = null;
						}
						_scenePreview.LastFrame = link;
						if (directive.Keyframes.Count > 0)
						{
							_scenePreview.LinkedFrame = directive.Keyframes[0];
						}
						else
						{
							_scenePreview.LinkedFrame = directive;
						}
					}
					else if (directive == _selectedDirective)
					{
						int index = directive.Keyframes.IndexOf(_selectedKeyframe);
						if (index > 0)
						{
							_scenePreview.LastFrame = directive.Keyframes[index - 1];
						}
						else
						{
							_scenePreview.LastFrame = _selectedScene.GetLastFrame("camera", directive);
							if (_scenePreview.LastFrame == directive)
							{
								_scenePreview.LastFrame = null;
							}
						}
						_scenePreview.LinkedFrame = _selectedKeyframe;
					}
					AddAnimation(_scenePreview, directive);
					_scenePreview.Update(directive, _scenePreview);
					break;
				case "wait":
					if (_pendingDirectives.Count > 0)
					{
						_waitingForAnims = true;
					}
					for (int i = 0; i < _animations.Count; i++)
					{
						if (!_animations[i].Looped || _animations[i].Iterations > 0)
						{
							_waitingForAnims = true;
							break;
						}
					}
					return !_waitingForAnims;
				case "pause":
					return false;
				case "stop":
					string id = directive.Id;
					for (int i = _animations.Count - 1; i >= 0; i--)
					{
						SceneAnimation anim = _animations[i];
						if (anim.Id == id)
						{
							anim.Halt();
							_animations.RemoveAt(i);
						}
					}
					break;
				case "clear":
					SceneObject textBox = _scenePreview.TextBoxes.Find(t => t.Id == directive.Id);
					_scenePreview.TextBoxes.Remove(textBox);
					break;
				case "clear-all":
					_scenePreview.TextBoxes.Clear();
					break;
				case "emit":
					SceneEmitter emitter = _scenePreview.Objects.Find(o => o.Id == directive.Id && o.ObjectType == SceneObjectType.Emitter) as SceneEmitter;
					if (emitter != null && (directive == _selectedDirective || _mode == EditMode.Playback))
					{
						for (int i = 0; i < directive.Count; i++)
						{
							emitter.Emit();
						}
					}
					break;
			}

			if (_mode == EditMode.Edit)
			{
				if (obj != null)
				{
					if (((_selectedKeyframe != null && _selectedDirective == directive) || _selectedDirective != directive))
					{
						//Apply keyframes up to the selected one too if either the keyframe is in this directive, or this isn't the selected directive
						for (int i = 0; i < directive.Keyframes.Count; i++)
						{
							Keyframe frame = directive.Keyframes[i];
							obj.Update(frame, _scenePreview);
							if (frame == _selectedKeyframe)
							{
								break;
							}
						}
					}
					else if (_selectedKeyframe == null && _selectedDirective == directive && directive.Keyframes.Count > 0)
					{
						//if selecting a root directive with keyframes, also apply the first keyframe
						obj.Update(directive.Keyframes[0], _scenePreview);
					}
				}
			}
			return true;
		}

		private class PendedDirective : IComparable<PendedDirective>
		{
			public long TriggerTime;
			public Directive Directive;

			public PendedDirective(Directive directive, float delay)
			{
				TriggerTime = DateTime.Now.AddMilliseconds(delay).Ticks;
				Directive = directive;
			}

			public int CompareTo(PendedDirective other)
			{
				return TriggerTime.CompareTo(other.TriggerTime);
			}
		}

		private void PendDirective(Directive directive, float delay)
		{
			PendedDirective info = new PendedDirective(directive, delay);
			_pendingDirectives.Add(info);
			_pendingDirectives.Sort();
		}

		/// <summary>
		/// Resizes textboxes in the screen to reflect changes to the viewport
		/// </summary>
		private void RebuildTextBoxes()
		{
			for (int i = 0; i < _scenePreview.TextBoxes.Count; i++)
			{
				_scenePreview.TextBoxes[i] = new SceneObject(_scenePreview, _character, _scenePreview.TextBoxes[i].LinkedFrame as Directive);
			}
		}

		/// <summary>
		/// Creates an animation from a directive
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="directive"></param>
		private void AddAnimation(SceneObject obj, Directive directive)
		{
			float time;
			if (!string.IsNullOrEmpty(directive.Time))
			{
				if (!float.TryParse(directive.Time, NumberStyles.Float, CultureInfo.InvariantCulture, out time))
				{
					return;
				}
			}

			SceneAnimation animation = new SceneAnimation(obj, directive, _scenePreview, _mode == EditMode.Playback);
			if (_selectedDirective == directive)
			{
				_selectedAnimation = animation;
			}
			if (_mode == EditMode.Playback || (_selectedDirective == directive && _selectedKeyframe == null))
			{
				obj.LinkedAnimation = animation;
			}
			foreach (Keyframe kf in directive.Keyframes)
			{
				if (_selectedKeyframe == kf)
				{
					obj.LinkedAnimation = animation;
					break;
				}
			}

			_animations.Add(animation);
		}

		private void cmdRecenter_Click(object sender, EventArgs e)
		{
			FitToScreen();
		}

		private void cmdLock_Click(object sender, EventArgs e)
		{
			LockViewport();
		}

		private void LockViewport()
		{
			_viewportLocked = cmdLock.Checked;
			sliderZoom.Enabled = !_viewportLocked;
			if (_viewportLocked)
			{
				_prelockOffset = _canvasOffset;
				_prelockZoom = ZoomLevel;
				FitToCamera();
			}
			else
			{
				_canvasOffset = _prelockOffset;
				ZoomLevel = _prelockZoom;
			}
			canvas.Invalidate();
		}

		/// <summary>
		/// Adjusts the zoom level so that the whole scene if possible
		/// </summary>
		private void FitToScreen()
		{
			if (_scenePreview == null) { return; }
			_canvasOffset = new Point(0, 0);

			//determine an appropriate zoom level to fit everything
			float canvasWidth = canvas.Width;

			float zoom = canvasWidth / _scenePreview.Width;
			ZoomLevel = Math.Max(MinZoom, Math.Min(MaxZoom, zoom));

			//center on camera
			Point desiredCenter = new Point(canvas.Width / 2, canvas.Height / 2);
			_canvasOffset = new Point(0, 0);

			//get camera's position
			int cx = (int)(_scenePreview.X + _scenePreview.Width / 2);
			int cy = (int)(_scenePreview.Y + _scenePreview.Height / 2);

			float width = _scenePreview.Width / _scenePreview.Zoom / 2;
			float height = _scenePreview.Height / _scenePreview.Zoom / 2;

			float l = cx - width;
			float t = cy - height;

			Point worldSize = ToWorldPoint(new Point(canvas.Width, canvas.Height));
			_canvasOffset = new Point((int)(-l + worldSize.X / 2 - width), (int)(-t + worldSize.Y / 2 - height));

			//float sceneWidth = _scenePreview.Width * ZoomLevel;
			//if (sceneWidth < canvasWidth)
			//{
			//	_canvasOffset.X = (int)((canvasWidth - sceneWidth) / 2.0f / ZoomLevel);
			//}

			//float canvasHeight = canvas.Height;
			//float sceneHeight = _scenePreview.Height * ZoomLevel;
			//if (sceneHeight < canvasHeight)
			//{
			//	_canvasOffset.Y = (int)((canvasHeight - sceneHeight) / 2.0f / ZoomLevel);
			//}

			canvas.Invalidate();
		}

		/// <summary>
		/// Adjusts the zoom and scroll so that the camera viewport covers the whole screen
		/// </summary>
		private void FitToCamera()
		{
			if (_scenePreview == null) { return; }

			//adjust zoom level
			int windowWidth = canvas.Width;
			int windowHeight = canvas.Height;

			float aspectRatio = _scenePreview.AspectRatio;
			float viewWidth = aspectRatio * windowHeight;

			int viewportHeight = 0;
			if (viewWidth > windowWidth)
			{
				//take full width of window
				viewportHeight = (int)(windowWidth / aspectRatio);
			}
			else
			{
				//take full height of window
				viewportHeight = windowHeight;
			}

			//set the zoom to match the viewport height
			float zoom = viewportHeight * _scenePreview.Zoom / _scenePreview.Height;
			ZoomLevel = zoom;

			//center on camera
			Point desiredCenter = new Point(windowWidth / 2, windowHeight / 2);
			_canvasOffset = new Point(0, 0);

			//get camera's position
			int cx = (int)(_scenePreview.X + _scenePreview.Width / 2);
			int cy = (int)(_scenePreview.Y + _scenePreview.Height / 2);

			float width = _scenePreview.Width / _scenePreview.Zoom / 2;
			float height = _scenePreview.Height / _scenePreview.Zoom / 2;

			float l = cx - width;
			float t = cy - height;

			Point worldSize = ToWorldPoint(new Point(windowWidth, windowHeight));
			_canvasOffset = new Point((int)(-l + worldSize.X / 2 - width), (int)(-t + worldSize.Y / 2 - height));
		}

		private void canvas_Resize(object sender, EventArgs e)
		{
			if (_viewportLocked)
			{
				FitToCamera();
			}
		}

		private void UpdateArrowPosition()
		{
			if (_selectedObject?.ObjectType != SceneObjectType.Text)
			{
				return;
			}
			string position = "";
			switch (_moveContext)
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
			_selectedObject.Arrow = position;
			(_selectedObject.LinkedFrame as Directive).Arrow = position;
			propertyTable.UpdateProperty("Arrow");
		}

		private void tmrPlay_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;
			if (_sceneTransition != null)
			{
				_sceneTransition.Update(elapsedSec);
				canvas.Invalidate();
			}
			else
			{
				long ticks = now.Ticks;
				for (int i = 0; i < _pendingDirectives.Count; i++)
				{
					PendedDirective info = _pendingDirectives[i];
					if (info.TriggerTime <= ticks)
					{
						ApplyDirective(info.Directive);
						_pendingDirectives.RemoveAt(i);
						i--;
					}
				}

				//updates can add to the object collection and resort it, which will royally screw up this loop, so copy off first, as inefficient as that is
				List<SceneObject> objects = new List<SceneObject>();
				objects.AddRange(_scenePreview.Objects);

				for (int i = 0; i < objects.Count; i++)
				{
					SceneObject obj = objects[i];
					obj.UpdateTick(elapsedSec, _scenePreview);
					if (obj.ObjectType == SceneObjectType.Emitter)
					{
						canvas.Invalidate();
					}
				}
				UpdateAnimations(elapsedSec);
				if (_viewportLocked)
				{
					FitToCamera();
				}
			}
		}

		private void UpdateAnimations(float elapsedSec)
		{
			int nonLoopingCount = _pendingDirectives.Count;

			if (_mode == EditMode.Playback)
			{
				for (int i = _animations.Count - 1; i >= 0; i--)
				{
					SceneAnimation anim = _animations[i];
					anim.Update(elapsedSec);
					if (anim.IsComplete)
					{
						_animations.RemoveAt(i);
					}
					else
					{
						if (!anim.Looped)
						{
							nonLoopingCount++;
						}
					}
				}
				if (nonLoopingCount == 0 && _waitingForAnims)
				{
					AdvanceDirective();
				}
			}
			else
			{
				_selectedAnimation?.Update(elapsedSec);
			}
			canvas.Invalidate();
		}

		private void cmdPlayDirective_Click(object sender, EventArgs e)
		{
			_lastTick = DateTime.Now;
			EnableDirectivePlayback(!tmrPlay.Enabled);
		}

		private void EnableDirectivePlayback(bool enabled)
		{
			tmrPlay.Enabled = enabled;
			foreach (SceneAnimation animation in _animations)
			{
				animation.Elapsed = 0;
			}
			cmdPlayDirective.ToolTipText = enabled ? "Stop animation" : "Play selection animation";
			cmdPlayDirective.Image = enabled ? Resources.PlaybackPause : Resources.Playback;
			canvas.Invalidate();
		}

		private void cmdPlay_Click(object sender, EventArgs e)
		{
			EnablePlaybackMode(_mode == EditMode.Edit);
		}

		private void EnablePlaybackMode(bool enabled)
		{
			_mode = enabled ? EditMode.Playback : EditMode.Edit;
			cmdPlay.Image = (enabled ? Resources.Stop : Resources.Play);
			splitContainer1.Panel1Collapsed = enabled;
			if (enabled)
			{
				_lockedBeforePlayback = _viewportLocked;
			}
			cmdLock.Checked = _lockedBeforePlayback || enabled;
			cmdLock.Enabled = !enabled;
			cmdPlayDirective.Enabled = !enabled;
			cmdFit.Enabled = !enabled;
			LockViewport();
			canvas.Cursor = Cursors.Default;
			BuildScene(enabled);
			if (enabled)
			{
				EnableDirectivePlayback(false);
				_directiveIndex = -1;
				PerformDirective();
			}

			_lastTick = DateTime.Now;
			tmrPlay.Enabled = enabled;
		}

		private void AdvanceDirective()
		{
			HaltAnimations(false);

			if (_directiveIndex >= _selectedScene.Directives.Count)
			{
				//exit playback mode
				EnablePlaybackMode(false);
			}
			else
			{
				PerformDirective();
			}
		}

		private void HaltAnimations(bool haltLooping)
		{
			foreach (PendedDirective info in _pendingDirectives)
			{
				ApplyDirective(info.Directive);
			}
			_pendingDirectives.Clear();
			_waitingForAnims = false;
			for (int i = 0; i < _animations.Count; i++)
			{
				if (haltLooping || !_animations[i].Looped)
				{
					_animations[i].Halt();
					_animations.RemoveAt(i--);
				}
			}
		}

		public void MoveSelectedObject(int x, int y)
		{
			if (_selectedObject != null)
			{
				if (_selectedObject.AdjustPosition((int)_selectedObject.X + x, (int)_selectedObject.Y + y, _scenePreview))
				{
					treeScenes.UpdateNode(_selectedObject.LinkedFrame);
					if (_selectedObject.LinkedFrame == propertyTable.Data)
					{
						propertyTable.UpdateProperty("X");
						propertyTable.UpdateProperty("Y");
					}
					canvas.Invalidate();
				}
			}
		}

		private void cmdToggleFade_Click(object sender, EventArgs e)
		{
			_showOverlay = !_showOverlay;
			canvas.Invalidate();
		}

		private void cmdMarkers_Click(object sender, EventArgs e)
		{
			MarkerSetup form = new MarkerSetup();
			form.SetData(_character, _markers);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_markers = form.Markers;
				BuildScene(false);
			}
		}
	}

	public class EpilogueContext : IAutoCompleteList, ICharacterContext
	{
		public ISkin Character { get; set; }
		public CharacterContext Context { get; private set; }
		public Epilogue Epilogue { get; set; }
		public Scene Scene { get; set; }

		public EpilogueContext(Character character, Epilogue epilogue, Scene scene)
		{
			Character = character;
			Epilogue = epilogue;
			Scene = scene;
			Context = CharacterContext.Epilogue;
		}

		public override string ToString()
		{
			return $"{Character} {Epilogue} {Scene}";
		}

		public string[] GetAutoCompleteList(object data)
		{
			Directive dir = data as Directive;
			if (dir == null)
			{
				return null;
			}
			HashSet<string> sourceType = new HashSet<string>();
			bool allowCamera = false;
			bool allowFade = false;
			if (dir.DirectiveType == "move" || dir.DirectiveType == "stop" || dir.DirectiveType == "remove" || dir.DirectiveType == "emit")
			{
				sourceType.Add("sprite");
				sourceType.Add("emitter");

				if (dir.DirectiveType == "stop")
				{
					allowCamera = true;
					allowFade = true;
				}
			}
			else if (dir.DirectiveType == "clear")
			{
				sourceType.Add("text");
			}
			if (sourceType.Count == 0)
			{
				return null;
			}
			HashSet<string> items = new HashSet<string>();
			if (Scene != null)
			{
				if (allowCamera)
				{
					items.Add("camera");
				}
				if (allowFade)
				{
					items.Add("fade");
				}
				foreach (Directive d in Scene.Directives)
				{
					if (sourceType.Contains(d.DirectiveType))
					{
						string id = d.Id;
						if (!string.IsNullOrEmpty(id) && !items.Contains(id))
						{
							items.Add(id);
						}
					}
				}
			}
			if (items.Count > 0)
			{
				string[] list = new string[items.Count];
				int i = 0;
				foreach (string item in items)
				{
					list[i++] = item;
				}
				return list;
			}
			return null;
		}
	}

	[Flags]
	public enum HoverContext
	{
		None = 0,
		Drag = 1 << 0,
		SizeLeft = 1 << 1,
		SizeRight = 1 << 2,
		SizeTop = 1 << 3,
		SizeBottom = 1 << 4,
		ScaleLeft = 1 << 5,
		ScaleRight = 1 << 6,
		ScaleTop = 1 << 7,
		ScaleBottom = 1 << 8,
		ScaleBottomLeft = ScaleBottom | ScaleLeft,
		ScaleBottomRight = ScaleBottom | ScaleRight,
		ScaleTopLeft = ScaleTop | ScaleLeft,
		ScaleTopRight = ScaleTop | ScaleRight,
		Rotate = 1 << 9,
		ArrowUp = 1 << 10,
		ArrowLeft = 1 << 11,
		ArrowRight = 1 << 12,
		ArrowDown = 1 << 13,
		CameraSizeLeft = 1 << 14,
		CameraSizeRight = 1 << 15,
		CameraSizeTop = 1 << 16,
		CameraSizeBottom = 1 << 17,
		CameraZoomTopLeft = 1 << 18,
		CameraZoomTopRight = 1 << 19,
		CameraZoomBottomLeft = 1 << 20,
		CameraZoomBottomRight = 1 << 21,
		CameraPan = 1 << 22,
		Select = 1 << 23,
		Pivot = 1 << 24,
		Locked = 1 << 25,
		SkewLeft = 1 << 26,
		SkewRight = 1 << 27,
		SkewTop = 1 << 28,
		SkewBottom = 1 << 29,

		ScaleVertical = ScaleTop | ScaleBottom,
		ScaleHorizontal = ScaleLeft | ScaleRight,
		Object = Drag | SizeLeft | SizeRight | SizeTop | SizeBottom | Rotate |
			ArrowUp | ArrowDown | ArrowLeft | ArrowRight | Pivot | ScaleLeft | ScaleTop | ScaleRight | ScaleBottom | SkewLeft | SkewRight | SkewTop | SkewBottom,
		Camera = CameraPan | CameraSizeBottom | CameraSizeLeft | CameraSizeRight | CameraSizeTop | CameraZoomBottomLeft | CameraZoomBottomRight | CameraZoomTopLeft | CameraZoomTopRight,
		SkewVertical = SkewLeft | SkewRight,
		SkewHorizontal = SkewTop | SkewBottom,
	}

	public enum CanvasState
	{
		Normal,
		Panning,
		MovingObject,
		Scaling,
		Resizing,
		Rotating,
		Skewing,
		MovingCamera,
		ResizingCamera,
		ZoomingCamera,
		MovingPivot,
	}

	public enum EditMode
	{
		/// <summary>
		/// Can drag and tweak objects
		/// </summary>
		Edit,
		/// <summary>
		/// Playing a scene. Clicking will merely advance
		/// </summary>
		Playback
	}

}
