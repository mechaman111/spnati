using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	[TestClass]
	public class IntellisenseTests
	{
		private Intellisense _intellisense;

		[TestInitialize]
		public void Init()
		{
			_intellisense = new Intellisense();
		}

		[TestMethod]
		public void Variable_Empty()
		{
			IntellisenseContext context = _intellisense.GetContext("");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_None_OneWord()
		{
			IntellisenseContext context = _intellisense.GetContext("word");
			Assert.AreEqual(ContextType.None, context.Context);

			context = _intellisense.GetContext("multiple words");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_None_MultiWord()
		{
			IntellisenseContext context = _intellisense.GetContext("multiple words");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_Isolated()
		{
			IntellisenseContext context = _intellisense.GetContext("~variable~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_Empty()
		{
			IntellisenseContext context = _intellisense.GetContext("~~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_End()
		{
			IntellisenseContext context = _intellisense.GetContext("end ~variable~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_Start()
		{
			IntellisenseContext context = _intellisense.GetContext("~variable~ start");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_Middle()
		{
			IntellisenseContext context = _intellisense.GetContext("a ~variable~ b");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Closed_Multiple()
		{
			IntellisenseContext context = _intellisense.GetContext("here ~are~ to ~variables~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Syntax_Tilde()
		{
			IntellisenseContext context = _intellisense.GetContext("~ space");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Syntax_NonAlpha()
		{
			IntellisenseContext context = _intellisense.GetContext("~not!alpha");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Variable_Open_Isolated()
		{
			IntellisenseContext context = _intellisense.GetContext("~");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("", context.VariableName);
		}

		[TestMethod]
		public void Variable_Open_Start()
		{
			IntellisenseContext context = _intellisense.GetContext("~start");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("start", context.VariableName);
		}

		[TestMethod]
		public void Variable_Open_Middle()
		{
			IntellisenseContext context = _intellisense.GetContext("at ~middle");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("middle", context.VariableName);
		}

		[TestMethod]
		public void Variable_Open_End()
		{
			IntellisenseContext context = _intellisense.GetContext("at ~");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("", context.VariableName);
		}

		[TestMethod]
		public void Variable_Open_And_Closed()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed~ ~open");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Variable_Open_Running_Syntax()
		{
			IntellisenseContext context = _intellisense.GetContext("~bad!~real");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("real", context.VariableName);
		}

		[TestMethod]
		public void Function_Closed_Isolated()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now()~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Parameterless()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Start()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now()~ start");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_End()
		{
			IntellisenseContext context = _intellisense.GetContext("end ~closed.now()~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Parameter()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now(param)~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Parameters()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now(param|param2)~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Whitespace()
		{
			IntellisenseContext context = _intellisense.GetContext("~closed.now(most any character,^isokay:here)~");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Closed_Combined()
		{
			IntellisenseContext context = _intellisense.GetContext("~var~notvar");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Open_Ended()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.now()");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Function_Syntax_Missing_Tilde()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.now() ");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Syntax_NonAlpha()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.n!w");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Syntax_NoName()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.(");
			Assert.AreEqual(ContextType.None, context.Context);
		}

		[TestMethod]
		public void Function_Open()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good");
			Assert.AreEqual(ContextType.FunctionName, context.Context);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Function_Open_No_Tilde()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.now()");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("now", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}


		[TestMethod]
		public void Parameter_First_Empty()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_First()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_First_Whitespace()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param with some whitespace");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second_Empty()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param|");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(1, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second_First_Empty()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(|");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(1, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second_First_Punctuation()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(not.function");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1|param2");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(1, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second_Whitespace()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1 with space|param2 with space");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(1, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Second_NonAlpha()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(par$@%@am1!|param2*with&junk");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(1, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Third()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1|param2|param3");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(2, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Closed()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1|param2)");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1 ~nested");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("nested", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested_Two()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1 ~nested1~ ~nested2");
			Assert.AreEqual(ContextType.VariableName, context.Context);
			Assert.AreEqual("nested2", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested_Syntax()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(param1 ~nest!ed");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("good", context.FunctionName);
			Assert.AreEqual("open", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested_Function()
		{
			IntellisenseContext context = _intellisense.GetContext("~open.good(~var.func");
			Assert.AreEqual(ContextType.FunctionName, context.Context);
			Assert.AreEqual("func", context.FunctionName);
			Assert.AreEqual("var", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested_Function_Parameter()
		{
			IntellisenseContext context = _intellisense.GetContext("param1|~open.good(~var.func(param");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("func", context.FunctionName);
			Assert.AreEqual("var", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Nested_Nested_Function()
		{
			IntellisenseContext context = _intellisense.GetContext("~var2.func1(~var2.func2(~var3.func3");
			Assert.AreEqual(ContextType.FunctionName, context.Context);
			Assert.AreEqual("func3", context.FunctionName);
			Assert.AreEqual("var3", context.VariableName);
		}

		[TestMethod]
		public void Parameter_Complex_Nesting()
		{
			IntellisenseContext context = _intellisense.GetContext("~var.func(a|b|c~var2.func1(~var2.func2(~var3.func3|");
			Assert.AreEqual(ContextType.Parameter, context.Context);
			Assert.AreEqual(0, context.ParameterIndex);
			Assert.AreEqual("func2", context.FunctionName);
			Assert.AreEqual("var2", context.VariableName);
		}
	}
}
