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
	public class MarkerTests
	{
		[TestMethod]
		public void Raw_Global()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("foo", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Raw_Target()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("foo*", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.IsNull(value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Increment_Global()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("+foo", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("+", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Increment_Target()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("+foo*", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("+", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Decrement_Global()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("-foo", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("-", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Decrement_Target()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("-foo*", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("-", value);
			Assert.IsTrue(perTarget);
		}

		[TestMethod]
		public void Set_Global()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("foo=bar", out value, out perTarget);
			Assert.AreEqual("foo", marker);
			Assert.AreEqual("bar", value);
			Assert.IsFalse(perTarget);
		}

		[TestMethod]
		public void Set_Target()
		{
			string marker;
			string value;
			bool perTarget;
			marker = Marker.ExtractPieces("foo*=bar", out value, out perTarget);
			Assert.AreEqual("foo", marker);
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
	}
}
