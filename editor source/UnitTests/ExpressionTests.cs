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
	public class ExpressionTests
	{
		private static Character _female;
		private static Character _male;

		[TestInitialize]
		public void Init()
		{
			TriggerDatabase.Load();
			AddVariableFunction("costume");
			AddVariableFunction("marker");
			_female = new Character
			{
				Gender = "female",
				FolderName = "female"
			};
			_male = new Character
			{
				Gender = "male",
				FolderName = "male"
			};
			CharacterDatabase.Add(_female);
			CharacterDatabase.Add(_male);
		}

		[TestCleanup]
		public void CleanUp()
		{
			TriggerDatabase.Clear();
			VariableDatabase.Clear();
			CharacterDatabase.Clear();
		}

		public static void AddVariableFunction(string name)
		{
			AddVariableFunction("target", name);
			AddVariableFunction("player", name);
			AddVariableFunction("self", name);
		}

		public static void AddVariableFunction(string variable, string name)
		{
			Variable v = VariableDatabase.Get(variable);
			bool flag = v == null;
			if (flag)
			{
				v = new Variable();
				v.Name = variable;
				VariableDatabase.Add(v);
			}
			v.Functions.Add(new VariableFunction
			{
				Name = name
			});
		}

		[TestMethod]
		public void SelfTarget()
		{
			ExpressionTest test = new ExpressionTest("self.costume", "blah");
			Assert.AreEqual<string>("self", test.GetTarget());
		}

		[TestMethod]
		public void NamedTarget()
		{
			ExpressionTest test = new ExpressionTest("male.costume", "blah");
			Assert.AreEqual<string>("male", test.GetTarget());
		}

		[TestMethod]
		public void MultiLevelTarget()
		{
			ExpressionTest test = new ExpressionTest("male.marker.test", "blah");
			Assert.AreEqual<string>("male", test.GetTarget());
		}

		[TestMethod]
		public void SelfLessTarget()
		{
			ExpressionTest test = new ExpressionTest("costume", "blah");
			Assert.AreEqual<string>("self", test.GetTarget());
		}

		[TestMethod]
		public void SelfLessMultiLevelTarget()
		{
			ExpressionTest test = new ExpressionTest("marker.test", "blah");
			Assert.AreEqual<string>("self", test.GetTarget());
		}

		[TestMethod]
		public void RefersToSelf()
		{
			ExpressionTest test = new ExpressionTest("self.costume", "blah");
			Assert.IsTrue(test.RefersTo(_male, _male, null));
		}

		[TestMethod]
		public void RefersToOtherSelf()
		{
			ExpressionTest test = new ExpressionTest("self.costume", "blah");
			Assert.IsFalse(test.RefersTo(_female, _male, null));
		}

		[TestMethod]
		public void RefersToOther()
		{
			ExpressionTest test = new ExpressionTest("male.costume", "blah");
			Assert.IsTrue(test.RefersTo(_male, _female, null));
		}

		[TestMethod]
		public void RefersToTarget()
		{
			ExpressionTest test = new ExpressionTest("target.costume", "blah");
			Assert.IsTrue(test.RefersTo(_female, _male, "female"));
		}

		[TestMethod]
		public void RefersToOtherTarget()
		{
			ExpressionTest test = new ExpressionTest("target.costume", "blah");
			Assert.IsFalse(test.RefersTo(_female, _male, "male"));
		}

		[TestMethod]
		public void RefersToNamed()
		{
			ExpressionTest test = new ExpressionTest("male.costume", "blah");
			Assert.IsTrue(test.RefersTo(_male, _male, null));
		}
	}
}
