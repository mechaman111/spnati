using System.Collections.Generic;
using System.Text;

namespace SPNATI_Character_Editor
{
	public class Intellisense
	{
		public IntellisenseContext GetContext(string text)
		{
			Stack<IntellisenseContext> contexts = new Stack<IntellisenseContext>(); //nested variable tracking
			IntellisenseContext currentContext = new IntellisenseContext(ContextType.None, -1);
			StringBuilder sb = currentContext.Builder;
			contexts.Push(currentContext);

			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				switch (currentContext.Context)
				{
					case ContextType.None:
						if (c == '~')
						{
							currentContext = new IntellisenseContext(ContextType.VariableName, i);
							contexts.Push(currentContext);
							sb = currentContext.Builder;
						}
						break;
					case ContextType.VariableName:
						if (c == '.')
						{
							//possible function beginning
							currentContext.Context = ContextType.FunctionName;
							currentContext.VariableName = sb.ToString();
							sb.Clear();
						}
						else if (c == '~' || (!char.IsLetterOrDigit(c) && c != '_'))
						{
							//End of variable, so go up. We don't need to remember anything from this context
							contexts.Pop();
							currentContext = contexts.Peek();
							currentContext.Builder.Append("~" + sb.ToString() + "~");
							sb = currentContext.Builder;
							break;
						}
						else
						{
							sb.Append(c);
						}
						break;
					case ContextType.FunctionName:
						if (c == '(' && sb.Length > 0)
						{
							//start of a parameter
							currentContext.FunctionName = sb.ToString();
							currentContext.Context = ContextType.Parameter;
							sb.Clear();
						}
						else if (c == '~')
						{
							//parameterless function. This variable is finished, so we don't need to remember anything from its context
							contexts.Pop();
							currentContext = contexts.Peek();
							currentContext.Builder.Append("~" + sb.ToString() + "~");
							sb = currentContext.Builder;
						}
						else if (!char.IsLetterOrDigit(c) && c != '_')
						{
							//not a valid function name, which means this wasn't a valid variable at all
							contexts.Pop();
							currentContext = contexts.Peek();
							currentContext.Builder.Append("~" + sb.ToString() + "~");
							sb = currentContext.Builder;
						}
						else
						{
							sb.Append(c);
						}
						break;
					case ContextType.Parameter:
						if (c == '|')
						{
							//finished a parameter, another one should be coming
							currentContext.ParameterIndex++;
							sb.Clear();
						}
						else if (c == ')')
						{
							//end of all parameters
							currentContext.Context = ContextType.FunctionEnd;
							sb.Clear();
						}
						else if (c == '~')
						{
							//nested variable, so push a new context onto the stack
							currentContext = new IntellisenseContext(ContextType.VariableName, i);
							contexts.Push(currentContext);
							sb = currentContext.Builder;
						}
						else
						{
							sb.Append(c);
						}
						break;
					case ContextType.FunctionEnd:
						//regardless of the character, we're at the end of the function or have invalid syntax for one, so we can just reset
						contexts.Pop();
						currentContext = contexts.Peek();
						sb = currentContext.Builder;
						break;
				}
			}

			currentContext.FinalizeContext();

			return currentContext;
		}
	}

	public class IntellisenseContext
	{
		/// <summary>
		/// Variable the context is for
		/// </summary>
		public string VariableName;
		/// <summary>
		/// Variable function being used in the function
		/// </summary>
		public string FunctionName;
		/// <summary>
		/// Index of the function's parameter
		/// </summary>
		public int ParameterIndex;
		/// <summary>
		/// Whether this is for a variable, a function, or a parameter
		/// </summary>
		public ContextType Context;
		public StringBuilder Builder = new StringBuilder();
		/// <summary>
		/// Where in the string this variable started
		/// </summary>
		public int StartIndex;

		public IntellisenseContext(ContextType type, int index)
		{
			Context = type;
			StartIndex = index;
		}

		public override string ToString()
		{
			string result = VariableName;
			if (!string.IsNullOrEmpty(FunctionName))
			{
				result += "." + FunctionName;
			}
			if (Context == ContextType.Parameter)
			{
				result += $"({ParameterIndex})";
			}
			return result;
		}

		internal void FinalizeContext()
		{
			switch (Context)
			{
				case ContextType.VariableName:
					VariableName = Builder.ToString();
					break;
				case ContextType.FunctionName:
					FunctionName = Builder.ToString();
					break;
				case ContextType.FunctionEnd:
					Context = ContextType.VariableName;
					break;
			}
		}
	}

	/// <summary>
	/// What part of a variable is currently being read character by character where the variable comes in the form ~variable~ or ~variable.function(parameter|parameter|...)~ and parameter is any non-| or -) characters including nested variables
	/// </summary>
	public enum ContextType
	{
		/// <summary>
		/// Not reading anything variable-related currently
		/// </summary>
		None,
		/// <summary>
		/// Reading a variable name
		/// </summary>
		VariableName,
		/// <summary>
		/// Reading a function name
		/// </summary>
		FunctionName,
		/// <summary>
		/// Reading a parameter, which could include nested variables
		/// </summary>
		Parameter,
		/// <summary>
		/// Next character should be a ~ or it was all invalid
		/// </summary>
		FunctionEnd,
	}
}
