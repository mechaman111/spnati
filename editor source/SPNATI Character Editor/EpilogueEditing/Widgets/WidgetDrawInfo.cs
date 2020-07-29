using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class WidgetDrawInfo
	{
		public List<WidgetBlock> Blocks = new List<WidgetBlock>();
		private Dictionary<LiveKeyframe, KeyframeDrawStyle> _keyframesType = new Dictionary<LiveKeyframe, KeyframeDrawStyle>();

		private SolidBrush _repeatFill;
		private SolidBrush _fillExtra = new SolidBrush(Color.White);
		private SolidBrush _accentFill = new SolidBrush(Color.Blue);

		public WidgetDrawInfo(LiveAnimatedObject Data, string property, float duration)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			_repeatFill = new SolidBrush(Color.FromArgb(103, 106, 116));
			if (skin.AppColors.ContainsKey("WidgetRepeat"))
			{
				_repeatFill.Color = skin.GetAppColor("WidgetRepeat");
			}

			int kfCount = 0;

			if (property == "")
			{
				//merge the blocks of each property
				HashSet<Interval> intervals = new HashSet<Interval>();

				foreach (string prop in Data.Properties)
				{
					LiveKeyframe blockStart = null;
					LiveKeyframe blockEnd = null;
					bool inLoop = false;
					foreach (LiveKeyframe kf in Data.Keyframes)
					{
						if (kf.HasProperty(prop))
						{
							LiveKeyframeMetadata md = kf.GetMetadata(prop, false);
							if (blockStart == null)
							{
								if (md.Indefinite)
								{
									inLoop = true;
								}
								blockStart = kf;
								blockEnd = blockStart;
							}
							else
							{
								switch (md.FrameType)
								{
									case KeyframeType.Normal:
										blockEnd = kf;
										break;
									case KeyframeType.Begin:
									case KeyframeType.Split:
										if (md.FrameType == KeyframeType.Split)
										{
											//this is the end of a block and the start of the next
											blockEnd = kf;
										}
										if (!inLoop)
										{
											intervals.Add(new Interval(blockStart.Time, blockEnd.Time));
										}

										inLoop = false;
										blockEnd = kf;
										blockStart = kf;
										break;
								}
							}
						}
					}
					if (blockStart != null && !inLoop)
					{
						if (blockEnd == null)
						{
							blockEnd = blockStart;
						}
						intervals.Add(new Interval(blockStart.Time, blockEnd.Time));
					}
				}

				List<Interval> finalIntervals = new List<Interval>();
				foreach (Interval interval in intervals)
				{
					float start = interval.Start;
					float end = interval.End;
					if (start == end)
					{
						continue;
					}

					//find the intervals containing start and end
					Interval startInterval = finalIntervals.Find(t => t.Start < start && t.End > start);
					Interval endInterval = finalIntervals.Find(t => t.Start < end && t.End > end);

					if (startInterval == null && endInterval == null)
					{
						//block is not in any interval, so add it to the final list
						finalIntervals.Add(interval);
					}
					else if (startInterval == endInterval)
					{
						//both are contained within the same interval; we can ignore this one
					}
					else if (startInterval == null && endInterval != null)
					{
						//start is outside; expand the interval to include the start
						endInterval.Start = start;
						//may need to merge other intervals into this one now
						for (int j = finalIntervals.Count - 1; j >= 0; j--)
						{
							Interval other = finalIntervals[j];
							if (other == endInterval)
							{
								continue;
							}
							if (other.Start >= endInterval.Start && other.End <= endInterval.End)
							{
								finalIntervals.RemoveAt(j);
							}
						}
					}
					else if (startInterval != null && endInterval == null)
					{
						//end is outside; expand the interval to include it
						startInterval.End = end;
						//may need to merge other intervals into this one now
						for (int j = finalIntervals.Count - 1; j >= 0; j--)
						{
							Interval other = finalIntervals[j];
							if (other == startInterval)
							{
								continue;
							}
							if (other.Start >= startInterval.Start && other.End <= startInterval.End)
							{
								finalIntervals.RemoveAt(j);
							}
						}
					}
					else
					{
						//these are in different intervals. Combine them
						finalIntervals.Remove(endInterval);
						startInterval.End = endInterval.End;
					}
				}

				//create blocks for each remaining interval
				HashSet<float> starts = new HashSet<float>();
				HashSet<float> ends = new HashSet<float>();
				foreach (Interval i in finalIntervals)
				{
					if (i.Start == i.End)
					{
						continue;
					}
					WidgetBlock block = new WidgetBlock();
					Blocks.Add(block);
					block.Start = i.Start + Data.Start;
					starts.Add(i.Start);
					ends.Add(i.End);
					block.Length = i.End - i.Start;
				}
				if (Blocks.Count == 0)
				{
					WidgetBlock block = new WidgetBlock();
					block.Start = Data.Start;
					block.Length = Data.Length;
					Blocks.Add(block);
				}

				//global keyframes
				foreach (LiveKeyframe kf in Data.Keyframes)
				{
					bool onEnd = ends.Contains(kf.Time);
					bool onStart = starts.Contains(kf.Time);
					_keyframesType[kf] = onEnd && onStart ? KeyframeDrawStyle.Split : onStart ? KeyframeDrawStyle.Begin : KeyframeDrawStyle.Normal;
				}
			}
			else
			{
				if (Data.Keyframes.Count == 1)
				{
					WidgetBlock block = new WidgetBlock();
					block.Start = Data.Start;
					block.Length = Data.Length;
					Blocks.Add(block);
					kfCount++;
				}
				else
				{
					//fill blocks of contiguous animation
					HashSet<LiveKeyframe> visited = new HashSet<LiveKeyframe>();
					LiveKeyframe lastFrame = null;
					int lastFrameIndex = -1;
					for (int i = 0; i < Data.Keyframes.Count; i++)
					{
						LiveKeyframe kf = Data.Keyframes[i];
						if (kf.HasProperty(property))
						{
							kfCount++;
							LiveKeyframe startFrame = null;
							LiveKeyframe endFrame = null;

							KeyframeType type = kf.GetFrameType(property);
							_keyframesType[kf] = ToDrawStyle(type);
							if (type == KeyframeType.Begin && lastFrame != null && _keyframesType[lastFrame] != KeyframeDrawStyle.Begin && lastFrameIndex > 0)
							{
								_keyframesType[lastFrame] = KeyframeDrawStyle.End;
							}
							lastFrame = kf;
							lastFrameIndex = i;

							Data.GetBlock(property, kf.Time, false, out startFrame, out endFrame);
							if (visited.Contains(startFrame)) { continue; }

							visited.Add(startFrame);

							WidgetBlock block = new WidgetBlock();
							Blocks.Add(block);

							LiveKeyframeMetadata metadata = startFrame.GetMetadata(property, false);
							block.Start = Data.Start + startFrame.Time;
							block.Length = endFrame.Time - startFrame.Time;
							block.Repeat = metadata.Looped;
						}
					}

					if (Blocks.Count == 1 && Blocks[0].Length == 0 && Data.Keyframes.Count > 0)
					{
						//if there's only one keyframe, display the block out to the last keyframe
						Blocks[0].Length = Data.Keyframes[Data.Keyframes.Count - 1].Time - (Blocks[0].Start - Data.Start);
					}
				}
			}

			if (Data.LinkedToEnd && Blocks.Count > 0 && (property == "" || kfCount <= 1))
			{
				//if linking to the end, extend the last block to the end
				WidgetBlock block = Blocks[Blocks.Count - 1];
				block.Length = duration - block.Start;
			}
		}

		private KeyframeDrawStyle ToDrawStyle(KeyframeType type)
		{
			switch (type)
			{
				case KeyframeType.Begin:
					return KeyframeDrawStyle.Begin;
				case KeyframeType.Split:
					return KeyframeDrawStyle.Split;
				default:
					return KeyframeDrawStyle.Normal;
			}
		}

		public KeyframeDrawStyle GetKeyframeType(LiveKeyframe kf)
		{
			KeyframeDrawStyle type;
			if (!_keyframesType.TryGetValue(kf, out type))
			{
				return KeyframeDrawStyle.Normal;
			}
			return type;
		}

		public void Draw(Graphics g, SolidBrush brush, Pen outline, int y, float pps, int rowHeight, Color? accentColor, float dataEndTime)
		{
			for (int i = 0; i < Blocks.Count; i++)
			{
				WidgetBlock block = Blocks[i];
				float length = block.Length * pps;
				float startX = block.Start * pps + Timeline.StartBuffer;
				g.FillRectangle(brush, startX, y, length, rowHeight + 1);
				if (accentColor.HasValue)
				{
					_accentFill.Color = accentColor.Value;
					g.FillRectangle(_accentFill, startX, y, length, 2);
				}
				g.DrawLine(outline, startX, y, startX, y + rowHeight);
				g.DrawLine(outline, startX + length, y, startX + length, y + rowHeight);

				if (block.Repeat)
				{
					int repeatX = (int)((block.Start + block.Length) * pps) + Timeline.StartBuffer;
					g.FillEllipse(_repeatFill, repeatX + 6, y + rowHeight / 3 - 2, 4, 4);
					g.FillEllipse(_repeatFill, repeatX + 6, y + 2 * rowHeight / 3 - 2, 4, 4);
					g.FillRectangle(_repeatFill, repeatX + 11, y, 1, rowHeight + 1);
					g.FillRectangle(_repeatFill, repeatX + 13, y, 3, rowHeight + 1);
				}
			}
		}

		private class Interval
		{
			public float Start;
			public float End;
			public Interval(float s, float e)
			{
				Start = s;
				End = e;
			}

			public override int GetHashCode()
			{
				return (Start.GetHashCode() * 397) ^ End.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				Interval i = obj as Interval;
				return i != null && i.Start == Start && i.End == End;
			}

			public override string ToString()
			{
				return $"({Start}-{End})";
			}
		}
	}

	public class WidgetBlock
	{
		public float Start { get; set; }
		public float Length { get; set; }
		public bool Repeat { get; set; }

		public override string ToString()
		{
			return $"{Start} - {Length}s";
		}
	}

	public enum KeyframeDrawStyle
	{
		Normal,
		Begin,
		End,
		Split
	}
}
