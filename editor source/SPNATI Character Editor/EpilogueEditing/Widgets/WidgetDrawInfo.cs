using System;
using System.Collections.Generic;
using System.Drawing;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class WidgetDrawInfo
	{
		public List<WidgetBlock> Blocks = new List<WidgetBlock>();
		private Dictionary<LiveKeyframe, KeyframeDrawStyle> _keyframesType = new Dictionary<LiveKeyframe, KeyframeDrawStyle>();

		private SolidBrush _repeatFill;

		public WidgetDrawInfo(LiveAnimatedObject Data, string property)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			_repeatFill = new SolidBrush(Color.FromArgb(103, 106, 116));
			if (skin.AppColors.ContainsKey("WidgetRepeat"))
			{
				_repeatFill.Color = skin.GetAppColor("WidgetRepeat");
			}

			if (property == "")
			{
				WidgetBlock block = new WidgetBlock();
				Blocks.Add(block);
				float length = Data.Keyframes[Data.Keyframes.Count - 1].Time;
				if (Data.Keyframes.Count == 1)
				{
					length = Data.Length;
				}

				block.Start = Data.Start;
				block.Length = length;

				//global keyframes
				foreach (LiveKeyframe kf in Data.Keyframes)
				{
					_keyframesType[kf] = KeyframeDrawStyle.Normal;
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
				}
				else
				{
					//fill blocks of contiguous animation
					HashSet<LiveKeyframe> visited = new HashSet<LiveKeyframe>();
					LiveKeyframe lastFrame = null;
					for (int i = 0; i < Data.Keyframes.Count; i++)
					{
						LiveKeyframe kf = Data.Keyframes[i];
						if (kf.HasProperty(property))
						{
							LiveKeyframe startFrame = null;
							LiveKeyframe endFrame = null;

							KeyframeType type = kf.GetFrameType(property);
							_keyframesType[kf] = ToDrawStyle(type);
							if (type == KeyframeType.Begin && lastFrame != null && _keyframesType[lastFrame] != KeyframeDrawStyle.Begin)
							{
								_keyframesType[lastFrame] = KeyframeDrawStyle.End;
							}
							lastFrame = kf;

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

		public void Draw(Graphics g, Brush brush, Pen outline, int y, float pps, int rowHeight, float dataEndTime)
		{
			foreach (WidgetBlock block in Blocks)
			{
				float length = block.Length * pps;
				float startX = block.Start * pps;
				if (dataEndTime > 0)
				{
					length = (dataEndTime - block.Start) * pps;
				}
				g.FillRectangle(brush, startX, y, length, rowHeight + 1);
				g.DrawLine(outline, startX, y, startX, y + rowHeight);
				g.DrawLine(outline, startX + length, y, startX + length, y + rowHeight);

				if (block.Repeat)
				{
					int repeatX = (int)((block.Start + block.Length) * pps);
					g.FillEllipse(_repeatFill, repeatX + 6, y + rowHeight / 3 - 2, 4, 4);
					g.FillEllipse(_repeatFill, repeatX + 6, y + 2 * rowHeight / 3 - 2, 4, 4);
					g.FillRectangle(_repeatFill, repeatX + 11, y, 1, rowHeight + 1);
					g.FillRectangle(_repeatFill, repeatX + 13, y, 3, rowHeight + 1);
				}
			}
		}
	}

	public class WidgetBlock
	{
		public float Start { get; set; }
		public float Length { get; set; }
		public bool Repeat { get; set; }
	}

	public enum KeyframeDrawStyle
	{
		Normal,
		Begin,
		End,
		Split
	}
}
