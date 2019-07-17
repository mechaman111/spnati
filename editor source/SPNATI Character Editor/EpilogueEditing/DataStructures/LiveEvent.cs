using Desktop.DataStructures;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public abstract class LiveEvent : BindableObject
	{
		public LiveEvent() { }

		public LiveAnimatedObject Data;

		public float Time
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		public override string ToString()
		{
			return Time.ToString();
		}

		public abstract void Trigger();

		public abstract Directive CreateDirectiveDefinition();
	}
}
