using System;
using System.Collections.Generic;
using System.Drawing;
using Desktop;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Actions.TimelineActions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class SpriteWidget : ITimelineWidget
	{
		private bool _selected;
		private static Brush _titleFill;
		private static Brush _headerKeyframeFill;
		private static Brush _keyframeFill;
		private static Brush _keyframeFillSelected;
		private static Pen _penKeyframe;
		private static Brush _subrowFill;
		private static Brush _selectedSubrowFill;
		private static Brush _repeatFill;
		private static Dictionary<string, Image> _easeIcons;
		private static Dictionary<string, Image> _tweenIcons;

		private const int KeyframeRadius = 5;
		private const int ThumbnailHeight = 32;

		public LiveSprite Sprite { get; private set; }

		private bool _collapsed = false;
		private Image _thumbnail;

		private Timeline _timeline;
		public event EventHandler Invalidated;
		private LiveKeyframe _selectedFrame;
		private HashSet<string> _selectedProperties = new HashSet<string>();
		private LiveKeyframe _hoverFrame;
		private int _hoverRow;
		private bool _playing;

		static SpriteWidget()
		{
			_titleFill = new SolidBrush(Color.FromArgb(153, 197, 255));
			_subrowFill = new SolidBrush(Color.FromArgb(203, 206, 216));
			_selectedSubrowFill = new SolidBrush(Color.FromArgb(223, 226, 236));
			_headerKeyframeFill = new SolidBrush(Color.FromArgb(255, 226, 66));
			_keyframeFill = new SolidBrush(Color.FromArgb(180, 180, 180));
			_keyframeFillSelected = new SolidBrush(Color.FromArgb(245, 245, 255));
			_repeatFill = new SolidBrush(Color.FromArgb(103, 106, 116));

			_penKeyframe = Pens.Black;

			_easeIcons = new Dictionary<string, Image>();
			_easeIcons["linear"] = Properties.Resources.Curve_Linear;
			_easeIcons["smooth"] = Properties.Resources.Curve_Smooth;
			_easeIcons["ease-in"] = Properties.Resources.Curve_EaseIn;
			_easeIcons["ease-out"] = Properties.Resources.Curve_EaseOut;
			_easeIcons["ease-in-cubic"] = Properties.Resources.Curve_EaseInCubic;
			_easeIcons["ease-out-cubic"] = Properties.Resources.Curve_EaseOutCubic;
			_easeIcons["ease-in-sin"] = Properties.Resources.Curve_EaseInSin;
			_easeIcons["ease-out-sin"] = Properties.Resources.Curve_EaseOutSin;
			_easeIcons["ease-in-out-cubic"] = Properties.Resources.Curve_EaseInOutCubic;
			_easeIcons["elastic"] = Properties.Resources.Curve_Elastic;
			_easeIcons["bounce"] = Properties.Resources.Curve_Bounce;

			_tweenIcons = new Dictionary<string, Image>();
			_tweenIcons["linear"] = Properties.Resources.Tween_Linear;
			_tweenIcons["spline"] = Properties.Resources.Tween_Spline;
			_tweenIcons["none"] = Properties.Resources.Tween_None;
		}

		public SpriteWidget(LiveSprite sprite)
		{
			Sprite = sprite;
			Sprite.Widget = this;
			sprite.PropertyChanged += Sprite_PropertyChanged;
			sprite.Keyframes.CollectionChanged += Keyframes_CollectionChanged;
		}

		private void Keyframes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (_selected)
			{
				if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					LiveKeyframe kf = e.NewItems[0] as LiveKeyframe;
					SelectKeyframe(kf, null, false);
					_timeline.SelectData(kf);
				}
			}
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Sprite.Id) ? "Empty Sprite" : Sprite.Id;
		}

		public void AdvanceSubWidget(bool forward)
		{
			if (Sprite.Keyframes.Count > 0)
			{
				int index = 0;
				if (_selectedFrame != null)
				{
					index = Sprite.Keyframes.IndexOf(_selectedFrame);
					if (forward)
					{
						index++;
						if (index >= Sprite.Keyframes.Count)
						{
							index = 0;
						}
					}
					else
					{
						index--;
						if (index < 0)
						{
							index = Sprite.Keyframes.Count - 1;
						}
					}
				}
				LiveKeyframe kf = Sprite.Keyframes[index];
				SelectKeyframe(kf, null, false);
				_timeline.CurrentTime = kf.Time + Sprite.Start;
				SelectFrameDataWithPreview(_timeline.CurrentTime);
			}
		}

		public void OnWidgetSelectionChanged(WidgetSelectionArgs args)
		{
			_timeline = args.Timeline;
			if (args.Modifiers.HasFlag(Keys.Control))
			{
				return;
			}
			_selected = (args.IsSelected != SelectionType.Deselect);
			if (args.IsSelected != SelectionType.Select)
			{
				_hoverFrame = null;
				ClearSelection();
			}
			else
			{
				float time = args.Timeline.CurrentTime;
				LiveKeyframe kf = Sprite.Keyframes.Find(k => k.Time == time);
				if (kf == null)
				{
					args.Timeline.SelectData(Sprite);
					if (!Sprite.IsVisible)
					{
						args.Timeline.CurrentTime = Sprite.Start;
					}
				}
				else
				{
					SelectKeyframe(kf, null, false);
					args.Timeline.SelectData(kf);
				}
			}
		}

		public void OnPlaybackChanged(bool playing)
		{
			_playing = playing;
		}

		public object GetData()
		{
			return Sprite;
		}

		private void Invalidate()
		{
			Invalidated?.Invoke(this, EventArgs.Empty);
		}

		private void Sprite_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Keyframes")
			{
				_thumbnail?.Dispose();
				_thumbnail = null;
			}
			Invalidate();
		}

		public bool LinkedToEnd
		{
			get { return Sprite.LinkedToEnd; }
		}

		public bool IsResizable
		{
			get { return true; }
		}

		public int GetRowCount()
		{
			return _collapsed ? 1 : Sprite.AnimatedProperties.Count + 1;
		}

		public float GetStart()
		{
			return Sprite.Start;
		}
		public void SetStart(float time)
		{
			Sprite.Start = time;
		}

		public float GetLength(float duration)
		{
			if (LinkedToEnd && duration != 0)
			{
				return duration - GetStart();
			}
			else
			{
				return Sprite.Length;
			}
		}
		public void SetLength(float value)
		{
			Sprite.Length = value;
		}

		public Brush GetFillBrush()
		{
			return _selected ? _selectedSubrowFill : _subrowFill;
		}

		public void ClearSelection()
		{
			_selectedFrame = null;
			_selectedProperties.Clear();
			Invalidate();
		}

		public List<string> SelectedProperties
		{
			get
			{
				List<string> properties = new List<string>();
				foreach (string property in _selectedProperties)
				{
					properties.Add(property);
				}
				return properties;
			}
		}

		public LiveKeyframe SelectedFrame
		{
			get { return _selectedFrame; }
		}

		public void SelectKeyframe(LiveKeyframe keyframe, string property, bool addToSelection)
		{
			Invalidate();
			if (_selectedFrame != keyframe || property == null)
			{
				_selectedProperties.Clear();
			}
			else if (_selectedFrame != null && _selectedProperties.Count == 0 && property != null)
			{
				//selecting an individual property after selecting the whole frame - ignore
				return;
			}
			else
			{
				bool alreadySelected = _selectedProperties.Contains(property);
				if (alreadySelected)
				{
					if (addToSelection)
					{
						_selectedProperties.Remove(property);
						return;
					}
				}
				else
				{
					if (!addToSelection)
					{
						_selectedProperties.Clear();
					}
				}
			}
			_selectedFrame = keyframe;
			if (property != null)
			{
				_selectedProperties.Add(property);
			}
		}

		public int GetHeaderIconCount(int row)
		{
			return row == 0 ? 2 : 3;
		}

		public void DrawHeaderIcon(Graphics g, int rowIndex, int iconIndex, int x, int y, int iconWidth, int highlightedIconIndex)
		{
			Image icon = null;
			if (rowIndex == 0)
			{
				switch (iconIndex)
				{
					case 0:
						icon = Sprite.Hidden ? Properties.Resources.EyeClosed : Properties.Resources.EyeOpen;
						break;
					case 1:
						if (Sprite.Parent != null)
						{
							icon = Sprite.Parent.Image;
						}
						else
						{
							icon = Properties.Resources.AddLink;
						}
						break;
				}
			}
			else
			{
				string property = Sprite.Properties[rowIndex - 1];
				AnimatedProperty propData = Sprite.GetAnimationProperties(property);
				switch (iconIndex)
				{
					case 0:
						if (highlightedIconIndex == 0)
						{
							icon = Properties.Resources.Loop;
						}
						else
						{
							icon = (propData.Looped ? Properties.Resources.Loop : null);
						}
						break;
					case 1:
						string tween = propData.Interpolation ?? "none";
						if (!_tweenIcons.TryGetValue(tween, out icon))
						{
							icon = _tweenIcons["none"];
						}
						break;
					case 2:
						string ease = propData.Ease ?? "smooth";
						if (!_easeIcons.TryGetValue(ease, out icon))
						{
							icon = _easeIcons["smooth"];
						}
						break;
				}
			}
			if (icon != null)
			{
				g.DrawImage(icon, x, y, iconWidth, iconWidth);
			}
		}

		public void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int widgetWidth, int rowHeight)
		{
			g.DrawLine(Pens.DarkGray, x + 1, y + rowHeight + 1, x + widgetWidth - 1, y + rowHeight + 1);
			if (rowIndex == 0)
			{
				g.FillRectangle(_titleFill, x, y, widgetWidth, rowHeight + (GetRowCount() == 1 ? 0 : 1));
				//global keyframes
				foreach (LiveKeyframe kf in Sprite.Keyframes)
				{
					DrawKeyframe(g, (kf == _selectedFrame && _selectedProperties.Count == 0) || (kf == _hoverFrame && _hoverRow == 0) ? _keyframeFillSelected : _headerKeyframeFill, TimeToX(Sprite.Start + kf.Time, pps), y + rowHeight / 2 - KeyframeRadius - 1, kf, null);
				}
			}
			else
			{
				string property = Sprite.Properties[rowIndex - 1];

				if (_playing)
				{
					float start, end;
					float time = Sprite.GetInterpolatedTime(property, Sprite.Time, null, null, null, out start, out end);
					int right = TimeToX(end + Sprite.Start, pps);
					int left = TimeToX(start + Sprite.Start, pps);
					int width = (right - left);
					int interpolatedX = left + (int)(width * time);
					g.FillRectangle(Brushes.DarkBlue, interpolatedX - 1, y, 3, rowHeight);
				}

				AnimatedProperty prop = Sprite.GetAnimationProperties(property);
				if (prop.Looped)
				{
					//draw a repeat sign after the last keyframe containing this property
					for (int i = Sprite.Keyframes.Count - 1; i >= 0; i--)
					{
						LiveKeyframe kf = Sprite.Keyframes[i];
						if (kf.HasProperty(property))
						{
							int repeatX = TimeToX(Sprite.Start + kf.Time, pps);
							g.FillEllipse(_repeatFill, repeatX + 6, y + rowHeight / 3 - 2, 4, 4);
							g.FillEllipse(_repeatFill, repeatX + 6, y + 2 * rowHeight / 3 - 2, 4, 4);
							g.FillRectangle(_repeatFill, repeatX + 11, y, 1, rowHeight + 1);
							g.FillRectangle(_repeatFill, repeatX + 13, y, 3, rowHeight + 1);
							break;
						}
					}
				}

				foreach (LiveKeyframe kf in Sprite.Keyframes)
				{
					if (kf.HasProperty(property))
					{
						DrawKeyframe(g, (kf == _selectedFrame && (_selectedProperties.Count == 0 || _selectedProperties.Contains(property))) || (kf == _hoverFrame && (_hoverRow == 0 || _hoverRow == rowIndex)) ? _keyframeFillSelected : _keyframeFill, TimeToX(Sprite.Start + kf.Time, pps), y + rowHeight / 2 - KeyframeRadius - 1, kf, property);
					}
				}
			}
		}

		private void DrawKeyframe(Graphics g, Brush brush, int x, int y, LiveKeyframe kf, string property)
		{
			y += KeyframeRadius + 1;
			Point[] pts;
			if (property != null && kf.InterpolationBreaks.ContainsKey(property))
			{
				pts = new Point[] { new Point(x, y - KeyframeRadius), new Point(x + KeyframeRadius, y), new Point(x, y + KeyframeRadius) };
			}
			else
			{
				pts = new Point[] { new Point(x - KeyframeRadius, y), new Point(x, y - KeyframeRadius), new Point(x + KeyframeRadius, y), new Point(x, y + KeyframeRadius) };
			}
			g.FillPolygon(brush, pts);
			g.DrawPolygon(_penKeyframe, pts);
		}

		private int TimeToX(float time, float pps)
		{
			return (int)(time * pps);
		}

		public string GetLabel(int row)
		{
			if (row == 0)
			{
				return Sprite.Id;
			}
			else
			{
				string property = Sprite.Properties[row - 1];
				PropertyDefinition definition = Definitions.Instance.Get<PropertyDefinition>(property);
				if (definition != null)
				{
					return definition.Name;
				}
				return "Unknown property";
			}
		}

		public Image GetThumbnail()
		{
			if (_thumbnail == null && Sprite.Properties.Contains("Src"))
			{
				string src = Sprite.GetPropertyValue<string>("Src", 0, null);
				if (!string.IsNullOrEmpty(src))
				{
					try
					{
						Image bmp = LiveImageCache.Get(src);

						//create a 32px tall image matching the source's aspect ratio
						int width = (int)((float)bmp.Width / bmp.Height * ThumbnailHeight);
						_thumbnail = new Bitmap(width, ThumbnailHeight);
						using (Graphics g = Graphics.FromImage(_thumbnail))
						{
							g.DrawImage(bmp, 0, 0, _thumbnail.Width, _thumbnail.Height);
						}
					}
					catch { }
				}
			}
			return _thumbnail;
		}

		public string GetHeaderTooltip(WidgetActionArgs args, int iconIndex)
		{
			if (args.Row > 0)
			{
				string property = Sprite.Properties[args.Row - 1];
				AnimatedProperty prop = Sprite.GetAnimationProperties(property);
				switch (iconIndex)
				{
					case 0:
						return "Toggle looping";
					case 1:
						return $"Tweening: {prop.Interpolation ?? "none"}";
					case 2:
						return $"Easing method: {prop.Ease ?? "smooth"}";
				}
			}
			else
			{
				switch (iconIndex)
				{
					case 0:
						return "Toggle visibility";
					case 1:
						return Sprite.Parent == null ? "Unlinked" : $"Linked to: {Sprite.ParentId}";
				}
			}
			return null;
		}

		public void OnClickHeaderIcon(WidgetActionArgs args, int iconIndex)
		{
			if (args.Row == 0)
			{
				switch (iconIndex)
				{
					case 0:
						Sprite.Hidden = !Sprite.Hidden;
						break;
					case 1:
						List<LiveSprite> sprites = new List<LiveSprite>();
						foreach (LiveSprite sprite in Sprite.Pose.Sprites)
						{
							if (string.IsNullOrEmpty(sprite.Id) || sprite == Sprite)
							{
								continue;
							}
							//if this is an ancestor of the sprite, disallow it to avoid infinite chains
							LiveSprite parent = sprite.Parent;
							bool isAncestor = false;
							while (parent != null)
							{
								if (parent == Sprite)
								{
									isAncestor = true;
									break;
								}
								parent = parent.Parent;
							}
							if (!isAncestor)
							{
								sprites.Add(sprite);
							}
						}
						sprites.Sort();
						ContextMenuItem[] items = new ContextMenuItem[sprites.Count];
						for (int i = 0; i < sprites.Count; i++)
						{
							LiveSprite sprite = sprites[i];
							items[i] = new ContextMenuItem(sprite.Id, sprite.Image, SelectParent, sprite.Id, Sprite.Parent == sprite);
						}
						args.Timeline.ShowContextMenu(items);
						break;
				}
			}
			else
			{
				string property = Sprite.Properties[args.Row - 1];
				AnimatedProperty prop = Sprite.GetAnimationProperties(property);
				switch (iconIndex)
				{
					case 0:
						prop.Looped = !prop.Looped;
						break;
					case 1:
						string tween = prop.Interpolation;
						args.Timeline.ShowContextMenu(
							new ContextMenuItem("Linear", Properties.Resources.Tween_Linear, SelectTween, new Tuple<string, string>(property, "linear"), tween == "linear"),
							new ContextMenuItem("Spline", Properties.Resources.Tween_Spline, SelectTween, new Tuple<string, string>(property, "spline"), tween == "spline"),
							new ContextMenuItem("None", Properties.Resources.Tween_None, SelectTween, new Tuple<string, string>(property, "none"), tween == "none" || string.IsNullOrEmpty(tween))
							);
						break;
					case 2:
						string ease = prop.Ease;
						args.Timeline.ShowContextMenu(
							new ContextMenuItem("Linear", Properties.Resources.Curve_Linear, SelectEase, new Tuple<string, string>(property, "linear"), ease == "linear"),
							new ContextMenuItem("Smooth", Properties.Resources.Curve_Smooth, SelectEase, new Tuple<string, string>(property, "smooth"), ease == "smooth" || string.IsNullOrEmpty(ease)),
							new ContextMenuItem("Ease-In-Out Cubic", Properties.Resources.Curve_EaseInOutCubic, SelectEase, new Tuple<string, string>(property, "ease-in-out-cubic"), ease == "ease-in-out-cubic"),
							new ContextMenuItem("Ease-In", Properties.Resources.Curve_EaseIn, SelectEase, new Tuple<string, string>(property, "ease-in"), ease == "ease-in"),
							new ContextMenuItem("Ease-In Sine", Properties.Resources.Curve_EaseInSin, SelectEase, new Tuple<string, string>(property, "ease-in-sin"), ease == "ease-in-sin"),
							new ContextMenuItem("Ease-In Cubic", Properties.Resources.Curve_EaseInCubic, SelectEase, new Tuple<string, string>(property, "ease-in-cubic"), ease == "ease-in-cubic"),
							new ContextMenuItem("Ease-Out", Properties.Resources.Curve_EaseOut, SelectEase, new Tuple<string, string>(property, "ease-out"), ease == "ease-out"),
							new ContextMenuItem("Ease-Out Sine", Properties.Resources.Curve_EaseOutSin, SelectEase, new Tuple<string, string>(property, "ease-out-sin"), ease == "ease-out-sin"),
							new ContextMenuItem("Ease-Out Cubic", Properties.Resources.Curve_EaseOutCubic, SelectEase, new Tuple<string, string>(property, "ease-out-cubic"), ease == "ease-out-cubic"),
							new ContextMenuItem("Bounce", Properties.Resources.Curve_Bounce, SelectEase, new Tuple<string, string>(property, "bounce"), ease == "bounce"),
							new ContextMenuItem("Elastic", Properties.Resources.Curve_Elastic, SelectEase, new Tuple<string, string>(property, "elastic"), ease == "elastic")
							);
						break;
				}
			}
		}

		private void SelectTween(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Tuple<string, string> tag = item.Tag as Tuple<string, string>;
			string property = tag.Item1;
			string tween = tag.Item2;
			AnimatedProperty prop = Sprite.GetAnimationProperties(property);
			prop.Interpolation = tween;
		}

		private void SelectEase(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Tuple<string, string> tag = item.Tag as Tuple<string, string>;
			string property = tag.Item1;
			string ease = tag.Item2;
			AnimatedProperty prop = Sprite.GetAnimationProperties(property);
			prop.Ease = ease;
		}

		private void SelectParent(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			string id = item.Tag?.ToString();
			Sprite.ParentId = id;
		}

		public void OnClickHeader(WidgetActionArgs args)
		{
			int row = args.Row;
			if (row == 0)
			{
				ClearSelection();
				args.Timeline.SelectData(Sprite);
			}
			else
			{
				bool add = args.Modifiers.HasFlag(Keys.Control);
				if (!add)
				{
					ClearSelection();
				}
				_selectedFrame = null;
				string property = Sprite.Properties[row - 1];
				_selectedProperties.Add(property);
				args.Timeline.SelectData(Sprite.GetAnimationProperties(property));
				Invalidate();
			}
		}

		public ITimelineAction GetAction(int x, int width, float start, int row, int timelineWidth, float pps)
		{
			_hoverFrame = null;

			//see if a keyframe is selected
			for (int i = 0; i < Sprite.Keyframes.Count; i++)
			{
				LiveKeyframe kf = Sprite.Keyframes[i];
				int kfX = TimeToX(kf.Time, pps);
				if (Math.Abs(x - kfX) <= 5)
				{
					_hoverFrame = kf;
					_hoverRow = row;
					string property = row > 0 ? Sprite.Properties[row - 1] : null;
					if (string.IsNullOrEmpty(property) || kf.HasProperty(property))
					{
						return new SelectKeyframeTimelineAction(this, kf, property);
					}
				}
			}

			if (Math.Abs(width - x) <= 5 && !Sprite.LinkedToEnd && Sprite.Keyframes.Count <= 1)
			{
				return new WidgetEndTimelineAction();
			}
			if (x >= 5 && x <= width - 5)
			{
				return new MoveWidgetTimelineAction();
			}
			return null;
		}

		public void OnStartMove(WidgetActionArgs args)
		{
			ClearSelection();
			args.Timeline.SelectData(GetData());
		}

		public void OnMouseOut()
		{
			_hoverFrame = null;
			Invalidate();
		}

		public void OnTimeChanged(WidgetOperationArgs args)
		{
			float time = args.Time;

			if (_selected)
			{
				if (time < Sprite.Start)
				{
					args.Timeline.SelectData(Sprite);
				}
				else
				{
					SelectFrameDataWithPreview(time);
				}
			}
		}

		private void SelectFrameDataWithPreview(float time)
		{
			LiveKeyframe previewFrame = Sprite.GetInterpolatedFrame(time - Sprite.Start);

			//show whatever keyframe is under the current time, or an interpolated placeholder if there is none
			LiveKeyframe frame = Sprite.Keyframes.Find(kf => kf.Time == time);
			if (frame == null)
			{
				frame = new LiveKeyframe(time - Sprite.Start);
				frame.Sprite = Sprite;
				frame.PropertyChanged += NewFrame_PropertyChanged;
			}
			_timeline.SelectData(frame, previewFrame);
		}

		private void NewFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//the frame has a real property now, so add it to the sprite
			LiveKeyframe frame = sender as LiveKeyframe;
			frame.PropertyChanged -= NewFrame_PropertyChanged;
			Sprite.AddKeyframe(frame);
			_timeline.SelectData(frame);

		}

		public bool IsCollapsible
		{
			get { return Sprite.AnimatedProperties.Count > 0; }

		}

		public bool IsCollapsed
		{
			get { return _collapsed; }
			set { _collapsed = value; }
		}

		public bool IsRowHighlighted(int row)
		{
			string property = row > 0 ? Sprite.Properties[row - 1] : null;
			return row == 0 ? _selectedProperties.Count == 0 : _selectedProperties.Contains(property);
		}

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			if (_selectedFrame != null)
			{
				//keyframe selected
				if (!Sprite.Keyframes.Contains(_selectedFrame))
				{
					ClearSelection();
				}
				else if (_selectedProperties.Count > 0)
				{
					List<string> rows = new List<string>();
					foreach (string property in _selectedProperties)
					{
						rows.Add(property);
					}
					foreach (string row in rows)
					{
						if (!_selectedFrame.HasProperty(row))
						{
							_selectedProperties.Remove(row);
						}
					}
					if (_selectedProperties.Count == 0)
					{
						_selectedFrame = null;
					}
				}
				if (_selectedFrame != null)
				{
					args.AllowCut = true;
					args.AllowCopy = true;
					args.AllowDelete = _selectedFrame.Time > 0 || _selectedProperties.Count > 0;
					args.AllowDuplicate = true;
				}
			}
			if (_selectedFrame == null)
			{
				if (_selectedProperties.Count == 0)
				{
					//widget selected
					args.AllowCut = true;
					args.AllowCopy = true;
					args.AllowDelete = true;
					args.AllowDuplicate = true;
				}
				else
				{
					//one or more rows are selected
					args.AllowCut = true;
					args.AllowCopy = true;
					args.AllowDelete = true;
					args.AllowDuplicate = false;
				}
			}

			args.AllowPaste = false;
			object clipboardData = Clipboards.Get<SpriteWidget, object>();
			if (clipboardData is AnimatedPropertyClipboardData)
			{
				args.AllowPaste = true;
			}
			else if (clipboardData is LiveSprite)
			{
				args.AllowPaste = true;
			}
			else if (clipboardData is LiveKeyframe)
			{
				args.AllowPaste = args.Timeline.CurrentTime >= Sprite.Start;
			}
		}

		public bool OnCopy(WidgetOperationArgs args)
		{
			if (_selectedFrame != null)
			{
				//copy a frame
				LiveKeyframe kf = Sprite.CopyKeyframe(_selectedFrame, _selectedProperties);
				Clipboards.Set<SpriteWidget>(kf);
				return true;
			}
			else if (_selectedProperties.Count > 0)
			{
				//copy a property across the animation
				List<string> properties = new List<string>();
				properties.AddRange(_selectedProperties);
				AnimatedPropertyClipboardData data = new AnimatedPropertyClipboardData(Sprite, properties);
				Clipboards.Set<SpriteWidget>(data);
				return true;
			}
			else
			{
				//copy the whole sprite
				LiveSprite sprite = Sprite.Copy();
				Clipboards.Set<SpriteWidget>(sprite);
				return true;
			}
		}

		public bool OnDelete(WidgetOperationArgs args)
		{
			if (_selectedFrame != null)
			{
				if (_selectedProperties.Count == 0)
				{
					DeleteKeyframeCommand command = new DeleteKeyframeCommand(Sprite, _selectedFrame);
					args.History.Commit(command);
				}
				else
				{
					args.History.StartBulkRecord();
					foreach (string property in _selectedProperties)
					{
						DeletePropertyCommand command = new DeletePropertyCommand(Sprite, _selectedFrame, property);
						command.Do();
						args.History.Record(command);
					}
					args.History.EndBulkRecord();
				}
			}
			else if (_selectedProperties.Count > 0)
			{
				//delete properties
				args.History.StartBulkRecord();
				foreach (string property in _selectedProperties)
				{
					DeleteAnimatedPropertyCommand command = new DeleteAnimatedPropertyCommand(Sprite, property);
					command.Do();
					args.History.Record(command);
				}
				args.History.EndBulkRecord();
			}
			else
			{
				//delete whole widget
				if (args.IsSilent || MessageBox.Show($"Are you sure you want to completely remove {ToString()}?", "Remove Sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					DeleteWidgetCommand command = new DeleteWidgetCommand(Sprite.Pose, this);
					args.History.Commit(command);
					args.Timeline.SelectData(null);
				}
			}
			return true;
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			object clipboardData = Clipboards.Get<SpriteWidget, object>();
			if (clipboardData is AnimatedPropertyClipboardData)
			{
				AnimatedPropertyClipboardData data = clipboardData as AnimatedPropertyClipboardData;
				if (data != null)
				{
					data.Apply(Sprite);
					args.History.Record(data);
				}
			}
			else if (clipboardData is LiveKeyframe)
			{
				//Pastes a copied frame into the current position, overwriting any properties in frame already there
				LiveKeyframe copiedFrame = clipboardData as LiveKeyframe;
				if (copiedFrame != null)
				{
					float time = args.Time;

					//if a keyframe is nearby, paste into it
					int x = args.Timeline.TimeToX(time);
					float minTime = args.Timeline.XToTime(x - KeyframeRadius);
					float maxTime = args.Timeline.XToTime(x + KeyframeRadius);
					LiveKeyframe frame = Sprite.Keyframes.Find(k => minTime <= k.Time && maxTime >= k.Time);
					if (frame != null)
					{
						time = frame.Time;
					}

					PasteKeyframeCommand command = new PasteKeyframeCommand(Sprite, copiedFrame, time, false);
					args.History.Commit(command);
					_selectedFrame = command.NewKeyframe;
					_selectedProperties.Clear();
					args.Timeline.SelectData(_selectedFrame);
				}
			}
			else if (clipboardData is LiveSprite)
			{
				return Sprite.Pose.Paste(args, Sprite.Pose.Sprites.IndexOf(Sprite) + 1);
			}
			return true;
		}

		public bool OnDuplicate(WidgetOperationArgs args)
		{
			if (_selectedFrame == null)
			{
				//duplicating whole sprite
				LiveSprite sprite = Sprite.Copy();
				object oldClipboard = Clipboards.Get<SpriteWidget, object>();
				Clipboards.Set<SpriteWidget>(sprite);
				Sprite.Pose.Paste(args, Sprite.Pose.Sprites.IndexOf(Sprite) + 1);
				Clipboards.Set<SpriteWidget>(oldClipboard);
				return true;
			}
			else
			{
				//duplicating keyframe
				float time = args.Time;

				//if a keyframe is nearby, paste into it
				int x = args.Timeline.TimeToX(time);
				float minTime = args.Timeline.XToTime(x - KeyframeRadius);
				float maxTime = args.Timeline.XToTime(x + KeyframeRadius);
				LiveKeyframe frame = Sprite.Keyframes.Find(k => minTime <= k.Time && maxTime >= k.Time);
				if (frame != null)
				{
					time = frame.Time;
				}
				if (Math.Abs(_selectedFrame.Time + Sprite.Start - time) < 0.001f)
				{
					return false; //can't duplicate into itself
				}

				PasteKeyframeCommand command = new PasteKeyframeCommand(Sprite, _selectedFrame, time, false);
				args.History.Commit(command);
				_selectedFrame = command.NewKeyframe;
				_selectedProperties.Clear();
				args.Timeline.SelectData(_selectedFrame);
				return true;
			}
		}

		public void OnOpeningContextMenu(ContextMenuArgs args)
		{
			if (_selectedFrame != null)
			{
				bool split = false;
				if (_selectedProperties.Count == 0)
				{
					foreach (string property in LiveKeyframe.TrackedProperties)
					{
						if (_selectedFrame.HasProperty(property))
						{
							if (_selectedFrame.InterpolationBreaks.ContainsKey(property))
							{
								split = true;
								break;
							}
						}
					}
				}
				else
				{
					foreach (string property in _selectedProperties)
					{
						if (_selectedFrame.InterpolationBreaks.ContainsKey(property))
						{
							split = true;
							break;
						}
					}
				}
				args.ItemsToAdd.Add(new ContextMenuItem("Split animation", Properties.Resources.SplitKeyframe, ToggleSplit, null, split));
			}
		}

		private void ToggleSplit(object sender, EventArgs e)
		{
			if (_selectedFrame == null)
			{
				return;
			}
			ToggleAnimationBreakCommand command = new ToggleAnimationBreakCommand(_selectedFrame, _selectedProperties);
			_timeline.CommandHistory.Commit(command);
		}
	}
}
