using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class BehaviorTests : IDisposable
	{
		private Character _character;

		[TestInitialize]
		public void Init()
		{
			TriggerDatabase.FakeUsedInStage = UsedInStage;

			_character = new Character();
			for (int i = 0; i < 5; i++)
			{
				_character.Wardrobe.Add(new Clothing());
			}
		}

		private static bool UsedInStage(string tag, Character character, int stage)
		{
			return true;
		}

		private Case CreateWorkingCase(Behaviour behavior, string tag, int[] stages, params string[] lines)
		{
			Case c = new Case(tag);
			foreach (string line in lines)
			{
				c.Lines.Add(new DialogueLine("test", line));
			}
			c.Stages.AddRange(stages);
			behavior.AddWorkingCase(c);
			return c;
		}

		private Case CreateCase(Behaviour behavior, int stageId, string tag, params string[] lines)
		{
			while (behavior.Stages.Count <= stageId)
			{
				behavior.Stages.Add(new Stage(behavior.Stages.Count));
			}
			Stage stage = behavior.Stages[stageId];
			Case stageCase = new Case(tag);
			stage.Cases.Add(stageCase);
			foreach (string line in lines)
			{
				stageCase.Lines.Add(new DialogueLine(stageId + "-test.png", line));
			}
			return stageCase;
		}

		[TestMethod]
		public void SplitSharedCase()
		{
			Behaviour behavior = _character.Behavior;
			CreateCase(behavior, 1, "a", "a1");
			CreateCase(behavior, 2, "a", "a1");
			CreateCase(behavior, 3, "a", "a1");
			behavior.PrepareForEdit(_character);
			Assert.AreEqual(1, behavior.GetWorkingCases().Count());
			Assert.AreEqual(3, behavior.GetWorkingCases().ToList()[0].Stages.Count);
		}

		[TestMethod]
		public void SplitSharedCaseExtraLine()
		{
			Behaviour behavior = _character.Behavior;
			CreateCase(behavior, 1, "a", "a1", "a2");
			CreateCase(behavior, 2, "a", "a1", "a2");
			CreateCase(behavior, 3, "a", "a1");
			behavior.PrepareForEdit(_character);
			Assert.AreEqual(2, behavior.GetWorkingCases().Count());
		}

		[TestMethod]
		public void SplitSharedCaseSharedLines()
		{
			Behaviour behavior = _character.Behavior;
			CreateCase(behavior, 1, "a", "a1", "a2", "a3");
			CreateCase(behavior, 2, "a", "a1", "a2");
			CreateCase(behavior, 3, "a", "a1", "a2");
			behavior.PrepareForEdit(_character);
			Assert.AreEqual(2, behavior.GetWorkingCases().Count());
		}

		[TestMethod]
		public void TreatsConditionsAsAlternativeCases()
		{
			Behaviour behavior = _character.Behavior;
			CreateCase(behavior, 0, "a", "a1", "a2");
			Case conditioned = CreateCase(behavior, 0, "a", "a1", "a2");
			conditioned.Filter = "x";
			behavior.PrepareForEdit(_character);
			Assert.AreEqual(1, behavior.GetWorkingCases().Count());
			Case first = behavior.GetWorkingCases().First();
			Assert.AreEqual(2, first.AlternativeConditions.Count);
		}

		[TestMethod]
		public void SplitIntegration()
		{
			Behaviour behavior = _character.Behavior;
			CreateCase(behavior, 1, "a", "a1", "a2");
			CreateCase(behavior, 1, "b", "b1");
			CreateCase(behavior, 1, "c", "b1");
			CreateCase(behavior, 2, "a", "a1", "a2", "a3");
			CreateCase(behavior, 2, "b", "b2", "b3");
			CreateCase(behavior, 3, "a", "a1", "a2");
			CreateCase(behavior, 3, "b", "b1", "b2");
			behavior.PrepareForEdit(_character);
			Assert.AreEqual(6, behavior.GetWorkingCases().Count());
		}

		[TestMethod]
		public void ReplaceReplaces()
		{
			Behaviour behavior = _character.Behavior;
			behavior.PrepareForEdit(_character);
			CreateWorkingCase(behavior, "a", new int[] { 0, 1, 2 }, "a1");
			CreateWorkingCase(behavior, "b", new int[] { 0, 1, 2 }, "c");
			HashSet<string> dest = new HashSet<string>();
			dest.Add("b");
			behavior.BulkReplace("a", dest);
			Assert.AreEqual(2, behavior.GetWorkingCases().Count());
			Assert.AreEqual("a1", behavior.GetWorkingCases().ToList()[1].Lines[0].Text);
		}

		[TestMethod]
		public void ReplaceReplacesMultiple()
		{
			Behaviour behavior = _character.Behavior;
			behavior.PrepareForEdit(_character);
			CreateWorkingCase(behavior, "a", new int[] { 0, 1, 2 }, "a1");
			CreateWorkingCase(behavior, "b", new int[] { 0, 1, 2 }, "c");
			HashSet<string> dest = new HashSet<string>();
			dest.Add("b");
			dest.Add("c");
			dest.Add("d");
			behavior.BulkReplace("a", dest);
			Assert.AreEqual(4, behavior.GetWorkingCases().Count());
			Assert.AreEqual("a1", behavior.GetWorkingCases().ToList()[1].Lines[0].Text);
			Assert.AreEqual("a1", behavior.GetWorkingCases().ToList()[2].Lines[0].Text);
			Assert.AreEqual("a1", behavior.GetWorkingCases().ToList()[3].Lines[0].Text);
		}

		[TestMethod]
		public void ReplaceIgnoresConditions()
		{
			Behaviour behavior = _character.Behavior;
			behavior.PrepareForEdit(_character);
			CreateWorkingCase(behavior, "a", new int[] { 0, 1, 2 }, "a1");
			Case c = CreateWorkingCase(behavior, "b", new int[] { 0, 1, 2 }, "c");
			c.Filter = "filter";
			HashSet<string> dest = new HashSet<string>();
			dest.Add("b");
			behavior.BulkReplace("a", dest);
			Assert.AreEqual(3, behavior.GetWorkingCases().Count());
			Assert.AreEqual("c", behavior.GetWorkingCases().ToList()[1].Lines[0].Text);
		}

		public void Dispose()
		{
			_character.Dispose();
		}
	}
}
