using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class IntellisenseControl : UserControl
	{
		[DllImport("user32")]
		private extern static int GetCaretPos(out Point p);

		private Character _character;
		private Case _case;
		private TextBox _textBox;

		private Intellisense _intellisense = new Intellisense();
		private IntellisenseContext _lastContext = new IntellisenseContext(ContextType.None, -1);

		private List<string> _availableVars = new List<string>();

		public event EventHandler<InsertEventArgs> InsertSnippet;

		public IntellisenseControl()
		{
			InitializeComponent();

			VisibleChanged += Intellisense_VisibleChanged;
		}

		private void Intellisense_VisibleChanged(object sender, EventArgs e)
		{
			if (!Visible)
			{
				lblTooltip.Text = "";
			}
		}

		private void WireEvents()
		{
			_textBox.KeyUp += _textBox_KeyUp;
		}

		private void UnwireEvents()
		{
			if (_textBox != null)
			{
				_textBox.KeyUp -= _textBox_KeyUp;
				_textBox = null;
			}
		}

		private bool IsNavigationKey(Keys code)
		{
			switch (code)
			{
				case Keys.Shift:
				case Keys.ShiftKey:
				case Keys.Control:
				case Keys.ControlKey:
				case Keys.Alt:
				case Keys.Left:
				case Keys.Right:
				case Keys.Up:
				case Keys.Down:
					return true;
				default: return false;
			}
		}

		private void _textBox_KeyUp(object sender, KeyEventArgs e)
		{
			UpdateIntellisense(e.KeyCode);
		}

		private void UpdateIntellisense(Keys keyCode)
		{
			if (_textBox.SelectionStart == 0 || IsNavigationKey(keyCode)) { return; }

			if (_lastContext.Context != ContextType.None && keyCode != Keys.Back && keyCode != Keys.Delete && keyCode != Keys.None)
			{
				char lastChar = _textBox.Text[_textBox.SelectionStart - 1];
				switch (_lastContext.Context)
				{
					case ContextType.VariableName:
						if (lastChar == ',' || lastChar == '?' || lastChar == ';' || lastChar == '!' ||
							lastChar == ' ' || lastChar == '~' || lastChar == '.' || lastChar == ',')
						{
							AutoComplete(lastChar);
							if (lastChar != '.')
							{
								return;
							}
						}
						break;
					case ContextType.FunctionName:
						if (lastChar == '(' || lastChar == '~')
						{
							AutoComplete(lastChar);
						}
						break;
					case ContextType.Parameter:
						if (lastChar == ')')
						{
							AutoComplete(lastChar);
						}
						break;
				}
			}

			string text = _textBox.Text;
			string startText = text.Substring(0, _textBox.SelectionStart);
			_lastContext = _intellisense.GetContext(startText);
			switch (_lastContext.Context)
			{
				case ContextType.None:
					Hide();
					break;
				case ContextType.VariableName:
					UpdateVariableList(_lastContext.VariableName);
					break;
				case ContextType.FunctionName:
					UpdateFunctionList(_lastContext.FunctionName);
					DisplayTooltip();
					break;
				case ContextType.Parameter:
					UpdateFunctionList(_lastContext.FunctionName);
					DisplayTooltip();
					break;
			}
		}

		/// <summary>
		/// Inserts whatever's in the context into the textbox
		/// </summary>
		/// <param name="key">Key that was pressed to trigger auto-completion</param>
		private void AutoComplete(char endingChar)
		{
			switch (_lastContext.Context)
			{
				case ContextType.None:
				case ContextType.FunctionEnd:
					return;
			}

			int insertion = _lastContext.StartIndex;
			string value = lstItems.SelectedItem?.ToString();

			string insertionText = "";
			int selectionLength = 0;

			if (!string.IsNullOrEmpty(value))
			{
				switch (_lastContext.Context)
				{
					case ContextType.VariableName:
						Variable variable = VariableDatabase.Get(value);
						string text = _lastContext.VariableName;
						insertion += text.Length + 1; //1 = starting ~
						insertionText = variable.Name.Substring(text.Length, variable.Name.Length - text.Length);
						if (endingChar != '~' && (!variable.HasFunctions() || endingChar != '.'))
						{
							insertionText += '~';
						}
						if (endingChar != '\0')
						{
							insertionText += endingChar;
							selectionLength++;
						}
						else
						{
							endingChar = '~';
						}
						_lastContext.Context = ContextType.None;
						InsertSnippet?.Invoke(this, new InsertEventArgs(insertionText, insertion, selectionLength));
						UpdateIntellisense(Keys.None);
						break;
					case ContextType.FunctionName:
						text = _lastContext.FunctionName;
						string varName = _lastContext.VariableName;
						insertion += varName.Length + 2; //2 = starting ~ + function .
						selectionLength += text.Length; //replace the text to ensure proper case
						insertionText = value;
						if (endingChar == '\0')
						{
							insertionText += '(';
							endingChar = '(';
						}
						else
						{
							insertionText += endingChar;
							selectionLength++;
						}
						InsertSnippet?.Invoke(this, new InsertEventArgs(insertionText, insertion, selectionLength));
						UpdateIntellisense(Keys.None);
						break;
					case ContextType.Parameter:
						insertionText = ")~";
						if (endingChar == '\0')
						{
							selectionLength = 0;
							insertion = _textBox.SelectionStart;
							endingChar = '~';
						}
						else
						{
							selectionLength = 1;
							insertion = _textBox.SelectionStart - 1;
						}
						InsertSnippet?.Invoke(this, new InsertEventArgs(insertionText, insertion, selectionLength));
						UpdateIntellisense(Keys.None);
						break;
				}			
			}
		}

		public void SetContext(TextBox editBox, Character character, Case stageCase)
		{
			UnwireEvents();
			_textBox = editBox;
			_lastContext.Context = ContextType.None;
			WireEvents();
			Reset();
			_character = character;
			_case = stageCase;
			_availableVars.Clear();
			Trigger trigger = TriggerDatabase.GetTrigger(_case.Tag);
			if (trigger != null)
			{
				_availableVars.AddRange(trigger.AvailableVariables);
			}
			foreach (Variable v in VariableDatabase.GlobalVariables)
			{
				_availableVars.Add(v.Name);
			}
			_availableVars.Sort();
		}

		private void Display()
		{
			if (!Visible)
			{
				Point cp;
				GetCaretPos(out cp);
				SetBounds(cp.X + _textBox.Parent.Left, cp.Y + _textBox.Parent.Top + 20, Width, Height);
				Show();
				BringToFront();
			}
		}

		public void Reset()
		{
			if (!Visible) { return; }
			Hide();
			_availableVars.Clear();
		}

		private void UpdateVariableList(string variable)
		{
			lstItems.DataSource = null;
			List<string> options = _availableVars
				.Where(var => string.IsNullOrEmpty(variable) || var.ToLower().StartsWith(variable.ToLower()))
				.Select(var => var).ToList();

			if (options.Count > 0)
			{
				lstItems.DataSource = options;
				Display();
			}
			else
			{
				Hide();
			}
		}

		private void UpdateFunctionList(string function)
		{
			if (_lastContext.Context != ContextType.FunctionName && _lastContext.Context != ContextType.Parameter) { return; }
			lstItems.DataSource = null;
			Variable variable = VariableDatabase.Get(_lastContext.VariableName);
			if (variable == null || !variable.HasFunctions())
			{
				return;
			}

			List<string> options = variable.GetFunctions(_character)
				.Where(f => string.IsNullOrEmpty(function) || f.Name.ToLower().StartsWith(function.ToLower()))
				.Select(f => f.Name).ToList();

			if (options.Count > 0)
			{
				lstItems.DataSource = options;
				Display();
			}
			else
			{
				Hide();
			}
		}

		private void DisplayTooltip()
		{
			string value = lstItems.SelectedItem?.ToString();
			if (_lastContext.Context == ContextType.None || string.IsNullOrEmpty(value))
			{
				lblTooltip.Text = "";
				return;
			}

			switch (_lastContext.Context)
			{
				case ContextType.VariableName:
					Variable v = VariableDatabase.Get(value);
					if (v != null)
					{
						lblTooltip.Text = v.Description;
						lblTooltip.Links.Clear();
					}
					break;
				case ContextType.FunctionName:
					v = VariableDatabase.Get(_lastContext.VariableName);
					if (v != null)
					{
						VariableFunction func = v.GetFunction(_character, value);
						if (func != null)
						{
							lblTooltip.Text = func.Description;
						}
						else
						{
							lblTooltip.Text = v.Description;
						}
						lblTooltip.Links.Clear();
					}
					break;
				case ContextType.Parameter:
					v = VariableDatabase.Get(_lastContext.VariableName);
					if (v != null)
					{
						VariableFunction func = v.GetFunction(_character, value);
						if (func != null)
						{
							string description = "";
							lblTooltip.Text = $"{_lastContext.VariableName}.{_lastContext.FunctionName}(";
							lblTooltip.Links.Clear();
							for (int i = 0; i < func.Parameters.Count; i++)
							{
								VariableParameter parameter = func.Parameters[i];
								int start = lblTooltip.Text.Length;
								lblTooltip.Text += parameter.Name;
								if (_lastContext.ParameterIndex == i)
								{
									lblTooltip.Links.Add(start, parameter.Name.Length);
									description = parameter.Description;
								}
								if (i < func.Parameters.Count - 1)
								{
									lblTooltip.Text += " | ";
								}
							}
							lblTooltip.Text += ") - " + description;
						}
					}
					break;
			}
		}

		private void lstItems_DoubleClick(object sender, EventArgs e)
		{
			AutoComplete('\0');
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			DisplayTooltip();
		}
	}

	public struct InsertEventArgs
	{
		public string Text { get; set; }
		public int InsertionStart { get; set; }
		public int InsertionLength { get; set; }

		public InsertEventArgs(string text, int start, int length)
		{
			Text = text;
			InsertionStart = start;
			InsertionLength = length;
		}
	}
}
