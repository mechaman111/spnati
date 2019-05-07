using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LivePose : BindableObject, ITimelineData, ILabel
	{
		public ISkin Character;
		public Pose Pose;

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
				Set(value);
				LabelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		[Numeric(DisplayName = "Base Height", Key = "baseHeight", GroupOrder = 10, Minimum = 0, Maximum = 50000)]
		public int BaseHeight
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		private float _time;

		public LivePose(ISkin character, Pose pose)
		{
			Character = character;

			ConvertPose(pose);
		}

		/// <summary>
		/// Converts a Pose definition into a LivePose
		/// </summary>
		/// <param name="pose"></param>
		private void ConvertPose(Pose pose)
		{
			Pose = pose;

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
			Sprites = new ObservableCollection<LiveSprite>();
			Sprites.CollectionChanged += Sprites_CollectionChanged;

			Dictionary<string, LiveSprite> sprites = new Dictionary<string, LiveSprite>();
			foreach (Sprite sprite in pose.Sprites)
			{
				LiveSprite preview = new LiveSprite(this, sprite, _time);
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
				preview.AddDirective(directive);
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
			sprite.Pose = this;
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
		}

		private void Sprites_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ReorderSprites();
		}

		private void ReorderSprites()
		{
			Dictionary<LiveSprite, int> order = new Dictionary<LiveSprite, int>();
			DrawingOrder.Clear();
			for (int i = 0; i < Sprites.Count; i++)
			{
				order[Sprites[i]] = i;
			}
			DrawingOrder.AddRange(Sprites);
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

		public event EventHandler<WidgetCreationArgs> WidgetMoved;
		public event EventHandler<WidgetCreationArgs> WidgetCreated;
		public event EventHandler<WidgetCreationArgs> WidgetRemoved;

		public List<ITimelineWidget> CreateWidgets(Timeline timeline)
		{
			List<ITimelineWidget> list = new List<ITimelineWidget>();
			for (int i = 0; i < Sprites.Count; i++)
			{
				SpriteWidget widget = new SpriteWidget(Sprites[i], timeline);
				list.Add(widget);
			}
			return list;
		}

		public ITimelineWidget CreateWidget(Timeline timeline, float time, object context)
		{
			LiveSprite sprite = AddSprite(time);
			SpriteWidget widget = new SpriteWidget(sprite, timeline);
			if (context != null)
			{
				string src = context.ToString();
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

		public ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index)
		{
			LiveSprite sprite = data as LiveSprite;
			sprite.Id += "(copy)";
			AddSprite(sprite, index);
			SpriteWidget widget = new SpriteWidget(sprite, timeline);
			return widget;
		}

		public void MoveWidget(ITimelineWidget widget, int track)
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

		public void InsertWidget(ITimelineWidget widget, float time, int index)
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

		public int RemoveWidget(ITimelineWidget widget)
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

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			object clipboardData = Clipboards.Get<SpriteWidget, object>();
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
		public void UpdateTime(float time, bool inPlayback)
		{
			_time = time;
			foreach (LiveSprite sprite in Sprites)
			{
				sprite.Update(time, inPlayback);
			}
		}

		public bool Paste(WidgetOperationArgs args, int index)
		{
			LiveSprite clipboardData = Clipboards.Get<SpriteWidget, LiveSprite>();
			if (clipboardData != null)
			{
				args.Timeline.CreateWidget(clipboardData.Copy(), index);
				return true;
			}
			return false;
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			return Paste(args, -1);
		}
		#endregion;
	}
}
