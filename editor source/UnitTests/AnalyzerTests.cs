using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.Analyzers;
using System;
using System.Collections.Generic;

namespace UnitTests
{
	[TestClass]
	public class AnalyzerTests : IDisposable
	{
		private Character _character = new Character();

		[TestMethod]
		public void Criteria_One_Is_True()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);

			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_One_Is_False()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, false);

			Assert.IsFalse(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_One_Is_True_Not()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);

			report.Expression = "NOT";
			Assert.IsTrue(!report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_One_Is_False_Not()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, false);

			report.Expression = "NOT";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Two_And_Succeed()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);

			report.Expression = "AND";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Three_And_Succeed()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);

			report.Expression = "AND";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Three_And_Fail()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, false);
			AddTestCriterion(report, true);

			report.Expression = "AND";
			Assert.IsTrue(!report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Or_Succeed()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			AddTestCriterion(report, false);

			report.Expression = "OR";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Or_Fail()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, false);
			AddTestCriterion(report, false);
			AddTestCriterion(report, false);

			report.Expression = "OR";
			Assert.IsTrue(!report.MeetsCriteria(_character));
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Criteria_Bad_Criterion()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			report.Expression = "5";
			report.MeetsCriteria(_character);
		}

		[TestMethod]
		public void Criteria_Three_And_Numbered()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			AddTestCriterion(report, true);

			report.Expression = "1 AND 2 AND 3";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Three_Or_Numbered()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			AddTestCriterion(report, true);
			AddTestCriterion(report, false);
			AddTestCriterion(report, true);

			report.Expression = "1 OR 2 OR 3";
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Custom1()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			TestAnalyzer test1 = AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			TestAnalyzer test3 = AddTestCriterion(report, false);

			report.Expression = "1 AND 2 OR 3"; //TRUE AND TRUE OR FALSE
			Assert.IsTrue(report.MeetsCriteria(_character));

			test1.Met = false; //FALSE AND TRUE OR FALSE
			Assert.IsFalse(report.MeetsCriteria(_character));

			test3.Met = true; //FALSE AND TRUE OR TRUE
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Custom2()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			TestAnalyzer test1 = AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			TestAnalyzer test3 = AddTestCriterion(report, false);

			report.Expression = "1 OR 2 AND 3"; //TRUE OR TRUE AND FALSE
			Assert.IsTrue(report.MeetsCriteria(_character));

			test1.Met = false; //FALSE OR TRUE AND TRUE
			test3.Met = true;
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Custom3()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			TestAnalyzer test1 = AddTestCriterion(report, true);
			AddTestCriterion(report, true);
			TestAnalyzer test3 = AddTestCriterion(report, true);

			report.Expression = "1 OR 2 AND NOT 3"; //T OR (T AND ~T)
			Assert.IsTrue(report.MeetsCriteria(_character));

			test1.Met = false; //F OR (T AND ~T)
			Assert.IsFalse(report.MeetsCriteria(_character));

			test3.Met = false; //F OR (T AND ~F)
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		[TestMethod]
		public void Criteria_Custom4()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			report.Expression = "1 AND NOT 2 AND 3";
			TestAnalyzer test1 = AddTestCriterion(report, true);
			TestAnalyzer test2 = AddTestCriterion(report, true);
			AddTestCriterion(report, true);

			//(T AND ~T) AND T
			Assert.IsFalse(report.MeetsCriteria(_character));

			test1.Met = false; //(F AND ~T) AND T
			Assert.IsFalse(report.MeetsCriteria(_character));

			test1.Met = true;
			test2.Met = false; //(T AND ~F) AND T
			Assert.IsTrue(report.MeetsCriteria(_character));
		}


		[TestMethod]
		public void Criteria_Complicated()
		{
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			report.Expression = "1 AND NOT (2 OR (3 AND 1)) AND NOT NOT 4";
			TestAnalyzer test1 = AddTestCriterion(report, true);
			TestAnalyzer test2 = AddTestCriterion(report, true);
			TestAnalyzer test3 = AddTestCriterion(report, true);
			AddTestCriterion(report, true);

			//1 AND ~(2 OR (3 AND 1)) AND ~~1
			//T AND ~(T OR (T AND T)) AND ~~T
			Assert.IsFalse(report.MeetsCriteria(_character));

			//F AND ~(T OR (T AND F)) AND ~~T
			test1.Met = false; //(F AND ~T) AND T
			Assert.IsFalse(report.MeetsCriteria(_character));

			//T AND ~(F OR (F AND T)) AND ~~T
			test1.Met = true;
			test2.Met = false;
			test3.Met = false;
			Assert.IsTrue(report.MeetsCriteria(_character));
		}

		private TestAnalyzer AddTestCriterion(AnalyzerReportCriteria report, bool isMet)
		{
			DataCriterion criterion = new DataCriterion();
			TestAnalyzer test = new TestAnalyzer(isMet);
			criterion.Analyzer = test;
			report.Criteria.Add(criterion);
			return test;
		}

		public void Dispose()
		{
			_character.Dispose();
		}
	}

	public class TestAnalyzer : IDataAnalyzer
	{
		public bool Met;

		public TestAnalyzer(bool met)
		{
			Met = met;
		}

		public string Key
		{
			get
			{
				return "";
			}
		}

		public string Name
		{
			get
			{
				return "";
			}
		}

		public string FullName
		{
			get
			{
				return "";
			}
		}

		public string ParentKey
		{
			get
			{
				return "";
			}
		}

		public string[] GetValues()
		{
			throw new NotImplementedException();
		}

		public Type GetValueType()
		{
			throw new NotImplementedException();
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			return Met;
		}

		public override string ToString()
		{
			return Met ? "T" : "F";
		}
	}
}
