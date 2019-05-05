using Desktop;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Forms;
using SPNATI_Character_Editor.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
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
		private bool _ignoreMarkers = false;

		private Point _lastMouse;
		private Point _canvasOffset = new Point(0, 0);
		/// <summary>
		/// Mousedown position in screen space
		/// </summary>
		private Point _downPoint = new Point(0, 0);
		/// <summary>
		/// Object's initial position in local space
		/// </summary>
		private PointF _startDragPosition = new Point(0, 0);
		/// <summary>
		/// Object's initial rotation angle in local space
		/// </summary>
		private float _startDragRotation;
		private HoverContext _moveContext;
		private CanvasState _state = CanvasState.Normal;

		private Pen _penOuterSelection;
		private Pen _penInnerSelection;
		private Pen _penBoundary;
		private Pen _penKeyframe;
		private Brush _brushHandle;

		private const int DefaultZoomIndex = 3;
		private int _zoomIndex = DefaultZoomIndex;
		private float[] _zoomLevels = new float[] { 0.25f, 0.5f, 0.75f, 1, 1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5f };
		private float _zoom = 1;

		private float _time;
		private float _playbackTime;

		public UndoManager UndoManager;

		private LiveSprite _selectedObject;
		private LiveSprite _selectionSource;

		private Matrix SceneTransform;

		public event EventHandler<CanvasSelectionArgs> ObjectSelected;

		public LiveCanvas()
		{
			InitializeComponent();

			canvas.MouseWheel += Canvas_MouseWheel;
			canvas.KeyDown += Canvas_KeyDown;
			UpdateSceneTransform();
		}

		private void CleanUp()
		{
			if (_pose != null)
			{
				_pose.PropertyChanged -= _pose_PropertyChanged;
				_pose = null;
			}
		}

		public void SetData(ISkin character, LivePose pose)
		{
			CleanUp();
			_character = character;
			_pose = pose;
			if (_pose != null)
			{
				_pose.PropertyChanged += _pose_PropertyChanged;
			}
			UpdateSceneTransform();
			canvas.Invalidate();

			_penBoundary = new Pen(Color.Gray, 1);
			_penBoundary.DashStyle = DashStyle.Dash;

			_brushHandle = new SolidBrush(Color.Black);
			_penOuterSelection = new Pen(Brushes.White, 3);
			_penOuterSelection.DashStyle = DashStyle.DashDotDot;
			_penInnerSelection = new Pen(Brushes.Black, 1);
			_penInnerSelection.DashStyle = DashStyle.DashDotDot;

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
			if (e.PropertyName == "BaseHeight")
			{
				UpdateSceneTransform();
			}
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
			_pose.UpdateTime(_playing ? _playbackTime : _time, true);
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
			else if (e.PropertyName == "ParentId")
			{
				_selectedObject.ParentId = _selectionSource.ParentId;
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

		private void UpdateSceneTransform()
		{
			SceneTransform = new Matrix();
			float screenScale = canvas.Height * _zoom / (_pose == null ? 1400 : _pose.BaseHeight);
			SceneTransform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display * zoom
			SceneTransform.Translate(canvas.Width * 0.5f + _canvasOffset.X, _canvasOffset.Y, MatrixOrder.Append); // center horizontally
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
			List<string> markers = _ignoreMarkers ? null : _markers;
			foreach (LiveSprite sprite in _pose.DrawingOrder)
			{
				sprite.Draw(g, SceneTransform, markers);
				if (_selectionSource == sprite && _selectedObject != null && _selectedObject.IsVisible && !_selectionSource.Hidden && (_recording || !_playing))
				{
					_selectedObject.Draw(g, SceneTransform, markers);
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

					//rotate to face the object's pivot
					PointF center = _selectedObject.ToScreenPt(_selectedObject.PivotX * _selectedObject.Width, _selectedObject.PivotY * _selectedObject.Height, SceneTransform);

					double angle = Math.Atan2(center.Y - _lastMouse.Y, center.X - _lastMouse.X);
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

			int midX = sprite.Width / 2;
			int midY = sprite.Height / 2;
			PointF[] localPts = new PointF[] {
				new PointF(0,0),
				new PointF(sprite.Width, 0),
				new PointF(sprite.Width, sprite.Height),
				new PointF(0, sprite.Height),
				new PointF(midX, midY), //index 4: center
				new PointF(midX, 0), //index 5: top handle
				new PointF(sprite.Width, midY), //index 6: right handle
				new PointF(midX, sprite.Height), //index 7: bottom handle
				new PointF(0, midY), //index 8: left handle
				new PointF(sprite.PivotX * sprite.Width, sprite.PivotY * sprite.Height), //index 9: pivot
			};
			PointF[] boundPts = sprite.ToScreenPt(SceneTransform, localPts);
			PointF[] outerPts = new PointF[4];
			for (int i = 0; i < 4; i++)
			{
				PointF boundPt = boundPts[i];
				outerPts[i] = boundPt;
			}

			g.DrawPolygon(_penOuterSelection, outerPts);
			g.DrawPolygon(_penInnerSelection, outerPts);


			//Grab handles
			for (int i = 5; i <= 8; i++)
			{
				PointF handle = boundPts[i];
				g.FillRectangle(_brushHandle, handle.X - 3, handle.Y - 3, 6, 6);
			}

			//pivot point
			if (_state == CanvasState.MovingPivot || _moveContext == HoverContext.Pivot)
			{
				g.MultiplyTransform(sprite.UnscaledWorldTransform);
				g.MultiplyTransform(SceneTransform, MatrixOrder.Append);
				g.DrawRectangle(_penKeyframe, 0, 0, sprite.Width, sprite.Height);
				g.ResetTransform();
			}

			PointF pt = localPts[9];
			g.FillEllipse(Brushes.White, pt.X - 3, pt.Y - 3, 6, 6);
			g.FillEllipse(Brushes.Black, pt.X - 2, pt.Y - 2, 4, 4);
		}


		private void Canvas_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
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

		public void MoveSelectedObject(int x, int y)
		{
			if (_selectedObject != null)
			{
				PointF worldPt = _selectedObject.ToWorldPt(_selectedObject.X, _selectedObject.Y);
				worldPt.X += x;
				worldPt.Y += y;
				_selectedObject.SetWorldPosition(worldPt);
				canvas.Invalidate();
				canvas.Update();
			}
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			_downPoint = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
			{
				if (_playing && !_recording) { return; }
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
					//if (obj == null && _selectedObject != null)
					//{
					//	PointF worldPt = _selectedObject.ScreenToWorldPt(SceneTransform, _downPoint)[0];
					//	SimpleIK ik = new SimpleIK();
					//	ik.Solve(_selectedObject, worldPt, SceneTransform);
					//}
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				_lastMouse = new Point(e.X, e.Y);
				_state = CanvasState.Panning;
				canvas.Cursor = Cursors.NoMove2D;
			}
		}

		/// <summary>
		/// Gets the topmost object beneath the given screen coordinate
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		private LiveSprite GetObjectAtPoint(int x, int y, List<LiveSprite> objects)
		{
			//search in reverse order because objects are sorted by depth
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				LiveSprite obj = objects[i];
				if (!obj.IsVisible || obj.Hidden || obj.Alpha == 0 || obj.HiddenByMarker(_ignoreMarkers ? null : _markers)) { continue; }
				
				//transform point to local space
				PointF localPt = obj.ToLocalPt(x, y, SceneTransform);
				if (localPt.X >= 0 && localPt.X <= obj.Width &&
					localPt.Y >= 0 && localPt.Y <= obj.Height)
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
		private HoverContext GetContext(PointF screenPt)
		{
			LiveSprite objAtPoint = GetObjectAtPoint((int)screenPt.X, (int)screenPt.Y, _pose.DrawingOrder);
			if (_selectedObject != null)
			{
				List<LiveSprite> selection = new List<LiveSprite>();
				selection.Add(_selectedObject);
				LiveSprite selected = GetObjectAtPoint((int)screenPt.X, (int)screenPt.Y, selection);
				if (selected != null)
				{
					objAtPoint = selected;
				}
			}

			if (_selectedObject != null && !_selectionSource.Hidden)
			{
				bool locked = false;

				//convert the screen pt to the selected object's local space
				PointF pt = _selectedObject.ToLocalPt(SceneTransform, screenPt)[0];
				Point localPt = new Point(0, 0);
				localPt.X = (int)Math.Round(pt.X);
				localPt.Y = (int)Math.Round(pt.Y);

				PointF[] corners = new PointF[] {
					new PointF(0, 0),
					new PointF(_selectedObject.Width, 0),
					new PointF(_selectedObject.Width, _selectedObject.Height),
					new PointF(0, _selectedObject.Height),
					new PointF(_selectedObject.PivotX * _selectedObject.Width, _selectedObject.PivotY * _selectedObject.Height),
				};
				_selectedObject.ToScreenPt(SceneTransform, corners);
				PointF tl = corners[0];
				PointF tr = corners[1];
				PointF br = corners[2];
				PointF bl = corners[3];

				bool visible = _selectedObject.IsVisible;
				bool allowTranslate = visible;
				bool allowPivot = visible;
				bool allowRotate = visible;
				bool allowScale = visible;
				bool allowSkew = visible;

				float dl = screenPt.DistanceFromLineSegment(bl, tl);
				float dr = screenPt.DistanceFromLineSegment(br, tr);
				float dt = screenPt.DistanceFromLineSegment(tl, tr);
				float db = screenPt.DistanceFromLineSegment(bl, br);

				//pivot position
				if (allowPivot)
				{
					PointF pivot = corners[4];

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
					if (localPt.X < -SelectionLeeway && localPt.X >= -RotationLeeway && dt <= RotationLeeway ||
						localPt.Y < -SelectionLeeway && localPt.Y >= -RotationLeeway && dl <= RotationLeeway ||
						localPt.X > _selectedObject.Width + SelectionLeeway && localPt.X <= _selectedObject.Width + RotationLeeway && dt <= RotationLeeway ||
						localPt.Y < -SelectionLeeway && localPt.Y >= -RotationLeeway && dr <= RotationLeeway ||
						localPt.X < -SelectionLeeway && localPt.X >= -RotationLeeway && db <= RotationLeeway ||
						localPt.Y > _selectedObject.Height + SelectionLeeway && localPt.Y <= _selectedObject.Height + RotationLeeway && dl <= RotationLeeway ||
						localPt.X > _selectedObject.Width + SelectionLeeway && localPt.X <= _selectedObject.Width + RotationLeeway && db <= RotationLeeway ||
						localPt.Y > _selectedObject.Height + SelectionLeeway && localPt.Y <= _selectedObject.Height + RotationLeeway && dr <= RotationLeeway)
					{
						return locked ? HoverContext.Locked : HoverContext.Rotate;
					}
				}

				if (allowSkew && ModifierKeys.HasFlag(Keys.Shift))
				{
					//skewing - grabbing an edge while Shift is held down
					if (0 <= localPt.Y && localPt.Y <= _selectedObject.Height)
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
					if (0 <= localPt.X && localPt.X <= _selectedObject.Width)
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
						else if (0 <= localPt.Y && localPt.Y <= _selectedObject.Height)
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
						else if (0 <= localPt.Y && localPt.Y <= _selectedObject.Height)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleRight;
						}
					}

					if (dt <= SelectionLeeway && 0 <= localPt.X && localPt.X <= _selectedObject.Width)
					{
						return locked ? HoverContext.Locked : HoverContext.ScaleTop;
					}

					if (db <= SelectionLeeway && 0 <= localPt.X && localPt.X <= _selectedObject.Width)
					{
						return locked ? HoverContext.Locked : HoverContext.ScaleBottom;
					}
				}

				if (objAtPoint != null && objAtPoint != _selectedObject && objAtPoint != _selectionSource)
				{
					//selecting another object takes priority over translation but nothing else
					return HoverContext.Select;
				}

				if (allowTranslate)
				{
					if (0 <= localPt.X && localPt.X <= _selectedObject.Width &&
						0 <= localPt.Y && localPt.Y <= _selectedObject.Height)
					{
						return HoverContext.Drag;
					}
				}
			}

			if (objAtPoint != null && objAtPoint != _selectedObject && objAtPoint != _selectionSource)
			{
				return HoverContext.Select;
			}

			return HoverContext.None;
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
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
			if (_playing && !_recording && context != HoverContext.CameraPan)
			{
				context = HoverContext.None;
			}
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
							_startDragPosition = _selectedObject.ToWorldPt(_startDragPosition)[0];
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
							_moveContext = context;
							Cursor = Timeline.HandClosed;
							_state = CanvasState.Scaling;
							break;
						case HoverContext.Rotate:
							_startDragRotation = _selectedObject.Rotation;
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
				case HoverContext.ScaleTopRight:
				case HoverContext.ScaleBottomLeft:
				case HoverContext.ScaleLeft:
				case HoverContext.ScaleRight:
				case HoverContext.ScaleTop:
				case HoverContext.ScaleBottom:
					canvas.Cursor = Timeline.HandOpen;
					break;
				case HoverContext.Drag:
					canvas.Cursor = Cursors.SizeAll;
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
				UpdateSceneTransform();
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
			PointF[] pts = new PointF[] {
				_downPoint,
				screenPt
			};

			//convert to world space
			pts = _selectedObject.ScreenToWorldPt(SceneTransform, pts);
			float x = pts[1].X - pts[0].X + _startDragPosition.X;
			float y = pts[1].Y - pts[0].Y + _startDragPosition.Y;

			_selectedObject.SetWorldPosition(new PointF(x, y));
			canvas.Invalidate();
			canvas.Update();
		}

		/// <summary>
		/// Moves a sprite's pivot point
		/// </summary>
		/// <param name="screenPt"></param>
		private void MovePivot(Point screenPt)
		{
			_selectedObject.AdjustPivot(screenPt, SceneTransform);
			canvas.Invalidate();
			canvas.Update();
		}

		private void ScaleObject(Point screenPt)
		{
			_selectedObject.Scale(screenPt, SceneTransform, _moveContext);
			canvas.Invalidate();
			canvas.Update();
		}

		private void RotateObject(Point screenPt)
		{
			PointF pivot = _selectedObject.ToScreenPt(_selectedObject.PivotX * _selectedObject.Width, _selectedObject.PivotY * _selectedObject.Height, SceneTransform);
			_selectedObject.Rotate(screenPt, pivot, _downPoint, _startDragRotation);
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
			UpdateSceneTransform();
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

		private void canvas_Resize(object sender, EventArgs e)
		{
			UpdateSceneTransform();
		}

		private void tsFilter_Click(object sender, EventArgs e)
		{
			_ignoreMarkers = tsFilter.Checked;
			canvas.Invalidate();
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
