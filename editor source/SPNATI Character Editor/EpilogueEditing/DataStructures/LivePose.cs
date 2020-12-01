using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using Desktop;
using Desktop.CommonControls.PropertyControls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LivePose : LiveData, ILabel
	{
		public ISkin Character;
		public Pose Pose;

		private int _stage;
		public int CurrentStage
		{
			get { return _stage; }
			set
			{
				_stage = value;
				UpdateSpriteStages();
			}
		}

		public event EventHandler LabelChanged;

		public ObservableCollection<LiveSprite> Sprites
		{
			get { return Get<ObservableCollection<LiveSprite>>(); }
			set { Set(value); }
		}
		public List<LiveSprite> DrawingOrder = new List<LiveSprite>();

		[Text(DisplayName = "Id", GroupOrder = 0)]
		public string Id
		{
			get { return Get<string>(); }
			set
			{
				if (value == null || Id == value)
				{
					return; //prevent null IDs
				}
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
				if (AllowsCrossStageImages)
				{
					UpdateSpriteStages();
				}
			}
		}

		[Numeric(DisplayName = "Base Height", Key = "baseHeight", GroupOrder = 10, Minimum = 1, Maximum = 50000)]
		public override int BaseHeight
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Boolean(DisplayName = "Cross Stage", Key = "crossStage", GroupOrder = 15, Description = "When checked, sprite images will dynamically change to the current stage for their prefix (ex. 1-happy.png, 2-happy.png).")]
		public bool CrossStage
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		private float _time;

		public LivePose()
		{
			Sprites = new ObservableCollection<LiveSprite>();
		}
		public LivePose(ISkin character, Pose pose, int stage)
		{
			Character = character;
			CurrentStage = stage;

			ConvertPose(pose);
		}

		public override LiveObject Find(string id)
		{
			return Sprites.Find(s => s.Id == id);
		}

		public override void FitScene(int windowWidth, int windowHeight, ref Point offset, ref float zoom)
		{
			offset = new Point(0, 0);
			zoom = 1;
		}

		public override Matrix GetSceneTransform(int width, int height, Point offset, float zoom)
		{
			Matrix transform = new Matrix();
			float screenScale = height * zoom / BaseHeight;
			transform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display * zoom
			transform.Translate(width * 0.5f + offset.X, offset.Y, MatrixOrder.Append); // center horizontally
			return transform;
		}

		/// <summary>
		/// Converts a Pose definition into a LivePose
		/// </summary>
		/// <param name="pose"></param>
		private void ConvertPose(Pose pose)
		{
			Pose = pose;
			Sprites = new ObservableCollection<LiveSprite>();

			//1. Pose-level data
			Id = pose.Id;
			int height;
			if (int.TryParse(pose.BaseHeight, out height))
			{
				BaseHeight = height;
			}
			else
			{
				BaseHeight = 1440;
			}

			//2. convert all the Sprites into LiveSprites with their properties as Keyframe 0.
			Sprites.CollectionChanged += Sprites_CollectionChanged;

			Dictionary<string, LiveSprite> sprites = new Dictionary<string, LiveSprite>();
			foreach (Sprite sprite in pose.Sprites)
			{
				LiveSprite preview = new LiveSprite(this, sprite, _time);
				if (sprite.Src.Contains("#-"))
				{
					CrossStage = true;
				}
				preview.Stage = CurrentStage;
				preview.PropertyChanged += Sprite_PropertyChanged;
				Sprites.Add(preview);
				if (!string.IsNullOrEmpty(sprite.Id))
				{
					sprites[sprite.Id] = preview;
				}
			}

			//3. Add directives into their corresponding LiveSprite, making a placeholder LiveSprite if there is no match (which would mean a bad ID, but anyway...)
			foreach (PoseDirective directive in pose.Directives)
			{
				LiveSprite preview;
				if (string.IsNullOrEmpty(directive.Id) || !sprites.TryGetValue(directive.Id, out preview))
				{
					//create a sprite preview if the ID doesn't match anything
					preview = new LiveSprite(this, new Sprite(), _time);
					preview.Id = directive.Id;
					preview.PropertyChanged += Sprite_PropertyChanged;
					Sprites.Add(preview);
					if (!string.IsNullOrEmpty(preview.Id))
					{
						sprites[preview.Id] = preview;
					}
				}
				preview.AddKeyframeDirective(directive, 0, "linear", "none", false);
			}
		}

		public override string ToString()
		{
			return Id;
		}

		public string GetLabel()
		{
			return $"Pose Properties";
		}

		/// <summary>
		/// Adds a sprite to the end of the collection
		/// </summary>
		/// <param name="sprite"></param>
		/// <returns></returns>
		public LiveSprite AddSprite(LiveSprite sprite, int index)
		{
			sprite.Data = this;
			sprite.PropertyChanged += Sprite_PropertyChanged;
			if (index == -1)
			{
				Sprites.Add(sprite);
			}
			else
			{
				Sprites.Insert(index, sprite);
			}
			return sprite;
		}

		/// <summary>
		/// Adds a new sprite to the end of the collection
		/// </summary>
		public LiveSprite AddSprite(float time)
		{
			LiveSprite sprite = new LiveSprite(this, time);
			sprite.PropertyChanged += Sprite_PropertyChanged;
			Sprites.Add(sprite);
			return sprite;
		}

		private void Sprite_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Z")
			{
				ReorderSprites();
			}
			else if (e.PropertyName == "Id")
			{
				string newId = (sender as LiveObject)?.Id;
				if (!string.IsNullOrEmpty(newId))
				{
					//relink children
					foreach (LiveSprite track in Sprites)
					{
						if (track.Parent == sender)
						{
							track.ParentId = newId;
						}
					}
				}
			}
		}

		private void Sprites_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ReorderSprites();
		}

		private void ReorderSprites()
		{
			DrawingOrder = LiveObject.SortLayers(Sprites);
			//Dictionary<LiveSprite, int> order = new Dictionary<LiveSprite, int>();
			//DrawingOrder.Clear();
			//for (int i = 0; i < Sprites.Count; i++)
			//{
			//	order[Sprites[i]] = i;
			//}
			//DrawingOrder.AddRange(Sprites);
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

		public override event EventHandler<WidgetCreationArgs> WidgetMoved;
		public override event EventHandler<WidgetCreationArgs> WidgetCreated;
		public override event EventHandler<WidgetCreationArgs> WidgetRemoved;

		/// <summary>
		/// Gets the topmost object beneath the given screen coordinate
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public override LiveObject GetObjectAtPoint(int x, int y, Matrix sceneTransform, bool ignoreMarkers, List<string> markers)
		{
			//search in reverse order because objects are sorted by depth
			for (int i = DrawingOrder.Count - 1; i >= 0; i--)
			{
				LiveObject obj = DrawingOrder[i];
				if (!obj.IsVisible || obj.Hidden || obj.Alpha == 0 || obj.HiddenByMarker(ignoreMarkers ? null : markers)) { continue; }

				//transform point to local space
				PointF localPt = obj.ToLocalPt(x, y, sceneTransform);
				if (localPt.X >= 0 && localPt.X <= obj.Width &&
					localPt.Y >= 0 && localPt.Y <= obj.Height)
				{
					return obj;
				}
			}
			return null;
		}

		public override List<ITimelineWidget> CreateWidgets(Timeline timeline)
		{
			List<ITimelineWidget> list = new List<ITimelineWidget>();
			for (int i = 0; i < Sprites.Count; i++)
			{
				SpriteWidget widget = new SpriteWidget(Sprites[i], timeline);
				list.Add(widget);
			}
			return list;
		}

		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object context)
		{
			LiveSprite sprite = AddSprite(time);
			SpriteWidget widget = new SpriteWidget(sprite, timeline);
			if (context != null)
			{
				string src = context.ToString();

				if (AllowsCrossStageImages)
				{
					string filename = Path.GetFileName(src);
					int stage;
					string outId;
					PoseMap.ParseImage(filename, out stage, out outId);
					if (stage >= 0)
					{
						src = src.Replace($"{stage}-", "#-");
					}
				}

				sprite.AddValue<string>(0, "Src", src);

				string id = Path.GetFileNameWithoutExtension(src);
				int hyphen = id.IndexOf('-');
				if (hyphen == 1 || hyphen == 2)
				{
					id = id.Substring(hyphen + 1);
				}
				sprite.Id = GetUniqueId(id);
			}
			return widget;
		}

		private string GetUniqueId(string id)
		{
			int suffix = 0;
			string prefix = id;
			while (Sprites.Find(s => s.Id == id) != null)
			{
				suffix++;
				id = prefix + suffix;
			}

			return id;
		}

		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index)
		{
			LiveSprite sprite = data as LiveSprite;
			sprite.Id = GetCopyId(sprite.Id);
			AddSprite(sprite, index);
			SpriteWidget widget = new SpriteWidget(sprite, timeline);
			return widget;
		}

		private string GetCopyId(string id)
		{
			HashSet<string> ids = new HashSet<string>();
			foreach (LiveSprite sprite in Sprites)
			{
				ids.Add(sprite.Id);
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

		public override void MoveWidget(ITimelineObject widget, int track)
		{
			if (widget is SpriteWidget)
			{
				SpriteWidget sprite = widget as SpriteWidget;
				LiveSprite data = sprite.GetData() as LiveSprite;
				int index = Sprites.IndexOf(data);
				Sprites.RemoveAt(index);
				if (track >= Sprites.Count || track == -1)
				{
					Sprites.Add(data);
				}
				else
				{
					Sprites.Insert(track, data);
				}
			}
			WidgetMoved?.Invoke(this, new WidgetCreationArgs(widget, track));
		}

		public override void InsertWidget(ITimelineObject widget, float time, int index)
		{
			if (widget is SpriteWidget)
			{
				SpriteWidget sprite = widget as SpriteWidget;
				LiveSprite data = sprite.GetData() as LiveSprite;
				data.PropertyChanged -= Sprite_PropertyChanged;
				if (index == -1)
				{
					//new widget
					Sprites.Add(data);
					index = Sprites.Count - 1;
				}
				else
				{
					Sprites.Insert(index, data);
				}
			}
			WidgetCreated?.Invoke(this, new WidgetCreationArgs(widget, index));
		}

		public override int RemoveWidget(ITimelineObject widget)
		{
			if (widget is SpriteWidget)
			{
				SpriteWidget sprite = widget as SpriteWidget;
				LiveSprite data = sprite.GetData() as LiveSprite;
				int index = Sprites.IndexOf(data);
				if (index >= 0)
				{
					Sprites.RemoveAt(index);
					data.PropertyChanged -= Sprite_PropertyChanged;
					WidgetRemoved?.Invoke(this, new WidgetCreationArgs(widget, index));
				}
				return index;
			}
			return -1;
		}

		public override void UpdateSelection(WidgetSelectionArgs args)
		{
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			args.AllowCut = false;
			args.AllowCopy = false;
			args.AllowDelete = false;
			args.AllowDuplicate = false;
			args.AllowPaste = false;
			if (clipboardData is LiveSprite)
			{
				args.AllowPaste = true;
			}
		}

		#region Drawing
		public override void UpdateTime(float time, float elapsedTime, bool inPlayback)
		{
			_time = time;
			foreach (LiveSprite sprite in Sprites)
			{
				sprite.Update(time, elapsedTime, inPlayback);
			}
		}

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			return false;
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, LiveObject selectedObject, LiveObject selectedPreview, bool inPlayback)
		{
			foreach (LiveSprite sprite in DrawingOrder)
			{
				sprite.Draw(g, sceneTransform, markers, inPlayback);
				if (selectedObject == sprite && !selectedObject.Hidden && selectedPreview != null)
				{
					selectedPreview.Draw(g, sceneTransform, markers, inPlayback);
				}
			}
		}
		#endregion

		public override bool Paste(WidgetOperationArgs args, LiveObject after)
		{
			int index = -1;
			if (after != null)
			{
				index = Sprites.IndexOf(after as LiveSprite) + 1;
			}
			LiveSprite clipboardData = Clipboards.Get<KeyframedWidget, LiveSprite>();
			if (clipboardData != null)
			{
				args.Timeline.CreateWidget(clipboardData.Copy(), index);
				return true;
			}
			return false;
		}

		public override bool OnPaste(WidgetOperationArgs args)
		{
			return Paste(args, null);
		}

		public override List<LiveObject> GetAvailableParents(LiveObject child)
		{
			List<LiveObject> list = new List<LiveObject>();
			foreach (LiveSprite sprite in Sprites)
			{
				if (string.IsNullOrEmpty(sprite.Id) || sprite == child)
				{
					continue;
				}
				//if this is an ancestor of the sprite, disallow it to avoid infinite chains
				LiveObject parent = sprite.Parent;
				bool isAncestor = false;
				while (parent != null)
				{
					if (parent == child)
					{
						isAncestor = true;
						break;
					}
					parent = parent.Parent as LiveSprite;
				}
				if (!isAncestor)
				{
					list.Add(sprite);
				}
			}
			list.Sort();
			return list;
		}

		public override List<ITimelineBreak> CreateBreaks(Timeline timeline)
		{
			List<ITimelineBreak> list = new List<ITimelineBreak>();
			return list;
		}
		public override ITimelineBreak AddBreak(float time) { throw new NotImplementedException(); }

		/// <summary>
		/// Gets the time of the latest keyframe in this scene
		/// </summary>
		/// <returns></returns>
		public override float GetDuration()
		{
			return Sprites.Select(t =>
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

		private void UpdateSpriteStages()
		{
			if (Sprites != null)
			{
				foreach (LiveSprite obj in Sprites)
				{
					obj.Stage = _stage;
				}
			}
		}

		#region debugging
		public void PrintPlainText()
		{
			for (int i = 0; i < Sprites.Count; i++)
			{
				List<List<string>> output = new List<List<string>>();
				List<string> header = new List<string>();
				header.Add("Keyframe".PadRight(15));
				HashSet<string> includedProperties = new HashSet<string>();
				Dictionary<string, List<string>> lines = new Dictionary<string, List<string>>();
				output.Add(header);
				LiveSprite sprite = Sprites[i];
				for (int j = 0; j < sprite.Keyframes.Count; j++)
				{
					LiveKeyframe kf = sprite.Keyframes[j];
					header.Add((sprite.Start + kf.Time).ToString().PadRight(7));
					foreach (string property in kf.TrackedProperties)
					{
						List<string> line;
						if (!lines.TryGetValue(property, out line))
						{
							line = new List<string>();
							lines[property] = line;
							line.Add(property.PadRight(15));
							output.Add(line);
						}
						if (kf.HasProperty(property))
						{
							includedProperties.Add(property);
						}
						LiveKeyframeMetadata metadata;
						if (kf.PropertyMetadata.TryGetValue(property, out metadata))
						{
							line.Add($"{TypeToString(metadata)}{EaseToString(metadata)}{InterpolationToString(metadata)}{LoopToString(metadata)}".PadRight(7));
						}
						else
						{
							line.Add((kf.HasProperty(property) ? "-" : "").PadRight(7));
						}
					}
				}
				foreach (List<string> line in output)
				{
					if (line[0].StartsWith("Keyframe") || includedProperties.Contains(line[0].Substring(0, line[0].IndexOf(' '))))
					{
						Console.WriteLine(string.Join("\t", line));
					}
				}
			}
		}

		private string TypeToString(LiveKeyframeMetadata metadata)
		{
			switch (metadata.FrameType)
			{
				case KeyframeType.Begin:
					return ">";
				case KeyframeType.Split:
					return "|";
				default:
					return "O";
			}
		}

		private string EaseToString(LiveKeyframeMetadata metadata)
		{
			string ease = metadata.Ease;
			if (string.IsNullOrEmpty(ease))
			{
				return "-";
			}
			if (ease.StartsWith("smooth"))
			{
				return "S";
			}
			else if (ease.StartsWith("ease-in"))
			{
				return "I";
			}
			else if (ease.StartsWith("ease-out"))
			{
				return "O";
			}
			else if (ease.StartsWith("bounce"))
			{
				return "B";
			}
			else if (ease.StartsWith("elastic"))
			{
				return "L";
			}
			return "-";
		}

		private string InterpolationToString(LiveKeyframeMetadata metadata)
		{
			string tween = metadata.Interpolation;
			if (string.IsNullOrEmpty(tween)) { return " "; }

			if (tween.StartsWith("linear"))
			{
				return "-";
			}
			else if (tween.StartsWith("spline"))
			{
				return "S";
			}
			else if (tween.StartsWith("none"))
			{
				return "N";
			}
			return "-";
		}

		private string LoopToString(LiveKeyframeMetadata metadata)
		{
			bool loop = metadata.Looped;
			int count = metadata.Iterations;
			if (!loop) { return "-"; }
			return count.ToString();
		}
		#endregion

		public override bool AllowsCrossStageImages
		{
			get
			{
				return CrossStage;
			}
		}
	}
}
