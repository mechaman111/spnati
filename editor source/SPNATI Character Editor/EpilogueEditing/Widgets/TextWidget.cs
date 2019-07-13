using System;
using System.Drawing;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class TextWidget : KeyframedWidget
	{
		private SolidBrush _fillBrush;
		private SolidBrush _selectedBrush;

		public TextWidget(LiveAnimatedObject data, Timeline timeline) : base(data, timeline)
		{
			data.PropertyChanged += Bubble_PropertyChanged;
			_fillBrush = new SolidBrush(Color.LightGray);
			_selectedBrush = new SolidBrush(Color.White);
			ShowPropertyRows = false;
		}

		private void Bubble_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Text")
			{
				Invalidate();
			}
		}

		protected override Brush GetFillBrush(bool selected)
		{
			return selected ? _selectedBrush : _fillBrush;
		}

		public override Image GetThumbnail()
		{
			return Properties.Resources.SpeechBubble;
		}

		protected override Brush GetTitleBrush()
		{
			return Brushes.HotPink;
		}

		public override string GetLabel(int row)
		{
			if (row == 0)
			{
				LiveBubbleKeyframe kf = Data.Keyframes[0] as LiveBubbleKeyframe;
				return kf.Text ?? Data.Id;
			}
			else
			{
				return base.GetLabel(row);
			}
		}

		public override string ToString()
		{
			return $"Text: {Data.ToString()}";
		}
	}
}
