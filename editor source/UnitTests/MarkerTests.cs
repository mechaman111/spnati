using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;

namespace UnitTests
{
	[TestClass]
	public class MarkerTests
	{
		[TestMethod]
		public void Raw_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(op);
			Assert.IsNull(value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Raw_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo*", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(op);
			Assert.IsNull(value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Increment_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("+foo", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("1", value);
			Assert.AreEqual("+", op);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Increment_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("+foo*", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("1", value);
			Assert.AreEqual("+", op);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Decrement_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("-foo", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("1", value);
			Assert.AreEqual("-", op);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Decrement_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("-foo*", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("1", value);
			Assert.AreEqual("-", op);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Set_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo=bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("=", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Set_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo*=bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("=", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Add_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo += bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("+", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Add_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo* += bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("+", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Subtract_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo -= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("-", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Subtract_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo* -= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("-", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Multiply_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo *= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("*", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Multiply_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo* *= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("*", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Divide_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo /= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("/", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Divide_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo* /= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("/", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Modulo_Global()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo %= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("%", op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Modulo_Target()
		{
			string marker;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces("foo* %= bar", out value, out perTarget, out op);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("%", op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void No_Condition_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void No_Condition_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_Equals_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo==bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.Equals, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_Equals_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*==bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.Equals, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_NotEqual_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo!=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.NotEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_NotEqual_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*!=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.NotEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_Equals_Target_Variable()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*==~bar~", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.Equals, op);
			Assert.AreEqual("~bar~", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_Less_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo<bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.LessThan, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_Less_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*<bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.LessThan, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_LessEqual_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo<=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.LessThanOrEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_LessEqual_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*<=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.LessThanOrEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_More_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo>bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.GreaterThan, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_More_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*>bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.GreaterThan, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Condition_MoreEqual_Global()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo>=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.GreaterThanOrEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Condition_MoreEqual_Target()
		{
			string marker;
			string value;
			bool perTarget;
			MarkerOperator op;
			marker = Marker.ExtractConditionPieces("foo*>=bar", out op, out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual(MarkerOperator.GreaterThanOrEqual, op);
			Assert.AreEqual("bar", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void MarkerOperation_To_Raw()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			Assert.AreEqual("foo", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_To_Increment()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "+";
			Assert.AreEqual("+foo", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_To_Decrement()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "+";
			Assert.AreEqual("+foo", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Set()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "=";
			marker.Value = "bar";
			Assert.AreEqual("foo=bar", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Add()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "+";
			marker.Value = "5";
			Assert.AreEqual("foo += 5", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Subttract()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "-";
			marker.Value = "5";
			Assert.AreEqual("foo -= 5", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Multiply()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "*";
			marker.Value = "5";
			Assert.AreEqual("foo *= 5", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Divide()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "/";
			marker.Value = "5";
			Assert.AreEqual("foo /= 5", marker.ToString());
		}

		[TestMethod]
		public void MarkerOperation_Modulo()
		{
			MarkerOperation marker = new MarkerOperation();
			marker.Name = "foo";
			marker.Operator = "%";
			marker.Value = "5";
			Assert.AreEqual("foo %= 5", marker.ToString());
		}
	}
}
