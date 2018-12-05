using System;

namespace Desktop.CommonControls.PropertyControls
{
	/// <summary>
	/// Edit control for selecting from a fixed list of strings
	/// </summary>
	public partial class ComboBoxControl : PropertyEditControl
	{
		public ComboBoxControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			ComboBoxAttribute p = parameters as ComboBoxAttribute;
			cboItems.Items.Clear();
			cboItems.Items.Add("");
			cboItems.Items.AddRange(p.Options);
		}

		protected override void OnBoundData()
		{
			cboItems.SelectedItem = GetValue()?.ToString() ?? "";
		}

		public override void Clear()
		{
			cboItems.SelectedItem = "";
			Save();
		}

		public override void Save()
		{
			string text = cboItems.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			SetValue(text);
		}

		private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class ComboBoxAttribute : EditControlAttribute
	{
		public string[] Options;

		public override Type EditControlType
		{
			get { return typeof(ComboBoxControl); }
		}
	}
}
