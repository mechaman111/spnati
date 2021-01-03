using Desktop.Skinning;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Controls.VariableControls;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class VariableWizard : SkinnedForm
	{
		private Line _line;

		public VariableWizard()
		{
			InitializeComponent();

			recVariable.RecordType = typeof(Variable);
			recFunction.RecordType = typeof(VariableFunction);
		}

		public void SetData(Case dialogueCase, Line line)
		{
			recVariable.RecordContext = new VariableContext(dialogueCase, line);

			_line = line;
			int index = _line.Position;
			if (index < 0)
			{
				index = 0;
			}
			string replaceText = _line.Text.Substring(0, index) + "^" + _line.Text.Substring(index);
			if (_line.Position >= 0)
			{
				//see if we're already in a variable and prefill with that if so
				//this is a simplistic implementation that doesn't handle nested variables well
				int previous = _line.Text.Substring(0, index).LastIndexOf('~');
				if (previous >= 0 && previous <= index)
				{
					int next = _line.Text.IndexOf('~', previous == index ? index + 1 : previous + 1);
					string full = next > previous ? _line.Text.Substring(previous + 1, next - previous - 1) : _line.Text.Substring(previous + 1);
					int dot = full.IndexOf('.');
					int open = full.IndexOf('(');
					string name = full;
					if (dot >= 0 || open >= 0)
					{
						int nameEnd = dot;
						if ((open >= 0 && open < dot) || dot == -1)
						{
							nameEnd = open;
						}
						name = full.Substring(0, nameEnd);
					}
					Variable v = VariableDatabase.Get(name, false);
					if (v != null)
					{
						recVariable.Record = v;

						//check for a function
						if (dot >= 0)
						{
							string func = full.Substring(dot + 1);
							if (open >= 0 && open > dot)
							{
								func = full.Substring(dot + 1, open - dot - 1);
							}

							VariableFunction f = v.Functions.Find(fn => fn.Name == func);
							if (f != null)
							{
								recFunction.Record = f;
							}
						}

						//check for parameters
						if (open >= 0 && panelParameters.Controls.Count > 0)
						{
							string paramText = full.Substring(open + 1);
							int close = paramText.IndexOf(')');
							if (close >= 0)
							{
								paramText = paramText.Substring(0, close);
							}
							string[] pieces = paramText.Split(new char[] { '|' }, panelParameters.Controls.Count);
							for (int i = 0; i < pieces.Length; i++)
							{
								ParameterField field = panelParameters.Controls[i] as ParameterField;
								field.Text = pieces[i];
							}
						}
						replaceText = _line.Text.ReplaceAt(previous + 1, full, "^").Replace("~^~", "^");
						if (replaceText == "~^")
						{
							replaceText = "^";
						}
					}
				}
			}
			txtLine.Text = replaceText;
		}

		private void recVariable_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Variable variable = recVariable.Record as Variable;
			recFunction.Record = null;
			lblParams.Visible = lblFunction.Visible = recFunction.Visible = variable != null && variable.HasFunctions();
			recFunction.RecordContext = variable;
			UpdateParameters();
			UpdateExamples();
		}

		private void recFunction_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			VariableFunction func = recFunction.Record as VariableFunction;
			UpdateParameters();
			UpdateExamples();
		}

		private void txtLine_TextChanged(object sender, System.EventArgs e)
		{
			UpdateExamples();
		}

		private void UpdateParameters()
		{
			foreach (Control ctl in panelParameters.Controls)
			{
				ParameterField field = ctl as ParameterField;
				if (field != null)
				{
					field.TextChanged -= Field_TextChanged;
				}
			}
			panelParameters.Controls.Clear();
			Variable v = recVariable.Record as Variable;
			VariableFunction func = recFunction.Record as VariableFunction;
			List<VariableParameter> parameters = func != null && func.Parameters != null && func.Parameters.Count > 0 ? func.Parameters :
				v?.Parameters;

			lblParams.Visible = parameters != null && parameters.Count > 0;
			if (parameters == null) { return; }

			for (int i = 0; i < parameters.Count; i++)
			{
				VariableParameter param = parameters[i];
				ParameterField field = new ParameterField();
				field.Label = param.Label ?? param.Name;
				field.Text = "";
				field.TextChanged += Field_TextChanged;
				field.Margin = new Padding(3);
				field.Tag = param;
				field.TabIndex = txtLine.TabIndex - (1 + i);
				panelParameters.Controls.Add(field);
				field.Dock = DockStyle.Top;
			}
		}

		public string LineText
		{
			get
			{
				string text = txtLine.Text;
				Variable v = recVariable.Record as Variable;
				if (v == null)
				{
					return text.Replace("^", "");
				}
				VariableFunction func = recFunction.Record as VariableFunction;
				StringBuilder sb = new StringBuilder();
				sb.Append($"~{v.Name}");
				if (func != null)
				{
					sb.Append($".{func.Name}");
				}
				if (panelParameters.Controls.Count > 0)
				{
					sb.Append("(");
					for (int i = 0; i < panelParameters.Controls.Count; i++)
					{
						if (i > 0)
						{
							sb.Append("|");
						}
						ParameterField field = panelParameters.Controls[i] as ParameterField;
						sb.Append(field.Text);
					}
					sb.Append(")");
				}
				sb.Append("~");
				string variable = sb.ToString();
				return text.Replace("^", variable);
			}
		}

		private void Field_TextChanged(object sender, System.EventArgs e)
		{
			UpdateExamples();
		}

		private void UpdateExamples()
		{
			panelExamples.Controls.Clear();

			Variable v = recVariable.Record as Variable;
			string text = txtLine.Text;
			int index = text.IndexOf("^");
			if (index == -1)
			{
				text = "Line is missing \"^\". Add ^ where you wish for the variable to be inserted.";
				AddExample(text);
				return;
			}

			if (v == null)
			{
				AddExample(text);
				return;
			}
			VariableFunction func = recFunction.Record as VariableFunction;

			if (func == null)
			{
				if (v.Parameters != null && v.Parameters.Count > 0)
				{
					foreach (Control ctl in panelParameters.Controls)
					{
						ParameterField field = ctl as ParameterField;
						if (field != null)
						{
							VariableParameter param = field.Tag as VariableParameter;
							string line = text.Replace("^", param.Example ?? field.Text);
							AddExample(line);
						}
					}
				}
				else
				{
					string expanded = string.IsNullOrEmpty(v.Example) ? $"~{v.Name}~" : v.Example;
					AddExample(text.Replace("^", expanded));
				}
			}
			else
			{
				if (func.Parameters == null || func.Parameters.Count == 0)
				{
					string expanded = string.IsNullOrEmpty(func.Example) ? $"~{v.Name}.{func.Name}~" : func.Example;
					AddExample(text.Replace("^", expanded));
				}
				else
				{
					foreach (Control ctl in panelParameters.Controls)
					{
						ParameterField field = ctl as ParameterField;
						if (field != null)
						{
							VariableParameter param = field.Tag as VariableParameter;
							string line = text.Replace("^", field.Text);
							if (!string.IsNullOrEmpty(param.Example))
							{
								line = line.Replace($"~{v.Name}~", param.Example);
							}
							AddExample(line);
						}
					}
				}
			}
		}

		private void AddExample(string text)
		{
			SkinnedLabel label = new SkinnedLabel();
			label.Text = text;
			panelExamples.Controls.Add(label);
			label.Dock = DockStyle.Top;
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
