using System;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.EditControls
{
	public partial class AnimDurationControl : FloatControl
	{
		public AnimDurationControl()
		{
			InitializeComponent();
		}

		protected override void OnBindingUpdated(string property)
		{
			base.OnBindingUpdated(property);
			UpdateEnabled();
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();

			UpdateEnabled();
		}

		private void UpdateEnabled()
		{
			LiveSprite data = Data as LiveSprite;
			if (data != null && data.Keyframes.Count > 1)
			{
				Enabled = false;
			}
			else
			{
				Enabled = true;
			}
		}
	}

	public class AnimDurationAttribute : FloatAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(AnimDurationControl); }
		}
	}
}
