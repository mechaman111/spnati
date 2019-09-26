using System;
using System.Collections.Generic;

namespace Desktop.CommonControls.PropertyControls
{
	/// <summary>
	/// Edit control for selecting from an enum
	/// </summary>
	public partial class EnumControl : PropertyEditControl
	{
		public EnumControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			EnumControlAttribute p = parameters as EnumControlAttribute;
			cboItems.Items.Clear();
			cboItems.Items.AddRange(Enum.GetValues(p.ValueType));
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				int v;
				int.TryParse(values[0], out v);
				cboItems.SelectedIndex = v;
			}
		}

		public override void BuildMacro(List<string> values)
		{
			string text = cboItems.SelectedIndex.ToString();
			values.Add(text);
		}

		protected override void RemoveHandlers()
		{
			cboItems.SelectedIndexChanged -= cboItems_SelectedIndexChanged;
		}

		protected override void AddHandlers()
		{
			cboItems.SelectedIndexChanged += cboItems_SelectedIndexChanged;
		}

		protected override void OnBoundData()
		{
			cboItems.SelectedIndex = (int)GetValue();
		}

		protected override void OnClear()
		{
			cboItems.SelectedIndex = -1;
			Save();
		}

		protected override void OnSave()
		{
			int value = cboItems.SelectedIndex;
			SetValue(value);
		}

		private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class EnumControlAttribute : EditControlAttribute
	{
		public Type ValueType;

		public override Type EditControlType
		{
			get { return typeof(EnumControl); }
		}
	}
}
