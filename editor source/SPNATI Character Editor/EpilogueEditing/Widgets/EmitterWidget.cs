using System.ComponentModel;
using System.Drawing;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class EmitterWidget : KeyframedWidget
	{
		private const int ThumbnailHeight = 32;

		private static SolidBrush _titleFill;
		private static SolidBrush _subrowFill;
		private static SolidBrush _selectedSubrowFill;
		private static Color _accentColor;

		public LiveEmitter Emitter { get; private set; }

		private Image _thumbnail;

		protected override void OnSetData(LiveAnimatedObject data)
		{
			Emitter = data as LiveEmitter;
		}

		static EmitterWidget()
		{
			_titleFill = new SolidBrush(Color.White);
			_subrowFill = new SolidBrush(Color.White);
			_selectedSubrowFill = new SolidBrush(Color.White);
			SetEmitterSkin(SkinManager.Instance.CurrentSkin);
		}

		protected override void OnSetDefaultColors()
		{
			_titleFill.Color = Color.FromArgb(153, 197, 255);
			_subrowFill.Color = Color.FromArgb(203, 206, 216);
			_selectedSubrowFill.Color = Color.FromArgb(223, 226, 236);
			_accentColor = Color.Pink;
		}

		protected override void OnUpdateSkin(Skin skin)
		{
			SetEmitterSkin(skin);
		}

		protected override Color GetAccentColor()
		{
			return _accentColor;
		}

		private static void SetEmitterSkin(Skin skin)
		{
			_titleFill.Color = skin.GetAppColor("WidgetHeaderRow");
			_subrowFill.Color = skin.GetAppColor("WidgetRow");
			_selectedSubrowFill.Color = skin.GetAppColor("WidgetRowSelected");
			_accentColor = skin.GetAppColor("EmitterAccent");
		}

		public EmitterWidget(LiveEmitter emitter, Timeline timeline) : base(emitter, timeline)
		{
			_timeline = timeline;
			Emitter = emitter;
			Emitter.Widget = this;
			AllowParenting = false;
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Emitter.Id) ? "Empty Emitter" : Emitter.Id;
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
	}
}
