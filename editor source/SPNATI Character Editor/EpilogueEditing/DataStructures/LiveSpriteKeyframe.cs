using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using System.IO;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveSpriteKeyframe : LiveKeyframe
	{
		public LiveSpriteKeyframe() : base()
		{
			TrackedProperties.Add("Src");
			TrackedProperties.Add("ScaleX");
			TrackedProperties.Add("ScaleY");
			TrackedProperties.Add("Alpha");
			TrackedProperties.Add("Rotation");
			TrackedProperties.Add("SkewX");
			TrackedProperties.Add("SkewY");
		}

		[FileSelect(DisplayName = "Source", GroupOrder = 10, Key = "src", Description = "Sprite source image")]
		public string Src
		{
			get { return Get<string>(); }
			set
			{
				if (value == Src)
				{
					return;
				}
				if (Data.AllowsCrossStageImages)
				{
					string filename = Path.GetFileName(value);
					int stage;
					string id;
					PoseMap.ParseImage(filename, out stage, out id);
					if (stage >= 0)
					{
						value = value.Replace($"{stage}-", "#-");
					}
				}
				Set(value);
			}
		}

		[Float(DisplayName = "Scale X", GroupOrder = 40, Key = "scalex", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Scale Y", GroupOrder = 45, Key = "scaley", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Slider(DisplayName = "Opacity (0-100)", GroupOrder = 30, Key = "alpha", Description = "Opacity/transparency level")]
		public float? Alpha
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Rotation (deg)", GroupOrder = 50, Key = "rotation", Description = "Sprite rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		public float? Rotation
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew X", GroupOrder = 60, Key = "skewx", Description = "Sprite shearing factor horizontally", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew Y", GroupOrder = 65, Key = "skewx", Description = "Sprite shearing factor vertically", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		protected override object GetDefaultValue(string property)
		{
			switch (property)
			{
				case "ScaleX":
				case "ScaleY":
					return 1.0f;
				case "Alpha":
					return 100f;
				default: return base.GetDefaultValue(property);
			}
		}

	}
}
