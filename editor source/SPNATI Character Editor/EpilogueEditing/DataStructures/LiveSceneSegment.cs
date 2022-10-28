using Desktop;
using Desktop.CommonControls.PropertyControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// A single block of a scene (animations, text boxes, etc. up to the next pause)
	/// </summary>
	public class LiveSceneSegment : LiveData, ICanvasViewport, ILabel
	{
		public const float MinZoom = 0.25f;
		public const float MaxZoom = 3;

		public LiveScene Scene;

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

		/// <summary>
		/// The segment preceding this one, if any
		/// </summary>
		public LiveSceneSegment PreviousSegment;

		[Text(DisplayName = "Name", GroupOrder = 0, Description = "Scene name")]
		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); LabelChanged?.Invoke(this, EventArgs.Empty); }
		}

		[Text(DisplayName = "Marker", GroupOrder = 10, Description = "Conditional marker")]
		public string Marker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		/// <summary>
		/// Current absolute time from the last pause. Delays are relative to this
		/// </summary>
		private float _time;
		/// <summary>
		/// Absolute time to jump to after the next pause
		/// </summary>
		private float _nextTime;

		public float AspectRatio { get { return Scene.Width / (float)Scene.Height; } }

		public LiveSceneSegment()
		{
			Camera = new LiveCamera();
			Tracks = new ObservableCollection<LiveObject>();
		}

		/// <summary>
		/// Updates a path to be relative to opponents
		/// </summary>
		/// <param name="path"></param>
		/// <param name="character"></param>
		public static string FixPath(string path, Character character)
		{
			if (character == null)
			{
				return path;
			}
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			if (path.StartsWith("/opponents/"))
			{
				return path.Substring("/opponents/".Length);
			}
			else
			{
				return character.FolderName + "/" + path;
			}
		}

		public LiveSceneSegment(LiveScene scene, Character character) : this()
		{
			Character = character;
			Scene = scene;
			ActivateForEdit();
			Camera.PropertyChanged += Camera_PropertyChanged;
			Camera.Data = this;
			Tracks.Add(Camera);

			Tracks.CollectionChanged += Objects_CollectionChanged;
		}

		public void ActivateForEdit()
		{
			Camera.Width = Scene.Width;
			Camera.Height = Scene.Height;
		}

		/// <summary>
		/// Gets the time of the latest keyframe in this scene
		/// </summary>
		/// <returns></returns>
		public override float GetDuration()
		{
			return Tracks.Select(t =>
			{
				IFixedLength l = t as IFixedLength;
				if (l != null)
				{
					return t.Start + l.Length;
				}
				else
				{
					return t.Start;
				}
			}).Max();
		}

		public void AddMetadataDirective(Directive directive)
		{
			Name = directive.Name;
			Marker = directive.Marker;
		}

		public void AddSpriteDirective(Directive directive, HashSet<LiveObject> currentBatch)
		{
			LiveSprite sprite = new LiveSprite(this, directive, Character, _time);
			sprite.PropertyChanged += Sprite_PropertyChanged;
			currentBatch.Add(sprite);
			Tracks.Add(sprite);

			_nextTime = Math.Max(_nextTime, sprite.Start);
		}

		public void AddEmitterDirective(Directive directive, HashSet<LiveObject> currentBatch)
		{
			LiveEmitter emitter = new LiveEmitter(this, directive, Character, _time);
			emitter.PropertyChanged += Sprite_PropertyChanged;
			currentBatch.Add(emitter);
			Tracks.Add(emitter);

			_nextTime = Math.Max(_nextTime, emitter.Start);
		}

		public LiveSceneSegment(LiveScene liveScene, Scene scene, Character character) : this()
		{
			Character = character;
			Scene = liveScene;

			Camera = new LiveCamera(scene);
			Camera.Data = this;
			Camera.PropertyChanged += Camera_PropertyChanged;
			Tracks.Add(Camera);

			Tracks.CollectionChanged += Objects_CollectionChanged;

			HashSet<LiveObject> currentBatch = new HashSet<LiveObject>();

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
					case "fade":
						AddMoveDirective(directive, currentBatch);
						break;
					case "stop":
						AddStopDirective(directive, currentBatch);
						break;
					case "wait":
					case "pause":
						AddPauseDirective();
						break;
					case "remove":
						AddRemoveDirective(directive, currentBatch);
						break;
					case "clear":
						AddClearDirective(directive, currentBatch);
						break;
					case "clear-all":
						AddClearAllDirective(directive, currentBatch);
						break;
					case "emitter":
						AddEmitterDirective(directive, currentBatch);
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

		public void AddBubbleDirective(Directive directive, HashSet<LiveObject> batch)
		{
			//see if any bubbles with this ID already exist
			LiveBubble bubble = null;
			string directiveId = directive.Id;
			string newId = directiveId;

			string remappedId = null;
			if (!string.IsNullOrEmpty(directiveId))
			{
				Scene.IdRemap.TryGetValue(directiveId, out remappedId);
				bubble = Tracks.Find(t =>
				{
					return t.Id == directiveId || t.Id == remappedId;
				}) as LiveBubble;
			}

			if (bubble != null)
			{
				//reusing a bubble; make a new ID suffix and remove the old one at this point
				AddClearDirective(new Directive("clear")
				{
					Id = directiveId,
					Delay = directive.Delay
				}, batch);
				if (directive.Delay == null)
				{
					Tracks.Remove(bubble);
					Scene.IdRemap.Remove(directiveId);
				}

				int suffix = 0;
				string id = directiveId + (suffix > 0 ? suffix.ToString() : "");
				while (Scene.IdRemap.ContainsKey(id))
				{
					suffix++;
					id = directiveId + suffix;
				}
				newId = id;
			}

			bubble = new LiveBubble(this, directive, GetDelayedTime(directive));
			bubble.Id = newId;
			if (!string.IsNullOrEmpty(directiveId))
			{
				Scene.IdRemap[directiveId] = newId;
			}
			Tracks.Add(bubble);

			_nextTime = Math.Max(_nextTime, bubble.Start);

			batch.Add(bubble);
		}

		public void AddMoveDirective(Directive directive, HashSet<LiveObject> batch)
		{
			LiveAnimatedObject obj = null;
			if (directive.DirectiveType == "camera" || directive.DirectiveType == "fade")
			{
				obj = Camera;
			}
			else
			{
				obj = Tracks.Find(o => o.Id == directive.Id) as LiveAnimatedObject;
			}
			if (obj == null) { return; }

			batch.Add(obj);

			_nextTime = Math.Max(_nextTime, GetEndTime(directive));

			//add the directive's keyframes
			obj.AddKeyframeDirective(directive, _time, "smooth", "linear", true);
		}

		public void AddRemoveDirective(Directive directive, HashSet<LiveObject> batch)
		{
			LiveAnimatedObject obj = Tracks.Find(o => o.Id == directive.Id) as LiveAnimatedObject;
			if (obj == null)
			{
				return;
			}
			float time = GetDelayedTime(directive);
			if (time == 0)
			{
				//cleared at the start of the segment, no need to keep the track around
				Tracks.Remove(obj);
				if (obj.Previous != null && PreviousSegment != null)
				{
					obj.Previous.LinkedToEnd = false;
					LiveAnimatedObject anim = obj.Previous as LiveAnimatedObject;
					float length = PreviousSegment.GetDuration() - obj.Previous.Start;
					if (anim.Keyframes.Count > 0 && anim.Keyframes[anim.Keyframes.Count - 1].Time < length)
					{
						//make sure there's a keyframe at the desired length, or it won't be able to stretch out long enough
						anim.AddKeyframe(length);
					}
					(obj.Previous as IFixedLength).Length = length;
				}
			}
			else
			{
				//cleared mid-segment
				obj.LinkedToEnd = false;
				obj.Length = time - obj.Start;
			}
			batch.Remove(obj);
		}

		private void ClearBubble(float time, LiveBubble bubble, HashSet<LiveObject> toRemove)
		{
			if (time == 0)
			{
				//cleared at the start of the segment, no need to keep the track around
				Tracks.Remove(bubble);
				if (bubble.Previous != null && PreviousSegment != null)
				{
					bubble.Previous.LinkedToEnd = false;
					(bubble.Previous as LiveBubble).Length = PreviousSegment.GetDuration() - bubble.Previous.Start;
				}
			}
			else
			{
				//cleared mid-segment
				bubble.LinkedToEnd = false;
				bubble.Length = time - bubble.Start;
			}
			toRemove.Add(bubble);
		}

		public void AddClearDirective(Directive directive, HashSet<LiveObject> batch)
		{
			string id = directive.Id;
			if (Scene.IdRemap.ContainsKey(id))
			{
				id = Scene.IdRemap[id];
			}
			LiveBubble bubble = batch.FirstOrDefault(b => b.Id == id && b is LiveBubble) as LiveBubble;
			if (bubble != null)
			{
				float time = GetDelayedTime(directive);
				ClearBubble(time, bubble, batch);
				batch.Remove(bubble);
			}
		}

		public void AddClearAllDirective(Directive directive, HashSet<LiveObject> batch)
		{
			float time = GetDelayedTime(directive);
			HashSet<LiveObject> toRemove = new HashSet<LiveObject>();
			foreach (LiveObject obj in batch)
			{
				LiveBubble bubble = obj as LiveBubble;
				if (bubble != null)
				{
					ClearBubble(time, bubble, toRemove);
				}
			}
			foreach (LiveObject obj in toRemove)
			{
				batch.Remove(obj);
			}
		}

		public void AddEmitDirective(Directive directive, HashSet<LiveObject> batch)
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

		public void AddStopDirective(Directive directive, HashSet<LiveObject> batch)
		{
			LiveAnimatedObject obj = null;
			if (directive.Id == "camera" || directive.Id == "fade")
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

		/// <summary>
		/// Gets the overall time at the end of a directive
		/// </summary>
		/// <param name="directive"></param>
		/// <returns></returns>
		private float GetEndTime(Directive directive)
		{
			float delay = GetDelayedTime(directive);
			if (directive.Looped)
			{
				return delay;
			}
			string totalTime = directive.Keyframes.Count > 0 ? directive.Keyframes[directive.Keyframes.Count - 1].Time : directive.Time;
			float endTime;
			float.TryParse(totalTime ?? "0", NumberStyles.Number, CultureInfo.InvariantCulture, out endTime);
			return delay + endTime;
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

		public void AddPauseDirective()
		{
			//update the current start basis to match the end time of the last non-looping animation
			_time = _nextTime;
		}

		public override int BaseHeight
		{
			get { return Scene.Height; }
			set { Scene.Height = value; }
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
			else if (e.PropertyName == "Id")
			{
				string newId = (sender as LiveObject)?.Id;
				if (!string.IsNullOrEmpty(newId))
				{
					//relink children
					foreach (LiveObject track in Tracks)
					{
						if (track.Parent == sender)
						{
							track.ParentId = newId;
						}
					}
				}
			}
		}

		private void Objects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ReorderObjects();
		}

		private void ReorderObjects()
		{
			//Dictionary<LiveObject, int> order = new Dictionary<LiveObject, int>();
			DrawingOrder.Clear();
			for (int i = 0; i < Tracks.Count; i++)
			{
				//order[Tracks[i]] = i;
				LiveObject obj = Tracks[i];
				if (obj is LiveSprite || obj is LiveEmitter)
				{
					DrawingOrder.Add(obj);
				}
			}
			DrawingOrder = LiveObject.SortLayers(DrawingOrder);
			//DrawingOrder.Sort((s1, s2) =>
			//{
			//	int compare = s1.Z.CompareTo(s2.Z);
			//	if (compare == 0)
			//	{
			//		compare = order[s1].CompareTo(order[s2]);
			//	}
			//	return compare;
			//});
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, LiveObject selectedObject, LiveObject selectedPreview, bool inPlayback)
		{
			Scene.Draw(g, sceneTransform, false);

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
			zoom = viewportHeight * camZoom / Scene.Height;

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

			zoom = (float)viewportHeight / Scene.Height;
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
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}
			foreach (LiveObject obj in Tracks)
			{
				if (obj.Id == id)
				{
					return obj;
				}
			}
			return null;
		}

		public override LiveObject GetObjectAtPoint(int x, int y, Matrix sceneTransform, bool ignoreMarkers, List<string> markers)
		{
			LiveObject previewSource = Tracks.Find(t => t.LinkedPreview != null);
			if (previewSource != null)
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
				LiveBubble bubble = Tracks[i] as LiveBubble;
				if (bubble == null || !bubble.IsVisible || bubble.Hidden) { continue; }
				Matrix boxTransform = new Matrix();
				boxTransform.Multiply(Camera.WorldTransform);
				boxTransform.Multiply(sceneTransform, MatrixOrder.Append);
				Point[] mousePt = new Point[] { new Point(x, y) };
				boxTransform.Invert();
				boxTransform.TransformPoints(mousePt);

				if (bubble.Contains(mousePt[0]))
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
			const float CamLeeway = 20;
			PointF localToCam = Camera.ToLocalPt(x, y, sceneTransform);
			float camLeft = Math.Abs(localToCam.X);
			float camRight = Math.Abs(Camera.Width - localToCam.X);
			float camTop = Math.Abs(localToCam.Y);
			float camBottom = Math.Abs(Camera.Height - localToCam.Y);
			if ((camLeft <= CamLeeway || camRight <= CamLeeway) && (camTop <= CamLeeway || camBottom <= CamLeeway))
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

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			bool invalidated = false;
			foreach (LiveObject sprite in Tracks)
			{
				invalidated = sprite.UpdateRealTime(deltaTime, inPlayback) || invalidated;
			}
			return invalidated;
		}

		public LiveSprite AddSprite(float time)
		{
			LiveSprite sprite = new LiveSprite(this, time);
			sprite.LinkedToEnd = true;
			sprite.CenterX = false;
			sprite.PreserveOriginalDimensions = true;
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
				if (string.IsNullOrEmpty(obj.Id) || obj == child || obj is LiveBubble || obj is LiveCamera)
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
			WidgetCreated?.Invoke(this, new WidgetCreationArgs(widget, index));
		}
		public override int RemoveWidget(ITimelineObject widget)
		{
			LiveObject data = widget.GetData() as LiveObject;
			if (data != null)
			{
				int index = Tracks.IndexOf(data);
				if (index >= 0)
				{
					Tracks.RemoveAt(index);
					data.PropertyChanged -= Sprite_PropertyChanged;
					WidgetRemoved?.Invoke(this, new WidgetCreationArgs(widget, index));
				}
				return index;
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

		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(Name))
				{
					return Name;
				}
				LiveBubble firstText = Tracks.Find(t => t is LiveBubble && !t.LinkedFromPrevious) as LiveBubble;
				if (firstText != null)
				{
					return firstText.Text;
				}
				LiveAnimatedObject firstAnim = Tracks.Find(t => t is LiveAnimatedObject && ((LiveAnimatedObject)t).Keyframes.Count > 1) as LiveAnimatedObject;
				if (firstAnim != null)
				{
					return $"Move {firstAnim.Id}";
				}
				return GetLabel();
			}
		}

		public string GetLabel()
		{
			return Name ?? "Action";
		}

		public override List<ITimelineBreak> CreateBreaks(Timeline timeline)
		{
			return new List<ITimelineBreak>();
		}

		public override ITimelineBreak AddBreak(float time)
		{
			throw new NotImplementedException();
		}
	}
}
