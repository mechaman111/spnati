using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class TextControl : PropertyEditControl
	{
		private string _validatorMethodName;
		private string _formatterMethodName;

		public TextControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			TextAttribute attrib = parameters as TextAttribute;
			txtValue.Multiline = attrib.Multiline;
			_validatorMethodName = attrib.Validator;
			_formatterMethodName = attrib.Formatter;
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				txtValue.Text = values[0];
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(txtValue.Text);
		}

		protected override void AddHandlers()
		{
			txtValue.TextChanged += txtValue_TextChanged;
			txtValue.Validating += TxtValue_Validating;
		}

		protected override void RemoveHandlers()
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
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			txtValue.Text = "";
			AddHandlers();
			Save();
		}

		protected override void OnSave()
		{
			string text = txtValue.Text;

			if (!string.IsNullOrEmpty(_formatterMethodName))
			{
				MethodInfo mi = Data.GetType().GetMethod(_formatterMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (mi != null)
				{
					Func<string, string> formatter = (Func<string, string>)Delegate.CreateDelegate(typeof(Func<string, string>), Data, mi);
					text = formatter(text);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				SetValue(null);
			}
			else
			{
				SetValue(text);
			}
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
		public string Formatter;
	}

	public interface IAutoCompleteList
	{
		string[] GetAutoCompleteList(object data);
	}
}
