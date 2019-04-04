using Desktop;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Forms;
using SPNATI_Character_Editor.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public partial class LiveCanvas : UserControl
	{
		public const int SelectionLeeway = 5;
		public const int RotationLeeway = 30;

		private bool _recording;
		private bool _playing;
		private ISkin _character;
		private LivePose _pose;
		private List<string> _markers = new List<string>();

		private Point _lastMouse;
		private Point _canvasOffset = new Point(0, 0);
		private Point _downPoint = new Point(0, 0);
		private Point _startDragPosition = new Point(0, 0);
		private HoverContext _moveContext;
		private CanvasState _state = CanvasState.Normal;

		private Pen _penOuterSelection;
		private Pen _penInnerSelection;
		private Pen _penBoundary;
		private Pen _penKeyframe;

		private const int DefaultZoomIndex = 3;
		private int _zoomIndex = DefaultZoomIndex;
		private float[] _zoomLevels = new float[] { 0.25f, 0.5f, 0.75f, 1, 1.5f, 2, 2.5f, 3 };
		private float _zoom = 1;

		private float _time;
		private float _playbackTime;

		public UndoManager UndoManager;

		private LiveSprite _selectedObject;
		private LiveSprite _selectionSource;

		public event EventHandler<CanvasSelectionArgs> ObjectSelected;

		public LiveCanvas()
		{
			InitializeComponent();

			canvas.MouseWheel += Canvas_MouseWheel;
		}

		private void CleanUp()
		{
			if (_pose != null)
			{
				_pose.PropertyChanged -= _pose_PropertyChanged;
			}
		}

		public void SetData(ISkin character, LivePose pose)
		{
			_character = character;
			_pose = pose;
			if (_pose != null)
			{
				_pose.PropertyChanged += _pose_PropertyChanged;
			}
			canvas.Invalidate();

			_penBoundary = new Pen(Color.Gray, 1);
			_penBoundary.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			_penOuterSelection = new Pen(Brushes.White, 1);
			_penOuterSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			_penInnerSelection = new Pen(Brushes.Black, 1);
			_penInnerSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

			_penKeyframe = new Pen(Color.FromArgb(127, 255, 255, 255));
			_penKeyframe.Width = 2;

			UpdateTime(_time, _playbackTime);
		}

		public void SetPlayback(bool playing)
		{
			_playing = playing;
			UpdateTime(_time, _playbackTime);
		}

		private void _pose_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			UpdatePose();
		}

		public void UpdateTime(float time, float playbackTime)
		{
			_playbackTime = playbackTime;
			_time = time;
			UpdatePose();
		}

		private void UpdatePose()
		{
			if (_pose == null) { return; }
			_pose.UpdateTime(_playing ? _playbackTime : _time, _playing);
			_selectedObject?.Update(_time, false);
			canvas.Invalidate();
		}

		public void SelectData(object data)
		{
			if (_selectionSource != null)
			{
				DetachSourceListener();
			}

			LiveSprite sprite = data as LiveSprite;
			_selectionSource = sprite;
			canvas.Invalidate();

			if (_selectionSource != null)
			{
				AttachSourceListener();
			}
			CreateSelectionPreview();
		}

		private void CreateSelectionPreview()
		{
			if (_selectedObject != null)
			{
				DetachPreviewListener();
				_selectedObject = null;
			}
			if (_selectionSource == null) { return; }

			_selectedObject = _selectionSource.Copy();
			_selectedObject.Pose = _selectionSource.Pose;
			_selectedObject.Hidden = false;
			if (_selectionSource.Keyframes.Count > 0)
			{
				_selectedObject.Keyframes.Clear();
				foreach (LiveKeyframe kf in _selectionSource.Keyframes) //use the same keyframe references so we can modify them indirectly
				{
					_selectedObject.Keyframes.Add(kf);
				}
			}
			AttachPreviewListener();
			_selectedObject.Update(_time, false);
		}

		private void _selectedObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			DetachSourceListener();
			if (e.PropertyName == "PivotX")
			{
				_selectionSource.PivotX = _selectedObject.PivotX;
			}
			else if (e.PropertyName == "PivotY")
			{
				_selectionSource.PivotY = _selectedObject.PivotY;
			}
			AttachSourceListener();
		}

		private void AttachSourceListener()
		{
			_selectionSource.Keyframes.CollectionChanged += Source_CollectionChanged;
			_selectionSource.PropertyChanged += _selectionSource_PropertyChanged;
		}

		private void DetachSourceListener()
		{
			_selectionSource.Keyframes.CollectionChanged -= Source_CollectionChanged;
			_selectionSource.PropertyChanged -= _selectionSource_PropertyChanged;
		}

		private void AttachPreviewListener()
		{
			_selectedObject.PropertyChanged += _selectedObject_PropertyChanged;
			_selectedObject.Keyframes.CollectionChanged += Keyframes_CollectionChanged;
		}

		private void DetachPreviewListener()
		{
			_selectedObject.Keyframes.CollectionChanged -= Keyframes_CollectionChanged;
			_selectedObject.PropertyChanged -= _selectedObject_PropertyChanged;
		}

		private void _selectionSource_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Start")
			{
				_selectedObject.Start = _selectionSource.Start;
				_selectedObject?.Update(_time, false);
			}
			if (e.PropertyName == "PivotX")
			{
				_selectedObject.PivotX = _selectionSource.PivotX;
			}
			else if (e.PropertyName == "PivotY")
			{
				_selectedObject.PivotY = _selectionSource.PivotY;
			}
		}

		private void Keyframes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				DetachSourceListener();
				foreach (LiveKeyframe kf in e.NewItems)
				{
					_selectionSource.AddKeyframe(kf);
				}
				AttachSourceListener();
			}
		}

		private void Source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			CreateSelectionPreview();
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			if (_pose == null)
			{
				return;
			}

			Graphics g = e.Graphics;

			//draw the "screen"
			g.FillRectangle(Brushes.LightGray, 0, _canvasOffset.Y, canvas.Width, canvas.Height * _zoom);

			//center marker
			g.DrawLine(_penBoundary, canvas.Width / 2 + _canvasOffset.X, 0, canvas.Width / 2 + _canvasOffset.X, canvas.Height);

			//draw the pose
			foreach (LiveSprite sprite in _pose.DrawingOrder)
			{
				sprite.Draw(g, canvas.Width, canvas.Height, _canvasOffset, _zoom, _markers);
				if (_selectionSource == sprite && _selectedObject != null && _selectedObject.IsVisible && !_selectionSource.Hidden && (_recording || !_playing))
				{
					_selectedObject.Draw(g, canvas.Width, canvas.Height, _canvasOffset, _zoom, _markers);
				}
			}

			//selection and gizmos
			if (_selectedObject != null && _selectedObject.IsVisible && !_selectionSource.Hidden && (_recording || !_playing))
			{
				DrawSelection(g, _selectedObject);

				//rotation arrow
				if (_moveContext == HoverContext.Rotate)
				{
					Image arrow = Resources.rotate_arrow;
					Point pt = new Point(_lastMouse.X - arrow.Width / 2, _lastMouse.Y - arrow.Height / 2);

					//rotate to face the object's center
					PointF center = _selectedObject.ToScreenCenter(canvas.Width, canvas.Height, _canvasOffset, _zoom);

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

		private void DrawSelection(Graphics g, LiveSprite sprite)
		{
			if (_selectionSource != null && _selectionSource.Hidden)
			{
				return;
			}

			RectangleF bounds = sprite.ToAbsScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);
			const int SelectionPadding = 0;
			g.DrawRectangle(_penOuterSelection, bounds.X - 2 - SelectionPadding, bounds.Y - 2 - SelectionPadding, bounds.Width + 4 + SelectionPadding * 2, bounds.Height + 4 + SelectionPadding * 2);
			g.DrawRectangle(_penInnerSelection, bounds.X - 1 - SelectionPadding, bounds.Y - 1 - SelectionPadding, bounds.Width + 2 + SelectionPadding * 2, bounds.Height + 2 + SelectionPadding * 2);

			//pivot point
			bounds = sprite.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);

			if (_state == CanvasState.MovingPivot || _moveContext == HoverContext.Pivot)
			{
				g.DrawRectangle(_penKeyframe, bounds.X, bounds.Y, bounds.Width, bounds.Height);
			}

			PointF pt = new PointF(bounds.X + sprite.PivotX * bounds.Width, bounds.Y + sprite.PivotY * bounds.Height);
			g.FillEllipse(Brushes.White, pt.X - 3, pt.Y - 3, 6, 6);
			g.FillEllipse(Brushes.Black, pt.X - 2, pt.Y - 2, 4, 4);
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (_playing && !_recording) { return; }
			_downPoint = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
			{
				//object selection
				LiveSprite obj = null;
				if (_moveContext == HoverContext.None || _moveContext == HoverContext.Select)
				{
					//Sprite
					if (obj == null)
					{
						obj = GetObjectAtPoint(e.X, e.Y, _pose.DrawingOrder);
					}

					if (obj != null && _selectedObject != obj)
					{
						SelectObject(obj);
					}
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				_lastMouse = new Point(e.X, e.Y);
				_state = CanvasState.Panning;
				canvas.Cursor = Cursors.NoMove2D;
			}
		}

		private LiveSprite GetObjectAtPoint(int x, int y, List<LiveSprite> objects)
		{
			//search in reverse order because objects are sorted by depth
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				LiveSprite obj = objects[i];
				if (!obj.IsVisible || obj.Hidden) { continue; }
				RectangleF bounds = obj.ToAbsScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);
				if (bounds.X <= x && x <= bounds.X + bounds.Width &&
					bounds.Y <= y && y <= bounds.Y + bounds.Height)
				{
					return obj;
				}
			}
			return null;
		}

		private void SelectObject(LiveSprite obj)
		{
			ObjectSelected?.Invoke(this, new CanvasSelectionArgs(obj, ModifierKeys));
		}

		/// <summary>
		/// Gets a contextual action based on where the mouse is relative to objects on screen
		/// </summary>
		/// <param name="worldPt"></param>
		private HoverContext GetContext(Point screenPt)
		{
			if (_selectedObject != null && !_selectionSource.Hidden)
			{
				bool locked = false;

				RectangleF bounds = _selectedObject.ToAbsScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);
				bool visible = _selectedObject.IsVisible;
				bool allowTranslate = visible;
				bool allowPivot = visible;
				bool allowRotate = visible;
				bool allowScale = visible;
				bool allowSkew = visible;

				float dl = Math.Abs(screenPt.X - bounds.X);
				float dr = Math.Abs(screenPt.X - (bounds.X + bounds.Width));
				float dt = Math.Abs(screenPt.Y - bounds.Y);
				float db = Math.Abs(screenPt.Y - (bounds.Y + bounds.Height));

				//pivot position
				if (allowPivot)
				{
					RectangleF pivotBounds = _selectedObject.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);
					PointF pivot = new PointF(pivotBounds.X + _selectedObject.PivotX * pivotBounds.Width, pivotBounds.Y + _selectedObject.PivotY * pivotBounds.Height);

					//pivoting - hovering over the pivot circle
					float px = Math.Abs(screenPt.X - pivot.X);
					float py = Math.Abs(screenPt.Y - pivot.Y);
					if (px <= SelectionLeeway && py <= SelectionLeeway)
					{
						return HoverContext.Pivot;
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

				if (allowTranslate)
				{
					if (bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width &&
						bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
					{
						return HoverContext.Drag;
					}
				}
			}

			//see if we're on top of an object
			LiveSprite obj = GetObjectAtPoint(screenPt.X, screenPt.Y, _pose.DrawingOrder);
			if (obj != null)
			{
				return HoverContext.Select;
			}

			return HoverContext.None;
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (_playing && !_recording) { return; }
			Point screenPt = new Point(e.X, e.Y);

			switch (_state)
			{
				case CanvasState.Normal:
					SetContext(e, screenPt);
					break;
				case CanvasState.Panning:
					Pan(screenPt);
					break;
				case CanvasState.MovingObject:
					TranslateObject(screenPt);
					break;
				case CanvasState.MovingPivot:
					MovePivot(screenPt);
					break;
				case CanvasState.Scaling:
					ScaleObject(screenPt);
					break;
				case CanvasState.Rotating:
					RotateObject(screenPt);
					break;
				case CanvasState.Skewing:
					SkewObject(screenPt);
					break;
			}
			_lastMouse = screenPt;
			if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
				_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot ||
				HoverContext.SkewHorizontal.HasFlag(_moveContext) || HoverContext.SkewVertical.HasFlag(_moveContext))
			{
				canvas.Invalidate();
			}
		}

		/// <summary>
		/// Determines what action a click will perform at the current mouse position and provides the relevant feedback (i.e. cursor change),
		/// Or starts that context if the mouse is down
		/// </summary>
		/// <param name="e"></param>
		/// <param name="screenPt"></param>
		private void SetContext(MouseEventArgs e, Point screenPt)
		{
			HoverContext context = GetContext(screenPt);
			if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
				_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot)
			{
				canvas.Invalidate();
			}
			_moveContext = context;
			UpdateHoverCursor(context);

			if (e.Button == MouseButtons.Left && context != HoverContext.None && context != HoverContext.Locked)
			{
				//start dragging
				if (HoverContext.Object.HasFlag(context))
				{
					switch (context)
					{
						case HoverContext.Drag:
							_startDragPosition = new Point((int)_selectedObject.X, (int)_selectedObject.Y);
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
							_moveContext = context;
							_state = CanvasState.Scaling;
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
			}
		}

		private void UpdateHoverCursor(HoverContext context)
		{
			switch (context)
			{
				case HoverContext.ScaleTopLeft:
				case HoverContext.ScaleBottomRight:
					canvas.Cursor = Cursors.SizeNWSE;
					break;
				case HoverContext.ScaleTopRight:
				case HoverContext.ScaleBottomLeft:
					canvas.Cursor = Cursors.SizeNESW;
					break;
				case HoverContext.Drag:
					canvas.Cursor = Cursors.SizeAll;
					break;
				case HoverContext.ScaleLeft:
				case HoverContext.ScaleRight:
					canvas.Cursor = Cursors.SizeWE;
					break;
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
				default:
					canvas.Cursor = Cursors.Default;
					break;
			}
		}

		private void canvas_MouseUp(object sender, MouseEventArgs e)
		{
			_moveContext = HoverContext.None;
			canvas.Cursor = Cursors.Default;
			switch (_state)
			{
				case CanvasState.MovingObject:
				case CanvasState.Resizing:
				case CanvasState.Rotating:
				case CanvasState.Scaling:
				case CanvasState.Panning:
				case CanvasState.MovingPivot:
				case CanvasState.Skewing:
					_state = CanvasState.Normal;
					canvas.Invalidate();
					break;
			}
		}

		/// <summary>
		/// Pans the viewable part of the canvas
		/// </summary>
		/// <param name="screenPt"></param>
		private void Pan(Point screenPt)
		{
			int dx = screenPt.X - _lastMouse.X;
			int dy = screenPt.Y - _lastMouse.Y;
			if (dx != 0 || dy != 0)
			{
				if (dx != 0)
				{
					int moveX = dx;
					if (moveX == 0)
					{
						moveX = (dx < 0 ? -1 : 1);
					}
					_canvasOffset.X += moveX;
				}
				if (dy != 0)
				{
					int moveY = dy;
					if (moveY == 0)
					{
						moveY = (dy < 0 ? -1 : 1);
					}
					_canvasOffset.Y += moveY;
				}
				canvas.Invalidate();
				canvas.Update();
			}
		}

		/// <summary>
		/// Translates the selected object
		/// </summary>
		/// <param name="screenPt"></param>
		private void TranslateObject(Point screenPt)
		{
			//get difference from screen downPoint
			int offsetX = screenPt.X - _downPoint.X;
			int offsetY = screenPt.Y - _downPoint.Y;

			Rectangle rect = _selectedObject.ToScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);

			//convert this to world space
			float screenUnitsPerWorldX = rect.Width / (float)_selectedObject.Width / _selectedObject.ScaleX;
			float screenUnitsPerWorldY = rect.Height / (float)_selectedObject.Height / _selectedObject.ScaleY;
			int x = (int)(offsetX / screenUnitsPerWorldX + _startDragPosition.X);
			int y = (int)(offsetY / screenUnitsPerWorldY + _startDragPosition.Y);

			_selectedObject.Translate(x, y);
			canvas.Invalidate();
			canvas.Update();
		}

		/// <summary>
		/// Moves a sprite's pivot point
		/// </summary>
		/// <param name="screenPt"></param>
		private void MovePivot(Point screenPt)
		{
			//figure out where new pivot position is in relation to object bounds
			RectangleF pivotRect = _selectedObject.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset, _zoom);
			_selectedObject.AdjustPivot(screenPt, pivotRect);
			canvas.Invalidate();
			canvas.Update();
		}

		private void ScaleObject(Point screenPt)
		{
			_selectedObject.Scale(screenPt, canvas.Width, canvas.Height, _canvasOffset, _zoom, _downPoint, _moveContext, ModifierKeys.HasFlag(Keys.Shift));
			canvas.Invalidate();
			canvas.Update();
		}

		private void RotateObject(Point screenPt)
		{
			Point center = _selectedObject.ToScreenCenter(canvas.Width, canvas.Height, _canvasOffset, _zoom);
			_selectedObject.Rotate(screenPt, center);
			canvas.Invalidate();
			canvas.Update();
		}

		private void SkewObject(Point screenPt)
		{
			_selectedObject.Skew(screenPt, _downPoint, _moveContext, _zoom);
			canvas.Invalidate();
			canvas.Update();
		}

		private void cmdMarkers_Click(object sender, EventArgs e)
		{
			MarkerSetup form = new MarkerSetup();
			form.SetData(_character.Character, _markers);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_markers = form.Markers;
				canvas.Invalidate();
			}
		}

		private void cmdFit_Click(object sender, EventArgs e)
		{
			_canvasOffset = new Point(0, 0);
			UpdateZoomIndex(DefaultZoomIndex);
			canvas.Invalidate();
		}

		private void tsZoomIn_Click(object sender, EventArgs e)
		{
			UpdateZoomIndex(_zoomIndex + 1);
		}

		private void tsZoomOut_Click(object sender, EventArgs e)
		{
			UpdateZoomIndex(_zoomIndex - 1);
		}

		private void tsZoom_Click(object sender, EventArgs e)
		{
			UpdateZoomIndex(DefaultZoomIndex);
		}

		private void UpdateZoomIndex(int index)
		{
			_zoomIndex = Math.Max(0, Math.Min(_zoomLevels.Length - 1, index));
			tsZoomIn.Enabled = _zoomIndex < _zoomLevels.Length - 1;
			tsZoomOut.Enabled = _zoomIndex > 0;
			_zoom = _zoomLevels[_zoomIndex];
			tsZoom.Text = $"{_zoom}x";
			canvas.Invalidate();
		}


		private void Canvas_MouseWheel(object sender, MouseEventArgs e)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				((HandledMouseEventArgs)e).Handled = true;
				if (e.Delta > 0)
				{
					UpdateZoomIndex(_zoomIndex + 1);
				}
				else if (e.Delta < 0)
				{
					UpdateZoomIndex(_zoomIndex - 1);
				}
			}
		}

		private void tsHelp_Click(object sender, EventArgs e)
		{
			CanvasHelp form = new CanvasHelp();
			form.ShowDialog();
		}

		private void tsRecord_Click(object sender, EventArgs e)
		{
			_recording = tsRecord.Checked;
		}
	}

	public class CanvasSelectionArgs : EventArgs
	{
		public object Object;
		public Keys Modifiers;

		public CanvasSelectionArgs(object obj, Keys modifiers)
		{
			Object = obj;
			Modifiers = modifiers;
		}
	}
}
