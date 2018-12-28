using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class SliderControl : PropertyEditControl
	{
		private bool _cleared;

		public SliderControl()
		{
			InitializeComponent();
		}

		private void RemoveHandlers()
		{
			slider.ValueChanged -= Slider_ValueChanged;
			valValue.TextChanged -= Field_ValueChanged;
		}

		private void AddHandlers()
		{
			slider.ValueChanged += Slider_ValueChanged;
			valValue.TextChanged += Field_ValueChanged;
		}

		private void Slider_ValueChanged(object sender, EventArgs e)
		{
			_cleared = false;
			RemoveHandlers();
			valValue.Value = slider.Value;
			AddHandlers();
			Save();
		}

		private void Field_ValueChanged(object sender, EventArgs e)
		{
			_cleared = false;
			RemoveHandlers();
			slider.Value = (int)valValue.Value;
			AddHandlers();
			Save();
		}

		protected override void OnBoundData()
		{
			_cleared = false;
			string value = GetValue()?.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				int v;
				if (int.TryParse(value, out v))
				{
					slider.Value = Math.Max(slider.Minimum, Math.Min(slider.Maximum, v));
					valValue.Value = slider.Value;
				}
			}
			else
			{
				_cleared = true;
				slider.Value = 0;
				valValue.Text = "";
			}

			AddHandlers();
		}

		protected override void OnRebindData()
		{
			RemoveHandlers();
			OnBoundData();
		}

		public override void Clear()
		{
			_cleared = true;
			Save();
		}

		public override void Save()
		{
			if (DataType == typeof(int))
			{
				int v = (int)valValue.Value;
				SetValue(slider.Value);
			}
			else if (DataType == typeof(string))
			{
				if (_cleared || valValue.Text == "")
				{
					SetValue(null);
				}
				else
				{
					SetValue(valValue.Value.ToString());
				}
			}
		}
	}

	public class SliderAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(SliderControl); }
		}
	}
}
