using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Base class for working objects in an epilogue or pose
	/// </summary>
	public abstract class LiveObject : BindableObject, ILabel, IComparable<LiveObject>
	{
		public event EventHandler PreviewInvalidated;
		public event EventHandler LabelChanged;
		public LiveData Data;
		public bool CenterX { get; set; } = true;
		public bool CenterY { get; set; } = false;

		/** LiveObject describing the characteristics of this one prior to the data's start */
		public LiveObject Previous;

		private static Pen _penOuterSelection;
		private static Pen _penInnerSelection;
		private static Brush _brushHandle;
		private static Pen _penPivotBox;

		static LiveObject()
		{
			_penPivotBox = new Pen(Color.FromArgb(127, 255, 255, 255));
			_penPivotBox.Width = 2;

			_brushHandle = new SolidBrush(Color.Black);
			_penOuterSelection = new Pen(Brushes.White, 3);
			_penOuterSelection.DashStyle = DashStyle.DashDotDot;
			_penInnerSelection = new Pen(Brushes.Black, 1);
			_penInnerSelection.DashStyle = DashStyle.DashDotDot;
		}

		/// <summary>
		/// Set on a source object pointing to its live preview
		/// </summary>
		public LiveObject LinkedPreview;

		[Text(DisplayName = "Id", Key = "id", GroupOrder = 0)]
		public string Id
		{
			get { return Get<string>(); }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					Set(value);
					LabelChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public LiveObject Parent { get; private set; }
		public string ParentId
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				Parent = Data.Find(value);
				InvalidateTransform();
			}
		}

		/// <summary>
		/// Hide from canvas UI
		/// </summary>
		public bool Hidden
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public abstract bool IsVisible { get; }

		public bool AllowLinkToEnd
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public bool LinkedToEnd
		{
			get { return Get<bool>(); }
			set { Set(value); if (value) { AllowLinkToEnd = true; } }
		}

		[Text(DisplayName = "Marker", GroupOrder = 13, Key = "marker", Description = "Run this directive only if the marker's condition is met")]
		public string Marker
		{
			get { return Get<string>(); }
			set
			{
				bool perTarget;
				MarkerOperator op;
				string markerValue;
				MarkerName = SPNATI_Character_Editor.Marker.ExtractConditionPieces(value, out op, out markerValue, out perTarget);
				MarkerOp = op;
				MarkerValue = markerValue;
				Set(value);
			}
		}

		private string MarkerName;
		private MarkerOperator MarkerOp;
		private string MarkerValue;

		[Numeric(DisplayName = "Layer", Key = "z", GroupOrder = 15, Minimum = -100)]
		public int Z
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Pivot X", Key = "pivotx", GroupOrder = 20, Description = "X value of rotation/scale point of origin as a percentage of the sprite's physical size.", Minimum = -1000, Maximum = 1000, Increment = 0.1f)]
		public float PivotX
		{
			get { return Get<float>(); }
			set
			{
				if (float.IsNaN(value))
				{
					value = 0;
				}
				Set(value);
				InvalidateTransform();
			}
		}
		[Float(DisplayName = "Pivot Y", Key = "pivoty", GroupOrder = 20, Description = "Y value of rotation/scale point of origin as a percentage of the sprite's physical size.", Minimum = -1000, Maximum = 1000, Increment = 0.1f)]
		public float PivotY
		{
			get { return Get<float>(); }
			set
			{
				if (float.IsNaN(value))
				{
					value = 0;
				}
				Set(value);
				InvalidateTransform();
			}
		}

		[Float(DisplayName = "Start", Key = "start", GroupOrder = 5, Description = "Starting time to display the sprite.", Minimum = 0, Maximum = 1000, Increment = 0.1f)]
		public float Start
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		public float Time { get; protected set; }
		public float TimeOffset { get; protected set; }

		protected float GetRelativeTime()
		{
			return Time - Start;
		}

		public int CompareTo(LiveObject other)
		{
			return Id.CompareTo(other.Id);
		}

		public virtual string GetLabel()
		{
			return Id;
		}

		public override string ToString()
		{
			return $"{Id} ({Start})";
		}

		public abstract ITimelineWidget CreateWidget(Timeline timeline);

		#region Interpolated values based on the current time
		private string _src;
		public string Src
		{
			get { return _src; }
			set
			{
				if (_src != value)
				{
					_src = value;
					OnPropertyChanged("Src");
				}
			}
		}
		public Bitmap Image;

		public int Height
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public int Width
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public int? WidthOverride
		{
			get { return Get<int?>(); }
			set { Set(value); }
		}
		public int? HeightOverride
		{
			get { return Get<int?>(); }
			set { Set(value); }
		}

		private float _x;
		public float X
		{
			get { return _x; }
			set
			{
				if (_x != value)
				{
					_x = (float)Math.Round(value, 0); InvalidateTransform(); NotifyPropertyChanged(nameof(X));
				}
			}
		}

		private float _y;
		public float Y
		{
			get { return _y; }
			set
			{
				if (_y != value)
				{
					_y = (float)Math.Round(value, 0); InvalidateTransform(); NotifyPropertyChanged(nameof(Y));
				}
			}
		}

		private float _rotation;
		public float Rotation
		{
			get { return _rotation; }
			set { _rotation = (float)Math.Round(value, 2); InvalidateTransform(); }
		}

		private float _scaleX = 1;
		public float ScaleX
		{
			get { return _scaleX; }
			set
			{
				value = (float)Math.Round(value, 2);
				if (value == 0)
				{
					value = 0.01f;
				}
				_scaleX = value;
				InvalidateTransform();
			}
		}

		private float _scaleY = 1;
		public float ScaleY
		{
			get { return _scaleY; }
			set
			{
				value = (float)Math.Round(value, 2);
				if (value == 0)
				{
					value = 0.01f;
				}
				_scaleY = value;
				InvalidateTransform();
			}
		}

		private float _skewX = 0;
		public float SkewX
		{
			get { return _skewX; }
			set
			{
				value = (float)Math.Round(value, 2);
				_skewX = value;
				InvalidateTransform();
			}
		}

		private float _skewY = 0;
		public float SkewY
		{
			get { return _skewY; }
			set
			{
				value = (float)Math.Round(value, 2);
				_skewY = value;
				InvalidateTransform();
			}
		}

		public float Alpha = 100;

		private Matrix _localTransform;
		public Matrix LocalTransform
		{
			get
			{
				if (_localTransform == null)
				{
					_localTransform = UpdateLocalTransform();
				}
				return _localTransform;
			}
		}

		public void InvalidateTransform()
		{
			_localTransform = null;
		}

		protected virtual Matrix UpdateLocalTransform()
		{
			Matrix transform = new Matrix();
			float pivotX = PivotX * Width;
			float pivotY = PivotY * Height;
			transform.Translate(-pivotX, -pivotY, MatrixOrder.Append);
			transform.Scale(ScaleX, ScaleY, MatrixOrder.Append);
			transform.Rotate(Rotation, MatrixOrder.Append);
			transform.Translate(pivotX, pivotY, MatrixOrder.Append);

			transform.Translate(X - (Parent == null && CenterX ? Width / 2 : 0), Y - (Parent == null && CenterY ? Height / 2 : 0), MatrixOrder.Append); //local position
			return transform;
		}

		public Matrix WorldTransform
		{
			get
			{
				LiveObject transform = this;
				Matrix m = new Matrix();
				while (transform != null)
				{
					m.Multiply(transform.LocalTransform, MatrixOrder.Append);

					if (transform.Parent == this)
					{
						transform = null;
					}
					else
					{
						transform = transform.Parent;
					}
				}
				return m;
			}
		}

		public Matrix UnscaledWorldTransform
		{
			get
			{
				LiveObject transform = this;
				Matrix m = new Matrix();
				while (transform != null)
				{
					Matrix localTransform;
					if (transform == this)
					{
						localTransform = new Matrix();
						localTransform.Translate(X - (Parent == null && CenterX ? Width / 2 : 0), Y - (Parent == null && CenterY ? Height / 2 : 0), MatrixOrder.Append);
					}
					else
					{
						localTransform = transform.LocalTransform;
					}

					m.Multiply(localTransform, MatrixOrder.Append);

					if (transform.Parent == this)
					{
						transform = null;
					}
					else
					{
						transform = transform.Parent;
					}
				}
				return m;
			}
		}

		public bool LinkedFromPrevious
		{
			get { return Previous != null; }
		}

		/// <summary>
		/// Converts a point from local space to screen space
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public PointF ToScreenPt(float x, float y, Matrix sceneTransform)
		{
			PointF[] pt = new PointF[] { new PointF(x, y) };
			return ToScreenPt(sceneTransform, pt)[0];
		}

		/// <summary>
		/// Converts a point from local space to screen space
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public virtual PointF[] ToScreenPt(Matrix sceneTransform, params PointF[] pts)
		{
			Matrix m = new Matrix();
			m.Multiply(sceneTransform);
			m.Multiply(WorldTransform);
			m.TransformPoints(pts);
			return pts;
		}

		/// <summary>
		/// Converts a point from screen spcae to local space
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="sceneTransform"></param>
		/// <returns></returns>
		public PointF ToLocalPt(float x, float y, Matrix sceneTransform)
		{
			PointF[] pt = new PointF[] { new PointF(x, y) };
			return ToLocalPt(sceneTransform, pt)[0];
		}

		/// <summary>
		/// Converts one or more points in screen space to local space
		/// </summary>
		/// <param name="sceneTransform"></param>
		/// <param name="pts"></param>
		/// <returns></returns>
		public virtual PointF[] ToLocalPt(Matrix sceneTransform, params PointF[] pts)
		{
			Matrix m = new Matrix();
			m.Multiply(sceneTransform);
			m.Multiply(WorldTransform);
			m.Invert();
			m.TransformPoints(pts);
			return pts;
		}

		/// <summary>
		/// Converts a point in local space to world space
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public PointF ToWorldPt(float x, float y)
		{
			PointF[] pt = new PointF[] { new PointF(x, y) };
			return ToWorldPt(pt)[0];
		}
		/// <summary>
		/// Converts one or more points in local space to world space
		/// </summary>
		/// <param name="sceneTransform"></param>
		/// <param name="pts"></param>
		/// <returns></returns>
		public virtual PointF[] ToWorldPt(params PointF[] pts)
		{
			if (Parent == null)
			{
				return pts;
			}
			Matrix m = new Matrix();
			m.Multiply(Parent.WorldTransform);
			m.TransformPoints(pts);
			return pts;
		}

		/// <summary>
		/// Converts a rotation into world space
		/// </summary>
		/// <param name="rot"></param>
		/// <returns></returns>
		public float ToWorldRotation(float rot)
		{
			LiveObject parent = Parent;
			while (parent != null)
			{
				rot += parent.Rotation;
				parent = parent.Parent;
			}
			return rot;
		}

		/// <summary>
		/// Converts one or more points in screen space to world space
		/// </summary>
		/// <param name="sceneTransform"></param>
		/// <param name="pts"></param>
		/// <returns></returns>
		public virtual PointF[] ScreenToWorldPt(Matrix sceneTransform, params PointF[] pts)
		{
			Matrix m = new Matrix();
			m.Multiply(sceneTransform);
			m.Invert();
			m.TransformPoints(pts);
			return pts;
		}

		/// <summary>
		/// Converts a point from world space to local space
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		public PointF WorldToLocalPt(PointF pt)
		{
			PointF[] pts = new PointF[] { pt };
			Matrix m = new Matrix();
			m.Multiply(WorldTransform);
			m.Invert();
			m.TransformPoints(pts);
			return pts[0];
		}

		/// <summary>
		/// Converts a point from screen space to local unscaled space
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public PointF[] ToLocalUnscaledPt(Matrix sceneTransform, params PointF[] pts)
		{
			Matrix m = new Matrix();
			m.Multiply(sceneTransform);
			m.Multiply(UnscaledWorldTransform);
			m.Invert();
			m.TransformPoints(pts);
			return pts;
		}

		public float WorldAlpha
		{
			get
			{
				float alpha = 1;
				LiveObject parent = this;
				while (parent != null)
				{
					alpha *= parent.Alpha / 100.0f;
					parent = parent.Parent;
				}
				return alpha * 100;
			}
		}
		#endregion

		public LiveObject Copy()
		{
			Type type = GetType();
			LiveObject copy = Activator.CreateInstance(type) as LiveObject;
			copy.CenterX = CenterX;
			copy.CenterY = CenterY;
			CopyPropertiesInto(copy);
			copy.Parent = Parent;
			OnCopyTo(copy);
			copy.InvalidateTransform();
			return copy;
		}
		protected virtual void OnCopyTo(LiveObject copy)
		{
		}

		/// <summary>
		/// Adds a property value to a keyframe at the given time
		/// </summary>
		/// <param name="time">Time in seconds from start </param>
		/// <param name="propName"></param>
		/// <param name="serializedValue"></param>
		/// <returns>Keyframe at that point</returns>
		public void AddValue<T>(float time, string propName, string serializedValue)
		{
			AddValue<T>(time, propName, serializedValue, false);
		}

		/// <summary>
		/// Adds a property value to a keyframe at the given time
		/// </summary>
		/// <param name="time">Time in seconds from start </param>
		/// <param name="propName"></param>
		/// <param name="serializedValue"></param>
		/// <returns>Keyframe at that point</returns>
		public virtual void AddValue<T>(float time, string propName, string serializedValue, bool addAnimBreak)
		{
		}

		public void InvalidatePreview()
		{
			PreviewInvalidated?.Invoke(this, EventArgs.Empty);
		}
		public abstract LiveObject CreateLivePreview(float time);
		public abstract void DestroyLivePreview();

		public abstract bool UpdateRealTime(float deltaTime, bool inPlayback);
		public abstract void Update(float time, float elapsedTime, bool inPlayback);

		public abstract void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback);
		public virtual void DrawSelection(Graphics g, Matrix sceneTransform, CanvasState editState, HoverContext hoverContext)
		{
			PointF[] localPts = new PointF[] {
				new PointF(0,0),
				new PointF(Width, 0),
				new PointF(Width, Height),
				new PointF(0, Height),
				new PointF(PivotX * Width, PivotY * Height), //index 4: pivot
			};
			PointF[] boundPts = ToScreenPt(sceneTransform, localPts);
			PointF[] outerPts = new PointF[4];
			for (int i = 0; i < 4; i++)
			{
				outerPts[i] = boundPts[i];
			}

			DrawSelectionBox(g, outerPts);
			DrawHandles(g, outerPts);

			//pivot point
			if (CanPivot)
			{
				if (editState == CanvasState.MovingPivot || hoverContext == HoverContext.Pivot)
				{
					g.MultiplyTransform(UnscaledWorldTransform);
					g.MultiplyTransform(sceneTransform, MatrixOrder.Append);
					g.DrawRectangle(_penPivotBox, 0, 0, Width, Height);
					g.ResetTransform();
				}

				PointF pt = localPts[4];
				g.FillEllipse(Brushes.White, pt.X - 3, pt.Y - 3, 6, 6);
				g.FillEllipse(Brushes.Black, pt.X - 2, pt.Y - 2, 4, 4);
			}
		}

		protected void DrawSelectionBox(Graphics g, params PointF[] vertices)
		{
			g.DrawPolygon(_penOuterSelection, vertices);
			g.DrawPolygon(_penInnerSelection, vertices);
		}

		protected void DrawHandles(Graphics g, params PointF[] vertices)
		{
			//Grab handles
			if (CanScale)
			{
				for (int i = 0; i < vertices.Length; i++)
				{
					PointF pt = vertices[i];
					PointF next = i < vertices.Length - 1 ? vertices[i + 1] : vertices[0];
					PointF mid = new PointF((pt.X + next.X) / 2, (pt.Y + next.Y) / 2);
					g.FillRectangle(_brushHandle, mid.X - 3, mid.Y - 3, 6, 6);
				}
			}
		}

		public bool HiddenByMarker(List<string> markers)
		{
			if (markers != null && !string.IsNullOrEmpty(MarkerName))
			{
				switch (MarkerOp)
				{
					case MarkerOperator.NotEqual:
					case MarkerOperator.LessThan:
					case MarkerOperator.GreaterThan:
						if (markers.Contains(MarkerName) && MarkerValue != "0" || !markers.Contains(MarkerName) && MarkerValue == "0")
						{
							return true;
						}
						break;
					default:
						if (markers.Contains(MarkerName) && MarkerValue == "0" || !markers.Contains(MarkerName) && MarkerValue != "0")
						{
							return true;
						}
						break;
				}
			}
			return false;
		}

		#region Point-and-click editing
		public virtual bool CanTranslate { get { return true; } }
		public virtual bool CanRotate { get { return true; } }
		public virtual bool CanScale { get { return true; } }
		public virtual bool CanSkew { get { return true; } }
		public virtual bool CanPivot { get { return true; } }
		public virtual bool CanArrow { get { return false; } }

		/// <summary>
		/// Sets the object's local position so that its world position is at the given value
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetWorldPosition(PointF worldPos, Matrix sceneTransform)
		{
			PointF local = Parent == null ? worldPos : Parent.WorldToLocalPt(worldPos);
			Translate(local.X, local.Y, sceneTransform);
		}

		/// <summary>
		/// Updates the sprite's position to a new value, updating the underlying data structures too
		/// </summary>
		/// <returns>List of objects that were modified</returns>
		public virtual void Translate(float x, float y, Matrix sceneTransform)
		{
			if (X == x && Y == y)
			{
				return;
			}

			x = (float)Math.Round(x, 0);
			y = (float)Math.Round(y, 0);

			float time = GetRelativeTime();
			AddValue<float>(time, "X", x.ToString(CultureInfo.InvariantCulture));
			AddValue<float>(time, "Y", y.ToString(CultureInfo.InvariantCulture));
		}

		public void AdjustPivot(PointF screenPt, Matrix sceneTransform)
		{
			PointF[] pts = new PointF[] {
				screenPt
			};
			PointF[] localPts = ToLocalUnscaledPt(sceneTransform, pts);
			PointF localPt = localPts[0];
			float xPct = localPt.X / Width;
			float yPct = localPt.Y / Height;

			float pivotX = (float)Math.Round(xPct, 2);
			float pivotY = (float)Math.Round(yPct, 2);
			if (pivotX == PivotX && pivotY == PivotY)
			{
				return;
			}
			PivotX = xPct;
			PivotY = yPct;
		}

		public virtual void Scale(Point screenPoint, Matrix sceneTransform, HoverContext context)
		{
			float time = GetRelativeTime();
			bool horizontal = (context & HoverContext.ScaleHorizontal) != 0;
			bool vertical = (context & HoverContext.ScaleVertical) != 0;

			//scale is determined by first converting point to local space
			PointF localPt = ToLocalPt(sceneTransform, screenPoint)[0];
			PointF pivotPt = new PointF(PivotX * Width, PivotY * Height);

			float scaleX = ScaleX;
			float scaleY = ScaleY;

			if (context.HasFlag(HoverContext.ScaleRight))
			{
				float scaledDist = Width - pivotPt.X;
				float distFromPivot = localPt.X - pivotPt.X;
				float unscaledDist = scaledDist / ScaleX;
				scaleX = distFromPivot / unscaledDist;
			}
			else if (context.HasFlag(HoverContext.ScaleLeft))
			{
				float scaledDist = pivotPt.X;
				float distFromPivot = pivotPt.X - localPt.X;
				float unscaledDist = scaledDist / ScaleX;
				scaleX = distFromPivot / unscaledDist;
			}
			if (context.HasFlag(HoverContext.ScaleBottom))
			{
				float scaledDist = Height - pivotPt.Y;
				float distFromPivot = localPt.Y - pivotPt.Y;
				float unscaledDist = scaledDist / ScaleY;
				scaleY = distFromPivot / unscaledDist;
			}
			else if (context.HasFlag(HoverContext.ScaleTop))
			{
				float scaledDist = pivotPt.Y;
				float distFromPivot = pivotPt.Y - localPt.Y;
				float unscaledDist = scaledDist / ScaleY;
				scaleY = distFromPivot / unscaledDist;
			}
			if (horizontal && !float.IsInfinity(scaleX))
			{
				if (scaleX == 0)
				{
					scaleX = 0.01f;
				}
				scaleX = (float)Math.Round(scaleX, 2);
				if (scaleX != ScaleX)
				{
					AddValue<float>(time, "ScaleX", scaleX.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (vertical && !float.IsInfinity(scaleY))
			{
				if (scaleY == 0)
				{
					scaleY = 0.01f;
				}
				scaleY = (float)Math.Round(scaleY, 2);
				if (scaleY != ScaleY)
				{
					AddValue<float>(time, "ScaleY", scaleY.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		public void Rotate(Point screenPoint, PointF screenPivot, Point downPoint, float initialRotation)
		{
			double downAngle = Math.Atan2(screenPivot.Y - downPoint.Y, screenPivot.X - downPoint.X);
			downAngle = downAngle * (180 / Math.PI) - 90;

			double angle = Math.Atan2(screenPivot.Y - screenPoint.Y, screenPivot.X - screenPoint.X);
			angle = angle * (180 / Math.PI) - 90;

			angle -= downAngle;
			double rotation = Math.Round(initialRotation + angle, 0);

			if (Rotation == rotation)
			{
				return;
			}

			float time = GetRelativeTime();
			Rotation = (float)rotation;
			AddValue<float>(time, "Rotation", Rotation.ToString(CultureInfo.InvariantCulture));
		}

		public void Skew(Point screenPoint, Point downPoint, HoverContext context, float zoom)
		{
			float dx = (screenPoint.X - downPoint.X) / zoom;
			float dy = (screenPoint.Y - downPoint.Y) / zoom;
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

			float time = GetRelativeTime();

			//skew formula: shift = size * tan(radians) / 2
			//solved for angle: angle = atan(2 * shift / size)
			if (HoverContext.SkewHorizontal.HasFlag(context))
			{
				//skewX
				float skewX = (float)(Math.Atan(2 * dx / Height) * 180 / Math.PI);
				AddValue<float>(time, "SkewX", skewX.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				//skewY
				float skewY = (float)(Math.Atan(2 * dy / Width) * 180 / Math.PI);
				AddValue<float>(time, "SkewY", skewY.ToString(CultureInfo.InvariantCulture));
			}
		}

		public virtual void UpdateArrowPosition(HoverContext arrowContext) { }
		#endregion

		public virtual bool FilterRecord(string key)
		{
			return true;
		}

		/// <summary>
		/// Adds this object to a scene using directives
		/// </summary>
		/// <param name="scene">Scene to add to</param>
		/// <returns>A Directive that initially creates this object, if any</returns>
		public virtual Directive AddToScene(Scene scene)
		{
			return null;
		}

		/// <summary>
		/// Sets the previous object that this one is linked to
		/// </summary>
		/// <param name="obj"></param>
		public void SetPrevious(LiveObject obj)
		{
			Previous = obj;
			if (obj != null)
			{
				Id = obj.Id;
			}
			OnSetPrevious();
		}
		protected virtual void OnSetPrevious()
		{
		}

		public object GetDefaultValue(string property)
		{
			switch (property)
			{
				case "Src":
					return "";
				case "ScaleX":
				case "ScaleY":
				case "Zoom":
					return 1.0f;
				case "Alpha":
					return this is LiveCamera ? 0f : 100f;
				case "Color":
					return Color.Black;
				default:
					return 0f;
			}
		}

		public static List<T> SortLayers<T>(ICollection<T> list) where T : LiveObject
		{
			//save off the original order
			Dictionary<T, int> order = new Dictionary<T, int>();
			Dictionary<string, List<T>> buckets = new Dictionary<string, List<T>>();
			foreach (T o in list)
			{
				order[o] = order.Count;
				if (!string.IsNullOrEmpty(o.ParentId))
				{
					List<T> bucket = new List<T>();
					if (!buckets.TryGetValue(o.ParentId, out bucket))
					{
						bucket = new List<T>();
						buckets[o.ParentId] = bucket;
					}
					bucket.Add(o);
				}
			}

			//sort all parentless objects
			List<T> rootObjects = list.Where(o => string.IsNullOrEmpty(o.ParentId)).ToList();
			rootObjects.Sort((s1, s2) =>
			{
				int compare = s1.Z.CompareTo(s2.Z);
				if (compare == 0)
				{
					compare = order[s1].CompareTo(order[s2]);
				}
				return compare;
			});

			//sort each bucket and insert it into the root list after
			foreach (List<T> bucket in buckets.Values)
			{
				string parentId = bucket[0].ParentId;
				for (int i = 0; i < rootObjects.Count; i++)
				{
					T parent = rootObjects[i];
					if (parent.Id == parentId)
					{
						//put the parent into the bucket and sort it
						rootObjects.RemoveAt(i);
						bucket.Add(parent);
						bucket.Sort((s1, s2) =>
						{
							int z1 = (s1 == parent ? 0 : s1.Z);
							int z2 = (s2 == parent ? 0 : s2.Z);
							int compare = z1.CompareTo(z2);
							if (compare == 0)
							{
								compare = order[s1].CompareTo(order[s2]);
							}
							return compare;
						});

						//now insert the whole bucket into the main list where the parent was
						rootObjects.InsertRange(i, bucket);
						break;
					}
				}
			}

			return rootObjects;
		}
	}
}
