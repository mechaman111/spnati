using System;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class TextControl : PropertyEditControl
	{
		private string _validatorMethodName;

		public TextControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			TextAttribute attrib = parameters as TextAttribute;
			txtValue.Multiline = attrib.Multiline;
			_validatorMethodName = attrib.Validator;
		}

		private void AddHandlers()
		{
			txtValue.TextChanged += txtValue_TextChanged;
			txtValue.Validating += TxtValue_Validating;
		}

		private void RemoveHandlers()
		{
			txtValue.TextChanged -= txtValue_TextChanged;
			txtValue.Validating -= TxtValue_Validating;
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
			AddHandlers();
		}

		public override void Clear()
		{
			RemoveHandlers();
			txtValue.Text = "";
			AddHandlers();
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

		private void TxtValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!string.IsNullOrEmpty(_validatorMethodName))
			{
				MethodInfo mi = Data.GetType().GetMethod(_validatorMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (mi != null)
				{
					Func<string, object, string> filter = (Func<string, object, string>)Delegate.CreateDelegate(typeof(Func<string, object, string>), Data, mi);
					string msg = filter(txtValue.Text, Context);
					if (!string.IsNullOrEmpty(msg))
					{
						e.Cancel = true;
						error.SetError(txtValue, msg);
					}
					else
					{
						error.SetError(txtValue, "");
					}
				}
			}
		}
	}

	public class TextAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(TextControl); }
		}

		public bool Multiline;
		public string Validator;
	}

	public interface IAutoCompleteList
	{
		string[] GetAutoCompleteList(object data);
	}
}
