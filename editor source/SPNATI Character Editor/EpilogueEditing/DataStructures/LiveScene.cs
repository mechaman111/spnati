using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Editable scene
	/// </summary>
	public class LiveScene : LiveData, ICanvasViewport, ILabel
	{
		public const float MinZoom = 0.25f;
		public const float MaxZoom = 3;

		public ObservableCollection<LiveBreak> Breaks
		{
			get { return Get<ObservableCollection<LiveBreak>>(); }
			set { Set(value); }
		}

		public ObservableCollection<LiveObject> Tracks
		{
			get { return Get<ObservableCollection<LiveObject>>(); }
			set { Set(value); }
		}
		public LiveCamera Camera
		{
			get { return Get<LiveCamera>(); }
			set { Set(value); }
		}

		public bool LockToCamera
		{
			get { return Camera.BlockOutsideView; }
			set { Camera.BlockOutsideView = value; }
		}

		public List<LiveObject> DrawingOrder = new List<LiveObject>();

		public Character Character { get; set; }

		public override event EventHandler<WidgetCreationArgs> WidgetMoved;
		public override event EventHandler<WidgetCreationArgs> WidgetCreated;
		public override event EventHandler<WidgetCreationArgs> WidgetRemoved;
		public event EventHandler ViewportUpdated;
		public event EventHandler LabelChanged;

		[Text(DisplayName = "Name", GroupOrder = 0, Description = "Scene name")]
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); LabelChanged?.Invoke(this, EventArgs.Empty); }
		}

		private Image _background;
		[FileSelect(DisplayName = "Background", GroupOrder = 5, Description = "Scene background image")]
		public string BackgroundImage
		{
			get { return Get<string>(); }
			set
			{
				Set(value);
				_background = LiveImageCache.Get(value);
				if (_background != null)
				{
					Width = _background.Width;
					Height = _background.Height;
				}
			}
		}

		private SolidBrush _backColor = new SolidBrush(Color.Gray);
		[Color(DisplayName = "Background Color", GroupOrder = 10, Description = "Scene background color")]
		public Color BackColor
		{
			get { return Get<Color>(); }
			set
			{
				if (_backColor != null)
				{
					_backColor.Dispose();
				}
				Set(value);
				_backColor = new SolidBrush(value);
			}
		}

		[Numeric(DisplayName = "Width", GroupOrder = 15, Description = "Scene width in pixels when at full scale", Minimum = 100, Maximum = 2000)]
		public int Width
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Numeric(DisplayName = "Height", GroupOrder = 16, Description = "Scene height in pixels when at full scale", Minimum = 100, Maximum = 2000)]
		public int Height
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		private float _time;

		public float AspectRatio { get { return Width / (float)Height; } }

		public LiveScene()
		{
			Width = 100;
			Height = 100;
			Camera = new LiveCamera();
			Tracks = new ObservableCollection<LiveObject>();
			Breaks = new ObservableCollection<LiveBreak>();
		}

		/// <summary>
		/// Updates a path to be relative to opponents
		/// </summary>
		/// <param name="path"></param>
		/// <param name="character"></param>
		public static string FixPath(string path, Character character)
		{
			if (path.StartsWith("/opponents/"))
			{
				return path.Substring("/opponents/".Length);
			}
			else
			{
				return character.FolderName + "/" + path;
			}
		}

		public LiveScene(Scene scene, Character character) : this()
		{
			Character = character;
			Name = scene.Name ?? "New scene";
			if (!string.IsNullOrEmpty(scene.BackgroundColor))
			{
				try
				{
					BackColor = ColorTranslator.FromHtml(scene.BackgroundColor);
				}
				catch { }
			}
			if (!string.IsNullOrEmpty(scene.Background))
			{
				BackgroundImage = FixPath(scene.Background, character);
			}
			if (!string.IsNullOrEmpty(scene.Width))
			{
				int w;
				if (int.TryParse(scene.Width, out w))
				{
					Width = w;
				}
			}
			if (!string.IsNullOrEmpty(scene.Height))
			{
				int h;
				if (int.TryParse(scene.Height, out h))
				{
					Height = h;
				}
			}
			Camera = new LiveCamera(scene);
			Camera.PropertyChanged += Camera_PropertyChanged;
			Tracks.Add(Camera);

			Tracks.CollectionChanged += Objects_CollectionChanged;

			HashSet<LiveAnimatedObject> currentBatch = new HashSet<LiveAnimatedObject>();

			foreach (Directive directive in scene.Directives)
			{
				switch (directive.DirectiveType)
				{
					case "sprite":
						LiveSprite sprite = new LiveSprite(this, directive, Character, _time);
						sprite.PropertyChanged += Sprite_PropertyChanged;
						currentBatch.Add(sprite);
						Tracks.Add(sprite);
						break;
					case "text":
						AddBubbleDirective(directive, currentBatch);
						break;
					case "move":
					case "camera":
						AddMoveDirective(directive, currentBatch);
						break;
					case "fade":
						AddFadeDirective(directive, currentBatch);
						break;
					case "stop":
						AddStopDirective(directive, currentBatch);
						break;
					case "wait":
					case "pause":
						AddPauseDirective(directive, currentBatch);
						break;
					case "remove":
					case "clear":
						AddRemoveDirective(directive, currentBatch);
						break;
					case "clear-all":
						AddClearAllDirective(directive, currentBatch);
						break;
					case "emitter":
						LiveEmitter emitter = new LiveEmitter(this, directive, Character, _time);
						Tracks.Add(emitter);
						break;
					case "emit":
						AddEmitDirective(directive, currentBatch);
						break;
					case "skip":
						break;
				}
			}

			_time = 0;
		}

		private void Camera_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (LockToCamera)
			{
				ViewportUpdated?.Invoke(this, EventArgs.Empty);
			}
		}

		protected override void OnPropertyChanged(string propName)
		{
			if (propName == "Width" && Camera != null)
			{
				Camera.Width = Width;
				if (Camera.LinkedPreview != null)
				{
					Camera.LinkedPreview.Width = Width;
				}
			}
			if (propName == "Height" && Camera != null)
			{
				Camera.Height = Height;
				if (Camera.LinkedPreview != null)
				{
					Camera.LinkedPreview.Height = Height;
				}
			}
		}

		private void AddBubbleDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			//see if any bubbles with this ID already exist
			LiveBubble bubble = null;
			if (!string.IsNullOrEmpty(directive.Id))
			{
				bubble = Tracks.Find(t => t.Id == directive.Id) as LiveBubble;
			}
			if (bubble == null)
			{
				bubble = new LiveBubble(this, directive, GetDelayedTime(directive));
				Tracks.Add(bubble);
			}
			else
			{
				//reusing a bubble; add a keyframe
				float time = GetDelayedTime(directive);
				bubble.AddValue<string>(time - bubble.Start, "Text", directive.Text);
			}
			if (bubble != null)
			{
				batch.Add(bubble);
			}
		}

		private void AddMoveDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			LiveAnimatedObject obj = null;
			if (directive.Id == "camera")
			{
				obj = Camera;
			}
			else
			{
				obj = Tracks.Find(o => o.Id == directive.Id) as LiveAnimatedObject;
			}
			if (obj == null) { return; }

			batch.Add(obj);

			float time = GetDelayedTime(directive);

			//create a keyframe from the directive
			if (directive.Keyframes.Count == 0)
			{
				LiveKeyframe firstFrame;
				obj.AddKeyframe(directive, time - obj.Start, false, out firstFrame);
			}

			//add the directive's keyframes
			obj.AddKeyframeDirective(directive, _time, "smooth", "linear");

			//if the first frame isn't at time 0, need to copy the previous frame's values to there
			if (obj.Keyframes.Count > 0 && (directive.Keyframes.Count == 0 && directive.Time != "0" || directive.Keyframes.Count > 0 && directive.Keyframes[0].Time != "0"))
			{
				int startIndex = 0;
				float relTime = time - obj.Start;
				for (int i = 0; i < obj.Keyframes.Count; i++)
				{
					if (obj.Keyframes[i].Time > relTime)
					{
						startIndex = i;
						break;
					}
				}

				foreach (string property in obj.Properties)
				{
					//for each property, get the first frame from this directive that modified the property
					LiveKeyframe next = null;
					for (int i = startIndex; i < obj.Keyframes.Count; i++)
					{
						if (obj.Keyframes[i].HasProperty(property))
						{
							next = obj.Keyframes[i];
							break;
						}
					}

					if (next != null)
					{
						//then, get the first frame from an earlier directive that modified the property
						LiveKeyframe previous = null;
						for (int i = startIndex - 1; i >= 0; i--)
						{
							if (obj.Keyframes[i].HasProperty(property))
							{
								previous = obj.Keyframes[i];
								break;
							}
						}

						if (previous != null)
						{
							//finally, copy the previous frame's value into the start of this directive
							obj.AddValue<object>(relTime, property, Convert.ToString(previous.Get<object>(property), CultureInfo.InvariantCulture), true);

							//and make the next frame not a begin anymore
							LiveKeyframeMetadata metadata = next.GetMetadata(property, false);
							LiveKeyframeMetadata oldData = obj.Keyframes.Find(k => k.Time == relTime).GetMetadata(property, true);
							metadata.CopyPropertiesInto(oldData);
							metadata.Clear();
							metadata.FrameType = KeyframeType.Normal;
						}
					}
				}
			}
		}

		private void AddRemoveDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			LiveAnimatedObject obj = Tracks.Find(o => o.Id == directive.Id) as LiveAnimatedObject;
			if (obj == null)
			{
				return;
			}
			obj.LinkedToEnd = false;
			obj.Length = GetDelayedTime(directive) - obj.Start;
			batch.Add(obj);
		}

		private void AddClearAllDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			float time = GetDelayedTime(directive);
			foreach (LiveObject obj in Tracks)
			{
				if (obj is LiveBubble && obj.LinkedToEnd)
				{
					obj.LinkedToEnd = false;
					LiveAnimatedObject o = obj as LiveAnimatedObject;
					batch.Add(o);
					o.Length = time - obj.Start;
				}
			}
		}

		private void AddEmitDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			float time = GetDelayedTime(directive);
			LiveEmitter emitter = Tracks.Find(o => o.Id == directive.Id) as LiveEmitter;
			if (emitter == null)
			{
				return;
			}

			batch.Add(emitter);
			LiveBurst burst = emitter.AddEvent(time) as LiveBurst;
			burst.Count = directive.Count;
		}

		private void AddStopDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			LiveAnimatedObject obj = null;
			if (directive.Id == "camera")
			{
				obj = Camera;
			}
			else
			{
				obj = Tracks.Find(o => o.Id == directive.Id) as LiveAnimatedObject;
			}
			if (obj == null) { return; }

			batch.Add(obj);
			float time = GetDelayedTime(directive);
			obj.AddStopDirective(directive, time);
		}

		private float GetDelayedTime(Directive directive)
		{
			float delay;
			if (!string.IsNullOrEmpty(directive.Delay) && float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out delay))
			{
				return _time + delay;
			}
			return _time;
		}

		private void AddPauseDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			//update the current start basis to match the end time of the last non-looping animation
			float max = _time;
			foreach (LiveObject obj in Tracks)
			{
				LiveAnimatedObject animObj = obj as LiveAnimatedObject;
				if (animObj == null)
				{
					continue;
				}
				float end = GetEnd(animObj) + 1;
				max = Math.Max(end, max);
			}
			_time = Math.Max(max, _time);
			if (directive.DirectiveType == "pause")
			{
				LiveBreak brk = new LiveBreak();
				brk.Data = this;
				brk.Time = _time;
				Breaks.Add(brk);
			}
		}

		private void AddFadeDirective(Directive directive, HashSet<LiveAnimatedObject> batch)
		{
			LiveCamera camera = Camera;
			camera.AddKeyframeDirective(directive, _time, "smooth", "linear");
			batch.Add(camera);
		}

		private static float GetEnd(LiveAnimatedObject obj)
		{
			float end = obj.Start;
			if (obj.Keyframes.Count == 0)
			{
				end += obj.Time;
			}
			else
			{
				foreach (string prop in obj.AnimatedProperties)
				{
					for (int i = obj.Keyframes.Count - 1; i >= 0; i--)
					{
						LiveKeyframe kf = obj.Keyframes[i];
						if (kf.HasProperty(prop))
						{
							LiveKeyframeMetadata metadata = obj.GetBlockMetadata(prop, kf.Time);
							if (!metadata.Looped)
							{
								if (!obj.LinkedToEnd)
								{
									end = Math.Max(end, obj.Start);
								}
								else
								{
									end = Math.Max(end, obj.Start + kf.Time);
								}
								break;
							}
						}
					}
				}
			}

			return end;
		}

		public override int BaseHeight
		{
			get { return Height; }
			set { Height = value; }
		}

		public bool AllowPan
		{
			get { return !LockToCamera; }
		}

		private void Sprite_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Z")
			{
				ReorderObjects();
			}
		}

		private void Objects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ReorderObjects();
		}

		private void ReorderObjects()
		{
			Dictionary<LiveObject, int> order = new Dictionary<LiveObject, int>();
			DrawingOrder.Clear();
			for (int i = 0; i < Tracks.Count; i++)
			{
				order[Tracks[i]] = i;
				LiveObject obj = Tracks[i];
				if (obj is LiveSprite || obj is LiveEmitter)
				{
					DrawingOrder.Add(obj);
				}
			}
			DrawingOrder.Sort((s1, s2) =>
			{
				int compare = s1.Z.CompareTo(s2.Z);
				if (compare == 0)
				{
					compare = order[s1].CompareTo(order[s2]);
				}
				return compare;
			});
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, LiveObject selectedObject, LiveObject selectedPreview, bool inPlayback)
		{
			g.FillRectangle(_backColor, 0, 0, g.VisibleClipBounds.Width, g.VisibleClipBounds.Height);

			if (_background != null)
			{
				g.MultiplyTransform(sceneTransform);
				g.DrawImage(_background, 0, 0);
				g.ResetTransform();
			}

			foreach (LiveObject obj in DrawingOrder)
			{
				obj.Draw(g, sceneTransform, markers, inPlayback);
				if (selectedObject == obj && !selectedObject.Hidden && selectedPreview != null)
				{
					selectedPreview.Draw(g, sceneTransform, markers, inPlayback);
				}
			}

			//camera bounds
			Camera.Draw(g, sceneTransform, markers, inPlayback);

			//textboxes
			g.MultiplyTransform(Camera.WorldTransform);
			g.MultiplyTransform(sceneTransform, MatrixOrder.Append);
			foreach (LiveObject obj in Tracks)
			{
				if (obj is LiveBubble)
				{
					obj.Draw(g, sceneTransform, markers, inPlayback);
				}
			}
			g.ResetTransform();
		}

		public void FitToViewport(int windowWidth, int windowHeight, ref Point offset, ref float zoom)
		{
			//adjust zoom level
			float aspectRatio = AspectRatio;
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
			float camZoom = Camera.Zoom;
			zoom = viewportHeight * camZoom / Height;

			//center on camera
			int cx = (int)(Camera.X + Camera.Width / 2);
			int cy = (int)(Camera.Y + Camera.Height / 2);

			float camWidth = Camera.Width / Camera.Zoom;
			float camHeight = Camera.Height / Camera.Zoom;

			int scaledWidth = (int)(zoom * camWidth);
			int scaledHeight = (int)(zoom * camHeight);

			int x0 = (int)((cx - camWidth / 2) * zoom);
			int y0 = (int)((cy - camHeight / 2) * zoom);

			offset = new Point(-x0 + (windowWidth - scaledWidth) / 2, -y0 + (windowHeight - scaledHeight) / 2);
		}

		/// <summary>
		/// Adjusts the zoom and offset to fit the scene on the screen
		/// </summary>
		/// <param name="windowWidth"></param>
		/// <param name="windowHeight"></param>
		/// <param name="offset"></param>
		/// <param name="zoom"></param>
		public override void FitScene(int windowWidth, int windowHeight, ref Point offset, ref float zoom)
		{
			if (LockToCamera)
			{
				FitToViewport(windowWidth, windowHeight, ref offset, ref zoom);
				return;
			}
			offset = new Point(0, 0);

			//adjust zoom level
			float aspectRatio = AspectRatio;
			float viewWidth = aspectRatio * windowHeight;

			int viewportHeight = 0;
			if (viewWidth > windowWidth)
			{
				//take full width of window
				viewportHeight = (int)(windowWidth / aspectRatio);
				offset = new Point(0, (windowHeight - (int)viewportHeight) / 2);
			}
			else
			{
				//take full height of window
				viewportHeight = windowHeight;
				offset = new Point((windowWidth - (int)viewWidth) / 2, 0);
			}

			zoom = (float)viewportHeight / Height;
		}

		public override Matrix GetSceneTransform(int windowWidth, int windowHeight, Point offset, float zoom)
		{
			Matrix transform = new Matrix();
			float screenScale = zoom;
			transform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display * zoom
			transform.Translate(offset.X, offset.Y, MatrixOrder.Append); // center horizontally
			return transform;
		}

		public override LiveObject Find(string id)
		{
			return null;
		}

		public override LiveObject GetObjectAtPoint(int x, int y, Matrix sceneTransform, bool ignoreMarkers, List<string> markers)
		{
			LiveObject previewSource = Tracks.Find(t => t.LinkedPreview != null);
			if (previewSource != null && !(previewSource is LiveBubble))
			{
				PointF localPt = previewSource.ToLocalPt(x, y, sceneTransform);
				if (localPt.X >= 0 && localPt.X <= previewSource.Width &&
					localPt.Y >= 0 && localPt.Y <= previewSource.Height)
				{
					return previewSource;
				}
			}

			//search in reverse order because objects are sorted by depth
			for (int i = Tracks.Count - 1; i >= 0; i--)
			{
				Matrix boxTransform = new Matrix();
				boxTransform.Multiply(Camera.WorldTransform);
				boxTransform.Multiply(sceneTransform, MatrixOrder.Append);
				Point[] mousePt = new Point[] { new Point(x, y) };
				boxTransform.Invert();
				boxTransform.TransformPoints(mousePt);
				LiveBubble bubble = Tracks[i] as LiveBubble;
				if (bubble == null || !bubble.IsVisible || bubble.Hidden) { continue; }

				if (bubble.Contains(mousePt[0], sceneTransform))
				{
					return bubble;
				}
			}

			for (int i = DrawingOrder.Count - 1; i >= 0; i--)
			{
				LiveObject obj = DrawingOrder[i];
				if (!obj.IsVisible || obj.Hidden || obj.Alpha == 0) { continue; }

				//transform point to local space
				PointF localPt = obj.ToLocalPt(x, y, sceneTransform);
				if (localPt.X >= 0 && localPt.X <= obj.Width &&
					localPt.Y >= 0 && localPt.Y <= obj.Height)
				{
					return obj;
				}
			}

			//see if the camera is selected
			PointF localToCam = Camera.ToLocalPt(x, y, sceneTransform);
			if (localToCam.X >= 0 && localToCam.X <= Camera.Width && localToCam.Y >= 0 && localToCam.Y <= Camera.Height)
			{
				return Camera;
			}
			return null;
		}

		public override void UpdateTime(float time, float elapsedTime, bool inPlayback)
		{
			_time = time;
			foreach (LiveObject obj in Tracks)
			{
				obj.Update(time, elapsedTime, inPlayback);
			}
		}

		public override void UpdateRealTime(float deltaTime, bool inPlayback)
		{
			foreach (LiveObject sprite in Tracks)
			{
				sprite.UpdateRealTime(deltaTime, inPlayback);
			}
		}

		public LiveSprite AddSprite(float time, string src)
		{
			LiveSprite sprite = new LiveSprite(this, time);
			sprite.LinkedToEnd = true;
			sprite.CenterX = false;
			sprite.DisplayPastEnd = false;
			sprite.PropertyChanged += Sprite_PropertyChanged;
			Tracks.Add(sprite);
			ReorderObjects();
			return sprite;
		}

		public LiveBubble AddBubble(float time)
		{
			LiveBubble bubble = new LiveBubble(this, time);
			bubble.Id = GetUniqueId("text");
			bubble.LinkedToEnd = true;
			Tracks.Add(bubble);
			return bubble;
		}

		public LiveEmitter AddEmitter(float time)
		{
			LiveEmitter emitter = new LiveEmitter(this, time);
			emitter.Id = GetUniqueId("emitter");
			emitter.LinkedToEnd = true;
			Tracks.Add(emitter);
			ReorderObjects();
			return emitter;
		}

		public override List<LiveObject> GetAvailableParents(LiveObject child)
		{
			List<LiveObject> list = new List<LiveObject>();
			if (child is LiveBubble)
			{
				return list;
			}
			foreach (LiveObject obj in Tracks)
			{
				if (string.IsNullOrEmpty(obj.Id) || obj == child || obj is LiveBubble)
				{
					continue;
				}

				//disallow ancestors
				LiveObject parent = obj.Parent;
				bool isAncestor = false;
				while (parent != null)
				{
					if (parent == child)
					{
						isAncestor = true;
						break;
					}
					parent = parent.Parent;
				}
				if (!isAncestor)
				{
					list.Add(obj);
				}
			}
			list.Sort();
			return list;
		}

		public override List<ITimelineWidget> CreateWidgets(Timeline timeline)
		{
			List<ITimelineWidget> list = new List<ITimelineWidget>();
			for (int i = 0; i < Tracks.Count; i++)
			{
				ITimelineWidget widget = Tracks[i].CreateWidget(timeline);
				if (widget.IsCollapsible)
				{
					widget.IsCollapsed = true;
				}
				list.Add(widget);
			}
			return list;
		}
		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object context)
		{
			if (context is LiveObject)
			{
				LiveObject obj = context as LiveObject;
				ITimelineWidget widget = obj.CreateWidget(timeline);
				if (widget.IsCollapsible)
				{
					widget.IsCollapsed = true;
				}
				return widget;
			}

			throw new NotSupportedException();
		}

		public string GetUniqueId(string id)
		{
			int suffix = 0;
			string prefix = id;
			while (Tracks.Find(s => s.Id == id) != null)
			{
				suffix++;
				id = prefix + suffix;
			}

			return id;
		}

		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index)
		{
			LiveObject obj = data as LiveObject;
			if (obj != null)
			{
				obj.Id = GetCopyId(obj.Id);
				AddObject(obj, index);
				ITimelineWidget widget = obj.CreateWidget(timeline);
				return widget;
			}
			throw new NotSupportedException("Cannot create a widget for " + data.GetType().Name);
		}

		private string GetCopyId(string id)
		{
			HashSet<string> ids = new HashSet<string>();
			foreach (LiveObject obj in Tracks)
			{
				ids.Add(obj.Id);
			}
			string prefix = id;
			string newId = id;
			int suffix = 0;
			while (ids.Contains(newId))
			{
				if (prefix == id)
				{
					prefix += "(copy)";
					newId = prefix;
				}
				else
				{
					++suffix;
					newId = prefix + suffix;
				}
			}
			return newId;
		}

		public override List<ITimelineBreak> CreateBreaks(Timeline timeline)
		{
			List<ITimelineBreak> list = new List<ITimelineBreak>();
			list.AddRange(Breaks);
			return list;
		}

		/// <summary>
		/// Gets the nearest time after the given time where a break can appear
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public float GetBreakTime(float time)
		{
			float latestEnd = -1;
			float earliestBegin = float.MaxValue;
			foreach (LiveObject obj in Tracks)
			{
				if (obj is LiveBubble)
				{
					continue; //speech bubbles don't have any bearing on break points
				}
				LiveAnimatedObject anim = obj as LiveAnimatedObject;
				if (anim != null)
				{
					if (anim.Keyframes.Count > 0)
					{
						foreach (string property in anim.Properties)
						{
							LiveKeyframe start;
							LiveKeyframe end;
							anim.GetBlock(property, time, true, out start, out end);
							if (start != null && end != null)
							{
								float startTime = start.Time + obj.Start;
								float endTime = end.Time + obj.Start;
								if (startTime > time)
								{
									//this block starts after the desired time, so don't consider its end point
									earliestBegin = Math.Min(earliestBegin, obj.Start + start.Time);
								}
								else
								{
									if (endTime < time)
									{
										//if this ends before the desired time, don't consider it
										continue;
									}
									else
									{
										//desired time is inside this block, so use the end point
										latestEnd = Math.Max(latestEnd, endTime);
									}
								}
							}
						}
					}
					else if (anim.Keyframes.Count == 1)
					{
						if (obj.Start <= time)
						{
							latestEnd = Math.Max(obj.Start + anim.Length, latestEnd);
						}
					}
				}
			}

			if (latestEnd != -1)
			{
				return latestEnd;
			}
			else if (earliestBegin != float.MaxValue)
			{
				return earliestBegin;
			}
			return time;
		}

		public override ITimelineBreak AddBreak(float time)
		{
			//figure out the next appropriate break point based on the animations underway at this time
			time = GetBreakTime(time);

			//see if there's already a breakpoint at this time and abort if so
			LiveBreak existing = Breaks.Find(b => b.Time == time);
			if (existing != null)
			{
				return null;
			}

			LiveBreak brk = new LiveBreak();
			brk.Data = this;
			brk.Time = time;
			Breaks.Add(brk);
			return brk;
		}

		/// <summary>
		/// Adds a sprite to the end of the collection
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private LiveObject AddObject(LiveObject obj, int index)
		{
			obj.Data = this;
			obj.PropertyChanged += Sprite_PropertyChanged;
			if (index == -1)
			{
				Tracks.Add(obj);
			}
			else
			{
				Tracks.Insert(index, obj);
			}
			return obj;
		}

		public override void InsertWidget(ITimelineObject widget, float time, int index)
		{
			if (widget is KeyframedWidget)
			{
				KeyframedWidget objWidget = widget as KeyframedWidget;
				LiveObject data = objWidget.GetData() as LiveObject;
				data.PropertyChanged -= Sprite_PropertyChanged;
				if (index == -1)
				{
					Tracks.Add(data);
					index = Tracks.Count - 1;
				}
				else
				{
					Tracks.Insert(index, data);
				}
			}
			else if (widget is LiveBreak)
			{
				throw new NotImplementedException();
			}
			WidgetCreated?.Invoke(this, new WidgetCreationArgs(widget, index));
		}
		public override int RemoveWidget(ITimelineObject widget)
		{
			if (widget is KeyframedWidget)
			{
				KeyframedWidget objWidget = widget as KeyframedWidget;
				LiveAnimatedObject data = objWidget.GetData() as LiveAnimatedObject;
				int index = Tracks.IndexOf(data);
				if (index >= 0)
				{
					Tracks.RemoveAt(index);
					data.PropertyChanged -= Sprite_PropertyChanged;
					WidgetRemoved?.Invoke(this, new WidgetCreationArgs(widget, index));
				}
				return index;
			}
			else if (widget is LiveBreak)
			{
				Breaks.Remove(widget as LiveBreak);
				WidgetRemoved?.Invoke(this, new WidgetCreationArgs(widget, -1));
			}
			return -1;
		}
		public override void MoveWidget(ITimelineObject widget, int newTrack)
		{
			if (widget is ITimelineWidget)
			{
				LiveObject data = ((ITimelineWidget)widget).GetData() as LiveObject;
				int index = Tracks.IndexOf(data);
				Tracks.RemoveAt(index);
				if (newTrack >= Tracks.Count || newTrack == -1)
				{
					Tracks.Add(data);
				}
				else
				{
					Tracks.Insert(newTrack, data);
				}
			}
			else if (widget is LiveBreak)
			{
				throw new NotImplementedException();
			}
			WidgetMoved?.Invoke(this, new WidgetCreationArgs(widget, newTrack));
		}
		public override bool OnPaste(WidgetOperationArgs args)
		{
			return Paste(args, null);
		}
		public override void UpdateSelection(WidgetSelectionArgs args)
		{
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			args.AllowCut = false;
			args.AllowCopy = false;
			args.AllowDelete = false;
			args.AllowDuplicate = false;
			args.AllowPaste = false;
			if (clipboardData is LiveAnimatedObject)
			{
				args.AllowPaste = true;
			}
		}
		public override bool Paste(WidgetOperationArgs args, LiveObject after)
		{
			int index = -1;
			if (after != null)
			{
				index = Tracks.IndexOf(after as LiveObject) + 1;
			}
			LiveObject clipboardData = Clipboards.Get<KeyframedWidget, LiveObject>();
			if (clipboardData != null)
			{
				args.Timeline.CreateWidget(clipboardData.Copy(), index);
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return GetLabel();
		}

		public string GetLabel()
		{
			return Name ?? "Unnamed scene";
		}
	}
}
