using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveEmitterKeyframe : LiveKeyframe
	{
		public LiveEmitterKeyframe() : base()
		{
			TrackedProperties.Add("Src");
			TrackedProperties.Add("Rotation");
			TrackedProperties.Add("Rate");
		}

		[FileSelect(DisplayName = "Source", GroupOrder = 10, Key = "src", Description = "Particle source image")]
		public string Src
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Rotation (deg)", GroupOrder = 50, Key = "rotation", Description = "Emitter rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		public float? Rotation
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Rate", Key = "rate", GroupOrder = 25, Description = "Emissions per second", Minimum = 0, Maximum = 100, DecimalPlaces = 2)]
		public float? Rate
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}
	}
}
