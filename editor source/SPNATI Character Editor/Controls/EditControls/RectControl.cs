using System;
using Desktop.CommonControls;
using Desktop;
using KisekaeImporter.ImageImport;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class RectControl : PropertyEditControl
	{
		public RectControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			Rect rect = (Rect)GetValue();
			valLeft.Value = rect.Left;
			valWidth.Value = rect.Right - rect.Left;
			valTop.Value = rect.Top;
			valHeight.Value = rect.Bottom - rect.Top;
		}

		protected override void AddHandlers()
		{
			valLeft.ValueChanged += ValueChanged;
			valTop.ValueChanged += ValueChanged;
			valWidth.ValueChanged += ValueChanged;
			valHeight.ValueChanged += ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			valLeft.ValueChanged -= ValueChanged;
			valTop.ValueChanged -= ValueChanged;
			valWidth.ValueChanged -= ValueChanged;
			valHeight.ValueChanged -= ValueChanged;
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			AddHandlers();
			Save();
		}

		protected override void OnSave()
		{
			base.OnSave();
		}
	}

	public class RectAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(RectControl); }
		}
	}
}
