using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Editable scene
	/// </summary>
	public class LiveScene : LiveData, ILabel
	{
		public event EventHandler LabelChanged;

		public override event EventHandler<WidgetCreationArgs> WidgetMoved
		{
			add { }
			remove { }
		}
		public override event EventHandler<WidgetCreationArgs> WidgetCreated
		{
			add { }
			remove { }
		}
		public override event EventHandler<WidgetCreationArgs> WidgetRemoved
		{
			add { }
			remove { }
		}

		public Character Character { get; set; }

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
				_background = LiveImageCache.Get(value);
				if (_background != null)
				{
					Width = _background.Width;
					Height = _background.Height;

					foreach (LiveSceneSegment segment in Segments)
					{
						segment.Camera.Width = _background.Width;
						segment.Camera.Height = _background.Height;
					}
				}
				Set(value);
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

		[EnumControl(DisplayName = "When Ending Scene", GroupOrder = 1, ValueType = typeof(PauseType))]
		public PauseType WaitMethod
		{
			get { return Get<PauseType>(); }
			set { Set(value); }
		}

		public override int BaseHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public List<LiveSceneSegment> Segments = new List<LiveSceneSegment>();

		public float AspectRatio { get { return Width / (float)Height; } }

		public Dictionary<string, string> IdRemap = new Dictionary<string, string>();

		public override string ToString()
		{
			return Name;
		}

		public LiveScene(Character character)
		{
			Width = 100;
			Height = 100;
		}

		/// <summary>
		/// Constructs a scene from a list of directives
		/// </summary>
		/// <param name="scene"></param>
		public LiveScene(Scene scene, Character character)
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
				BackgroundImage = LiveSceneSegment.FixPath(scene.Background, character);
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

			HashSet<LiveObject> currentBatch = new HashSet<LiveObject>();
			LiveSceneSegment segment = new LiveSceneSegment(this, character);
			Segments.Add(segment);
			//for the first segment, use the scene setting for the camera's initial values
			segment.Camera.PopulateSceneAttributes(scene);
			bool newlyAdded = false;

			foreach (Directive directive in scene.Directives)
			{
				directive.CacheProperties();
				newlyAdded = false;
				switch (directive.DirectiveType)
				{
					case "metadata":
						segment.AddMetadataDirective(directive, currentBatch);
						break;
					case "sprite":
						segment.AddSpriteDirective(directive, currentBatch);
						break;
					case "text":
						segment.AddBubbleDirective(directive, currentBatch);
						break;
					case "move":
					case "camera":
					case "fade":
						segment.AddMoveDirective(directive, currentBatch);
						break;
					case "stop":
						segment.AddStopDirective(directive, currentBatch);
						break;
					case "wait":
					case "pause":
						segment.AddPauseDirective(directive, currentBatch);
						if (directive.DirectiveType == "pause")
						{
							//Pauses lead to a new segment
							segment = AddSegment(segment, currentBatch, -1);
							newlyAdded = true;
						}
						break;
					case "remove":
						segment.AddRemoveDirective(directive, currentBatch);
						break;
					case "clear":
						segment.AddClearDirective(directive, currentBatch);
						break;
					case "clear-all":
						segment.AddClearAllDirective(directive, currentBatch);
						break;
					case "emitter":
						segment.AddEmitterDirective(directive, currentBatch);
						break;
					case "emit":
						segment.AddEmitDirective(directive, currentBatch);
						break;
					case "skip":
						break;
				}
			}

			//Determine the wait method
			if (scene.Directives.Count > 0)
			{
				Directive lastDirective = scene.Directives[scene.Directives.Count - 1];
				if (lastDirective.DirectiveType == "pause")
				{
					WaitMethod = PauseType.WaitForInput;
				}
				else if (lastDirective.DirectiveType == "wait")
				{
					WaitMethod = PauseType.WaitForAnimations;
				}
				else
				{
					WaitMethod = PauseType.AdvanceImmediately;
				}
			}
			else
			{
				WaitMethod = PauseType.WaitForInput;
			}

			if (newlyAdded)
			{
				//there wasn't actually anything to add to the segment, so get rid of it
				Segments.RemoveAt(Segments.Count - 1);
			}
		}

		/// <summary>
		/// Adds a new segment to the scene
		/// </summary>
		/// <param name="currentBatch">Objects that have persisted from the last segment</param>
		public LiveSceneSegment AddSegment(LiveSceneSegment current, HashSet<LiveObject> currentBatch, int index)
		{
			LiveSceneSegment segment = new LiveSceneSegment(this, Character);
			segment.PreviousSegment = current;
			if (index >= 0)
			{
				Segments.Insert(index, segment);
			}
			else
			{
				Segments.Add(segment);
			}
			HashSet<LiveObject> newBatch = new HashSet<LiveObject>();
			foreach (LiveObject obj in current.Tracks)
			{
				if (obj is LiveCamera)
				{
					segment.Camera.Data = segment;
					segment.Camera.Keyframes.Clear();
					segment.Camera.SetPrevious(obj);
					continue;
				}
				if (obj.LinkedToEnd)
				{
					LiveObject copy = obj.Copy();
					LiveAnimatedObject anim = copy as LiveAnimatedObject;
					if (anim != null)
					{
						anim.Keyframes.Clear();
						anim.AnimatedProperties.Clear();
						anim.Properties.Clear();
						anim.Events.Clear();
						anim.Character = Character;
					}
					copy.Data = segment;
					copy.Start = 0;
					copy.SetPrevious(obj);
					anim?.Update(0, 0, false);
					newBatch.Add(copy);
					segment.Tracks.Add(copy);
				}
			}
			foreach (LiveObject obj in newBatch)
			{
				//link up parents
				if (obj.Previous != null)
				{
					obj.ParentId = obj.Previous.ParentId;
				}
			}
			currentBatch.Clear();
			currentBatch.AddRange(newBatch);
			return segment;
		}

		public void Draw(Graphics g, Matrix sceneTransform, bool drawOutline)
		{
			g.FillRectangle(_backColor, 0, 0, g.VisibleClipBounds.Width, g.VisibleClipBounds.Height);

			g.MultiplyTransform(sceneTransform);
			if (_background != null)
			{
				g.DrawImage(_background, 0, 0);
			}
			if (drawOutline)
			{
				g.DrawRectangle(Pens.White, 0, 0, Width, Height);
				g.DrawRectangle(Pens.Black, -1, -1, Width + 2, Height + 2);
			}
			g.ResetTransform();

		}

		public override LiveObject Find(string id)
		{
			return null;
		}

		public override LiveObject GetObjectAtPoint(int x, int y, Matrix sceneTransform, bool ignoreMarkers, List<string> markers)
		{
			return null;
		}

		public override void UpdateTime(float time, float elapsedTime, bool inPlayback)
		{
		}

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			return false;
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, LiveObject selectedObject, LiveObject selectedPreview, bool inPlayback)
		{
			Draw(g, sceneTransform, true);
		}

		public override void FitScene(int windowWidth, int windowHeight, ref Point offset, ref float zoom)
		{
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

		public override Matrix GetSceneTransform(int width, int height, Point offset, float zoom)
		{
			Matrix transform = new Matrix();
			float screenScale = zoom;
			transform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display * zoom
			transform.Translate(offset.X, offset.Y, MatrixOrder.Append); // center horizontally
			return transform;
		}

		public override List<LiveObject> GetAvailableParents(LiveObject child)
		{
			return new List<LiveObject>();
		}

		public override bool Paste(WidgetOperationArgs args, LiveObject after)
		{
			return false;
		}

		public override List<ITimelineWidget> CreateWidgets(Timeline timeline)
		{
			return new List<ITimelineWidget>();
		}

		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object context)
		{
			throw new NotImplementedException();
		}

		public override ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index)
		{
			throw new NotImplementedException();
		}

		public override void InsertWidget(ITimelineObject widget, float time, int index)
		{
			throw new NotImplementedException();
		}

		public override int RemoveWidget(ITimelineObject widget)
		{
			throw new NotImplementedException();
		}

		public override void MoveWidget(ITimelineObject widget, int newTrack)
		{
			throw new NotImplementedException();
		}

		public override bool OnPaste(WidgetOperationArgs args)
		{
			return false;
		}

		public override void UpdateSelection(WidgetSelectionArgs args)
		{
		}

		public override List<ITimelineBreak> CreateBreaks(Timeline timeline)
		{
			return new List<ITimelineBreak>();
		}

		public override ITimelineBreak AddBreak(float time)
		{
			return null;
		}

		public string SceneName
		{
			get { return GetLabel(); }
		}

		public string GetLabel()
		{
			return Name ?? "Unnamed scene";
		}

		public override float GetDuration()
		{
			return 0;
		}
	}

	public enum PauseType
	{
		//Wait for Input
		WaitForInput,
		//Wait for Animations
		WaitForAnimations,
		//Move immediately to the next segment. Only available for the final segment in a scene.
		AdvanceImmediately,
	}
}
