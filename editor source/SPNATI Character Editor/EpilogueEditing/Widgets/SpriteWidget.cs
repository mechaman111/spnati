using System.ComponentModel;
using System.Drawing;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class SpriteWidget : KeyframedWidget
	{
		private const int ThumbnailHeight = 32;

		private static SolidBrush _titleFill;
		private static SolidBrush _subrowFill;
		private static SolidBrush _selectedSubrowFill;
		private static Color _accentColor;

		public LiveSprite Sprite { get; private set; }

		private Image _thumbnail;

		protected override void OnSetData(LiveAnimatedObject data)
		{
			Sprite = data as LiveSprite;
		}

		static SpriteWidget()
		{
			_titleFill = new SolidBrush(Color.White);
			_subrowFill = new SolidBrush(Color.White);
			_selectedSubrowFill = new SolidBrush(Color.White);
			SetSpriteSkin(SkinManager.Instance.CurrentSkin);
		}

		protected override void OnSetDefaultColors()
		{
			_titleFill.Color = Color.FromArgb(153, 197, 255);
			_subrowFill.Color = Color.FromArgb(203, 206, 216);
			_selectedSubrowFill.Color = Color.FromArgb(223, 226, 236);
		}

		protected override void OnUpdateSkin(Skin skin)
		{
			SetSpriteSkin(skin);
		}

		private static void SetSpriteSkin(Skin skin)
		{
			_titleFill.Color = skin.GetAppColor("WidgetHeaderRow");
			_subrowFill.Color = skin.GetAppColor("WidgetRow");
			_selectedSubrowFill.Color = skin.GetAppColor("WidgetRowSelected");
			_accentColor = skin.GetAppColor("SpriteAccent");
		}

		public SpriteWidget(LiveSprite sprite, Timeline timeline) : base(sprite, timeline)
		{
			_timeline = timeline;
			Sprite = sprite;
			Sprite.Widget = this;
			AllowParenting = true; // sprite.Data is LivePose;
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Sprite.Id) ? "Empty Sprite" : Sprite.Id;
		}

		protected override void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Keyframes")
			{
				_thumbnail?.Dispose();
				_thumbnail = null;
			}
		}

		protected override SolidBrush GetFillBrush(bool selected)
		{
			return selected ? _selectedSubrowFill : _subrowFill;
		}

		protected override SolidBrush GetTitleBrush()
		{
			return _titleFill;
		}

		protected override int GetExtraHeaderIconCount()
		{
			return 1;
		}

		protected override Color GetAccentColor()
		{
			return _accentColor;
		}

		protected override Image GetExtraHeaderIcon(int iconIndex)
		{
			switch (iconIndex)
			{
				case 0:
					return Properties.Resources.Image;
			}
			return null;
		}

		public override Image GetThumbnail()
		{
			if (_thumbnail == null && Data.Properties.Contains("Src"))
			{
				string src = Data.GetPropertyValue<string>("Src", 0, 0, null);
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

		protected override string GetExtraHeaderTooltip(WidgetActionArgs args, int iconIndex)
		{
			switch (iconIndex)
			{
				case 0:
					return "Change source for first frame";
			}
			return "";
		}

		protected override void OnClickExtraHeaderIcon(WidgetActionArgs args, int iconIndex)
		{
			switch (iconIndex)
			{
				case 0:
					if (SelectedFrame != null)
					{
						_timeline.RequestUI(SelectedFrame);
					}
					else
					{
						LiveKeyframe frame = SelectFrameDataWithPreview(Data.Start);
						if (frame != null)
						{
							_timeline.RequestUI(frame);
						}
					}
					break;
			}
		}
	}
}
