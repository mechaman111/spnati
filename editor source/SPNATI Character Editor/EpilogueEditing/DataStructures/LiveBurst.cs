using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveBurst : LiveEvent
	{
		[Desktop.CommonControls.PropertyControls.Numeric(DisplayName = "Count", Key = "count", GroupOrder = 100, Description = "Number of particles to emit at this moment", Minimum = 1, Maximum = 100)]
		public int Count
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public override string ToString()
		{
			return "Particle Burst - " + base.ToString();
		}

		public override void Trigger()
		{
			LiveEmitter emitter = Data as LiveEmitter;
			if (emitter != null)
			{
				for (int i = 0; i < Count; i++)
				{
					emitter.Emit();
				}
			}
		}

		public override Directive CreateDirectiveDefinition()
		{
			Directive directive = new Directive();
			directive.DirectiveType = "emit";
			directive.Count = Count;
			directive.Delay = Time.ToString(CultureInfo.InvariantCulture);
			return directive;
		}
	}
}
