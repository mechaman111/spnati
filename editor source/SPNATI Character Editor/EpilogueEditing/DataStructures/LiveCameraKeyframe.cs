using System.Drawing;
using Desktop.CommonControls.PropertyControls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveCameraKeyframe : LiveKeyframe
	{
		[Float(DisplayName = "Zoom", GroupOrder = 100, Description = "Zoom scaling factor for the camera", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f)]
		public float? Zoom
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Slider(DisplayName = "Opacity (0-100)", GroupOrder = 90, Key = "alpha", Description = "Opacity/transparency level")]
		public float? Alpha
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Color(DisplayName = "Color", Key = "color", GroupOrder = 85, Description = "Color")]
		public Color Color
		{
			get { return Get<Color>(); }
			set { Set(value); }
		}

		public LiveCameraKeyframe() : base()
		{
			TrackedProperties.Add("Zoom");
			TrackedProperties.Add("Color");
			TrackedProperties.Add("Alpha");
		}

		protected override object GetDefaultValue(string property)
		{
			switch (property)
			{
				case "Zoom":
					return 1.0f;
				case "Alpha":
					return 0f;
				default: return base.GetDefaultValue(property);
			}
		}
	}
}
