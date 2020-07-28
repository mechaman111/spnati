using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Forms;
using SPNATI_Character_Editor.Properties;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public partial class LiveCanvas : UserControl, ISkinControl
	{
		public const int SelectionLeeway = 5;
		public const int RotationLeeway = 30;

		private bool _recording;
		public bool Playing { get; private set; }
		private ISkin _character;
		private LiveData _data;
		private ICanvasViewport _viewport;
		private List<string> _markers = new List<string>();
		private List<string> _userMarkers = new List<string>();
		private List<string> _addedMarkers = new List<string>();
		private List<string> _removedMarkers = new List<string>();
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

		private bool _backColorCustomized = false;
		private SolidBrush _backColor = new SolidBrush(Color.LightGray);
		private Pen _penBoundary;

		private const int DefaultZoomIndex = 3;
		private int _zoomIndex = DefaultZoomIndex;
		private float[] _zoomLevels = new float[] { 0.25f, 0.5f, 0.75f, 1, 1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5f };
		private float _zoom = 1;

		private float _time;
		private float _playbackTime;
		private float _elapsedTime;
		private bool _inChange;
		private bool _inUpdate;

		public UndoManager UndoManager;

		private LiveObject _selectedPreview;
		private LiveObject _selectionSource;

		private Matrix SceneTransform;

		public event EventHandler<CanvasSelectionArgs> ObjectSelected;
		public event EventHandler ToolBarButtonClicked;
		public event EventHandler CanvasClicked;
		public event EventHandler<CanvasPaintArgs> CustomPaint;

		public bool AllowZoom { get; set; }
		public bool DisallowEdit { get; set; }

		private bool _customDraw;
		public bool CustomDraw
		{
			get { return _customDraw; }
			set
			{
				_customDraw = value;
				foreach (Control ctl in skinnedPanel1.Controls)
				{
					ctl.Enabled = !_customDraw;
				}
				canvas.Invalidate();
			}
		}

		public LiveCanvas()
		{
			InitializeComponent();

			AllowZoom = true;
			canvas.MouseWheel += Canvas_MouseWheel;
			canvas.KeyDown += Canvas_KeyDown;
			UpdateSceneTransform();
		}

		public int CanvasWidth
		{
			get { return canvas.Width; }
		}
		public int CanvasHeight
		{
			get { return canvas.Height; }
		}

		private void CleanUp()
		{
			if (_data != null)
			{
				DestroyLivePreview();
				_data.PropertyChanged -= _data_PropertyChanged;
				_data = null;
				if (_viewport != null)
				{
					_viewport.ViewportUpdated -= _viewport_ViewportUpdated;
					_viewport = null;
				}
			}
		}

		public void AddToolBarButton(Image icon, string tooltip, bool checkOnClick, Action<ToolStripButton> clickHandler)
		{
			ToolStripButton item = new ToolStripButton("", icon, CustomToolbarButton_Click);
			item.ToolTipText = tooltip;
			item.CheckOnClick = checkOnClick;
			item.Tag = clickHandler;
			canvasStrip.Items.Add(item);
		}
		private void CustomToolbarButton_Click(object sender, EventArgs e)
		{
			ToolStripButton btn = sender as ToolStripButton;
			Action<ToolStripButton> click = btn?.Tag as Action<ToolStripButton>;
			ToolBarButtonClicked?.Invoke(sender, e);
			click?.Invoke(btn);
		}

		public void SetData(ISkin character, LiveData data)
		{
			CleanUp();
			_character = character;
			_data = data;
			if (_data != null)
			{
				_data.PropertyChanged += _data_PropertyChanged;
				if (_data is ICanvasViewport)
				{
					_viewport = _data as ICanvasViewport;
					_viewport.ViewportUpdated += _viewport_ViewportUpdated;
				}
			}
			UpdateSceneTransform();
			canvas.Invalidate();

			_penBoundary = new Pen(Color.Gray, 1);
			_penBoundary.DashStyle = DashStyle.Dash;

			UpdateTime(_time, _playbackTime, _elapsedTime);
		}

		public void LockToolbar(bool locked, ToolStripButton excludedButton)
		{
			foreach (ToolStripItem item in canvasStrip.Items)
			{
				if (item == excludedButton) { continue; }
				item.Enabled = !locked;
			}
		}

		private void _viewport_ViewportUpdated(object sender, EventArgs e)
		{
			_viewport.FitToViewport(canvas.Width, canvas.Height, ref _canvasOffset, ref _zoom);
			UpdateSceneTransform();
			canvas.Invalidate();
		}

		public void SetPlayback(bool playing)
		{
			Playing = playing;
			UpdateTime(_time, _playbackTime, _elapsedTime);
		}

		private void _data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (_inChange) { return; }
			_inChange = true;
			if (e.PropertyName == "BaseHeight")
			{
				UpdateSceneTransform();
			}
			UpdateData();
			_inChange = false;
		}

		public void UpdateTime(float time, float playbackTime, float elapsedTime)
		{
			_elapsedTime = elapsedTime;
			_playbackTime = playbackTime;
			_time = time;
			UpdateData();
		}

		private void UpdateData()
		{
			if (_data == null || _inUpdate) { return; }
			_inUpdate = true;
			_data.UpdateTime(Playing ? _playbackTime : _time, _elapsedTime, true);
			if (_selectedPreview != _selectionSource)
			{
				_selectedPreview?.Update(_time, _elapsedTime, false);
			}
			canvas.Invalidate();
			if (Playing)
			{
				//force it to draw immediately
				canvas.Update();
			}
			_inUpdate = false;
		}

		public void SelectData(object data)
		{
			DestroyLivePreview();

			LiveObject obj = data as LiveObject;
			_selectionSource = obj;
			canvas.Invalidate();

			if (_selectionSource != null)
			{
				CreateSelectionPreview();
				ICanInvalidate invalidatableSource = data as ICanInvalidate;
				if (invalidatableSource != null)
				{
					invalidatableSource.Invalidated += InvalidatableSource_Invalidated;
				}
			}
		}

		private void InvalidatableSource_Invalidated(object sender, EventArgs e)
		{
			canvas.Invalidate();
		}

		private void DestroyLivePreview()
		{
			if (_selectionSource != null)
			{
				foreach (string marker in _removedMarkers)
				{
					if (_userMarkers.Contains(marker))
					{
						_markers.Add(marker);
					}
				}
				_removedMarkers.Clear();
				foreach (string marker in _addedMarkers)
				{
					if (!_userMarkers.Contains(marker))
					{
						_markers.Remove(marker);
					}
				}
				_addedMarkers.Clear();
				ICanInvalidate invalidatableSource = _selectionSource as ICanInvalidate;
				if (invalidatableSource != null)
				{
					invalidatableSource.Invalidated -= InvalidatableSource_Invalidated;
				}
				_selectionSource.DestroyLivePreview();
				_selectionSource.PreviewInvalidated -= _selectionSource_PreviewInvalidated;
				_selectedPreview = null;
			}
		}

		private void _selectionSource_PreviewInvalidated(object sender, EventArgs e)
		{
			CreateSelectionPreview();
		}

		private void CreateSelectionPreview()
		{
			DestroyLivePreview();
			if (_selectionSource == null) { return; }

			_selectionSource.PreviewInvalidated += _selectionSource_PreviewInvalidated;
			_selectedPreview = _selectionSource.CreateLivePreview(_time);
			if (_selectedPreview != null && !string.IsNullOrEmpty(_selectedPreview.Marker))
			{
				MarkerOperator op;
				string value;
				bool perTarget;
				string marker = Marker.ExtractConditionPieces(_selectedPreview.Marker, out op, out value, out perTarget);
				if (value == "0" && op == MarkerOperator.Equals || value != "0" && op == MarkerOperator.NotEqual)
				{
					_removedMarkers.Add(marker);
					_markers.Remove(marker);
				}
				else if (value != "0" && op != MarkerOperator.NotEqual && !_markers.Contains(marker))
				{
					_addedMarkers.Add(marker);
					_markers.Add(marker);
				}
			}
			canvas.Invalidate();
		}

		private void UpdateSceneTransform()
		{
			if (_data == null)
			{
				SceneTransform = new Matrix();
			}
			else
			{
				SceneTransform = _data.GetSceneTransform(canvas.Width, canvas.Height, _canvasOffset, _zoom);
			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (CustomDraw)
			{
				CustomPaint?.Invoke(this, new CanvasPaintArgs(g, canvas.Width, canvas.Height));
				return;
			}

			if (_data == null)
			{
				return;
			}

			//draw the "screen"
			g.FillRectangle(_backColor, 0, _canvasOffset.Y, canvas.Width, canvas.Height * _zoom);

			//center marker
			g.DrawLine(_penBoundary, canvas.Width / 2 + _canvasOffset.X, 0, canvas.Width / 2 + _canvasOffset.X, canvas.Height);

			//draw the data
			List<string> markers = _ignoreMarkers ? null : _markers;
			LiveObject preview = _selectedPreview;
			if (preview != null && (!preview.IsVisible || (!_recording && Playing)))
			{
				preview = null;
			}
			_data.Draw(g, SceneTransform, markers, _selectionSource, preview, Playing);

			//selection and gizmos
			if (_selectionSource != null && _selectedPreview != null && _selectedPreview.IsVisible && !_selectionSource.Hidden && (_recording || !Playing))
			{
				DrawSelection(g);

				//rotation arrow
				if (_moveContext == HoverContext.Rotate)
				{
					Image arrow = Resources.rotate_arrow;
					Point pt = new Point(_lastMouse.X - arrow.Width / 2, _lastMouse.Y - arrow.Height / 2);

					//rotate to face the object's pivot
					PointF center = _selectedPreview.ToScreenPt(_selectedPreview.PivotX * _selectedPreview.Width, _selectedPreview.PivotY * _selectedPreview.Height, SceneTransform);

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

		private void DrawSelection(Graphics g)
		{
			LiveObject selection = _selectedPreview;
			if (selection != null && _selectionSource != null && _selectionSource.Hidden)
			{
				return;
			}

			selection.DrawSelection(g, SceneTransform, _state, _moveContext);
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
			if (_selectedPreview != null)
			{
				PointF worldPt = _selectedPreview.ToWorldPt(_selectedPreview.X, _selectedPreview.Y);
				worldPt.X += x;
				worldPt.Y += y;
				_selectedPreview.SetWorldPosition(worldPt, SceneTransform);
				canvas.Invalidate();
				canvas.Update();
			}
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			_downPoint = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
			{
				CanvasClicked?.Invoke(this, EventArgs.Empty);
				if (DisallowEdit || (Playing && !_recording)) { return; }
				//object selection
				LiveObject obj = null;
				if (_moveContext == HoverContext.None || _moveContext == HoverContext.Select)
				{
					//object
					if (obj == null)
					{
						obj = _data.GetObjectAtPoint(e.X, e.Y, SceneTransform, _ignoreMarkers, _markers);
					}

					if (obj != null && _selectedPreview != obj)
					{
						SelectObject(obj);
					}
				}
				switch (_moveContext)
				{
					case HoverContext.ArrowLeft:
					case HoverContext.ArrowDown:
					case HoverContext.ArrowRight:
					case HoverContext.ArrowUp:
						UpdateArrowPosition();
						break;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (_viewport == null || _viewport.AllowPan)
				{
					_lastMouse = new Point(e.X, e.Y);
					_state = CanvasState.Panning;
					canvas.Cursor = Cursors.NoMove2D;
				}
			}
		}

		private void SelectObject(LiveObject obj)
		{
			ObjectSelected?.Invoke(this, new CanvasSelectionArgs(obj, ModifierKeys));
		}

		/// <summary>
		/// Gets a contextual action based on where the mouse is relative to objects on screen
		/// </summary>
		/// <param name="worldPt"></param>
		private HoverContext GetContext(PointF screenPt)
		{
			LiveObject objAtPoint = _data.GetObjectAtPoint((int)screenPt.X, (int)screenPt.Y, SceneTransform, _ignoreMarkers, _markers);
			if (_selectedPreview != null)
			{
				List<LiveObject> selection = new List<LiveObject>();
				selection.Add(_selectedPreview);
				LiveObject selected = _data.GetObjectAtPoint((int)screenPt.X, (int)screenPt.Y, SceneTransform, _ignoreMarkers, _markers);
				if (selected != null)
				{
					objAtPoint = selected;
				}
			}

			if (_selectedPreview != null && !_selectionSource.Hidden)
			{
				bool locked = false;

				//convert the screen pt to the selected object's local space
				PointF pt = _selectedPreview.ToLocalPt(SceneTransform, screenPt)[0];
				Point localPt = new Point(0, 0);
				localPt.X = (int)Math.Round(pt.X);
				localPt.Y = (int)Math.Round(pt.Y);

				PointF[] corners = new PointF[] {
					new PointF(0, 0),
					new PointF(_selectedPreview.Width, 0),
					new PointF(_selectedPreview.Width, _selectedPreview.Height),
					new PointF(0, _selectedPreview.Height),
					new PointF(_selectedPreview.PivotX * _selectedPreview.Width, _selectedPreview.PivotY * _selectedPreview.Height),
				};
				_selectedPreview.ToScreenPt(SceneTransform, corners);
				PointF tl = corners[0];
				PointF tr = corners[1];
				PointF br = corners[2];
				PointF bl = corners[3];

				bool visible = _selectedPreview.IsVisible;
				bool allowTranslate = visible && _selectedPreview.CanTranslate;
				bool allowPivot = visible && _selectedPreview.CanPivot;
				bool allowRotate = visible && _selectedPreview.CanRotate;
				bool allowScale = visible && _selectedPreview.CanScale;
				bool allowSkew = visible && _selectedPreview.CanSkew;
				bool allowArrow = visible && _selectedPreview.CanArrow;

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
						localPt.X > _selectedPreview.Width + SelectionLeeway && localPt.X <= _selectedPreview.Width + RotationLeeway && dt <= RotationLeeway ||
						localPt.Y < -SelectionLeeway && localPt.Y >= -RotationLeeway && dr <= RotationLeeway ||
						localPt.X < -SelectionLeeway && localPt.X >= -RotationLeeway && db <= RotationLeeway ||
						localPt.Y > _selectedPreview.Height + SelectionLeeway && localPt.Y <= _selectedPreview.Height + RotationLeeway && dl <= RotationLeeway ||
						localPt.X > _selectedPreview.Width + SelectionLeeway && localPt.X <= _selectedPreview.Width + RotationLeeway && db <= RotationLeeway ||
						localPt.Y > _selectedPreview.Height + SelectionLeeway && localPt.Y <= _selectedPreview.Height + RotationLeeway && dr <= RotationLeeway)
					{
						return locked ? HoverContext.Locked : HoverContext.Rotate;
					}
				}

				if (allowSkew && ModifierKeys.HasFlag(Keys.Shift))
				{
					//skewing - grabbing an edge while Shift is held down
					if (0 <= localPt.Y && localPt.Y <= _selectedPreview.Height)
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
					if (0 <= localPt.X && localPt.X <= _selectedPreview.Width)
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

				if (allowArrow)
				{
					if (tr.Y <= screenPt.Y && screenPt.Y <= br.Y)
					{
						if (dl > SelectionLeeway && screenPt.X < tl.X && dl <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowLeft;
						}
						else if (dr <= RotationLeeway && dr > SelectionLeeway && screenPt.X > tr.X)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowRight;
						}
					}
					if (tl.X <= screenPt.X && screenPt.X <= tr.X)
					{
						if (screenPt.Y < tl.Y && dt <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowUp;
						}
						else if (screenPt.Y >= br.Y && db <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.ArrowDown;
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
						else if (0 <= localPt.Y && localPt.Y <= _selectedPreview.Height)
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
						else if (0 <= localPt.Y && localPt.Y <= _selectedPreview.Height)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleRight;
						}
					}

					if (dt <= SelectionLeeway && 0 <= localPt.X && localPt.X <= _selectedPreview.Width)
					{
						return locked ? HoverContext.Locked : HoverContext.ScaleTop;
					}

					if (db <= SelectionLeeway && 0 <= localPt.X && localPt.X <= _selectedPreview.Width)
					{
						return locked ? HoverContext.Locked : HoverContext.ScaleBottom;
					}
				}

				if (objAtPoint != null && objAtPoint != _selectedPreview && objAtPoint != _selectionSource)
				{
					//selecting another object takes priority over translation but nothing else
					return HoverContext.Select;
				}

				if (allowTranslate)
				{
					if (0 <= localPt.X && localPt.X <= _selectedPreview.Width &&
						0 <= localPt.Y && localPt.Y <= _selectedPreview.Height)
					{
						return HoverContext.Drag;
					}
				}
			}

			if (objAtPoint != null && objAtPoint != _selectedPreview && objAtPoint != _selectionSource)
			{
				return HoverContext.Select;
			}

			return HoverContext.None;
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (DisallowEdit) { return; }
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
			if (Playing && !_recording && context != HoverContext.CameraPan)
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
							_startDragPosition = new Point((int)_selectedPreview.X, (int)_selectedPreview.Y);
							_startDragPosition = _selectedPreview.ToWorldPt(_startDragPosition)[0];
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
							Cursor = Cursors.Hand;
							_state = CanvasState.Scaling;
							break;
						case HoverContext.Rotate:
							_startDragRotation = _selectedPreview.Rotation;
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
					canvas.Cursor = Cursors.Hand;
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
			pts = _selectedPreview.ScreenToWorldPt(SceneTransform, pts);
			float x = pts[1].X - pts[0].X + _startDragPosition.X;
			float y = pts[1].Y - pts[0].Y + _startDragPosition.Y;

			_selectedPreview.SetWorldPosition(new PointF(x, y), SceneTransform);
			canvas.Invalidate();
			canvas.Update();
		}

		/// <summary>
		/// Moves an object's pivot point
		/// </summary>
		/// <param name="screenPt"></param>
		private void MovePivot(Point screenPt)
		{
			_selectedPreview.AdjustPivot(screenPt, SceneTransform);
			canvas.Invalidate();
			canvas.Update();
		}

		private void ScaleObject(Point screenPt)
		{
			_selectedPreview.Scale(screenPt, SceneTransform, _moveContext);
			canvas.Invalidate();
			canvas.Update();
		}

		private void RotateObject(Point screenPt)
		{
			PointF pivot = _selectedPreview.ToScreenPt(_selectedPreview.PivotX * _selectedPreview.Width, _selectedPreview.PivotY * _selectedPreview.Height, SceneTransform);
			_selectedPreview.Rotate(screenPt, pivot, _downPoint, _startDragRotation);
			canvas.Invalidate();
			canvas.Update();
		}

		private void SkewObject(Point screenPt)
		{
			_selectedPreview.Skew(screenPt, _downPoint, _moveContext, _zoom);
			canvas.Invalidate();
			canvas.Update();
		}

		private void UpdateArrowPosition()
		{
			_selectedPreview.UpdateArrowPosition(_moveContext);
		}

		private void cmdMarkers_Click(object sender, EventArgs e)
		{
			MarkerSetup form = new MarkerSetup();
			form.SetData(_character.Character, _userMarkers);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_userMarkers = form.Markers;
				_markers.Clear();
				_markers.AddRange(_userMarkers);
				foreach (string marker in _removedMarkers)
				{
					_markers.Remove(marker);
				}
				foreach (string marker in _addedMarkers)
				{
					if (!_markers.Contains(marker))
					{
						_markers.Add(marker);
					}
				}

				canvas.Invalidate();
			}
		}

		private void cmdFit_Click(object sender, EventArgs e)
		{
			FitScreen();
		}

		public void FitScreen()
		{
			if (_data == null) { return; }
			_data.FitScene(canvas.Width, canvas.Height, ref _canvasOffset, ref _zoom);
			//find the most appropriate index for the new zoom
			int closestIndex = 0;
			for (int i = 0; i < _zoomLevels.Length; i++)
			{
				float diff = Math.Abs(_zoomLevels[i] - _zoom);
				if (diff < 0.001f)
				{
					UpdateZoomIndex(i);
					return;
				}
				else if (_zoomLevels[i] < _zoom)
				{
					closestIndex = i;
				}
				else
				{
					break;
				}
			}
			_zoomIndex = closestIndex;
			tsZoomIn.Enabled = AllowZoom && _zoomIndex < _zoomLevels.Length - 1;
			tsZoomOut.Enabled = AllowZoom && _zoomIndex > 0;
			tsZoom.Enabled = AllowZoom;
			tsZoom.Text = $"{_zoom}x";
			UpdateSceneTransform();
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
				if (AllowZoom)
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

		private void tsBackColor_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = _backColor.Color;
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				_backColor.Color = colorDialog1.Color;
				_backColorCustomized = true;
				canvas.Invalidate();
			}
		}

		public void InvalidateCanvas()
		{
			canvas.Invalidate();
		}

		public void OnUpdateSkin(Skin skin)
		{
			if (!_backColorCustomized)
			{
				if (skin.Group == "Dark")
				{
					_backColor.Color = Color.FromArgb(50, 50, 50);
				}
				else
				{
					_backColor.Color = Color.LightGray;
				}
			}
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

	public class CanvasPaintArgs : EventArgs
	{
		public Graphics Graphics { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public CanvasPaintArgs(Graphics g, int width, int height)
		{
			Graphics = g;
			Width = width;
			Height = height;
		}
	}
}
