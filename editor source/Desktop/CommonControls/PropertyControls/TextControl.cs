using System;
using System.Windows.Forms;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class TextControl : PropertyEditControl
	{
		public TextControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			TextAttribute attrib = parameters as TextAttribute;
			txtValue.Multiline = attrib.Multiline;
		}

		protected override void OnBoundData()
		{
			IAutoCompleteList autoComplete = Context as IAutoCompleteList;
			if (autoComplete != null)
			{
				AutoCompleteStringCollection list = new AutoCompleteStringCollection();
				txtValue.AutoCompleteCustomSource = list;
				txtValue.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				txtValue.AutoCompleteSource = AutoCompleteSource.CustomSource;

				string[] items = autoComplete.GetAutoCompleteList(Data);
				if (items != null)
				{
					list.AddRange(items);
				}
			}
			txtValue.Text = GetValue()?.ToString();
		}

		public override void Clear()
		{
			txtValue.Text = "";
			Save();
		}

		public override void Save()
		{
			SetValue(txtValue.Text);
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			Save();
		}
	}

	public class TextAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(TextControl); }
		}

		public bool Multiline;
	}

	public interface IAutoCompleteList
	{
		string[] GetAutoCompleteList(object data);
	}
}
