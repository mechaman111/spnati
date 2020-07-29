using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveKeyframeMetadata : BindableObject
	{
		private string _property;

		/// <summary>
		/// Type of keyframe (start of new set, split, mid-animation, etc.)
		/// </summary>
		public KeyframeType FrameType
		{
			get { return Get<KeyframeType>(); }
			set { Set(value); }
		}

		public string Ease
		{
			get { return Get<string>(); }
			set
			{
				if (_property == "Src" && value != "linear")
				{
					value = "linear";
				}
				Set(value);
			}
		}

		public string Interpolation
		{
			get { return Get<string>(); }
			set
			{
				if (_property == "Src")
				{
					value = "none";
				}
				Set(value);
			}
		}

		/// <summary>
		/// Whether this frame is the start of a loop
		/// </summary>
		public bool Looped
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[ComboBox(DisplayName = "Repeat Method", Key = "clamp", GroupOrder = 43, Description = "How a looping animation loops", Options = new string[] { "clamp", "wrap", "mirror" })]
		/// <summary>
		/// Clamping method to use for restricting a looped animations's time between 0 and 1
		/// </summary>
		public string ClampMethod
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Numeric(DisplayName = "Iterations", Key = "iterations", GroupOrder = 44, Description = "How many times to repeat a looped animation. 0 means infinite.", Minimum = 0, Maximum = 1000)]
		/// <summary>
		/// Number of iterations to loop, if Looped is true. 0 means loop indefinitely.
		/// </summary>
		public int Iterations
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public LiveKeyframeMetadata() { }

		public LiveKeyframeMetadata(string property)
		{
			_property = property;
			Interpolation = (property == "Src" || property == "Text" || property == "Burst") ? "none" : "linear";
			Ease = (property == "Src" || property == "Text") ? "linear" : "smooth";
		}

		public string ToKey()
		{
			string looped = Looped ? "1" : "";
			return $"{looped}|{(Ease ?? "")}|{(Interpolation ?? "")}|{(ClampMethod ?? "")}|{Iterations}";
		}

		public override string ToString()
		{
			return $"{_property} - Keyframe Animation Settings";
		}

		public bool Indefinite
		{
			get { return Looped && Iterations == 0; }
		}
	}

	public enum KeyframeType
	{
		/// <summary>
		/// Normal mid-animation keyframe
		/// </summary>
		Normal,
		/// <summary>
		/// Beginning of a new animation
		/// </summary>
		Begin,
		/// <summary>
		/// End of previous and beginning of next
		/// </summary>
		Split,
	}
}
