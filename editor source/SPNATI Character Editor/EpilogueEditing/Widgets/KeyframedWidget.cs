using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Actions.TimelineActions;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Base class for any widget that can hold keyframes
	/// </summary>
	public abstract class KeyframedWidget : ITimelineWidget
	{
		protected bool _selected;
		protected bool _collapsed = false;

		protected bool AllowParenting = false;
		protected bool AllowDelete = true;
		protected bool ShowPropertyRows = true;

		private static SolidBrush _repeatFill;
		private static Dictionary<string, Image> _easeIcons;
		private static Dictionary<string, Image> _tweenIcons;
		private static SolidBrush _headerKeyframeFill;
		private static SolidBrush _keyframeFill;
		private static SolidBrush _keyframeFillSelected;
		private static Pen _penKeyframe;

		private const int KeyframeRadius = 5;

		protected Timeline _timeline;

		public event EventHandler Invalidated;
		private LiveKeyframe _selectedFrame;
		private HashSet<string> _selectedProperties = new HashSet<string>();
		private LiveKeyframe _hoverFrame;
		private int _hoverRow;
		private bool _playing;

		private LiveAnimatedObject _data;
		public LiveAnimatedObject Data
		{
			get { return _data; }
			private set
			{
				_data = value;
				OnSetData(value);
			}
		}
		protected virtual void OnSetData(LiveAnimatedObject data)
		{

		}

		static KeyframedWidget()
		{
			_headerKeyframeFill = new SolidBrush(Color.FromArgb(255, 226, 66));
			_keyframeFill = new SolidBrush(Color.FromArgb(180, 180, 180));
			_keyframeFillSelected = new SolidBrush(Color.FromArgb(245, 245, 255));
			_repeatFill = new SolidBrush(Color.FromArgb(103, 106, 116));

			_penKeyframe = new Pen(Color.Black);

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
			_easeIcons["ease-out-in"] = Properties.Resources.Curve_EaseOutIn;
			_easeIcons["ease-out-in-cubic"] = Properties.Resources.Curve_EaseOutInCubic;
			_easeIcons["elastic"] = Properties.Resources.Curve_Elastic;
			_easeIcons["bounce"] = Properties.Resources.Curve_Bounce;

			_tweenIcons = new Dictionary<string, Image>();
			_tweenIcons["linear"] = Properties.Resources.Tween_Linear;
			_tweenIcons["spline"] = Properties.Resources.Tween_Spline;
			_tweenIcons["none"] = Properties.Resources.Tween_None;

			SetSkin(SkinManager.Instance.CurrentSkin);
		}

		public void UpdateSkin(Skin skin)
		{
			SetSkin(skin);
			OnUpdateSkin(skin);
		}
		protected virtual void OnUpdateSkin(Skin skin)
		{
		}

		private static void SetDefaultColors()
		{
			_headerKeyframeFill.Color = Color.FromArgb(255, 226, 66);
			_keyframeFill.Color = Color.FromArgb(180, 180, 180);
			_keyframeFillSelected.Color = Color.FromArgb(245, 245, 255);
			_repeatFill.Color = Color.FromArgb(103, 106, 116);
			_penKeyframe.Color = Color.Black;
		}
		protected virtual void OnSetDefaultColors()
		{

		}

		public static void SetSkin(Skin skin)
		{
			if (!skin.AppColors.ContainsKey("WidgetHeaderRow"))
			{
				SetDefaultColors();
				return;
			}
			_headerKeyframeFill.Color = skin.GetAppColor("KeyframeHeader");
			_keyframeFill.Color = skin.GetAppColor("Keyframe0");
			_penKeyframe.Color = skin.GetAppColor("KeyframeBorder");
			_keyframeFillSelected.Color = skin.GetAppColor("KeyframeSelected");
			_repeatFill.Color = skin.GetAppColor("WidgetRepeat");
		}

		public KeyframedWidget(LiveAnimatedObject data, Timeline timeline)
		{
			_timeline = timeline;
			Data = data;
			data.PropertyChanged += Data_PropertyChanged;
			data.Keyframes.CollectionChanged += Keyframes_CollectionChanged;
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

		public void AdvanceSubWidget(bool forward)
		{
			if (Data.Keyframes.Count > 1)
			{
				int index = 0;
				if (_selectedFrame != null)
				{
					index = Data.Keyframes.IndexOf(_selectedFrame);
					if (forward)
					{
						index++;
						if (index >= Data.Keyframes.Count)
						{
							index = 0;
						}
					}
					else
					{
						index--;
						if (index < 0)
						{
							index = Data.Keyframes.Count - 1;
						}
					}
				}
				LiveKeyframe kf = Data.Keyframes[index];
				SelectKeyframe(kf, null, false);
				_timeline.CurrentTime = kf.Time + Data.Start;
				SelectFrameDataWithPreview(_timeline.CurrentTime);
			}
			else
			{
				if (forward)
				{
					_timeline.CurrentTime = Data.Start + Data.Length;
					SelectFrameDataWithPreview(_timeline.CurrentTime);
				}
				else
				{
					LiveKeyframe kf = Data.Keyframes[0];
					SelectKeyframe(kf, null, false);
					_timeline.CurrentTime = kf.Time + Data.Start;
					SelectFrameDataWithPreview(_timeline.CurrentTime);
				}
			}
		}

		public void OnWidgetSelectionChanged(WidgetSelectionArgs args)
		{
			_timeline = args.Timeline;
			//if (args.Modifiers.HasFlag(Keys.Control))
			//{
			//	return;
			//}
			_selected = (args.IsSelected != SelectionType.Deselect);
			if (args.IsSelected != SelectionType.Select)
			{
				_hoverFrame = null;
				ClearSelection();
			}
			else
			{
				float time = args.Timeline.CurrentTime;
				if (Data == null || Data.Data is LiveScene)
				{
					SelectFrameDataWithPreview(time);
				}
				else
				{
					LiveKeyframe kf = Data.Keyframes.Find(k => k.Time == time);
					if (kf == null)
					{
						args.Timeline.SelectData(Data);
					}
					else
					{
						SelectKeyframe(kf, null, false);
						args.Timeline.SelectData(kf);
					}
				}
			}
		}

		public void OnPlaybackChanged(bool playing)
		{
			_playing = playing;
		}

		public object GetData()
		{
			return Data;
		}

		protected void Invalidate()
		{
			Invalidated?.Invoke(this, EventArgs.Empty);
		}

		private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			OnDataPropertyChanged(sender, e);
			Invalidate();
		}
		protected virtual void OnDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

		}

		public int GetRowCount()
		{
			return !ShowPropertyRows || _collapsed ? 1 : Data.AnimatedProperties.Count + 1;
		}

		public float GetStart()
		{
			return Data.Start;
		}
		public void SetStart(float time)
		{
			Data.Start = time;
		}

		public float GetLength(float duration)
		{
			if (duration != 0)
			{
				return duration - GetStart();
			}
			else
			{
				return Data.Length;
			}
		}

		private Brush GetFillBrush()
		{
			return GetFillBrush(_selected);
		}
		protected abstract Brush GetFillBrush(bool selected);
		protected abstract Brush GetTitleBrush();

		private Pen GetOutline()
		{
			//return _selected ? Timeline.WidgetSelectedOutline : Timeline.WidgetOutline;
			return Timeline.WidgetOutline;
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

		private int GetStandardIconCount()
		{
			if (AllowParenting)
			{
				return 2;
			}
			else
			{
				return 1;
			}
		}

		public int GetHeaderIconCount(int row)
		{
			if (row == 0)
			{
				return GetStandardIconCount() + GetExtraHeaderIconCount();
			}
			else
			{
				return 3;
			}
		}

		public void DrawHeaderIcon(Graphics g, int rowIndex, int iconIndex, int x, int y, int iconWidth, int highlightedIconIndex)
		{
			Image icon = null;
			if (rowIndex == 0)
			{
				int count = GetStandardIconCount();
				if (iconIndex < count)
				{
					switch (iconIndex)
					{
						case 0:
							icon = Data.Hidden ? Properties.Resources.EyeClosed : Properties.Resources.EyeOpen;
							break;
						case 1:
							if (AllowParenting)
							{
								if (Data.Parent != null)
								{
									icon = Data.Parent.Image;
								}
								else
								{
									icon = Properties.Resources.AddLink;
								}
							}
							break;
					}
				}
				else
				{
					icon = GetExtraHeaderIcon(iconIndex - GetStandardIconCount());
				}
			}
			else
			{
				string property = Data.Properties[rowIndex - 1];
				LiveKeyframeMetadata metadata = Data.GetBlockMetadata(property, _timeline.CurrentTime);
				switch (iconIndex)
				{
					case 0:
						if (highlightedIconIndex == 0)
						{
							icon = Properties.Resources.Loop;
						}
						else
						{
							icon = metadata.Looped ? Properties.Resources.Loop : null;
						}
						break;
					case 1:
						string tween = metadata.Interpolation ?? "none";
						if (!_tweenIcons.TryGetValue(tween, out icon))
						{
							icon = _tweenIcons["none"];
						}
						break;
					case 2:
						string ease = metadata.Ease ?? "smooth";
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

		protected virtual int GetExtraHeaderIconCount() { return 0; }
		protected virtual Image GetExtraHeaderIcon(int iconIndex) { return null; }
		protected virtual string GetExtraHeaderTooltip(WidgetActionArgs args, int iconIndex) { return ""; }

		private void DrawBlock(Graphics g, LiveKeyframe start, LiveKeyframe end, int y, float pps, int rowHeight)
		{
			DrawBlock(g, start.Time, end.Time, y, pps, rowHeight);
		}

		private void DrawBlock(Graphics g, float start, float end, int y, float pps, int rowHeight)
		{
			float startX = (Data.Start + start) * pps;
			float length = (end - start) * pps;
			g.FillRectangle(GetFillBrush(), startX, y, length, rowHeight + 1);
			Pen outline = GetOutline();
			g.DrawLine(outline, startX, y, startX, y + rowHeight);
			g.DrawLine(outline, startX + length, y, startX + length, y + rowHeight);
		}

		public void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int rowHeight)
		{
			if (rowIndex == 0)
			{
				float length = Data.Keyframes[Data.Keyframes.Count - 1].Time * pps;
				if (Data.Keyframes.Count == 1)
				{
					length = Data.Length * pps;
				}
				float startX = Data.Start * pps;
				g.FillRectangle(GetTitleBrush(), startX, y, length, rowHeight + 1);
				g.DrawLine(Timeline.WidgetOutline, startX, y, startX, y + rowHeight);
				g.DrawLine(Timeline.WidgetOutline, startX + length, y, startX + length, y + rowHeight);

				//global keyframes
				foreach (LiveKeyframe kf in Data.Keyframes)
				{
					DrawKeyframe(g, (kf == _selectedFrame && _selectedProperties.Count == 0) || (kf == _hoverFrame && _hoverRow == 0) ? _keyframeFillSelected : _headerKeyframeFill, TimeToX(Data.Start + kf.Time, pps), y + rowHeight / 2 - KeyframeRadius - 1, kf, -1, null);
				}
			}
			else
			{
				string property = Data.Properties[rowIndex - 1];

				if (Data.Keyframes.Count == 1)
				{
					DrawBlock(g, 0, Data.Length, y, pps, rowHeight);
				}
				else
				{
					//fill blocks of contiguous animation
					LiveKeyframe startFrame = null;
					LiveKeyframe lastFrame = null;
					for (int i = 0; i < Data.Keyframes.Count; i++)
					{
						LiveKeyframe kf = Data.Keyframes[i];
						if (kf.HasProperty(property))
						{
							KeyframeType type = kf.GetFrameType(property);
							if (startFrame == null)
							{
								startFrame = kf;
								lastFrame = kf;
							}
							else if (type == KeyframeType.Split)
							{
								//found the end frame
								DrawBlock(g, startFrame, kf, y, pps, rowHeight);
								startFrame = kf;
								lastFrame = kf;
							}
							else if (type == KeyframeType.Begin)
							{
								//found the end frame
								DrawBlock(g, startFrame, lastFrame, y, pps, rowHeight);
								startFrame = kf;
							}
							else
							{
								lastFrame = kf;
							}
						}
					}
					if (startFrame != null && startFrame != Data.Keyframes[Data.Keyframes.Count - 1])
					{
						DrawBlock(g, startFrame, Data.Keyframes[Data.Keyframes.Count - 1], y, pps, rowHeight);
					}
				}

				if (_playing)
				{
					float start, end;
					float time = Data.GetInterpolatedTime(property, Data.Time, Data.TimeOffset, null, null, out start, out end);
					int right = TimeToX(end + Data.Start, pps);
					int left = TimeToX(start + Data.Start, pps);
					int width = (right - left);
					int interpolatedX = left + (int)(width * time);
					g.FillRectangle(Brushes.DarkBlue, interpolatedX - 1, y, 3, rowHeight);
				}

				HashSet<LiveKeyframe> drawnBlocks = new HashSet<LiveKeyframe>();
				for (int i = 0; i < Data.Keyframes.Count; i++)
				{
					LiveKeyframe kf = Data.Keyframes[i];
					if (!kf.HasProperty(property)) { continue; }

					//repeat sign if this is the last frame of a looped block
					LiveKeyframe start, end;
					Data.GetBlock(property, kf.Time, out start, out end);
					if (start != null)
					{
						if (!drawnBlocks.Contains(start))
						{
							drawnBlocks.Add(start);
							LiveKeyframeMetadata metadata = start.GetMetadata(property, false);
							if (metadata != null && metadata.Looped)
							{
								int repeatX = TimeToX(Data.Start + end.Time, pps);
								g.FillEllipse(_repeatFill, repeatX + 6, y + rowHeight / 3 - 2, 4, 4);
								g.FillEllipse(_repeatFill, repeatX + 6, y + 2 * rowHeight / 3 - 2, 4, 4);
								g.FillRectangle(_repeatFill, repeatX + 11, y, 1, rowHeight + 1);
								g.FillRectangle(_repeatFill, repeatX + 13, y, 3, rowHeight + 1);
							}
						}
					}

					//keyframe
					DrawKeyframe(g, (kf == _selectedFrame && (_selectedProperties.Count == 0 || _selectedProperties.Contains(property))) || (kf == _hoverFrame && (_hoverRow == 0 || _hoverRow == rowIndex)) ? _keyframeFillSelected : _keyframeFill, TimeToX(Data.Start + kf.Time, pps), y + rowHeight / 2 - KeyframeRadius - 1, kf, i, property);
				}
			}
		}

		private void DrawKeyframe(Graphics g, Brush brush, int x, int y, LiveKeyframe kf, int frameIndex, string property)
		{
			y += KeyframeRadius + 1;
			Point[] pts;
			KeyframeType type = kf.GetFrameType(property);
			LiveKeyframe nextFrame = null;
			if (property != null)
			{
				for (int i = frameIndex + 1; i < Data.Keyframes.Count; i++)
				{
					LiveKeyframe next = Data.Keyframes[i];
					if (next.HasProperty(property))
					{
						nextFrame = next;
						break;
					}
				}
			}
			if (property != null && type == KeyframeType.Begin)
			{
				// >
				pts = new Point[] { new Point(x, y - KeyframeRadius), new Point(x + KeyframeRadius, y), new Point(x, y + KeyframeRadius) };
			}
			else if (property != null && frameIndex > 0 && nextFrame != null && nextFrame.GetFrameType(property) == KeyframeType.Begin)
			{
				// <
				pts = new Point[] { new Point(x, y - KeyframeRadius), new Point(x, y + KeyframeRadius), new Point(x - KeyframeRadius, y) };
			}
			else if (property != null && type == KeyframeType.Split)
			{
				// <|>
				pts = new Point[] { new Point(x, y - KeyframeRadius), new Point(x + KeyframeRadius, y), new Point(x, y + KeyframeRadius), new Point(x - KeyframeRadius, y), new Point(x, y - KeyframeRadius), new Point(x, y + KeyframeRadius) };
			}
			else
			{
				// <>
				pts = new Point[] { new Point(x - KeyframeRadius, y), new Point(x, y - KeyframeRadius), new Point(x + KeyframeRadius, y), new Point(x, y + KeyframeRadius) };
			}
			g.FillPolygon(brush, pts);
			g.DrawPolygon(_penKeyframe, pts);
		}

		protected int TimeToX(float time, float pps)
		{
			return (int)(time * pps);
		}

		public virtual string GetLabel(int row)
		{
			if (row == 0)
			{
				return Data.Id;
			}
			else
			{
				string property = Data.Properties[row - 1];
				PropertyDefinition definition = Definitions.Instance.Get<PropertyDefinition>(property);
				if (definition != null)
				{
					return definition.Name;
				}
				return "Unknown property";
			}
		}

		public virtual Image GetThumbnail() { return null; }

		public string GetHeaderTooltip(WidgetActionArgs args, int iconIndex)
		{
			if (args.Row > 0)
			{
				string property = Data.Properties[args.Row - 1];
				LiveKeyframeMetadata metadata = Data.GetBlockMetadata(property, _timeline.CurrentTime);
				switch (iconIndex)
				{
					case 0:
						return "Toggle looping";
					case 1:
						return $"Tweening: {metadata.Interpolation ?? "none"}";
					case 2:
						return $"Easing method: {metadata.Ease ?? "smooth"}";
				}
			}
			else
			{
				if (iconIndex < GetStandardIconCount())
				{
					switch (iconIndex)
					{
						case 0:
							return "Toggle visibility";
						case 1:
							return AllowParenting ? (Data.Parent == null ? "Unlinked" : $"Linked to: {Data.ParentId}") : "";
					}
				}
				else
				{
					return GetExtraHeaderTooltip(args, iconIndex - GetStandardIconCount());
				}
			}
			return null;
		}

		public void OnClickHeaderIcon(WidgetActionArgs args, int iconIndex)
		{
			if (args.Row == 0)
			{
				if (iconIndex < GetStandardIconCount())
				{
					switch (iconIndex)
					{
						case 0:
							Data.Hidden = !Data.Hidden;
							break;
						case 1:
							if (AllowParenting)
							{
								List<LiveSprite> sprites = new List<LiveSprite>();
								LivePose pose = Data.Data as LivePose;
								foreach (LiveSprite sprite in pose.Sprites)
								{
									if (string.IsNullOrEmpty(sprite.Id) || sprite == Data)
									{
										continue;
									}
									//if this is an ancestor of the sprite, disallow it to avoid infinite chains
									LiveObject parent = sprite.Parent;
									bool isAncestor = false;
									while (parent != null)
									{
										if (parent == Data)
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
								ContextMenuItem[] items = new ContextMenuItem[sprites.Count + 1];
								items[0] = new ContextMenuItem("Unlinked", null, SelectParent, null, Data.Parent == null);
								for (int i = 0; i < sprites.Count; i++)
								{
									LiveSprite sprite = sprites[i];
									items[i + 1] = new ContextMenuItem(sprite.Id, sprite.Image, SelectParent, sprite.Id, Data.Parent == sprite);
								}
								args.Timeline.ShowContextMenu(items);
							}
							break;
					}
				}
				else
				{
					OnClickExtraHeaderIcon(args, iconIndex - GetStandardIconCount());
				}
			}
			else
			{
				string property = Data.Properties[args.Row - 1];
				LiveKeyframeMetadata metadata = Data.GetBlockMetadata(property, args.Time);
				switch (iconIndex)
				{
					case 0:
						ToggleLooping(property, metadata, !metadata.Looped);
						Invalidate();
						break;
					case 1:
						string tween = metadata.Interpolation;
						args.Timeline.ShowContextMenu(
							new ContextMenuItem("Linear", Properties.Resources.Tween_Linear, SelectTween, new Tuple<string, string>(property, "linear"), tween == "linear"),
							new ContextMenuItem("Spline", Properties.Resources.Tween_Spline, SelectTween, new Tuple<string, string>(property, "spline"), tween == "spline"),
							new ContextMenuItem("None", Properties.Resources.Tween_None, SelectTween, new Tuple<string, string>(property, "none"), tween == "none" || string.IsNullOrEmpty(tween))
							);
						break;
					case 2:
						string ease = metadata.Ease;
						args.Timeline.ShowContextMenu(
							new ContextMenuItem("Linear", Properties.Resources.Curve_Linear, SelectEase, new Tuple<string, string>(property, "linear"), ease == "linear"),
							new ContextMenuItem("Smooth", Properties.Resources.Curve_Smooth, SelectEase, new Tuple<string, string>(property, "smooth"), ease == "smooth" || string.IsNullOrEmpty(ease)),
							new ContextMenuItem("Ease-In-Out Cubic", Properties.Resources.Curve_EaseInOutCubic, SelectEase, new Tuple<string, string>(property, "ease-in-out-cubic"), ease == "ease-in-out-cubic"),
							new ContextMenuItem("Ease-Out-In", Properties.Resources.Curve_EaseOutIn, SelectEase, new Tuple<string, string>(property, "ease-out-in"), ease == "ease-out-in"),
							new ContextMenuItem("Ease-Out-In Cubic", Properties.Resources.Curve_EaseOutInCubic, SelectEase, new Tuple<string, string>(property, "ease-out-in-cubic"), ease == "ease-out-in-cubic"),
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
		protected virtual void OnClickExtraHeaderIcon(WidgetActionArgs args, int iconIndex) { }

		private void ToggleLooping(string property, LiveKeyframeMetadata metadata, bool looped)
		{
			//TODO: make this an ICommand
			LiveKeyframe kf = Data.GetBlockKeyframe(property, _timeline.CurrentTime);
			kf.GetMetadata(property, true).Looped = looped;
		}

		private void SelectTween(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Tuple<string, string> tag = item.Tag as Tuple<string, string>;
			string property = tag.Item1;
			string tween = tag.Item2;

			LiveKeyframe kf = Data.GetBlockKeyframe(property, _timeline.CurrentTime);
			kf.GetMetadata(property, true).Interpolation = tween;
		}

		private void SelectEase(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Tuple<string, string> tag = item.Tag as Tuple<string, string>;
			string property = tag.Item1;
			string ease = tag.Item2;

			LiveKeyframe kf = Data.GetBlockKeyframe(property, _timeline.CurrentTime);
			kf.GetMetadata(property, true).Ease = ease;
		}

		private void SelectParent(object sender, EventArgs e)
		{
			//TODO: Make this an ICommand
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			string id = item.Tag?.ToString();
			Data.ParentId = id;
		}

		public void OnClickHeader(WidgetActionArgs args)
		{
			int row = args.Row;
			if (row == 0)
			{
				ClearSelection();
				args.Timeline.SelectData(Data);
			}
			else
			{
				bool add = args.Modifiers.HasFlag(Keys.Control);
				if (!add)
				{
					ClearSelection();
				}
				_selectedFrame = null;
				string property = Data.Properties[row - 1];
				_selectedProperties.Add(property);
				LiveKeyframeMetadata metadata = Data.GetBlockMetadata(property, args.Time);
				args.Timeline.SelectData(metadata);
				Invalidate();
			}
		}

		public void OnDoubleClickHeader(WidgetActionArgs args)
		{
			//select first keyframe
			SelectFrameDataWithPreview(0);
		}

		public ITimelineAction GetAction(int x, float start, int row, int timelineWidth, float pps)
		{
			_hoverFrame = null;

			//see if a keyframe is selected
			for (int i = Data.Keyframes.Count - 1; i >= 0; i--)
			{
				LiveKeyframe kf = Data.Keyframes[i];
				int kfX = TimeToX(kf.Time, pps);
				if (Math.Abs(x - kfX) <= 5)
				{
					_hoverFrame = kf;
					_hoverRow = row;
					string property = row > 0 ? Data.Properties[row - 1] : null;
					if (string.IsNullOrEmpty(property) || kf.HasProperty(property))
					{
						return new SelectKeyframeTimelineAction(this, kf, property);
					}
				}
			}

			float end = Data.Length * pps;
			if (Data.Keyframes.Count <= 1 && x > end - 5 && x <= end + 5)
			{
				return new ModifyWidgetLengthTimelineAction();
			}
			else if (x >= 5 && x <= end - 5)
			{
				return new MoveWidgetTimelineAction(true);
			}
			return null;
		}

		public void OnStartMove(WidgetActionArgs args)
		{
			ClearSelection();
			if (Data == null || Data.Data is LivePose)
			{
				args.Timeline.SelectData(GetData());
			}
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
				if (time < Data.Start)
				{
					args.Timeline.SelectData(Data);
				}
				else
				{
					SelectFrameDataWithPreview(time);
				}
			}
		}

		protected LiveKeyframe SelectFrameDataWithPreview(float time)
		{
			LiveKeyframe previewFrame = Data.GetInterpolatedFrame(time - Data.Start);

			//show whatever keyframe is under the current time, or an interpolated placeholder if there is none
			LiveKeyframe frame = Data.Keyframes.Find(kf => kf.Time == time);
			if (frame == null)
			{
				frame = Data.CreateKeyframe(time - Data.Start);
				frame.Data = Data;
				frame.PropertyChanged += NewFrame_PropertyChanged;
			}
			_timeline.SelectData(frame, previewFrame);
			return frame;
		}

		private void NewFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//the frame has a real property now, so add it to the sprite
			LiveKeyframe frame = sender as LiveKeyframe;
			frame.PropertyChanged -= NewFrame_PropertyChanged;
			Data.AddKeyframe(frame);
			_timeline.SelectData(frame);
		}

		public bool IsCollapsible
		{
			get { return ShowPropertyRows && Data.AnimatedProperties.Count > 0; }
		}

		public bool IsCollapsed
		{
			get { return _collapsed; }
			set { _collapsed = value; }
		}

		public bool IsRowHighlighted(int row)
		{
			string property = row > 0 ? Data.Properties[row - 1] : null;
			return row == 0 ? _selectedProperties.Count == 0 : _selectedProperties.Contains(property);
		}

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			if (_selectedFrame != null)
			{
				//keyframe selected
				if (!Data.Keyframes.Contains(_selectedFrame))
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
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			if (clipboardData is LiveSprite)
			{
				args.AllowPaste = true;
			}
			else if (clipboardData is LiveKeyframe)
			{
				args.AllowPaste = args.Timeline.CurrentTime >= Data.Start;
			}
		}

		public bool OnCopy(WidgetOperationArgs args)
		{
			if (_selectedFrame != null)
			{
				//copy a frame
				LiveKeyframe kf = Data.CopyKeyframe(_selectedFrame, _selectedProperties);
				Clipboards.Set<KeyframedWidget>(kf);
				return true;
			}
			else
			{
				//copy the whole widget
				LiveAnimatedObject sprite = Data.Copy() as LiveAnimatedObject;
				Clipboards.Set<KeyframedWidget>(sprite);
				return true;
			}
		}

		public bool OnDelete(WidgetOperationArgs args)
		{
			if (_selectedFrame != null)
			{
				if (_selectedProperties.Count == 0)
				{
					DeleteKeyframeCommand command = new DeleteKeyframeCommand(Data, _selectedFrame);
					args.History.Commit(command);
				}
				else
				{
					args.History.StartBulkRecord();
					foreach (string property in _selectedProperties)
					{
						DeletePropertyCommand command = new DeletePropertyCommand(Data, _selectedFrame, property);
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
					DeleteAnimatedPropertyCommand command = new DeleteAnimatedPropertyCommand(Data, property);
					command.Do();
					args.History.Record(command);
				}
				args.History.EndBulkRecord();
			}
			else
			{
				//delete whole widget
				if (AllowDelete && (args.IsSilent || MessageBox.Show($"Are you sure you want to completely remove {ToString()}?", "Remove Sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
				{
					DeleteWidgetCommand command = new DeleteWidgetCommand(Data.Data, this);
					args.History.Commit(command);
					args.Timeline.SelectData(null);
				}
			}
			return true;
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			if (clipboardData is LiveKeyframe)
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
					LiveKeyframe frame = Data.Keyframes.Find(k => minTime <= k.Time && maxTime >= k.Time);
					if (frame != null)
					{
						time = frame.Time;
					}

					PasteKeyframeCommand command = new PasteKeyframeCommand(Data, copiedFrame, time);
					args.History.Commit(command);
					_selectedFrame = command.NewKeyframe;
					_selectedProperties.Clear();
					args.Timeline.SelectData(_selectedFrame);
				}
			}
			else if (clipboardData is LiveAnimatedObject)
			{
				return Data.Data.Paste(args, Data);
			}
			return true;
		}

		public bool OnDuplicate(WidgetOperationArgs args)
		{
			if (_selectedFrame == null)
			{
				//duplicating whole widget
				LiveObject sprite = Data.Copy();
				object oldClipboard = Clipboards.Get<KeyframedWidget, object>();
				Clipboards.Set<KeyframedWidget>(sprite);
				Data.Data.Paste(args, Data);
				Clipboards.Set<KeyframedWidget>(oldClipboard);
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
				LiveKeyframe frame = Data.Keyframes.Find(k => minTime <= k.Time && maxTime >= k.Time);
				if (frame != null)
				{
					time = frame.Time;
				}
				if (Math.Abs(_selectedFrame.Time + Data.Start - time) < 0.001f)
				{
					return false; //can't duplicate into itself
				}

				PasteKeyframeCommand command = new PasteKeyframeCommand(Data, _selectedFrame, time);
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
				args.ItemsToAdd.Add(new ContextMenuItem("Toggle Keyframe Type", Properties.Resources.SplitKeyframe, ToggleSplit, null, false));
			}
		}

		private void ToggleSplit(object sender, EventArgs e)
		{
			if (_selectedFrame == null)
			{
				return;
			}
			ToggleKeyframeTypeCommand command = new ToggleKeyframeTypeCommand(Data, _selectedFrame, _selectedProperties);
			_timeline.CommandHistory.Commit(command);
		}

		public void OnDoubleClick(WidgetActionArgs args)
		{
			SelectFrameDataWithPreview(args.Time);
		}
	}
}
